
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Diagnostics
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.Threading.Tasks

#End Region

Namespace Core.Extensions

#Region " StreamReaderExtensions "

    ''' <summary>
    ''' Provides extension methods for the <see cref="StreamReader"/> class.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Module StreamReaderExtensions

        ''' <summary>
        ''' Reads a line of characters asynchronously from the current stream and returns the data as a string.
        ''' <para></para>
        ''' This extension allows passing a <see cref="CancellationToken"/> uniformly in both .NET Framework and .NET Core targets,
        ''' working around the lack of a native overload in .NET Framework.
        ''' </summary>
        ''' 
        ''' <param name="reader">
        ''' The <see cref="StreamReader"/> instance to read from.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of String)"/> that represents the asynchronous operation, 
        ''' containing the next line from the stream.
        ''' </returns>
        <Extension>
        Friend Async Function CompatibleReadLineAsync(reader As StreamReader,
                                                      cancellationToken As CancellationToken
                                                     ) As Task(Of String)

#If Not NETCOREAPP Then
            cancellationToken.ThrowIfCancellationRequested()
            Return Await reader.ReadLineAsync().ConfigureAwait(continueOnCapturedContext:=False)
#Else
            Return Await reader.ReadLineAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext:=False)
#End If
        End Function


        ''' <summary>
        ''' Asynchronously reads all characters from the current position to the end of the stream.
        ''' <para></para>
        ''' This extension allows passing a <see cref="CancellationToken"/> uniformly in both .NET Framework and .NET Core targets,
        ''' working around the lack of a native overload in .NET Framework.
        ''' </summary>
        ''' 
        ''' <param name="reader">
        ''' The <see cref="StreamReader"/> to read from.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of String)"/> representing the asynchronous read operation.
        ''' </returns>
        <Extension>
        Friend Async Function CompatibleReadToEndAsync(reader As StreamReader,
                                                               cancellationToken As CancellationToken
                                                              ) As Task(Of String)

#If Not NETCOREAPP Then

            If cancellationToken.IsCancellationRequested Then
                Throw New OperationCanceledException(cancellationToken)
            End If

            Dim tcs As New TaskCompletionSource(Of Boolean)()

            Using ctr As CancellationTokenRegistration = cancellationToken.Register(Sub() tcs.TrySetCanceled())
                Dim readTask As Task(Of String) = reader.ReadToEndAsync()
                Dim completedTask As Task =
                    Await Task.WhenAny(readTask, tcs.Task).
                               ConfigureAwait(continueOnCapturedContext:=False)

                If completedTask Is tcs.Task Then
                    Throw New OperationCanceledException(cancellationToken)
                End If

                Return Await readTask.ConfigureAwait(continueOnCapturedContext:=False)
            End Using
#Else
            Return Await reader.ReadToEndAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext:=False)
#End If
        End Function


    End Module

#End Region

End Namespace
