
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

#Region " EmbeddingsRequest "

    ''' <summary>
    ''' Represents the request to generate an embeddings response from a specified Ollama model.
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
    Public Class EmbeddingsRequest : Inherits GenerationRequestBase

#Region " Properties "

        ''' <summary>
        ''' Mandatory. Gets or sets an array of input strings to generate embeddings for.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.Never)>
        <JsonPropertyName("input")>
        <DisplayName("Input")>
        <Description("The input strings to generate embeddings for.")>
        Public Property Inputs As String()

        ''' <summary>
        ''' Optional. Gets or sets a <see langword="Boolean"/> value indicating whether to 
        ''' truncate the end of each input string to fit within context length. 
        ''' <para></para>
        ''' Note: Returns error if <see langword="False"/> and context length is exceeded.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <see langword="True"/>.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("truncate")>
        <DisplayName("Truncate")>
        <Description("A boolean value indicating whether to truncate the end of each input string to fit within context length.")>
        Public Property Truncate As Boolean?

        ''' <summary>
        ''' Optional. Gets or sets the number of dimensions for the embeddings.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("dimensions")>
        <DisplayName("Dimensions")>
        <Description("The number of dimensions for the embeddings.")>
        Public Property Dimensions As Integer?

        ''' <summary>
        ''' Gets the string to display in the debugger variable windows.
        ''' </summary>
        <Browsable(False)>
        <DebuggerBrowsable(DebuggerBrowsableState.Never)>
        Protected Overrides ReadOnly Property DebuggerDisplay As String
            Get
                Dim baseDisplay As String = MyBase.DebuggerDisplay

                Return $"{baseDisplay}, Truncate = {Me.Truncate}, Dimensions = {Me.Dimensions}"
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="EmbeddingsRequest"/> class.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="EmbeddingsRequest"/> class.
        ''' </summary>
        ''' 
        ''' <param name="model">
        ''' Mandatory. The name of the model to generate embeddings (e.g., "<c>llama3.2</c>").
        ''' </param>
        ''' 
        ''' <param name="inputs">
        ''' Mandatory. An array of input strings to generate embeddings for.
        ''' </param>
        ''' 
        ''' <param name="truncate">
        ''' Optional. A <see langword="Boolean"/> value indicating whether to 
        ''' truncate the end of each input string to fit within context length. 
        ''' <para></para>
        ''' Note: Returns error if <see langword="False"/> and context length is exceeded.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <see langword="True"/>.
        ''' </param>
        ''' 
        ''' <param name="options">
        ''' Optional. Additional model parameters for generation.
        ''' <para></para>
        ''' Default value is null.
        ''' </param>
        ''' 
        ''' <param name="keepAlive">
        ''' Optional. A value indicating how long the model will stay loaded into memory following the request.
        ''' <para></para>
        ''' Default value is null, which defaults to "<c>5m</c>".
        ''' </param>
        ''' 
        ''' <param name="dimensions">
        ''' Optional. The number of dimensions for the embeddings.
        ''' </param>
        Public Sub New(model As String,
                       inputs As String(),
              Optional truncate As Boolean? = Nothing,
              Optional options As GenerationOptions = Nothing,
              Optional keepAlive As KeepAliveOption = Nothing,
              Optional dimensions As Integer? = Nothing)

            Me.Model = model
            Me.Inputs = inputs
            Me.Truncate = truncate
            Me.Options = options
            Me.KeepAlive = keepAlive
            Me.Dimensions = dimensions
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="EmbeddingsRequest"/> class.
        ''' </summary>
        ''' 
        ''' <param name="model">
        ''' Mandatory. The name of the model to generate embeddings (e.g., "<c>llama3.2</c>").
        ''' </param>
        ''' 
        ''' <param name="input">
        ''' Mandatory. The input string to generate embeddings for.
        ''' </param>
        ''' 
        ''' <param name="truncate">
        ''' Optional. A <see langword="Boolean"/> value indicating whether to 
        ''' truncate the end of each input string to fit within context length. 
        ''' <para></para>
        ''' Note: Returns error if <see langword="False"/> and context length is exceeded.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <see langword="True"/>.
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
        ''' <param name="dimensions">
        ''' Optional. The number of dimensions for the embeddings.
        ''' </param>
        Public Sub New(model As String,
                       input As String,
              Optional truncate As Boolean? = Nothing,
              Optional options As GenerationOptions = Nothing,
              Optional keepAlive As KeepAliveOption = Nothing,
              Optional dimensions As Integer? = Nothing)

            Me.New(model:=model,
                   inputs:={input},
                   truncate:=truncate,
                   options:=options,
                   keepAlive:=keepAlive,
                   dimensions:=dimensions)
        End Sub

#End Region

    End Class

#End Region

End Namespace
