#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

Imports Xunit
Imports Ollamantis.Contracts

Namespace Ollamantis.Tests.Contracts

    ''' <summary>
    ''' Contains unit tests for the <see cref="ResponseBase"/> class, specifically targeting the metadata hydration logic.
    ''' </summary>
    Public Class ResponseBaseTests

#Region " Helper Classes "

        ''' <summary>
        ''' A minimal concrete implementation of the abstract <see cref="ResponseBase"/> class for testing purposes.
        ''' </summary>
        Private Class DummyResponse : Inherits ResponseBase
            ' No additional properties needed; we just need to access HydrateMetadata.
        End Class

#End Region

#Region " Tests "

        <Fact>
        Public Sub HydrateMetadata_WhenHttpIsSuccessAndJsonIsValid_ShouldMaintainSuccess()
            ' Arrange
            Dim response As New DummyResponse()
            Dim validJson As String = "{ ""model"": ""llama3"", ""created_at"": ""2026-08-18T14:00:00Z"" }"

            ' Act
            response.HydrateMetadata(isSuccessful:=True, statusCode:=200, reasonPhrase:="OK", rawJson:=validJson)

            ' Assert
            Assert.True(response.IsSuccessful, "The response should remain successful when no error is found in the JSON.")
            Assert.Equal(200, response.StatusCode)
            Assert.Null(response.ErrorMessage)
        End Sub

        <Fact>
        Public Sub HydrateMetadata_WhenHttp200ButJsonContainsError_ShouldForceFailure()
            ' Arrange: This is the classic Ollama NDJSON stream trap.
            Dim response As New DummyResponse()
            Dim deceptiveJson As String = "{ ""error"": ""model 'llama3' not found, try pulling it first"" }"

            ' Act
            response.HydrateMetadata(isSuccessful:=True, statusCode:=200, reasonPhrase:="OK", rawJson:=deceptiveJson)

            ' Assert
            Assert.False(response.IsSuccessful, "The response MUST be marked as failed if the JSON payload contains an 'error' property, regardless of HTTP 200.")
            Assert.Equal("model 'llama3' not found, try pulling it first", response.ErrorMessage)
        End Sub

        <Fact>
        Public Sub HydrateMetadata_WhenMalformedJsonAndHttpFails_ShouldFallbackToRawMessage()
            ' Arrange: Server crashes or proxy intercepts, returning HTML or garbage instead of JSON.
            Dim response As New DummyResponse()
            Dim garbagePayload As String = "<html><body>502 Bad Gateway</body></html>"

            ' Act
            response.HydrateMetadata(isSuccessful:=False, statusCode:=502, reasonPhrase:="Bad Gateway", rawJson:=garbagePayload)

            ' Assert
            Assert.False(response.IsSuccessful)
            ' Since it doesn't start with '{', it skips JSON parsing and yields the clean HTTP error string
            Assert.Equal("HTTP 502 (Bad Gateway).", response.ErrorMessage)
        End Sub

        <Fact>
        Public Sub HydrateMetadata_WhenEmptyPayloadAndHttpFails_ShouldFallbackToStandardMessage()
            ' Arrange: Standard HTTP error with absolutely no body content.
            Dim response As New DummyResponse()
            Dim emptyPayload As String = "   "

            ' Act
            response.HydrateMetadata(isSuccessful:=False, statusCode:=404, reasonPhrase:="Not Found", rawJson:=emptyPayload)

            ' Assert
            Assert.False(response.IsSuccessful)
            ' Check that the ElseIf block handled the lack of JSON properly
            Assert.Equal("HTTP 404 (Not Found).", response.ErrorMessage)
        End Sub

#End Region

    End Class

End Namespace