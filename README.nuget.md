<!-- Common Project Tags:
ai 
api-client 
artificial-intelligence 
c# 
c-sharp 
chat 
cross-platform 
csharp 
dotnet 
dotnet-core 
dotnetcore 
embeddings 
linux 
llm 
llms 
machine-learning 
macOS 
model-management 
net10 
netcore 
netframework 
networking 
ollama 
ollama-api 
ollama-client 
rest-api 
sdk 
streaming 
tool 
tool-calling 
tools 
vbnet 
vision 
visual-studio 
vs-code 
vscode 
windows 
wrapper 
 -->

# Ollamantis: The .NET SDK for Ollama

![CS-VB_LINES Logo](https://raw.githubusercontent.com/ElektroStudios/Ollamantis-SDK-for-Ollama/main/Images/App.ico)

### A comprehensive .NET client for the Ollama REST API to interact with LLMs.

------------------

## 👋 Introduction

**Ollamantis** is a .NET library designed to integrate Ollama's large language models (LLMs) into your applications.

Whether you need to manage local model registries, generate a text completion, handle multimodal vision tasks, or maintain complex AI conversations, **Ollamantis** abstracts the raw HTTP REST API into a strongly-typed, beautifully architected .NET ecosystem.

## 💡 Motivation

While there are some .NET libraries out there that do a solid job connecting to the API, they often feel like generic HTTP clients bolted onto it, lacking the polish of a truly idiomatic .NET SDK—forcing developers to write boilerplate code for multimodal inputs (like converting images to base64) or manually orchestrating conversation turns.

I built **Ollamantis** because I wanted a plug-and-play, ready-to-use SDK that feels native to the .NET ecosystem. It handles the network plumbing, automatic image conversions, and stateful chat sessions under the hood, delivering a frictionless developer experience right out of the box, so I can focus entirely on building actual applications

##### ⚡ The Real Question
Why wrestle with with your own network and stream-parsing pipeline when all you need is a clean way to run local AI?

## 🤖 Features

- **Model Management Operations:** List local or running models, inspect model details, copy, delete, and perform pulls from the Ollama remote library.
- **Inference Operations:** Support for raw text generation, conversational chat, and vector embeddings generation.
- **Tool Calling:** Define and execute tool-calling (function calling) capabilities in supported models.
- **Chat Sessions:** Features a built-in, thread-safe `ChatSession` manager that automatically tracks conversation history (context) across consecutive interactions.

  ### ✔️ Supported Ollama Endpoints

  * [`/api/chat`](https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-chat-completion)
  * [`/api/copy`](https://github.com/ollama/ollama/blob/main/docs/api.md#copy-a-model)
  * [`/api/delete`](https://github.com/ollama/ollama/blob/main/docs/api.md#delete-a-model)
  * [`/api/embed`](https://github.com/ollama/ollama/blob/main/docs/api.md#generate-embeddings)
  * [`/api/generate`](https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-completion)
  * [`/api/ps`](https://github.com/ollama/ollama/blob/main/docs/api.md#list-running-models)
  * [`/api/pull`](https://github.com/ollama/ollama/blob/main/docs/api.md#pull-a-model)
  * [`/api/push`](https://github.com/ollama/ollama/blob/main/docs/api.md#push-a-model)
  * [`/api/show`](https://github.com/ollama/ollama/blob/main/docs/api.md#show-model-information)
  * [`/api/tags`](https://github.com/ollama/ollama/blob/main/docs/api.md#list-local-models)
  * [`/api/version`](https://github.com/ollama/ollama/blob/main/docs/api.md#version)

  ### ❌ Unsupported Ollama Endpoints

  * [`/api/blobs`](https://github.com/ollama/ollama/blob/main/docs/api.md#push-a-blob)
  * [`/api/create`](https://github.com/ollama/ollama/blob/main/docs/api.md#create-a-model)

  The primary focus of this SDK is **consumption and interaction**—that allows to build smart applications that generate text, analyze images, utilize tools via pre-existing models, etc. These endpoints are excluded (at least for now) as creating models and pushing raw binary blobs to the Ollama remote library are administrative operations that do not fit into application-layer features, and these could require additional work to be maintained in the future.

---

## 💎 What makes Ollamantis unique?

**Ollamantis** is meticulously crafted to offer more than just a direct 1:1 mapping wrapper of REST endpoints. It is built from the ground up as a fully-fledged SDK focused on developer experience, safety, and architectural purity. 

Here is what sets it apart:

### 🦗 It's a f\*cking Mantis!
- **The Apex Predator:** Lean, direct, and highly adapted to the environment, attacking every consumer-facing Ollama endpoint with lethal efficiency while translating your .NET configurations into raw REST requests easily with zero wasted energy.
- **Territorial Dominance:** Fiercely cross-platform. It runs natively across **Windows, Linux, and macOS**.
- **Perfect Mimicry:** Blending naturally into its ecosystem by accurately mirroring the official Ollama API specifications at the time of this release, giving you direct access to its actual features while molting legacy clutter like an old exoskeleton.
- **Surgical Prey Dissection:** Tearing its target apart by enforcing a ruthless anatomical separation of concerns: Network contracts (`Ollamantis.Contracts`), domain entities (`Ollamantis.Entities`), and operational engines (`Ollamantis.Core`) are strictly divided so you always know exactly what is a transport envelope and what is an object in memory.

### 🛠️ Built By and For the .NET Ecosystem
- **Unified Framework Support:** Offered in two distinct builds, allowing the SDK to be consumed natively in either legacy .NET Framework or modern .NET Core targets.
- **Zero External Dependencies:** Relies entirely on the native .NET ecosystem (e.g., `System.Text.Json`). No third-party libraries like `Newtonsoft.Json` are required.
- **Sync & Async Support:** Every single API operation provides both synchronous and asynchronous (`Async`/`Await`) methods, bridging the gap for older codebases and avoiding the absolute requirement of async programming where it isn't desired.
- **UI-Ready Design:** All contract and entity members are heavily decorated with `DisplayName`, `Description`, and `Browsable` attributes among others, ready to be plugged directly into UI controls, like the `PropertyGrid` in Windows Forms.

### 🧠 Developer Experience (DX) First
- **Complete XML Documentation:** Absolutely every public and private class, property, field or method includes surgical XML documentation, complete with Intellisense remarks and ready-to-use code examples written in both C# and VB.NET.
- **Enhanced Debugging:** All contract and entity classes are strictly decorated with the `DebuggerDisplay` attribute. This provides instant, at-a-glance visibility of key object states right in your IDE's watch windows, saving you from drilling down into complex object hierarchies while debugging.
- **Smart Serialization:** All contract and entity classes override the `ToString()` method to output beautifully indented JSON representations, allowing you to print in a console, debug or log the current state of any object instantly without effort.
- **Built-In State Equality:** All entity classes implement `IEquatable(of T)` with equality operators (`=` and `<>`), allowing deep-equality evaluation through their serialized JSON state, so you can instantly compare two different instances to verify if they hold the exact same data without manually writing property-by-property checks.
- **Strongly-Typed Abstractions:** Features dedicated classes like `ImageOption` to handle multimodal visual tasks natively—automatically converting your local files or `System.Drawing` image objects to Base64 without boilerplate code, automatically handling JSON conversions. Other similar classes are: `RoleOption`, `ThinkOption`, `FormatOption` and `KeepAliveOption`.
- **Human-Readable Metrics:** Models exposing Ollama's raw API data—such as nanoseconds, byte sizes, or date-time offsets—include extra string properties that automatically translate these values into human-readable formats for effortless logging or UI display.

---

## 📝 Requirements

- Windows: Requires either .NET Framework 4.8 or [.NET 10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) or higher, depending on the specific build you choose.
- Linux & macOS: Requires [.NET 10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) or higher.
- A running instance of [Ollama](https://ollama.com/download) (locally or remotely accessible).

## 🚀 Getting Started

Choose the installation method that best fits your workflow.

### Option A: NuGet package installation

The easiest and most maintainable way to include **Ollamantis** in your project is through NuGet.

Open your terminal or the Package Manager Console in Visual Studio and run:

```bash
dotnet add package Ollamantis
```

### Option B: Standard Download (Manual Reference)

If you prefer not to use a package manager or need to work in an offline environment, you can download the compiled library directly.

1. Navigate to the [Releases page](https://github.com/ElektroStudios/Ollamantis-SDK-for-Ollama/releases/latest).
2. Download the latest `.zip` targeting either .NET Framework 4.8 or .NET 10, depending on your environment.
3. Extract the contents to your preferred directory.
4. In Visual Studio, right-click on your project's Dependencies (or References), select `Add Project Reference`..., browse to the extracted folder, and select the `Ollamantis.dll` file.

## ⚙️ Usage

### 👌 Examples

#### Generate a text completion using a vision model with C#:
```csharp
using (OllamaClient client = new OllamaClient()) {

    List<ImageOption> images = new List<ImageOption> {
        ImageOption.FromFile(@"C:\image.png")
    };

    GenerationOptions genOptions = new GenerationOptions {
        MaxTokens = -1
    };

    CompletionRequest genRequest = new CompletionRequest {
        Model = "qwen2.5vl:7b",
        System = "You are an expert image analyst.",
        Prompt = "Accurately describe everything visible in the image.",
        Images = images,
        Options = genOptions
    };

    CompletionResponse genResponse =
        await client.Generation.GenerateCompletionAsync(genRequest, CancellationToken.None);

    string responseJson = genResponse.ToString(writeIndented: true);
    Console.WriteLine(responseJson);

}
```

#### Generate a text completion using a vision model with VB.NET:
```vbnet
Using client As New OllamaClient()

    Dim images As New List(Of ImageOption) From {
        ImageOption.FromFile("C:\image.png")
    }

    Dim genOptions As New GenerationOptions With {
        .MaxTokens = -1
    }

    Dim genRequest As New CompletionRequest With {
        .Model = "qwen2.5vl:7b",
        .System = "You are an expert image analyst.",
        .Prompt = "Accurately describe everything visible in the image.",
        .Images = images,
        .Options = genOptions
    }

    Dim genResponse As CompletionResponse =
        Await client.Generation.GenerateCompletionAsync(genRequest, CancellationToken.None)

    Dim responseJson As String = genResponse.ToString(writeIndented:=True)
    Console.WriteLine(responseJson)

End Using
```

#### 📝 JSON Output
```json
{
  "model": "qwen2.5vl:7b",
  "response": "The image depicts a fantasy scene featuring a female warrior in the foreground. She has long, flowing red hair and is dressed in red and gold armor, holding a shield and a sword. Her expression is serious and determined. Surrounding her are skeletal figures, possibly zombies or undead soldiers, who appear to be advancing towards her. The setting is a dark, eerie forest with tall, leafless trees and a dimly lit atmosphere. Blue energy or magical effects are visible in the air, suggesting some form of magical or supernatural activity. The overall mood of the image is tense and foreboding, hinting at a battle or confrontation.",
  "context": [
    151644,
    ...
    13
  ],
  "created_at": "2026-08-18T08:02:53.0773668+00:00",
  "created_at_formatted": "Tuesday, August 18, 2026 at 10:02:53",
  "done": true,
  "done_reason": "stop",
  "total_duration": 19980223800,
  "total_duration_formatted": "19.98s",
  "load_duration": 426285800,
  "load_duration_formatted": "426.29ms",
  "prompt_eval_count": 2723,
  "prompt_eval_duration": 257505999,
  "prompt_eval_duration_formatted": "257.51ms",
  "eval_count": 128,
  "eval_duration": 18947816000,
  "eval_duration_formatted": "18.95s",
  "tokens_per_second": 6.8,
  "isSuccessful": true,
  "statusCode": 200,
  "reasonPhrase": "OK"
}
```

### 📚 Full code example list:

 - [📝 Generate a text completion](Docs/Examples/EXAMPLE_Generate_a_completion.md)
 - [💬 Generate a chat completion](Docs/Examples/EXAMPLE_Generate_a_chat_completion.md)
 - [🔄 Create a chat session](Docs/Examples/EXAMPLE_Create_a_chat_session.md)
 - [📊 Generate vector embeddings](Docs/Examples/EXAMPLE_Generate_embeddings.md)
 - [📥 Download (pull) a model from the Ollama remote library](Docs/Examples/EXAMPLE_Pull_a_model.md)
 - [📥 Upload (push) a model to the Ollama remote library](Docs/Examples/EXAMPLE_Push_a_model.md)
 - [📦 List local models (and show its details)](Docs/Examples/EXAMPLE_List_local_models.md)
 - [📦 List running models (and show its details)](Docs/Examples/EXAMPLE_List_running_models.md)
 - [📦 Show detailed information about a model](Docs/Examples/EXAMPLE_Show_model_details.md)
 - [📋 Copy a local model](Docs/Examples/EXAMPLE_Copy_a_local_model.md)
 - [🗑️ Delete a local model](Docs/Examples/EXAMPLE_Delete_a_local_model.md)
 - [🏷️ Get Ollama server version](Docs/Examples/EXAMPLE_Get_Ollama_version.md)

---

## 🔄 Change Log

Explore the complete list of changes, bug fixes, and improvements across different releases by clicking [here](https://github.com/ElektroStudios/Ollamantis-SDK-for-Ollama/blob/main/Docs/CHANGELOG.md).

## 🏆 Credits

This work relies on the following technologies, libraries or resources: 

 - [.NET Framework](https://dotnet.microsoft.com/en-us/download/dotnet-framework)
 - [.NET Core](https://dotnet.microsoft.com/en-us/download/dotnet-core)
 - [Ollama API](https://docs.ollama.com/api/introduction)
 
## ⚠️ Disclaimer:

This software and its associated repository are provided strictly on an "as is" basis, without warranties of any kind, whether express or implied. This includes, but is not limited to, any implied warranties of merchantability, reliability, or fitness for a particular purpose.

The authors and copyright holders assume no liability for any direct, indirect, incidental, or consequential damages—including data loss or system errors—arising from the use, misuse, or inability to use this software. You are solely responsible for determining the appropriateness of using this tool and assume all associated risks.

Furthermore, this project operates entirely independently. The utilization of any third-party libraries or components within this software does not imply any affiliation with, or endorsement or approval by, their respective original authors.

This software may interact with third-party services, websites, or platforms. It is the user's sole responsibility to ensure that such use complies with the applicable terms of service, laws, and regulations. The authors do not endorse, and are not responsible for, any misuse of this software to violate third-party terms of service or applicable law.

By using this software, you agree to indemnify and hold harmless the authors from any claims, damages, or liabilities arising from your use or misuse of it.

This project is licensed under the **Apache License, Version 2.0**.

## 💪 Contributing

Your contribution is highly appreciated!. If you have any ideas, suggestions, or encounter issues, feel free to open an issue by clicking [here](https://github.com/ElektroStudios/Ollamantis-SDK-for-Ollama/issues/new/choose). 

Your input helps make this Work better for everyone. Thank you for your support! 🚀

## 💰 Beyond Contribution 

This project is distributed for educational purposes without any profit motive. However, if you find value in my efforts and wish to support financially my ongoing work, please consider visiting the main repository page on GitHub to view the full sponsorship section and explore the available financial contribution options.

Your support means the world to me! Thank you for considering it! 👍
