
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Text.Json.Serialization

Imports Ollamantis.Core

#End Region

Namespace Entities

#Region " ToolCall "

    ''' <summary>
    ''' Represents a tool call returned by an Ollama model inside a <see cref="ChatMessage"/>.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#tool-calling">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("FunctionCall = {Me.FunctionCall}")>
    Public Class ToolCall : Inherits JsonObjectBaseImmutable

#Region " Properties "

        ''' <summary>
        ''' Gets the function the model wants to invoke.
        ''' </summary>
        <JsonPropertyName("function")>
        <DisplayName("Function Call")>
        <Description("The function the model wants to invoke.")>
        Public ReadOnly Property FunctionCall As FunctionCall

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="ToolCall"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ToolCall"/> class.
        ''' </summary>
        ''' 
        ''' <param name="functionCall">
        ''' The function the model wants to invoke. 
        ''' </param>
        <JsonConstructor>
        Public Sub New(functionCall As FunctionCall)

            Me.FunctionCall = functionCall
        End Sub

#End Region

    End Class

#End Region

End Namespace
