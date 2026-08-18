#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Threading

#End Region

Namespace Ollamantis.Tests.Mocks

    ''' <summary>
    ''' A mock HTTP handler to intercept requests and return predefined JSON responses,
    ''' avoiding real network calls during unit testing.
    ''' </summary>
    Public NotInheritable Class MockHttpMessageHandler : Inherits HttpMessageHandler

        Private ReadOnly ResponseJson As String
        Private ReadOnly StatusCode As HttpStatusCode

        ''' <summary>
        ''' Initializes a new instance of the <see cref="MockHttpMessageHandler"/> class.
        ''' </summary>
        Public Sub New(responseJson As String,
              Optional statusCode As HttpStatusCode = HttpStatusCode.OK)

            Me.ResponseJson = responseJson
            Me.StatusCode = statusCode
        End Sub

        ''' <summary>
        ''' Intercepts the HTTP request and returns the fake response asynchronously.
        ''' </summary>
        Protected Overrides Function SendAsync(request As HttpRequestMessage,
                                               cancellationToken As CancellationToken) As Task(Of HttpResponseMessage)

            Dim response As New HttpResponseMessage(Me.StatusCode) With {
                .Content = New StringContent(Me.ResponseJson, Encoding.UTF8, "application/json")
            }

            Return Task.FromResult(response)
        End Function

    End Class

End Namespace