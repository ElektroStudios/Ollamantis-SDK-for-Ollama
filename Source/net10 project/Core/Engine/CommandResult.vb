
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System
Imports System.ComponentModel
Imports System.Text.Json.Serialization

#End Region

Namespace Core

    ''' <summary>
    ''' Represents the result of a command execution through the Ollama CLI app.
    ''' </summary>
    Public NotInheritable Class CliCommandResult : Inherits JsonObjectBaseImmutable

#Region " Properties "

        ''' <summary>
        ''' Gets a <see cref="Boolean"/> value indicating whether the Ollama CLI process ran and terminated its execution lifecycle successfully.
        ''' <para></para>
        ''' This value does not reflect whether the command's intended operation succeeded.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.Never)>
        <JsonPropertyName("success")>
        <DisplayName("Success")>
        <Description("A boolean value indicating whether the Ollama CLI process ran and terminated its execution lifecycle successfully. This value does not reflect whether the command's intended operation succeeded.")>
        Public ReadOnly Property Success As Boolean

        ''' <summary>
        ''' Gets the termination status code returned by the Ollama CLI process. A value of zero indicate success.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("exitCode")>
        <DisplayName("Exit Code")>
        <Description("The termination status code returned by the Ollama CLI process. A value of zero indicate success.")>
        Public ReadOnly Property ExitCode As Integer?

        ''' <summary>
        ''' Gets the standard output stream text captured from the command execution.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("standardOutput")>
        <DisplayName("Standard Output")>
        <Description("The standard output stream text captured from the command execution.")>
        Public ReadOnly Property StandardOutput As String

        ''' <summary>
        ''' Gets the standard error stream text captured from the command execution.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("standardError")>
        <DisplayName("Standard Error")>
        <Description("The standard error stream text captured from the command execution.")>
        Public ReadOnly Property StandardError As String

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="CliCommandResult"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="CliCommandResult"/> class.
        ''' </summary>
        ''' 
        ''' <param name="success">
        ''' A<see cref="Boolean"/> value indicating whether the Ollama CLI process ran and terminated its execution lifecycle successfully.
        ''' <para></para>
        ''' This value does not reflect whether the command's intended operation succeeded.
        ''' </param>
        ''' 
        ''' <param name="exitCode">
        ''' The termination status code returned by the Ollama CLI process. A value of zero indicate success.
        ''' </param>
        ''' 
        ''' <param name="standardOutput">
        ''' The standard output stream text captured from the command execution.
        ''' </param>
        ''' 
        ''' <param name="standardError">
        ''' The standard error stream text captured from the command execution.
        ''' </param>
        Public Sub New(success As Boolean,
                       exitCode As Integer?,
                       standardOutput As String,
                       standardError As String)

            Me.Success = success
            Me.ExitCode = exitCode
            Me.StandardOutput = standardOutput
            Me.StandardError = standardError
        End Sub

#End Region

    End Class

End Namespace