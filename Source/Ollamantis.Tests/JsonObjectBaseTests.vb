#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Globalization
Imports System.Threading

Imports Ollamantis.Contracts
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

        ''' <summary>Verifies that the SDK deserializes a model size above the 32-bit integer range.</summary>
        <Fact>
        Public Async Function OllamaClientDeserializesLargeModelSizes() As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient()
                handler.Enqueue("{""models"":[{""name"":""llama3"",""size"":4000000000}]}")
                Dim response As LocalModelsResponse =
                    Await client.Management.ListLocalModelsAsync(CancellationToken.None).ConfigureAwait(False)

                Assert.True(response.IsSuccessful)
                Assert.Equal(4000000000L, Assert.Single(response.Models).FileSize)
            End Using
        End Function

#End Region

    End Class

End Namespace
