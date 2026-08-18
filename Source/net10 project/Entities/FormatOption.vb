
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

Imports Ollamantis.Core.Helpers
Imports Ollamantis.Contracts

#End Region

Namespace Entities

#Region " FormatOption "

#Disable Warning BC40000 ' Type or member is obsolete (Allow ref structs: Utf8JsonReader)

    ''' <summary>
    ''' Represents the 'format' option in a <see cref="CompletionRequestBase"/>.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <JsonConverter(GetType(FormatOptionJsonConverter))>
    <DebuggerStepThrough>
    <DebuggerDisplay("IsString = {Me.IsString}, StringValue = {Me.StringValue}")>
    Public Class FormatOption : Inherits EntityOptionBase

#Enable Warning BC40000 ' Type or member is obsolete (Allow ref structs: Utf8JsonReader)

#Region " Properties "

        ''' <summary>
        ''' Gets a value indicating whether this format option represents a simple string.
        ''' </summary>
        Public ReadOnly IsString As Boolean

        ''' <summary>
        ''' Gets the string value of this format option.
        ''' <para></para>
        ''' This value is only meaningful if <see cref="FormatOption.IsString"/> is <see langword="True"/>.
        ''' </summary>
        Public ReadOnly StringValue As String

        ''' <summary>
        ''' Gets the schema object of this format option.
        ''' <para></para>
        ''' This value is only meaningful if <see cref="FormatOption.IsString"/> is <see langword="False"/>.
        ''' </summary>
        Public ReadOnly SchemaObject As Object

#End Region

#Region " Static Fields "

        ''' <summary>
        ''' Represents the JSON response format.
        ''' </summary>
        Public Shared ReadOnly Json As New FormatOption("json")

        ''' <summary>
        ''' Represents the standard response format. This value is null.
        ''' </summary>
        Public Shared ReadOnly Standard As FormatOption = Nothing

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="FormatOption"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="FormatOption"/> class with the specified string value.
        ''' </summary>
        ''' 
        ''' <param name="stringValue">
        ''' The string value indicating the format (e.g., "<c>json</c>").
        ''' </param>
        Private Sub New(stringValue As String)

            Me.IsString = True
            Me.StringValue = stringValue
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="FormatOption"/> class with the specified schema object.
        ''' </summary>
        ''' 
        ''' <param name="schemaObject">
        ''' The object representing a valid JSON schema.
        ''' </param>
        Private Sub New(schemaObject As Object)

            Me.IsString = False
            Me.SchemaObject = schemaObject
        End Sub

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Creates a new <see cref="FormatOption"/> instance from the specified JSON schema object.
        ''' </summary>
        ''' 
        ''' <param name="schema">
        ''' An object or dictionary representing the JSON schema structure.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="FormatOption"/> wrapping the provided schema object.
        ''' </returns>
        ''' 
        ''' <exception cref="ArgumentNullException">
        ''' Thrown when the <paramref name="schema"/> is <see langword="Nothing"/>.
        ''' </exception>
        Public Shared Function FromJsonSchema(schema As Object) As FormatOption

            ArgumentValidator.ThrowIfNull(schema, NameOf(schema))
            Return New FormatOption(schema)
        End Function

#End Region

#Region " Implicit Operators "

        ''' <summary>
        ''' Performs an implicit conversion from <see cref="String"/> to <see cref="FormatOption"/>.
        ''' </summary>
        ''' 
        ''' <param name="value">
        ''' The <see cref="String"/> format value to convert (e.g., "<c>json</c>").
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="FormatOption"/> equivalent to the provided <see cref="String"/> value.
        ''' </returns>
        Public Shared Widening Operator CType(value As String) As FormatOption

            If String.IsNullOrWhiteSpace(value) Then
                Throw New ArgumentException("Format string cannot be null or whitespace.", NameOf(value))
            End If

            Return If(String.Equals(value.Trim(), "json", StringComparison.OrdinalIgnoreCase),
                      FormatOption.Json,
                      New FormatOption(value.Trim().ToLowerInvariant()))
        End Operator

        ''' <summary>
        ''' Performs an implicit conversion from <see cref="FormatOption"/> to <see cref="String"/>.
        ''' </summary>
        ''' 
        ''' <param name="option">
        ''' The <see cref="FormatOption"/> to convert.
        ''' </param>
        ''' 
        ''' <returns>
        ''' The string representation of the format option.
        ''' </returns>
        Public Shared Widening Operator CType([option] As FormatOption) As String

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

            Return If(Me.IsString,
                      Me.StringValue,
                      If(Me.SchemaObject IsNot Nothing,
                         JsonSerializer.Serialize(Me.SchemaObject),
                         String.Empty))
        End Function

#End Region

    End Class

#End Region

End Namespace
