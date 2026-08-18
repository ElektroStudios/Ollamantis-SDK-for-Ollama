
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

#Region " ShowModelResponse "

    ''' <summary>
    ''' Represents the response containing the result of a <see cref="ShowModelRequest"/>.
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
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public Class ShowModelResponse : Inherits ResponseBase

#Region " Properties "

        ''' <summary>
        ''' Gets the contents of the Modelfile.
        ''' </summary>
        <JsonPropertyOrder(-1)> ' Forces this property to be serialized first (at top of JSON).
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("modelfile")>
        <DisplayName("Modelfile")>
        <Description("The contents of the Modelfile.")>
        Public ReadOnly Property Modelfile As String

        ''' <summary>
        ''' Gets the parameters of the model.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("parameters")>
        <DisplayName("Parameters")>
        <Description("The parameters of the model.")>
        Public ReadOnly Property Parameters As String

        ''' <summary>
        ''' Gets the prompt template to use (overrides what is defined in the Modelfile).
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("template")>
        <DisplayName("Template")>
        <Description("The prompt template to use (overrides what is defined in the Modelfile).")>
        Public ReadOnly Property Template As String

        ''' <summary>
        ''' Gets additional details of the model.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("details")>
        <DisplayName("Details")>
        <Description("Additional details of the model")>
        Public ReadOnly Property Details As ModelDetails

        ''' <summary>
        ''' Gets a <see cref="Dictionary(Of String, Object)"/> containing 
        ''' advanced architecture and tokenizer information of the model.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("model_info")>
        <DisplayName("Model Info")>
        <Description("The advanced architecture and tokenizer information of the model.")>
        Public ReadOnly Property ModelInfo As Dictionary(Of String, Object)

        ''' <summary>
        ''' Gets the model capabilities, such as "<c>completion</c>" or "<c>vision</c>".
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("capabilities")>
        <DisplayName("Capabilities")>
        <Description("The model capabilities, such as ""completion"", or ""vision"".")>
        Public ReadOnly Property Capabilities As String()

        ''' <summary>
        ''' Gets the model capabilities, such as "<c>chat</c>", "<c>completion</c>", or "<c>vision</c>" 
        ''' (from <see cref="ShowModelResponse.Capabilities"/> property),
        ''' formatted as a single-line string representation.
        ''' </summary>
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        <DebuggerBrowsable(DebuggerBrowsableState.Never)>
        <JsonIgnore(Condition:=JsonIgnoreCondition.Always)>
        Private ReadOnly Property CapabilitiesFormatted As String
            Get
                Return If(Me.Capabilities Is Nothing OrElse Me.Capabilities.Length = 0,
                         "None",
                         String.Join(", ", Me.Capabilities))
            End Get
        End Property

        ''' <summary>
        ''' Gets the string to display in the debugger variable windows.
        ''' </summary>
        <Browsable(False)>
        <DebuggerBrowsable(DebuggerBrowsableState.Never)>
        Protected Overrides ReadOnly Property DebuggerDisplay As String
            Get
                Dim baseDisplay As String = MyBase.DebuggerDisplay

                Return $"{baseDisplay}, Parameters = {Me.Parameters}, CapabilitiesFormatted = {Me.CapabilitiesFormatted}"
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="ShowModelResponse"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ShowModelResponse"/> class.
        ''' </summary>
        ''' 
        ''' <param name="modelfile">
        ''' The contents of the Modelfile.
        ''' </param>
        ''' 
        ''' <param name="parameters">
        ''' The parameters of the model.
        ''' </param>
        ''' 
        ''' <param name="template">
        ''' The prompt template to use (overrides what is defined in the Modelfile).
        ''' </param>
        ''' 
        ''' <param name="details">
        ''' Additional details of the model.
        ''' </param>
        ''' 
        ''' <param name="modelInfo">
        ''' A <see cref="Dictionary(Of String, Object)"/> containing 
        ''' advanced architecture and tokenizer information of the model.
        ''' </param>
        ''' 
        ''' <param name="capabilities">
        ''' The model capabilities, such as "<c>chat</c>", "<c>completion</c>", or "<c>vision</c>".
        ''' </param>
        <JsonConstructor>
        Public Sub New(modelfile As String,
                       parameters As String,
                       template As String,
                       details As ModelDetails,
                       modelInfo As Dictionary(Of String, Object),
                       capabilities As String())

            Me.Modelfile = modelfile
            Me.Parameters = parameters
            Me.Template = template
            Me.Details = details
            Me.ModelInfo = modelInfo
            Me.Capabilities = capabilities
        End Sub

#End Region

    End Class

#End Region

End Namespace
