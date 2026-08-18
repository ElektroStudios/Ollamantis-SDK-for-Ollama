
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

#Region " EmbeddingsResponse "

    ''' <summary>
    ''' Represents the response containing the result of an <see cref="EmbeddingsRequest"/>.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#generate-embeddings">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public Class EmbeddingsResponse : Inherits GenerationResponseBase

#Region " Properties "

        ''' <summary>
        ''' Gets the generated embeddings. Each input text corresponds to one array of floating-point numbers.
        ''' </summary>
        <JsonPropertyName("embeddings")>
        <DisplayName("Embeddings")>
        <Description("The generated embeddings as an array of floating-point numbers.")>
        Public ReadOnly Property Embeddings As Double()()

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="EmbeddingsResponse"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="EmbeddingsResponse"/> class.
        ''' </summary>
        ''' 
        ''' <param name="model">
        ''' The name of the model used to generate the embeddings. (e.g., "<c>llama3.2</c>").
        ''' </param>
        ''' 
        ''' <param name="embeddings">
        ''' The generated embeddings. Each input text corresponds to one array of floating-point numbers.
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
        <JsonConstructor>
        Public Sub New(model As String,
                       embeddings As Double()(),
                       totalDuration As Long?,
                       loadDuration As Long?,
                       promptEvalCount As Integer?)

            ' Embeddings response only returns: model, embeddings, total_duration, load_duration, and prompt_eval_count.
            MyBase.New(model:=model,
                       createdAt:=Nothing,
                       done:=Nothing,
                       doneReason:=Nothing,
                       totalDuration:=totalDuration,
                       loadDuration:=loadDuration,
                       promptEvalCount:=promptEvalCount,
                       promptEvalDuration:=Nothing,
                       evalCount:=Nothing,
                       evalDuration:=Nothing)

            Me.Embeddings = embeddings
        End Sub

#End Region

    End Class

#End Region

End Namespace
