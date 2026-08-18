
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

#Region " RunningModel "

    ''' <summary>
    ''' Represents information about an Ollama model that is currently loaded in memory.
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
    Public Class RunningModel : Inherits ModelBase

#Region " Properties "

        ''' <summary>
        ''' Gets the expiration timestamp for the model (when the model will be unloaded from memory), 
        ''' formatted as an ISO 8601 date and time string in UTC 
        ''' (e.g., "<c>2024-06-04T14:38:31.83753-07:00</c>").
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("expires_at")>
        <DisplayName("Expires At (ISO 8601)")>
        <Description("The expiration timestamp for the model (when the model will be unloaded from memory), formatted as an ISO 8601 date and time string in UTC.")>
        Public ReadOnly Property ExpiresAt As DateTimeOffset?

        ''' <summary>
        ''' Gets the expiration timestamp for the model (when the model will be unloaded from memory), 
        ''' converted to local time and formatted as a human-readable 24-hour string 
        ''' (e.g., "<c>Saturday, August 15, 2026 at 32:17:55</c>").
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("expires_at_formatted")>
        <DisplayName("Expires At (formatted)")>
        <Description("The expiration timestamp for the model (when the model will be unloaded from memory), converted to local time and formatted as a human-readable 24-hour string.")>
        Public ReadOnly Property ExpiresAtFormatted As String
            Get
                Return MyBase.FormatDateTimeOffset(Me.ExpiresAt)
            End Get
        End Property

        ''' <summary>
        ''' Gets the size of the VRAM (Video RAM) consumed by the model, in bytes.
        ''' </summary>
        <JsonPropertyName("size_vram")>
        <DisplayName("VRAM Size (in bytes)")>
        <Description("The size of the VRAM (Video RAM) consumed by the model, in bytes.")>
        Public ReadOnly Property VRamSize As Long

        ''' <summary>
        ''' Gets the size of the VRAM (Video RAM) consumed by the model, in a human-readable format (e.g., KB, MB, GB).
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenReading)>
        <JsonPropertyName("size_vram_formated")>
        <DisplayName("VRAM Size (formatted)")>
        <Description("The size of the VRAM (Video RAM) consumed by the model, in a human-readable format.")>
        Public ReadOnly Property VRamSizeFormatted As String
            <DebuggerStepThrough>
            Get
                Return MyBase.FormatByteSize(Me.VRamSize)
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

                Return $"{baseDisplay}, VRamSizeFormatted = {Me.VRamSizeFormatted}, ExpiresAtFormatted = {Me.ExpiresAtFormatted}"
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="RunningModel"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="RunningModel"/> class.
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
        ''' used to verify the integrity of the model file.
        ''' </param>
        ''' 
        ''' <param name="details">
        ''' Additional details of the model.
        ''' </param>
        ''' 
        ''' <param name="expiresAt">
        ''' The expiration timestamp for the model (when the model will be unloaded from memory), 
        ''' formatted as an ISO 8601 date and time string in UTC 
        ''' (e.g., "<c>2024-06-04T14:38:31.83753-07:00</c>").
        ''' </param>
        ''' 
        ''' <param name="vramSize">
        ''' The size of the VRAM (Video RAM) consumed by the model, in bytes. 
        ''' </param>
        <JsonConstructor>
        Public Sub New(name As String,
                       model As String,
                       fileSize As Long,
                       digest As String,
                       details As ModelDetails,
                       expiresAt As DateTimeOffset?,
                       vramSize As Long)

            MyBase.New(name:=name,
                       model:=model,
                       fileSize:=fileSize,
                       digest:=digest,
                       details:=details)

            Me.ExpiresAt = expiresAt
            Me.VRamSize = vramSize
        End Sub

#End Region

    End Class

#End Region

End Namespace
