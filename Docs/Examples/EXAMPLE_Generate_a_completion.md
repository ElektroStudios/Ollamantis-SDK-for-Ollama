# 📝 Generate a text completion

Generates a text completion from a prompt using a specified model.

- **Ollama Endpoint:** `POST /api/generate`
- **Standard Method:** `GenerateCompletionAsync` / `GenerateCompletion`
- **Streaming Method:** `StreamCompletionAsync`

---

## 💻 Standard Code Example

### C#
```csharp
using (OllamaClient client = new OllamaClient()) {

    GenerationOptions genOptions = new GenerationOptions {
        MaxTokens = -1
    };

    CompletionRequest genRequest = new CompletionRequest {
        Model = "qwen2.5vl:7b",
        System = "You are a helpful and knowledgeable AI assistant. Provide clear and scientifically accurate answers.",
        Prompt = "Why is the sky blue?.",
        Options = genOptions
    };

    CompletionResponse genResponse =
        await client.Generation.GenerateCompletionAsync(genRequest, CancellationToken.None);

    string responseJson = genResponse.ToString(writeIndented: true);
    Console.WriteLine(responseJson);

}
```

### VB.NET
```vb
Using client As New OllamaClient()

    Dim genOptions As New GenerationOptions With {
        .MaxTokens = -1
    }

    Dim genRequest As New CompletionRequest With {
        .Model = "qwen2.5vl:7b",
        .System = "You are a helpful and knowledgeable AI assistant. Provide clear and scientifically accurate answers.",
        .Prompt = "Why is the sky blue?.",
        .Options = genOptions
    }

    Dim genResponse As CompletionResponse =
        Await client.Generation.GenerateCompletionAsync(genRequest, CancellationToken.None)

    Dim responseJson As String = genResponse.ToString(writeIndented:=True)
    Console.WriteLine(responseJson)

End Using
```

## 💻 Streaming Code Example

### C#
```csharp
using (OllamaClient client = new OllamaClient()) {

    GenerationOptions genOptions = new GenerationOptions {
        MaxTokens = -1
    };

    CompletionRequest genRequest = new CompletionRequest {
        Model = "qwen2.5vl:7b",
        System = "You are a helpful and knowledgeable AI assistant. Provide clear and scientifically accurate answers.",
        Prompt = "Why is the sky blue?.",
        Options = genOptions
    };

    Action<CompletionResponse> onChunkReceived =
        chunk => {
            // Handle the streamed response here.
            Console.Write(chunk.Response);
        };

    CompletionResponse genResponse =
        await client.Generation.StreamCompletionAsync(genRequest, onChunkReceived, CancellationToken.None);

    string responseJson = genResponse.ToString(writeIndented: true);
    Console.WriteLine(responseJson);

}
```

### VB.NET
```vb
Using client As New OllamaClient()

    Dim genOptions As New GenerationOptions With {
        .MaxTokens = -1
    }

    Dim genRequest As New CompletionRequest With {
        .Model = "qwen2.5vl:7b",
        .System = "You are a helpful and knowledgeable AI assistant. Provide clear and scientifically accurate answers.",
        .Prompt = "Why is the sky blue?.",
        .Options = genOptions
    }

    Dim onChunkReceived As Action(Of CompletionResponse) =
        Sub(chunk) ' Handle the streamed response here.
            Console.Write(chunk.Response)
        End Sub

    Dim genResponse As CompletionResponse =
        Await client.Generation.StreamCompletionAsync(genRequest, onChunkReceived, CancellationToken.None)

    Dim responseJson As String = genResponse.ToString(writeIndented:=True)
    Console.WriteLine(responseJson)

End Using
```

## 📝 Intermediate Console Output (For Streaming Code Example):
```
The sky appears blue primarily due to a phenomenon called Rayleigh scattering. When sunlight enters Earth's atmosphere, it encounters air molecules and other particles. According to the Rayleigh scattering theory, shorter wavelengths of light are scattered more effectively than longer wavelengths. The blue and violet wavelengths (shorter wavelengths) are scattered more efficiently than the red and orange wavelengths (longer wavelengths). This causes these shorter wavelengths to scatter in all directions, including towards the observer's eye, making the sky appear blue.

However, because violet light is absorbed by oxygen and water in the atmosphere, blue light is predominantly scattered. The combined effect of this scattering and the absorption of violet light results in a predominantly blue hue. As the sun rises or sets, the light has to travel through more of the atmosphere to reach the observer, which scatters the blue light more effectively than the red light, making the sky appear more yellow or orange.

The scattering effect is strongest near the horizon, where sunlight has to travel through a thicker layer of atmosphere. This is why the sky looks darker or even black at the horizon, as the light that reaches there has been scattered and absorbed more extensively.
```

## 📝 JSON Output

### On success:
```json
{
  "model": "qwen2.5vl:7b",
  "response": "The sky appears blue primarily due to a phenomenon called Rayleigh scattering. When sunlight enters Earth\u0027s atmosphere, it encounters air molecules and other particles. According to the Rayleigh scattering theory, shorter wavelengths of light are scattered more effectively than longer wavelengths. The blue and violet wavelengths (shorter wavelengths) are scattered more efficiently than the red and orange wavelengths (longer wavelengths). This causes these shorter wavelengths to scatter in all directions, including towards the observer\u0027s eye, making the sky appear blue.\n\nHowever, because violet light is absorbed by oxygen and water in the atmosphere, blue light is predominantly scattered. The combined effect of this scattering and the absorption of violet light results in a predominantly blue hue. As the sun rises or sets, the light has to travel through more of the atmosphere to reach the observer, which scatters the blue light more effectively than the red light, making the sky appear more yellow or orange.\n\nThe scattering effect is strongest near the horizon, where sunlight has to travel through a thicker layer of atmosphere. This is why the sky looks darker or even black at the horizon, as the light that reaches there has been scattered and absorbed more extensively.",
  "context": [
    151644,
    8948,
    ...
    41717,
    13
  ],
  "created_at": "2026-08-18T06:05:37.5565699+00:00",
  "created_at_formatted": "Tuesday, August 18, 2026 at 08:05:37",
  "done": true,
  "done_reason": "stop",
  "total_duration": 50178254000,
  "total_duration_formatted": "50.18s",
  "load_duration": 21494289700,
  "load_duration_formatted": "21.49s",
  "prompt_eval_count": 38,
  "prompt_eval_duration": 331837000,
  "prompt_eval_duration_formatted": "331.84ms",
  "eval_count": 232,
  "eval_duration": 28349586000,
  "eval_duration_formatted": "28.35s",
  "tokens_per_second": 8.2,
  "isSuccessful": true,
  "statusCode": 200,
  "reasonPhrase": "OK"
}
```

### On failure:
```json
{
  "model": null,
  "response": null,
  "context": null,
  "isSuccessful": false,
  "statusCode": 400,
  "reasonPhrase": "Bad Request",
  "errorMessage": "\u0022qwen2.5vl:7b\u0022 does not support thinking"
}
```
