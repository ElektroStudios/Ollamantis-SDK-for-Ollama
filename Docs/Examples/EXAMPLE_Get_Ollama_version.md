# 🏷️ Get Ollama server version

Retrieves the current version of the Ollama server.

- **Ollama Endpoint:** `GET /api/version`
- **Method:** `GetOllamaVersionAsync` / `GetOllamaVersion`
---

## 💻 Code Example

### C#
```csharp
using (OllamaClient client = new OllamaClient()) {

    OllamaVersionResponse versionResponse = 
        await client.Management.GetOllamaVersionAsync(CancellationToken.None);

    string responseJson = versionResponse.ToString(writeIndented: true);
    Console.WriteLine(responseJson);
    
}
```

### VB.NET
```vb
Using client As New OllamaClient()

    Dim versionResponse As OllamaVersionResponse =
        Await client.Management.GetOllamaVersionAsync(CancellationToken.None)

    Dim responseJson As String = versionResponse.ToString(writeIndented:=True)
    Console.WriteLine(responseJson)

End Using
```

## 📝 JSON Output

```json
{
  "version": "0.32.14",
  "isSuccessful": true,
  "statusCode": 200,
  "reasonPhrase": "OK"
}
```
