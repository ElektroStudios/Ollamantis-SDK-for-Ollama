
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

#Region " PullModelRequest "

    ''' <summary>
    ''' Represents the request to download (pull) a model from the Ollama library.
    ''' <para></para>
    ''' Cancelled pulls are resumed from where they left off, and multiple calls will share the same download progress.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the  
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#pull-a-model">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public Class PullModelRequest : Inherits ModelTransferRequestBase

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="PullModelRequest"/> class.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="PullModelRequest"/> class.
        ''' </summary>
        ''' 
        ''' <param name="name">
        ''' Mandatory. The name of the source model to pull (e.g., "<c>llama3.2</c>").
        ''' </param>
        ''' 
        ''' <param name="insecure">
        ''' Optional. A <see cref="Boolean"/> value indicating whether to allow insecure connections to the library (<see langword="True"/>), or not (<see langword="False"/>).
        ''' <para></para>
        ''' Only set this value to <see langword="True"/> if you are pulling from your own library during development.
        ''' <para></para>
        ''' Default value is <see langword="False"/>.
        ''' </param>
        Public Sub New(name As String,
              Optional insecure As Boolean = False)

            MyBase.New(name:=name, insecure:=insecure)
        End Sub

#End Region

    End Class

#End Region

End Namespace
