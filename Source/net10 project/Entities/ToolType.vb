
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Text.Json.Serialization

#End Region

Namespace Entities

#Region " ToolType "

    ''' <summary>
    ''' Defines the supported types for a <see cref="Tool"/> in the Ollama API.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#tool-calling">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <JsonConverter(GetType(JsonStringEnumConverter))>
    Public Enum ToolType

        ''' <summary>
        ''' A '<c>function</c>' tool.
        ''' </summary>
        [function] = 0

    End Enum

#End Region

End Namespace
