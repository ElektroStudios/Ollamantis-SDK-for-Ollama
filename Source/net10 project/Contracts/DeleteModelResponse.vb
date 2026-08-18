
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

#Region " DeleteModelResponse "

    ''' <summary>
    ''' Represents the response containing the result of a <see cref="DeleteModelRequest"/>.
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
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public Class DeleteModelResponse : Inherits ResponseBase

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="DeleteModelResponse"/> class.
        ''' </summary>
        <JsonConstructor>
        Public Sub New()
        End Sub

#End Region

    End Class

#End Region

End Namespace