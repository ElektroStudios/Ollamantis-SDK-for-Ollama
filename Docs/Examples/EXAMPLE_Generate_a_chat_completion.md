# 📝 Generate a chat completion

Generates a chat completion using a specified model.

- **Ollama Endpoint:** `POST /api/chat`
- **Standard Method:** `OllamaClient.Generation.GenerateChatCompletionAsync` / `GenerateChatCompletion`
- **Streaming Method:** `OllamaClient.Generation.StreamChatCompletionAsync`

---

## 💻 Standard Code Example

### C#
```csharp
using (OllamaClient client = new OllamaClient()) {

    List<ChatMessage> messages = new List<ChatMessage> {
        new ChatMessage {
            Role = RoleOption.User, // Or simply "user" string.
            Content = "Why the color of the sky is blue?."
        }
    };

    GenerationOptions chatOptions = new GenerationOptions {
        MaxTokens = -1
    };

    ChatCompletionRequest chatRequest = new ChatCompletionRequest {
        Model = "deepseek-r1:1.5b",
        Messages = messages,
        Think = ThinkOption.Max, // Or simply "max" string.
        Options = chatOptions
    };

    ChatCompletionResponse chatResponse =
        await client.Generation.GenerateChatCompletionAsync(chatRequest, CancellationToken.None);

    string responseJson = chatResponse.ToString(writeIndented: true);
    Console.WriteLine(responseJson);

}
```

### VB.NET
```vb
Using client As New OllamaClient()

    Dim messages As New List(Of ChatMessage) From {
        New ChatMessage With {
            .Role = RoleOption.User, ' Or simply "user" string.
            .Content = "Why the color of the sky is blue?."
        }
    }

    Dim chatOptions As New GenerationOptions With {
        .MaxTokens = -1
    }

    Dim chatRequest As New ChatCompletionRequest With {
        .Model = "deepseek-r1:1.5b",
        .Messages = messages,
        .Think = ThinkOption.Max, ' Or simply "max" string.
        .Options = chatOptions
    }

    Dim chatResponse As ChatCompletionResponse =
        Await client.Generation.GenerateChatCompletionAsync(chatRequest, CancellationToken.None)

    Dim responseJson As String = chatResponse.ToString(writeIndented:=True)
    Console.WriteLine(responseJson)
End Using
```

## 💻 Streaming Code Example

### C#
```csharp
using (OllamaClient client = new OllamaClient()) {

    List<ChatMessage> messages = new List<ChatMessage> {
        new ChatMessage {
            Role = RoleOption.User, // Or simply "user" string.
            Content = "Why the color of the sky is blue?."
        }
    };

    GenerationOptions chatOptions = new GenerationOptions {
        MaxTokens = -1
    };

    ChatCompletionRequest chatRequest = new ChatCompletionRequest {
        Model = "deepseek-r1:1.5b",
        Messages = messages,
        Think = ThinkOption.Max, // Or simply "max" string.
        Options = chatOptions
    };

    Action<ChatCompletionResponse> onChunkReceived =
        chunk => {
            if (chunk.Message != null) {
                // If the model outputs a reasoning trace, print it in gray.
                if (!string.IsNullOrEmpty(chunk.Message.Thinking)) {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(chunk.Message.Thinking);
                    Console.ResetColor();
                }

                // If the model outputs standard content, print it using the default color.
                if (!string.IsNullOrEmpty(chunk.Message.Content)) {
                    Console.Write(chunk.Message.Content);
                }
            }
        };

    ChatCompletionResponse chatResponse =
        await client.Generation.StreamChatCompletionAsync(chatRequest, onChunkReceived, CancellationToken.None);

    string responseJson = chatResponse.ToString(writeIndented: true);
    Console.WriteLine(responseJson);

}
```

### VB.NET
```vb
Using client As New OllamaClient()

    Dim messages As New List(Of ChatMessage) From {
        New ChatMessage With {
            .Role = RoleOption.User, ' Or simply "user" string.
            .Content = "Why the color of the sky is blue?."
        }
    }

    Dim chatOptions As New GenerationOptions With {
        .MaxTokens = -1
    }

    Dim chatRequest As New ChatCompletionRequest With {
        .Model = "deepseek-r1:1.5b",
        .Messages = messages,
        .Think = ThinkOption.Max, ' Or simply "max" string.
        .Options = chatOptions
    }

    Dim onChunkReceived As Action(Of ChatCompletionResponse) =
        Sub(chunk)
            If chunk.Message IsNot Nothing Then
                ' If the model outputs a reasoning trace, print it in gray.
                If Not String.IsNullOrEmpty(chunk.Message.Thinking) Then
                    Console.ForegroundColor = ConsoleColor.DarkGray
                    Console.Write(chunk.Message.Thinking)
                    Console.ResetColor()
                End If

                ' If the model outputs standard content, print it using the default color.
                If Not String.IsNullOrEmpty(chunk.Message.Content) Then
                    Console.Write(chunk.Message.Content)
                End If
            End If
        End Sub

    Dim chatResponse As ChatCompletionResponse =
        Await client.Generation.StreamChatCompletionAsync(chatRequest, onChunkReceived, CancellationToken.None)

    Dim responseJson As String = chatResponse.ToString(writeIndented:=True)
    Console.WriteLine(responseJson)
End Using
```

## 📝 Intermediate Console Output (The Thinking Output For Streaming Code Example):
```
Alright, so I'm trying to understand why the sky is blue. I've heard it's something to do with the sun, but I'm not entirely sure how that works. Let me think about this step by step.

First, I know that when the sun is up, the light is red or orange because it's reflecting off the ground. That makes sense because the sun is actually white, but the sky appears orange or red. But why does the sky turn blue at night?

I've read somewhere that the Earth's atmosphere is transparent to red and orange light, which is why we don't see those colors in the sky. But blue is almost invisible to humans, right? So, how does the blue get into the air?

I remember something about absorption and scattering. When sunlight passes through the atmosphere, the blue light, which has a shorter wavelength, gets scattered more than other colors. But wait, the sun emits blue light in the morning and evening, I think. So, if the sun is up in the morning, the blue light is already in the atmosphere, and when we look up, we see the blue light as the sky. But what about when the sun is down? The blue light would have been absorbed by the atmosphere, right?

I've heard about Rayleigh scattering. I think it's when shorter wavelength light is scattered more than longer wavelength light. So, blue light, being shorter, is scattered more, and that's why the sky is blue. That makes sense. But why does this happen?

Maybe it's because the atmosphere doesn't reflect much of blue light. It's absorbed or scattered. So, the sky is blue because the sunlight has mostly scattered blue light away, leaving it visible. But how much of that happens?

I also remember something about the atmosphere's composition. It's mostly nitrogen and oxygen, with trace amounts of other gases. The gases that scatter blue light are the ones with longer wavelengths. I think those are things like water vapor and atmospheric ozone. So, blue light is scattered by these gases, making the sky appear blue.

I also think about the UV radiation. The sun emits a lot of UV, which is blue and violet. Do they escape the atmosphere? I'm not sure. Maybe they get absorbed or scattered. I think they do get absorbed by the atmosphere, which would make the sky appear a different color.

Wait, I've also heard about the cosmic microwave background and the different colors we see. Maybe the blue is due to the scattering of different particles in space, but that's more about the overall sky color.

I should probably look up some diagrams or scientific explanations to solidify my understanding. Maybe there are animations or equations that explain how blue light is scattered and how it appears in the sky. But for now, I think I have a basic grasp of why the sky is blue, mostly due to Rayleigh scattering and the atmosphere's composition. The sun emits blue light in the morning and evening, which is either absorbed or scattered away, allowing us to see it as blue in the sky.
```

## 📝 JSON Output

### On success:
```json
{
  "model": "deepseek-r1:1.5b",
  "message": {
    "role": "assistant",
    "content": "\n\nThe color of the sky, often referred to as the \u0022blue color of the night,\u0022 is a result of a combination of factors related to Earth\u0027s atmosphere and the Sun. Here are the key reasons why the sky appears blue:\n\n1. **Turbulence in the Air**: On clear days, the Earth\u0027s atmosphere is calm and stable, and the sky doesn\u0027t have much turbulence. However, on cloudy days, particles like dust, water droplets, and tiny water vapor bubbles in the air cause the light passing through the atmosphere to scatter in all directions. This scattering of light is known as Rayleigh scattering.\n\n2. **Rayleigh Scattering**: When sunlight enters the atmosphere and reaches Earth, it travels long distances before exiting into space. Because the atmosphere is transparent to most colors of light, much of the blue light is scattered back towards us. This scattering, caused by the tiny particles in the air and the Sun\u0027s intense radiation, results in the scattering of blue light more than any other color of light.\n\n3. **Rainbow Light**: When it rains, the water droplets in the clouds refract and reflect the red, orange, and blue light that reaches our eyes. This causes the sky to appear a hues of red, orange, and blue. As more rain occurs, the sky darkens and turns black, turning especially red on average days.\n\n4. **Halogey**: In some cases, especially over dark skies, a halo or a dark patch of light can be seen. This is caused by the particles in the atmosphere scattering the sunlight back towards Earth.\n\n5. **Sunlight and Wavelengths**: The Sun emits light across the entire spectrum, including a wide range of colors. Blue light is scattered less by particles in the atmosphere compared to other colors, resulting in the sky being more blue.\n\nThis phenomenon is known as Rayleigh scattering, named after Lord Rayleigh, a British physicist who studied this effect. The blue color of the sky is a beautiful demonstration of how light interacts with matter in the atmosphere."
  },
  "created_at": "2026-08-18T06:35:20.8795095+00:00",
  "created_at_formatted": "Tuesday, August 18, 2026 at 08:35:20",
  "done": true,
  "done_reason": "stop",
  "total_duration": 12479187300,
  "total_duration_formatted": "12.48s",
  "load_duration": 4896394800,
  "load_duration_formatted": "4.9s",
  "prompt_eval_count": 12,
  "prompt_eval_duration": 43360000,
  "prompt_eval_duration_formatted": "43.36ms",
  "eval_count": 417,
  "eval_duration": 7529471000,
  "eval_duration_formatted": "7.53s",
  "tokens_per_second": 55.4,
  "isSuccessful": true,
  "statusCode": 200,
  "reasonPhrase": "OK"
}
```

### On failure:
```json
{
  "model": null,
  "isSuccessful": false,
  "statusCode": 400,
  "reasonPhrase": "Bad Request",
  "errorMessage": "\u0022llama3.2\u0022 does not support thinking"
}
```
