
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Text.Json.Serialization

#End Region

Namespace Entities

#Region " RoleOption "

#Disable Warning BC40000 ' Type or member is obsolete (Allow ref structs: Utf8JsonReader)

    ''' <summary>
    ''' Represents the 'role' option in a <see cref="ChatMessage"/>.
    ''' </summary>
    ''' 
    ''' <remarks>
    ''' For additional information, visit the 
    ''' <see href="https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-chat-completion">
    ''' Ollama API documentation</see>.
    ''' </remarks>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <JsonConverter(GetType(RoleOptionJsonConverter))>
    <DebuggerStepThrough>
    <DebuggerDisplay("Value = {Me.Value}")>
    Public Class RoleOption : Inherits EntityOptionBase

#Enable Warning BC40000 ' Type or member is obsolete (Allow ref structs: Utf8JsonReader)

#Region " Properties "

        ''' <summary>
        ''' Gets the raw string value of this role.
        ''' </summary>
        Public ReadOnly Value As String

#End Region

#Region " Static Fields "

        ''' <summary>
        ''' Represents the "system" role.
        ''' </summary>
        Public Shared ReadOnly System As New RoleOption("system")

        ''' <summary>
        ''' Represents the "user" role.
        ''' </summary>
        Public Shared ReadOnly User As New RoleOption("user")

        ''' <summary>
        ''' Represents the "assistant" role.
        ''' </summary>
        Public Shared ReadOnly Assistant As New RoleOption("assistant")

        ''' <summary>
        ''' Represents the "tool" role.
        ''' </summary>
        Public Shared ReadOnly Tool As New RoleOption("tool")

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="RoleOption"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="RoleOption"/> class with the specified string value.
        ''' </summary>
        ''' 
        ''' <param name="value">
        ''' The string value indicating the role (e.g., "<c>system</c>", "<c>user</c>", "<c>assistant</c>").
        ''' </param>
        Private Sub New(value As String)
            Me.Value = value
        End Sub

#End Region

#Region " Implicit Operators "

        ''' <summary>
        ''' Performs an implicit conversion from <see cref="String"/> to <see cref="RoleOption"/>.
        ''' </summary>
        ''' 
        ''' <param name="value">
        ''' The <see cref="String"/> role value to convert.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A <see cref="RoleOption"/> equivalent to the provided <see cref="String"/> value.
        ''' </returns>
        Public Shared Widening Operator CType(value As String) As RoleOption

            If String.IsNullOrWhiteSpace(value) Then
                Throw New ArgumentException("Role string cannot be null or whitespace.", NameOf(value))
            End If

            Return New RoleOption(value.Trim().ToLowerInvariant())
        End Operator

        ''' <summary>
        ''' Performs an implicit conversion from <see cref="RoleOption"/> to <see cref="String"/>.
        ''' </summary>
        ''' 
        ''' <param name="option">
        ''' The <see cref="RoleOption"/> to convert.
        ''' </param>
        ''' 
        ''' <returns>
        ''' The raw string value of the role.
        ''' </returns>
        Public Shared Widening Operator CType([option] As RoleOption) As String

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