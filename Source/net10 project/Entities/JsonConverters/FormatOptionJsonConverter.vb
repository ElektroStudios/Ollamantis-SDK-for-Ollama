
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

#Region " FormatOptionJsonConverter "

    ''' <summary>
    ''' Provides custom JSON serialization for the <see cref="FormatOption"/> class.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <Browsable(False)>
    <Obsolete("Allow ref structs.", False)> ' VB.NET compiler hack required to allow Utf8JsonReader.
    <DebuggerStepThrough>
    Friend NotInheritable Class FormatOptionJsonConverter : Inherits JsonConverter(Of FormatOption)

#Region " Public Methods "

        ''' <summary>
        ''' Reads and converts the JSON to type <see cref="FormatOption"/>.
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
        ''' The resulting <see cref="FormatOption"/>.
        ''' </returns>
        Public Overrides Function Read(ByRef refReader As Utf8JsonReader,
                                             typeToConvert As Type,
                                             options As JsonSerializerOptions) As FormatOption

            If refReader.TokenType = JsonTokenType.String Then
                Return CType(refReader.GetString(), FormatOption)

            ElseIf refReader.TokenType = JsonTokenType.StartObject Then
                ' Safely capture the raw JSON schema object into a JsonElement.
                Dim schemaObj As JsonElement = JsonSerializer.Deserialize(Of JsonElement)(refReader, options)
                Return FormatOption.FromJsonSchema(schemaObj)

            Else
                Return Nothing

            End If
        End Function

        ''' <summary>
        ''' Writes a specified <see cref="FormatOption"/> object as JSON.
        ''' </summary>
        ''' 
        ''' <param name="writer">
        ''' The <see cref="Utf8JsonWriter"/> to write to.
        ''' </param>
        ''' 
        ''' <param name="value">
        ''' The <see cref="FormatOption"/> object to convert to JSON.
        ''' </param>
        ''' 
        ''' <param name="options">
        ''' An object that specifies serialization options to use.
        ''' </param>
        Public Overrides Sub Write(writer As Utf8JsonWriter,
                                   value As FormatOption,
                                   options As JsonSerializerOptions)

            If value Is Nothing Then
                writer.WriteNullValue()

            ElseIf value.IsString Then
                writer.WriteStringValue(value.StringValue)

            Else
                ' Delegate the serialization of the complex schema object back to the standard serializer.
                JsonSerializer.Serialize(writer, value.SchemaObject, value.SchemaObject.GetType(), options)

            End If
        End Sub

#End Region

    End Class

#End Region

End Namespace
