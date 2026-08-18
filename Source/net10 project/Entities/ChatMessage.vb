
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
Imports Ollamantis.Contracts

#End Region

Namespace Entities

#Region " ChatMessage "

    ''' <summary>
    ''' Represents a single message within a chat conversation with an Ollama model.
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
    <DebuggerDisplay("Role = {Me.Role}, ToolName = {Me.ToolName}, Content = {Me.Content}")>
    Public Class ChatMessage : Inherits JsonObjectBase

#Region " Properties "

        ''' <summary>
        ''' Mandatory. Gets or sets the role of the message sender (e.g., "system", "user", or "assistant").
        ''' </summary>
        <JsonPropertyName("role")>
        <DisplayName("Role")>
        <Description("The role of the message sender (e.g., ""system"", ""user"", or ""assistant"").")>
        Public Property Role As RoleOption

        ''' <summary>
        ''' Mandatory. Gets or sets the textual content of the message.
        ''' </summary>
        <JsonPropertyName("content")>
        <DisplayName("Content")>
        <Description("The textual content of the message.")>
        Public Property Content As String

        ''' <summary>
        ''' Optional. Gets or sets a list of base64-encoded images to include in the message (for multimodal models such as 'llava').
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("images")>
        <DisplayName("Images")>
        <Description("A list of base64-encoded images to include in the message (for multimodal models such as 'llava').")>
        Public Property Images As List(Of ImageOption)

        ''' <summary>
        ''' Optional. Gets or sets the tool calls the model wants to make. Present when the model invokes a function.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("tool_calls")>
        <DisplayName("Tool Calls")>
        <Description("The tool calls the model wants to make. Present when the model invokes a function.")>
        Public Property ToolCalls As List(Of ToolCall)

        ''' <summary>
        ''' Optional. Gets or sets the name of the tool that was executed to inform the model of the result.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("tool_name")>
        <DisplayName("Tool Name")>
        <Description("The tool name of the tool that was executed to inform the model of the result.")>
        Public Property ToolName As String

        ''' <summary>
        ''' Use reserved for the model response. Gets the reasoning trace produced by the model before the response.
        ''' <para></para>
        ''' Only present in streaming chunks when the <see cref="ChatCompletionRequest.Stream"/> property is <see langword="True"/> 
        ''' and the <see cref="ChatCompletionRequest.Think"/> property was sent in the request.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWriting)>
        <JsonPropertyName("thinking")>
        <DisplayName("Thinking")>
        <Description("The reasoning trace produced by the model before the response.")>
        Public ReadOnly Property Thinking As String

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ChatMessage"/> class.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ChatMessage"/> class.
        ''' </summary>
        ''' 
        ''' <param name="role">
        ''' Mandatory. The role of the message sender (e.g., "system", "user", or "assistant").
        ''' </param>
        ''' 
        ''' <param name="content">
        ''' Mandatory. The textual content of the message.
        ''' </param>
        ''' 
        ''' <param name="images">
        ''' Optional. A list of base64-encoded images to include in the message (for multimodal models such as 'llava'). 
        ''' </param>
        ''' 
        ''' <param name="toolCalls">
        ''' Optional. The tool calls the model wants to make. Present when the model invokes a function.
        ''' </param>
        ''' 
        ''' <param name="toolName">
        ''' Optional. The name of the tool that was executed to inform the model of the result. 
        ''' </param>
        Public Sub New(role As RoleOption,
                       content As String,
              Optional images As List(Of ImageOption) = Nothing,
              Optional toolCalls As List(Of ToolCall) = Nothing,
              Optional toolName As String = Nothing)

            Me.New(role:=role,
                   content:=content,
                   thinking:=Nothing,
                   images:=images,
                   toolCalls:=toolCalls,
                   toolName:=toolName)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ChatMessage"/> class.
        ''' </summary>
        ''' 
        ''' <param name="role">
        ''' The role of the message sender (e.g., "system", "user", or "assistant").
        ''' </param>
        ''' 
        ''' <param name="content">
        ''' The textual content of the message.
        ''' </param>
        ''' 
        ''' <param name="thinking">
        ''' The reasoning trace produced by the model before the response.
        ''' <para></para>
        ''' Only present in streaming chunks when the <see cref="ChatCompletionRequest.Stream"/> property is <see langword="True"/> 
        ''' and the <see cref="ChatCompletionRequest.Think"/> property was sent in the request.
        ''' </param>
        ''' 
        ''' <param name="images">
        ''' A list of base64-encoded images to include in the message (for multimodal models such as 'llava'). 
        ''' </param>
        ''' 
        ''' <param name="toolCalls">
        ''' The tool calls the model wants to make. Present when the model invokes a function.
        ''' </param>
        ''' 
        ''' <param name="toolName">
        ''' The name of the tool that was executed to inform the model of the result. 
        ''' </param>
        <JsonConstructor>
        Public Sub New(role As RoleOption,
                       content As String,
                       thinking As String,
                       images As List(Of ImageOption),
                       toolCalls As List(Of ToolCall),
                       toolName As String)

            Me.Role = role
            Me.Content = content
            Me.Thinking = thinking
            Me.Images = images
            Me.ToolCalls = toolCalls
            Me.ToolName = toolName
        End Sub

#End Region

    End Class

#End Region

End Namespace