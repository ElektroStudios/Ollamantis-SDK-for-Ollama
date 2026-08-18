
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

#Region " LocalModel "

    ''' <summary>
    ''' Represents information about an Ollama model that is available locally.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#list-local-models">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public Class LocalModel : Inherits ModelBase

#Region " Properties "

        ''' <summary>
        ''' Gets the timestamp when the model was last modified, 
        ''' formatted as an ISO 8601 date and time string in UTC 
        ''' (e.g., "<c>2024-06-04T14:38:31.83753-07:00</c>").
        ''' </summary>
        <JsonPropertyName("modified_at")>
        <DisplayName("Modified At (ISO 8601)")>
        <Description("The timestamp when the model was last modified, formatted as an ISO 8601 date and time string in UTC.")>
        Public ReadOnly Property ModifiedAt As DateTimeOffset

        ''' <summary>
        ''' Gets the timestamp when the model was last modified, 
        ''' converted to local time and formatted as a human-readable 24-hour string 
        ''' (e.g., "<c>Saturday, August 15, 2026 at 32:17:55</c>").
        ''' </summary>
        <JsonPropertyName("modified_at_formatted")>
        <DisplayName("Modified At (formatted)")>
        <Description("The timestamp when the model was last modified, converted to local time and formatted as a human-readable 24-hour string.")>
        Public ReadOnly Property ModifiedAtFormatted As String
            Get
                Return MyBase.FormatDateTimeOffset(Me.ModifiedAt)
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

                Return $"{baseDisplay}, ModifiedAtFormatted = {Me.ModifiedAtFormatted}"
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="LocalModel"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="LocalModel"/> class.
        ''' </summary>
        ''' 
        ''' <param name="name">
        ''' The name of the model, including the tag ("<c>name:tag</c>").
        ''' </param>
        ''' 
        ''' <param name="model">
        ''' The name of the model (e.g., "<c>llama3.2</c>").
        ''' </param>
        ''' 
        ''' <param name="modifiedAt">
        ''' The timestamp when the model was last modified, 
        ''' formatted as an ISO 8601 date and time string in UTC 
        ''' (e.g., "<c>2024-06-04T14:38:31.83753-07:00</c>").
        ''' </param>
        ''' 
        ''' <param name="fileSize">
        ''' The size of the model file on disk, in bytes.
        ''' </param>
        ''' 
        ''' <param name="digest">
        ''' The expected <c>SHA-256</c> digest of the model file, 
        ''' used to verify the integrity of the file.
        ''' </param>
        ''' 
        ''' <param name="details">
        ''' Additional details of the model.
        ''' </param>
        <JsonConstructor>
        Public Sub New(name As String,
                       model As String,
                       modifiedAt As DateTimeOffset,
                       fileSize As Long,
                       digest As String,
                       details As ModelDetails)

            MyBase.New(name:=name,
                       model:=model,
                       fileSize:=fileSize,
                       digest:=digest,
                       details:=details)

            Me.ModifiedAt = modifiedAt
        End Sub

#End Region

    End Class

#End Region

End Namespace
