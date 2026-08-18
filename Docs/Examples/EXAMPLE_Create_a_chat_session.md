# 🔄 Create a chat session

Creates a chat session with an Ollama model, automatically maintaining conversation history across consecutive interactions.

- **Ollama Endpoint:** `POST /api/chat`
- **Standard Method:** `ChatSession.GenerateChatCompletionAsync` / `GenerateChatCompletion`
- **Streaming Method:** `ChatSession.StreamChatCompletionAsync`

---

## 💻 Standard Code Example

### C#
```csharp
using (OllamaClient client = new OllamaClient()) {

    ChatSession session = new ChatSession(client);

    // Generate a unique id for this specific chat session.
    Guid chatId = Guid.NewGuid();

    // Specify the model to use for the chat conversation.
    string model = "qwen2.5vl:7b";

    // Turn 1: Introduce yourself to the model.
    ChatCompletionRequest request1 = new ChatCompletionRequest(chatId) {
        Model = model,
        Messages = new List<ChatMessage> {
            new ChatMessage(
                RoleOption.User,
                "Hello, my name is Elektro and I am a VB.NET developer.")
        }
    };

    Console.WriteLine($"user (request 1): {request1.Messages[0].Content}");
    Console.WriteLine();

    ChatCompletionResponse response1 =
        await session.GenerateChatCompletionAsync(request1, CancellationToken.None);

    Console.WriteLine($"assistant (response 1): {response1.Message.Content}");
    Console.WriteLine();

    // Turn 2: Test the model's memory.
    ChatCompletionRequest request2 = new ChatCompletionRequest(chatId) {
        Model = model,
        Messages = new List<ChatMessage> {
            new ChatMessage(
                RoleOption.User,
                "Do you remember my name and which programming language I use?.")
        }
    };

    Console.WriteLine($"user (request 2): {request2.Messages[0].Content}");
    Console.WriteLine();

    ChatCompletionResponse response2 =
        await session.GenerateChatCompletionAsync(request2, CancellationToken.None);

    // The model will/should answer acknowledging your name and your preference for VB.NET.
    Console.WriteLine($"assistant (response 2): {response2.Message.Content}");
    Console.WriteLine();
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}
```

### VB.NET
```vb
Using client As New OllamaClient()

    Dim session As New ChatSession(client)

    ' Generate a unique id for this specific chat session.
    Dim chatid As Guid = Guid.NewGuid()

    ' Specify the model to use for the chat conversation.
    Dim model As String = "qwen2.5vl:7b"

    ' Turn 1: Introduce yourself to the model.
    Dim request1 As New ChatCompletionRequest(chatid) With {
        .Model = model,
        .Messages = New List(Of ChatMessage) From {
            New ChatMessage(RoleOption.User, "Hello, my name is Elektro and I am a VB.NET developer.")
        }
    }

    Console.WriteLine($"user (request 1): {request1.Messages(0).Content}")
    Console.WriteLine()

    Dim response1 As ChatCompletionResponse =
        Await session.GenerateChatCompletionAsync(request1, CancellationToken.None)

    Console.WriteLine($"assistant (response 1): {response1.Message.Content}")
    Console.WriteLine()

    ' Turn 2: Test the model's memory.
    Dim request2 As New ChatCompletionRequest(chatid) With {
        .Model = model,
        .Messages = New List(Of ChatMessage) From {
            New ChatMessage(RoleOption.User, "Do you remember my name and which programming language I use?.")
        }
    }

    Console.WriteLine($"user (request 2): {request2.Messages(0).Content}")
    Console.WriteLine()

    Dim response2 As ChatCompletionResponse =
        Await session.GenerateChatCompletionAsync(request2, CancellationToken.None)

    ' The model will/should answer acknowledging your name and your preference for VB.NET.
    Console.WriteLine($"assistant (response 2): {response2.Message.Content}")
    Console.WriteLine()
    Console.WriteLine("Press any key to exit...")
    Console.ReadKey()

End Using
```

## 💻 Streaming Code Example

### C#
```csharp
using (OllamaClient client = new OllamaClient()) {

    ChatSession session = new ChatSession(client);

    // Generate a unique id for this specific chat session.
    Guid chatId = Guid.NewGuid();

    // Specify the model to use for the chat conversation.
    string model = "qwen2.5vl:7b";

    Action<ChatCompletionResponse> onChunkReceived =
        chunk => {
            if (chunk.Message != null) {
                // If the model outputs a reasoning trace, print it in gray.
                if (!string.IsNullOrEmpty(chunk.Message.Thinking)) {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(chunk.Message.Thinking);
                    Console.ResetColor();
                }

                // If the model outputs standard content, print it using the default color.
                if (!string.IsNullOrEmpty(chunk.Message.Content)) {
                    Console.Write(chunk.Message.Content);
                }
            }
        };

    // Turn 1: Introduce yourself to the model.
    ChatCompletionRequest request1 = new ChatCompletionRequest(chatId) {
        Model = model,
        Messages = new List<ChatMessage> {
            new ChatMessage(
                RoleOption.User,
                "Hello, my name is Elektro and I am a VB.NET developer.")
        }
    };

    Console.WriteLine($"user (request 1): {request1.Messages[0].Content}");
    Console.WriteLine();

    ChatCompletionResponse response1 =
        await session.StreamChatCompletionAsync(request1, onChunkReceived, CancellationToken.None);

    Console.WriteLine($"assistant (response 1): {response1.Message.Content}");
    Console.WriteLine();

    // Turn 2: Test the model's memory.
    ChatCompletionRequest request2 = new ChatCompletionRequest(chatId) {
        Model = model,
        Messages = new List<ChatMessage> {
            new ChatMessage(
                RoleOption.User,
                "Do you remember my name and which programming language I use?.")
        }
    };

    Console.WriteLine($"user (request 2): {request2.Messages[0].Content}");
    Console.WriteLine();

    ChatCompletionResponse response2 =
        await session.StreamChatCompletionAsync(request2, onChunkReceived, CancellationToken.None);

    // The model will/should answer acknowledging your name and your preference for VB.NET.
    Console.WriteLine($"assistant (response 2): {response2.Message.Content}");
    Console.WriteLine();
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();

}
```

### VB.NET
```vb
Using client As New OllamaClient()

    Dim session As New ChatSession(client)

    ' Generate a unique id for this specific chat session.
    Dim chatid As Guid = Guid.NewGuid()

    ' Specify the model to use for the chat conversation.
    Dim model As String = "qwen2.5vl:7b"

    Dim onChunkReceived As Action(Of ChatCompletionResponse) =
        Sub(chunk)
            If chunk.Message IsNot Nothing Then
                ' If the model outputs a reasoning trace, print it in gray.
                If Not String.IsNullOrEmpty(chunk.Message.Thinking) Then
                    Console.ForegroundColor = ConsoleColor.DarkGray
                    Console.Write(chunk.Message.Thinking)
                    Console.ResetColor()
                End If

                ' If the model outputs standard content, print it using the default color.
                If Not String.IsNullOrEmpty(chunk.Message.Content) Then
                    Console.Write(chunk.Message.Content)
                End If
            End If
        End Sub

    ' Turn 1: Introduce yourself to the model.
    Dim request1 As New ChatCompletionRequest(chatid) With {
        .Model = model,
        .Messages = New List(Of ChatMessage) From {
            New ChatMessage(RoleOption.User, "Hello, my name is Elektro and I am a VB.NET developer.")
        }
    }

    Console.WriteLine($"user (request 1): {request1.Messages(0).Content}")
    Console.WriteLine()

    Dim response1 As ChatCompletionResponse =
        Await session.StreamChatCompletionAsync(request1, onChunkReceived, CancellationToken.None)

    Console.WriteLine($"assistant (response 1): {response1.Message.Content}")
    Console.WriteLine()

    ' Turn 2: Test the model's memory.
    Dim request2 As New ChatCompletionRequest(chatid) With {
        .Model = model,
        .Messages = New List(Of ChatMessage) From {
            New ChatMessage(RoleOption.User, "Do you remember my name and which programming language I use?.")
        }
    }

    Console.WriteLine($"user (request 2): {request2.Messages(0).Content}")
    Console.WriteLine()

    Dim response2 As ChatCompletionResponse =
        Await session.StreamChatCompletionAsync(request1, onChunkReceived, CancellationToken.None)

    ' The model will/should answer acknowledging your name and your preference for VB.NET.
    Console.WriteLine($"assistant (response 2): {response2.Message.Content}")
    Console.WriteLine()
    Console.WriteLine("Press any key to exit...")
    Console.ReadKey()

End Using
```

## 📝 Intermediate Console Output:
```
user (request 1): Hello, my name is Elektro and I am a VB.NET developer.

assistant (response 1): Hello Elektro! It's great to meet you. As a VB.NET developer, you're part of a rich and diverse community of developers who work with Visual Basic, a programming language that has been around for quite some time and is still widely used, especially in the Windows development space.

user (request 2): Do you remember my name and which programming language I use?.

assistant (response 2): Yes, I remember your name is Elektro and you are a VB.NET developer. It's great to know that you're familiar with VB.NET, a powerful language that's widely used for Windows application development. If you have any specific questions or need help with VB.NET, feel free to ask!

Press any key to exit...
```

## 📝 JSON Output

### On success:
```json
{
  "model": "qwen2.5vl:7b",
  "message": {
    "role": "assistant",
    "content": "Hello Elektro! It\u0027s great to meet you. As a VB.NET developer, you\u0027re part of a rich and diverse community of developers who work with Visual Basic, a programming language that has been around for quite some time and is still widely used, especially in the Windows development space."
  },
  "created_at": "2026-08-18T08:54:34.1140854+00:00",
  "created_at_formatted": "Tuesday, August 18, 2026 at 10:54:34",
  "done": true,
  "done_reason": "stop",
  "total_duration": 25458577800,
  "total_duration_formatted": "25.46s",
  "load_duration": 7539172200,
  "load_duration_formatted": "7.54s",
  "prompt_eval_count": 35,
  "prompt_eval_duration": 324535000,
  "prompt_eval_duration_formatted": "324.54ms",
  "eval_count": 154,
  "eval_duration": 17591982000,
  "eval_duration_formatted": "17.59s",
  "tokens_per_second": 8.8,
  "isSuccessful": true,
  "statusCode": 200,
  "reasonPhrase": "OK"
}
```

### On failure:
```json
{
  "model": null,
  "isSuccessful": false,
  "statusCode": 400,
  "reasonPhrase": "Bad Request",
  "errorMessage": "\u0022llama3.2\u0022 does not support thinking"
}
```
