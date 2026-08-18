# 📊 Generate vector embeddings

Creates vector embeddings representing the input text.

- **Ollama Endpoint:** `POST /api/embed`
- **Method:** `OllamaClient.Management.GenerateEmbeddingsAsync` / `GenerateEmbeddings`

---

## 💻 Code Example

### C#
```csharp
using (OllamaClient client = new OllamaClient()) {

    EmbeddingsRequest embedRequest = new EmbeddingsRequest {
        Name = "all-minilm",
        Inputs = new[] { "Why is the sky blue?." }
    };

    EmbeddingsResponse embedResponse = 
        await client.Management.GenerateEmbeddingsAsync(embedRequest, CancellationToken.None);

    string responseJson = embedResponse.ToString(writeIndented: true);
    Console.WriteLine(responseJson);
    
}
```

### VB.NET
```vb
Using client As New OllamaClient()

    Dim embedRequest As New EmbeddingsRequest With {
        .Model = "all-minilm",
        .Inputs = {"Why is the sky blue?."}
    }

    Dim embedResponse As EmbeddingsResponse =
        Await client.Generation.GenerateEmbeddingsAsync(embedRequest, CancellationToken.None)

    Dim responseJson As String = embedResponse.ToString(writeIndented:=True)
    Console.WriteLine(responseJson)
End Using
```

## 📝 JSON Output

### On success:
```json
{
  "model": "all-minilm",
  "embeddings": [
    [
      0.017421605,
      -0.019393096,
      0.07122929,
      0.057451103,
      ...
      0.06708114,
      0.011635004,
      0.1297694,
      0.037748676
    ]
  ],
  "total_duration": 1936719200,
  "total_duration_formatted": "1.94s",
  "load_duration": 1806011800,
  "load_duration_formatted": "1.81s",
  "prompt_eval_count": 9,
  "isSuccessful": true,
  "statusCode": 200,
  "reasonPhrase": "OK"
}
```

### On failure:
```json
{
  "model": null,
  "embeddings": null,
  "isSuccessful": false,
  "statusCode": 501,
  "reasonPhrase": "Not Implemented",
  "errorMessage": "This server does not support embeddings. Start it with \u0060--embeddings\u0060"
}
```
