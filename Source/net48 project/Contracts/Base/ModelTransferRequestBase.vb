
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

#Region " ModelTransferRequestBase "

    ''' <summary>
    ''' Provides a base implementation for requests that transfer models to or from Ollama remote library, 
    ''' such as push and pull operations.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public MustInherit Class ModelTransferRequestBase : Inherits JsonObjectBase

#Region " Properties "

        ''' <summary>
        ''' Mandatory. Gets or sets the name of the model to transfer (e.g., "<c>llama3.2</c>" or "<c>namespace/mymodel:3b</c>").
        ''' </summary>
        <JsonPropertyOrder(-1)> ' Forces this property to be serialized first (at top of JSON).
        <JsonIgnore(Condition:=JsonIgnoreCondition.Never)>
        <JsonPropertyName("name")>
        <DisplayName("Name")>
        <Description("Mandatory. The name of the model to transfer.")>
        Public Property Name As String

        ''' <summary>
        ''' Optional. Gets or sets a <see cref="Boolean"/> value indicating whether to allow insecure connections to the library (<see langword="True"/>), or not (<see langword="False"/>).
        ''' <para></para>
        ''' Only set this value to <see langword="True"/> if you are transferring to/from your own library during development.
        ''' <para></para>
        ''' Default value is <see langword="False"/>.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingDefault)>
        <JsonPropertyName("insecure")>
        <DisplayName("Insecure")>
        <Description("Optional. A boolean value indicating whether to allow insecure connections to the library (True), or not (False).")>
        Public Property Insecure As Boolean

        ''' <summary>
        ''' Optional. Gets or sets a <see cref="Boolean"/> value indicating whether the response will be returned as 
        ''' a single response object (<see langword="False"/>), rather than 
        ''' a stream of objects (<see langword="True"/>).
        ''' <para></para>
        ''' Default value is <see langword="False"/>.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Never)>
        <JsonInclude>
        <JsonIgnore(Condition:=JsonIgnoreCondition.Never)>
        <JsonPropertyName("stream")>
        <DisplayName("Stream")>
        <Description("Optional. A boolean value indicating whether the response will be returned as a single response object (False), rather than a stream of objects (True).")>
        Protected Friend Property Stream As Boolean ' Note: Default value in Ollama API is True, but we set it to False here for convenience.

        ''' <summary>
        ''' Gets the string to display in the debugger variable windows.
        ''' </summary>
        <Browsable(False)>
        <DebuggerBrowsable(DebuggerBrowsableState.Never)>
        Protected Overridable ReadOnly Property DebuggerDisplay As String
            Get
                Return $"Name = {Me.Name}, Insecure = {Me.Insecure}, Stream = {Me.Stream}"
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="ModelTransferRequestBase"/> class from being created.
        ''' </summary>
        Protected Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ModelTransferRequestBase"/> class.
        ''' </summary>
        ''' 
        ''' <param name="name">
        ''' Mandatory. The name of the model to transfer (e.g., "<c>llama3.2</c>" or "<c>namespace/mymodel:3b</c>").
        ''' </param>
        ''' 
        ''' <param name="insecure">
        ''' Optional. A <see cref="Boolean"/> value indicating whether the response will be returned as 
        ''' a single response object (<see langword="False"/>), rather than 
        ''' a stream of objects (<see langword="True"/>).
        ''' <para></para>
        ''' Default value is <see langword="False"/>.
        ''' </param>
        Protected Sub New(name As String,
                 Optional insecure As Boolean = False)

            Me.Name = name
            Me.Insecure = insecure
        End Sub

#End Region

    End Class

#End Region

End Namespace
