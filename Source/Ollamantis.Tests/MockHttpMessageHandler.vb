Option Strict On
Option Explicit On
Option Infer Off

Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Threading
Imports Ollamantis.Core

Namespace Ollamantis.Tests.Mocks

    ''' <summary>Records requests and serves a deterministic sequence of HTTP responses.</summary>
    Friend NotInheritable Class MockHttpMessageHandler
        Inherits HttpMessageHandler

        ''' <summary>Stores response factories in the order in which they were queued.</summary>
        Private ReadOnly responses As New Queue(Of Func(Of CancellationToken, Task(Of HttpResponseMessage)))()

        ''' <summary>Gets the recorded HTTP methods, URLs, content types and bodies.</summary>
        Friend ReadOnly Property Requests As New List(Of (Method As HttpMethod, Url As Uri, MediaType As String, Body As String))()

        ''' <summary>Creates a client configured to send requests through this handler.</summary>
        Friend Function CreateClient(Optional host As String = "http://ollama.test:11434/") As OllamaClient
            Dim client As New OllamaClient(host)
            client.HttpClient.Dispose()
            client.HttpClient = New HttpClient(Me)
            Return client
        End Function

        ''' <summary>Queues a response with the specified status and body.</summary>
        Friend Sub Enqueue(body As String, Optional statusCode As HttpStatusCode = HttpStatusCode.OK)
            Me.Enqueue(Function(token As CancellationToken) As Task(Of HttpResponseMessage)
                           Return Task.FromResult(New HttpResponseMessage(statusCode) With {
                               .Content = New StringContent(body, Encoding.UTF8, "application/json")
                           })
                       End Function)
        End Sub

        ''' <summary>Queues a response factory that can observe cancellation or fail the request.</summary>
        Friend Sub Enqueue(factory As Func(Of CancellationToken, Task(Of HttpResponseMessage)))
            SyncLock Me.responses
                Me.responses.Enqueue(factory)
            End SyncLock
        End Sub

        ''' <summary>Captures the request before returning its queued response.</summary>
        Protected Overrides Async Function SendAsync(request As HttpRequestMessage,
                                                     cancellationToken As CancellationToken) As Task(Of HttpResponseMessage)
            Dim body As String = Nothing
            If request.Content IsNot Nothing Then
                body = Await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(False)
            End If

            Dim factory As Func(Of CancellationToken, Task(Of HttpResponseMessage))
            SyncLock Me.responses
                Me.Requests.Add((request.Method, request.RequestUri, request.Content?.Headers.ContentType?.MediaType, body))
                If Me.responses.Count = 0 Then
                    Throw New InvalidOperationException("An unexpected HTTP request was sent.")
                End If
                factory = Me.responses.Dequeue()
            End SyncLock

            Return Await factory.Invoke(cancellationToken).ConfigureAwait(False)
        End Function

    End Class

End Namespace
