#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Text.Json.Serialization
Imports System.Threading

Imports Ollamantis.Contracts

#End Region

Namespace Entities

#Region " KeepAliveOption "

#Disable Warning BC40000 ' Type or member is obsolete (Allow ref structs: Utf8JsonReader)

    ''' <summary>
    ''' Represents the 'keep_alive' option in a <see cref="GenerationRequestBase"/>.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <JsonConverter(GetType(KeepAliveOptionJsonConverter))>
    <DebuggerStepThrough>
    <DebuggerDisplay("Value = {Me.Value}")>
    Public Class KeepAliveOption : Inherits EntityOptionBase

#Enable Warning BC40000 ' Type or member is obsolete (Allow ref structs: Utf8JsonReader)

#Region " Properties "

        ''' <summary>
        ''' Gets the raw string value of this keep-alive option formatted for the Ollama API.
        ''' </summary>
        Public ReadOnly Value As String

#End Region

#Region " Static Fields "

        ''' <summary>
        ''' Represents an infinite keep-alive duration. The model will stay loaded in memory indefinitely.
        ''' </summary>
        Public Shared ReadOnly Infinite As New KeepAliveOption("-1m")

        ''' <summary>
        ''' Represents a zero keep-alive duration. The model will be unloaded from memory immediately after the response.
        ''' </summary>
        Public Shared ReadOnly Zero As New KeepAliveOption("0s")

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="KeepAliveOption"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="KeepAliveOption"/> class with the specified string value.
        ''' </summary>
        ''' 
        ''' <param name="value">
        ''' The string value indicating the duration (e.g., "<c>5m</c>", "<c>1h</c>").
        ''' </param>
        Private Sub New(value As String)

            Me.Value = value
        End Sub

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Creates a new <see cref="KeepAliveOption"/> instance from the specified <see cref="TimeSpan"/> duration.
        ''' </summary>
        ''' 
        ''' <param name="timeSpan">
        ''' The <see cref="TimeSpan"/> duration to convert.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="KeepAliveOption"/> formatted as seconds, 
        ''' or infinite if the value is <see cref="Timeout.InfiniteTimeSpan"/>.
        ''' </returns>
        Public Shared Function FromTimeSpan(timeSpan As TimeSpan) As KeepAliveOption

            If timeSpan = Timeout.InfiniteTimeSpan Then
                Return KeepAliveOption.Infinite
            End If

            ' Format to seconds to maintain maximum compatibility with Ollama's parser.
            Return New KeepAliveOption($"{Math.Round(timeSpan.TotalSeconds)}s")
        End Function

#End Region

#Region " Implicit Operators "

        ''' <summary>
        ''' Performs an implicit conversion from <see cref="String"/> to <see cref="KeepAliveOption"/>.
        ''' </summary>
        ''' 
        ''' <param name="value">
        ''' The <see cref="String"/> duration value to convert (e.g., "<c>5m</c>").
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="KeepAliveOption"/> equivalent to the provided <see cref="String"/> value.
        ''' </returns>
        Public Shared Widening Operator CType(value As String) As KeepAliveOption

            If String.IsNullOrWhiteSpace(value) Then
                Throw New ArgumentException("Keep-alive duration string cannot be null or whitespace.", NameOf(value))
            End If

            Return New KeepAliveOption(value.Trim().ToLowerInvariant())
        End Operator

        ''' <summary>
        ''' Performs an implicit conversion from <see cref="TimeSpan"/> to <see cref="KeepAliveOption"/>.
        ''' </summary>
        ''' 
        ''' <param name="value">
        ''' The <see cref="TimeSpan"/> duration to convert.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="KeepAliveOption"/> formatted as seconds, 
        ''' or infinite if the value is <see cref="Timeout.InfiniteTimeSpan"/>.
        ''' </returns>
        Public Shared Widening Operator CType(value As TimeSpan) As KeepAliveOption

            If value = Timeout.InfiniteTimeSpan Then
                Return KeepAliveOption.Infinite
            End If

            ' Format to seconds to maintain maximum compatibility with Ollama's parser.
            Return New KeepAliveOption($"{Math.Round(value.TotalSeconds)}s")
        End Operator

        ''' <summary>
        ''' Performs an implicit conversion from an <see cref="Integer"/> representing seconds to <see cref="KeepAliveOption"/>.
        ''' </summary>
        ''' 
        ''' <param name="seconds">
        ''' The amount of seconds to convert.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="KeepAliveOption"/> formatted as seconds.
        ''' </returns>
        Public Shared Widening Operator CType(seconds As Integer) As KeepAliveOption

            Return If(seconds < 0, KeepAliveOption.Infinite, New KeepAliveOption($"{seconds}s"))
        End Operator

        ''' <summary>
        ''' Performs an implicit conversion from <see cref="KeepAliveOption"/> to <see cref="String"/>.
        ''' </summary>
        ''' 
        ''' <param name="option">
        ''' The <see cref="KeepAliveOption"/> to convert.
        ''' </param>
        ''' 
        ''' <returns>
        ''' The raw string value of the keep-alive duration.
        ''' </returns>
        Public Shared Widening Operator CType([option] As KeepAliveOption) As String

            Return [option]?.ToString()
        End Operator

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Returns a string that represents the current instance.
        ''' </summary>
        ''' 
        ''' <returns>
        ''' A <see cref="String"/> that represents the current instance.
        ''' </returns>
        Public Overrides Function ToString() As String

            Return Me.Value
        End Function

#End Region

    End Class

#End Region

End Namespace