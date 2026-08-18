
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

Namespace Entities

#Region " ModelDetails "

    ''' <summary>
    ''' Represents additional details about an Ollama model.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#show-model-information">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("ParentModel = {Me.ParentModel}, Format = {Me.Format}, Family = {Me.Family}, ParameterSize = {Me.ParameterSize}, QuantizationLevel = {Me.QuantizationLevel}")>
    Public Class ModelDetails : Inherits JsonObjectBaseImmutable

#Region " Properties "

        ''' <summary>
        ''' Gets the parent model (if any) from which the current model is derived or based on.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("parent_model")>
        <DisplayName("Parent Model")>
        <Description("The parent model (if any) from which the current model is derived or based on.")>
        Public ReadOnly Property ParentModel As String

        ''' <summary>
        ''' Gets the format of the model (e.g., "gguf").
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("format")>
        <DisplayName("Format")>
        <Description("The format of the model (e.g., ""gguf"").")>
        Public ReadOnly Property Format As String

        ''' <summary>
        ''' Gets the primary architectural family of the model (e.g., "llama", "GPT").
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("family")>
        <DisplayName("Family")>
        <Description("The primary architectural family of the model (e.g., ""llama"", ""GPT"").")>
        Public ReadOnly Property Family As String

        ''' <summary>
        ''' Gets a <see cref="String"/> array containing all architectural families the model belongs to.
        ''' <para></para>
        ''' This is particularly useful for multimodal or hybrid models that integrate multiple architectures 
        ''' (e.g., {"llama", "clip"}).
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("families")>
        <DisplayName("Families")>
        <Description("All architectural families the model belongs to (e.g., {""llama"", ""clip""}.")>
        Public ReadOnly Property Families As String()

        ''' <summary>
        ''' Gets the number of parameters in the model, which can affect its performance and capabilities.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("parameter_size")>
        <DisplayName("Parameter Size")>
        <Description("The number of parameters in the model, which can affect its performance and capabilities.")>
        Public ReadOnly Property ParameterSize As String

        ''' <summary>
        ''' Gets the level of quantization applied to the model, which can affect its size and performance.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("quantization_level")>
        <DisplayName("Quantization Level")>
        <Description("The level of quantization applied to the model, which can affect its size and performance.")>
        Public ReadOnly Property QuantizationLevel As String

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="ModelDetails"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ModelDetails"/> class.
        ''' </summary>
        ''' 
        ''' <param name="parentModel">
        ''' The parent model (if any) from which the current model is derived or based on.
        ''' </param>
        ''' 
        ''' <param name="format">
        ''' The format of the model (e.g., "gguf").
        ''' </param>
        ''' 
        ''' <param name="family">
        ''' The primary architectural family of the model (e.g., "llama", "GPT").
        ''' </param>
        ''' 
        ''' <param name="families">
        ''' A <see cref="String"/> array containing all architectural families the model belongs to.
        ''' <para></para>
        ''' This is particularly useful for multimodal or hybrid models that integrate multiple architectures 
        ''' (e.g., {"llama", "clip"}).
        ''' </param>
        ''' 
        ''' <param name="parameterSize">
        ''' The number of parameters in the model, which can affect its performance and capabilities.
        ''' </param>
        ''' 
        ''' <param name="quantizationLevel">
        ''' The level of quantization applied to the model, which can affect its size and performance.
        ''' </param>
        <JsonConstructor>
        Public Sub New(parentModel As String,
                       format As String,
                       family As String,
                       families As String(),
                       parameterSize As String,
                       quantizationLevel As String)

            Me.ParentModel = parentModel
            Me.Format = format
            Me.Family = family
            Me.Families = families
            Me.ParameterSize = parameterSize
            Me.QuantizationLevel = quantizationLevel
        End Sub

#End Region

    End Class

#End Region

End Namespace
