Option Strict On
Option Explicit On
Option Infer Off

Imports System.Net
Imports System.Threading
Imports Ollamantis.Contracts
Imports Ollamantis.Core
Imports Ollamantis.Tests.Mocks
Imports Xunit

Namespace Ollamantis.Tests.Core

    ''' <summary>Exercises error responses and argument validation at the public API boundary.</summary>
    Public NotInheritable Class HttpFailureTests

        ''' <summary>Verifies that the API error body and HTTP status are available to callers.</summary>
        <Theory>
        <InlineData(400)>
        <InlineData(401)>
        <InlineData(403)>
        <InlineData(404)>
        <InlineData(408)>
        <InlineData(429)>
        <InlineData(500)>
        <InlineData(502)>
        <InlineData(503)>
        <InlineData(504)>
        Public Async Function ApiErrorPreservesTheStatusAndMessage(status As Integer) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient()
                handler.Enqueue("{""error"":""server rejected the request""}", CType(status, HttpStatusCode))
                Dim result As OllamaVersionResponse =
                    Await client.Management.GetOllamaVersionAsync(CancellationToken.None).ConfigureAwait(False)

                Assert.False(result.IsSuccessful)
                Assert.Equal(status, result.StatusCode)
                Assert.Equal("server rejected the request", result.ErrorMessage)
            End Using
        End Function

        ''' <summary>Verifies that invalid request arguments never reach the HTTP handler.</summary>
        <Theory>
        <InlineData("show")>
        <InlineData("copy")>
        <InlineData("delete")>
        <InlineData("push")>
        <InlineData("pull")>
        <InlineData("generate")>
        <InlineData("chat")>
        <InlineData("embed")>
        Public Async Function MissingRequestIsRejectedLocally(endpoint As String) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient()
                Dim action As Func(Of Task)
                Select Case endpoint
                    Case "show"
                        action = Function() client.Management.ShowModelAsync(Nothing, CancellationToken.None)
                    Case "copy"
                        action = Function() client.Management.CopyModelAsync(Nothing, CancellationToken.None)
                    Case "delete"
                        action = Function() client.Management.DeleteModelAsync(Nothing, CancellationToken.None)
                    Case "push"
                        action = Function() client.Management.PushModelAsync(Nothing, CancellationToken.None)
                    Case "pull"
                        action = Function() client.Management.PullModelAsync(Nothing, CancellationToken.None)
                    Case "generate"
                        action = Function() client.Generation.GenerateCompletionAsync(Nothing, CancellationToken.None)
                    Case "chat"
                        action = Function() client.Generation.GenerateChatCompletionAsync(Nothing, CancellationToken.None)
                    Case "embed"
                        action = Function() client.Generation.GenerateEmbeddingsAsync(Nothing, CancellationToken.None)
                    Case Else
                        Throw New ArgumentOutOfRangeException(NameOf(endpoint))
                End Select

                Dim exception As ArgumentNullException =
                    Await Assert.ThrowsAsync(Of ArgumentNullException)(action).ConfigureAwait(False)
                Assert.Equal("request", exception.ParamName)
                Assert.Empty(handler.Requests)
            End Using
        End Function

    End Class

End Namespace
