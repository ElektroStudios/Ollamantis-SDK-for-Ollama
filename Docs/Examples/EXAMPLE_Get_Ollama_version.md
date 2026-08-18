# 🏷️ Get Ollama server version

Retrieves the current version of the Ollama server.

- **Ollama Endpoint:** `GET /api/version`
- **Method:** `OllamaClient.Management.GetOllamaVersionAsync` / `GetOllamaVersion`
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

### On success:
```json
{
  "status": null,
  "isSuccessful": true,
  "statusCode": 200
}

```

### On failure:
```json
{
  "status": null,
  "isSuccessful": false,
  "statusCode": 500,
  "reasonPhrase": "Internal Server Error",
  "errorMessage": "open C:\\Users\\Administrador\\.ollama\\models\\manifests\\registry.ollama.ai\\namespace\\mymodel\\3b: El sistema no puede encontrar la ruta especificada."
}
```