Option Strict On
Option Explicit On
Option Infer Off

Imports System.Net
Imports System.Net.Http
Imports System.Text.Json
Imports System.Threading
Imports Ollamantis.Contracts
Imports Ollamantis.Core
Imports Ollamantis.Entities
Imports Ollamantis.Tests.Mocks
Imports Xunit

Namespace Ollamantis.Tests.Core

    ''' <summary>Verifies how chat sessions store and send conversation history.</summary>
    Public NotInheritable Class ChatSessionTests

        ''' <summary>Provides a successful assistant message for each test turn.</summary>
        Private Const Reply As String = "{""model"":""gemma"",""message"":{""role"":""assistant"",""content"":""reply""},""done"":true}"

        ''' <summary>Verifies that each conversation receives only its own prior turns.</summary>
        <Theory>
        <InlineData(False)>
        <InlineData(True)>
        Public Async Function KeepsHistoryByConversationId(stream As Boolean) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient()
                Dim session As New ChatSession(client)
                Dim firstId As Guid = Guid.NewGuid()
                Dim secondId As Guid = Guid.NewGuid()
                handler.Enqueue(Reply)
                handler.Enqueue(Reply)
                handler.Enqueue(Reply)

                Assert.True((Await SendTurnAsync(session, CreateRequest(firstId, "first"), stream).ConfigureAwait(False)).IsSuccessful)
                Assert.True((Await SendTurnAsync(session, CreateRequest(secondId, "other"), stream).ConfigureAwait(False)).IsSuccessful)
                Assert.True((Await SendTurnAsync(session, CreateRequest(firstId, "next"), stream).ConfigureAwait(False)).IsSuccessful)

                Assert.Equal(New String() {"first"}, GetMessageContents(handler.Requests(0).Body))
                Assert.Equal(New String() {"other"}, GetMessageContents(handler.Requests(1).Body))
                Assert.Equal(New String() {"first", "reply", "next"}, GetMessageContents(handler.Requests(2).Body))
            End Using
        End Function

        ''' <summary>Verifies that failed turns can be retried without adding duplicate or null messages.</summary>
        <Theory>
        <InlineData(False, "http")>
        <InlineData(True, "http")>
        <InlineData(False, "transport")>
        <InlineData(True, "transport")>
        <InlineData(False, "canceled")>
        <InlineData(True, "canceled")>
        Public Async Function AFailedTurnDoesNotPolluteHistory(stream As Boolean, failure As String) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient()
                Dim session As New ChatSession(client)
                Dim id As Guid = Guid.NewGuid()
                handler.Enqueue(Reply)
                Await SendTurnAsync(session, CreateRequest(id, "first"), stream).ConfigureAwait(False)

                Dim request As ChatCompletionRequest = CreateRequest(id, "retry")
                Select Case failure
                    Case "http"
                        handler.Enqueue("{""error"":""try again""}", HttpStatusCode.ServiceUnavailable)
                        Dim errorResult As ChatCompletionResponse = Await SendTurnAsync(session, request, stream).ConfigureAwait(False)
                        Assert.False(errorResult.IsSuccessful)
                        Assert.Equal("try again", errorResult.ErrorMessage)
                    Case "transport"
                        handler.Enqueue(Function(token As CancellationToken) Task.FromException(Of HttpResponseMessage)(
                                            New HttpRequestException("Connection closed.")))
                        Await Assert.ThrowsAsync(Of HttpRequestException)(
                            Function() SendTurnAsync(session, request, stream)).ConfigureAwait(False)
                    Case "canceled"
                        handler.Enqueue(Function(token As CancellationToken) Task.FromCanceled(Of HttpResponseMessage)(
                                            New CancellationToken(canceled:=True)))
                        Await Assert.ThrowsAnyAsync(Of OperationCanceledException)(
                            Function() SendTurnAsync(session, request, stream)).ConfigureAwait(False)
                End Select

                handler.Enqueue(Reply)
                Assert.True((Await SendTurnAsync(session, request, stream).ConfigureAwait(False)).IsSuccessful)

                Assert.Equal(New String() {"first", "reply", "retry"}, GetMessageContents(handler.Requests(1).Body))
                Assert.Equal(New String() {"first", "reply", "retry"}, GetMessageContents(handler.Requests(2).Body))
                Assert.Equal("retry", Assert.Single(request.Messages).Content)
            End Using
        End Function

        ''' <summary>Verifies that a streamed turn can be retried after it fails following a partial response.</summary>
        <Theory>
        <InlineData(False)>
        <InlineData(True)>
        Public Async Function APartialStreamDoesNotPolluteHistory(callbackThrows As Boolean) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient()
                Dim session As New ChatSession(client)
                Dim id As Guid = Guid.NewGuid()
                handler.Enqueue(Reply)
                Assert.True((Await SendTurnAsync(session, CreateRequest(id, "first"), stream:=True).
                             ConfigureAwait(False)).IsSuccessful)

                Dim request As ChatCompletionRequest = CreateRequest(id, "retry")
                Const partialChunk As String = "{""message"":{""role"":""assistant"",""content"":""partial""}}"
                If callbackThrows Then
                    handler.Enqueue(partialChunk & vbLf & Reply)
                    Dim expected As New InvalidOperationException("The callback failed.")
                    Dim actual As InvalidOperationException = Await Assert.ThrowsAsync(Of InvalidOperationException)(
                        Function() session.StreamChatCompletionAsync(request,
                            Sub(chunk As ChatCompletionResponse)
                                Assert.Equal("partial", chunk.Message.Content)
                                Throw expected
                            End Sub, CancellationToken.None)).ConfigureAwait(False)
                    Assert.Same(expected, actual)
                Else
                    handler.Enqueue(partialChunk & vbLf & "{""error"":""generation stopped""}")
                    Dim chunks As New List(Of ChatCompletionResponse)()
                    Dim failure As ChatCompletionResponse = Await session.StreamChatCompletionAsync(
                        request, AddressOf chunks.Add, CancellationToken.None).ConfigureAwait(False)
                    Assert.Equal("partial", Assert.Single(chunks).Message.Content)
                    Assert.False(failure.IsSuccessful)
                    Assert.Equal(200, failure.StatusCode)
                    Assert.Equal("generation stopped", failure.ErrorMessage)
                End If

                handler.Enqueue(Reply)
                Assert.True((Await SendTurnAsync(session, request, stream:=True).
                             WaitAsync(TimeSpan.FromSeconds(5)).ConfigureAwait(False)).IsSuccessful)
                Assert.Equal(3, handler.Requests.Count)
                Assert.Equal(New String() {"first", "reply", "retry"}, GetMessageContents(handler.Requests(1).Body))
                Assert.Equal(New String() {"first", "reply", "retry"}, GetMessageContents(handler.Requests(2).Body))
                Assert.Equal("retry", Assert.Single(request.Messages).Content)
            End Using
        End Function

        ''' <summary>Verifies that overlapping turns for the same conversation use committed history.</summary>
        <Theory>
        <InlineData(False)>
        <InlineData(True)>
        Public Async Function OverlappingTurnsWaitForTheirConversation(stream As Boolean) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient()
                Dim session As New ChatSession(client)
                Dim id As Guid = Guid.NewGuid()
                Dim firstResponse As New TaskCompletionSource(Of HttpResponseMessage)(TaskCreationOptions.RunContinuationsAsynchronously)
                Dim firstRequestSent As New TaskCompletionSource(Of Boolean)(TaskCreationOptions.RunContinuationsAsynchronously)
                handler.Enqueue(Function(token As CancellationToken) As Task(Of HttpResponseMessage)
                                    firstRequestSent.SetResult(True)
                                    Return firstResponse.Task
                                End Function)
                handler.Enqueue(Reply)
                handler.Enqueue(Reply)

                Dim firstTurn As Task(Of ChatCompletionResponse) = SendTurnAsync(session, CreateRequest(id, "first"), stream)
                Await firstRequestSent.Task.WaitAsync(TimeSpan.FromSeconds(5)).ConfigureAwait(False)
                Dim secondTurn As Task(Of ChatCompletionResponse) = SendTurnAsync(session, CreateRequest(id, "second"), stream)
                Try
                    Await SendTurnAsync(session, CreateRequest(Guid.NewGuid(), "independent"), stream).
                        WaitAsync(TimeSpan.FromSeconds(5)).ConfigureAwait(False)
                    Assert.False(secondTurn.IsCompleted)
                Finally
                    firstResponse.SetResult(New HttpResponseMessage(HttpStatusCode.OK) With {.Content = New StringContent(Reply)})
                End Try

                Await Task.WhenAll(firstTurn, secondTurn).WaitAsync(TimeSpan.FromSeconds(5)).ConfigureAwait(False)
                Assert.Equal(New String() {"first"}, GetMessageContents(handler.Requests(0).Body))
                Assert.Equal(New String() {"independent"}, GetMessageContents(handler.Requests(1).Body))
                Assert.Equal(New String() {"first", "reply", "second"}, GetMessageContents(handler.Requests(2).Body))
            End Using
        End Function

        ''' <summary>Builds a request containing the new user message for one turn.</summary>
        Private Shared Function CreateRequest(id As Guid, content As String) As ChatCompletionRequest
            Return New ChatCompletionRequest(id) With {
                .Model = "gemma",
                .Messages = New List(Of ChatMessage) From {New ChatMessage("user", content)}
            }
        End Function

        ''' <summary>Sends a chat turn through the streaming or single-response API.</summary>
        Private Shared Function SendTurnAsync(session As ChatSession, request As ChatCompletionRequest,
                                              stream As Boolean) As Task(Of ChatCompletionResponse)
            If stream Then
                Return session.StreamChatCompletionAsync(request, Sub(chunk As ChatCompletionResponse)
                                                                  End Sub, CancellationToken.None)
            End If

            Return session.GenerateChatCompletionAsync(request, CancellationToken.None)
        End Function

        ''' <summary>Extracts the ordered message contents from the actual request body.</summary>
        Private Shared Function GetMessageContents(body As String) As String()
            Using document As JsonDocument = JsonDocument.Parse(body)
                Dim contents As New List(Of String)()
                For Each message As JsonElement In document.RootElement.GetProperty("messages").EnumerateArray()
                    contents.Add(If(message.ValueKind = JsonValueKind.Null, Nothing, message.GetProperty("content").GetString()))
                Next
                Return contents.ToArray()
            End Using
        End Function

    End Class

End Namespace
