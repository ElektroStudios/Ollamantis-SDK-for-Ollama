
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

Namespace Contracts

#Region " OllamaVersionResponse "

    ''' <summary>
    ''' Represents the response containing the version of the current Ollama server.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#version">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public Class OllamaVersionResponse : Inherits ResponseBase

#Region " Properties "

        ''' <summary>
        ''' Gets the version of the current Ollama client. 
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.Never)>
        <JsonPropertyName("version")>
        <DisplayName("Version")>
        <Description("The version of the current Ollama client.")>
        Public Property Version As String

        ''' <summary>
        ''' Gets the string to display in the debugger variable windows.
        ''' </summary>
        <Browsable(False)>
        <DebuggerBrowsable(DebuggerBrowsableState.Never)>
        Protected Overrides ReadOnly Property DebuggerDisplay As String
            Get
                Dim baseDisplay As String = MyBase.DebuggerDisplay

                Return $"{baseDisplay}, Version = {Me.Version}"
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="OllamaVersionResponse"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="OllamaVersionResponse"/> class.
        ''' </summary>
        ''' 
        ''' <param name="version">
        ''' The version of the current Ollama client.
        ''' </param>
        <JsonConstructor>
        Public Sub New(version As String)

            Me.Version = version
        End Sub

#End Region

    End Class

#End Region

End Namespace
