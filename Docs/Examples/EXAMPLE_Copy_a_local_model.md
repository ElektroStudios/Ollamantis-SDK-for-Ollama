# 📋 Copy a local model

Creates a copy of an existing model in your local Ollama storage under a new name.

- **Ollama Endpoint:** `POST /api/copy`
- **Method:** `CopyModelAsync` / `CopyModel`

---

## 💻 Code Example

### C#
```csharp
using (OllamaClient client = new OllamaClient()) {

    CopyModelRequest copyRequest = new CopyModelRequest {
        SourceName      = "llama3.2",
        DestinationName = "llama3.2-backup"
    };

    CopyModelResponse copyResponse = 
        await client.Management.CopyModelAsync(copyRequest, CancellationToken.None);

    string responseJson = copyResponse.ToString(writeIndented: true);
    Console.WriteLine(responseJson);
    
}
```

### VB.NET
```vb
Using client As New OllamaClient()

    Dim copyRequest As New CopyModelRequest With {
        .SourceName      = "llama3.2",
        .DestinationName = "llama3.2-backup"
    }

    Dim copyResponse As CopyModelResponse =
        Await client.Management.CopyModelAsync(copyRequest, CancellationToken.None)

    Dim responseJson As String = copyResponse.ToString(writeIndented:=True)
    Console.WriteLine(responseJson)

End Using
```

## 📝 JSON Output

### On success:
```json
{
  "isSuccessful": true,
  "statusCode": 200,
  "reasonPhrase": "OK"
}
```

### On failure:
```json
{
  "isSuccessful": false,
  "statusCode": 404,
  "reasonPhrase": "Not Found",
  "errorMessage": "model \u0022llama3.2\u0022 not found"
}
```
