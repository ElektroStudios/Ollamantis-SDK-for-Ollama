
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

#Region " PushModelResponse "

    ''' <summary>
    ''' Represents the response containing the result of a <see cref="PushModelRequest"/>.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#push-a-model">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public Class PushModelResponse : Inherits ModelTransferResponseBase

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="PushModelResponse"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="PushModelResponse"/> class.
        ''' </summary>
        ''' 
        ''' <param name="status">
        ''' The status of the model being pushed.
        ''' </param>
        ''' 
        ''' <param name="digest">
        ''' The expected <c>SHA-256</c> digest of the model file, 
        ''' used to verify the integrity of the file.
        ''' </param>
        ''' 
        ''' <param name="totalSize">
        ''' The file size of the model in bytes, or null if the size is unknown.
        ''' </param>
        ''' 
        ''' <param name="completedSize">
        ''' The actually completed size of the model in bytes, or null if the size is unknown.
        ''' </param>
        <JsonConstructor>
        Public Sub New(status As String,
                       digest As String,
                       totalSize As Long?,
                       completedSize As Long?)

            MyBase.New(status:=status,
                       digest:=digest,
                       totalSize:=totalSize,
                       completedSize:=completedSize)
        End Sub

#End Region

    End Class

#End Region

End Namespace
