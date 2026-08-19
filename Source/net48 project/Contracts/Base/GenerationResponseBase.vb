
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Diagnostics.CodeAnalysis
Imports System.Globalization
Imports System.Text.Json.Serialization

Imports Ollamantis.Core

#End Region

Namespace Contracts

#Region " GenerationResponseBase "

    ''' <summary>
    ''' Provides the base implementation for generation response contracts.
    ''' </summary>
    <SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression")>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public MustInherit Class GenerationResponseBase : Inherits ResponseBase

#Region " Properties "

        ''' <summary>
        ''' Gets the name of the model used for generation (e.g., "<c>llama3.2</c>").
        ''' </summary>
        <JsonPropertyOrder(-1)> ' Forces this property to be serialized first (at top of JSON).
        <JsonPropertyName("model")>
        <DisplayName("Model")>
        <Description("The model name used for generation (e.g., ""llama3.2"").")>
        Public ReadOnly Property Model As String

        ''' <summary>
        ''' Gets the timestamp when the response was created, 
        ''' formatted as an ISO 8601 date and time string in UTC 
        ''' (e.g., "<c>2024-06-04T14:38:31.83753-07:00</c>").
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("created_at")>
        <DisplayName("Created At (ISO 8601)")>
        <Description("The timestamp when the response was created, formatted as an ISO 8601 date and time string in UTC.")>
        Public ReadOnly Property CreatedAt As DateTimeOffset?

        ''' <summary>
        ''' Gets the timestamp when the response was created, 
        ''' converted to local time and formatted as a human-readable 24-hour string 
        ''' (e.g., "<c>Saturday, August 15, 2026 at 32:17:55</c>").
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("created_at_formatted")>
        <DisplayName("Created At (formatted)")>
        <Description("The timestamp when the response was created, converted to local time and formatted as a human-readable 24-hour string.")>
        Public ReadOnly Property CreatedAtFormatted As String
            Get
                Return MyBase.FormatDateTimeOffset(Me.CreatedAt)
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="Boolean"/> value indicating the completion status of the response generation.
        ''' <para></para>
        ''' If streaming is disabled (<see cref="CompletionRequest.Stream"/> = <see langword="False"/>), 
        ''' this will always be <see langword="True"/> since <see cref="CompletionResponse.Response"/> 
        ''' contains the full streamed response.
        ''' <para></para>
        ''' If streaming is enabled (<see cref="CompletionRequest.Stream"/> = <see langword="True"/>), 
        ''' this can be <see langword="False"/> if the response is still streaming,
        ''' or <see langword="True"/> if the response has finished streaming and 
        ''' <see cref="CompletionResponse.Response"/> contains the final streamed chunk.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("done")>
        <DisplayName("Done")>
        <Description("A boolean value indicating the completion status of the response generation. If streaming is disabled, this will always be True; if streaming is enabled, it can be False if the response is still streaming or True if the response has finished streaming.")>
        Public ReadOnly Property Done As Boolean?

        ''' <summary>
        ''' Gets the completion reason of the response generation.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("done_reason")>
        <DisplayName("Done Reason")>
        <Description("The completion reason of the response generation.")>
        Public ReadOnly Property DoneReason As String

        ''' <summary>
        ''' Gets the total time spent processing the entire request, in nanoseconds.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("total_duration")>
        <DisplayName("Total Duration (in nanoseconds)")>
        <Description("The total time spent processing the entire request, in nanoseconds.")>
        Public ReadOnly Property TotalDuration As Long?

        ''' <summary>
        ''' Gets the total time spent processing the entire request, in a human-readable representation.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("total_duration_formatted")>
        <DisplayName("Total Duration (formatted)")>
        <Description("The total time spent processing the entire request, in a human-readable representation.")>
        Public ReadOnly Property TotalDurationFormatted As String
            Get
                Return GenerationResponseBase.FormatNanoseconds(Me.TotalDuration)
            End Get
        End Property

        ''' <summary>
        ''' Gets the time spent loading the model, in nanoseconds.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("load_duration")>
        <DisplayName("Load Duration (in nanoseconds)")>
        <Description("The time spent loading the model, in nanoseconds.")>
        Public ReadOnly Property LoadDuration As Long?

        ''' <summary>
        ''' Gets the time spent loading the model, in a human-readable representation.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("load_duration_formatted")>
        <DisplayName("Load Duration (formatted)")>
        <Description("The time spent loading the model, in a human-readable representation.")>
        Public ReadOnly Property LoadDurationFormatted As String
            Get
                Return GenerationResponseBase.FormatNanoseconds(Me.LoadDuration)
            End Get
        End Property

        ''' <summary>
        ''' Gets the number of tokens evaluated in the prompt.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("prompt_eval_count")>
        <DisplayName("Prompt Eval Count")>
        <Description("The number of tokens evaluated in the prompt.")>
        Public ReadOnly Property PromptEvalCount As Integer?

        ''' <summary>
        ''' Gets the time spent evaluating the prompt, in nanoseconds.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("prompt_eval_duration")>
        <DisplayName("Prompt Eval Duration (in nanoseconds)")>
        <Description("The time spent evaluating the prompt, in nanoseconds.")>
        Public ReadOnly Property PromptEvalDuration As Long?

        ''' <summary>
        ''' Gets the time spent evaluating the prompt, in a human-readable representation. 
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("prompt_eval_duration_formatted")>
        <DisplayName("Prompt Eval Duration (formatted)")>
        <Description("The time spent evaluating the prompt, in a human-readable representation.")>
        Public ReadOnly Property PromptEvalDurationFormatted As String
            Get
                Return GenerationResponseBase.FormatNanoseconds(Me.PromptEvalDuration)
            End Get
        End Property

        ''' <summary>
        ''' Gets the number of tokens generated in the response.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("eval_count")>
        <DisplayName("Eval Count")>
        <Description("The number of tokens generated in the response.")>
        Public ReadOnly Property EvalCount As Integer?

        ''' <summary>
        ''' Gets the time spent strictly generating the response tokens, in nanoseconds.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("eval_duration")>
        <DisplayName("Eval Duration (in nanoseconds)")>
        <Description("The time spent strictly generating the response tokens, in nanoseconds.")>
        Public ReadOnly Property EvalDuration As Long?

        ''' <summary>
        ''' Gets time spent strictly generating the response tokens, in a human-readable representation.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("eval_duration_formatted")>
        <DisplayName("Eval Duration (formatted)")>
        <Description("The time spent strictly generating the response tokens, in a human-readable representation.")>
        Public ReadOnly Property EvalDurationFormatted As String
            Get
                Return GenerationResponseBase.FormatNanoseconds(Me.EvalDuration)
            End Get
        End Property

        ''' <summary>
        ''' Gets the generation speed of the model in tokens per second (token/s), dynamically calculated using 
        ''' <see cref="GenerationResponseBase.EvalCount"/> and <see cref="GenerationResponseBase.EvalDuration"/>.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("tokens_per_second")>
        <DisplayName("Tokens Per Second")>
        <Description("The text generation speed of the model in tokens per second (token/s).")>
        Public ReadOnly Property TokensPerSecond() As Decimal?
            Get
                If Not Me.EvalCount.HasValue OrElse
                   Not Me.EvalDuration.HasValue OrElse
                       Me.EvalDuration.Value <= 0 Then

                    Return Nothing
                End If

                Dim fullValue As Double = If(Me.EvalDuration.Value > 0,
                                             (Me.EvalCount.Value / Me.EvalDuration.Value) * 1000000000.0R,
                                             0.0R)

                ' Paranoid safety check: Handle arithmetic anomalies and prevent Decimal overflow.
                If Double.IsNaN(fullValue) OrElse
                   Double.IsInfinity(fullValue) Then
                    Return 0.0D

                ElseIf fullValue >= Decimal.MaxValue Then
                    fullValue = Decimal.MaxValue

                ElseIf fullValue <= Decimal.MinValue Then
                    fullValue = Decimal.MinValue

                End If

                ' Convert to Decimal to ensure exact base-10 representation before rounding.
                Dim oneDecimalPlace As Decimal = Math.Round(CDec(fullValue), decimals:=1)

                Return oneDecimalPlace
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

                Return $"{baseDisplay}, Model = {Me.Model}, Done = {Me.Done}, DoneReason = {Me.DoneReason}, TotalDurationFormatted = {Me.TotalDurationFormatted}, PromptEvalCount = {Me.PromptEvalCount}, EvalCount = {Me.EvalCount}, TokensPerSecond = {Me.TokensPerSecond}, CreatedAtFormatted = {Me.CreatedAtFormatted}"
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="CompletionResponse"/> class.
        ''' </summary>
        Protected Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="CompletionResponse"/> class.
        ''' </summary>
        ''' 
        ''' <param name="model">
        ''' The name of the model used for generation (e.g., "<c>llama3.2</c>").
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
                       createdAt As DateTimeOffset?,
                       done As Boolean?, doneReason As String,
                       totalDuration As Long?, loadDuration As Long?,
                       promptEvalCount As Integer?, promptEvalDuration As Long?,
                       evalCount As Integer?, evalDuration As Long?)

            Me.Model = model
            Me.CreatedAt = createdAt
            Me.Done = done
            Me.DoneReason = doneReason
            Me.TotalDuration = totalDuration
            Me.LoadDuration = loadDuration
            Me.PromptEvalCount = promptEvalCount
            Me.PromptEvalDuration = promptEvalDuration
            Me.EvalCount = evalCount
            Me.EvalDuration = evalDuration
        End Sub

#End Region

#Region " Private Methods "

        ''' <summary>
        ''' Converts a nullable <see cref="Long"/> expressed as a duration in nanoseconds, 
        ''' to a human-readable string (e.g., "1m 5s", "12.53s", "450.2ms").
        ''' </summary>
        ''' 
        ''' <param name="nanoseconds">
        ''' The duration in nanoseconds.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A formatted <see cref="String"/> representing the duration.
        ''' </returns>
        Private Shared Function FormatNanoseconds(nanoseconds As Long?) As String

            If Not nanoseconds.HasValue Then
                Return Nothing

            ElseIf nanoseconds <= 0 Then
                Return "0ms"

            End If

            Dim value As Long = nanoseconds.Value

            ' 1 tick in .NET = 100 nanoseconds.
            Dim ts As TimeSpan = TimeSpan.FromTicks(value \ 100L)

            If ts.TotalHours >= 1.0 Then
                Return $"{(Math.Truncate(ts.TotalHours))}h {ts.Minutes}m {ts.Seconds}s"

            ElseIf ts.TotalMinutes >= 1.0 Then
                Return $"{ts.Minutes}m {ts.Seconds}s"

            ElseIf ts.TotalSeconds >= 1.0 Then
                ' Formats to 2 decimal places using invariant culture to ensure dot (.) decimal separator.
                Return $"{ts.TotalSeconds.ToString("0.##", CultureInfo.InvariantCulture)}s"

            Else
                Return $"{ts.TotalMilliseconds.ToString("0.##", CultureInfo.InvariantCulture)}ms"

            End If
        End Function

#End Region

    End Class

#End Region

End Namespace
