
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

Namespace Contracts

#Region " ShowModelRequest "

    ''' <summary>
    ''' Represents the request to retrieve the information of a specific model in the Ollama API.
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
    <DebuggerDisplay("Name = {Me.Name}, Verbose = {Me.Verbose}")>
    Public Class ShowModelRequest : Inherits JsonObjectBase

#Region " Properties "

        ''' <summary>
        ''' Mandatory. Gets or sets the name of the model for which to retrieve its details (e.g., "<c>llama3.2</c>")
        ''' </summary>
        <JsonPropertyOrder(-1)> ' Forces this property to be serialized first (at top of JSON).
        <JsonIgnore(Condition:=JsonIgnoreCondition.Never)>
        <JsonPropertyName("name")>
        <DisplayName("Name")>
        <Description("Mandatory. The name of the model for which to retrieve its details (e.g., ""llama3.2"").")>
        Public Property Name As String

        ''' <summary>
        ''' Optional. Gets or sets a <see cref="Boolean"/> value indicating whether to 
        ''' return full data for verbose fields in the response (<see langword="True"/>), 
        ''' rather than standard data (<see langword="False"/>).
        ''' <para></para>
        ''' Default value is <see langword="False"/>.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingDefault)>
        <JsonPropertyName("verbose")>
        <DisplayName("Verbose")>
        <Description("Optional. A boolean value indicating whether to return full data for verbose fields in the response.")>
        Public Property Verbose As Boolean

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ShowModelRequest"/> class.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ShowModelRequest"/> class.
        ''' </summary>
        ''' 
        ''' <param name="name">
        ''' Mandatory. The name of the model for which to retrieve its details (e.g., "<c>llama3.2</c>").
        ''' </param>
        ''' 
        ''' <param name="verbose">
        ''' Optional. Gets or sets a <see cref="Boolean"/> value indicating whether to return full data for verbose fields in the response.
        ''' <para></para>
        ''' Default value is <see langword="False"/>.
        ''' </param>
        Public Sub New(name As String,
              Optional verbose As Boolean = False)

            Me.Name = name
            Me.Verbose = verbose
        End Sub

#End Region

    End Class

#End Region

End Namespace
