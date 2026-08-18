# 🗑️ Delete a local model

Deletes an existing model and its associated data from your local Ollama storage.

- **Ollama Endpoint:** `DELETE /api/delete`
- **Method:** `DeleteModelAsync` / `DeleteModel`

---

## 💻 Code Example
```csharp
using (OllamaClient client = new OllamaClient()) {

    DeleteModelRequest delRequest = new DeleteModelRequest {
        Name = "llama3.2"
    };

    DeleteModelResponse delResponse = 
        await client.Management.DeleteModelAsync(delRequest, CancellationToken.None);

    string responseJson = delResponse.ToString(writeIndented: true);
    Console.WriteLine(responseJson);
    
}
```

### VB.NET
```vb
Using client As New OllamaClient()

    Dim delRequest As New DeleteModelRequest With {
        .Name = "llama3.2"
    }

    Dim delResponse As DeleteModelResponse =
        Await client.Management.DeleteModelAsync(delRequest, CancellationToken.None)

    Dim responseJson As String = delResponse.ToString(writeIndented:=True)
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
  "errorMessage": "model \u0027llama3.2\u0027 not found"
}
```
