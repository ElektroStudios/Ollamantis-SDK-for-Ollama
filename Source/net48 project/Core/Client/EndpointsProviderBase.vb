
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics

Imports Ollamantis.Core.Helpers

#End Region

Namespace Core

#Region " EndpointsProviderBase "

    ''' <summary>
    ''' Provides the base implementation for an Ollama endpoints provider.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <DebuggerStepThrough>
    Public MustInherit Class EndpointsProviderBase

#Region " Protected Fields "

        ''' <summary>
        ''' The underlying <see cref="OllamaClient"/> instance containing the connection settings and the initialized HTTP client.
        ''' </summary>
        Protected ReadOnly Client As OllamaClient

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="EndpointsProviderBase"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="EndpointsProviderBase"/> class.
        ''' </summary>
        ''' 
        ''' <param name="client">
        ''' The <see cref="OllamaClient"/> containing the connection settings and the initialized HTTP client.
        ''' </param>
        Protected Friend Sub New(client As OllamaClient)

            ArgumentValidator.ThrowIfNull(client, NameOf(client))

            Me.Client = client
        End Sub

#End Region

    End Class

#End Region

End Namespace
