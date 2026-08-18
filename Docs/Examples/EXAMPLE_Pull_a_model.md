# 📥 Download (pull) a model from the Ollama remote library

Downloads a model from the Ollama remote library onto your local storage.

- **Ollama Endpoint:** `POST /api/pull`
- **Standard Method:** `OllamaClient.Management.PullModelAsync` / `PullModel`
- **Streaming Method:** `OllamaClient.Management.StreamPullModelAsync`

---

## 💻 Standard Code Example

### C#
```csharp
using (OllamaClient client = new OllamaClient()) {

    PullModelRequest pullRequest = new PullModelRequest {
        Name = "llama3.2"
    };

    PullModelResponse pullResponse = 
        await client.Management.PullModelAsync(pullRequest, CancellationToken.None);

    string responseJson = pullResponse.ToString(writeIndented: true);
    Console.WriteLine(responseJson);
    
}
```

### VB.NET
```vb
Using client As New OllamaClient()

    Dim pullRequest As New PullModelRequest With {
        .Name = "llama3.2"
    }

    Dim pullResponse As PullModelResponse =
        Await client.Management.PullModelAsync(pullRequest, CancellationToken.None)

    Dim responseJson As String = pullResponse.ToString(writeIndented:=True)
    Console.WriteLine(responseJson)

End Using
```

## 💻 Streaming Code Example

### C#
```csharp
using (OllamaClient client = new OllamaClient()) {

    PullModelRequest pullRequest = new PullModelRequest {
        Name = "llama3.2"
    };

    Action<PullModelResponse> onChunkReceived = chunk => {
        // Handle the streamed progress here.
        string output = $"Status: {chunk.Status,-25} | Layer Total: {chunk.TotalSizeFormatted}";
        string paddedOutput = output.PadRight(Console.WindowWidth - 1);

        Console.Write("\r" + paddedOutput);
    };

    PullModelResponse pullResponse = 
        await client.Management.StreamPullModelAsync(pullRequest, onChunkReceived, CancellationToken.None);

    string responseJson = pullResponse.ToString(writeIndented: true);
    Console.WriteLine(responseJson);
}
```

### VB.NET
```vb
Using client As New OllamaClient()

    Dim pullRequest As New PullModelRequest With {
        .Name = "llama3.2"
    }

    Dim onChunkReceived As Action(Of PullModelResponse) =
        Sub(chunk) ' Handle the streamed progress here.
            Dim output As String = $"Status: {chunk.Status,-25} | Layer Total: {chunk.TotalSizeFormatted}"
            Dim paddedOutput As String = output.PadRight(Console.WindowWidth - 1)

            Console.Write(Constants.vbCr & paddedOutput)
        End Sub

    Dim pullResponse As PullModelResponse =
        Await client.Management.StreamPullModelAsync(pullRequest, onChunkReceived, CancellationToken.None)

    Dim responseJson As String = pullResponse.ToString(writeIndented:=True)
    Console.WriteLine(responseJson)
End Using
```

## 📝 Intermediate Console Output (For Streaming Code Example):
```
Status: pulling dde5aa3fc5ff      | Layer Total: 1.88 GB
```

## 📝 JSON Output

### On success:
```json
{
  "status": "success",
  "isSuccessful": true,
  "statusCode": 200,
  "reasonPhrase": "OK"
}
```

### On failure:
```json
{
  "status": null,
  "isSuccessful": false,
  "statusCode": 500,
  "reasonPhrase": "Internal Server Error",
  "errorMessage": "pull model manifest: file does not exist"
}
```
