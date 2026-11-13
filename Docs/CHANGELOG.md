# Ollamantis Change Log 📋

## v1.0.2 *(current)* 🆕
 - Fixed chat session retries duplicating messages or leaving null entries in conversation history after a failed turn.
 - Fixed completion and chat requests throwing a null reference exception for a missing request.
 - Fixed streaming completion and chat requests starting an HTTP call when the callback is missing.
 - Fixed streamed chat responses losing tool calls emitted before the final chunk.

## v1.0.1
 - `OllamaClient` class: renamed property `EndpointBase` to `Host`.
 - Fixed exception when calling static method `EnvironmentVariables.ToString()` due wrong `KeepAliveOption` type casting.

## v1.0
Initial Release.