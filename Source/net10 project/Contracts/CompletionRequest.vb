
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Diagnostics.CodeAnalysis
Imports System.Text.Json.Serialization

Imports Ollamantis.Entities

#End Region

Namespace Contracts

#Region " CompletionRequest "

    ''' <summary>
    ''' Represents the request to generate a completion response from a given prompt using a specified model.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-completion">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression")>
    <SuppressMessage("Major Code Smell", "S1133:Deprecated code should be removed", Justification:="Maintained for backward compatibility with older Ollama API versions.")>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public Class CompletionRequest : Inherits CompletionRequestBase

#Region " Properties "

        ''' <summary>
        ''' Mandatory. Gets or sets the prompt to generate a response for (e.g., "<c>Why is the sky blue?</c>").
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.Never)>
        <JsonPropertyName("prompt")>
        <DisplayName("Prompt")>
        <Description("Mandatory. The prompt to generate a response for (e.g., ""Why is the sky blue?"").")>
        Public Property Prompt As String

        ''' <summary>
        ''' Optional. Gets or sets the text after the model response. Used to guide the model to fill in the middle (e.g., "<c>...and they lived happily ever after.</c>").
        ''' <para></para>
        ''' Default value is null.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("suffix")>
        <DisplayName("Suffix")>
        <Description("Optional. The text after the model response. Used to guide the model to fill in the middle (e.g., '...and they lived happily ever after.').")>
        Public Property Suffix As String

        ''' <summary>
        ''' Optional. Gets or sets a list of base64-encoded images (for multimodal models such as <c>llava</c>).
        ''' <para></para>
        ''' Default value is null.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("images")>
        <DisplayName("Images")>
        <Description("Optional. A list of base64-encoded images (for multimodal models such as 'llava').")>
        Public Property Images As List(Of ImageOption)

        ''' <summary>
        ''' Optional. Gets or sets the system message to use (overrides what is defined in the <c>Modelfile</c>) (e.g., "<c>You are an expert VB.NET developer.</c>").
        ''' <para></para>
        ''' Default value is null.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("system")>
        <DisplayName("System")>
        <Description("Optional. The system message to use (overrides what is defined in the Modelfile) (e.g., ""You are an expert VB.NET developer."").")>
        Public Property System As String

        ''' <summary>
        ''' Optional. Gets or sets the prompt template to use (overrides what is defined in the <c>Modelfile</c>) (e.g., "<c>{{.System}} User: {{.Prompt}} Assistant:</c>").
        ''' <para></para>
        ''' Default value is null.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("template")>
        <DisplayName("Template")>
        <Description("Optional. The prompt template to use (overrides what is defined in the Modelfile) (e.g., ""{{.System}} User: {{.Prompt}} Assistant:"").")>
        Public Property Template As String

        ''' <summary>
        ''' Optional. Gets or sets a <see cref="Boolean"/> value indicating whether formatting will be applied to the prompt (<see langword="False"/>), 
        ''' rather than sending the prompt as-is (<see langword="True"/>).
        ''' <para></para>
        ''' You may choose to set this value to <see langword="True"/> if you are specifying a full templated prompt in your request.
        ''' <para></para>
        ''' Default value is <see langword="False"/>.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.Never)>
        <JsonPropertyName("raw")>
        <DisplayName("Raw")>
        <Description("Optional. A boolean value indicating whether formatting will be applied to the prompt (False), rather than sending the prompt as-is (True). You may choose to set this value to True if you are specifying a full templated prompt in your request.")>
        Public Property Raw As Boolean

        ''' <summary>
        ''' Optional. Gets or sets the context parameter returned from a previous request to <c>/api/generate</c> endpoint (e.g., "<c>{ 12, 45, 902, 1 }</c>").
        ''' <para></para>
        ''' This array of tokens can be sent in the next request to keep a short conversational memory.
        ''' <para></para>
        ''' Default value is null.
        ''' </summary>
        <Obsolete("This parameter is deprecated by the Ollama API.", False)>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("context")>
        <DisplayName("Context")>
        <Description("Optional. The context parameter returned from a previous request to /api/generate endpoint (e.g., { 12, 45, 902, 1 }). This array of tokens can be sent in the next request to keep a short conversational memory.")>
        Public Property Context As Integer()

        ''' <summary>
        ''' Gets the string to display in the debugger variable windows.
        ''' </summary>
        <Browsable(False)>
        <DebuggerBrowsable(DebuggerBrowsableState.Never)>
        Protected Overrides ReadOnly Property DebuggerDisplay As String
            Get
                Dim baseDisplay As String = MyBase.DebuggerDisplay

                Return $"{baseDisplay}, Raw = {Me.Raw}"
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="CompletionRequest"/> class.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="CompletionRequest"/> class with the specified model and prompt, 
        ''' and optional parameters for suffix, images, think, format, options, system, template, stream, raw, keepAlive, and context.
        ''' </summary>
        ''' 
        ''' <param name="model">
        ''' Mandatory. The name of the model to use for completion (e.g., "<c>llama3.2</c>").
        ''' </param>
        ''' 
        ''' <param name="prompt">
        ''' Mandatory. The prompt to generate a response for (e.g., "<c>Why is the sky blue?</c>").
        ''' </param>
        ''' 
        ''' <param name="suffix">
        ''' Optional. The text after the model response. Used to guide the model to fill in the middle (e.g., "<c>...and they lived happily ever after.</c>").
        ''' <para></para>
        ''' Default value is null.
        ''' </param>
        ''' 
        ''' <param name="images">
        ''' Optional. A list of base64-encoded images (for multimodal models such as <c>llava</c>).
        ''' <para></para>
        ''' Default value is null.
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
        ''' Optional. The format to return a response in. This value can be null for a standard response, "<c>json</c>", or a JSON schema object.
        ''' <para></para>
        ''' When format is set to JSON, the output will always be a well-formed JSON object. It's important to also instruct the model to respond in JSON.
        ''' <para></para>
        ''' Default value is null (generate a standard response).
        ''' </param>
        ''' 
        ''' <param name="options">
        ''' Optional. Additional model parameters for generation.
        ''' <para></para>
        ''' Default value is null (use defaults).
        ''' </param>
        ''' 
        ''' <param name="system">
        ''' Optional. The system message to use (overrides what is defined in the <c>Modelfile</c>) (e.g., "<c>You are an expert VB.NET developer.</c>").
        ''' <para></para>
        ''' Default value is null.
        ''' </param>
        ''' 
        ''' <param name="template">
        ''' Optional. The prompt template to use (overrides what is defined in the <c>Modelfile</c>) (e.g., "<c>{{.System}} User: {{.Prompt}} Assistant:</c>").
        ''' <para></para>
        ''' Default value is null.
        ''' </param>
        ''' 
        ''' <param name="raw">
        ''' Optional. A <see cref="Boolean"/> value indicating whether formatting will be applied to the prompt (<see langword="False"/>), 
        ''' rather than sending the prompt as-is (<see langword="True"/>).
        ''' <para></para>
        ''' You may choose to set this value to <see langword="True"/> if you are specifying a full templated prompt in your request.
        ''' <para></para>
        ''' Default value is <see langword="False"/>.
        ''' </param>
        ''' 
        ''' <param name="keepAlive">
        ''' Optional. A value indicating how long the model will stay loaded into memory following the request.
        ''' <para></para>
        ''' Default value is null, which defaults to "<c>5m</c>".
        ''' </param>
        ''' 
        ''' <param name="context">
        ''' Optional. The context parameter returned from a previous request to <c>/api/generate</c> endpoint (e.g., "<c>{ 12, 45, 902, 1 }</c>").
        ''' <para></para>
        ''' This array of tokens can be sent in the next request to keep a short conversational memory.
        ''' <para></para>
        ''' Default value is null.
        ''' </param>
        <SuppressMessage("Major Code Smell", "S107:Methods should not have too many parameters", Justification:="Ollama API JSON deserialization requires a large number of parameters.")>
        Public Sub New(model As String, prompt As String,
              Optional suffix As String = Nothing,
              Optional images As List(Of ImageOption) = Nothing,
              Optional think As ThinkOption = Nothing,
              Optional format As FormatOption = Nothing,
              Optional options As GenerationOptions = Nothing,
              Optional system As String = Nothing,
              Optional template As String = Nothing,
              Optional raw As Boolean = False,
              Optional keepAlive As KeepAliveOption = Nothing,
              Optional context As Integer() = Nothing)

#Disable Warning BC40000 ' Type or member is obsolete

            Me.Model = model
            Me.Prompt = prompt
            Me.Suffix = suffix
            Me.Images = images
            Me.Think = think
            Me.Format = format
            Me.Options = options
            Me.System = system
            Me.Template = template
            Me.Raw = raw
            Me.KeepAlive = keepAlive

            ' Deprecated property parameters:
            Me.Context = context

#Enable Warning BC40000 ' Type or member is obsolete
        End Sub

#End Region

    End Class

#End Region

End Namespace
