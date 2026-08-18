
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Diagnostics
Imports System.IO
Imports System.Net.Http
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.Threading.Tasks

#End Region

Namespace Core.Extensions

#Region " HttpContentExtensions "

    ''' <summary>
    ''' Provides extension methods for the <see cref="HttpContent"/> class.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Module HttpContentExtensions

        ''' <summary>
        ''' Serialize the HTTP content to a string as an asynchronous operation.
        ''' <para></para>
        ''' This extension allows passing a <see cref="CancellationToken"/> uniformly in both .NET Framework and .NET Core targets,
        ''' working around the lack of a native overload in .NET Framework.
        ''' </summary>
        ''' 
        ''' <param name="content">
        ''' The <see cref="HttpContent"/> instance to read from.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of String)"/> that represents the asynchronous operation.
        ''' The task result contains the string content.
        ''' </returns>
        <Extension>
        Friend Async Function CompatibleReadAsStringAsync(content As HttpContent,
                                                          cancellationToken As CancellationToken
                                                         ) As Task(Of String)

#If Not NETCOREAPP Then
            cancellationToken.ThrowIfCancellationRequested()
            Return Await content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext:=False)
#Else
            Return Await content.ReadAsStringAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext:=False)
#End If
        End Function

        ''' <summary>
        ''' Serialize the HTTP content and return a stream that represents the content as an asynchronous operation.
        ''' <para></para>
        ''' This extension allows passing a <see cref="CancellationToken"/> uniformly in both .NET Framework and .NET Core targets,
        ''' working around the lack of a native overload in .NET Framework.
        ''' </summary>
        ''' 
        ''' <param name="content">
        ''' The <see cref="HttpContent"/> instance to read from.
        ''' </param>
        ''' 
        ''' <param name="cancellationToken">
        ''' A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.
        ''' <para></para>
        ''' This value can be <see cref="CancellationToken.None"/> if cancellation is not required.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="Task(Of String)"/> that represents the asynchronous operation.
        ''' The task result contains the content stream.
        ''' </returns>
        <Extension>
        Friend Async Function CompatibleReadAsStreamAsync(content As HttpContent,
                                                          cancellationToken As CancellationToken
                                                         ) As Task(Of Stream)

#If Not NETCOREAPP Then
            cancellationToken.ThrowIfCancellationRequested()
            Return Await content.ReadAsStreamAsync().ConfigureAwait(continueOnCapturedContext:=False)
#Else
            Return Await content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext:=False)
#End If
        End Function

    End Module

End Namespace

#End Region
