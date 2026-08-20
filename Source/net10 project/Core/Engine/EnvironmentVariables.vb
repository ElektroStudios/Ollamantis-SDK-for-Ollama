
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Text.Json
Imports System.Reflection
Imports System.Text.Json.Serialization

#If Not NETCOREAPP Then
Imports Ollamantis.Core.ProjectCompatibility
#Else
Imports System.Runtime.Versioning
#End If

Imports Ollamantis.Entities

#End Region

Namespace Core

    ' NOTE for UI devs:
    '   To display the static properties of this class in a PropertyGrid,
    '   an implementation of a custom ICustomTypeDescriptor and a PropertyDescriptor would be required:
    ' https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.icustomtypedescriptor
    ' https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.propertydescriptor

#Region " EnvironmentVariables "

    ''' <summary>
    ''' Provides access to the environment variables used by the Ollama engine.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, use the "<c>ollama serve -h</c>" command, or see the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/cmd/cmd.go">
    ''' cmd.go source-code</see>:
    ''' <para></para>
    ''' switch cmd {
    ''' <code>case runCmd:
    ''' 	appendEnvDocs(cmd, []envconfig.EnvVar{envVars["OLLAMA_EDITOR"], envVars["OLLAMA_HOST"], envVars["OLLAMA_NOHISTORY"]})
    ''' case serveCmd:
    ''' 	appendEnvDocs(cmd, []envconfig.EnvVar{
    ''' 		envVars["OLLAMA_DEBUG"],
    ''' 		envVars["OLLAMA_HOST"],
    ''' 		envVars["OLLAMA_CONTEXT_LENGTH"],
    ''' 		...
    ''' 	})
    ''' default:
    ''' 	appendEnvDocs(cmd, envs)
    ''' }</code>
    ''' </remarks>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <DebuggerStepThrough>
    Public NotInheritable Class EnvironmentVariables

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="EnvironmentVariables"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

#End Region

#Region " Static Properties (Environment Variables) "

        ''' <summary>
        ''' Gets or sets the <c>LLAMA_ARG_FIT</c> environment variable for the current process.
        ''' <para></para>
        ''' Enable llama.cpp automatic fit of unset memory options (default: "on").
        ''' </summary>
        <JsonPropertyName("LLAMA_ARG_FIT")>
        <DisplayName("LLAMA_ARG_FIT")>
        <Description("The LLAMA_ARG_FIT environment variable for the current process. Enable llama.cpp automatic fit of unset memory options (default: ""on"").")>
        Public Shared Property LLAMA_ARG_FIT As String
            Get
                Return Environment.GetEnvironmentVariable("LLAMA_ARG_FIT")
            End Get
            Set(value As String)
                Environment.SetEnvironmentVariable("LLAMA_ARG_FIT", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>LLAMA_ARG_FIT</c> environment variable for the specified target.
        ''' <para></para>
        ''' Enable llama.cpp automatic fit of unset memory options (default: "on").
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("LLAMA_ARG_FIT (Targeted)")>
        <Description("The LLAMA_ARG_FIT environment variable for the specified target. Enable llama.cpp automatic fit of unset memory options (default: ""on"").")>
        Public Shared Property LLAMA_ARG_FIT(target As EnvironmentVariableTarget) As String
            Get
                Return Environment.GetEnvironmentVariable("LLAMA_ARG_FIT", target)
            End Get
            Set(value As String)
                Environment.SetEnvironmentVariable("LLAMA_ARG_FIT", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>LLAMA_ARG_FIT_TARGET</c> environment variable for the current process.
        ''' <para></para>
        ''' Configures the target free VRAM margin per device for llama.cpp fit (in MiB).
        ''' </summary>
        <JsonPropertyName("LLAMA_ARG_FIT_TARGET")>
        <DisplayName("LLAMA_ARG_FIT_TARGET")>
        <Description("The LLAMA_ARG_FIT_TARGET environment variable for the current process. Configures the target free VRAM margin per device for llama.cpp fit (in MiB).")>
        Public Shared Property LLAMA_ARG_FIT_TARGET As Integer?
            Get
                Return EnvironmentVariables.GetIntegerVariable("LLAMA_ARG_FIT_TARGET")
            End Get
            Set(value As Integer?)
                EnvironmentVariables.SetIntegerVariable("LLAMA_ARG_FIT_TARGET", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>LLAMA_ARG_FIT_TARGET</c> environment variable for the specified target.
        ''' <para></para>
        ''' Configures the target free VRAM margin per device for llama.cpp fit (in MiB).
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("LLAMA_ARG_FIT_TARGET (Targeted)")>
        <Description("The LLAMA_ARG_FIT_TARGET environment variable for the specified target. Configures the target free VRAM margin per device for llama.cpp fit (in MiB).")>
        Public Shared Property LLAMA_ARG_FIT_TARGET(target As EnvironmentVariableTarget) As Integer?
            Get
                Return EnvironmentVariables.GetIntegerVariable("LLAMA_ARG_FIT_TARGET", target)
            End Get
            Set(value As Integer?)
                EnvironmentVariables.SetIntegerVariable("LLAMA_ARG_FIT_TARGET", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_MODELS</c> environment variable for the current process.
        ''' <para></para>
        ''' Defines the directory path where the Ollama engine stores its downloaded model binaries (blobs and manifests).
        ''' </summary>
        <JsonPropertyName("OLLAMA_MODELS")>
        <DisplayName("OLLAMA_MODELS")>
        <Description("The OLLAMA_MODELS environment variable for the current process. Defines the directory path where the Ollama engine stores its downloaded model binaries (blobs and manifests).")>
        Public Shared Property OLLAMA_MODELS As String
            Get
                Return Environment.GetEnvironmentVariable("OLLAMA_MODELS")
            End Get
            Set(value As String)
                Environment.SetEnvironmentVariable("OLLAMA_MODELS", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_MODELS</c> environment variable for the specified target.
        ''' <para></para>
        ''' Defines the directory path where the Ollama engine stores its downloaded model binaries (blobs and manifests).
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_MODELS (Targeted)")>
        <Description("The OLLAMA_MODELS environment variable for the specified target. Defines the directory path where the Ollama engine stores its downloaded model binaries (blobs and manifests).")>
        Public Shared Property OLLAMA_MODELS(target As EnvironmentVariableTarget) As String
            Get
                Return Environment.GetEnvironmentVariable("OLLAMA_MODELS", target)
            End Get
            Set(value As String)
                Environment.SetEnvironmentVariable("OLLAMA_MODELS", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_HOST</c> environment variable for the current process.
        ''' <para></para>
        ''' Defines the IP address and port where the Ollama server will listen (default: "127.0.0.1:11434").
        ''' </summary>
        <JsonPropertyName("OLLAMA_HOST")>
        <DisplayName("OLLAMA_HOST")>
        <Description("The OLLAMA_HOST environment variable for the current process. Defines the IP address and port where the Ollama server will listen (default: ""127.0.0.1:11434"").")>
        Public Shared Property OLLAMA_HOST As String
            Get
                Return Environment.GetEnvironmentVariable("OLLAMA_HOST")
            End Get
            Set(value As String)
                Environment.SetEnvironmentVariable("OLLAMA_HOST", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_HOST</c> environment variable for the specified target.
        ''' <para></para>
        ''' Defines the IP address and port where the Ollama server will listen (default: "127.0.0.1:11434").
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_HOST (Targeted)")>
        <Description("The OLLAMA_HOST environment variable for the specified target. Defines the IP address and port where the Ollama server will listen (default: ""127.0.0.1:11434"").")>
        Public Shared Property OLLAMA_HOST(target As EnvironmentVariableTarget) As String
            Get
                Return Environment.GetEnvironmentVariable("OLLAMA_HOST", target)
            End Get
            Set(value As String)
                Environment.SetEnvironmentVariable("OLLAMA_HOST", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_ORIGINS</c> environment variable for the current process.
        ''' <para></para>
        ''' A comma separated list of allowed origins (e.g., "*").
        ''' </summary>
        <JsonPropertyName("OLLAMA_ORIGINS")>
        <DisplayName("OLLAMA_ORIGINS")>
        <Description("The OLLAMA_ORIGINS environment variable for the current process. A comma separated list of allowed origins (e.g., ""*"").")>
        Public Shared Property OLLAMA_ORIGINS As String
            Get
                Return Environment.GetEnvironmentVariable("OLLAMA_ORIGINS")
            End Get
            Set(value As String)
                Environment.SetEnvironmentVariable("OLLAMA_ORIGINS", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_ORIGINS</c> environment variable for the specified target.
        ''' <para></para>
        ''' A comma separated list of allowed origins (e.g., "*").
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_ORIGINS (Targeted)")>
        <Description("The OLLAMA_ORIGINS environment variable for the specified target. A comma separated list of allowed origins (e.g., ""*"").")>
        Public Shared Property OLLAMA_ORIGINS(target As EnvironmentVariableTarget) As String
            Get
                Return Environment.GetEnvironmentVariable("OLLAMA_ORIGINS", target)
            End Get
            Set(value As String)
                Environment.SetEnvironmentVariable("OLLAMA_ORIGINS", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_FLASH_ATTENTION</c> environment variable for the current process.
        ''' <para></para>
        ''' Enables Flash Attention to reduce VRAM usage and speed up token generation (e.g., "1").
        ''' </summary>
        <JsonPropertyName("OLLAMA_FLASH_ATTENTION")>
        <DisplayName("OLLAMA_FLASH_ATTENTION")>
        <Description("The OLLAMA_FLASH_ATTENTION environment variable for the current process. Enables Flash Attention to reduce VRAM usage and speed up token generation (e.g., ""1"").")>
        Public Shared Property OLLAMA_FLASH_ATTENTION As Boolean?
            Get
                Return EnvironmentVariables.GetBooleanVariable("OLLAMA_FLASH_ATTENTION")
            End Get
            Set(value As Boolean?)
                EnvironmentVariables.SetBooleanVariable("OLLAMA_FLASH_ATTENTION", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_FLASH_ATTENTION</c> environment variable for the specified target.
        ''' <para></para>
        ''' Enables Flash Attention to reduce VRAM usage and speed up token generation (e.g., "1").
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_FLASH_ATTENTION (Targeted)")>
        <Description("The OLLAMA_FLASH_ATTENTION environment variable for the specified target. Enables Flash Attention to reduce VRAM usage and speed up token generation (e.g., ""1"").")>
        Public Shared Property OLLAMA_FLASH_ATTENTION(target As EnvironmentVariableTarget) As Boolean?
            Get
                Return EnvironmentVariables.GetBooleanVariable("OLLAMA_FLASH_ATTENTION", target)
            End Get
            Set(value As Boolean?)
                EnvironmentVariables.SetBooleanVariable("OLLAMA_FLASH_ATTENTION", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_KV_CACHE_TYPE</c> environment variable for the current process.
        ''' <para></para>
        ''' Specifies the quantization type for the Key-Value (K/V) cache (default: "f16").
        ''' </summary>
        <JsonPropertyName("OLLAMA_KV_CACHE_TYPE")>
        <DisplayName("OLLAMA_KV_CACHE_TYPE")>
        <Description("The OLLAMA_KV_CACHE_TYPE environment variable for the current process. Specifies the quantization type for the Key-Value (K/V) cache (default: ""f16"").")>
        Public Shared Property OLLAMA_KV_CACHE_TYPE As String
            Get
                Return Environment.GetEnvironmentVariable("OLLAMA_KV_CACHE_TYPE")
            End Get
            Set(value As String)
                Environment.SetEnvironmentVariable("OLLAMA_KV_CACHE_TYPE", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_KV_CACHE_TYPE</c> environment variable for the specified target.
        ''' <para></para>
        ''' Specifies the quantization type for the Key-Value (K/V) cache (default: "f16").
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_KV_CACHE_TYPE (Targeted)")>
        <Description("The OLLAMA_KV_CACHE_TYPE environment variable for the specified target. Specifies the quantization type for the Key-Value (K/V) cache (default: ""f16"").")>
        Public Shared Property OLLAMA_KV_CACHE_TYPE(target As EnvironmentVariableTarget) As String
            Get
                Return Environment.GetEnvironmentVariable("OLLAMA_KV_CACHE_TYPE", target)
            End Get
            Set(value As String)
                Environment.SetEnvironmentVariable("OLLAMA_KV_CACHE_TYPE", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_NUM_PARALLEL</c> environment variable for the current process.
        ''' <para></para>
        ''' Defines the maximum number of concurrent requests the server will process in parallel (e.g., "4").
        ''' </summary>
        <JsonPropertyName("OLLAMA_NUM_PARALLEL")>
        <DisplayName("OLLAMA_NUM_PARALLEL")>
        <Description("The OLLAMA_NUM_PARALLEL environment variable for the current process. Defines the maximum number of concurrent requests the server will process in parallel (e.g., ""4"").")>
        Public Shared Property OLLAMA_NUM_PARALLEL As Integer?
            Get
                Return EnvironmentVariables.GetIntegerVariable("OLLAMA_NUM_PARALLEL")
            End Get
            Set(value As Integer?)
                EnvironmentVariables.SetIntegerVariable("OLLAMA_NUM_PARALLEL", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_NUM_PARALLEL</c> environment variable for the specified target.
        ''' <para></para>
        ''' Defines the maximum number of concurrent requests the server will process in parallel (e.g., "4").
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_NUM_PARALLEL (Targeted)")>
        <Description("The OLLAMA_NUM_PARALLEL environment variable for the specified target. Defines the maximum number of concurrent requests the server will process in parallel (e.g., ""4"").")>
        Public Shared Property OLLAMA_NUM_PARALLEL(target As EnvironmentVariableTarget) As Integer?
            Get
                Return EnvironmentVariables.GetIntegerVariable("OLLAMA_NUM_PARALLEL", target)
            End Get
            Set(value As Integer?)
                EnvironmentVariables.SetIntegerVariable("OLLAMA_NUM_PARALLEL", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_MAX_LOADED_MODELS</c> environment variable for the current process.
        ''' <para></para>
        ''' Defines the maximum number of loaded models simultaneously per GPU (e.g., "2").
        ''' </summary>
        <JsonPropertyName("OLLAMA_MAX_LOADED_MODELS")>
        <DisplayName("OLLAMA_MAX_LOADED_MODELS")>
        <Description("The OLLAMA_MAX_LOADED_MODELS environment variable for the current process. Defines the maximum number of loaded models simultaneously per GPU (e.g., ""2"").")>
        Public Shared Property OLLAMA_MAX_LOADED_MODELS As Integer?
            Get
                Return EnvironmentVariables.GetIntegerVariable("OLLAMA_MAX_LOADED_MODELS")
            End Get
            Set(value As Integer?)
                EnvironmentVariables.SetIntegerVariable("OLLAMA_MAX_LOADED_MODELS", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_MAX_LOADED_MODELS</c> environment variable for the specified target.
        ''' <para></para>
        ''' Defines the maximum number of loaded models simultaneously per GPU (e.g., "2").
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_MAX_LOADED_MODELS (Targeted)")>
        <Description("The OLLAMA_MAX_LOADED_MODELS environment variable for the specified target. Defines the maximum number of loaded models simultaneously per GPU (e.g., ""2"").")>
        Public Shared Property OLLAMA_MAX_LOADED_MODELS(target As EnvironmentVariableTarget) As Integer?
            Get
                Return EnvironmentVariables.GetIntegerVariable("OLLAMA_MAX_LOADED_MODELS", target)
            End Get
            Set(value As Integer?)
                EnvironmentVariables.SetIntegerVariable("OLLAMA_MAX_LOADED_MODELS", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_KEEP_ALIVE</c> environment variable for the current process.
        ''' <para></para>
        ''' Defines the duration that models stay loaded in memory (e.g., "5m").
        ''' </summary>
        <JsonPropertyName("OLLAMA_KEEP_ALIVE")>
        <DisplayName("OLLAMA_KEEP_ALIVE")>
        <Description("The OLLAMA_KEEP_ALIVE environment variable for the current process. Defines the duration that models stay loaded in memory (e.g., ""5m"").")>
        Public Shared Property OLLAMA_KEEP_ALIVE As KeepAliveOption
            Get
                Dim rawValue As String =
                    Environment.GetEnvironmentVariable("OLLAMA_KEEP_ALIVE")

                If String.IsNullOrWhiteSpace(rawValue) Then
                    Return Nothing
                End If

                Try
                    Dim keepaliveOption As KeepAliveOption = rawValue
                    Return keepaliveOption

                Catch ex As Exception
                    ' The value in the environment variable is completely incompatible.
                    ' We swallow the exception to prevent crashes and return Nothing.
                    Return Nothing
                End Try
            End Get
            Set(value As KeepAliveOption)
                Environment.SetEnvironmentVariable("OLLAMA_KEEP_ALIVE", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_KEEP_ALIVE</c> environment variable for the specified target.
        ''' <para></para>
        ''' Defines the duration that models stay loaded in memory (e.g., "5m").
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_KEEP_ALIVE (Targeted)")>
        <Description("The OLLAMA_KEEP_ALIVE environment variable for the specified target. Defines the duration that models stay loaded in memory (e.g., ""5m"").")>
        Public Shared Property OLLAMA_KEEP_ALIVE(target As EnvironmentVariableTarget) As KeepAliveOption
            Get
                Dim rawValue As String =
                    Environment.GetEnvironmentVariable("OLLAMA_KEEP_ALIVE", target)

                If String.IsNullOrWhiteSpace(rawValue) Then
                    Return Nothing
                End If

                Try
                    Dim keepaliveOption As KeepAliveOption = rawValue
                    Return keepaliveOption

                Catch ex As Exception
                    ' The value in the environment variable is completely incompatible.
                    ' We swallow the exception to prevent crashes and return Nothing.
                    Return Nothing
                End Try
            End Get
            Set(value As KeepAliveOption)
                Environment.SetEnvironmentVariable("OLLAMA_KEEP_ALIVE", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_DEBUG</c> environment variable for the current process.
        ''' <para></para>
        ''' Enables verbose debug logging output (e.g., "1").
        ''' </summary>
        <JsonPropertyName("OLLAMA_DEBUG")>
        <DisplayName("OLLAMA_DEBUG")>
        <Description("The OLLAMA_DEBUG environment variable for the current process. Enables verbose debug logging output (e.g., ""1"").")>
        Public Shared Property OLLAMA_DEBUG As Boolean?
            Get
                Return EnvironmentVariables.GetBooleanVariable("OLLAMA_DEBUG")
            End Get
            Set(value As Boolean?)
                EnvironmentVariables.SetBooleanVariable("OLLAMA_DEBUG", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_DEBUG</c> environment variable for the specified target.
        ''' <para></para>
        ''' Enables verbose debug logging output (e.g., "1").
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_DEBUG (Targeted)")>
        <Description("The OLLAMA_DEBUG environment variable for the specified target. Enables verbose debug logging output (e.g., ""1"").")>
        Public Shared Property OLLAMA_DEBUG(target As EnvironmentVariableTarget) As Boolean?
            Get
                Return EnvironmentVariables.GetBooleanVariable("OLLAMA_DEBUG", target)
            End Get
            Set(value As Boolean?)
                EnvironmentVariables.SetBooleanVariable("OLLAMA_DEBUG", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_CONTEXT_LENGTH</c> environment variable for the current process.
        ''' <para></para>
        ''' Defines the context length to use for all models unless otherwise specified (default: 4k/32k/256k based on VRAM).
        ''' </summary>
        <JsonPropertyName("OLLAMA_CONTEXT_LENGTH")>
        <DisplayName("OLLAMA_CONTEXT_LENGTH")>
        <Description("The OLLAMA_CONTEXT_LENGTH environment variable for the current process. Defines the context length to use for all models unless otherwise specified (default: 4k/32k/256k based on VRAM).")>
        Public Shared Property OLLAMA_CONTEXT_LENGTH As String
            Get
                Return Environment.GetEnvironmentVariable("OLLAMA_CONTEXT_LENGTH")
            End Get
            Set(value As String)
                Environment.SetEnvironmentVariable("OLLAMA_CONTEXT_LENGTH", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_CONTEXT_LENGTH</c> environment variable for the specified target.
        ''' <para></para>
        ''' Defines the context length to use for all models unless otherwise specified (default: 4k/32k/256k based on VRAM).
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_CONTEXT_LENGTH (Targeted)")>
        <Description("The OLLAMA_CONTEXT_LENGTH environment variable for the specified target. Defines the context length to use for all models unless otherwise specified (default: 4k/32k/256k based on VRAM).")>
        Public Shared Property OLLAMA_CONTEXT_LENGTH(target As EnvironmentVariableTarget) As String
            Get
                Return Environment.GetEnvironmentVariable("OLLAMA_CONTEXT_LENGTH", target)
            End Get
            Set(value As String)
                Environment.SetEnvironmentVariable("OLLAMA_CONTEXT_LENGTH", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_MAX_TRANSFER_STREAMS</c> environment variable for the current process.
        ''' <para></para>
        ''' Defines the maximum parallel transfer streams for safetensors model pulls/pushes (default: 4).
        ''' </summary>
        <JsonPropertyName("OLLAMA_MAX_TRANSFER_STREAMS")>
        <DisplayName("OLLAMA_MAX_TRANSFER_STREAMS")>
        <Description("The OLLAMA_MAX_TRANSFER_STREAMS environment variable for the current process. Defines the maximum parallel transfer streams for safetensors model pulls/pushes (default: 4).")>
        Public Shared Property OLLAMA_MAX_TRANSFER_STREAMS As Integer?
            Get
                Return EnvironmentVariables.GetIntegerVariable("OLLAMA_MAX_TRANSFER_STREAMS")
            End Get
            Set(value As Integer?)
                EnvironmentVariables.SetIntegerVariable("OLLAMA_MAX_TRANSFER_STREAMS", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_MAX_TRANSFER_STREAMS</c> environment variable for the specified target.
        ''' <para></para>
        ''' Defines the maximum parallel transfer streams for safetensors model pulls/pushes (default: 4).
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_MAX_TRANSFER_STREAMS (Targeted)")>
        <Description("The OLLAMA_MAX_TRANSFER_STREAMS environment variable for the specified target. Defines the maximum parallel transfer streams for safetensors model pulls/pushes (default: 4).")>
        Public Shared Property OLLAMA_MAX_TRANSFER_STREAMS(target As EnvironmentVariableTarget) As Integer?
            Get
                Return EnvironmentVariables.GetIntegerVariable("OLLAMA_MAX_TRANSFER_STREAMS", target)
            End Get
            Set(value As Integer?)
                EnvironmentVariables.SetIntegerVariable("OLLAMA_MAX_TRANSFER_STREAMS", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_MAX_QUEUE</c> environment variable for the current process.
        ''' <para></para>
        ''' Defines the maximum number of incoming server requests that can be queued before rejecting new ones.
        ''' </summary>
        <JsonPropertyName("OLLAMA_MAX_QUEUE")>
        <DisplayName("OLLAMA_MAX_QUEUE")>
        <Description("The OLLAMA_MAX_QUEUE environment variable for the current process. Defines the maximum number of incoming server requests that can be queued before rejecting new ones.")>
        Public Shared Property OLLAMA_MAX_QUEUE As Integer?
            Get
                Return EnvironmentVariables.GetIntegerVariable("OLLAMA_MAX_QUEUE")
            End Get
            Set(value As Integer?)
                EnvironmentVariables.SetIntegerVariable("OLLAMA_MAX_QUEUE", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_MAX_QUEUE</c> environment variable for the specified target.
        ''' <para></para>
        ''' Defines the maximum number of incoming server requests that can be queued before rejecting new ones.
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_MAX_QUEUE (Targeted)")>
        <Description("The OLLAMA_MAX_QUEUE environment variable for the specified target. Defines the maximum number of incoming server requests that can be queued before rejecting new ones.")>
        Public Shared Property OLLAMA_MAX_QUEUE(target As EnvironmentVariableTarget) As Integer?
            Get
                Return EnvironmentVariables.GetIntegerVariable("OLLAMA_MAX_QUEUE", target)
            End Get
            Set(value As Integer?)
                EnvironmentVariables.SetIntegerVariable("OLLAMA_MAX_QUEUE", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_NO_CLOUD</c> environment variable for the current process.
        ''' <para></para>
        ''' Disable Ollama cloud features like remote inference and web search (e.g., "1").
        ''' </summary>
        <JsonPropertyName("OLLAMA_NO_CLOUD")>
        <DisplayName("OLLAMA_NO_CLOUD")>
        <Description("The OLLAMA_NO_CLOUD environment variable for the current process. Disable Ollama cloud features like remote inference and web search (e.g., ""1"").")>
        Public Shared Property OLLAMA_NO_CLOUD As Boolean?
            Get
                Return EnvironmentVariables.GetBooleanVariable("OLLAMA_NO_CLOUD")
            End Get
            Set(value As Boolean?)
                EnvironmentVariables.SetBooleanVariable("OLLAMA_NO_CLOUD", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_NO_CLOUD</c> environment variable for the specified target.
        ''' <para></para>
        ''' Disable Ollama cloud features like remote inference and web search (e.g., "1").
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_NO_CLOUD (Targeted)")>
        <Description("The OLLAMA_NO_CLOUD environment variable for the specified target. Disable Ollama cloud features like remote inference and web search (e.g., ""1"").")>
        Public Shared Property OLLAMA_NO_CLOUD(target As EnvironmentVariableTarget) As Boolean?
            Get
                Return EnvironmentVariables.GetBooleanVariable("OLLAMA_NO_CLOUD", target)
            End Get
            Set(value As Boolean?)
                EnvironmentVariables.SetBooleanVariable("OLLAMA_NO_CLOUD", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_NOPRUNE</c> environment variable for the current process.
        ''' <para></para>
        ''' Instructs the engine not to prune model blobs on startup (e.g., "1").
        ''' </summary>
        <JsonPropertyName("OLLAMA_NOPRUNE")>
        <DisplayName("OLLAMA_NOPRUNE")>
        <Description("The OLLAMA_NOPRUNE environment variable for the current process. Instructs the engine not to prune model blobs on startup (e.g., ""1"").")>
        Public Shared Property OLLAMA_NOPRUNE As Boolean?
            Get
                Return EnvironmentVariables.GetBooleanVariable("OLLAMA_NOPRUNE")
            End Get
            Set(value As Boolean?)
                EnvironmentVariables.SetBooleanVariable("OLLAMA_NOPRUNE", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_NOPRUNE</c> environment variable for the specified target.
        ''' <para></para>
        ''' Instructs the engine not to prune model blobs on startup (e.g., "1").
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_NOPRUNE (Targeted)")>
        <Description("The OLLAMA_NOPRUNE environment variable for the specified target. Instructs the engine not to prune model blobs on startup (e.g., ""1"").")>
        Public Shared Property OLLAMA_NOPRUNE(target As EnvironmentVariableTarget) As Boolean?
            Get
                Return EnvironmentVariables.GetBooleanVariable("OLLAMA_NOPRUNE", target)
            End Get
            Set(value As Boolean?)
                EnvironmentVariables.SetBooleanVariable("OLLAMA_NOPRUNE", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_SCHED_SPREAD</c> environment variable for the current process.
        ''' <para></para>
        ''' Always schedule model across all available GPUs (e.g., "1").
        ''' </summary>
        <JsonPropertyName("OLLAMA_SCHED_SPREAD")>
        <DisplayName("OLLAMA_SCHED_SPREAD")>
        <Description("The OLLAMA_SCHED_SPREAD environment variable for the current process. Always schedule model across all available GPUs (e.g., ""1"").")>
        Public Shared Property OLLAMA_SCHED_SPREAD As Boolean?
            Get
                Return EnvironmentVariables.GetBooleanVariable("OLLAMA_SCHED_SPREAD")
            End Get
            Set(value As Boolean?)
                EnvironmentVariables.SetBooleanVariable("OLLAMA_SCHED_SPREAD", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_SCHED_SPREAD</c> environment variable for the specified target.
        ''' <para></para>
        ''' Always schedule model across all available GPUs (e.g., "1").
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_SCHED_SPREAD (Targeted)")>
        <Description("The OLLAMA_SCHED_SPREAD environment variable for the specified target. Always schedule model across all available GPUs (e.g., ""1"").")>
        Public Shared Property OLLAMA_SCHED_SPREAD(target As EnvironmentVariableTarget) As Boolean?
            Get
                Return EnvironmentVariables.GetBooleanVariable("OLLAMA_SCHED_SPREAD", target)
            End Get
            Set(value As Boolean?)
                EnvironmentVariables.SetBooleanVariable("OLLAMA_SCHED_SPREAD", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_LLM_LIBRARY</c> environment variable for the current process.
        ''' <para></para>
        ''' Forces the use of a specific LLM backend execution library (e.g., "cpu", "cuda", "rocm", "metal").
        ''' </summary>
        <JsonPropertyName("OLLAMA_LLM_LIBRARY")>
        <DisplayName("OLLAMA_LLM_LIBRARY")>
        <Description("The OLLAMA_LLM_LIBRARY environment variable for the current process. Forces the use of a specific LLM backend execution library (e.g., ""cpu"", ""cuda"", ""rocm"", ""metal"").")>
        Public Shared Property OLLAMA_LLM_LIBRARY As String
            Get
                Return Environment.GetEnvironmentVariable("OLLAMA_LLM_LIBRARY")
            End Get
            Set(value As String)
                Environment.SetEnvironmentVariable("OLLAMA_LLM_LIBRARY", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_LLM_LIBRARY</c> environment variable for the specified target.
        ''' <para></para>
        ''' Forces the use of a specific LLM backend execution library (e.g., "cpu", "cuda", "rocm", "metal").
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_LLM_LIBRARY (Targeted)")>
        <Description("The OLLAMA_LLM_LIBRARY environment variable for the specified target. Forces the use of a specific LLM backend execution library (e.g., ""cpu"", ""cuda"", ""rocm"", ""metal"").")>
        Public Shared Property OLLAMA_LLM_LIBRARY(target As EnvironmentVariableTarget) As String
            Get
                Return Environment.GetEnvironmentVariable("OLLAMA_LLM_LIBRARY", target)
            End Get
            Set(value As String)
                Environment.SetEnvironmentVariable("OLLAMA_LLM_LIBRARY", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_GPU_OVERHEAD</c> environment variable for the current process.
        ''' <para></para>
        ''' Defines a portion of VRAM to reserve per GPU (in bytes).
        ''' </summary>
        <JsonPropertyName("OLLAMA_GPU_OVERHEAD")>
        <DisplayName("OLLAMA_GPU_OVERHEAD")>
        <Description("The OLLAMA_GPU_OVERHEAD environment variable for the current process. Defines a portion of VRAM to reserve per GPU (in bytes).")>
        Public Shared Property OLLAMA_GPU_OVERHEAD As Integer?
            Get
                Return EnvironmentVariables.GetIntegerVariable("OLLAMA_GPU_OVERHEAD")
            End Get
            Set(value As Integer?)
                EnvironmentVariables.SetIntegerVariable("OLLAMA_GPU_OVERHEAD", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_GPU_OVERHEAD</c> environment variable for the specified target.
        ''' <para></para>
        ''' Defines a portion of VRAM to reserve per GPU (in bytes).
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_GPU_OVERHEAD (Targeted)")>
        <Description("The OLLAMA_GPU_OVERHEAD environment variable for the specified target. Defines a portion of VRAM to reserve per GPU (in bytes).")>
        Public Shared Property OLLAMA_GPU_OVERHEAD(target As EnvironmentVariableTarget) As Integer?
            Get
                Return EnvironmentVariables.GetIntegerVariable("OLLAMA_GPU_OVERHEAD", target)
            End Get
            Set(value As Integer?)
                EnvironmentVariables.SetIntegerVariable("OLLAMA_GPU_OVERHEAD", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_IGPU_ENABLE</c> environment variable for the current process.
        ''' <para></para>
        ''' Enables the use of Integrated GPUs alongside Dedicated GPUs.
        ''' </summary>
        <JsonPropertyName("OLLAMA_IGPU_ENABLE")>
        <DisplayName("OLLAMA_IGPU_ENABLE")>
        <Description("The OLLAMA_IGPU_ENABLE environment variable for the current process. Enables the use of Integrated GPUs alongside Dedicated GPUs.")>
        Public Shared Property OLLAMA_IGPU_ENABLE As Boolean?
            Get
                Return EnvironmentVariables.GetBooleanVariable("OLLAMA_IGPU_ENABLE")
            End Get
            Set(value As Boolean?)
                EnvironmentVariables.SetBooleanVariable("OLLAMA_IGPU_ENABLE", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_IGPU_ENABLE</c> environment variable for the specified target.
        ''' <para></para>
        ''' Enables the use of Integrated GPUs alongside Dedicated GPUs.
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_IGPU_ENABLE (Targeted)")>
        <Description("The OLLAMA_IGPU_ENABLE environment variable for the specified target. Enables the use of Integrated GPUs alongside Dedicated GPUs.")>
        Public Shared Property OLLAMA_IGPU_ENABLE(target As EnvironmentVariableTarget) As Boolean?
            Get
                Return EnvironmentVariables.GetBooleanVariable("OLLAMA_IGPU_ENABLE", target)
            End Get
            Set(value As Boolean?)
                EnvironmentVariables.SetBooleanVariable("OLLAMA_IGPU_ENABLE", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_LOAD_TIMEOUT</c> environment variable for the current process.
        ''' <para></para>
        ''' Defines the maximum time allowed for a model to load into memory (e.g., "5m").
        ''' </summary>
        <JsonPropertyName("OLLAMA_LOAD_TIMEOUT")>
        <DisplayName("OLLAMA_LOAD_TIMEOUT")>
        <Description("The OLLAMA_LOAD_TIMEOUT environment variable for the current process. Defines the maximum time allowed for a model to load into memory (e.g., ""5m"").")>
        Public Shared Property OLLAMA_LOAD_TIMEOUT As KeepAliveOption
            Get
                Return Environment.GetEnvironmentVariable("OLLAMA_LOAD_TIMEOUT")
            End Get
            Set(value As KeepAliveOption)
                Environment.SetEnvironmentVariable("OLLAMA_LOAD_TIMEOUT", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_LOAD_TIMEOUT</c> environment variable for the specified target.
        ''' <para></para>
        ''' Defines the maximum time allowed for a model to load into memory (e.g., "5m").
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_LOAD_TIMEOUT (Targeted)")>
        <Description("The OLLAMA_LOAD_TIMEOUT environment variable for the specified target. Defines the maximum time allowed for a model to load into memory (e.g., ""5m"").")>
        Public Shared Property OLLAMA_LOAD_TIMEOUT(target As EnvironmentVariableTarget) As KeepAliveOption
            Get
                Return Environment.GetEnvironmentVariable("OLLAMA_LOAD_TIMEOUT", target)
            End Get
            Set(value As KeepAliveOption)
                Environment.SetEnvironmentVariable("OLLAMA_LOAD_TIMEOUT", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_EDITOR</c> environment variable for the current process.
        ''' <para></para>
        ''' Defines the default text editor to be used by the Ollama CLI (e.g., "nano", "vim", or "notepad").
        ''' </summary>
        <JsonPropertyName("OLLAMA_EDITOR")>
        <DisplayName("OLLAMA_EDITOR")>
        <Description("The OLLAMA_EDITOR environment variable for the current process. Defines the default text editor to be used by the Ollama CLI (e.g., ""nano"", ""vim"", or ""notepad"")")>
        Public Shared Property OLLAMA_EDITOR As String
            Get
                Return Environment.GetEnvironmentVariable("OLLAMA_EDITOR")
            End Get
            Set(value As String)
                Environment.SetEnvironmentVariable("OLLAMA_EDITOR", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_EDITOR</c> environment variable for the specified target.
        ''' <para></para>
        ''' Defines the default text editor to be used by the Ollama CLI (e.g., "nano", "vim", or "notepad").
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_EDITOR (Targeted)")>
        <Description("The OLLAMA_EDITOR environment variable for the specified target. Defines the default text editor to be used by the Ollama CLI (e.g., ""nano"", ""vim"", or ""notepad"")")>
        Public Shared Property OLLAMA_EDITOR(target As EnvironmentVariableTarget) As String
            Get
                Return Environment.GetEnvironmentVariable("OLLAMA_EDITOR", target)
            End Get
            Set(value As String)
                Environment.SetEnvironmentVariable("OLLAMA_EDITOR", value, target)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_NOHISTORY</c> environment variable for the current process.
        ''' <para></para>
        ''' Disables saving the command and prompt history for the Ollama CLI when set to "1".
        ''' </summary>
        <JsonPropertyName("OLLAMA_NOHISTORY")>
        <DisplayName("OLLAMA_NOHISTORY")>
        <Description("The OLLAMA_NOHISTORY environment variable for the current process. Disables saving the command and prompt history for the Ollama CLI when set to ""1""")>
        Public Shared Property OLLAMA_NOHISTORY As Boolean?
            Get
                Return EnvironmentVariables.GetBooleanVariable("OLLAMA_NOHISTORY")
            End Get
            Set(value As Boolean?)
                EnvironmentVariables.SetBooleanVariable("OLLAMA_NOHISTORY", value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <c>OLLAMA_NOHISTORY</c> environment variable for the specified target.
        ''' <para></para>
        ''' Disables saving the command and prompt history for the Ollama CLI when set to "1".
        ''' </summary>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored or retrieved in Windows.
        ''' </param>
        <SupportedOSPlatform("windows")>
        <DisplayName("OLLAMA_NOHISTORY (Targeted)")>
        <Description("The OLLAMA_NOHISTORY environment variable for the specified target. Disables saving the command and prompt history for the Ollama CLI when set to ""1""")>
        Public Shared Property OLLAMA_NOHISTORY(target As EnvironmentVariableTarget) As Boolean?
            Get
                Return EnvironmentVariables.GetBooleanVariable("OLLAMA_NOHISTORY", target)
            End Get
            Set(value As Boolean?)
                EnvironmentVariables.SetBooleanVariable("OLLAMA_NOHISTORY", value, target)
            End Set
        End Property

#End Region

#Region " Private Methods "

        ''' <summary>
        ''' Retrieves a process-level environment variable and converts its value to a nullable <see cref="Boolean"/>.
        ''' <para></para>
        ''' Evaluates to <see langword="True"/> if strictly "1", <see langword="False"/> if any other value, 
        ''' and <see langword="Nothing"/> if the variable is not set.
        ''' </summary>
        ''' 
        ''' <param name="variableName">
        ''' The name of the environment variable to retrieve.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Nullable(Of Boolean)"/> representing the parsed state.
        ''' </returns>
        Private Shared Function GetBooleanVariable(variableName As String) As Boolean?

            Dim intValue As Integer? = EnvironmentVariables.GetIntegerVariable(variableName)

            Return If(Not intValue.HasValue, Nothing, CType(intValue.Value = 1, Boolean?))
        End Function

        ''' <summary>
        ''' Retrieves an environment variable for the specified target and converts its value to a nullable <see cref="Boolean"/>.
        ''' <para></para>
        ''' Evaluates to <see langword="True"/> if strictly "1", <see langword="False"/> if any other value, 
        ''' and <see langword="Nothing"/> if the variable is not set.
        ''' </summary>
        ''' 
        ''' <param name="variableName">
        ''' The name of the environment variable to retrieve.
        ''' </param>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Nullable(Of Boolean)"/> representing the parsed state.
        ''' </returns>
        <SupportedOSPlatform("windows")>
        Private Shared Function GetBooleanVariable(variableName As String, target As EnvironmentVariableTarget) As Boolean?

            Dim intValue As Integer? = EnvironmentVariables.GetIntegerVariable(variableName, target)

            Return If(Not intValue.HasValue, Nothing, CType(intValue.Value = 1, Boolean?))
        End Function

        ''' <summary>
        ''' Sets or removes a process-level environment variable based on a nullable <see cref="Boolean"/> value.
        ''' <para></para>
        ''' Writes "1" if <see langword="True"/>, "0" if <see langword="False"/>, and removes the variable if <see langword="Nothing"/>.
        ''' </summary>
        ''' 
        ''' <param name="variableName">
        ''' The name of the environment variable to set.
        ''' </param>
        ''' 
        ''' <param name="value">
        ''' The nullable <see cref="Boolean"/> value to apply.
        ''' </param>
        Private Shared Sub SetBooleanVariable(variableName As String, value As Boolean?)

            Dim intValue As Integer? = Nothing

            If value.HasValue Then
                intValue = If(value.Value, 1, 0)
            End If

            EnvironmentVariables.SetIntegerVariable(variableName, intValue)
        End Sub

        ''' <summary>
        ''' Sets or removes an environment variable for the specified target based on a nullable <see cref="Boolean"/> value.
        ''' <para></para>
        ''' Writes "1" if <see langword="True"/>, "0" if <see langword="False"/>, and removes the variable if <see langword="Nothing"/>.
        ''' </summary>
        ''' 
        ''' <param name="variableName">
        ''' The name of the environment variable to set.
        ''' </param>
        ''' 
        ''' <param name="value">
        ''' The nullable <see cref="Boolean"/> value to apply.
        ''' </param>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable will be stored.
        ''' </param>
        <SupportedOSPlatform("windows")>
        Private Shared Sub SetBooleanVariable(variableName As String, value As Boolean?, target As EnvironmentVariableTarget)

            Dim intValue As Integer? = Nothing

            If value.HasValue Then
                intValue = If(value.Value, 1, 0)
            End If

            EnvironmentVariables.SetIntegerVariable(variableName, intValue, target)
        End Sub

        ''' <summary>
        ''' Retrieves a process-level environment variable and converts its value to a nullable <see cref="Integer"/>.
        ''' <para></para>
        ''' Returns <see langword="Nothing"/> if the variable is not set, empty, or cannot be successfully parsed as an integer.
        ''' </summary>
        ''' 
        ''' <param name="variableName">
        ''' The name of the environment variable to retrieve.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Nullable(Of Integer)"/> representing the parsed state.
        ''' </returns>
        Private Shared Function GetIntegerVariable(variableName As String) As Integer?

            Dim rawValue As String = Environment.GetEnvironmentVariable(variableName)

            If String.IsNullOrWhiteSpace(rawValue) Then
                Return Nothing
            End If

            Dim parsedValue As Integer
            If Integer.TryParse(rawValue.Trim(), parsedValue) Then
                Return parsedValue
            End If

            ' If parsing fails (e.g., the value is text instead of a number), safely return Nothing.
            Return Nothing
        End Function

        ''' <summary>
        ''' Retrieves an environment variable for the specified target and converts its value to a nullable <see cref="Integer"/>.
        ''' <para></para>
        ''' Returns <see langword="Nothing"/> if the variable is not set, empty, or cannot be successfully parsed as an integer.
        ''' </summary>
        ''' 
        ''' <param name="variableName">
        ''' The name of the environment variable to retrieve.
        ''' </param>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable is stored.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Nullable(Of Integer)"/> representing the parsed state.
        ''' </returns>
        <SupportedOSPlatform("windows")>
        Private Shared Function GetIntegerVariable(variableName As String, target As EnvironmentVariableTarget) As Integer?

            Dim rawValue As String = Environment.GetEnvironmentVariable(variableName, target)

            If String.IsNullOrWhiteSpace(rawValue) Then
                Return Nothing
            End If

            Dim parsedValue As Integer
            Return If(Integer.TryParse(rawValue.Trim(), parsedValue), parsedValue, DirectCast(Nothing, Integer?))
        End Function

        ''' <summary>
        ''' Sets or removes a process-level environment variable based on a nullable <see cref="Integer"/> value.
        ''' <para></para>
        ''' Removes the variable if <see langword="Nothing"/> is provided.
        ''' </summary>
        ''' 
        ''' <param name="variableName">
        ''' The name of the environment variable to set.
        ''' </param>
        ''' 
        ''' <param name="value">
        ''' The nullable <see cref="Integer"/> value to apply.
        ''' </param>
        Private Shared Sub SetIntegerVariable(variableName As String, value As Integer?)

            If Not value.HasValue Then
                ' Passing Nothing to SetEnvironmentVariable deletes the variable from the process.
                Environment.SetEnvironmentVariable(variableName, Nothing)
            Else
                Environment.SetEnvironmentVariable(variableName, value.Value.ToString())
            End If
        End Sub

        ''' <summary>
        ''' Sets or removes an environment variable for the specified target based on a nullable <see cref="Integer"/> value.
        ''' <para></para>
        ''' Removes the variable if <see langword="Nothing"/> is provided.
        ''' </summary>
        ''' 
        ''' <param name="variableName">
        ''' The name of the environment variable to set.
        ''' </param>
        ''' 
        ''' <param name="value">
        ''' The nullable <see cref="Integer"/> value to apply.
        ''' </param>
        ''' 
        ''' <param name="target">
        ''' The <see cref="EnvironmentVariableTarget"/> location where the environment variable will be stored.
        ''' </param>
        <SupportedOSPlatform("windows")>
        Private Shared Sub SetIntegerVariable(variableName As String, value As Integer?, target As EnvironmentVariableTarget)

            If Not value.HasValue Then
                Environment.SetEnvironmentVariable(variableName, Nothing, target)
            Else
                Environment.SetEnvironmentVariable(variableName, value.Value.ToString(), target)
            End If
        End Sub

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Returns a formatted JSON <see cref="String"/> representing the <see cref="EnvironmentVariables"/> class, 
        ''' containing the current state of all process-level environment variables.
        ''' </summary>
        ''' 
        ''' <param name="writeIndented">
        ''' Optional. A <see cref="Boolean"/> value indicating whether the JSON should use pretty printing, which includes: 
        ''' <para></para>
        ''' <list type="bullet">
        '''     <item><description>Indenting nested JSON tokens</description></item>
        '''     <item><description>Adding new lines</description></item>
        '''     <item><description>Adding white space between property names and values.</description></item>
        ''' </list>
        ''' <para></para>
        ''' By default, the JSON is serialized without any extra white space.
        ''' <para></para>
        ''' Set this value to <see langword="True"/> to use pretty printing; otherwise, <see langword="False"/>.
        ''' <para></para>
        ''' Default value is <see langword="False"/>.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A formatted JSON <see cref="String"/> containing the names and current values of all static properties 
        ''' in <see cref="EnvironmentVariables"/> class.
        ''' </returns>
        Public Overloads Shared Function ToString(Optional writeIndented As Boolean = False) As String

            ' Grab all public static properties of this class using Reflection.
            Dim classType As Type = GetType(EnvironmentVariables)
            Dim properties As PropertyInfo() =
                classType.GetProperties(BindingFlags.Public Or BindingFlags.Static)

            ' Create a dictionary to hold the property names and their current environment values.
            ' Iterate through properties and extract values.
            Dim stateDictionary As New Dictionary(Of String, String)()

            For Each prop As PropertyInfo In properties

                ' Skip parameterized properties (e.g., those requiring EnvironmentVariableTarget).
                ' We only want the default parameterless properties (Process scope).
                If prop.GetIndexParameters().Length = 0 Then

                    Dim rawValue As Object = prop.GetValue(Nothing)
                    Dim stringValue As String = If(rawValue IsNot Nothing, CType(rawValue, String), Nothing)

                    stateDictionary.Add(prop.Name, stringValue)
                End If
            Next

            Dim options As JsonSerializerOptions =
                If(writeIndented,
                   JsonObjectBase.JsonOptionsIndented.Value,
                   JsonObjectBase.JsonOptionsCompact.Value)

            Return JsonSerializer.Serialize(stateDictionary, options)

        End Function

#End Region

    End Class

#End Region

End Namespace
