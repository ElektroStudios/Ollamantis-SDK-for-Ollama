# ⚙️ Read and write Ollama environment variables

Demonstrates how to configure process-level Ollama environment variables programmatically, and represent them as formatted JSON.

---

## 💻 Code Example

### C#
```csharp
using System;
using Ollamantis.Core;
using Ollamantis.Entities;

internal static class Program {

    private static void Main() {
        
        // Set some process-level environment variables.
        EnvironmentVariables.OLLAMA_KEEP_ALIVE = KeepAliveOption.FromTimeSpan(TimeSpan.FromMinutes(5));
        EnvironmentVariables.OLLAMA_NUM_PARALLEL = 4;
        EnvironmentVariables.OLLAMA_NO_CLOUD = true;

        // Print the current state of all process-level environment variables.
        Console.WriteLine(EnvironmentVariables.ToString(writeIndented: true));

    }
}
```

### VB.NET
```vb
Imports System
Imports Ollamantis.Core
Imports Ollamantis.Entities

Module Program

    Sub Main()

        ' Set some process-level environment variables.
        EnvironmentVariables.OLLAMA_KEEP_ALIVE = KeepAliveOption.FromTimeSpan(TimeSpan.FromMinutes(5))
        EnvironmentVariables.OLLAMA_NUM_PARALLEL = 4
        EnvironmentVariables.OLLAMA_NO_CLOUD = True

        ' Print the current state of all process-level environment variables.
        Console.WriteLine(EnvironmentVariables.ToString(writeIndented:=True))

    End Sub

End Module
```

## 📝 JSON Output

```json
{
  "LLAMA_ARG_FIT": null,
  "LLAMA_ARG_FIT_TARGET": null,
  "OLLAMA_MODELS": null,
  "OLLAMA_HOST": null,
  "OLLAMA_ORIGINS": null,
  "OLLAMA_FLASH_ATTENTION": null,
  "OLLAMA_KV_CACHE_TYPE": null,
  "OLLAMA_NUM_PARALLEL": "4",
  "OLLAMA_MAX_LOADED_MODELS": null,
  "OLLAMA_KEEP_ALIVE": "300s",
  "OLLAMA_DEBUG": null,
  "OLLAMA_CONTEXT_LENGTH": null,
  "OLLAMA_MAX_TRANSFER_STREAMS": null,
  "OLLAMA_MAX_QUEUE": null,
  "OLLAMA_NO_CLOUD": "1",
  "OLLAMA_NOPRUNE": null,
  "OLLAMA_SCHED_SPREAD": null,
  "OLLAMA_GPU_OVERHEAD": null,
  "OLLAMA_IGPU_ENABLE": null,
  "OLLAMA_LOAD_TIMEOUT": null,
  "OLLAMA_EDITOR": null,
  "OLLAMA_NOHISTORY": null,
  "OLLAMA_LLM_LIBRARY": null
}
```
