
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Net.Http
Imports System.Threading
Imports System.Threading.Tasks

Imports Ollamantis.Contracts
Imports Ollamantis.Core.Helpers

#End Region

Namespace Core

#Region " ManagementEndpointsProvider "

    ''' <summary>
    ''' Provides methods for managing Ollama models, such as listing, pulling, copying, and deleting them.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <DebuggerStepThrough>
    Public NotInheritable Class ManagementEndpointsProvider : Inherits EndpointsProviderBase

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ManagementEndpointsProvider"/> class.
        ''' </summary>
        ''' 
        ''' <param name="client">
        ''' The <see cref="OllamaClient"/> containing the connection settings and the initialized HTTP client.
        ''' </param>
        Friend Sub New(client As OllamaClient)

            MyBase.New(client)
        End Sub

#End Region

#Region " Public Methods (Endpoints) "

#Region " /api/version " ' https://github.com/ollama/ollama/blob/main/docs/api.md#version

        ''' <summary>
        ''' Retrieves the version of the current Ollama server.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     OllamaVersionResponse versionResponse = 
        '''         client.Management.GetOllamaVersion();
        ''' 
        '''     string responseJson = versionResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        '''     
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim versionResponse As OllamaVersionResponse =
        '''         client.Management.GetOllamaVersion()
        ''' 
        '''     Dim responseJson As String = versionResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' 
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <returns>
        ''' An <see cref="OllamaVersionResponse"/> containing the Ollama version string.
        ''' </returns>
        Public Function GetOllamaVersion() As OllamaVersionResponse

            Return Me.GetOllamaVersionAsync(CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Asynchronously retrieves the version of the current Ollama server.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     OllamaVersionResponse versionResponse = 
        '''         await client.Management.GetOllamaVersionAsync(CancellationToken.None);
        ''' 
        '''     string responseJson = versionResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        '''     
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim versionResponse As OllamaVersionResponse =
        '''         Await client.Management.GetOllamaVersionAsync(CancellationToken.None)
        ''' 
        '''     Dim responseJson As String = versionResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' 
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of OllamaVersionResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the <see cref="OllamaVersionResponse"/> with the Ollama version string.
        ''' </returns>
        Public Async Function GetOllamaVersionAsync(cancellationToken As CancellationToken) As Task(Of OllamaVersionResponse)

            Return Await OllamaClientHelper.GetAsJsonAsync(Of OllamaVersionResponse)(
                                                           Me.Client, "version", cancellationToken).
                                            ConfigureAwait(continueOnCapturedContext:=False)
        End Function

#End Region

#Region " /api/create " ' https://github.com/ollama/ollama/blob/main/docs/api.md#create-a-model

        ' Sorry, the implementation of the /api/create endpoint methods is not planned.

#End Region

#Region " /api/blobs " ' https://github.com/ollama/ollama/blob/main/docs/api.md#push-a-blob

        ' Sorry, the implementation of the /api/blobs endpoint methods is not planned.

#End Region

#Region " /api/push " ' https://github.com/ollama/ollama/blob/main/docs/api.md#push-a-model

        ''' <summary>
        ''' Uploads (pushes) a model to the remote Ollama library.
        ''' <para></para>
        ''' Requires registering for ollama.ai and adding a public key first.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     PushModelRequest pushRequest = new PushModelRequest {
        '''         Name = "namespace/mymodel:3B"
        '''     };
        ''' 
        '''     PushModelResponse pushResponse = 
        '''         client.Management.PushModel(pushRequest);
        ''' 
        '''     string responseJson = pushResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        '''     
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim pushRequest As New PushModelRequest With {
        '''         .Name = "namespace/mymodel:3B"
        '''     }
        ''' 
        '''     Dim pushResponse As PushModelResponse =
        '''         client.Management.PushModel(pushRequest)
        ''' 
        '''     Dim responseJson As String = pushResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' 
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="PushModelRequest"/> containing the request parameters for the push operation.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="PushModelResponse"/> containing the result of the push operation.
        ''' </returns>
        Public Function PushModel(request As PushModelRequest) As PushModelResponse

            Return Me.PushModelAsync(request, CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Asynchronously uploads (pushes) a model to the remote Ollama library.
        ''' <para></para>
        ''' Requires registering for ollama.ai and adding a public key first.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     PushModelRequest pushRequest = new PushModelRequest {
        '''         Name = "namespace/mymodel:3B"
        '''     };
        ''' 
        '''     PushModelResponse pushResponse = 
        '''         await client.Management.PushModelAsync(pushRequest, CancellationToken.None);
        ''' 
        '''     string responseJson = pushResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        '''     
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim pushRequest As New PushModelRequest With {
        '''         .Name = "namespace/mymodel:3B"
        '''     }
        ''' 
        '''     Dim pushResponse As PushModelResponse =
        '''         await client.Management.PushModelAsync(pushRequest, CancellationToken.None)
        ''' 
        '''     Dim responseJson As String = pushResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' 
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="PushModelRequest"/> containing the request parameters for the push operation.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of PushModelResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the <see cref="PushModelResponse"/> with the result of the push operation.
        ''' </returns>
        Public Async Function PushModelAsync(request As PushModelRequest,
                                             cancellationToken As CancellationToken) As Task(Of PushModelResponse)

            Return Await OllamaClientHelper.PostAsJsonAsync(Of PushModelRequest, PushModelResponse)(
                                                            Me.Client, "push", request, cancellationToken).
                                            ConfigureAwait(continueOnCapturedContext:=False)
        End Function

        ''' <summary>
        ''' Asynchronously uploads (pushes) a model to the remote Ollama library 
        ''' and streams the progress incrementally via a callback delegate.
        ''' <para></para>
        ''' Requires registering for ollama.ai and adding a public key first.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     PushModelRequest pushRequest = new PushModelRequest {
        '''         Name = "namespace/mymodel:3B"
        '''     };
        ''' 
        '''     Action&lt;PushModelResponse&gt; onChunkReceived = chunk =&gt; {
        '''         // Handle the streamed progress here.
        '''         string output = $"Status: {chunk.Status,-25} | Layer Total: {chunk.TotalSizeFormatted}";
        '''         string paddedOutput = output.PadRight(Console.WindowWidth - 1);
        ''' 
        '''         Console.Write("\r" + paddedOutput);
        '''     };
        ''' 
        '''     PushModelResponse pushResponse = 
        '''         await client.Management.StreamPushModelAsync(pushRequest, onChunkReceived, CancellationToken.None);
        ''' 
        '''     string responseJson = pushResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim pushRequest As New PushModelRequest With {
        '''         .Name = "namespace/mymodel:3B"
        '''     }
        ''' 
        '''     Dim onChunkReceived As Action(Of PushModelResponse) =
        '''         Sub(chunk) ' Handle the streamed progress here.
        '''             Dim output As String = $"Status: {chunk.Status,-25} | Layer Total: {chunk.TotalSizeFormatted}"
        '''             Dim paddedOutput As String = output.PadRight(Console.WindowWidth - 1)
        ''' 
        '''             Console.Write(Constants.vbCr &amp; paddedOutput)
        '''         End Sub
        ''' 
        '''     Dim pushResponse As PushModelResponse =
        '''         Await client.Management.StreamPushModelAsync(pushRequest, onChunkReceived, CancellationToken.None)
        ''' 
        '''     Dim responseJson As String = pushResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="PushModelRequest"/> containing the request parameters for the push operation.
        ''' </param>
        ''' 
        ''' <param name="onChunkReceived">
        ''' An <see cref="Action(Of PushModelResponse)"/> callback that is invoked every time a 
        ''' new progress chunk is received from the stream.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of PushModelResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the <see cref="PushModelResponse"/> with the result of the push operation.
        ''' </returns>
        Public Async Function StreamPushModelAsync(request As PushModelRequest,
                                                   onChunkReceived As Action(Of PushModelResponse),
                                                   cancellationToken As CancellationToken
                                                  ) As Task(Of PushModelResponse)

            Dim factoryDelegate As Func(Of String, String, Long?, Long?, PushModelResponse) =
                Function(status As String, digest As String, total As Long?, completed As Long?)
                    Return New PushModelResponse(status, digest, total, completed)
                End Function

            Return Await Me.InternalStreamModelTransferAsync("push", request, onChunkReceived, factoryDelegate, cancellationToken).
                            ConfigureAwait(continueOnCapturedContext:=False)
        End Function

#End Region

#Region " /api/pull " ' https://github.com/ollama/ollama/blob/main/docs/api.md#pull-a-model

        ''' <summary>
        ''' Downloads (pulls) a model from the remote Ollama library.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     PullModelRequest pullRequest = new PullModelRequest {
        '''         Name = "llama3.2"
        '''     };
        ''' 
        '''     PullModelResponse pullResponse = 
        '''         client.Management.PullModel(delRequest);
        ''' 
        '''     string responseJson = pullResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        '''     
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim pullRequest As New PullModelRequest With {
        '''          .Name = "qwen2.5vl:3b"
        '''      }
        ''' 
        '''     Dim pullResponse As PullModelResponse =
        '''         client.Management.PullModel(pullRequest)
        ''' 
        '''     Dim responseJson As String = pullResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="PullModelRequest"/> containing the request parameters for the pull operation.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="PullModelResponse"/> containing the result of the pull operation.
        ''' </returns>
        Public Function PullModel(request As PullModelRequest) As PullModelResponse

            Return Me.PullModelAsync(request, CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Asynchronously downloads (pulls) a model from the remote Ollama library.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     PullModelRequest pullRequest = new PullModelRequest {
        '''         Name = "llama3.2"
        '''     };
        ''' 
        '''     PullModelResponse pullResponse = 
        '''         await client.Management.PullModelAsync(delRequest, CancellationToken.None);
        ''' 
        '''     string responseJson = pullResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        '''     
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim pullRequest As New PullModelRequest With {
        '''          .Name = "qwen2.5vl:3b"
        '''      }
        ''' 
        '''     Dim pullResponse As PullModelResponse =
        '''         Await client.Management.PullModelAsync(pullRequest, CancellationToken.None)
        ''' 
        '''     Dim responseJson As String = pullResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="PullModelRequest"/> containing the request parameters for the pull operation.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of PullModelResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the <see cref="PullModelResponse"/> with the result of the pull operation.
        ''' </returns>
        Public Async Function PullModelAsync(request As PullModelRequest,
                                             cancellationToken As CancellationToken) As Task(Of PullModelResponse)

            Return Await OllamaClientHelper.PostAsJsonAsync(Of PullModelRequest, PullModelResponse)(
                                                            Me.Client, "pull", request, cancellationToken).
                                            ConfigureAwait(continueOnCapturedContext:=False)
        End Function

        ''' <summary>
        ''' Asynchronously downloads (pulls) a model from the remote Ollama library 
        ''' and streams the progress incrementally via a callback delegate.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     PullModelRequest pullRequest = new PullModelRequest {
        '''         Name = "llama3.2"
        '''     };
        ''' 
        '''     Action&lt;PullModelResponse&gt; onChunkReceived = chunk =&gt; {
        '''         // Handle the streamed progress here.
        '''         string output = $"Status: {chunk.Status,-25} | Layer Total: {chunk.TotalSizeFormatted}";
        '''         string paddedOutput = output.PadRight(Console.WindowWidth - 1);
        ''' 
        '''         Console.Write("\r" + paddedOutput);
        '''     };
        ''' 
        '''     PullModelResponse pullResponse = 
        '''         await client.Management.StreamPullModelAsync(pullRequest, onChunkReceived, CancellationToken.None);
        ''' 
        '''     string responseJson = pullResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim pullRequest As New PullModelRequest With {
        '''         .Name = "llama3.2"
        '''     }
        ''' 
        '''     Dim onChunkReceived As Action(Of PullModelResponse) =
        '''         Sub(chunk) ' Handle the streamed progress here.
        '''             Dim output As String = $"Status: {chunk.Status,-25} | Layer Total: {chunk.TotalSizeFormatted}"
        '''             Dim paddedOutput As String = output.PadRight(Console.WindowWidth - 1)
        ''' 
        '''             Console.Write(Constants.vbCr &amp; paddedOutput)
        '''         End Sub
        ''' 
        '''     Dim pullResponse As PullModelResponse =
        '''         Await client.Management.StreamPullModelAsync(pullRequest, onChunkReceived, CancellationToken.None)
        ''' 
        '''     Dim responseJson As String = pullResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="PullModelRequest"/> containing the request parameters for the pull operation.
        ''' </param>
        ''' 
        ''' <param name="onChunkReceived">
        ''' An <see cref="Action(Of PullModelResponse)"/> callback that is invoked every time a 
        ''' new progress chunk is received from the stream.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of PullModelResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the <see cref="PullModelResponse"/> with the result of the pull operation.
        ''' </returns>
        Public Async Function StreamPullModelAsync(request As PullModelRequest,
                                                   onChunkReceived As Action(Of PullModelResponse),
                                                   cancellationToken As CancellationToken
                                                  ) As Task(Of PullModelResponse)

            Dim factoryDelegate As Func(Of String, String, Long?, Long?, PullModelResponse) =
                Function(status As String, digest As String, total As Long?, completed As Long?)
                    Return New PullModelResponse(status, digest, total, completed)
                End Function

            Return Await Me.InternalStreamModelTransferAsync("pull", request, onChunkReceived, factoryDelegate, cancellationToken).
                            ConfigureAwait(continueOnCapturedContext:=False)
        End Function

#End Region

#Region " /api/copy " ' https://github.com/ollama/ollama/blob/main/docs/api.md#copy-a-model

        ''' <summary>
        ''' Creates a copy of an existing model in your local Ollama storage under a new name.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     CopyModelRequest copyRequest = new CopyModelRequest {
        '''         SourceName      = "llama3.2",
        '''         DestinationName = "llama3.2-backup"
        '''     };
        ''' 
        '''     CopyModelResponse copyResponse = 
        '''         client.Management.CopyModelAsync(copyRequest);
        ''' 
        '''     string responseJson = copyResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim copyRequest As New CopyModelRequest With {
        '''         .SourceName = "llama3.2",
        '''         .DestinationName = "llama3.2-backup"
        '''     }
        ''' 
        '''     Dim copyResponse As CopyModelResponse =
        '''         client.Management.CopyModelAsync(copyRequest)
        ''' 
        '''     Dim responseJson As String = copyResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' 
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="CopyModelRequest"/> containing the request parameters for the copy operation.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="CopyModelResponse"/> containing the result of the copy operation.
        ''' </returns>
        Public Function CopyModel(request As CopyModelRequest) As CopyModelResponse

            Return Me.CopyModelAsync(request, CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Asynchronously creates a copy of an existing model in your local Ollama storage under a new name.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     CopyModelRequest copyRequest = new CopyModelRequest {
        '''         SourceName      = "llama3.2",
        '''         DestinationName = "llama3.2-backup"
        '''     };
        ''' 
        '''     CopyModelResponse copyResponse = 
        '''         await client.Management.CopyModelAsync(copyRequest, CancellationToken.None);
        ''' 
        '''     string responseJson = copyResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        '''     
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim copyRequest As New CopyModelRequest With {
        '''         .SourceName = "llama3.2",
        '''         .DestinationName = "llama3.2-backup"
        '''     }
        ''' 
        '''     Dim copyResponse As CopyModelResponse =
        '''         Await client.Management.CopyModelAsync(copyRequest, CancellationToken.None)
        ''' 
        '''     Dim responseJson As String = copyResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' 
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="CopyModelRequest"/> containing the request parameters for the copy operation.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of CopyModelResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the <see cref="CopyModelResponse"/> with the result of the copy operation.
        ''' </returns>
        Public Async Function CopyModelAsync(request As CopyModelRequest,
                                             cancellationToken As CancellationToken
                                            ) As Task(Of CopyModelResponse)

            Return Await OllamaClientHelper.PostAsJsonAsync(Of CopyModelRequest, CopyModelResponse)(
                                                            Me.Client, "copy", request, cancellationToken).
                                            ConfigureAwait(continueOnCapturedContext:=False)
        End Function

#End Region

#Region " /api/delete " ' https://github.com/ollama/ollama/blob/main/docs/api.md#delete-a-model

        ''' <summary>
        ''' Deletes an existing model and its associated data from your local Ollama storage.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     DeleteModelRequest delRequest = new DeleteModelRequest {
        '''         Name = "llama3.2"
        '''     };
        ''' 
        '''     DeleteModelResponse delResponse = 
        '''         client.Management.DeleteModelAsync(delRequest);
        ''' 
        '''     string responseJson = delResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim delRequest As New DeleteModelRequest With {
        '''         .Name = "llama3.2"
        '''     }
        ''' 
        '''     Dim delResponse As DeleteModelResponse =
        '''         client.Management.DeleteModelAsync(delRequest)
        ''' 
        '''     Dim responseJson As String = delResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        '''     
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="DeleteModelRequest"/> containing the request parameters for the deletion operation.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="DeleteModelResponse"/> containing the result of the deletion operation.
        ''' </returns>
        Public Function DeleteModel(request As DeleteModelRequest) As DeleteModelResponse

            Return Me.DeleteModelAsync(request, CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Asynchronously deletes an existing model and its associated data from your local Ollama storage.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     DeleteModelRequest delRequest = new DeleteModelRequest {
        '''         Name = "llama3.2"
        '''     };
        ''' 
        '''     DeleteModelResponse delResponse = 
        '''         await client.Management.DeleteModelAsync(delRequest, CancellationToken.None);
        ''' 
        '''     string responseJson = delResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim delRequest As New DeleteModelRequest With {
        '''         .Name = "llama3.2"
        '''     }
        ''' 
        '''     Dim delResponse As DeleteModelResponse =
        '''         Await client.Management.DeleteModelAsync(delRequest, CancellationToken.None)
        ''' 
        '''     Dim responseJson As String = delResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        '''     
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="DeleteModelRequest"/> containing the request parameters for the deletion operation.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of DeleteModelResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the <see cref="DeleteModelResponse"/> with the result of the deletion operation.
        ''' </returns>
        Public Async Function DeleteModelAsync(request As DeleteModelRequest,
                                               cancellationToken As CancellationToken) As Task(Of DeleteModelResponse)

            Return Await OllamaClientHelper.DeleteAsJsonAsync(Of DeleteModelRequest, DeleteModelResponse)(
                                                              Me.Client, "delete", request, cancellationToken).
                                            ConfigureAwait(continueOnCapturedContext:=False)
        End Function

#End Region

#Region " /api/tags " ' https://github.com/ollama/ollama/blob/main/docs/api.md#list-local-models

        ''' <summary>
        ''' Retrieves information about the Ollama models that are available locally.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     LocalModelsResponse listResponse = 
        '''         client.Management.ListLocalModels();
        ''' 
        '''     string responseJson = listResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        '''     
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim listResponse As LocalModelsResponse =
        '''         client.Management.ListLocalModels()
        ''' 
        '''     Dim responseJson As String = listResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' 
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <returns>
        ''' A <see cref="LocalModelsResponse"/> object containing the result of the operation.
        ''' </returns>
        Public Function ListLocalModels() As LocalModelsResponse

            Return Me.ListLocalModelsAsync(CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Asynchronously retrieves information about the Ollama models that are available locally.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     LocalModelsResponse listResponse = 
        '''         await client.Management.ListLocalModelsAsync(CancellationToken.None);
        ''' 
        '''     string responseJson = listResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        '''     
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim listResponse As LocalModelsResponse =
        '''         Await client.Management.ListLocalModelsAsync(CancellationToken.None)
        ''' 
        '''     Dim responseJson As String = listResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' 
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of LocalModelsResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the <see cref="LocalModelsResponse"/> with the result of the operation.
        ''' </returns>
        Public Async Function ListLocalModelsAsync(cancellationToken As CancellationToken) As Task(Of LocalModelsResponse)

            Return Await OllamaClientHelper.GetAsJsonAsync(Of LocalModelsResponse)(
                                                           Me.Client, "tags", cancellationToken).
                                            ConfigureAwait(continueOnCapturedContext:=False)
        End Function

#End Region

#Region " /api/ps " ' https://github.com/ollama/ollama/blob/main/docs/api.md#list-running-models

        ''' <summary>
        ''' Retrieves information about the Ollama models that are currently loaded in memory.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     RunningModelsResponse listResponse = 
        '''         client.Management.ListRunningModels();
        ''' 
        '''     string responseJson = listResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        '''     
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        '''
        '''     Dim listResponse As RunningModelsResponse =
        '''         client.Management.ListRunningModels()
        '''
        '''     Dim responseJson As String = listResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        '''
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <returns>
        ''' A <see cref="RunningModelsResponse"/> object containing the result of the operation.
        ''' </returns>
        Public Function ListRunningModels() As RunningModelsResponse

            Return Me.ListRunningModelsAsync(CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Asynchronously retrieves information about the Ollama models that are currently loaded in memory.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     RunningModelsResponse listResponse = 
        '''         await client.Management.ListRunningModelsAsync(CancellationToken.None);
        ''' 
        '''     string responseJson = listResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        '''     
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        '''
        '''     Dim listResponse As RunningModelsResponse =
        '''         Await client.Management.ListRunningModelsAsync(CancellationToken.None)
        '''
        '''     Dim responseJson As String = listResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        '''
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of RunningModelsResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the <see cref="RunningModelsResponse"/> with the result of the operation.
        ''' </returns>
        Public Async Function ListRunningModelsAsync(cancellationToken As CancellationToken) As Task(Of RunningModelsResponse)

            Return Await OllamaClientHelper.GetAsJsonAsync(Of RunningModelsResponse)(
                                                           Me.Client, "ps", cancellationToken).
                                            ConfigureAwait(continueOnCapturedContext:=False)
        End Function

#End Region

#Region " /api/show " ' https://github.com/ollama/ollama/blob/main/docs/api.md#show-model-information

        ''' <summary>
        ''' Retrieves detailed information about a specific Ollama model.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        '''     ShowModelRequest detailsRequest = new ShowModelRequest {
        '''         Name = "llama3.2"
        '''     };
        ''' 
        '''     ShowModelResponse detailsResponse = 
        '''         client.Management.PullModel(detailsRequest);
        ''' 
        '''     string responseJson = detailsResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        '''     
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim detailsRequest As New ShowModelRequest With {
        '''         .Name = "llama3.2"
        '''     }
        ''' 
        '''     Dim detailsResponse As ShowModelResponse =
        '''         client.Management.ShowModel(detailsRequest)
        ''' 
        '''     Dim responseJson As String = detailsResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' 
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="ShowModelRequest"/> containing the request parameters for the operation.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="ShowModelResponse"/> object containing the result of the operation.
        ''' </returns>
        Public Function ShowModel(request As ShowModelRequest) As ShowModelResponse

            Return Me.ShowModelAsync(request, CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Asynchronously retrieves detailed information about a specific Ollama model.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        '''     ShowModelRequest detailsRequest = new ShowModelRequest {
        '''         Name = "llama3.2"
        '''     };
        ''' 
        '''     ShowModelResponse detailsResponse = 
        '''         await client.Management.PullModelAsync(detailsRequest, CancellationToken.None);
        ''' 
        '''     string responseJson = detailsResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        '''     
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim detailsRequest As New ShowModelRequest With {
        '''         .Name = "llama3.2"
        '''     }
        ''' 
        '''     Dim detailsResponse As ShowModelResponse =
        '''         Await client.Management.ShowModelAsync(detailsRequest, CancellationToken.None)
        ''' 
        '''     Dim responseJson As String = detailsResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' 
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="ShowModelRequest"/> containing the request parameters for the operation.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of ShowModelResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the <see cref="ShowModelResponse"/> with the result of the operation.
        ''' </returns>
        Public Async Function ShowModelAsync(request As ShowModelRequest,
                                             cancellationToken As CancellationToken) As Task(Of ShowModelResponse)

            Return Await OllamaClientHelper.PostAsJsonAsync(Of ShowModelRequest, ShowModelResponse)(
                                                            Me.Client, "show", request, cancellationToken).
                                            ConfigureAwait(continueOnCapturedContext:=False)
        End Function

#End Region

#End Region

#Region " Private Methods "

        ''' <summary>
        ''' Asynchronously processes a streaming response for model transfer operations (push or pull),
        ''' aggregating the layer sizes to calculate the total and completed bytes.
        ''' </summary>
        ''' 
        ''' <typeparam name="TRequest">
        ''' The type of the request object, which must inherit from <see cref="ModelTransferRequestBase"/>.
        ''' </typeparam>
        ''' 
        ''' <typeparam name="TResponse">
        ''' The type of the response object, which must inherit from <see cref="ModelTransferResponseBase"/>.
        ''' </typeparam>
        ''' 
        ''' <param name="endpoint">
        ''' The Ollama API endpoint to call (e.g., "<c>push</c>" or "<c>pull</c>").
        ''' </param>
        ''' 
        ''' <param name="request">
        ''' The request payload containing the model name and transfer parameters.
        ''' </param>
        ''' 
        ''' <param name="onChunkReceived">
        ''' An <see cref="Action(Of TResponse)"/> callback that is invoked every time a 
        ''' new chunk is received from the model transfer stream.
        ''' </param>
        ''' 
        ''' <param name="responseFactory">
        ''' A factory function delegate that constructs the final <typeparamref name="TResponse"/> object.
        ''' <para></para>
        ''' It accepts the final status (<see cref="String"/>), the final digest (<see cref="String"/>), 
        ''' the aggregated total size (<see cref="Nullable(Of Long)"/>), 
        ''' and the aggregated completed size (<see cref="Nullable(Of Long)"/>).
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of TResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the aggregated final <typeparamref name="TResponse"/>.
        ''' </returns>
        ''' 
        ''' <exception cref="ArgumentNullException">
        ''' Thrown if the <paramref name="request"/> or 
        ''' <paramref name="onChunkReceived"/> is <see langword="Nothing"/>.
        ''' </exception>
        ''' 
        ''' <exception cref="InvalidOperationException">
        ''' Thrown if the API stream finishes without yielding any valid JSON objects.
        ''' </exception>
        Private Async Function InternalStreamModelTransferAsync(Of TRequest As ModelTransferRequestBase,
                                                                   TResponse As ModelTransferResponseBase)(endpoint As String,
                                                                                                           request As TRequest,
                                                                                                           onChunkReceived As Action(Of TResponse),
                                                                                                           responseFactory As Func(Of String, String, Long?, Long?, TResponse),
                                                                                                           cancellationToken As CancellationToken
                                                                                                          ) As Task(Of TResponse)

            ' Note: We later throw a 'NullReferenceException' in 'StreamingPostAsJsonAsync' if request or endpoint is null.
            ArgumentValidator.ThrowIfNull(onChunkReceived, NameOf(onChunkReceived))

            ' Enable streaming to guarantee the API returns a NDJSON (Newline Delimited JSON) instead of a single JSON object.
            If request IsNot Nothing Then
                request.Stream = True
            End If

            ' Dictionaries to track the total and completed/downloaded size PER LAYER (using the Digest as a unique key).
            Dim layerTotals As New Dictionary(Of String, Long)()
            Dim layerCompleted As New Dictionary(Of String, Long)()

            ' Receive the final chunk directly from the streaming processor.
            Dim finalChunk As TResponse =
                Await OllamaClientHelper.StreamingPostAsJsonAsync(Of TRequest, TResponse)(Me.Client, endpoint, request,
                Sub(chunk As TResponse)
                    ' Fire the user's callback.
                    onChunkReceived.Invoke(chunk)

                    ' Accumulate sizes.
                    If Not String.IsNullOrWhiteSpace(chunk.Digest) Then
                        If chunk.TotalSize.HasValue Then
                            layerTotals(chunk.Digest) = chunk.TotalSize.Value
                        End If

                        If chunk.CompletedSize.HasValue Then
                            layerCompleted(chunk.Digest) = chunk.CompletedSize.Value
                        End If
                    End If
                End Sub,
                cancellationToken
            ).ConfigureAwait(continueOnCapturedContext:=False)

            If finalChunk Is Nothing Then
                Throw New InvalidOperationException("The API stream did not yield any parsed JSON objects.")
            End If

            Dim baseFinalChunk As ResponseBase = finalChunk

            ' If the HTTP request failed (e.g., 404 or 500 error), do NOT aggregate totals or use the factory function.
            ' Just return the final chunk which already contains the properly hydrated ErrorMessage and StatusCode properties.
            If baseFinalChunk IsNot Nothing AndAlso Not baseFinalChunk.IsSuccessful Then
                Return finalChunk
            End If

            ' If the request was successful, compute the grand totals by suming the sizes of all discovered layers.

            Dim grandTotal As Long? = Nothing
            If layerTotals.Count > 0 Then
                grandTotal = 0L
                For Each sizeValue As Long In layerTotals.Values
                    grandTotal += sizeValue
                Next sizeValue
            End If

            Dim grandCompleted As Long? = Nothing
            If layerCompleted.Count > 0 Then
                grandCompleted = 0L
                For Each sizeValue As Long In layerCompleted.Values
                    grandCompleted += sizeValue
                Next sizeValue
            End If

            ' Enforce perfect completion match on success.
            ' Ollama's stream often skips the final byte-exact update for layers before switching to the verification phase.
            ' If the final status is "success", the download is 100% complete by definition.
            If String.Equals(finalChunk.Status, "success", StringComparison.OrdinalIgnoreCase) Then
                grandCompleted = grandTotal
            End If

            ' Create the aggregated response using the provided factory function.
            Dim aggregatedResponse As TResponse =
                responseFactory.Invoke(finalChunk.Status, finalChunk.Digest, grandTotal, grandCompleted)

            ' Hydrate the newly created object with the HTTP 200 OK metadata from the final chunk.
            Dim baseAggregated As ResponseBase = aggregatedResponse
            If baseAggregated IsNot Nothing AndAlso baseFinalChunk IsNot Nothing Then
                baseAggregated.HydrateMetadata(baseFinalChunk.IsSuccessful,
                                               baseFinalChunk.StatusCode,
                                               baseFinalChunk.ReasonPhrase,
                                               rawJson:=Nothing)
            End If

            Return aggregatedResponse
        End Function

#End Region

    End Class

#End Region

End Namespace
