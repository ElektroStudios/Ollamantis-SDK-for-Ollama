Option Strict On
Option Explicit On
Option Infer Off

Imports System.Net
Imports System.Net.Http
Imports System.Text.Json
Imports System.Threading
Imports Ollamantis.Contracts
Imports Ollamantis.Core
Imports Ollamantis.Entities
Imports Ollamantis.Tests.Mocks
Imports Xunit

Namespace Ollamantis.Tests.Core

    ''' <summary>Verifies the wire contract for the public synchronous and asynchronous endpoints.</summary>
    Public NotInheritable Class EndpointContractTests

        ''' <summary>Verifies the version route, request and typed response.</summary>
        <Theory>
        <InlineData(False)>
        <InlineData(True)>
        Public Async Function GetOllamaVersionUsesTheExpectedHttpContract(useAsync As Boolean) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient("http://ollama.test:11434/proxy/")
                handler.Enqueue("{""version"":""0.13.0""}")
                Dim result As OllamaVersionResponse = If(useAsync,
                                Await client.Management.GetOllamaVersionAsync(CancellationToken.None).ConfigureAwait(False),
                                client.Management.GetOllamaVersion())

                Assert.True(result.IsSuccessful)
                Assert.Equal(200, result.StatusCode)
                Assert.Equal("0.13.0", result.Version)

                Dim sent As (Method As HttpMethod, Url As Uri, MediaType As String, Body As String) = Assert.Single(handler.Requests)
                Assert.Equal(HttpMethod.Get, sent.Method)
                Assert.Equal("/proxy/api/version", sent.Url.AbsolutePath)
                Assert.Null(sent.Body)
            End Using
        End Function

        ''' <summary>Verifies the tags route, request and typed response.</summary>
        <Theory>
        <InlineData(False)>
        <InlineData(True)>
        Public Async Function ListLocalModelsUsesTheExpectedHttpContract(useAsync As Boolean) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient("http://ollama.test:11434/proxy/")
                handler.Enqueue("{""models"":[{""name"":""gemma"",""size"":4000000000}]}")
                Dim result As LocalModelsResponse = If(useAsync,
                                Await client.Management.ListLocalModelsAsync(CancellationToken.None).ConfigureAwait(False),
                                client.Management.ListLocalModels())

                Assert.True(result.IsSuccessful)
                Assert.Equal(200, result.StatusCode)
                Assert.Equal("gemma", Assert.Single(result.Models).Name)

                Dim sent As (Method As HttpMethod, Url As Uri, MediaType As String, Body As String) = Assert.Single(handler.Requests)
                Assert.Equal(HttpMethod.Get, sent.Method)
                Assert.Equal("/proxy/api/tags", sent.Url.AbsolutePath)
                Assert.Null(sent.Body)
            End Using
        End Function

        ''' <summary>Verifies the ps route, request and typed response.</summary>
        <Theory>
        <InlineData(False)>
        <InlineData(True)>
        Public Async Function ListRunningModelsUsesTheExpectedHttpContract(useAsync As Boolean) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient("http://ollama.test:11434/proxy/")
                handler.Enqueue("{""models"":[{""name"":""gemma"",""size"":4000000000}]}")
                Dim result As RunningModelsResponse = If(useAsync,
                                Await client.Management.ListRunningModelsAsync(CancellationToken.None).ConfigureAwait(False),
                                client.Management.ListRunningModels())

                Assert.True(result.IsSuccessful)
                Assert.Equal(200, result.StatusCode)
                Assert.Equal("gemma", Assert.Single(result.Models).Name)

                Dim sent As (Method As HttpMethod, Url As Uri, MediaType As String, Body As String) = Assert.Single(handler.Requests)
                Assert.Equal(HttpMethod.Get, sent.Method)
                Assert.Equal("/proxy/api/ps", sent.Url.AbsolutePath)
                Assert.Null(sent.Body)
            End Using
        End Function

        ''' <summary>Verifies the show route, request and typed response.</summary>
        <Theory>
        <InlineData(False)>
        <InlineData(True)>
        Public Async Function ShowModelUsesTheExpectedHttpContract(useAsync As Boolean) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient("http://ollama.test:11434/proxy/")
                handler.Enqueue("{""capabilities"":[""vision"",""completion""]}")
                Dim request As ShowModelRequest = New ShowModelRequest("gemma")
                Dim result As ShowModelResponse = If(useAsync,
                                Await client.Management.ShowModelAsync(request, CancellationToken.None).ConfigureAwait(False),
                                client.Management.ShowModel(request))

                Assert.True(result.IsSuccessful)
                Assert.Equal(200, result.StatusCode)
                Assert.Equal(New String() {"vision", "completion"}, result.Capabilities)

                Dim sent As (Method As HttpMethod, Url As Uri, MediaType As String, Body As String) = Assert.Single(handler.Requests)
                Assert.Equal(HttpMethod.Post, sent.Method)
                Assert.Equal("/proxy/api/show", sent.Url.AbsolutePath)
                Assert.Equal("application/json", sent.MediaType)
                Using document As JsonDocument = JsonDocument.Parse(sent.Body)
                    Assert.Equal("gemma", document.RootElement.GetProperty("name").GetString())
                End Using
            End Using
        End Function

        ''' <summary>Verifies the copy route, request and typed response.</summary>
        <Theory>
        <InlineData(False)>
        <InlineData(True)>
        Public Async Function CopyModelUsesTheExpectedHttpContract(useAsync As Boolean) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient("http://ollama.test:11434/proxy/")
                handler.Enqueue("", HttpStatusCode.NoContent)
                Dim request As CopyModelRequest = New CopyModelRequest("gemma", "gemma:copy")
                Dim result As CopyModelResponse = If(useAsync,
                                Await client.Management.CopyModelAsync(request, CancellationToken.None).ConfigureAwait(False),
                                client.Management.CopyModel(request))

                Assert.True(result.IsSuccessful)
                Assert.Equal(204, result.StatusCode)

                Dim sent As (Method As HttpMethod, Url As Uri, MediaType As String, Body As String) = Assert.Single(handler.Requests)
                Assert.Equal(HttpMethod.Post, sent.Method)
                Assert.Equal("/proxy/api/copy", sent.Url.AbsolutePath)
                Assert.Equal("application/json", sent.MediaType)
                Using document As JsonDocument = JsonDocument.Parse(sent.Body)
                    Assert.Equal("gemma", document.RootElement.GetProperty("source").GetString())
                    Assert.Equal("gemma:copy", document.RootElement.GetProperty("destination").GetString())
                End Using
            End Using
        End Function

        ''' <summary>Verifies the delete route, request and typed response.</summary>
        <Theory>
        <InlineData(False)>
        <InlineData(True)>
        Public Async Function DeleteModelUsesTheExpectedHttpContract(useAsync As Boolean) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient("http://ollama.test:11434/proxy/")
                handler.Enqueue("", HttpStatusCode.NoContent)
                Dim request As DeleteModelRequest = New DeleteModelRequest("gemma")
                Dim result As DeleteModelResponse = If(useAsync,
                                Await client.Management.DeleteModelAsync(request, CancellationToken.None).ConfigureAwait(False),
                                client.Management.DeleteModel(request))

                Assert.True(result.IsSuccessful)
                Assert.Equal(204, result.StatusCode)

                Dim sent As (Method As HttpMethod, Url As Uri, MediaType As String, Body As String) = Assert.Single(handler.Requests)
                Assert.Equal(HttpMethod.Delete, sent.Method)
                Assert.Equal("/proxy/api/delete", sent.Url.AbsolutePath)
                Assert.Equal("application/json", sent.MediaType)
                Using document As JsonDocument = JsonDocument.Parse(sent.Body)
                    Assert.Equal("gemma", document.RootElement.GetProperty("name").GetString())
                End Using
            End Using
        End Function

        ''' <summary>Verifies the pull route, request and typed response.</summary>
        <Theory>
        <InlineData(False)>
        <InlineData(True)>
        Public Async Function PullModelUsesTheExpectedHttpContract(useAsync As Boolean) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient("http://ollama.test:11434/proxy/")
                handler.Enqueue("{""status"":""success""}")
                Dim request As PullModelRequest = New PullModelRequest("gemma")
                Dim result As PullModelResponse = If(useAsync,
                                Await client.Management.PullModelAsync(request, CancellationToken.None).ConfigureAwait(False),
                                client.Management.PullModel(request))

                Assert.True(result.IsSuccessful)
                Assert.Equal(200, result.StatusCode)
                Assert.Equal("success", result.Status)

                Dim sent As (Method As HttpMethod, Url As Uri, MediaType As String, Body As String) = Assert.Single(handler.Requests)
                Assert.Equal(HttpMethod.Post, sent.Method)
                Assert.Equal("/proxy/api/pull", sent.Url.AbsolutePath)
                Assert.Equal("application/json", sent.MediaType)
                Using document As JsonDocument = JsonDocument.Parse(sent.Body)
                    Assert.Equal("gemma", document.RootElement.GetProperty("name").GetString())
                    Assert.False(document.RootElement.GetProperty("stream").GetBoolean())
                End Using
            End Using
        End Function

        ''' <summary>Verifies the push route, request and typed response.</summary>
        <Theory>
        <InlineData(False)>
        <InlineData(True)>
        Public Async Function PushModelUsesTheExpectedHttpContract(useAsync As Boolean) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient("http://ollama.test:11434/proxy/")
                handler.Enqueue("{""status"":""success""}")
                Dim request As PushModelRequest = New PushModelRequest("gemma")
                Dim result As PushModelResponse = If(useAsync,
                                Await client.Management.PushModelAsync(request, CancellationToken.None).ConfigureAwait(False),
                                client.Management.PushModel(request))

                Assert.True(result.IsSuccessful)
                Assert.Equal(200, result.StatusCode)
                Assert.Equal("success", result.Status)

                Dim sent As (Method As HttpMethod, Url As Uri, MediaType As String, Body As String) = Assert.Single(handler.Requests)
                Assert.Equal(HttpMethod.Post, sent.Method)
                Assert.Equal("/proxy/api/push", sent.Url.AbsolutePath)
                Assert.Equal("application/json", sent.MediaType)
                Using document As JsonDocument = JsonDocument.Parse(sent.Body)
                    Assert.Equal("gemma", document.RootElement.GetProperty("name").GetString())
                    Assert.False(document.RootElement.GetProperty("stream").GetBoolean())
                End Using
            End Using
        End Function

        ''' <summary>Verifies the embed route, request and typed response.</summary>
        <Theory>
        <InlineData(False)>
        <InlineData(True)>
        Public Async Function GenerateEmbeddingsUsesTheExpectedHttpContract(useAsync As Boolean) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient("http://ollama.test:11434/proxy/")
                handler.Enqueue("{""model"":""gemma"",""embeddings"":[[0.25,-0.5],[0.5,1.0]]}")
                Dim request As EmbeddingsRequest = New EmbeddingsRequest With {.Model = "gemma", .Inputs = New String() {"first", "second"}, .Dimensions = 2, .Truncate = False}
                Dim result As EmbeddingsResponse = If(useAsync,
                                Await client.Generation.GenerateEmbeddingsAsync(request, CancellationToken.None).ConfigureAwait(False),
                                client.Generation.GenerateEmbeddings(request))

                Assert.True(result.IsSuccessful)
                Assert.Equal(200, result.StatusCode)
                Assert.Equal(New Double() {0.25R, -0.5R}, result.Embeddings(0))
                Assert.Equal(2, result.Embeddings.Length)

                Dim sent As (Method As HttpMethod, Url As Uri, MediaType As String, Body As String) = Assert.Single(handler.Requests)
                Assert.Equal(HttpMethod.Post, sent.Method)
                Assert.Equal("/proxy/api/embed", sent.Url.AbsolutePath)
                Assert.Equal("application/json", sent.MediaType)
                Using document As JsonDocument = JsonDocument.Parse(sent.Body)
                    Assert.Equal("gemma", document.RootElement.GetProperty("model").GetString())
                    Assert.Equal(2, document.RootElement.GetProperty("input").GetArrayLength())
                    Assert.Equal("first", document.RootElement.GetProperty("input")(0).GetString())
                    Assert.Equal("second", document.RootElement.GetProperty("input")(1).GetString())
                    Assert.Equal(2, document.RootElement.GetProperty("dimensions").GetInt32())
                    Assert.False(document.RootElement.GetProperty("truncate").GetBoolean())
                End Using
            End Using
        End Function

        ''' <summary>Verifies the generate route, request and typed response.</summary>
        <Theory>
        <InlineData(False)>
        <InlineData(True)>
        Public Async Function GenerateCompletionUsesTheExpectedHttpContract(useAsync As Boolean) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient("http://ollama.test:11434/proxy/")
                handler.Enqueue("{""model"":""gemma"",""response"":""Hi"",""done"":true}")
                Dim request As CompletionRequest = New CompletionRequest("gemma", "Hello")
                Dim result As CompletionResponse = If(useAsync,
                                Await client.Generation.GenerateCompletionAsync(request, CancellationToken.None).ConfigureAwait(False),
                                client.Generation.GenerateCompletion(request))

                Assert.True(result.IsSuccessful)
                Assert.Equal(200, result.StatusCode)
                Assert.Equal("Hi", result.Response)

                Dim sent As (Method As HttpMethod, Url As Uri, MediaType As String, Body As String) = Assert.Single(handler.Requests)
                Assert.Equal(HttpMethod.Post, sent.Method)
                Assert.Equal("/proxy/api/generate", sent.Url.AbsolutePath)
                Assert.Equal("application/json", sent.MediaType)
                Using document As JsonDocument = JsonDocument.Parse(sent.Body)
                    Assert.Equal("gemma", document.RootElement.GetProperty("model").GetString())
                    Assert.Equal("Hello", document.RootElement.GetProperty("prompt").GetString())
                    Assert.False(document.RootElement.GetProperty("stream").GetBoolean())
                End Using
            End Using
        End Function

        ''' <summary>Verifies the chat route, request and typed response.</summary>
        <Theory>
        <InlineData(False)>
        <InlineData(True)>
        Public Async Function GenerateChatCompletionUsesTheExpectedHttpContract(useAsync As Boolean) As Task
            Using handler As New MockHttpMessageHandler(),
                  client As OllamaClient = handler.CreateClient("http://ollama.test:11434/proxy/")
                handler.Enqueue("{""model"":""gemma"",""message"":{""role"":""assistant"",""content"":""Hi""},""done"":true}")
                Dim request As ChatCompletionRequest = New ChatCompletionRequest("gemma", New List(Of ChatMessage) From {New ChatMessage("user", "Hello")})
                Dim result As ChatCompletionResponse = If(useAsync,
                                Await client.Generation.GenerateChatCompletionAsync(request, CancellationToken.None).ConfigureAwait(False),
                                client.Generation.GenerateChatCompletion(request))

                Assert.True(result.IsSuccessful)
                Assert.Equal(200, result.StatusCode)
                Assert.Equal("Hi", result.Message.Content)

                Dim sent As (Method As HttpMethod, Url As Uri, MediaType As String, Body As String) = Assert.Single(handler.Requests)
                Assert.Equal(HttpMethod.Post, sent.Method)
                Assert.Equal("/proxy/api/chat", sent.Url.AbsolutePath)
                Assert.Equal("application/json", sent.MediaType)
                Using document As JsonDocument = JsonDocument.Parse(sent.Body)
                    Assert.Equal("gemma", document.RootElement.GetProperty("model").GetString())
                    Assert.False(document.RootElement.GetProperty("stream").GetBoolean())
                    Dim message As JsonElement = Assert.Single(document.RootElement.GetProperty("messages").EnumerateArray())
                    Assert.Equal("user", message.GetProperty("role").GetString())
                    Assert.Equal("Hello", message.GetProperty("content").GetString())
                End Using
            End Using
        End Function

    End Class

End Namespace
