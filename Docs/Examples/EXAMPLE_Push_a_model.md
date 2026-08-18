# 📤 Upload (push) a model to the Ollama remote library

Uploads a local model to the Ollama remote model library. Requires registering on ollama.com and adding your public key first.

- **Ollama Endpoint:** `POST /api/push`
- **Standard Method:** `PushModelAsync` / `PushModel`
- **Streaming Method:** `StreamPushModelAsync`

---

## 💻 Standard Code Example

### C#
```csharp
using (OllamaClient client = new OllamaClient()) {

    PushModelRequest pushRequest = new PushModelRequest {
        Name = "namespace/mymodel:3B"
    };

    PushModelResponse pushResponse = 
        await client.Management.PushModelAsync(pushRequest, CancellationToken.None);

    string responseJson = pushResponse.ToString(writeIndented: true);
    Console.WriteLine(responseJson);
    
}
```

### VB.NET
```vb
Using client As New OllamaClient()

    Dim pushRequest As New PushModelRequest With {
        .Name = "namespace/mymodel:3B"
    }

    Dim pushResponse As PushModelResponse =
        Await client.Management.PushModelAsync(pushRequest, CancellationToken.None)

    Dim responseJson As String = pushResponse.ToString(writeIndented:=True)
    Console.WriteLine(responseJson)

End Using
```

## 💻 Streaming Code Example

### C#
```csharp
using (OllamaClient client = new OllamaClient()) {

    PushModelRequest pushRequest = new PushModelRequest {
        Name = "namespace/mymodel:3B"
    };

    Action<PushModelResponse> onChunkReceived = chunk => {
        // Handle the streamed progress here.
        string output = $"Status: {chunk.Status,-25} | Layer Total: {chunk.TotalSizeFormatted}";
        string paddedOutput = output.PadRight(Console.WindowWidth - 1);

        Console.Write("\r" + paddedOutput);
    };

    PushModelResponse pushResponse = 
        await client.Management.StreamPushModelAsync(pushRequest, onChunkReceived, CancellationToken.None);

    string responseJson = pushResponse.ToString(writeIndented: true);
    Console.WriteLine(responseJson);
}
```

### VB.NET
```vb
Using client As New OllamaClient()

    Dim pushRequest As New PushModelRequest With {
        .Name = "namespace/mymodel:3B"
    }

    Dim onChunkReceived As Action(Of PushModelResponse) =
        Sub(chunk) ' Handle the streamed progress here.
            Dim output As String = $"Status: {chunk.Status,-25} | Layer Total: {chunk.TotalSizeFormatted}"
            Dim paddedOutput As String = output.PadRight(Console.WindowWidth - 1)

            Console.Write(Constants.vbCr & paddedOutput)
        End Sub

    Dim pushResponse As PushModelResponse =
        Await client.Management.StreamPushModelAsync(pushRequest, onChunkReceived, CancellationToken.None)

    Dim responseJson As String = pushResponse.ToString(writeIndented:=True)
    Console.WriteLine(responseJson)
End Using
```

## 📝 Intermediate Console Output (For Streaming Code Example):
```
Status: pushing f4cae5edd1fc      | Layer Total: 1.29 GB
```

## 📝 JSON Output

### On failure:
```json
{
  "status": null,
  "isSuccessful": false,
  "statusCode": 500,
  "reasonPhrase": "Internal Server Error",
  "errorMessage": "open C:\\Users\\Administrator\\.ollama\\models\\manifests\\registry.ollama.ai\\namespace\\mymodel\\3b: The system cannot find the path specified."
}
```
