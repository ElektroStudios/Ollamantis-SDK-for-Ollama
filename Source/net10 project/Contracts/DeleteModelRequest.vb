
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

#Region " DeleteModelRequest "

    ''' <summary>
    ''' Represents the request to delete an existing model and its associated data from your local Ollama storage.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#delete-a-model">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("Name = {Me.Name}")>
    Public Class DeleteModelRequest : Inherits JsonObjectBase

#Region " Properties "

        ''' <summary>
        ''' Mandatory. Gets or sets the name of the model to delete (e.g., "<c>llama3.2</c>").
        ''' </summary>
        <JsonPropertyOrder(-1)> ' Forces this property to be serialized first (at top of JSON).
        <JsonIgnore(Condition:=JsonIgnoreCondition.Never)>
        <JsonPropertyName("name")>
        <DisplayName("Name")>
        <Description("Mandatory. The name of the model to delete (e.g., ""llama3.2"").")>
        Public Property Name As String

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="DeleteModelRequest"/> class.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="DeleteModelRequest"/> class.
        ''' </summary>
        ''' 
        ''' <param name="name">
        ''' Mandatory. The name of the model to delete (e.g., "<c>llama3.2</c>").
        ''' </param>
        Public Sub New(name As String)

            Me.Name = name
        End Sub

#End Region

    End Class

#End Region

End Namespace
