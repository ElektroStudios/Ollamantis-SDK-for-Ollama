
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO
Imports System.Net.Http
Imports System.Text
Imports System.Text.Json
Imports System.Threading
Imports System.Threading.Tasks

Imports Ollamantis.Contracts

Imports Ollamantis.Core.Extensions.HttpContentExtensions
Imports Ollamantis.Core.Extensions.StreamReaderExtensions

#End Region

Namespace Core.Helpers

#Region " OllamaClientHelper"

    ''' <summary>
    ''' Provides internal helper methods for executing HTTP operations, validating arguments, 
    ''' and processing JSON responses for the <see cref="OllamaClient"/>.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Module OllamaClientHelper

#Region " Constants and Read-Only Fields "

        ''' <summary>
        ''' The default media type header value used for HTTP content serialization and deserialization.
        ''' </summary>
        Const ContentMediaType As String = "application/json"

        ''' <summary>
        ''' The default character encoding used for all HTTP request and response content
        ''' serialization and deserialization processes.
        ''' </summary>
        Friend ReadOnly ContentEncoding As Encoding = Encoding.UTF8

#End Region

#Region " Methods "

        ''' <summary>
        ''' Asynchronously sends an HTTP GET request to a specified Ollama API endpoint, 
        ''' and returns the deserialized, single JSON response.
        ''' </summary>
        ''' 
        ''' <typeparam name="TResult">
        ''' The expected type of the object to be constructed from the JSON response.
        ''' </typeparam>
        ''' 
        ''' <param name="client">
        ''' The <see cref="OllamaClient"/> containing the connection settings and the initialized HTTP client.
        ''' </param>
        ''' 
        ''' <param name="endpointAction">
        ''' The final segment of the Ollama API URL to target (for example, "<c>generate</c>" or "<c>copy</c>"). 
        ''' This string is appended directly to the base <c>/api/</c> route.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' Returns a <see cref="Task(Of TResult)"/> representing the asynchronous operation. 
        ''' The task result contains the deserialized response object.
        ''' </returns>
        Friend Async Function GetAsJsonAsync(Of TResult As Class)(client As OllamaClient,
                                                                  endpointAction As String,
                                                                  cancellationToken As CancellationToken
                                                                 ) As Task(Of TResult)

            ArgumentValidator.ThrowIfNull(client, NameOf(client))
            ArgumentValidator.ThrowIfNullOrWhiteSpace(endpointAction, NameOf(endpointAction))

            Dim requestUrl As String = $"{client.EndpointBase.TrimEnd("/"c)}/api/{endpointAction}"

            Using httpRequest As New HttpRequestMessage(HttpMethod.Get, requestUrl),
                  httpResponse As HttpResponseMessage =
                      Await client.HttpClient.SendAsync(httpRequest, cancellationToken).
                                              ConfigureAwait(continueOnCapturedContext:=False)

                Return Await OllamaClientHelper.ProcessJsonResponseAsync(Of TResult)(httpResponse, cancellationToken).
                                                ConfigureAwait(continueOnCapturedContext:=False)

            End Using ' httpRequest, httpResponse
        End Function

        ''' <summary>
        ''' Asynchronously sends an HTTP request with a specified method, containing a serialized JSON object, 
        ''' to a specified Ollama API endpoint.
        ''' </summary>
        ''' 
        ''' <typeparam name="TRequest">
        ''' The specific type of the request object to be serialized into JSON.
        ''' </typeparam>
        ''' 
        ''' <typeparam name="TResult">
        ''' The expected type of the object to be constructed from the JSON response.
        ''' </typeparam>
        ''' 
        ''' <param name="httpMethod">
        ''' The <see cref="HttpMethod"/> to use for the request (e.g., Post, Delete).
        ''' </param>
        ''' 
        ''' <param name="client">
        ''' The <see cref="OllamaClient"/> containing the connection settings.
        ''' </param>
        ''' 
        ''' <param name="endpointAction">
        ''' The final segment of the Ollama API URL to target.
        ''' </param>
        ''' 
        ''' <param name="request">
        ''' The strongly-typed request object containing the parameters for the API call.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <param name="responseFactory">
        ''' Optional. A delegate to manually intercept and process the raw <see cref="HttpResponseMessage"/>.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of TResult)"/> representing the asynchronous operation, 
        ''' containing the deserialized response object or the custom factory result.
        ''' </returns>
        Private Async Function SendAsJsonAsync(Of TRequest As Class,
                                                  TResult As Class)(httpMethod As HttpMethod,
                                                                    client As OllamaClient,
                                                                    endpointAction As String,
                                                                    request As TRequest,
                                                                    cancellationToken As CancellationToken,
                                                                    Optional responseFactory As Func(Of HttpResponseMessage, TResult) = Nothing
                                                                   ) As Task(Of TResult)

            ArgumentValidator.ThrowIfNull(client, NameOf(client))
            ArgumentValidator.ThrowIfNullOrWhiteSpace(endpointAction, NameOf(endpointAction))
            ArgumentValidator.ThrowIfNull(request, NameOf(request))

            Dim requestUrl As String = $"{client.EndpointBase.TrimEnd("/"c)}/api/{endpointAction}"
            Dim jsonPayload As String = JsonSerializer.Serialize(request)

            Using content As New StringContent(jsonPayload, OllamaClientHelper.ContentEncoding, OllamaClientHelper.ContentMediaType),
                  httpRequest As New HttpRequestMessage(httpMethod, requestUrl) With {.Content = content},
                  httpResponse As HttpResponseMessage =
                      Await client.HttpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseContentRead, cancellationToken).
                                              ConfigureAwait(continueOnCapturedContext:=False)

                ' If a custom factory is provided, delegate full responsibility to it (including error handling).
                If responseFactory IsNot Nothing Then
                    Return responseFactory.Invoke(httpResponse)
                End If

                ' Otherwise, process validation and deserialization.
                Return Await OllamaClientHelper.ProcessJsonResponseAsync(Of TResult)(httpResponse, cancellationToken).
                                                ConfigureAwait(continueOnCapturedContext:=False)

            End Using ' content, httpRequest, httpResponse
        End Function

        ''' <summary>
        ''' Asynchronously sends an HTTP POST request containing a serialized JSON object to a specified Ollama API endpoint, 
        ''' and returns the deserialized, single JSON response.
        ''' </summary>
        ''' 
        ''' <typeparam name="TRequest">
        ''' The specific type of the request object to be serialized into JSON.
        ''' </typeparam>
        ''' 
        ''' <typeparam name="TResult">
        ''' The expected type of the object to be constructed from the JSON response.
        ''' </typeparam>
        ''' 
        ''' <param name="client">
        ''' The <see cref="OllamaClient"/> containing the connection settings and the initialized HTTP client.
        ''' </param>
        ''' 
        ''' <param name="endpointAction">
        ''' The final segment of the Ollama API URL to target (for example, "<c>generate</c>" or "<c>copy</c>"). 
        ''' This string is appended directly to the base <c>/api/</c> route.
        ''' </param>
        ''' 
        ''' <param name="request">
        ''' The strongly-typed request object containing the parameters for the API call.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <param name="responseFactory">
        ''' Optional. A delegate to manually intercept and process the raw <see cref="HttpResponseMessage"/> 
        ''' instead of relying on the default JSON deserialization.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of TResult)"/> representing the asynchronous operation. 
        ''' The task result contains the deserialized response object, or the 
        ''' custom result of the <paramref name="responseFactory"/> if provided.
        ''' </returns>
        Friend Async Function PostAsJsonAsync(Of TRequest As Class,
                                                  TResult As Class)(client As OllamaClient,
                                                                    endpointAction As String,
                                                                    request As TRequest,
                                                                    cancellationToken As CancellationToken,
                                                           Optional responseFactory As Func(Of HttpResponseMessage, TResult) = Nothing
                                                                   ) As Task(Of TResult)

            Return Await OllamaClientHelper.SendAsJsonAsync(HttpMethod.Post,
                                                            client, endpointAction, request, cancellationToken, responseFactory
                                                           ).ConfigureAwait(continueOnCapturedContext:=False)
        End Function

        ''' <summary>
        ''' Asynchronously sends an HTTP POST request containing a serialized JSON object to a specified Ollama API endpoint, 
        ''' and continuously processes the streamed NDJSON (Newline Delimited JSON) response.
        ''' </summary>
        ''' 
        ''' <typeparam name="TRequest">
        ''' The specific type of the request object to be serialized into JSON.
        ''' </typeparam>
        ''' 
        ''' <typeparam name="TResult">
        ''' The expected type of the object to be constructed from each individual line of the streamed JSON response.
        ''' </typeparam>
        ''' 
        ''' <param name="client">
        ''' The <see cref="OllamaClient"/> containing the connection settings and the initialized HTTP client.
        ''' </param>
        ''' 
        ''' <param name="endpointAction">
        ''' The final segment of the Ollama API URL to target (for example, "<c>generate</c>" or "<c>copy</c>"). 
        ''' This string is appended directly to the base <c>/api/</c> route.
        ''' </param>
        ''' 
        ''' <param name="request">
        ''' The strongly-typed request object containing the parameters for the API call.
        ''' </param>
        ''' 
        ''' <param name="onChunkReceived">
        ''' A delegate invoked immediately as each line of the NDJSON stream is received and successfully deserialized.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task"/> representing the asynchronous operation.
        ''' </returns>
        Friend Async Function StreamingPostAsJsonAsync(Of TRequest As Class,
                                                           TResult As Class)(client As OllamaClient,
                                                                             endpointAction As String,
                                                                             request As TRequest,
                                                                             onChunkReceived As Action(Of TResult),
                                                                             cancellationToken As CancellationToken
                                                                            ) As Task(Of TResult)

            ArgumentValidator.ThrowIfNull(client, NameOf(client))
            ArgumentValidator.ThrowIfNullOrWhiteSpace(endpointAction, NameOf(endpointAction))
            ArgumentValidator.ThrowIfNull(request, NameOf(request))
            ArgumentValidator.ThrowIfNull(onChunkReceived, NameOf(onChunkReceived))

            Dim requestUrl As String = $"{client.EndpointBase.TrimEnd("/"c)}/api/{endpointAction}"
            Dim jsonPayload As String = JsonSerializer.Serialize(request)
            Dim finalResult As TResult = Nothing

            Using content As New StringContent(jsonPayload, OllamaClientHelper.ContentEncoding, OllamaClientHelper.ContentMediaType),
                  httpRequest As New HttpRequestMessage(HttpMethod.Post, requestUrl) With {.Content = content},
                  httpResponse As HttpResponseMessage =
                      Await client.HttpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken).
                                              ConfigureAwait(continueOnCapturedContext:=False)

                Dim isSuccess As Boolean = httpResponse.IsSuccessStatusCode
                Dim statusCode As Integer = httpResponse.StatusCode
                Dim reasonPhrase As String = httpResponse.ReasonPhrase

                ' If it is an error, it is NOT an NDJSON stream. It is a single error JSON object.
                If Not isSuccess Then
                    Dim errorBody As String =
                        Await httpResponse.Content.CompatibleReadAsStringAsync(cancellationToken).
                                                   ConfigureAwait(continueOnCapturedContext:=False)

                    ' Try to deserialize the error JSON, or instantiate a fallback if it's malformed.
                    If Not String.IsNullOrWhiteSpace(errorBody) AndAlso errorBody.TrimStart().StartsWith("{"c) Then
                        Try
                            finalResult = JsonSerializer.Deserialize(Of TResult)(errorBody)
                        Catch ex As JsonException
                            finalResult = DirectCast(Activator.CreateInstance(GetType(TResult), nonPublic:=True), TResult)
                        End Try
                    Else
                        finalResult = DirectCast(Activator.CreateInstance(GetType(TResult), nonPublic:=True), TResult)
                    End If

                    ' Hydrate the HTTP error metadata safely.
                    Dim baseResponse As ResponseBase = TryCast(finalResult, ResponseBase)
                    baseResponse?.HydrateMetadata(isSuccess, statusCode, reasonPhrase, errorBody)

                    ' Return immediately. Do not invoke the onChunkReceived callback.
                    Return finalResult
                End If

                ' If it is a success, parse the NDJSON stream line by line.
                Using networkStream As Stream =
                    Await httpResponse.Content.CompatibleReadAsStreamAsync(cancellationToken).
                                               ConfigureAwait(continueOnCapturedContext:=False),
                      reader As New StreamReader(networkStream, OllamaClientHelper.ContentEncoding)

                    Dim line As String =
                        Await reader.CompatibleReadLineAsync(cancellationToken).
                                     ConfigureAwait(continueOnCapturedContext:=False)

                    While line IsNot Nothing
                        cancellationToken.ThrowIfCancellationRequested()

                        If Not String.IsNullOrWhiteSpace(line) Then
                            Dim chunk As TResult = JsonSerializer.Deserialize(Of TResult)(line)

                            ' Hydrate each chunk to catch potential embedded errors disguised under a 200 OK.
                            Dim baseChunk As ResponseBase = TryCast(chunk, ResponseBase)
                            If baseChunk IsNot Nothing Then
                                baseChunk.HydrateMetadata(isSuccess, statusCode, reasonPhrase, line)

                                ' If Ollama embedded an error inside the stream, abort instantly.
                                ' Do NOT invoke the chunk callback, act as if the whole request failed.
                                If Not baseChunk.IsSuccessful Then
                                    finalResult = chunk
                                    Exit While
                                End If
                            End If

                            ' If successful, save as final result and notify the consumer.
                            finalResult = chunk
                            onChunkReceived.Invoke(chunk)
                        End If

                        line = Await reader.CompatibleReadLineAsync(cancellationToken).
                                            ConfigureAwait(continueOnCapturedContext:=False)
                    End While
                End Using ' networkStream, reader
            End Using ' content, httpRequest, httpResponse

            Return finalResult
        End Function

        ''' <summary>
        ''' Asynchronously sends an HTTP DELETE request containing a serialized JSON object to a specified Ollama API endpoint.
        ''' </summary>
        ''' 
        ''' <typeparam name="TRequest">
        ''' The specific type of the request object to be serialized into JSON.
        ''' </typeparam>
        ''' 
        ''' <param name="client">
        ''' The <see cref="OllamaClient"/> containing the connection settings and the initialized HTTP client.
        ''' </param>
        ''' 
        ''' <param name="request">
        ''' The strongly-typed request object containing the parameters for the API call.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of Boolean)"/> that represents the asynchronous operation. 
        ''' The task result contains <see langword="True"/> if the server returns a 200 (OK) status code; 
        ''' otherwise, <see langword="False"/> if it returns a 404 (Not Found) status code.
        ''' </returns>
        Friend Async Function DeleteAsJsonAsync(Of TRequest As Class,
                                                    TResult As Class)(client As OllamaClient,
                                                                      endpointAction As String,
                                                                      request As TRequest,
                                                                      cancellationToken As CancellationToken,
                                                             Optional responseFactory As Func(Of HttpResponseMessage, TResult) = Nothing
                                                                     ) As Task(Of TResult)

            Return Await OllamaClientHelper.SendAsJsonAsync(HttpMethod.Delete,
                                                            client, endpointAction, request, cancellationToken, responseFactory
                                                           ).ConfigureAwait(continueOnCapturedContext:=False)
        End Function

#End Region

#Region " Private Methods "

        ''' <summary>
        ''' Validates the HTTP response and deserializes its JSON content into the specified type.
        ''' </summary>
        ''' 
        ''' <typeparam name="TResult">
        ''' The expected type of the object to be constructed from the JSON response.
        ''' </typeparam>
        ''' 
        ''' <param name="httpResponse">
        ''' The <see cref="HttpResponseMessage"/> containing the JSON content to be processed.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of TResult)"/> representing the asynchronous operation. 
        ''' The task result contains the deserialized object.
        ''' </returns>
        ''' 
        ''' <exception cref="HttpRequestException">
        ''' Thrown when the HTTP response is not successful.
        ''' </exception>
        ''' 
        ''' <exception cref="InvalidOperationException">
        ''' Thrown when the HTTP response content is completely empty or consists only of white-space characters.
        ''' </exception>
        ''' 
        ''' <exception cref="JsonException">
        ''' Thrown when the response content is not valid JSON or cannot be deserialized into the specified type.
        ''' </exception>
        Private Async Function ProcessJsonResponseAsync(Of TResult As Class)(httpResponse As HttpResponseMessage,
                                                                             cancellationToken As CancellationToken
                                                                            ) As Task(Of TResult)

            ' Read the response body.
            Dim responseBody As String =
                Await httpResponse.Content.CompatibleReadAsStringAsync(cancellationToken).
                                           ConfigureAwait(continueOnCapturedContext:=False)

            Dim result As TResult

            ' If we have a JSON body (even if it's an error like 500 or 404 from Ollama), deserialize it.
            If Not String.IsNullOrWhiteSpace(responseBody) AndAlso responseBody.TrimStart().StartsWith("{"c) Then
                Try
                    result = JsonSerializer.Deserialize(Of TResult)(responseBody)
                Catch ex As JsonException
                    ' Fallback if deserialization fails (e.g., malformed JSON on a critical server crash).
                    result = DirectCast(Activator.CreateInstance(GetType(TResult), nonPublic:=True), TResult)
                End Try
            Else
                ' Instantiate an empty object using the non-public constructor.
                result = DirectCast(Activator.CreateInstance(GetType(TResult), nonPublic:=True), TResult)
            End If

            ' Cast to ResponseBase to inject HTTP metadata and errors securely.
            Dim baseResponse As ResponseBase = TryCast(result, ResponseBase)
            baseResponse?.HydrateMetadata(httpResponse.IsSuccessStatusCode,
                                          httpResponse.StatusCode,
                                          httpResponse.ReasonPhrase,
                                          responseBody)

            Return result
        End Function

#End Region

    End Module

#End Region

End Namespace
