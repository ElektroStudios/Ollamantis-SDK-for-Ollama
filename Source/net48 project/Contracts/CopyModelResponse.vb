
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

#Region " CopyModelResponse "

    ''' <summary>
    ''' Represents the response containing the result of a <see cref="CopyModelRequest"/>.
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
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public Class CopyModelResponse : Inherits ResponseBase

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="CopyModelResponse"/> class.
        ''' </summary>
        <JsonConstructor>
        Public Sub New()
        End Sub

#End Region

    End Class

#End Region

End Namespace
