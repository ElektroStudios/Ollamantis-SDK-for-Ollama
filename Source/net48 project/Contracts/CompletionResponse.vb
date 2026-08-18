
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Diagnostics.CodeAnalysis
Imports System.Text.Json.Serialization

#End Region

Namespace Contracts

#Region " CompletionResponse "

    ''' <summary>
    ''' Represents the response containing the result of a <see cref="CompletionRequest"/>.
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
    Public Class CompletionResponse : Inherits GenerationResponseBase

#Region " Properties "

        ''' <summary>
        ''' Gets the generated response.
        ''' <para></para>
        ''' If streaming is disabled (<see cref="CompletionRequest.Stream"/> = <see langword="False"/>), 
        ''' this will contain the full streamed response. (e.g., "The sky is blue because it is the color of the sky.")
        ''' <para></para>
        ''' If streaming is enabled (<see cref="CompletionRequest.Stream"/> = <see langword="True"/>), 
        ''' this will contain the current streamed chunk. (e.g., "The")
        ''' </summary>
        <JsonPropertyName("response")>
        <DisplayName("Response")>
        <Description("The generated response. If streaming is disabled, it contains the full response; if streaming is enabled, it contains the current streamed chunk.")>
        Public ReadOnly Property Response As String

        ''' <summary>
        ''' Gets the context parameter returned by the <c>/api/generate</c> endpoint 
        ''' (e.g., <c>{ 12, 45, 902, 1 }</c>).
        ''' <para></para>
        ''' This array of tokens can be sent in the next request to keep a short conversational memory.
        ''' </summary>
        <Obsolete("This parameter is deprecated by the Ollama API.", False)>
        <JsonPropertyName("context")>
        <DisplayName("Context")>
        <Description("The context parameter returned by the /api/generate endpoint (e.g., { 12, 45, 902, 1 }). This array of tokens can be sent in the next request to keep a short conversational memory.")>
        Public ReadOnly Property Context As Integer()

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="CompletionResponse"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="CompletionResponse"/> class.
        ''' </summary>
        ''' 
        ''' <param name="model">
        ''' The name of the model used for generation (e.g., "<c>llama3.2</c>").
        ''' </param>
        ''' 
        ''' <param name="response">
        ''' The generated response.
        ''' <para></para>
        ''' If streaming is disabled (<see cref="CompletionRequest.Stream"/> = <see langword="False"/>), 
        ''' this will contain the full streamed response. (e.g., "The sky is blue because it is the color of the sky.")
        ''' <para></para>
        ''' If streaming is enabled (<see cref="CompletionRequest.Stream"/> = <see langword="True"/>), 
        ''' this will contain the current streamed chunk. (e.g., "The")
        ''' </param>
        ''' 
        ''' <param name="context">
        ''' The context parameter returned by the <c>/api/generate</c> endpoint (e.g., <c>{ 12, 45, 902, 1 }</c>).
        ''' <para></para>
        ''' This array of tokens can be sent in the next request to keep a short conversational memory.
        ''' </param>
        ''' 
        ''' <param name="createdAt">
        ''' The timestamp when the response was created, 
        ''' formatted as an ISO 8601 date and time string in UTC 
        ''' (e.g., "<c>2024-06-04T14:38:31.83753-07:00</c>").
        ''' </param>
        ''' 
        ''' <param name="done">
        ''' A <see cref="Boolean"/> value indicating the completion status of the response generation.
        ''' <para></para>
        ''' If streaming is disabled (<see cref="CompletionRequest.Stream"/> = <see langword="False"/>), 
        ''' this will always be <see langword="True"/> since <see cref="CompletionResponse.Response"/> 
        ''' contains the full streamed response.
        ''' <para></para>
        ''' If streaming is enabled (<see cref="CompletionRequest.Stream"/> = <see langword="True"/>), 
        ''' this can be <see langword="False"/> if the response is still streaming,
        ''' or <see langword="True"/> if the response has finished streaming and 
        ''' <see cref="CompletionResponse.Response"/> contains the final streamed chunk.
        ''' </param>
        ''' 
        ''' <param name="doneReason">
        ''' The completion reason of the response generation.
        ''' </param>
        ''' 
        ''' <param name="totalDuration">
        ''' The total time spent processing the entire request, in nanoseconds.
        ''' </param>
        ''' 
        ''' <param name="loadDuration">
        ''' The time spent loading the model, in nanoseconds.
        ''' </param>
        ''' 
        ''' <param name="promptEvalCount">
        ''' The number of tokens evaluated in the prompt.
        ''' </param>
        ''' 
        ''' <param name="promptEvalDuration">
        ''' The time spent evaluating the prompt, in nanoseconds.
        ''' </param>
        ''' 
        ''' <param name="evalCount">
        ''' The number of tokens generated in the response.
        ''' </param>
        ''' 
        ''' <param name="evalDuration">
        ''' The time spent strictly generating the response tokens, in nanoseconds.
        ''' </param>
        <JsonConstructor>
        <SuppressMessage("Major Code Smell", "S107:Methods should not have too many parameters", Justification:="Ollama API JSON deserialization requires a large number of parameters.")>
        Public Sub New(model As String,
                       response As String,
                       context As Integer(),
                       createdAt As DateTimeOffset?,
                       done As Boolean?,
                       doneReason As String,
                       totalDuration As Long?, loadDuration As Long?,
                       promptEvalCount As Integer?, promptEvalDuration As Long?,
                       evalCount As Integer?, evalDuration As Long?)

#Disable Warning BC40000 ' Type or member is obsolete

            MyBase.New(model:=model,
                       createdAt:=createdAt,
                       done:=done,
                       doneReason:=doneReason,
                       totalDuration:=totalDuration,
                       loadDuration:=loadDuration,
                       promptEvalCount:=promptEvalCount,
                       promptEvalDuration:=promptEvalDuration,
                       evalCount:=evalCount,
                       evalDuration:=evalDuration)

            Me.Response = response

            ' Deprecated property parameters:
            Me.Context = context

#Enable Warning BC40000 ' Type or member is obsolete
        End Sub

#End Region

    End Class

#End Region

End Namespace
