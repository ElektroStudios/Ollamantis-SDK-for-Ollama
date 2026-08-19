
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Text.Json.Serialization

Imports Ollamantis.Entities

#End Region

Namespace Contracts

#Region " CompletionRequestBase "

    ''' <summary>
    ''' Provides the base implementation for text/chat completion generation request contracts.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public MustInherit Class CompletionRequestBase : Inherits GenerationRequestBase

#Region " Properties "

        ''' <summary>
        ''' Optional. Gets or sets a value indicating whether the model should think before responding (for thinking models).
        ''' <para></para>
        ''' This value can be a boolean (<see langword="True"/>/<see langword="False"/>) 
        ''' or a thinking level string ("<c>low</c>", "<c>medium</c>", "<c>high</c>", or "<c>max</c>").
        ''' <para></para>
        ''' Default value is null (meaning the model will not think before responding).
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("think")>
        <DisplayName("Think")>
        <Description("Optional. A value indicating whether the model should think before responding (for thinking models). This value can be a boolean (True/False) or a thinking level string (""low"", ""medium"", ""high"", or ""max"").")>
        Public Property Think As ThinkOption

        ''' <summary>
        ''' Optional. Gets or sets the format to return a response in. 
        ''' This value can be null for a standard response, "<c>json</c>", or a JSON schema object.
        ''' <para></para>
        ''' When format is set to JSON, the output will always be a well-formed JSON object. 
        ''' It's important to also instruct the model to respond in JSON.
        ''' <para></para>
        ''' Default value is null (generate a standard response).
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("format")>
        <DisplayName("Format")>
        <Description("Optional. The format to return a response in. This value can be null for plain text, ""json"", or a JSON schema object. When format is set to JSON, the output will always be a well-formed JSON object. It's important to also instruct the model to respond in JSON.")>
        Public Property Format As FormatOption

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
        ''' Gets the string to display in the debugger DataTips and variable windows.
        ''' </summary>
        <Browsable(False)>
        <DebuggerBrowsable(DebuggerBrowsableState.Never)>
        Protected Overrides ReadOnly Property DebuggerDisplay As String
            Get
                Dim baseDisplay As String = MyBase.DebuggerDisplay

                Return $"{baseDisplay}, Think = {Me.Think}, Format = {Me.Format}, Stream = {Me.Stream}"
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="CompletionRequestBase"/> class.
        ''' </summary>
        Public Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="CompletionRequestBase"/> class.
        ''' </summary>
        ''' 
        ''' <param name="model">
        ''' Mandatory. The name of the model to use for the generation request (e.g., "<c>llama3.2</c>").
        ''' </param>
        ''' 
        ''' <param name="options">
        ''' Optional. Additional model parameters for generation.
        ''' <para></para>
        ''' Default value is null (use defaults).
        ''' </param>
        ''' 
        ''' <param name="keepAlive">
        ''' Optional. A value indicating how long the model will stay loaded into memory following the request.
        ''' <para></para>
        ''' Default value is null, which defaults to "<c>5m</c>".
        ''' </param>
        ''' 
        ''' <param name="think">
        ''' Optional. A value indicating whether the model should think before responding (for thinking models).
        ''' <para></para>
        ''' This value can be a boolean (<see langword="True"/>/<see langword="False"/>) 
        ''' or a thinking level string ("<c>low</c>", "<c>medium</c>", "<c>high</c>", or "<c>max</c>").
        ''' <para></para>
        ''' Default value is null (meaning the model will not think before responding).
        ''' </param>
        ''' 
        ''' <param name="format">
        ''' Optional. The format to return a response in. T
        ''' his value can be null for a standard response, "<c>json</c>", or a JSON schema object.
        ''' <para></para>
        ''' When format is set to JSON, the output will always be a well-formed JSON object. 
        ''' It's important to also instruct the model to respond in JSON.
        ''' <para></para>
        ''' Default value is null (generate a standard response).
        ''' </param>
        Public Sub New(model As String,
                       options As GenerationOptions,
                       keepAlive As KeepAliveOption,
                       think As ThinkOption,
                       format As FormatOption)

            MyBase.New(model:=model,
                       options:=options,
                       KeepAlive:=keepAlive)

            Me.Think = think
            Me.Format = format
        End Sub

#End Region

    End Class

#End Region

End Namespace
