
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Text.Json
Imports System.Text.Json.Serialization

Imports Ollamantis.Core

#End Region

Namespace Entities

#Region " FunctionDefinition "

    ''' <summary>
    ''' Describes a callable function exposed to an Ollama model via a <see cref="Tool"/>.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#tool-calling">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("Name = {Me.Name}, Parameters = {Me.Parameters.GetRawText()}")>
    Public Class FunctionDefinition : Inherits JsonObjectBase

#Region " Properties "

        ''' <summary>
        ''' Gets or sets the name of the function.
        ''' </summary>
        <JsonPropertyOrder(-1)> ' Forces this property to be serialized first (at top of JSON).
        <JsonPropertyName("name")>
        <DisplayName("Name")>
        <Description("The name of the function.")>
        Public Property Name As String

        ''' <summary>
        ''' Gets or sets the description of what the function does.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("description")>
        <DisplayName("Description")>
        <Description("The description of what the function does.")>
        Public Property Description As String

        ''' <summary>
        ''' Gets or sets the JSON Schema object describing the function's parameters.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingDefault)>
        <JsonPropertyName("parameters")>
        <DisplayName("Parameters")>
        <Description("The JSON Schema object describing the function's parameters.")>
        Public Property Parameters As JsonElement

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="FunctionDefinition"/> class.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="FunctionDefinition"/> class.
        ''' </summary>
        ''' 
        ''' <param name="name">
        ''' The name of the function. 
        ''' </param>
        ''' 
        ''' <param name="description">
        ''' The description of what the function does. 
        ''' </param>
        ''' 
        ''' <param name="parameters">
        ''' The JSON Schema object describing the function's parameters. 
        ''' </param>
        Public Sub New(name As String,
                       description As String,
                       parameters As JsonElement)

            Me.Name = name
            Me.Description = description
            Me.Parameters = parameters
        End Sub

#End Region

    End Class

#End Region

End Namespace
