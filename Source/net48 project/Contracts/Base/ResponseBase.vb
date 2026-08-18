
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Runtime.CompilerServices
Imports System.Text.Json
Imports System.Text.Json.Serialization

Imports Ollamantis.Core

#End Region

#Region " Assembly Attribute Statements "

<Assembly: InternalsVisibleTo("Ollamantis.Tests")>

#End Region

Namespace Contracts

#Region " ResponseBase "

    ''' <summary>
    ''' Provides a base implementation for all Ollama API responses.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public MustInherit Class ResponseBase : Inherits JsonObjectBaseImmutable

#Region " Properties "

        ''' <summary>
        ''' Gets a <see cref="Boolean"/> value indicating whether the HTTP response indicates success.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.Never)>
        <JsonPropertyName("isSuccessful")>
        <DisplayName("Is Successful")>
        <Description("")>
        Public ReadOnly Property IsSuccessful As Boolean
            Get
                Return Me.isSuccessful_
            End Get
        End Property
        ''' <summary>
        ''' ( Backing field of <see cref="ResponseBase.IsSuccessful"/> property. )
        ''' <para></para>
        ''' A <see cref="Boolean"/> value indicating whether the HTTP response indicates success.
        ''' </summary>
        Private isSuccessful_ As Boolean

        ''' <summary>
        ''' Gets the HTTP status code returned by the Ollama server.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.Never)>
        <JsonPropertyName("statusCode")>
        <DisplayName("Status Code")>
        <Description("")>
        Public ReadOnly Property StatusCode As Integer
            Get
                Return Me.statusCode_
            End Get
        End Property
        ''' <summary>
        ''' ( Backing field of <see cref="ResponseBase.StatusCode"/> property. )
        ''' <para></para>
        ''' The HTTP status code returned by the Ollama server.
        ''' </summary>
        Private statusCode_ As Integer

        ''' <summary>
        ''' Gets the reason phrase sent by the server together with the status code.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("reasonPhrase")>
        <DisplayName("Reason Phrase")>
        <Description("")>
        Public ReadOnly Property ReasonPhrase As String
            Get
                Return Me.reasonPhrase_
            End Get
        End Property
        ''' <summary>
        ''' ( Backing field of <see cref="ResponseBase.ReasonPhrase"/> property. )
        ''' <para></para>
        ''' The reason phrase sent by the server together with the status code.
        ''' </summary>
        Private reasonPhrase_ As String

        ''' <summary>
        ''' Gets the specific error message returned by the Ollama API if the operation failed.
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("errorMessage")>
        <DisplayName("Error Message")>
        <Description("")>
        Public ReadOnly Property ErrorMessage As String
            Get
                Return Me.errorMessage_
            End Get
        End Property
        ''' <summary>
        ''' ( Backing field of <see cref="ResponseBase.ErrorMessage"/> property. )
        ''' <para></para>
        ''' The specific error message returned by the Ollama API if the operation failed.
        ''' </summary>
        Private errorMessage_ As String

        ''' <summary>
        ''' Gets the string to display in the debugger variable windows.
        ''' </summary>
        <Browsable(False)>
        <DebuggerBrowsable(DebuggerBrowsableState.Never)>
        Protected Overridable ReadOnly Property DebuggerDisplay As String
            Get
                Return $"IsSuccessful = {Me.IsSuccessful}, StatusCode = {Me.StatusCode}, ReasonPhrase = {Me.ReasonPhrase}"
            End Get
        End Property

#End Region

#Region " Methods "

        ''' <summary>
        ''' Hydrates (populates) the HTTP metadata and extracts any API error message from the raw JSON payload.
        ''' </summary>
        ''' 
        ''' <param name="isSuccessful">
        ''' A <see cref="Boolean"/> value indicating whether the HTTP response indicates success.
        ''' </param>
        ''' 
        ''' <param name="statusCode">
        ''' The HTTP status code returned by the Ollama server.
        ''' </param>
        ''' 
        ''' <param name="reasonPhrase">
        ''' The reason phrase sent by the server together with the status code.
        ''' </param>
        ''' 
        ''' <param name="rawJson">
        ''' The raw JSON string payload returned by the server, used to extract nested error messages.
        ''' </param>
        Friend Sub HydrateMetadata(isSuccessful As Boolean,
                                   statusCode As Integer,
                                   reasonPhrase As String,
                                   rawJson As String)

            Me.isSuccessful_ = isSuccessful
            Me.statusCode_ = statusCode
            Me.reasonPhrase_ = reasonPhrase

            ' Check if there's a JSON payload to parse.
            If Not String.IsNullOrWhiteSpace(rawJson) AndAlso rawJson.TrimStart().StartsWith("{"c) Then
                Try
                    Using document As JsonDocument = JsonDocument.Parse(rawJson)
                        Dim errorElement As JsonElement
                        ' Ollama can return HTTP 200 OK for a stream, 
                        ' but embeds the real error inside the NDJSON line.
                        If document.RootElement.TryGetProperty("error", errorElement) Then
                            Me.errorMessage_ = errorElement.GetString()
                            Me.isSuccessful_ = False ' Force failure regardless of HTTP status.
                        End If
                    End Using

                Catch ex As JsonException
                    ' Fallback if the JSON is completely malformed and the HTTP request actually failed.
                    If Not Me.isSuccessful_ Then
                        Me.errorMessage_ = $"HTTP {statusCode} ({reasonPhrase}). Raw response: {rawJson}"
                    End If
                End Try

            ElseIf Not Me.isSuccessful_ Then
                ' No JSON body, but HTTP failed.
                Me.errorMessage_ = $"HTTP {statusCode} ({reasonPhrase})."

            End If
        End Sub

#End Region

    End Class

#End Region

End Namespace
