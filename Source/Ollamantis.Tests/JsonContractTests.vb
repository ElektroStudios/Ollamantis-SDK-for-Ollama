Option Strict On
Option Explicit On
Option Infer Off

Imports System.Globalization
Imports System.Text.Json
Imports Ollamantis.Contracts
Imports Ollamantis.Entities
Imports Xunit

Namespace Ollamantis.Tests.Contracts

    ''' <summary>Verifies JSON representations of the option types used by Ollama.</summary>
    Public NotInheritable Class JsonContractTests

        ''' <summary>Verifies the boolean and named thinking options.</summary>
        <Theory>
        <InlineData("true")>
        <InlineData("false")>
        <InlineData("""low""")>
        <InlineData("""medium""")>
        <InlineData("""high""")>
        <InlineData("""max""")>
        Public Sub ThinkOptionPreservesItsJsonType(json As String)
            Dim value As ThinkOption = JsonSerializer.Deserialize(Of ThinkOption)(json)
            Assert.Equal(json, JsonSerializer.Serialize(value))
        End Sub

        ''' <summary>Verifies numeric and string input for keep-alive values.</summary>
        <Theory>
        <InlineData("-1", "-1m")>
        <InlineData("0", "0s")>
        <InlineData("42", "42s")>
        <InlineData("3600", "3600s")>
        <InlineData("""5m""", "5m")>
        <InlineData(""" 30S """, "30s")>
        <InlineData("""1h""", "1h")>
        Public Sub KeepAliveNormalizesDurations(json As String, expected As String)
            Dim value As KeepAliveOption = JsonSerializer.Deserialize(Of KeepAliveOption)(json)
            Assert.Equal(expected, CStr(value))
            Assert.Equal(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(value))
        End Sub

        ''' <summary>Verifies normal roles and normalization of case and whitespace.</summary>
        <Theory>
        <InlineData("user", "user")>
        <InlineData(" SYSTEM ", "system")>
        <InlineData("Assistant", "assistant")>
        <InlineData("tool", "tool")>
        Public Sub RoleOptionWritesTheNormalizedRole(input As String, expected As String)
            Dim role As RoleOption = JsonSerializer.Deserialize(Of RoleOption)(JsonSerializer.Serialize(input))
            Assert.Equal(expected, CStr(role))
            Assert.Equal(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(role))
        End Sub

        ''' <summary>Verifies that JSON schemas are sent as objects and simple JSON mode as a string.</summary>
        <Theory>
        <InlineData("""json""")>
        <InlineData("{""type"":""object"",""properties"":{""count"":{""type"":""integer""}}}")>
        <InlineData("{""type"":""array"",""items"":{""type"":""string""}}")>
        Public Sub FormatOptionPreservesTheSchema(json As String)
            Dim format As FormatOption = JsonSerializer.Deserialize(Of FormatOption)(json)
            Using expected As JsonDocument = JsonDocument.Parse(json),
                  actual As JsonDocument = JsonDocument.Parse(JsonSerializer.Serialize(format))
                Assert.True(JsonElement.DeepEquals(expected.RootElement, actual.RootElement))
            End Using
        End Sub

        ''' <summary>Verifies that image options round-trip as base64 JSON strings.</summary>
        <Theory>
        <InlineData("AQ==")>
        <InlineData("AAEC")>
        <InlineData("/+4=")>
        Public Sub ImageOptionPreservesBase64(base64 As String)
            Dim image As ImageOption = ImageOption.FromBase64(base64)
            Dim json As String = JsonSerializer.Serialize(image)
            Assert.Equal(JsonSerializer.Serialize(base64), json)
            Assert.Equal(base64, JsonSerializer.Deserialize(Of ImageOption)(json).Base64Data)
        End Sub

        ''' <summary>Verifies that optional generation fields are omitted and explicit zero values are preserved.</summary>
        <Theory>
        <InlineData("en-US")>
        <InlineData("es-ES")>
        <InlineData("fr-FR")>
        Public Sub GenerationOptionsUseOllamaNamesAndInvariantNumbers(culture As String)
            Dim options As New GenerationOptions With {
                .Temperature = 0.25R,
                .ContextSize = 2048,
                .Seed = 0,
                .StopSequences = New String() {"END", "STOP"}
            }

            Using document As JsonDocument = JsonDocument.Parse(options.ToString(False, CultureInfo.GetCultureInfo(culture)))
                Dim root As JsonElement = document.RootElement
                Assert.Equal(0.25R, root.GetProperty("temperature").GetDouble())
                Assert.Equal(2048, root.GetProperty("num_ctx").GetInt32())
                Assert.Equal(0, root.GetProperty("seed").GetInt32())
                Assert.Equal("END", root.GetProperty("stop")(0).GetString())
                Assert.Equal("STOP", root.GetProperty("stop")(1).GetString())
                Dim unused As JsonElement
                Assert.False(root.TryGetProperty("top_p", unused))
            End Using
        End Sub

    End Class

End Namespace
