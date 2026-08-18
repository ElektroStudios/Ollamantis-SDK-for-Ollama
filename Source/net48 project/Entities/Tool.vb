
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

#Region " Tool "

    ''' <summary>
    ''' Represents a tool that an Ollama model can call during a chat conversation.
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
    <DebuggerDisplay("Type = {Me.Type}, FunctionDefinition = {Me.FunctionDefinition}")>
    Public Class Tool : Inherits JsonObjectBase

#Region " Properties "

        ''' <summary>
        ''' Gets or sets the type of the tool.
        ''' </summary>
        <JsonPropertyName("type")>
        <DisplayName("Type")>
        <Description("The type of the tool.")>
        Public Property Type As ToolType

        ''' <summary>
        ''' Gets or sets the function definition associated with the tool.
        ''' </summary>
        <JsonPropertyName("function")>
        <DisplayName("Function Definition")>
        <Description("The function definition associated with the tool.")>
        Public Property FunctionDefinition As FunctionDefinition

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Tool"/> class.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Tool"/> class.
        ''' </summary>
        ''' 
        ''' <param name="type">
        ''' The type of the tool.
        ''' </param>
        ''' 
        ''' <param name="functionDefinition">
        ''' The function definition associated with the tool. 
        ''' </param>
        Public Sub New(type As ToolType,
                       functionDefinition As FunctionDefinition)

            Me.Type = type
            Me.FunctionDefinition = functionDefinition
        End Sub

#End Region

    End Class

#End Region

End Namespace
