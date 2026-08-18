
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
Imports Ollamantis.Entities

#End Region

Namespace Contracts

#Region " RunningModelsResponse "

    ''' <summary>
    ''' Represents the response containing information about the Ollama models that are currently loaded in memory.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#list-running-models">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public Class RunningModelsResponse : Inherits ResponseBase

#Region " Properties "

        ''' <summary>
        ''' Gets an array of <see cref="RunningModel"/> containing the Ollama models that are currently loaded in memory.
        ''' </summary>
        <JsonPropertyOrder(-1)> ' Forces this property to be serialized first (at top of JSON).
        <JsonPropertyName("models")>
        <DisplayName("Models")>
        <Description("The Ollama models currently loaded in memory.")>
        Public ReadOnly Property Models As RunningModel()

#End Region

#Region " Debugger Display Properties "

        ''' <summary>
        ''' Gets the count of Ollama models that are currently loaded in memory (from <see cref="RunningModelsResponse.Models"/> property) 
        ''' for display in the Visual Studio debugger.
        ''' </summary>
        <Browsable(False)>
        <DebuggerBrowsable(DebuggerBrowsableState.Never)>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenReading)>
        <JsonPropertyName("models_count")>
        Private ReadOnly Property ModelsCount As Integer
            Get
                Return If(Me.Models Is Nothing, 0, Me.Models.Length)
            End Get
        End Property

        ''' <summary>
        ''' Gets the string to display in the debugger variable windows.
        ''' </summary>
        <Browsable(False)>
        <DebuggerBrowsable(DebuggerBrowsableState.Never)>
        Protected Overrides ReadOnly Property DebuggerDisplay As String
            Get
                Dim baseDisplay As String = MyBase.DebuggerDisplay

                Return $"{baseDisplay}, ModelsCount = {Me.ModelsCount}"
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="RunningModelsResponse"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="RunningModelsResponse"/> class.
        ''' </summary>
        ''' 
        ''' <param name="models">
        ''' An array of <see cref="RunningModel"/> containing the Ollama models that are currently loaded in memory.
        ''' </param>
        <JsonConstructor>
        Public Sub New(models As RunningModel())

            Me.Models = models
        End Sub

#End Region

    End Class

#End Region

End Namespace
