
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

#Region " PushModelRequest "

    ''' <summary>
    ''' Represents the request to upload (push) a model to the Ollama library.
    ''' Requires registering for ollama.ai and adding a public key first.
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
    Public Class PushModelRequest : Inherits ModelTransferRequestBase

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="PushModelRequest"/> class.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="PushModelRequest"/> class.
        ''' </summary>
        ''' 
        ''' <param name="name">
        ''' Mandatory. The name of the model to push, in the form of <c>&lt;namespace&gt;/&lt;model&gt;:&lt;tag&gt;</c>.
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
