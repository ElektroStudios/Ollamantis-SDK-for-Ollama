
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
Imports System.Text
Imports System.Text.Json
Imports System.Threading
Imports System.Threading.Tasks

Imports Ollamantis.Contracts
Imports Ollamantis.Core.Helpers
Imports Ollamantis.Entities

#End Region

Namespace Core

#Region " GenerationEndpointsProvider "

    ''' <summary>
    ''' Provides methods for text generation, chat completions, and generating embeddings.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <DebuggerStepThrough>
    Public NotInheritable Class GenerationEndpointsProvider : Inherits EndpointsProviderBase

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="OllamaClient"/> class.
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

#Region " /api/embed " ' https://github.com/ollama/ollama/blob/main/docs/api.md#generate-embeddings

        ''' <summary>
        ''' Generates embeddings for a given input text with a provided Ollama model.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     EmbeddingsRequest embedRequest = new EmbeddingsRequest {
        '''         Name = "all-minilm",
        '''         Inputs = new[] { "Why is the sky blue?." }
        '''     };
        ''' 
        '''     EmbeddingsResponse embedResponse = 
        '''         client.Management.GenerateEmbeddings(embedRequest);
        ''' 
        '''     string responseJson = embedResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        '''     
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        '''  Using client As New OllamaClient()
        ''' 
        '''     Dim embedRequest As New EmbeddingsRequest With {
        '''         .Model = "all-minilm",
        '''         .Inputs = {"Why is the sky blue?."}
        '''     }
        ''' 
        '''     Dim embedResponse As EmbeddingsResponse =
        '''         client.Generation.GenerateEmbeddings(embedRequest)
        ''' 
        '''     Dim responseJson As String = embedResponse.ToString(writeIndented:=False)
        '''     Console.WriteLine(responseJson)
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="EmbeddingsRequest"/> containing the request parameters for the embeddings generation operation.
        ''' </param>
        ''' 
        ''' <returns>
        ''' An <see cref="EmbeddingsResponse"/> containing the result of the embeddings generation operation.
        ''' </returns>
        Public Function GenerateEmbeddings(request As EmbeddingsRequest) As EmbeddingsResponse

            Return Me.GenerateEmbeddingsAsync(request, CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Asynchronously generates embeddings for a given input text with a provided Ollama model.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     EmbeddingsRequest embedRequest = new EmbeddingsRequest {
        '''         Name = "all-minilm",
        '''         Inputs = new[] { "Why is the sky blue?." }
        '''     };
        ''' 
        '''     EmbeddingsResponse embedResponse = 
        '''         await client.Management.GenerateEmbeddingsAsync(embedRequest, CancellationToken.None);
        ''' 
        '''     string responseJson = embedResponse.ToString(writeIndented: true);
        '''     Console.WriteLine(responseJson);
        '''     
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        '''  Using client As New OllamaClient()
        ''' 
        '''     Dim embedRequest As New EmbeddingsRequest With {
        '''         .Model = "all-minilm",
        '''         .Inputs = {"Why is the sky blue?."}
        '''     }
        ''' 
        '''     Dim embedResponse As EmbeddingsResponse =
        '''         Await client.Generation.GenerateEmbeddingsAsync(embedRequest, CancellationToken.None)
        ''' 
        '''     Dim responseJson As String = embedResponse.ToString(writeIndented:=False)
        '''     Console.WriteLine(responseJson)
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example that demonstrates how to perform semantic search using Ollama embeddings.
        ''' <code language="VB">
        ''' Public Async Function RunSemanticSearchExampleAsync() As Task
        ''' 
        '''     Dim baseText As String = "I write desktop software applications using the VB.NET language, do you?."
        '''     Dim similarText As String = "I develop Windows programs with Visual Basic dotnet, do you?."
        '''     Dim randomText As String = "I believe in aliens that can cook a potato with their mind, do you?."
        ''' 
        '''     Using client As New OllamaClient()
        ''' 
        '''         Console.WriteLine("Generating embeddings from Ollama...")
        '''         Console.WriteLine()
        ''' 
        '''         ' 1. Get the embedding array for the base text
        '''         Dim baseRequest As New EmbeddingsRequest("mxbai-embed-large", baseText)
        '''         Dim baseResponse As EmbeddingsResponse = Await client.Generation.GenerateEmbeddingsAsync(baseRequest, CancellationToken.None)
        '''         Dim baseVector As Double() = baseResponse.Embeddings(0)
        ''' 
        '''         ' 2. Get the embedding array for the similar text
        '''         Dim similarRequest As New EmbeddingsRequest("mxbai-embed-large", similarText)
        '''         Dim similarResponse As EmbeddingsResponse = Await client.Generation.GenerateEmbeddingsAsync(similarRequest, CancellationToken.None)
        '''         Dim similarVector As Double() = similarResponse.Embeddings(0)
        ''' 
        '''         ' 3. Get the embedding array for the random text
        '''         Dim randomRequest As New EmbeddingsRequest("mxbai-embed-large", randomText)
        '''         Dim randomResponse As EmbeddingsResponse = Await client.Generation.GenerateEmbeddingsAsync(randomRequest, CancellationToken.None)
        '''         Dim randomVector As Double() = randomResponse.Embeddings(0)
        ''' 
        '''         Console.WriteLine("Calculating semantic similarities...")
        '''         Console.WriteLine(New String("-"c, 40))
        ''' 
        '''         ' Compare Base vs Similar
        '''         Dim matchSimilar As Double = CalculateSemanticSimilarity(baseVector, similarVector)
        '''         Dim percentageSimilar As Double = Math.Round(matchSimilar * 100.0R, 2)
        '''         Console.WriteLine($"Base vs Similar : {percentageSimilar}% match.")
        ''' 
        '''         ' Compare Base vs Random
        '''         Dim matchRandom As Double = CalculateSemanticSimilarity(baseVector, randomVector)
        '''         Dim percentageRandom As Double = Math.Round(matchRandom * 100.0R, 2)
        '''         Console.WriteLine($"Base vs Potato  : {percentageRandom}% match.")
        ''' 
        '''     End Using
        ''' End Function
        ''' 
        ''' ' Calculates the semantic similarity between two text embeddings returned by Ollama.
        ''' ' The result is a value between -1.0 (opposite) and 1.0 (identical meaning).
        ''' Private Function CalculateSemanticSimilarity(searchVector As Double(),
        '''                                              targetVector As Double()) As Double
        ''' 
        '''     Dim dotProduct As Double = 0.0R
        '''     Dim magnitudeSearch As Double = 0.0R
        '''     Dim magnitudeTarget As Double = 0.0R
        ''' 
        '''     Dim arrayLength As Integer = searchVector.Length
        ''' 
        '''     For i As Integer = 0 To arrayLength - 1
        '''         Dim valA As Double = searchVector(i)
        '''         Dim valB As Double = targetVector(i)
        ''' 
        '''         dotProduct += (valA * valB)
        '''         magnitudeSearch += (valA * valA)
        '''         magnitudeTarget += (valB * valB)
        '''     Next
        ''' 
        '''     Return If(magnitudeSearch = 0.0R OrElse magnitudeTarget = 0.0R, 0.0R,
        '''               dotProduct / (Math.Sqrt(magnitudeSearch) * Math.Sqrt(magnitudeTarget)))
        ''' End Function
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="EmbeddingsRequest"/> containing the request parameters for the embeddings generation operation.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        '''
        ''' <returns>
        ''' A <see cref="Task(Of EmbeddingsResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the <see cref="EmbeddingsResponse"/> with the result of the embeddings generation operation.
        ''' </returns>
        Public Async Function GenerateEmbeddingsAsync(request As EmbeddingsRequest,
                                                      cancellationToken As CancellationToken
                                                     ) As Task(Of EmbeddingsResponse)

            Return Await OllamaClientHelper.PostAsJsonAsync(Of EmbeddingsRequest, EmbeddingsResponse)(
                                                            Me.Client, "embed", request, cancellationToken).
                                            ConfigureAwait(continueOnCapturedContext:=False)
        End Function

#End Region

#Region " /api/generate " ' https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-completion

        ''' <summary>
        ''' Generates a completion response from a given prompt using a specified model.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     GenerationOptions genOptions = new GenerationOptions {
        '''         MaxTokens = -1
        '''     };
        ''' 
        '''     CompletionRequest genRequest = new CompletionRequest {
        '''         Model = "qwen2.5vl:7b",
        '''         Prompt = "Why is the sky blue?.",
        '''         System = "You are a helpful and knowledgeable AI assistant. Provide clear, concise, and scientifically accurate answers.",
        '''         Options = genOptions
        '''     };
        ''' 
        '''     CompletionResponse genResponse =
        '''         client.Generation.GenerateCompletion(genRequest);
        ''' 
        '''     string responseJson = genResponse.ToString(writeIndented: true);
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
        '''     Dim genOptions As New GenerationOptions With {
        '''         .MaxTokens = -1
        '''     }
        ''' 
        '''     Dim genRequest As New CompletionRequest With {
        '''         .Model = "qwen2.5vl:7b",
        '''         .Prompt = "Why is the sky blue?.",
        '''         .System = "You are a helpful and knowledgeable AI assistant. Provide clear, concise, and scientifically accurate answers.",
        '''         .Options = genOptions
        '''     }
        ''' 
        '''     Dim genResponse As CompletionResponse =
        '''         client.Generation.GenerateCompletion(genRequest)
        ''' 
        '''     Dim responseJson As String = genResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' 
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="CompletionRequest"/> containing the request parameters for the completion response generation operation.
        ''' </param>
        ''' 
        ''' <returns>
        ''' An <see cref="CompletionResponse"/> containing the result of the completion response generation operation.
        ''' </returns>
        ''' 
        ''' <exception cref="ArgumentNullException">
        ''' Thrown when the <paramref name="request"/> parameter is null.
        ''' </exception>
        ''' 
        ''' <exception cref="HttpRequestException">
        ''' Thrown when the HTTP request fails or returns a non-success status code.
        ''' </exception>
        ''' 
        ''' <exception cref="InvalidOperationException">
        ''' Thrown when the JSON response is empty, or does not yield any parsed JSON objects.
        ''' </exception>
        ''' 
        ''' <exception cref="JsonException">
        ''' Thrown when the JSON response is malformed and cannot be correctly deserialized.
        ''' </exception>
        Public Function GenerateCompletion(request As CompletionRequest) As CompletionResponse

            Return Me.GenerateCompletionAsync(request, CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Asynchronously generates a completion response from a given prompt using a specified model.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     GenerationOptions genOptions = new GenerationOptions {
        '''         MaxTokens = -1
        '''     };
        ''' 
        '''     CompletionRequest genRequest = new CompletionRequest {
        '''         Model = "qwen2.5vl:7b",
        '''         Prompt = "Why is the sky blue?.",
        '''         System = "You are a helpful and knowledgeable AI assistant. Provide clear, concise, and scientifically accurate answers.",
        '''         Options = genOptions
        '''     };
        ''' 
        '''     CompletionResponse genResponse =
        '''         await client.Generation.GenerateCompletionAsync(genRequest, CancellationToken.None);
        ''' 
        '''     string responseJson = genResponse.ToString(writeIndented: true);
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
        '''     Dim genOptions As New GenerationOptions With {
        '''         .MaxTokens = -1
        '''     }
        ''' 
        '''     Dim genRequest As New CompletionRequest With {
        '''         .Model = "qwen2.5vl:7b",
        '''         .Prompt = "Why is the sky blue?.",
        '''         .System = "You are a helpful and knowledgeable AI assistant. Provide clear, concise, and scientifically accurate answers.",
        '''         .Options = genOptions
        '''     }
        ''' 
        '''     Dim genResponse As CompletionResponse =
        '''         Await client.Generation.GenerateCompletionAsync(genRequest, CancellationToken.None)
        ''' 
        '''     Dim responseJson As String = genResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' 
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="CompletionRequest"/> containing the request parameters for the completion response generation operation.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        '''
        ''' <returns>
        ''' A <see cref="Task(Of CompletionResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the <see cref="CompletionResponse"/> with the result of the Completion response generation operation.
        ''' </returns>
        ''' 
        ''' <exception cref="ArgumentNullException">
        ''' Thrown when the <paramref name="request"/> parameter is null.
        ''' </exception>
        ''' 
        ''' <exception cref="HttpRequestException">
        ''' Thrown when the HTTP request fails or returns a non-success status code.
        ''' </exception>
        ''' 
        ''' <exception cref="InvalidOperationException">
        ''' Thrown when the JSON response is empty, or does not yield any parsed JSON objects.
        ''' </exception>
        ''' 
        ''' <exception cref="JsonException">
        ''' Thrown when the JSON response is malformed and cannot be correctly deserialized.
        ''' </exception>
        Public Async Function GenerateCompletionAsync(request As CompletionRequest,
                                                      cancellationToken As CancellationToken
                                                     ) As Task(Of CompletionResponse)

            ' Disable streaming to guarantee the API returns a single JSON object instead of NDJSON.
            request.Stream = False

            Return Await OllamaClientHelper.PostAsJsonAsync(Of CompletionRequest, CompletionResponse)(
                                                            Me.Client, "generate", request, cancellationToken).
                                            ConfigureAwait(continueOnCapturedContext:=False)

        End Function

        ''' <summary>
        ''' Asynchronously generates a completion response from a given prompt using a specified model, 
        ''' and streams the response incrementally via a callback delegate.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     GenerationOptions genOptions = new GenerationOptions {
        '''         MaxTokens = -1
        '''     };
        ''' 
        '''     CompletionRequest genRequest = new CompletionRequest {
        '''         Model = "qwen2.5vl:7b",
        '''         Prompt = "Why is the sky blue?.",
        '''         System = "You are a helpful and knowledgeable AI assistant. Provide clear, concise, and scientifically accurate answers.",
        '''         Options = genOptions
        '''     };
        ''' 
        '''     Action&lt;CompletionResponse&gt; onChunkReceived =
        '''         chunk =&gt; {
        '''             // Handle the streamed response here.
        '''             Console.Write(chunk.Response);
        '''         };
        ''' 
        '''     CompletionResponse genResponse =
        '''         await client.Generation.StreamCompletionAsync(genRequest, onChunkReceived, CancellationToken.None);
        ''' 
        '''     string responseJson = genResponse.ToString(writeIndented: true);
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
        '''     Dim genOptions As New GenerationOptions With {
        '''         .MaxTokens = -1
        '''     }
        ''' 
        '''     Dim genRequest As New CompletionRequest With {
        '''         .Model = "qwen2.5vl:7b",
        '''         .Prompt = "Why is the sky blue?.",
        '''         .System = "You are a helpful and knowledgeable AI assistant. Provide clear, concise, and scientifically accurate answers.",
        '''         .Options = genOptions
        '''     }
        ''' 
        '''     Dim onChunkReceived As Action(Of CompletionResponse) =
        '''         Sub(chunk) ' Handle the streamed response here.
        '''             Console.Write(chunk.Response)
        '''         End Sub
        ''' 
        '''     Dim genResponse As CompletionResponse =
        '''         Await client.Generation.StreamCompletionAsync(genRequest, onChunkReceived, CancellationToken.None)
        ''' 
        '''     Dim responseJson As String = genResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' 
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="CompletionRequest"/> containing the request parameters for the completion response generation operation.
        ''' </param>
        ''' 
        ''' <param name="onChunkReceived">
        ''' An <see cref="Action(Of CompletionResponse)"/> callback that is invoked every time a 
        ''' new chunk is received from the model response.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of CompletionResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the <see cref="CompletionResponse"/> with the result of the completion response generation operation.
        ''' </returns>
        ''' 
        ''' <exception cref="ArgumentNullException">
        ''' Thrown when the <paramref name="request"/>, or <paramref name="onChunkReceived"/> parameter is null.
        ''' </exception>
        ''' 
        ''' <exception cref="HttpRequestException">
        ''' Thrown when the HTTP request fails or returns a non-success status code.
        ''' </exception>
        ''' 
        ''' <exception cref="InvalidOperationException">
        ''' Thrown when the API response does not yield any parsed JSON objects.
        ''' </exception>
        ''' 
        ''' <exception cref="JsonException">
        ''' Thrown when a JSON chunk is malformed and cannot be correctly deserialized.
        ''' </exception>
        Public Async Function StreamCompletionAsync(request As CompletionRequest,
                                                    onChunkReceived As Action(Of CompletionResponse),
                                                    cancellationToken As CancellationToken
                                                   ) As Task(Of CompletionResponse)

            ' Enable streaming to guarantee the API returns a NDJSON (Newline Delimited JSON) instead of a single JSON object.
            If request IsNot Nothing Then ' We later throw a 'NullReferenceException' in 'StreamingPostAsJsonAsync' if request is null.
                request.Stream = True
            End If

            ' We need to accumulate the text ourselves because the generic method only fires the event per chunk.
            Dim responseBuilder As New StringBuilder(capacity:=1024)

            ' Receive the value directly from the streaming processor.
            Dim finalChunk As CompletionResponse =
                Await OllamaClientHelper.StreamingPostAsJsonAsync(Of CompletionRequest, CompletionResponse)(
                    Me.Client, "generate", request,
                    Sub(chunk As CompletionResponse)
                        ' Fire the user's callback so it prints to the console in real-time
                        onChunkReceived.Invoke(chunk)

                        ' Accumulate the text
                        responseBuilder.Append(chunk.Response)
                    End Sub,
                    cancellationToken
            ).ConfigureAwait(continueOnCapturedContext:=False)

            If finalChunk Is Nothing Then
                Throw New InvalidOperationException("The API stream did not yield any parsed JSON objects.")
            End If

            Dim baseFinalChunk As ResponseBase = finalChunk

            ' If the HTTP request failed (e.g., 404 or 500 error), do NOT aggregate totals.
            ' Just return the final chunk which already contains the properly hydrated ErrorMessage and StatusCode properties.
            If Not baseFinalChunk.IsSuccessful Then
                Return finalChunk
            End If

            ' If the request was successful, reconstruct the aggregated response.

#Disable Warning BC40000 ' Type or member is obsolete

            ' Reconstruct the final aggregated response
            Dim generateResponse As New CompletionResponse(
                model:=finalChunk.Model,
                createdAt:=finalChunk.CreatedAt,
                response:=responseBuilder.ToString(),
                done:=finalChunk.Done,
                doneReason:=finalChunk.DoneReason,
                context:=finalChunk.Context,
                totalDuration:=finalChunk.TotalDuration,
                loadDuration:=finalChunk.LoadDuration,
                promptEvalCount:=finalChunk.PromptEvalCount,
                promptEvalDuration:=finalChunk.PromptEvalDuration,
                evalCount:=finalChunk.EvalCount,
                evalDuration:=finalChunk.EvalDuration
            )

#Enable Warning BC40000 ' Type or member is obsolete

            ' Hydrate the newly created object with the HTTP 200 OK metadata from the final chunk.
            Dim baseAggregated As ResponseBase = generateResponse
            baseAggregated.HydrateMetadata(baseFinalChunk.IsSuccessful,
                                           baseFinalChunk.StatusCode,
                                           baseFinalChunk.ReasonPhrase,
                                           rawJson:=Nothing)

            Return generateResponse
        End Function

#End Region

#Region " /api/chat " ' https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-chat-completion

        ''' <summary>
        ''' Generates a chat completion response using a specified model.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     List&lt;ChatMessage&gt; messages = new List&lt;ChatMessage&gt; {
        '''         new ChatMessage {
        '''             Role = RoleOption.User, // Or simply "user" string.
        '''             Content = "Why the color of the sky is blue?."
        '''         }
        '''     };
        ''' 
        '''     GenerationOptions chatOptions = new GenerationOptions {
        '''         MaxTokens = -1
        '''     };
        ''' 
        '''     ChatCompletionRequest chatRequest = new ChatCompletionRequest {
        '''         Model = "deepseek-r1:1.5b",
        '''         Messages = messages,
        '''         Think = ThinkOption.Max, // Or simply "max" string.
        '''         Options = chatOptions
        '''     };
        ''' 
        '''     ChatCompletionResponse chatResponse =
        '''         client.Generation.GenerateChatCompletion(chatRequest);
        ''' 
        '''     string responseJson = chatResponse.ToString(writeIndented: true);
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
        '''     Dim messages As New List(Of ChatMessage) From {
        '''         New ChatMessage With {
        '''             .Role = RoleOption.User, ' Or simply "user" string.
        '''             .Content = "Why the color of the sky is blue?."
        '''         }
        '''     }
        ''' 
        '''     Dim chatOptions As New GenerationOptions With {
        '''         .MaxTokens = -1
        '''     }
        ''' 
        '''     Dim chatRequest As New ChatCompletionRequest With {
        '''         .Model = "deepseek-r1:1.5b",
        '''         .Messages = messages,
        '''         .Think = ThinkOption.Max, ' Or simply "max" string.
        '''         .Options = chatOptions
        '''     }
        ''' 
        '''     Dim chatResponse As ChatCompletionResponse =
        '''         client.Generation.GenerateChatCompletion(chatRequest)
        ''' 
        '''     Dim responseJson As String = chatResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="ChatCompletionRequest"/> containing the request parameters for the chat completion operation.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="ChatCompletionResponse"/> containing the result of the chat completion operation.
        ''' </returns>
        Public Function GenerateChatCompletion(request As ChatCompletionRequest) As ChatCompletionResponse

            Return Me.GenerateChatCompletionAsync(request, CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Asynchronously generates a chat completion response using a specified model.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     List&lt;ChatMessage&gt; messages = new List&lt;ChatMessage&gt; {
        '''         new ChatMessage {
        '''             Role = RoleOption.User, // Or simply "user" string.
        '''             Content = "Why the color of the sky is blue?."
        '''         }
        '''     };
        ''' 
        '''     GenerationOptions chatOptions = new GenerationOptions {
        '''         MaxTokens = -1
        '''     };
        ''' 
        '''     ChatCompletionRequest chatRequest = new ChatCompletionRequest {
        '''         Model = "deepseek-r1:1.5b",
        '''         Messages = messages,
        '''         Think = ThinkOption.Max, // Or simply "max" string.
        '''         Options = chatOptions
        '''     };
        ''' 
        '''     ChatCompletionResponse chatResponse =
        '''         await client.Generation.GenerateChatCompletionAsync(chatRequest, CancellationToken.None);
        ''' 
        '''     string responseJson = chatResponse.ToString(writeIndented: true);
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
        '''     Dim messages As New List(Of ChatMessage) From {
        '''         New ChatMessage With {
        '''             .Role = RoleOption.User, ' Or simply "user" string.
        '''             .Content = "Why the color of the sky is blue?."
        '''         }
        '''     }
        ''' 
        '''     Dim chatOptions As New GenerationOptions With {
        '''         .MaxTokens = -1
        '''     }
        ''' 
        '''     Dim chatRequest As New ChatCompletionRequest With {
        '''         .Model = "deepseek-r1:1.5b",
        '''         .Messages = messages,
        '''         .Think = ThinkOption.Max, ' Or simply "max" string.
        '''         .Options = chatOptions
        '''     }
        ''' 
        '''     Dim chatResponse As ChatCompletionResponse =
        '''         Await client.Generation.GenerateChatCompletionAsync(chatRequest, CancellationToken.None)
        ''' 
        '''     Dim responseJson As String = chatResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="ChatCompletionRequest"/> containing the request parameters for the chat completion operation.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of ChatCompletionResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the aggregated <see cref="ChatCompletionResponse"/> with the result of the chat completion operation.
        ''' </returns>
        <DebuggerStepThrough>
        Public Async Function GenerateChatCompletionAsync(request As ChatCompletionRequest,
                                                          cancellationToken As CancellationToken
                                                         ) As Task(Of ChatCompletionResponse)

            ' Disable streaming to guarantee the API returns a single JSON object instead of NDJSON.
            request.Stream = False

            Return Await OllamaClientHelper.PostAsJsonAsync(Of ChatCompletionRequest, ChatCompletionResponse)(
                                                            Me.Client, "chat", request, cancellationToken, Nothing).
                                            ConfigureAwait(continueOnCapturedContext:=False)
        End Function

        ''' <summary>
        ''' Asynchronously generates a chat completion response using a specified model, 
        ''' and streams the response incrementally via a callback delegate.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        '''     
        '''     List&lt;ChatMessage&gt; messages = new List&lt;ChatMessage&gt; {
        '''         new ChatMessage {
        '''             Role = RoleOption.User, // Or simply "user" string.
        '''             Content = "Why the color of the sky is blue?."
        '''         }
        '''     };
        ''' 
        '''     GenerationOptions chatOptions = new GenerationOptions {
        '''         MaxTokens = -1
        '''     };
        ''' 
        '''     ChatCompletionRequest chatRequest = new ChatCompletionRequest {
        '''         Model = "deepseek-r1:1.5b",
        '''         Messages = messages,
        '''         Think = ThinkOption.Max, // Or simply "max" string.
        '''         Options = chatOptions
        '''     };
        ''' 
        '''     Action&lt;ChatCompletionResponse&gt; onChunkReceived =
        '''         chunk =&gt; {
        '''             if (chunk.Message != null) {
        '''                 // If the model outputs a reasoning trace, print it in gray.
        '''                 if (!string.IsNullOrEmpty(chunk.Message.Thinking)) {
        '''                     Console.ForegroundColor = ConsoleColor.DarkGray;
        '''                     Console.Write(chunk.Message.Thinking);
        '''                     Console.ResetColor();
        '''                 }
        ''' 
        '''                 // If the model outputs standard content, print it using the default color.
        '''                 if (!string.IsNullOrEmpty(chunk.Message.Content)) {
        '''                     Console.Write(chunk.Message.Content);
        '''                 }
        '''             }
        '''         };
        ''' 
        '''     ChatCompletionResponse chatResponse =
        '''         await client.Generation.StreamChatCompletionAsync(chatRequest, onChunkReceived, CancellationToken.None);
        ''' 
        '''     string responseJson = chatResponse.ToString(writeIndented: true);
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
        '''     Dim messages As New List(Of ChatMessage) From {
        '''         New ChatMessage With {
        '''             .Role = RoleOption.User, ' Or simply "user" string.
        '''             .Content = "Why the color of the sky is blue?."
        '''         }
        '''     }
        ''' 
        '''     Dim chatOptions As New GenerationOptions With {
        '''         .MaxTokens = -1
        '''     }
        ''' 
        '''     Dim chatRequest As New ChatCompletionRequest With {
        '''         .Model = "deepseek-r1:1.5b",
        '''         .Messages = messages,
        '''         .Think = ThinkOption.Max, ' Or simply "max" string.
        '''         .Options = chatOptions
        '''     }
        ''' 
        '''     Dim onChunkReceived As Action(Of ChatCompletionResponse) =
        '''         Sub(chunk)
        '''             If chunk.Message IsNot Nothing Then
        '''                 ' If the model outputs a reasoning trace, print it in gray.
        '''                 If Not String.IsNullOrEmpty(chunk.Message.Thinking) Then
        '''                     Console.ForegroundColor = ConsoleColor.DarkGray
        '''                     Console.Write(chunk.Message.Thinking)
        '''                     Console.ResetColor()
        '''                 End If
        ''' 
        '''                 ' If the model outputs standard content, print it using the default color.
        '''                 If Not String.IsNullOrEmpty(chunk.Message.Content) Then
        '''                     Console.Write(chunk.Message.Content)
        '''                 End If
        '''             End If
        '''         End Sub
        ''' 
        '''     Dim chatResponse As ChatCompletionResponse =
        '''         Await client.Generation.StreamChatCompletionAsync(chatRequest, onChunkReceived, CancellationToken.None)
        ''' 
        '''     Dim responseJson As String = chatResponse.ToString(writeIndented:=True)
        '''     Console.WriteLine(responseJson)
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="ChatCompletionRequest"/> containing the request parameters for the chat completion operation.
        ''' </param>
        ''' 
        ''' <param name="onChunkReceived">
        ''' An <see cref="Action(Of ChatCompletionResponse)"/> callback that is invoked every time a 
        ''' new chunk is received from the model response.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of ChatResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the aggregated <see cref="ChatCompletionResponse"/> with the result of the chat completion operation.
        ''' </returns>
        Public Async Function StreamChatCompletionAsync(request As ChatCompletionRequest,
                                                        onChunkReceived As Action(Of ChatCompletionResponse),
                                                        cancellationToken As CancellationToken
                                                       ) As Task(Of ChatCompletionResponse)

            ' Enable streaming to guarantee the API returns a NDJSON (Newline Delimited JSON) instead of a single JSON object.
            If request IsNot Nothing Then ' We later throw a 'NullReferenceException' in 'StreamingPostAsJsonAsync' if request is null.
                request.Stream = True
            End If

            ' We need to accumulate the text and thinking ourselves because the generic method only fires the event per chunk.
            Dim contentBuilder As New StringBuilder(capacity:=1024)
            Dim thinkingBuilder As New StringBuilder(capacity:=1024)
            Dim imagesList As New List(Of ImageOption)()
            Dim messageRole As String = "assistant"

            ' Receive the final chunk directly from the streaming processor.
            Dim finalChunk As ChatCompletionResponse =
                Await OllamaClientHelper.StreamingPostAsJsonAsync(Of ChatCompletionRequest, ChatCompletionResponse)(
                    Me.Client, "chat", request,
                    Sub(chunk As ChatCompletionResponse)
                        ' Fire the user's callback.
                        onChunkReceived.Invoke(chunk)

                        ' Accumulate everything from the Message object.
                        If chunk.Message IsNot Nothing Then
                            ' Accumulate Thinking.
                            If Not String.IsNullOrEmpty(chunk.Message.Thinking) Then
                                thinkingBuilder.Append(chunk.Message.Thinking)
                            End If

                            ' Accumulate Content.
                            If Not String.IsNullOrEmpty(chunk.Message.Content) Then
                                contentBuilder.Append(chunk.Message.Content)
                            End If

                            ' Accumulate Images (Future-proofing for Text-to-Image models).
                            If chunk.Message.Images IsNot Nothing AndAlso chunk.Message.Images.Count > 0 Then
                                imagesList.AddRange(chunk.Message.Images)
                            End If

                            ' Save Role.
                            If Not String.IsNullOrEmpty(chunk.Message.Role) Then
                                messageRole = chunk.Message.Role
                            End If
                        End If
                    End Sub,
                    cancellationToken
            ).ConfigureAwait(continueOnCapturedContext:=False)

            If finalChunk Is Nothing Then
                Throw New InvalidOperationException("The API stream did not yield any parsed JSON objects.")
            End If

            Dim baseFinalChunk As ResponseBase = finalChunk

            ' If the HTTP request failed (e.g., 404 or 500 error), do NOT aggregate totals.
            ' Just return the final chunk which already contains the properly hydrated ErrorMessage and StatusCode properties.
            If Not baseFinalChunk.IsSuccessful Then
                Return finalChunk
            End If

            ' If the request was successful, reconstruct the aggregated message.

            Dim finalMessage As New ChatMessage(
                role:=messageRole,
                content:=contentBuilder.ToString(),
                thinking:=thinkingBuilder.ToString(),
                images:=If(imagesList.Count > 0, imagesList, Nothing),
                toolCalls:=finalChunk.Message?.ToolCalls,
                toolName:=finalChunk.Message?.ToolName
            )

            Dim chatResponse As New ChatCompletionResponse(
                model:=finalChunk.Model,
                message:=finalMessage,
                createdAt:=finalChunk.CreatedAt,
                done:=finalChunk.Done,
                doneReason:=finalChunk.DoneReason,
                totalDuration:=finalChunk.TotalDuration,
                loadDuration:=finalChunk.LoadDuration,
                promptEvalCount:=finalChunk.PromptEvalCount,
                promptEvalDuration:=finalChunk.PromptEvalDuration,
                evalCount:=finalChunk.EvalCount,
                evalDuration:=finalChunk.EvalDuration
            )

            ' Hydrate the newly created object with the HTTP 200 OK metadata from the final chunk.
            Dim baseAggregated As ResponseBase = chatResponse
            baseAggregated.HydrateMetadata(baseFinalChunk.IsSuccessful,
                                           baseFinalChunk.StatusCode,
                                           baseFinalChunk.ReasonPhrase,
                                           rawJson:=Nothing)

            Return chatResponse
        End Function

#End Region

#End Region

    End Class

#End Region

End Namespace
