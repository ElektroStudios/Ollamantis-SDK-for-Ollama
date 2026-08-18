
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

Imports Ollamantis.Contracts
Imports Ollamantis.Core


#End Region

Namespace Entities

#Region " GenerationOptions "

    ''' <summary>
    ''' Represents the 'options' option in a <see cref="GenerationRequestBase"/>.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/modelfile.mdx#valid-parameters-and-values">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression")>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("UseMmap = {Me.UseMmap}, Temperature = {Me.Temperature}, Seed = {Me.Seed}, ContextSize = {Me.ContextSize}, MaxTokens = {Me.MaxTokens}, DraftMaxTokens = {Me.DraftMaxTokens}")>
    Public Class GenerationOptions : Inherits JsonObjectBase

#Region " Properties "

        ''' <summary>
        ''' Gets or sets the size of the context window used to generate the next token.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is dynamically set (<c>int(envconfig.ContextLength())</c>).
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("num_ctx")>
        <DisplayName("Size Context")>
        <Description("The size of the context window used to generate the next token. (Default value in Ollama is dynamically set)")>
        Public Property ContextSize As Integer?

        ''' <summary>
        ''' Gets or sets how far back for the model to look back to prevent repetition.
        ''' <para></para>
        ''' A value of <c>0</c> (zero) disables repetition prevention, 
        ''' while a value of <c>-1</c> uses the entire context window (<see cref="GenerationOptions.ContextSize"/>) 
        ''' for repetition prevention.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>64</c>.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("repeat_last_n")>
        <DisplayName("Repeat Last N")>
        <Description("How far back for the model to look back to prevent repetition. A value of 0 (zero) disables repetition prevention, while a value of -1 uses the entire context window. (Default value in Ollama: 64)")>
        Public Property RepeatLastN As Integer?

        ''' <summary>
        ''' Gets or sets a value indicating how strongly to penalize repetitions. 
        ''' <para></para>
        ''' A higher value (e.g., <c>1.5</c>) will penalize repetitions more strongly, 
        ''' while a lower value (e.g., <c>0.9</c>) will be more lenient.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>1.0</c> (disabled).
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("repeat_penalty")>
        <DisplayName("Repeat Penalty")>
        <Description("A value indicating how strongly to penalize repetitions. A higher value (e.g., 1.5) will penalize repetitions more strongly, while a lower value (e.g., 0.9) will be more lenient. (Default value in Ollama: 1.0, disabled)")>
        Public Property RepeatPenalty As Double?

        ''' <summary>
        ''' Gets or sets the temperature of the model. Increasing the temperature will make the model answer more creatively.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>0.8</c>.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("temperature")>
        <DisplayName("Temperature")>
        <Description("The temperature of the model. Increasing the temperature will make the model answer more creatively. (Default value in Ollama: 0.8)")>
        Public Property Temperature As Double?

        ''' <summary>
        ''' Gets or sets the random number seed to use for generation. 
        ''' <para></para>
        ''' Setting this to a specific number will make the model generate the same text for the same prompt.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>-1</c> (random seed).
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("seed")>
        <DisplayName("Seed")>
        <Description("The random number seed to use for generation. Setting this to a specific number will make the model generate the same text for the same prompt. (Default value in Ollama: 0)")>
        Public Property Seed As Integer?

        ''' <summary>
        ''' Gets or sets the stop sequences to use (e.g., <c>New String() {"<c>AI assistant:</c>"}</c>).
        ''' <para></para>
        ''' When this pattern is encountered the LLM will stop generating text and return. 
        ''' Multiple stop patterns may be set by specifying multiple separate stop parameters in a modelfile.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is null.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("stop")>
        <DisplayName("Stop Sequences")>
        <Description("The stop sequences to use. When this pattern is encountered the LLM will stop generating text and return. Multiple stop patterns may be set by specifying multiple separate stop parameters in a modelfile. (Default value in Ollama: null)")>
        Public Property StopSequences As String()

        ''' <summary>
        ''' Gets or sets the Maximum number of tokens to predict when generating text.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>-1</c> (infinite generation).
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("num_predict")>
        <DisplayName("Max Tokens")>
        <Description("The maximum number of tokens to predict when generating text. (Default value in Ollama: -1, infinite generation)")>
        Public Property MaxTokens As Integer?

        ''' <summary>
        ''' Gets or sets the maximum number of speculative draft tokens to predict per step when a draft model is available.
        ''' <para></para>
        ''' Embedded MTP tensors require setting this parameter. Set to <c>0</c> (zero) to disable speculative drafting.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>4</c>.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("draft_num_predict")>
        <DisplayName("Draft Max Tokens")>
        <Description("Maximum number of speculative draft tokens to predict per step when a draft model is available. Embedded MTP tensors require setting this parameter. Set to 0 (zero) to disable speculative drafting. (Default value in Ollama: 4)")>
        Public Property DraftMaxTokens As Integer?

        ''' <summary>
        ''' Gets or sets the <c>Top-K</c> sampling, which reduces the probability of generating nonsense.
        ''' <para></para>
        ''' A higher value (e.g., <c>100</c>) will give more diverse answers, 
        ''' while a lower value (e.g., <c>10</c>) will be more conservative.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>40</c>.
        ''' </summary>
        <JsonPropertyName("top_k")>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <DisplayName("Top-K")>
        <Description("The Top-K sampling, which reduces the probability of generating nonsense. A higher value (e.g., 100) will give more diverse answers, while a lower value (e.g., 10) will be more conservative. (Default value in Ollama: 40)")>
        Public Property TopK As Integer?

        ''' <summary>
        ''' Gets or sets the <c>Top-P</c> sampling, which works together with <see cref="GenerationOptions.TopK"/>. 
        ''' <para></para>
        ''' A higher value (e.g., <c>0.95</c>) will lead to more diverse text, 
        ''' while a lower value (e.g., <c>0.5</c>) will generate more focused and conservative text.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>0.9</c>.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("top_p")>
        <DisplayName("Top-P")>
        <Description("The Top-P sampling, which works together with Top-K. A higher value (e.g., 0.95) will lead to more diverse text, while a lower value (e.g., 0.5) will generate more focused and conservative text. (Default value in Ollama: 0.9)")>
        Public Property TopP As Double?

        ''' <summary>
        ''' Gets or sets the <c>Min-P</c> sampling, that is an alternative to the <see cref="GenerationOptions.TopP"/>, 
        ''' and aims to ensure a balance of quality and variety.
        ''' <para></para>
        ''' <c>Min-P</c> represents the minimum probability for a token to be considered, 
        ''' relative to the probability of the most likely token. 
        ''' For example, with a value of <c>0.05</c> and the most likely token having a probability of <c>0.9</c>, 
        ''' logits with a value less than <c>0.045</c> are filtered out. 
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>0.0</c>.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("min_p")>
        <DisplayName("Min-P")>
        <Description("The Min-P sampling, that is an alternative to the Top-P, and aims to ensure a balance of quality and variety. Min-P represents the minimum probability for a token to be considered, relative to the probability of the most likely token. For example, with a value of 0.05 and the most likely token having a probability of 0.9, logits with a value less than 0.045 are filtered out. (Default value in Ollama: 0.0)")>
        Public Property MinP As Double?

#Region " Properties supported but not listed in the 'modelfile.mdx#valid-parameters-and-values' "

        ' Listed under the "every available option" example in Ollama's API documentation:
        ' https://github.com/ollama/ollama/blob/main/docs/api.md#generate-request-with-options

        ' Defined under the 'func DefaultOptions() Options' function:
        ' https://github.com/ollama/ollama/blob/main/api/types.go#L1096

        ''' <summary>
        ''' Gets or sets the typicality parameter for text generation.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>1.0</c>.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("typical_p")>
        <DisplayName("Typical-P")>
        <Description("The typicality parameter for text generation. (Default value in Ollama: 1.0)")>
        Public Property TypicalP As Double?

        ''' <summary>
        ''' Gets or sets the penalty applied to new tokens based on whether they appear in the text so far.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>0.0</c>.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("presence_penalty")>
        <DisplayName("Presence Penalty")>
        <Description("The penalty applied to new tokens based on whether they appear in the text so far. (Default value in Ollama: 0.0)")>
        Public Property PresencePenalty As Double?

        ''' <summary>
        ''' Gets or sets the penalty applied to new tokens based on their existing frequency in the text so far.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>0.0</c>.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("frequency_penalty")>
        <DisplayName("Frequency Penalty")>
        <Description("The penalty applied to new tokens based on their existing frequency in the text so far. (Default value in Ollama: 0.0)")>
        Public Property FrequencyPenalty As Double?

        ''' <summary>
        ''' Gets or sets a <see cref="Boolean"/> value indicating whether to penalize newline tokens.
        ''' </summary>
        ''' 
        ''' <remarks>
        ''' This parameter is deprecated by the Ollama API and is ignored by the server
        ''' <para></para>
        ''' For additional information, see the 
        ''' <see href="https://github.com/ollama/ollama/blob/main/parser/parser.go">parser.go source-code</see>: 
        ''' <para></para>
        ''' <code>var deprecatedParameters = []string{
        ''' 	"penalize_newline"
        ''' 	...
        ''' }</code>
        ''' </remarks>
        <SuppressMessage("Major Code Smell", "S1133:Deprecated code should be removed", Justification:="Maintained for backward compatibility with older Ollama API versions.")>
        <Obsolete("This parameter is deprecated by the Ollama API and is ignored by the server.", False)>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("penalize_newline")>
        <DisplayName("Penalize Newline")>
        <Description("Deprecated. A boolean value indicating whether to penalize newline tokens.")>
        Public Property PenalizeNewline As Boolean?

        ''' <summary>
        ''' Gets or sets the number of tokens to keep from the prompt.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>4</c>.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("num_keep")>
        <DisplayName("Num Keep")>
        <Description("The number of tokens to keep from the prompt. (Default value in Ollama: 4)")>
        Public Property NumKeep As Integer?

        ''' <summary>
        ''' Gets or sets the maximum number of prompt tokens to batch together when evaluating.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>512</c>.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("num_batch")>
        <DisplayName("Num Batch")>
        <Description("The maximum number of prompt tokens to batch together when evaluating. (Default value in Ollama: 512)")>
        Public Property NumBatch As Integer?

        ''' <summary>
        ''' Gets or sets the number of layers to send to the GPU(s).
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>-1</c> 
        ''' (indicating that NumGPU should be set dynamically by the runtime).
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("num_gpu")>
        <DisplayName("Num GPU")>
        <Description("The number of layers to send to the GPU(s). (Default value in Ollama: -1, indicating that NumGPU should be set dynamically by the runtime)")>
        Public Property NumGPU As Integer?

        ''' <summary>
        ''' Gets or sets the GPU index used for small tensors when using multiple GPUs.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is set dynamically by the runtime.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("main_gpu")>
        <DisplayName("Main GPU")>
        <Description("The GPU index used for small tensors when using multiple GPUs. (Default value in Ollama is set dynamically by the runtime)")>
        Public Property MainGPU As Integer?

        ''' <summary>
        ''' Gets or sets the number of threads to use during computation.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>0</c> 
        ''' (indicating that NumThread should be set dynamically by the runtime).
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("num_thread")>
        <DisplayName("Num Thread")>
        <Description("The number of threads to use during computation. (Default value in Ollama: 0 - indicating that NumThread should be set dynamically by the runtime)")>
        Public Property NumThread As Integer?

        ''' <summary>
        ''' Gets or sets a <see cref="Boolean"/> value indicating whether the model should be mapped into memory.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <see langword="False"/>.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("use_mmap")>
        <DisplayName("Use Mmap")>
        <Description("A boolean value indicating whether the model should be mapped into memory. (Default value in Ollama: False)")>
        Public Property UseMmap As Boolean?

        ''' <summary>
        ''' Gets or sets a value indicating whether NUMA (Non-Uniform Memory Access) optimization is enabled.
        ''' </summary>
        ''' 
        ''' <remarks>
        ''' This parameter is deprecated by the Ollama API and is ignored by the server.
        ''' </remarks>
        <SuppressMessage("Major Code Smell", "S1133:Deprecated code should be removed", Justification:="Maintained for backward compatibility with older Ollama API versions.")>
        <Obsolete("This parameter is deprecated by the Ollama API and is ignored by the server.", False)>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("numa")>
        <DisplayName("Numa")>
        <Description("Deprecated. A value indicating whether NUMA (Non-Uniform Memory Access) optimization is enabled.")>
        Public Property Numa As Boolean?

#End Region

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="GenerationOptions"/> class.
        ''' </summary>
        Public Sub New()
        End Sub

#Disable Warning S117 ' Local variables should be camelCase

        ''' <summary>
        ''' Initializes a new instance of the <see cref="GenerationOptions"/> class with optional parameters.
        ''' </summary>
        ''' 
        ''' <param name="ContextSize">
        ''' The size of the context window used to generate the next token.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is dynamically set (<c>int(envconfig.ContextLength())</c>).
        ''' </param>
        ''' 
        ''' <param name="repeatLastN">
        ''' A value indicating how far back for the model to look back to prevent repetition.
        ''' <para></para>
        ''' A value of <c>0</c> (zero) disables repetition prevention, 
        ''' while a value of <c>-1</c> uses the entire context window (<see cref="GenerationOptions.ContextSize"/>) 
        ''' for repetition prevention.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>64</c>.
        ''' </param>
        ''' 
        ''' <param name="repeatPenalty">
        ''' A value indicating how strongly to penalize repetitions. 
        ''' <para></para>
        ''' A higher value (e.g., <c>1.5</c>) will penalize repetitions more strongly, 
        ''' while a lower value (e.g., <c>0.9</c>) will be more lenient.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>1.0</c> (disabled).
        ''' </param>
        ''' 
        ''' <param name="temperature">
        ''' The temperature of the model. Increasing the temperature will make the model answer more creatively.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>0.8</c>.
        ''' </param>
        ''' 
        ''' <param name="seed">
        ''' The random number seed to use for generation. 
        ''' <para></para>
        ''' Setting this to a specific number will make the model generate the same text for the same prompt.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>-1</c> (random seed).
        ''' </param>
        ''' 
        ''' <param name="stopSequences">
        ''' The stop sequences to use (e.g., <c>New String() {"<c>AI assistant:</c>"}</c>).
        ''' <para></para>
        ''' When this pattern is encountered the LLM will stop generating text and return. 
        ''' Multiple stop patterns may be set by specifying multiple separate stop parameters in a modelfile.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is null.
        ''' </param>
        ''' 
        ''' <param name="maxTokens">
        ''' The Maximum number of tokens to predict when generating text.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>-1</c> (infinite generation).
        ''' </param>
        ''' 
        ''' <param name="draftMaxTokens">
        ''' The maximum number of speculative draft tokens to predict per step when a draft model is available.
        ''' <para></para>
        ''' Embedded MTP tensors require setting this parameter. Set to <c>0</c> (zero) to disable speculative drafting.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>4</c>.
        ''' </param>
        ''' 
        ''' <param name="topK">
        ''' The <c>Top-K</c> sampling, which reduces the probability of generating nonsense.
        ''' <para></para>
        ''' A higher value (e.g., <c>100</c>) will give more diverse answers, 
        ''' while a lower value (e.g., <c>10</c>) will be more conservative.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>40</c>.
        ''' </param>
        ''' 
        ''' <param name="topP">
        ''' The <c>Top-P</c> sampling, which works together with <see cref="GenerationOptions.TopK"/>. 
        ''' <para></para>
        ''' A higher value (e.g., <c>0.95</c>) will lead to more diverse text, 
        ''' while a lower value (e.g., <c>0.5</c>) will generate more focused and conservative text.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>0.9</c>.
        ''' </param>
        ''' 
        ''' <param name="minP">
        ''' The <c>Min-P</c> sampling, that is an alternative to the <see cref="GenerationOptions.TopP"/>, 
        ''' and aims to ensure a balance of quality and variety.
        ''' <para></para>
        ''' <c>Min-P</c> represents the minimum probability for a token to be considered, 
        ''' relative to the probability of the most likely token. 
        ''' For example, with a value of <c>0.05</c> and the most likely token having a probability of <c>0.9</c>, 
        ''' logits with a value less than <c>0.045</c> are filtered out. 
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>0.0</c>.
        ''' </param>
        '''
        ''' <param name="typicalP">
        ''' The typicality parameter for text generation.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>1.0</c>.
        ''' </param>
        '''
        ''' <param name="presencePenalty">
        ''' The penalty applied to new tokens based on whether they appear in the text so far.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>0.0</c>.
        ''' </param>
        '''
        ''' <param name="frequencyPenalty">
        ''' The penalty applied to new tokens based on their existing frequency in the text so far.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>0.0</c>.
        ''' </param>
        '''
        ''' <param name="penalizeNewline">
        ''' A <see cref="Boolean"/> value indicating whether to penalize newline tokens.
        ''' <para></para>
        ''' This parameter is deprecated by the Ollama API and is ignored by the server.
        ''' </param>
        '''
        ''' <param name="numKeep">
        ''' The number of tokens to keep from the prompt.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>4</c>.
        ''' </param>
        '''
        ''' <param name="numBatch">
        ''' The maximum number of prompt tokens to batch together when evaluating.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>512</c>.
        ''' </param>
        '''
        ''' <param name="numGPU">
        ''' The number of layers to send to the GPU(s).
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>-1</c> 
        ''' (indicating that NumGPU should be set dynamically by the runtime).
        ''' </param>
        '''
        ''' <param name="mainGPU">
        ''' The GPU index used for small tensors when using multiple GPUs.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is set dynamically by the runtime.
        ''' </param>
        '''
        ''' <param name="numThread">
        ''' The number of threads to use during computation. 
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <c>0</c> 
        ''' (indicating that NumThread should be set dynamically by the runtime).
        ''' </param>
        '''
        ''' <param name="useMmap">
        ''' A <see cref="Boolean"/> value indicating whether the model should be mapped into memory.
        ''' <para></para>
        ''' If not specified, the default value in Ollama is <see langword="False"/>.
        ''' </param>
        '''
        ''' <param name="numa">
        ''' A value indicating whether NUMA (Non-Uniform Memory Access) optimization is enabled.
        ''' <para></para>
        ''' This parameter is deprecated by the Ollama API and is ignored by the server.
        ''' </param>
        <SuppressMessage("Major Code Smell", "S107:Methods should not have too many parameters", Justification:="Ollama API JSON deserialization requires a large number of parameters.")>
        <SuppressMessage("Naming", "S117:Local variables should be camelCase", Justification:="Parameter names preserve standard mathematical ML notation (e.g., N, K, P) to strictly align with the official Ollama API terminology.")>
        Public Sub New(Optional contextSize As Integer? = Nothing,
                       Optional repeatLastN As Integer? = Nothing,
                       Optional repeatPenalty As Double? = Nothing,
                       Optional temperature As Double? = Nothing,
                       Optional seed As Integer? = Nothing,
                       Optional stopSequences As String() = Nothing,
                       Optional maxTokens As Integer? = Nothing,
                       Optional draftMaxTokens As Integer? = Nothing,
                       Optional topK As Integer? = Nothing,
                       Optional topP As Double? = Nothing,
                       Optional minP As Double? = Nothing,
                       Optional typicalP As Double? = Nothing,
                       Optional presencePenalty As Double? = Nothing,
                       Optional frequencyPenalty As Double? = Nothing,
                       Optional penalizeNewline As Boolean? = Nothing,
                       Optional numKeep As Integer? = Nothing,
                       Optional numBatch As Integer? = Nothing,
                       Optional numGPU As Integer? = Nothing,
                       Optional mainGPU As Integer? = Nothing,
                       Optional numThread As Integer? = Nothing,
                       Optional useMmap As Boolean? = Nothing,
                       Optional numa As Boolean? = Nothing)

#Disable Warning BC40000 ' Type or member is obsolete

            Me.ContextSize = contextSize
            Me.RepeatLastN = repeatLastN
            Me.RepeatPenalty = repeatPenalty
            Me.Temperature = temperature
            Me.Seed = seed
            Me.StopSequences = stopSequences
            Me.MaxTokens = maxTokens
            Me.DraftMaxTokens = draftMaxTokens
            Me.TopK = topK
            Me.TopP = topP
            Me.MinP = minP
            Me.TypicalP = typicalP
            Me.PresencePenalty = presencePenalty
            Me.FrequencyPenalty = frequencyPenalty
            Me.NumKeep = numKeep
            Me.NumBatch = numBatch
            Me.NumGPU = numGPU
            Me.MainGPU = mainGPU
            Me.NumThread = numThread
            Me.UseMmap = useMmap

            ' Deprecated property parameters:
            Me.PenalizeNewline = penalizeNewline
            Me.Numa = numa

#Enable Warning BC40000 ' Type or member is obsolete
        End Sub

#Enable Warning S117 ' Local variables should be camelCase

#End Region

    End Class

#End Region

End Namespace
