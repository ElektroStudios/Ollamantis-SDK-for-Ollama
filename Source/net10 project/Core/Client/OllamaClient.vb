
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Net.Http

Imports Ollamantis.Core.Helpers

#End Region

Namespace Core

#Region " OllamaClient "

    ''' <summary>
    ''' Provides the core client for interacting with a running instance of the Ollama server.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(False)>
    <DebuggerStepThrough>
    Public Class OllamaClient : Implements IDisposable

#Region " Public Properties "

        ''' <summary>
        ''' Gets or sets the <see cref="System.Net.Http.HttpClient"/> instance used to send HTTP requests 
        ''' and receive HTTP responses from the Ollama API.
        ''' <para></para>
        ''' The default instance is preconfigured with a timeout of 30 minutes to accommodate long-running AI generation tasks.
        ''' </summary>
        <DisplayName("HttpClient")>
        <Description("The HTTP client used to send HTTP requests and receive HTTP responses from the Ollama API.")>
        Public Property HttpClient As HttpClient
            Get
                Return Me.httpClient_
            End Get
            <DebuggerStepThrough>
            Set(value As HttpClient)
                ArgumentValidator.ThrowIfNull(value, NameOf(value), NameOf(Me.HttpClient))
                Me.httpClient_ = value
            End Set
        End Property

        ''' <summary>
        ''' ( Backing field for the <see cref="OllamaClient.HttpClient"/> property. )
        ''' <para></para>
        ''' The <see cref="System.Net.Http.HttpClient"/> instance used to send HTTP requests 
        ''' and receive HTTP responses from the Ollama server.
        ''' </summary>
        Private httpClient_ As HttpClient

        ''' <summary>
        ''' Gets or sets the base URL endpoint of the Ollama server.
        ''' <para></para>
        ''' Default value is "<c>http://localhost:11434/</c>".
        ''' </summary>
        <DisplayName("Endpoint")>
        <Description("The base URL endpoint of the Ollama server")>
        Public Property EndpointBase As String
            Get
                Return Me.endpointBase_
            End Get
            <DebuggerStepThrough>
            Set(value As String)
                ArgumentValidator.ThrowIfNullOrWhiteSpace(value, NameOf(value), NameOf(Me.EndpointBase))
                Me.endpointBase_ = value
            End Set
        End Property

        ''' <summary>
        ''' ( Backing field for the <see cref="OllamaClient.EndpointBase"/> property. )
        ''' <para></para>
        ''' The base URL endpoint of the Ollama server.
        ''' </summary>
        Private endpointBase_ As String

        ''' <summary>
        ''' Gets access to the endpoints used for managing Ollama models, such as listing, pulling, copying, and deleting them.
        ''' </summary>
        Public ReadOnly Property Management As ManagementEndpointsProvider

        ''' <summary>
        ''' Gets access to the endpoints used for text generation, chat completions, and generating embeddings.
        ''' </summary>
        Public ReadOnly Property Generation As GenerationEndpointsProvider

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="OllamaClient"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="OllamaClient"/> class.
        ''' </summary>
        ''' 
        ''' <param name="endpointBase">
        ''' Optional. The base URL endpoint of the Ollama server.
        ''' <para></para>
        ''' Default value is "<c>http://localhost:11434/</c>".
        ''' </param>
        Public Sub New(Optional endpointBase As String = "http://localhost:11434/")

            Me.httpClient_ = OllamaClient.InitializeDefaultHttpClient()
            Me.EndpointBase = endpointBase

            Me.Management = New ManagementEndpointsProvider(Me)
            Me.Generation = New GenerationEndpointsProvider(Me)
        End Sub

#End Region

#Region " Private Methods "

        ''' <summary>
        ''' Creates a new instance of <see cref="System.Net.Http.HttpClient"/> for Ollama API communication,
        ''' preconfigured with a timeout of 30 minutes to prevent premature disconnections during long-running AI generation tasks.
        ''' </summary>
        ''' 
        ''' <returns>
        ''' The newly created instance of <see cref="System.Net.Http.HttpClient"/> for Ollama API communication.
        ''' </returns>
        Private Shared Function InitializeDefaultHttpClient() As HttpClient

            Return New HttpClient() With {
                .Timeout = TimeSpan.FromMinutes(30.0R)
            }
        End Function

#End Region

#Region " IDisposable Support "

        ''' <summary>
        ''' Flag to indicate whether the <see cref="OllamaClient"/> instance has already been disposed.
        ''' </summary>
        Private disposedValue As Boolean

        ''' <summary>
        ''' Releases the unmanaged resources used by the <see cref="OllamaClient"/> and optionally releases the managed resources.
        ''' </summary>
        ''' 
        ''' <param name="disposing">
        ''' true to release both managed and unmanaged resources; false to release only unmanaged resources.
        ''' </param>"
        <DebuggerStepperBoundary>
        Protected Overridable Sub Dispose(disposing As Boolean)

            If Not disposedValue Then
                If disposing Then
                    Me.httpClient_?.Dispose()
                End If

                Me.disposedValue = True
            End If
        End Sub

        ''' <summary>
        ''' Releases all resources used by the <see cref="OllamaClient"/> instance.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose

            ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
            Me.Dispose(disposing:=True)
            GC.SuppressFinalize(Me)
        End Sub

#End Region

    End Class

#End Region

End Namespace
