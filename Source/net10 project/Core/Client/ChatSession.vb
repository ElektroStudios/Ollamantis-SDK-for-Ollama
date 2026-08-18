
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Threading
Imports System.Threading.Tasks

Imports Ollamantis.Contracts
Imports Ollamantis.Core.Helpers
Imports Ollamantis.Entities

#End Region

Namespace Core

#Region " ChatSession "

    ''' <summary>
    ''' Provides the mechanism to create a chat session with an Ollama model, 
    ''' automatically maintaining conversation history across consecutive interactions.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#chat-request-with-history">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(False)>
    <DebuggerStepThrough>
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public Class ChatSession

#Region " Private Fields and Properties"

        ''' <summary>
        ''' The underlying <see cref="OllamaClient"/> instance used to communicate with the Ollama API.
        ''' </summary>
        Protected ReadOnly Client As OllamaClient

        ''' <summary>
        ''' A thread-safe dictionary that stores the conversation history for each unique session identifier.
        ''' </summary>
        Protected ReadOnly Conversations As New ConcurrentDictionary(Of Guid, List(Of ChatMessage))()

        ''' <summary>
        ''' Gets the string to display in the debugger watch window for this instance.
        ''' </summary>
        Private ReadOnly Property DebuggerDisplay As String
            Get
                Dim clientStatus As String = If(Me.Client IsNot Nothing, "Attached", "Null")
                Dim conversationsCount As Integer = If(Me.Conversations IsNot Nothing, Me.Conversations.Count, 0)

                Return $"Client = {clientStatus}, ConversationsCount = {conversationsCount}"
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="ChatSession"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ChatSession"/> class.
        ''' </summary>
        ''' 
        ''' <param name="client">
        ''' The <see cref="OllamaClient"/> instance used to communicate with the Ollama API.
        ''' </param>
        ''' 
        ''' <exception cref="ArgumentNullException">
        ''' Thrown when <paramref name="client"/> is <see langword="Nothing"/>.
        ''' </exception>
        Public Sub New(client As OllamaClient)

            ArgumentValidator.ThrowIfNull(client, NameOf(client))

            Me.Client = client
        End Sub

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Asynchronously generates a chat completion response using a specified model, 
        ''' automatically including the previous conversation history from this <see cref="ChatSession"/>.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     ChatSession session = new ChatSession(client);
        ''' 
        '''     // Generate a unique id for this specific chat session.
        '''     Guid chatId = Guid.NewGuid();
        ''' 
        '''     // Specify the model to use for the chat conversation.
        '''     string model = "qwen2.5vl:7b";
        ''' 
        '''     // Turn 1: Introduce yourself to the model.
        '''     ChatCompletionRequest request1 = new ChatCompletionRequest(chatId) {
        '''         Model = model,
        '''         Messages = new List&lt;ChatMessage&gt; {
        '''             new ChatMessage(
        '''                 RoleOption.User,
        '''                 "Hello, my name is Elektro and I am a VB.NET developer.")
        '''         }
        '''     };
        ''' 
        '''     Console.WriteLine($"user (request 1): {request1.Messages[0].Content}");
        '''     Console.WriteLine();
        ''' 
        '''     ChatCompletionResponse response1 =
        '''         session.GenerateChatCompletion(request1);
        ''' 
        '''     Console.WriteLine($"assistant (response 1): {response1.Message.Content}");
        '''     Console.WriteLine();
        ''' 
        '''     // Turn 2: Test the model's memory.
        '''     ChatCompletionRequest request2 = new ChatCompletionRequest(chatId) {
        '''         Model = model,
        '''         Messages = new List&lt;ChatMessage&gt; {
        '''             new ChatMessage(
        '''                 RoleOption.User,
        '''                 "Do you remember my name and which programming language I use?.")
        '''         }
        '''     };
        ''' 
        '''     Console.WriteLine($"user (request 2): {request2.Messages[0].Content}");
        '''     Console.WriteLine();
        ''' 
        '''     ChatCompletionResponse response2 =
        '''         session.GenerateChatCompletion(request2);
        ''' 
        '''     // The model will/should answer acknowledging your name and your preference for VB.NET.
        '''     Console.WriteLine($"assistant (response 2): {response2.Message.Content}");
        '''     Console.WriteLine();
        '''     Console.WriteLine("Press any key to exit...");
        '''     Console.ReadKey();
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim session As New ChatSession(client)
        ''' 
        '''     ' Generate a unique id for this specific chat session.
        '''     Dim chatid As Guid = Guid.NewGuid()
        ''' 
        '''     ' Specify the model to use for the chat conversation.
        '''     Dim model As String = "qwen2.5vl:7b"
        ''' 
        '''     ' Turn 1: Introduce yourself to the model.
        '''     Dim request1 As New ChatCompletionRequest(chatid) With {
        '''         .Model = model,
        '''         .Messages = New List(Of ChatMessage) From {
        '''             New ChatMessage(RoleOption.User, "Hello, my name is Elektro and I am a VB.NET developer.")
        '''         }
        '''     }
        ''' 
        '''     Console.WriteLine($"user (request 1): {request1.Messages(0).Content}")
        '''     Console.WriteLine()
        ''' 
        '''     Dim response1 As ChatCompletionResponse =
        '''         Await session.GenerateChatCompletion(request1)
        ''' 
        '''     Console.WriteLine($"assistant (response 1): {response1.Message.Content}")
        '''     Console.WriteLine()
        ''' 
        '''     ' Turn 2: Test the model's memory.
        '''     Dim request2 As New ChatCompletionRequest(chatid) With {
        '''         .Model = model,
        '''         .Messages = New List(Of ChatMessage) From {
        '''             New ChatMessage(RoleOption.User, "Do you remember my name and which programming language I use?.")
        '''         }
        '''     }
        ''' 
        '''     Console.WriteLine($"user (request 2): {request2.Messages(0).Content}")
        '''     Console.WriteLine()
        ''' 
        '''     Dim response2 As ChatCompletionResponse =
        '''         session.GenerateChatCompletion(request2)
        ''' 
        '''     ' The model will/should answer acknowledging your name and your preference for VB.NET.
        '''     Console.WriteLine($"assistant (response 2): {response2.Message.Content}")
        '''     Console.WriteLine()
        '''     Console.WriteLine("Press any key to exit...")
        '''     Console.ReadKey()
        ''' 
        ''' End Using
        ''' </code>
        ''' </example>
        ''' 
        ''' <param name="request">
        ''' A <see cref="ChatCompletionRequest"/> containing the request parameters for the chat completion operation.
        ''' </param>
        ''' 
        ''' <returns>
        ''' An <see cref="CompletionResponse"/> containing the result of the completion response generation operation.
        ''' </returns>
        Public Function GenerateChatCompletion(request As ChatCompletionRequest) As ChatCompletionResponse

            Return Me.GenerateChatCompletionAsync(request, CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Asynchronously generates a chat completion response using a specified model, 
        ''' automatically including the previous conversation history from this <see cref="ChatSession"/>.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     ChatSession session = new ChatSession(client);
        ''' 
        '''     // Generate a unique id for this specific chat session.
        '''     Guid chatId = Guid.NewGuid();
        ''' 
        '''     // Specify the model to use for the chat conversation.
        '''     string model = "qwen2.5vl:7b";
        ''' 
        '''     // Turn 1: Introduce yourself to the model.
        '''     ChatCompletionRequest request1 = new ChatCompletionRequest(chatId) {
        '''         Model = model,
        '''         Messages = new List&lt;ChatMessage&gt; {
        '''             new ChatMessage(
        '''                 RoleOption.User,
        '''                 "Hello, my name is Elektro and I am a VB.NET developer.")
        '''         }
        '''     };
        ''' 
        '''     Console.WriteLine($"user (request 1): {request1.Messages[0].Content}");
        '''     Console.WriteLine();
        ''' 
        '''     ChatCompletionResponse response1 =
        '''         await session.GenerateChatCompletionAsync(request1, CancellationToken.None);
        ''' 
        '''     Console.WriteLine($"assistant (response 1): {response1.Message.Content}");
        '''     Console.WriteLine();
        ''' 
        '''     // Turn 2: Test the model's memory.
        '''     ChatCompletionRequest request2 = new ChatCompletionRequest(chatId) {
        '''         Model = model,
        '''         Messages = new List&lt;ChatMessage&gt; {
        '''             new ChatMessage(
        '''                 RoleOption.User,
        '''                 "Do you remember my name and which programming language I use?.")
        '''         }
        '''     };
        ''' 
        '''     Console.WriteLine($"user (request 2): {request2.Messages[0].Content}");
        '''     Console.WriteLine();
        ''' 
        '''     ChatCompletionResponse response2 =
        '''         await session.GenerateChatCompletionAsync(request2, CancellationToken.None);
        ''' 
        '''     // The model will/should answer acknowledging your name and your preference for VB.NET.
        '''     Console.WriteLine($"assistant (response 2): {response2.Message.Content}");
        '''     Console.WriteLine();
        '''     Console.WriteLine("Press any key to exit...");
        '''     Console.ReadKey();
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim session As New ChatSession(client)
        ''' 
        '''     ' Generate a unique id for this specific chat session.
        '''     Dim chatid As Guid = Guid.NewGuid()
        ''' 
        '''     ' Specify the model to use for the chat conversation.
        '''     Dim model As String = "qwen2.5vl:7b"
        ''' 
        '''     ' Turn 1: Introduce yourself to the model.
        '''     Dim request1 As New ChatCompletionRequest(chatid) With {
        '''         .Model = model,
        '''         .Messages = New List(Of ChatMessage) From {
        '''             New ChatMessage(RoleOption.User, "Hello, my name is Elektro and I am a VB.NET developer.")
        '''         }
        '''     }
        ''' 
        '''     Console.WriteLine($"user (request 1): {request1.Messages(0).Content}")
        '''     Console.WriteLine()
        ''' 
        '''     Dim response1 As ChatCompletionResponse =
        '''         Await session.GenerateChatCompletionAsync(request1, CancellationToken.None)
        ''' 
        '''     Console.WriteLine($"assistant (response 1): {response1.Message.Content}")
        '''     Console.WriteLine()
        ''' 
        '''     ' Turn 2: Test the model's memory.
        '''     Dim request2 As New ChatCompletionRequest(chatid) With {
        '''         .Model = model,
        '''         .Messages = New List(Of ChatMessage) From {
        '''             New ChatMessage(RoleOption.User, "Do you remember my name and which programming language I use?.")
        '''         }
        '''     }
        ''' 
        '''     Console.WriteLine($"user (request 2): {request2.Messages(0).Content}")
        '''     Console.WriteLine()
        ''' 
        '''     Dim response2 As ChatCompletionResponse =
        '''         Await session.GenerateChatCompletionAsync(request2, CancellationToken.None)
        ''' 
        '''     ' The model will/should answer acknowledging your name and your preference for VB.NET.
        '''     Console.WriteLine($"assistant (response 2): {response2.Message.Content}")
        '''     Console.WriteLine()
        '''     Console.WriteLine("Press any key to exit...")
        '''     Console.ReadKey()
        ''' 
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
        ''' The task result contains the <see cref="ChatCompletionResponse"/> with the result of the operation.
        ''' </returns>
        Public Async Function GenerateChatCompletionAsync(request As ChatCompletionRequest,
                                                          cancellationToken As CancellationToken
                                                         ) As Task(Of ChatCompletionResponse)

            Return Await Me.InternalGenerateOrStreamChatCompletionAsync(stream:=False,
                                                                        request, onChunkReceived:=Nothing, cancellationToken
                                                                       ).ConfigureAwait(continueOnCapturedContext:=False)
        End Function

        ''' <summary>
        ''' Asynchronously generates a chat completion response using a specified model, 
        ''' and streams the response incrementally via a callback delegate, 
        ''' automatically including the previous conversation history from this <see cref="ChatSession"/>.
        ''' </summary>
        ''' 
        ''' <example> This is a code example for C#.
        ''' <code language="CS">
        ''' using (OllamaClient client = new OllamaClient()) {
        ''' 
        '''     ChatSession session = new ChatSession(client);
        ''' 
        '''     // Generate a unique id for this specific chat session.
        '''     Guid chatId = Guid.NewGuid();
        ''' 
        '''     // Specify the model to use for the chat conversation.
        '''     string model = "qwen2.5vl:7b";
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
        '''     // Turn 1: Introduce yourself to the model.
        '''     ChatCompletionRequest request1 = new ChatCompletionRequest(chatId) {
        '''         Model = model,
        '''         Messages = new List&lt;ChatMessage&gt; {
        '''             new ChatMessage(
        '''                 RoleOption.User,
        '''                 "Hello, my name is Elektro and I am a VB.NET developer.")
        '''         }
        '''     };
        ''' 
        '''     Console.WriteLine($"user (request 1): {request1.Messages[0].Content}");
        '''     Console.WriteLine();
        ''' 
        '''     ChatCompletionResponse response1 =
        '''         await session.StreamChatCompletionAsync(request1, onChunkReceived, CancellationToken.None);
        ''' 
        '''     Console.WriteLine($"assistant (response 1): {response1.Message.Content}");
        '''     Console.WriteLine();
        ''' 
        '''     // Turn 2: Test the model's memory.
        '''     ChatCompletionRequest request2 = new ChatCompletionRequest(chatId) {
        '''         Model = model,
        '''         Messages = new List&lt;ChatMessage&gt; {
        '''             new ChatMessage(
        '''                 RoleOption.User,
        '''                 "Do you remember my name and which programming language I use?.")
        '''         }
        '''     };
        ''' 
        '''     Console.WriteLine($"user (request 2): {request2.Messages[0].Content}");
        '''     Console.WriteLine();
        ''' 
        '''     ChatCompletionResponse response2 =
        '''         await session.StreamChatCompletionAsync(request2, onChunkReceived, CancellationToken.None);
        ''' 
        '''     // The model will/should answer acknowledging your name and your preference for VB.NET.
        '''     Console.WriteLine($"assistant (response 2): {response2.Message.Content}");
        '''     Console.WriteLine();
        '''     Console.WriteLine("Press any key to exit...");
        '''     Console.ReadKey();
        ''' 
        ''' }
        ''' </code>
        ''' </example>
        ''' 
        ''' <example> This is a code example for VB.NET.
        ''' <code language="VB">
        ''' Using client As New OllamaClient()
        ''' 
        '''     Dim session As New ChatSession(client)
        ''' 
        '''     ' Generate a unique id for this specific chat session.
        '''     Dim chatid As Guid = Guid.NewGuid()
        ''' 
        '''     ' Specify the model to use for the chat conversation.
        '''     Dim model As String = "qwen2.5vl:7b"
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
        '''     ' Turn 1: Introduce yourself to the model.
        '''     Dim request1 As New ChatCompletionRequest(chatid) With {
        '''         .Model = model,
        '''         .Messages = New List(Of ChatMessage) From {
        '''             New ChatMessage(RoleOption.User, "Hello, my name is Elektro and I am a VB.NET developer.")
        '''         }
        '''     }
        ''' 
        '''     Console.WriteLine($"user (request 1): {request1.Messages(0).Content}")
        '''     Console.WriteLine()
        ''' 
        '''     Dim response1 As ChatCompletionResponse =
        '''         Await session.StreamChatCompletionAsync(request1, onChunkReceived, CancellationToken.None)
        '''
        '''     Console.WriteLine($"assistant (response 1): {response1.Message.Content}")
        '''     Console.WriteLine()
        ''' 
        '''     ' Turn 2: Test the model's memory.
        '''     Dim request2 As New ChatCompletionRequest(chatid) With {
        '''         .Model = model,
        '''         .Messages = New List(Of ChatMessage) From {
        '''             New ChatMessage(RoleOption.User, "Do you remember my name and which programming language I use?.")
        '''         }
        '''     }
        ''' 
        '''     Console.WriteLine($"user (request 2): {request2.Messages(0).Content}")
        '''     Console.WriteLine()
        ''' 
        '''     Dim response2 As ChatCompletionResponse =
        '''         Await session.StreamChatCompletionAsync(request1, onChunkReceived, CancellationToken.None)
        ''' 
        '''     ' The model will/should answer acknowledging your name and your preference for VB.NET.
        '''     Console.WriteLine($"assistant (response 2): {response2.Message.Content}")
        '''     Console.WriteLine()
        '''     Console.WriteLine("Press any key to exit...")
        '''     Console.ReadKey()
        ''' 
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
        ''' A <see cref="Task(Of ChatCompletionResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the <see cref="ChatCompletionResponse"/> with the result of the operation.
        ''' </returns>
        Public Async Function StreamChatCompletionAsync(request As ChatCompletionRequest,
                                                        onChunkReceived As Action(Of ChatCompletionResponse),
                                                        cancellationToken As CancellationToken
                                                       ) As Task(Of ChatCompletionResponse)

            Return Await Me.InternalGenerateOrStreamChatCompletionAsync(stream:=True,
                                                                        request, onChunkReceived, cancellationToken
                                                                       ).ConfigureAwait(continueOnCapturedContext:=False)
        End Function

#End Region

#Region " Private Methods "

        ''' <summary>
        ''' Asynchronously sends a streamless or a streaming chat completion request, 
        ''' automatically including the previous conversation history from this <see cref="ChatSession"/>.
        ''' </summary>
        ''' 
        ''' <param name="stream">
        ''' A <see cref="Boolean"/> value indicating whether the response will be returned as 
        ''' a single response object (<see langword="False"/>), rather than 
        ''' a stream of objects (<see langword="True"/>).
        ''' </param>
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
        ''' A <see cref="Task(Of ChatCompletionResponse)"/> that represents the asynchronous operation. 
        ''' The task result contains the <see cref="ChatCompletionResponse"/> with the result of the operation.
        ''' </returns>
        Private Async Function InternalGenerateOrStreamChatCompletionAsync(stream As Boolean,
                                                                           request As ChatCompletionRequest,
                                                                           onChunkReceived As Action(Of ChatCompletionResponse),
                                                                           cancellationToken As CancellationToken
                                                                          ) As Task(Of ChatCompletionResponse)

            ArgumentValidator.ThrowIfNull(request, NameOf(request))
            If stream Then
                ArgumentValidator.ThrowIfNull(onChunkReceived, NameOf(onChunkReceived))
            End If

            Dim messages As List(Of ChatMessage) = Nothing

            ' Try to fetch the conversation history; if not found, create a new one.
            If Not Me.Conversations.TryGetValue(request.ConversationId, messages) Then
                messages = New List(Of ChatMessage)()
                Me.Conversations(request.ConversationId) = messages
            End If

            ' Append incoming user messages to the conversation history.
            messages.AddRange(request.Messages)

            ' Replace the request's message list with the fully assembled conversation history.
            request.Messages = messages

            Dim finalResponse As ChatCompletionResponse = Nothing
            If Not stream Then
                ' Disable streaming to guarantee the API returns a single JSON object instead of NDJSON.
                request.Stream = False
                finalResponse = Await Me.Client.Generation.GenerateChatCompletionAsync(request, cancellationToken).
                                                           ConfigureAwait(continueOnCapturedContext:=False)
            Else
                ' Enable streaming to guarantee the API returns a NDJSON (Newline Delimited JSON) instead of a single JSON object.
                request.Stream = True
                finalResponse = Await Me.Client.Generation.StreamChatCompletionAsync(request, onChunkReceived, cancellationToken).
                                                           ConfigureAwait(continueOnCapturedContext:=False)
            End If

            ' Ensure the model's final response is appended to the conversation history.
            ' Deterministic approach: If the finalResponse or its Message is Nothing, it will throw a NullReferenceException.
            messages.Add(finalResponse.Message)

            Return finalResponse
        End Function

#End Region

    End Class

#End Region

End Namespace
