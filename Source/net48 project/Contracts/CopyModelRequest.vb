
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

#Region " CopyModelRequest "

    ''' <summary>
    ''' Represents the request to create a copy of an existing model in your local Ollama storage under a new name.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#copy-a-model">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("Source = {Me.SourceName}, Destination = {Me.DestinationName}")>
    Public Class CopyModelRequest : Inherits JsonObjectBase

#Region " Properties "

        ''' <summary>
        ''' Mandatory. The name of the source model to copy to.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.Never)>
        <JsonPropertyName("source")>
        <DisplayName("Source Name")>
        <Description("Mandatory. The name of the source model to copy to.")>
        Public Property SourceName As String

        ''' <summary>
        ''' Mandatory. The new name for the destination copy.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.Never)>
        <JsonPropertyName("destination")>
        <DisplayName("Destination Name")>
        <Description("Mandatory. The new name for the destination copy.")>
        Public Property DestinationName As String

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="CopyModelRequest"/> class.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="CopyModelRequest"/> class.
        ''' </summary>
        ''' 
        ''' <param name="sourceName">
        ''' Mandatory. The name of the source model to copy to.
        ''' </param>
        ''' 
        ''' <param name="destinationName">
        ''' Mandatory. The new name for the destination copy.
        ''' </param>
        Public Sub New(sourceName As String,
                       destinationName As String)

            Me.SourceName = sourceName
            Me.DestinationName = destinationName
        End Sub

#End Region

    End Class

#End Region

End Namespace
