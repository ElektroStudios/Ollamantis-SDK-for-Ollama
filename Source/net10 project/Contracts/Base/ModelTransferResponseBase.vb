
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

Namespace Contracts

#Region " ModelTransferResponseBase "

    ''' <summary>
    ''' Provides a base implementation for responses that transfer models to or from Ollama remote library, 
    ''' such as push and pull operations.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public MustInherit Class ModelTransferResponseBase : Inherits ResponseBase

#Region " Properties "

        ''' <summary>
        ''' Gets the status of the model being trasfered to or from the Ollama remote library.
        ''' </summary>
        <JsonPropertyName("status")>
        <DisplayName("Status")>
        <Description("The status of the model being trasfered to or from the Ollama remote library.")>
        Public ReadOnly Property Status As String

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
        ''' Gets the total file size of the model in bytes, or null if the size is unknown.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("total")>
        <DisplayName("Total Size (in bytes)")>
        <Description("The file size of the model in bytes, or null if the size is unknown.")>
        Public ReadOnly Property TotalSize As Long?

        ''' <summary>
        ''' Gets the total file size of the model in a human-readable format (e.g., KB, MB, GB), 
        ''' or null if the size is unknown.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("total_formatted")>
        <DisplayName("Total Size (formatted)")>
        <Description("The file size of the model in a human-readable format, or null if the size is unknown.")>
        Public ReadOnly Property TotalSizeFormatted As String
            <DebuggerStepThrough>
            Get
                Return MyBase.FormatByteSize(Me.TotalSize)
            End Get
        End Property

        ''' <summary>
        ''' Gets the actually completed size of the model in bytes, or null if the size is unknown.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("completed")>
        <DisplayName("Completed Size (in bytes)")>
        <Description("The actually downloaded size of the model in bytes, or null if the size is unknown.")>
        Public ReadOnly Property CompletedSize As Long?

        ''' <summary>
        ''' Gets the actually completed size of the model in a human-readable format (e.g., KB, MB, GB), 
        ''' or null if the size is unknown.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("completed_formatted")>
        <DisplayName("Completed Size (formatted)")>
        <Description("The actually downloaded size of the model in a human-readable format, or null if the size is unknown.")>
        Public ReadOnly Property CompletedSizeFormatted As String
            <DebuggerStepThrough>
            Get
                Return MyBase.FormatByteSize(Me.CompletedSize)
            End Get
        End Property

        ''' <summary>
        ''' Gets the string to display in the debugger DataTips and variable windows.
        ''' </summary>
        <Browsable(False)>
        <DebuggerBrowsable(DebuggerBrowsableState.Never)>
        Protected Overrides ReadOnly Property DebuggerDisplay As String
            Get
                Dim baseDisplay As String = MyBase.DebuggerDisplay
                Dim totalSizeFormatted As String = If(Not String.IsNullOrWhiteSpace(Me.TotalSizeFormatted), $", TotalSizeFormatted = {Me.TotalSizeFormatted}", "")
                Dim completedSizeFormatted As String = If(Not String.IsNullOrWhiteSpace(Me.CompletedSizeFormatted), $", CompletedSizeFormatted = {Me.CompletedSizeFormatted}", "")

                Return $"{baseDisplay}, Status = {Me.Status}{totalSizeFormatted}{completedSizeFormatted}"
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="ModelTransferResponseBase"/> class from being created.
        ''' </summary>
        Protected Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ModelTransferResponseBase"/> class.
        ''' </summary>
        ''' 
        ''' <param name="status">
        ''' The status of the model being trasfered to or from the Ollama remote library.
        ''' </param>
        ''' 
        ''' <param name="digest">
        ''' The expected <c>SHA-256</c> digest of the model file, 
        ''' used to verify the integrity of the file.
        ''' </param>
        ''' 
        ''' <param name="totalSize">
        ''' The total file size of the model in bytes, or null if the size is unknown.
        ''' </param>
        ''' 
        ''' <param name="completedSize">
        ''' The actually completed size of the model in bytes, or null if the size is unknown.
        ''' </param>
        <JsonConstructor>
        Public Sub New(status As String,
                       digest As String,
                       totalSize As Long?,
                       completedSize As Long?)

            Me.Status = status
            Me.Digest = digest
            Me.TotalSize = totalSize
            Me.CompletedSize = completedSize
        End Sub

#End Region

    End Class

#End Region

End Namespace
