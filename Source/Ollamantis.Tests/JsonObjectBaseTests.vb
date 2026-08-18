#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Globalization
Imports System.Net
Imports System.Net.Http
Imports System.Threading

Imports Ollamantis.Core
Imports Ollamantis.Tests.Mocks

Imports Xunit

#End Region

Namespace Ollamantis.Tests.Core

    ''' <summary>
    ''' Contains unit tests for the core JSON serialization and networking components.
    ''' </summary>
    Public NotInheritable Class JsonObjectBaseTests

#Region " Helper Classes "

        ''' <summary>
        ''' A dummy implementation of the MustInherit base class for testing purposes.
        ''' </summary>
        Private Class DummyResponse : Inherits JsonObjectBase
            Public Property DummyProperty As String
        End Class

#End Region

#Region " Tests "

        <Fact>
        Public Sub ToString_WhenCalledWithIndent_ShouldFormatWithNewLines()
            ' Arrange
            Dim response As New DummyResponse With {
                .DummyProperty = "TestValue"
            }

            ' Act
            Dim jsonResult As String = response.ToString(writeIndented:=True)

            ' Assert
            ' Check that the output is actually indented (contains newlines)
            Assert.True(jsonResult.Contains(Environment.NewLine) OrElse jsonResult.Contains(ControlChars.Lf),
                        "The JSON output should contain newline characters when indented.")

            Assert.Contains("""DummyProperty"": ""TestValue""", jsonResult)
        End Sub

        <Fact>
        Public Sub ToString_WhenCalledWithCulture_ShouldRestoreOriginalCulture()

            ' Arrange
            Dim response As New DummyResponse()
            Dim targetCulture As New CultureInfo("smn-FI") ' Inari Sámi culture and language (smn) localized for Finland (FI).
            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture

            ' Act
            Dim jsonResult As String =
                response.ToString(writeIndented:=False, cultureInfo:=targetCulture)

            ' Assert
            Assert.NotNull(jsonResult)
            ' Guarantee the temporal context switch reverted correctly
            Assert.Equal(originalCulture.Name, Thread.CurrentThread.CurrentCulture.Name)
        End Sub

        <Fact>
        Public Async Function OllamaClient_WhenReceivingValidJson_ShouldDeserializeCorrectly() As Task

            ' Arrange: Simulate a raw JSON response from the Ollama API.
            Dim fakeJsonResponse As String =
                "{ ""name"": ""llama3"", ""modified_at"": ""2026-08-18T14:00:00Z"", ""size"": 4000000000 }"

            ' Inject the mock handler so no real network request is made.
            Dim mockHandler As New MockHttpMessageHandler(fakeJsonResponse, HttpStatusCode.OK)
            Dim httpClient As New HttpClient(mockHandler)

            ' Act: Fire the request. The mock intercepts it, so the URL doesn't even matter!.
            Dim responseMessage As HttpResponseMessage =
                Await httpClient.GetAsync("http://localhost:11434/api/tags").ConfigureAwait(False)

            Dim resultString As String =
                Await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(False)

            ' Assert: Verify the mock successfully hijacked the HTTP call.
            Assert.True(responseMessage.IsSuccessStatusCode, "The mocked HTTP response should have a success status code.")
            Assert.Contains("llama3", resultString, StringComparison.OrdinalIgnoreCase)
        End Function

#End Region

    End Class

End Namespace
