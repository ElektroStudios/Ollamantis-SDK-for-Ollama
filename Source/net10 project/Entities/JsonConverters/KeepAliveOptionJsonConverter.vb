
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Text.Json
Imports System.Text.Json.Serialization

#End Region

Namespace Entities

#Region " KeepAliveOptionJsonConverter "

    ''' <summary>
    ''' Provides custom JSON serialization for the <see cref="KeepAliveOption"/> class.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <Browsable(False)>
    <Obsolete("Allow ref structs.", False)> ' VB.NET compiler hack required to allow Utf8JsonReader.
    <DebuggerStepThrough>
    Friend NotInheritable Class KeepAliveOptionJsonConverter : Inherits JsonConverter(Of KeepAliveOption)

#Region " Public Methods "

        ''' <summary>
        ''' Reads and converts the JSON to type <see cref="KeepAliveOption"/>.
        ''' </summary>
        ''' 
        ''' <param name="refReader">
        ''' The <see cref="Utf8JsonReader"/> to read from.
        ''' </param>
        ''' 
        ''' <param name="typeToConvert">
        ''' The type to convert.
        ''' </param>
        ''' 
        ''' <param name="options">
        ''' An object that specifies serialization options to use.
        ''' </param>
        ''' 
        ''' <returns>
        ''' The resulting <see cref="KeepAliveOption"/>.
        ''' </returns>
        Public Overrides Function Read(ByRef refReader As Utf8JsonReader,
                                             typeToConvert As Type,
                                             options As JsonSerializerOptions) As KeepAliveOption

            If refReader.TokenType = JsonTokenType.String Then
                Return CType(refReader.GetString(), KeepAliveOption)

            ElseIf refReader.TokenType = JsonTokenType.Number Then
                ' If Ollama returns raw seconds instead of a duration string.
                Dim seconds As Integer = refReader.GetInt32()
                Return CType(seconds, KeepAliveOption)

            Else
                Return Nothing

            End If
        End Function

        ''' <summary>
        ''' Writes a specified <see cref="KeepAliveOption"/> object as JSON.
        ''' </summary>
        ''' 
        ''' <param name="writer">
        ''' The <see cref="Utf8JsonWriter"/> to write to.
        ''' </param>
        ''' 
        ''' <param name="value">
        ''' The <see cref="KeepAliveOption"/> object to convert to JSON.
        ''' </param>
        ''' 
        ''' <param name="options">
        ''' An object that specifies serialization options to use.
        ''' </param>
        Public Overrides Sub Write(writer As Utf8JsonWriter,
                                   value As KeepAliveOption,
                                   options As JsonSerializerOptions)

            If value Is Nothing Then
                writer.WriteNullValue()
            Else
                ' Ollama always expects the string representation (e.g., "300s", "5m", "-1m").
                writer.WriteStringValue(value.Value)
            End If
        End Sub

#End Region

    End Class

#End Region

End Namespace