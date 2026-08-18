# 📦 List running models (and show its details)

Retrieves a list of all models currently loaded in memory, including their details such as size, format, and parameter count.

- **Ollama Endpoint:** `GET /api/ps`
- **Method:** `OllamaClient.Management.ListRunningModelsAsync` / `ListRunningModels`
---

## 💻 Code Example

### C#
```csharp
using (OllamaClient client = new OllamaClient()) {

    RunningModelsResponse listResponse = 
        await client.Management.ListRunningModelsAsync(CancellationToken.None);

    string responseJson = listResponse.ToString(writeIndented: true);
    Console.WriteLine(responseJson);
    
}
```

### VB.NET
```vb
Using client As New OllamaClient()

    Dim listResponse As RunningModelsResponse =
        Await client.Management.ListRunningModelsAsync(CancellationToken.None)

    Dim responseJson As String = listResponse.ToString(writeIndented:=True)
    Console.WriteLine(responseJson)

End Using
```

## 📝 JSON Output

```json
{
  "models": [
    {
      "name": "llama3.2:latest",
      "model": "llama3.2:latest",
      "expires_at": "2026-08-18T06:02:50.485185+02:00",
      "expires_at_formatted": "Tuesday, August 18, 2026 at 06:02:50",
      "size_vram": 2554708622,
      "size_vram_formated": "2.38 GB",
      "size": 2554708622,
      "size_formatted": "2.38 GB",
      "digest": "a80c4f17acd55265feec403c7aef86be0c25983ab279d83f3bcd3abbcb5b8b72",
      "details": {
        "parent_model": "",
        "format": "gguf",
        "family": "llama",
        "families": [
          "llama"
        ],
        "parameter_size": "3.2B",
        "quantization_level": "Q4_K_M"
      }
    }
  ],
  "isSuccessful": true,
  "statusCode": 200,
  "reasonPhrase": "OK"
}
```
