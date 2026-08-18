# 📦 List local models (and show its details)

Retrieves a list of all models currently downloaded and available locally on the Ollama server, including their details such as size, format, and parameter count.

- **Ollama Endpoint:** `GET /api/tags`
- **Method:** `OllamaClient.Management.ListLocalModelsAsync` / `ListLocalModels`
---

## 💻 Code Example

### C#
```csharp
using (OllamaClient client = new OllamaClient()) {

    LocalModelsResponse listResponse = 
        await client.Management.ListLocalModelsAsync(CancellationToken.None);

    string responseJson = listResponse.ToString(writeIndented: true);
    Console.WriteLine(responseJson);
    
}
```

### VB.NET
```vb
Using client As New OllamaClient()

    Dim listResponse As LocalModelsResponse =
        Await client.Management.ListLocalModelsAsync(CancellationToken.None)

    Dim responseJson As String = listResponse.ToString(writeIndented:=True)
    Console.WriteLine(responseJson)

End Using
```

## 📝 JSON Output

```json
{
  "models": [
    {
      "name": "qwen2.5vl:7b",
      "model": "qwen2.5vl:7b",
      "modified_at": "2026-08-13T03:15:55.0485128+02:00",
      "modified_at_formatted": "Thursday, August 13, 2026 at 23:15:55",
      "size": 5969245856,
      "size_formatted": "5,56 GB",
      "digest": "5ced39dfa4bac325dc183dd1e4febaa1c46b3ea28bce48896c8e69c1e79611cc",
      "details": {
        "parent_model": "",
        "format": "gguf",
        "family": "qwen25vl",
        "families": [
          "qwen25vl"
        ],
        "parameter_size": "8.3B",
        "quantization_level": "Q4_K_M"
      }
    },
    {
      "name": "llama3.2-vision:latest",
      "model": "llama3.2-vision:latest",
      "modified_at": "2026-08-13T02:46:07.7284952+02:00",
      "modified_at_formatted": "Thursday, August 13, 2026 at 02:46:07",
      "size": 7816589186,
      "size_formatted": "7,28 GB",
      "digest": "6f2f9757ae97e8a3f8ea33d6adb2b11d93d9a35bef277cd2c0b1b5af8e8d0b1e",
      "details": {
        "parent_model": "",
        "format": "gguf",
        "family": "mllama",
        "families": [
          "mllama"
        ],
        "parameter_size": "10.7B",
        "quantization_level": "Q4_K_M"
      }
    }
  ],
  "isSuccessful": true,
  "statusCode": 200,
  "reasonPhrase": "OK"
}
```
