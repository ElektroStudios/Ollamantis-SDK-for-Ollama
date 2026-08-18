
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Diagnostics.CodeAnalysis
Imports System.Text.Json
Imports System.Text.Json.Serialization

#End Region

Namespace Entities

#Region " ImageOptionJsonConverter "

    ''' <summary>
    ''' Provides custom JSON serialization for the <see cref="ImageOption"/> class.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <Browsable(False)>
    <SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression")>
    <SuppressMessage("Major Code Smell", "S1133:Deprecated code should be removed", Justification:="Completely required in VB.NET.")>
    <Obsolete("Allow ref structs.", False)> ' VB.NET compiler hack required to allow Utf8JsonReader.
    <DebuggerStepThrough>
    Friend NotInheritable Class ImageOptionJsonConverter : Inherits JsonConverter(Of ImageOption)

#Region " Public Methods "

        ''' <summary>
        ''' Reads and converts the JSON to type <see cref="ImageOption"/>.
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
        ''' The resulting <see cref="ImageOption"/>.
        ''' </returns>
        Public Overrides Function Read(ByRef reader As Utf8JsonReader,
                                             typeToConvert As Type,
                                             options As JsonSerializerOptions) As ImageOption

            Return If(reader.TokenType = JsonTokenType.String,
                      ImageOption.FromBase64(reader.GetString()),
                      Nothing)
        End Function

        ''' <summary>
        ''' Writes a specified <see cref="ImageOption"/> object as JSON.
        ''' </summary>
        ''' 
        ''' <param name="writer">
        ''' The <see cref="Utf8JsonWriter"/> to write to.
        ''' </param>
        ''' 
        ''' <param name="value">
        ''' The <see cref="ImageOption"/> object to convert to JSON.
        ''' </param>
        ''' 
        ''' <param name="options">
        ''' An object that specifies serialization options to use.
        ''' </param>
        Public Overrides Sub Write(writer As Utf8JsonWriter,
                                   value As ImageOption,
                                   options As JsonSerializerOptions)

            If value Is Nothing OrElse String.IsNullOrEmpty(value.Base64Data) Then
                writer.WriteNullValue()

            Else
                ' The Ollama API simply expects the raw base64 string inside the array.
                writer.WriteStringValue(value.Base64Data)

            End If
        End Sub

#End Region

    End Class

#End Region

End Namespace