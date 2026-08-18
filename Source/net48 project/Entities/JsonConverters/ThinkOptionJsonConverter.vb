
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Diagnostics.CodeAnalysis
Imports System.Diagnostics.Eventing
Imports System.Text.Json
Imports System.Text.Json.Serialization

#End Region

Namespace Entities

#Region " ThinkOptionJsonConverter "

    ''' <summary>
    ''' Provides custom JSON serialization for the <see cref="ThinkOption"/> class.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <Browsable(False)>
    <SuppressMessage("Major Code Smell", "S1133:Deprecated code should be removed", Justification:="Completely required in VB.NET.")>
    <Obsolete("Allow ref structs.", False)> ' VB.NET compiler hack required to allow Utf8JsonReader.
    <DebuggerStepThrough>
    Friend NotInheritable Class ThinkOptionJsonConverter : Inherits JsonConverter(Of ThinkOption)

#Region " Public Methods "

        ''' <summary>
        ''' Reads and converts the JSON to type <see cref="ThinkOption"/>.
        ''' </summary>
        ''' 
        ''' <param name="reader">
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
        ''' The resulting <see cref="ThinkOption"/>.
        ''' </returns>
        Public Overrides Function Read(ByRef reader As Utf8JsonReader,
                                             typeToConvert As Type,
                                             options As JsonSerializerOptions) As ThinkOption

            If reader.TokenType = JsonTokenType.True Then
                Return ThinkOption.Enabled

            ElseIf reader.TokenType = JsonTokenType.False Then
                Return ThinkOption.Disabled

            ElseIf reader.TokenType = JsonTokenType.String Then
                Return reader.GetString()

            Else
                Return Nothing

            End If
        End Function

        ''' <summary>
        ''' Writes a specified <see cref="ThinkOption"/> object as JSON.
        ''' </summary>
        ''' 
        ''' <param name="writer">
        ''' The <see cref="Utf8JsonWriter"/> to write to.
        ''' </param>
        ''' 
        ''' <param name="value">
        ''' The <see cref="ThinkOption"/> object to convert to JSON.
        ''' </param>
        ''' 
        ''' <param name="options">
        ''' An object that specifies serialization options to use.
        ''' </param>
        Public Overrides Sub Write(writer As Utf8JsonWriter,
                                   value As ThinkOption,
                                   options As JsonSerializerOptions)

            If value Is Nothing Then
                writer.WriteNullValue()

            ElseIf value.IsBoolean Then
                writer.WriteBooleanValue(value.BooleanValue)

            Else
                writer.WriteStringValue(value.StringValue)

            End If
        End Sub

#End Region

    End Class

#End Region

End Namespace
