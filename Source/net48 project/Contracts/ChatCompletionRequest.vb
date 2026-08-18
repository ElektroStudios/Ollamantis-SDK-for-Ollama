
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Text.Json.Serialization

Imports Ollamantis.Core
Imports Ollamantis.Entities

#End Region

Namespace Contracts

#Region " ChatCompletionRequest "

    ''' <summary>
    ''' Represents the request to generate a chat completion response from an Ollama model.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-chat-completion">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public Class ChatCompletionRequest : Inherits CompletionRequestBase

#Region " Properties "

        ''' <summary>
        ''' Optional. Gets or sets the list of messages in the chat, used to keep a conversational memory.
        ''' <para></para>
        ''' Default value is null.
        ''' </summary>
        <JsonPropertyName("messages")>
        <DisplayName("Messages")>
        <Description("The list of messages in the chat, used to keep a conversational memory.")>
        Public Property Messages As List(Of ChatMessage)

        ''' <summary>
        ''' Optional. Gets or sets a list of tools the model may call. Requires a model that supports tool calling.
        ''' <para></para>
        ''' Default value is null.
        ''' </summary>
        <JsonPropertyName("tools")>
        <DisplayName("Tools")>
        <Description("A list of tools the model may call. Requires a model that supports tool calling.")>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        Public Property Tools As List(Of Tool)

        ''' <summary>
        ''' Gets or sets the unique identifier used to track and maintain the state of a conversation in a <see cref="ChatSession"/>.
        ''' <para></para>
        ''' When this request is processed by a <see cref="ChatSession"/>, this identifier links the current 
        ''' request to previous messages, allowing the model to remember past context.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingDefault)>
        <JsonPropertyName("conversation_id")>
        <DisplayName("Conversation Id")>
        <Description("The unique identifier used to track and maintain the state of a conversation in a 'ChatSession'.")>
        Public Property ConversationId As Guid

        ''' <summary>
        ''' Gets the string to display in the debugger variable windows.
        ''' </summary>
        <Browsable(False)>
        <DebuggerBrowsable(DebuggerBrowsableState.Never)>
        Protected Overrides ReadOnly Property DebuggerDisplay As String
            Get
                Dim baseDisplay As String = MyBase.DebuggerDisplay

                Return $"{baseDisplay}, ConversationId = {Me.ConversationId}"
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ChatCompletionRequest"/> class.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ChatCompletionRequest"/> class with the specified parameters.
        ''' </summary>
        ''' 
        ''' <param name="model">
        ''' Mandatory. The name of the model to use for the chat completion request (e.g., "<c>llama3.2</c>").
        ''' </param>
        ''' 
        ''' <param name="messages">
        ''' Optional. The list of messages in the chat, used to keep a conversational memory.
        ''' <para></para>
        ''' Default value is null.
        ''' </param>
        ''' 
        ''' <param name="tools">
        ''' Optional. A list of tools the model may call. Requires a model that supports tool calling.
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
        ''' Optional. The format to return a response in. T
        ''' his value can be null for a standard response, "<c>json</c>", or a JSON schema object.
        ''' <para></para>
        ''' When format is set to JSON, the output will always be a well-formed JSON object. 
        ''' It's important to also instruct the model to respond in JSON.
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
        ''' <param name="keepAlive">
        ''' Optional. A value indicating how long the model will stay loaded into memory following the request.
        ''' <para></para>
        ''' Default value is null, which defaults to "<c>5m</c>".
        ''' </param>
        Public Sub New(model As String,
              Optional messages As List(Of ChatMessage) = Nothing,
              Optional tools As List(Of Tool) = Nothing,
              Optional think As ThinkOption = Nothing,
              Optional format As FormatOption = Nothing,
              Optional options As GenerationOptions = Nothing,
              Optional keepAlive As KeepAliveOption = Nothing)

            Me.Model = model
            Me.Messages = messages
            Me.Tools = tools
            Me.Think = think
            Me.Format = format
            Me.Options = options
            Me.KeepAlive = keepAlive
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ChatCompletionRequest"/> class 
        ''' explicitly tied to a specific conversation identifier.    
        ''' </summary>
        ''' 
        ''' <param name="conversationId">
        ''' The unique identifier used to track and maintain the state of a conversation in a <see cref="ChatSession"/>.
        ''' <para></para>
        ''' When this request is processed by a <see cref="ChatSession"/>, this identifier links the current 
        ''' request to previous messages, allowing the model to remember past context.
        ''' </param>
        Public Sub New(conversationId As Guid)

            Me.ConversationId = conversationId
        End Sub

#End Region

    End Class

#End Region

End Namespace
