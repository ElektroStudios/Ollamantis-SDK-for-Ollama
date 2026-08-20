
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Net.Http
Imports System.Threading
Imports System.Threading.Tasks

Imports Ollamantis.Core.Extensions.ProcessExtensions
Imports Ollamantis.Core.Extensions.StreamReaderExtensions

#End Region

Namespace Core

#Region " OllamaUtil "

    ''' <summary>
    ''' Provides utility methods to interact with the Ollama CLI app and background server.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <DebuggerStepThrough>
    Public NotInheritable Class OllamaUtil

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="OllamaUtil"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Determines whether the Ollama CLI app is available in the current machine.
        ''' </summary>
        ''' 
        ''' <returns>
        ''' A <see cref="Task"/> representing the asynchronous operation. 
        ''' The task result is <see langword="True"/> if the Ollama CLI app is available; 
        ''' otherwise, <see langword="False"/>.
        ''' </returns>
        Public Shared Function IsOllamaCliAvailable() As Boolean

            Return OllamaUtil.IsOllamaCliAvailableAsync(CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Asynchronously determines whether the Ollama CLI app is available in the current machine.
        ''' </summary>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task"/> representing the asynchronous operation. 
        ''' <para></para>
        ''' The task result is <see langword="True"/> if the Ollama CLI app is available; 
        ''' otherwise, <see langword="False"/>.
        ''' </returns>
        Public Shared Async Function IsOllamaCliAvailableAsync(cancellationToken As CancellationToken) As Task(Of Boolean)

            Dim cmdResult As CliCommandResult =
                Await OllamaUtil.RunCommandAsync("--version", cancellationToken).
                                 ConfigureAwait(continueOnCapturedContext:=False)

            Return (cmdResult.Success AndAlso
                    cmdResult.ExitCode.HasValue AndAlso
                    cmdResult.ExitCode.Value = 0)
        End Function

        ''' <summary>
        ''' Determines whether the Ollama server is currently running and responding to HTTP requests 
        ''' in the default local endpoint ("http://127.0.0.1:11434/") using a connection timeout of 5 seconds.
        ''' </summary>
        ''' 
        ''' <returns>
        ''' Returns <see langword="True"/> if the Ollama server is running; otherwise, <see langword="False"/>.
        ''' </returns>
        Public Shared Function IsOllamaServerReachable() As Boolean

            Return OllamaUtil.IsOllamaServerReachableAsync(CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Determines whether the Ollama server is currently running and responding to HTTP requests 
        ''' in the provided endpoint using a connection timeout of 5 seconds.
        ''' </summary>
        ''' 
        ''' <param name="endpointBase">
        ''' The base URL endpoint where the Ollama server is hosted (e.g., <c>"http://127.0.0.1:11434/"</c>).
        ''' </param>
        ''' 
        ''' <returns>
        ''' Returns <see langword="True"/> if the Ollama server is running; otherwise, <see langword="False"/>.
        ''' </returns>
        Public Shared Function IsOllamaServerReachable(endpointBase As String) As Boolean

            Return OllamaUtil.IsOllamaServerReachableAsync(endpointBase, CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Determines whether the Ollama server is currently running and responding to HTTP requests 
        ''' in the provided endpoint using the specified connection timeout.
        ''' </summary>
        ''' 
        ''' <param name="endpointBase">
        ''' The base URL endpoint where the Ollama server is hosted (e.g., <c>"http://127.0.0.1:11434/"</c>).
        ''' </param>
        ''' 
        ''' <param name="timeout">
        ''' A <see cref="TimeSpan"/> representing the maximum time to wait for the server to respond.
        ''' </param>
        ''' 
        ''' <returns>
        ''' Returns <see langword="True"/> if the Ollama server is running; otherwise, <see langword="False"/>.
        ''' </returns>
        Public Shared Function IsOllamaServerReachable(endpointBase As String, timeout As TimeSpan) As Boolean

            Return OllamaUtil.IsOllamaServerReachableAsync(endpointBase, timeout, CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Asynchronously determines whether the Ollama server is currently running and responding to HTTP requests 
        ''' in the default local endpoint ("http://127.0.0.1:11434/") using a connection timeout of 5 seconds.
        ''' </summary>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task"/> representing the asynchronous operation. 
        ''' <para></para>
        ''' The task result is <see langword="True"/> if the Ollama server is running; otherwise, <see langword="False"/>.
        ''' </returns>
        Public Shared Function IsOllamaServerReachableAsync(cancellationToken As CancellationToken) As Task(Of Boolean)

            Return OllamaUtil.IsOllamaServerReachableAsync("http://127.0.0.1:11434/", TimeSpan.FromSeconds(5), cancellationToken)
        End Function

        ''' <summary>
        ''' Asynchronously determines whether the Ollama server is currently running and responding to HTTP requests 
        ''' in the provided endpoint using a connection timeout of 5 seconds.
        ''' </summary>
        ''' 
        ''' <param name="endpointBase">
        ''' The base URL endpoint where the Ollama server is hosted (e.g., <c>"http://127.0.0.1:11434/"</c>).
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task"/> representing the asynchronous operation. 
        ''' <para></para>
        ''' The task result is <see langword="True"/> if the Ollama server is running; otherwise, <see langword="False"/>.
        ''' </returns>
        Public Shared Function IsOllamaServerReachableAsync(endpointBase As String,
                                                            cancellationToken As CancellationToken
                                                           ) As Task(Of Boolean)

            Return OllamaUtil.IsOllamaServerReachableAsync(endpointBase, TimeSpan.FromSeconds(5), cancellationToken)
        End Function

        ''' <summary>
        ''' Asynchronously determines whether the Ollama server is currently running and responding to HTTP requests 
        ''' in the provided endpoint using the specified connection timeout.
        ''' </summary>
        ''' 
        ''' <param name="endpointBase">
        ''' The base URL endpoint where the Ollama server is hosted (e.g., <c>"http://127.0.0.1:11434/"</c>).
        ''' </param>
        ''' 
        ''' <param name="timeout">
        ''' A <see cref="TimeSpan"/> representing the maximum time to wait for the server to respond.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task"/> representing the asynchronous operation. 
        ''' <para></para>
        ''' The task result is <see langword="True"/> if the Ollama server is running; otherwise, <see langword="False"/>.
        ''' </returns>
        Public Shared Async Function IsOllamaServerReachableAsync(endpointBase As String,
                                                                  timeout As TimeSpan,
                                                                  cancellationToken As CancellationToken
                                                                 ) As Task(Of Boolean)

#If Not NETCOREAPP Then
            If String.IsNullOrWhiteSpace(endpointBase) Then
                Throw New ArgumentNullException(NameOf(endpointBase))
            End If
#Else
            ArgumentNullException.ThrowIfNullOrWhiteSpace(endpointBase, NameOf(endpointBase))
#End If

            If timeout <= TimeSpan.Zero Then
                Throw New ArgumentOutOfRangeException(NameOf(timeout), timeout, "The timeout duration must be greater than zero.")
            End If

            Dim isRunning As Boolean = False
            Dim formattedEndpoint As String = endpointBase.TrimEnd("/"c) & "/"

            Try
                Using client As New HttpClient()
                    client.Timeout = timeout

                    ' The base Ollama endpoint returns a 200 OK "Ollama is running" text payload.
                    Dim response As HttpResponseMessage =
                        Await client.GetAsync(formattedEndpoint, cancellationToken).
                                     ConfigureAwait(continueOnCapturedContext:=False)

                    isRunning = response.IsSuccessStatusCode
                End Using

            Catch ex As HttpRequestException
                ' The server is not reachable.
                isRunning = False

            Catch ex As TaskCanceledException
                ' The request timed out or was explicitly canceled.
                isRunning = False

            End Try

            Return isRunning
        End Function

        ''' <summary>
        ''' Runs the Ollama CLI app with the specified command-line arguments, 
        ''' waits for process termination and returns a <see cref="CliCommandResult"/> 
        ''' containing the execution result and the captured output streams.
        ''' </summary>
        ''' 
        ''' <param name="arguments">
        ''' The command-line arguments to pass to the Ollama executable (e.g., <c>"run llama3.2"</c>).
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="CliCommandResult"/> containing the execution result and the output streams.
        ''' </returns>
        Public Shared Function RunCommand(arguments As String) As CliCommandResult

            Return OllamaUtil.RunCommandAsync(arguments, CancellationToken.None).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Asynchronously runs the Ollama CLI app with the specified command-line arguments, 
        ''' waits for process termination and returns a <see cref="CliCommandResult"/> 
        ''' containing the execution result and the captured output streams.
        ''' </summary>
        ''' 
        ''' <param name="arguments">
        ''' The command-line arguments to pass to the Ollama executable (e.g., <c>"run llama3.2"</c>).
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task"/> representing the asynchronous operation. 
        ''' <para></para>
        ''' The task result contains a <see cref="CliCommandResult"/> with the execution result and the output streams.
        ''' </returns>
        Public Shared Async Function RunCommandAsync(arguments As String,
                                                     cancellationToken As CancellationToken
                                                    ) As Task(Of CliCommandResult)

#If Not NETCOREAPP Then
            If String.IsNullOrWhiteSpace(arguments) Then
                Throw New ArgumentNullException(NameOf(arguments))
            End If
#Else
            ArgumentNullException.ThrowIfNullOrWhiteSpace(arguments, NameOf(arguments))
#End If

            Dim processInfo As New ProcessStartInfo With {
                .FileName = "ollama",
                .Arguments = arguments,
                .RedirectStandardOutput = True,
                .RedirectStandardError = True,
                .UseShellExecute = False,
                .CreateNoWindow = True
            }

            Dim success As Boolean = True
            Dim exitCode As Integer? = Nothing
            Dim standardOutput As String = Nothing
            Dim standardError As String = Nothing

            Try
                Using proc As New Process()
                    proc.StartInfo = processInfo
                    proc.Start()

                    Dim outputTask As Task(Of String) = proc.StandardOutput.CompatibleReadToEndAsync(cancellationToken)
                    Dim errorTask As Task(Of String) = proc.StandardError.CompatibleReadToEndAsync(cancellationToken)

                    Await Task.WhenAll(outputTask, errorTask).
                               ConfigureAwait(continueOnCapturedContext:=False)

                    Await proc.CompatibleWaitForExitAsync(cancellationToken).
                               ConfigureAwait(continueOnCapturedContext:=False)

                    standardOutput = If(Not String.IsNullOrEmpty(outputTask.Result), outputTask.Result, Nothing)
                    standardError = If(Not String.IsNullOrEmpty(errorTask.Result), errorTask.Result, Nothing)
                    exitCode = proc.ExitCode
                End Using

            Catch ex As Win32Exception When ex.HResult = -2147467259
                ' Windows: The executable was not found in the system PATH variable.
                standardError = $"Executable 'ollama' was not found. Exception: {ex.Message}"
                success = False

            Catch ex As OperationCanceledException
                standardError = $"The operation was canceled by the user. Exception: {ex.Message}"
                success = False

            Catch ex As Exception
                standardError = $"An unexpected error occurred during process execution. Exception: {ex.Message}"
                success = False
            End Try

            Return New CliCommandResult(success, exitCode, standardOutput, standardError)
        End Function

        ''' <summary>
        ''' Runs the Ollama CLI app with the specified command-line arguments 
        ''' in a fire-and-forget manner without waiting for process termination.
        ''' </summary>
        ''' 
        ''' <param name="arguments">
        ''' The command-line arguments to pass to the Ollama executable (e.g., <c>"serve"</c>).
        ''' </param>
        Public Shared Sub RunCommandNoWait(arguments As String)

#If Not NETCOREAPP Then
            If String.IsNullOrWhiteSpace(arguments) Then
                Throw New ArgumentNullException(NameOf(arguments))
            End If
#Else
            ArgumentNullException.ThrowIfNullOrWhiteSpace(arguments, NameOf(arguments))
#End If

            Dim processInfo As New ProcessStartInfo With {
                .FileName = "ollama",
                .Arguments = arguments,
                .RedirectStandardOutput = False,
                .RedirectStandardError = False,
                .UseShellExecute = False,
                .CreateNoWindow = True
            }

            Using proc As New Process()
                proc.StartInfo = processInfo
                proc.Start()
            End Using
        End Sub

        ''' <summary>
        ''' Forcefully terminates any running Ollama background processes on the current computer.
        ''' </summary>
        ''' 
        ''' <returns>
        ''' A <see cref="Task"/> representing the asynchronous operation.
        ''' <para></para>
        ''' The task result is <see langword="True"/> if Ollama processes were found and at least one of them were killed; 
        ''' otherwise, <see langword="False"/>.
        ''' </returns>
        Public Shared Function KillOllamaProcesses() As Boolean

            Return OllamaUtil.KillOllamaProcessesAsync().GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Asynchronously forcefully terminates any running Ollama background processes on the current computer.
        ''' </summary>
        ''' 
        ''' <returns>
        ''' A <see cref="Task"/> representing the asynchronous operation.
        ''' <para></para>
        ''' The task result is <see langword="True"/> if Ollama processes were found and at least one of them were killed; 
        ''' otherwise, <see langword="False"/>.
        ''' </returns>
        Public Shared Async Function KillOllamaProcessesAsync() As Task(Of Boolean)

            Dim processesKilled As Boolean = False

            Dim parentProcesses As Process() = Process.GetProcessesByName("ollama app")
            For Each proc As Process In parentProcesses
                Try
                    If Not proc.HasExited Then
                        proc.Kill()
                        Await proc.CompatibleWaitForExitAsync(CancellationToken.None).
                                   ConfigureAwait(continueOnCapturedContext:=False)
                        processesKilled = True
                    End If
                Catch ex As Exception
                    ' Suppress exceptions for already dead or restricted processes.
                Finally
                    proc.Dispose()
                End Try
            Next

            Dim activeProcesses As Process() = Process.GetProcessesByName("ollama")
            If activeProcesses.Length > 0 Then
                For Each proc As Process In activeProcesses
                    Try
                        If Not proc.HasExited Then
                            proc.Kill()
                            Await proc.CompatibleWaitForExitAsync(CancellationToken.None).
                                       ConfigureAwait(continueOnCapturedContext:=False)
                            processesKilled = True
                        End If
                    Catch ex As Exception
                        ' Suppress exceptions for already dead or restricted processes.
                    Finally
                        proc.Dispose()
                    End Try
                Next
            End If

            Return processesKilled
        End Function

#End Region

    End Class

#End Region

End Namespace
