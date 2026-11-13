Option Strict On
Option Explicit On
Option Infer Off

Imports System.Net
Imports System.Text.Json
Imports System.Threading
Imports Ollamantis.Contracts
Imports Ollamantis.Core
Imports Ollamantis.Entities
Imports Ollamantis.Tests.Mocks
Imports Xunit

Namespace Ollamantis.Tests.Core

    ''' <summary>Exercises the public NDJSON endpoints with controlled HTTP responses.</summary>
    Public NotInheritable Class StreamingTests

        ''' <summary>Verifies that text fragments and final generation metadata are retained.</summary>
        <Theory>
        <InlineData(vbLf)>
        <InlineData(vbCrLf)>
        Public Async Function CompletionCombinesFragments(separator As String) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient()
                handler.Enqueue(String.Join(separator,
                                             "{""response"":""Hello""}",
                                             "",
                                             "  ",
                                             "{""response"":"" world""}",
                                             "{""model"":""gemma"",""done"":true,""done_reason"":""stop"",""eval_count"":4}"))
                Dim chunks As New List(Of String)()
                Dim result As CompletionResponse =
                    Await client.Generation.StreamCompletionAsync(
                        New CompletionRequest("gemma", "Hi"),
                        Sub(chunk As CompletionResponse) chunks.Add(chunk.Response),
                        CancellationToken.None).ConfigureAwait(False)

                Assert.Equal(3, chunks.Count)
                Assert.Equal("Hello", chunks(0))
                Assert.Equal(" world", chunks(1))
                Assert.True(result.IsSuccessful)
                Assert.Equal(200, result.StatusCode)
                Assert.Equal("Hello world", result.Response)
                Assert.True(result.Done)
                Assert.Equal("stop", result.DoneReason)
                Assert.Equal(4, result.EvalCount)
                Using document As JsonDocument = JsonDocument.Parse(Assert.Single(handler.Requests).Body)
                    Assert.True(document.RootElement.GetProperty("stream").GetBoolean())
                End Using
            End Using
        End Function

        ''' <summary>Verifies that thinking and content are assembled independently.</summary>
        <Fact>
        Public Async Function ChatCombinesThinkingAndContent() As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient()
                handler.Enqueue(String.Join(vbLf,
                                             "{""message"":{""role"":""assistant"",""thinking"":""Think""}}",
                                             "{""message"":{""role"":""assistant"",""thinking"":"" twice"",""content"":""Hel""}}",
                                             "{""message"":{""role"":""assistant"",""content"":""lo""},""done"":true}"))
                Dim chunks As New List(Of ChatCompletionResponse)()
                Dim result As ChatCompletionResponse =
                    Await client.Generation.StreamChatCompletionAsync(
                        New ChatCompletionRequest("gemma", New List(Of ChatMessage) From {New ChatMessage("user", "Hi")}),
                        AddressOf chunks.Add, CancellationToken.None).ConfigureAwait(False)

                Assert.Equal(3, chunks.Count)
                Assert.True(result.IsSuccessful)
                Assert.True(result.Done)
                Assert.Equal("assistant", CStr(result.Message.Role))
                Assert.Equal("Think twice", result.Message.Thinking)
                Assert.Equal("Hello", result.Message.Content)
            End Using
        End Function

        ''' <summary>Verifies that repeated progress reports replace each layer's previous sizes.</summary>
        <Theory>
        <InlineData("pull")>
        <InlineData("push")>
        Public Async Function ModelTransferTotalsEachDigestOnce(endpoint As String) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient()
                handler.Enqueue(String.Join(vbLf,
                                             "{""status"":""transfer"",""digest"":""sha256:a"",""total"":4000000000,""completed"":5}",
                                             "{""status"":""transfer"",""digest"":""sha256:b"",""total"":20,""completed"":20}",
                                             "{""status"":""transfer"",""digest"":""sha256:a"",""completed"":4000000000}",
                                             "{""status"":""success""}"))
                Dim statuses As New List(Of String)()
                Dim result As ModelTransferResponseBase
                If endpoint = "pull" Then
                    result = Await client.Management.StreamPullModelAsync(New PullModelRequest("gemma"),
                        Sub(chunk As PullModelResponse) statuses.Add(chunk.Status), CancellationToken.None).ConfigureAwait(False)
                Else
                    result = Await client.Management.StreamPushModelAsync(New PushModelRequest("gemma"),
                        Sub(chunk As PushModelResponse) statuses.Add(chunk.Status), CancellationToken.None).ConfigureAwait(False)
                End If

                Assert.True(result.IsSuccessful)
                Assert.Equal("success", result.Status)
                Assert.Equal(4000000020L, result.TotalSize)
                Assert.Equal(4000000020L, result.CompletedSize)
                Assert.Equal(New String() {"transfer", "transfer", "transfer", "success"}, statuses)
                Dim sent As (Method As Net.Http.HttpMethod, Url As Uri, MediaType As String, Body As String) = Assert.Single(handler.Requests)
                Assert.Equal("/api/" & endpoint, sent.Url.AbsolutePath)
                Using document As JsonDocument = JsonDocument.Parse(sent.Body)
                    Assert.True(document.RootElement.GetProperty("stream").GetBoolean())
                End Using
            End Using
        End Function

        ''' <summary>Verifies that a stream error stops callbacks and reaches the caller.</summary>
        <Theory>
        <InlineData(200, True)>
        <InlineData(400, False)>
        <InlineData(404, False)>
        <InlineData(500, False)>
        Public Async Function StreamErrorsAreReturnedWithHttpMetadata(statusCode As Integer, embedded As Boolean) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient()
                Const errorBody As String = "{""error"":""model unavailable""}"
                Dim body As String = If(embedded, String.Join(vbLf, "{""response"":""partial""}",
                                            errorBody, "{""response"":""ignored""}"), errorBody)
                handler.Enqueue(body, CType(statusCode, HttpStatusCode))
                Dim chunks As New List(Of CompletionResponse)()
                Dim result As CompletionResponse =
                    Await client.Generation.StreamCompletionAsync(New CompletionRequest("gemma", "Hi"),
                        AddressOf chunks.Add, CancellationToken.None).ConfigureAwait(False)

                Assert.Equal(If(embedded, 1, 0), chunks.Count)
                Assert.False(result.IsSuccessful)
                Assert.Equal(statusCode, result.StatusCode)
                Assert.Equal("model unavailable", result.ErrorMessage)
            End Using
        End Function

        ''' <summary>Verifies that a stream with no objects fails deterministically.</summary>
        <Theory>
        <InlineData("")>
        <InlineData(" ")>
        <InlineData(vbCrLf)>
        Public Async Function EmptyCompletionStreamIsRejected(body As String) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient()
                handler.Enqueue(body)
                Dim chunks As New List(Of CompletionResponse)()
                Await Assert.ThrowsAsync(Of InvalidOperationException)(
                    Function() client.Generation.StreamCompletionAsync(New CompletionRequest("gemma", "Hi"),
                        AddressOf chunks.Add, CancellationToken.None)).ConfigureAwait(False)
                Assert.Empty(chunks)
            End Using
        End Function

        ''' <summary>Verifies that canceling from a callback stops subsequent delivery.</summary>
        <Fact>
        Public Async Function CancellationStopsCompletionCallbacks() As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient(),
                  cancellation As New CancellationTokenSource()
                handler.Enqueue(String.Join(vbLf, "{""response"":""first""}", "{""response"":""second""}"))
                Dim chunks As New List(Of String)()
                Await Assert.ThrowsAnyAsync(Of OperationCanceledException)(
                    Function() client.Generation.StreamCompletionAsync(New CompletionRequest("gemma", "Hi"),
                        Sub(chunk As CompletionResponse)
                            chunks.Add(chunk.Response)
                            cancellation.Cancel()
                        End Sub, cancellation.Token)).ConfigureAwait(False)
                Assert.Equal("first", Assert.Single(chunks))
            End Using
        End Function

        ''' <summary>Verifies that a missing callback is rejected before contacting the server.</summary>
        <Theory>
        <InlineData(False)>
        <InlineData(True)>
        Public Async Function MissingCallbackIsRejectedBeforeSending(chat As Boolean) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient()
                Dim exception As ArgumentNullException
                If chat Then
                    exception = Await Assert.ThrowsAsync(Of ArgumentNullException)(
                        Function() client.Generation.StreamChatCompletionAsync(New ChatCompletionRequest("gemma"),
                            Nothing, CancellationToken.None)).ConfigureAwait(False)
                Else
                    exception = Await Assert.ThrowsAsync(Of ArgumentNullException)(
                        Function() client.Generation.StreamCompletionAsync(New CompletionRequest("gemma", "Hi"),
                            Nothing, CancellationToken.None)).ConfigureAwait(False)
                End If

                Assert.Equal("onChunkReceived", exception.ParamName)
                Assert.Empty(handler.Requests)
            End Using
        End Function

        ''' <summary>Verifies that the final message retains tool calls from earlier chunks.</summary>
        <Fact>
        Public Async Function ChatKeepsToolCallsFromEarlierChunks() As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient()
                handler.Enqueue(String.Join(vbLf,
                    "{""message"":{""role"":""assistant"",""tool_calls"":[{""function"":{""name"":""lookup"",""arguments"":{""id"":1}}}]}}",
                    "{""message"":{""role"":""assistant"",""tool_calls"":[{""function"":{""name"":""lookup"",""arguments"":{""id"":2}}}]}}",
                    "{""message"":{""role"":""assistant"",""content"":""""},""done"":true}"))

                Dim chunks As New List(Of ChatCompletionResponse)()
                Dim result As ChatCompletionResponse =
                    Await client.Generation.StreamChatCompletionAsync(New ChatCompletionRequest("gemma"),
                        AddressOf chunks.Add, CancellationToken.None).ConfigureAwait(False)

                Assert.True(result.IsSuccessful)
                Assert.Equal(3, chunks.Count)
                Assert.Equal(2, result.Message.ToolCalls.Count)
                Assert.All(result.Message.ToolCalls, Sub(callItem As ToolCall)
                                                        Assert.Equal("lookup", callItem.FunctionCall.Name)
                                                    End Sub)
                Assert.Equal(1, result.Message.ToolCalls(0).FunctionCall.Arguments.GetProperty("id").GetInt32())
                Assert.Equal(2, result.Message.ToolCalls(1).FunctionCall.Arguments.GetProperty("id").GetInt32())
            End Using
        End Function

    End Class

End Namespace
