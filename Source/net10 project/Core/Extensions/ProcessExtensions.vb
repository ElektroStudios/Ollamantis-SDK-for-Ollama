
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Diagnostics
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.Threading.Tasks

#End Region

Namespace Core.Extensions

#Region " ProcessExtensions "

    ''' <summary>
    ''' Provides extension methods for the <see cref="Process"/> class.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Module ProcessExtensions

        ''' <summary>
        ''' Asynchronously waits for the source <see cref="Process"/> to exit.
        ''' <para></para>
        ''' This extension allows passing a <see cref="CancellationToken"/> uniformly in both .NET Framework and .NET Core targets,
        ''' working around the lack of a native overload in .NET Framework.
        ''' </summary>
        ''' 
        ''' <param name="proc">
        ''' The <see cref="Process"/> to wait for.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task"/> representing the asynchronous wait operation.
        ''' </returns>
        <Extension>
        Friend Async Function CompatibleWaitForExitAsync(proc As Process,
                                                         cancellationToken As CancellationToken
                                                        ) As Task

#If Not NETCOREAPP Then
            If proc.HasExited Then
                Return
            End If

            Dim tcs As New TaskCompletionSource(Of Boolean)()

            Dim handler As EventHandler =
                Sub(sender As Object, e As EventArgs)
                    tcs.TrySetResult(True)
                End Sub

            proc.EnableRaisingEvents = True
            AddHandler proc.Exited, handler

            Try
                ' Double-check in case it exited exactly while we were attaching the event.
                If proc.HasExited Then
                    tcs.TrySetResult(True)
                End If

                Using ctr As CancellationTokenRegistration = cancellationToken.Register(Sub() tcs.TrySetCanceled())
                    Await tcs.Task.ConfigureAwait(continueOnCapturedContext:=False)
                End Using
            Finally
                RemoveHandler proc.Exited, handler
            End Try
#Else
            Await proc.WaitForExitAsync(cancellationToken).
                       ConfigureAwait(continueOnCapturedContext:=False)
#End If

        End Function

    End Module

End Namespace

#End Region
