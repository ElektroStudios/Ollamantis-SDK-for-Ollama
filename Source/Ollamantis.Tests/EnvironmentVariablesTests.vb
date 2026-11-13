Option Strict On
Option Explicit On
Option Infer Off

Imports System.Text.Json
Imports Ollamantis.Core
Imports Xunit

Namespace Ollamantis.Tests.Core

    ''' <summary>Verifies the process-scoped Ollama environment configuration.</summary>
    Public NotInheritable Class EnvironmentVariablesTests

        ''' <summary>Verifies that booleans and keep-alive durations can be exported together.</summary>
        <Theory>
        <InlineData(True, "1", "30s")>
        <InlineData(False, "0", "5m")>
        Public Sub ExportPreservesBooleanAndDurationValues(enabled As Boolean, expectedBoolean As String, duration As String)
            Const booleanVariable As String = "OLLAMA_DEBUG"
            Const durationVariable As String = "OLLAMA_KEEP_ALIVE"
            Dim previousBoolean As String = Environment.GetEnvironmentVariable(booleanVariable)
            Dim previousDuration As String = Environment.GetEnvironmentVariable(durationVariable)

            Try
                EnvironmentVariables.OLLAMA_DEBUG = enabled
                EnvironmentVariables.OLLAMA_KEEP_ALIVE = duration

                Assert.Equal(expectedBoolean, Environment.GetEnvironmentVariable(booleanVariable))
                Assert.Equal(duration, Environment.GetEnvironmentVariable(durationVariable))
                Using document As JsonDocument = JsonDocument.Parse(EnvironmentVariables.ToString())
                    Assert.Equal(expectedBoolean, document.RootElement.GetProperty(booleanVariable).GetString())
                    Assert.Equal(duration, document.RootElement.GetProperty(durationVariable).GetString())
                End Using
            Finally
                Environment.SetEnvironmentVariable(booleanVariable, previousBoolean)
                Environment.SetEnvironmentVariable(durationVariable, previousDuration)
            End Try
        End Sub

        ''' <summary>Verifies that invalid integers do not become valid configuration values.</summary>
        <Theory>
        <InlineData(Nothing, Nothing)>
        <InlineData(" ", Nothing)>
        <InlineData("many", Nothing)>
        <InlineData("2147483648", Nothing)>
        <InlineData(" 16 ", 16)>
        <InlineData("0", 0)>
        Public Sub IntegerConfigurationHandlesMalformedAndValidValues(raw As String, expected As Integer?)
            Const variableName As String = "OLLAMA_NUM_PARALLEL"
            Dim previous As String = Environment.GetEnvironmentVariable(variableName)
            Try
                Environment.SetEnvironmentVariable(variableName, raw)
                Assert.Equal(expected, EnvironmentVariables.OLLAMA_NUM_PARALLEL)
            Finally
                Environment.SetEnvironmentVariable(variableName, previous)
            End Try
        End Sub

    End Class

End Namespace
