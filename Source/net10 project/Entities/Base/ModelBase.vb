
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

#Region " ModelBase "

    ''' <summary>
    ''' Provides a base implementation for classes that represents information about an Ollama model.
    ''' <para></para>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#list-local-models">
    ''' Ollama API documentation</see>.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public MustInherit Class ModelBase : Inherits JsonObjectBaseImmutable

#Region " Properties "

        ''' <summary>
        ''' Gets the name of the model, including the tag ("<c>name:tag</c>").
        ''' </summary>
        <JsonPropertyOrder(-2)> ' Forces this property to be serialized first (at top of JSON).
        <JsonPropertyName("name")>
        <DisplayName("Name")>
        <Description("The name of the model, including the tag (""name:tag"").")>
        Public ReadOnly Property Name As String

        ''' <summary>
        ''' Gets the name of the model (e.g., "<c>llama3.2</c>").
        ''' </summary>
        <JsonPropertyOrder(-1)> ' Forces this property to be serialized second (at top of JSON).
        <JsonPropertyName("model")>
        <DisplayName("Model")>
        <Description("The name of the model (e.g., ""llama3.2"").")>
        Public ReadOnly Property Model As String

        ''' <summary>
        ''' Gets the size of the model file on disk, in bytes.
        ''' </summary>
        <JsonPropertyName("size")>
        <DisplayName("File Size (in bytes)")>
        <Description("The size of the model file on disk, in bytes.")>
        Public ReadOnly Property FileSize As Long

        ''' <summary>
        ''' Gets the size of the model file on disk, in a human-readable format (e.g., KB, MB, GB).
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenReading)>
        <JsonPropertyName("size_formatted")>
        <DisplayName("File Size (formatted)")>
        <Description("The size of the model file on disk, in a human-readable format.")>
        Public ReadOnly Property FileSizeFormatted As String
            <DebuggerStepThrough>
            Get
                Return MyBase.FormatByteSize(Me.FileSize)
            End Get
        End Property

        ''' <summary>
        ''' Gets the expected <c>SHA-256</c> digest of the model file, 
        ''' used to verify the integrity of the file.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("digest")>
        <DisplayName("Digest")>
        <Description("The expected SHA-256 digest of the model file, used to verify the integrity of the file.")>
        Public ReadOnly Property Digest As String

        ''' <summary>
        ''' Gets additional details of the model.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("details")>
        <DisplayName("Details")>
        <Description("Additional details of the model.")>
        Public ReadOnly Property Details As ModelDetails

        ''' <summary>
        ''' Gets the string to display in the debugger variable windows.
        ''' </summary>
        <Browsable(False)>
        <DebuggerBrowsable(DebuggerBrowsableState.Never)>
        Protected Overridable ReadOnly Property DebuggerDisplay As String
            Get
                Return $"Name = {Me.Name}, Model = {Me.Model}, FileSizeFormatted = {Me.FileSizeFormatted}"
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ModelBase"/> class.
        ''' </summary>
        Protected Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ModelBase"/> class.
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
                       fileSize As Long,
                       digest As String,
                       details As ModelDetails)

            Me.Name = name
            Me.Model = model
            Me.FileSize = fileSize
            Me.Digest = digest
            Me.Details = details
        End Sub

#End Region

    End Class

#End Region

End Namespace
