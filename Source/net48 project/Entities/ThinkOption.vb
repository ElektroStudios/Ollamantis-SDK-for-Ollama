
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Text.Json.Serialization

Imports Ollamantis.Contracts

#End Region

Namespace Entities

#Region " ThinkOption "

#Disable Warning BC40000 ' Type or member is obsolete (Allow ref structs: Utf8JsonReader)

    ''' <summary>
    ''' Represents the 'think' option in a <see cref="CompletionRequestBase"/>.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <JsonConverter(GetType(ThinkOptionJsonConverter))>
    <DebuggerStepThrough>
    <DebuggerDisplay("IsBoolean = {Me.IsBoolean}, BooleanValue = {Me.BooleanValue}, StringValue = {Me.StringValue}")>
    Public Class ThinkOption : Inherits EntityOptionBase

#Enable Warning BC40000 ' Type or member is obsolete (Allow ref structs: Utf8JsonReader)

#Region " Properties "

        ''' <summary>
        ''' Gets a value indicating whether this think option represents a <see cref="Boolean"/> value.
        ''' </summary>
        Public ReadOnly IsBoolean As Boolean

        ''' <summary>
        ''' Gets the boolean value of this think option.
        ''' <para></para>
        ''' This value is only meaningful if <see cref="ThinkOption.IsBoolean"/> is <see langword="True"/>.
        ''' </summary>
        Public ReadOnly BooleanValue As Boolean

        ''' <summary>
        ''' Gets the string value of this think option.
        ''' <para></para>
        ''' This value is only meaningful if <see cref="ThinkOption.IsBoolean"/> is <see langword="False"/>.
        ''' </summary>
        Public ReadOnly StringValue As String

#End Region

#Region " Static Fields "

        ''' <summary>
        ''' Represents the boolean <see langword="True"/> think option.
        ''' </summary>
        Public Shared ReadOnly Enabled As New ThinkOption(True)

        ''' <summary>
        ''' Represents the boolean <see langword="False"/> think option.
        ''' </summary>
        Public Shared ReadOnly Disabled As New ThinkOption(False)

        ''' <summary>
        ''' Represents the "<c>low</c>" think level.
        ''' </summary>
        Public Shared ReadOnly Low As New ThinkOption("low")

        ''' <summary>
        ''' Represents the "<c>medium</c>" think level.
        ''' </summary>
        Public Shared ReadOnly Medium As New ThinkOption("medium")

        ''' <summary>
        ''' Represents the "<c>high</c>" think level.
        ''' </summary>
        Public Shared ReadOnly High As New ThinkOption("high")

        ''' <summary>
        ''' Represents the "<c>max</c>" think level.
        ''' </summary>
        Public Shared ReadOnly Max As New ThinkOption("max")

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="ThinkOption"/> class from being created.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ThinkOption"/> class with the specified boolean value.
        ''' </summary>
        ''' 
        ''' <param name="booleanValue">
        ''' The boolean value indicating whether the model should think.
        ''' </param>
        Private Sub New(booleanValue As Boolean)

            Me.IsBoolean = True
            Me.BooleanValue = booleanValue
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ThinkOption"/> class with the specified string value.
        ''' </summary>
        ''' 
        ''' <param name="stringValue">
        ''' The string value indicating the think level.
        ''' </param>
        Private Sub New(stringValue As String)

            Me.IsBoolean = False
            Me.StringValue = stringValue
        End Sub

#End Region

#Region " Implicit Operators "

        ''' <summary>
        ''' Performs an implicit conversion from <see cref="Boolean"/> to <see cref="ThinkOption"/>.
        ''' </summary>
        ''' 
        ''' <param name="value">
        ''' The <see cref="Boolean"/> value to convert.
        ''' </param>
        ''' 
        ''' <returns>
        ''' The result of the conversion, which is a <see cref="ThinkOption"/> 
        ''' equivalent to the provided <see cref="Boolean"/> value.
        ''' </returns>
        Public Shared Widening Operator CType(value As Boolean) As ThinkOption

            Return If(value, ThinkOption.Enabled, ThinkOption.Disabled)
        End Operator

        ''' <summary>
        ''' Performs an implicit conversion from <see cref="String"/> to <see cref="ThinkOption"/>.
        ''' </summary>
        ''' 
        ''' <param name="value">
        ''' The <see cref="String"/> value to convert.
        ''' </param>
        ''' 
        ''' <returns>
        ''' The result of the conversion, which is a <see cref="ThinkOption"/> 
        ''' equivalent to the provided <see cref="String"/> value.
        ''' </returns>
        ''' 
        ''' <exception cref="ArgumentException">
        ''' Thrown when the string is not a valid think level.
        ''' </exception>
        Public Shared Widening Operator CType(value As String) As ThinkOption

            Select Case value.ToLowerInvariant()

                Case "low"
                    Return ThinkOption.Low

                Case "medium"
                    Return ThinkOption.Medium

                Case "high"
                    Return ThinkOption.High

                Case "max"
                    Return ThinkOption.Max

                Case Else
                    Throw New ArgumentException($"Invalid think option: '{value}'. Allowed values are 'low', 'medium', 'high', 'max'.")

            End Select
        End Operator

        ''' <summary>
        ''' Performs an implicit conversion from <see cref="ThinkOption"/> to <see cref="String"/>.
        ''' </summary>
        ''' 
        ''' <param name="option">
        ''' The <see cref="ThinkOption"/> to convert.
        ''' </param>
        ''' 
        ''' <returns>
        ''' The string representation of the think option.
        ''' </returns>
        Public Shared Widening Operator CType([option] As ThinkOption) As String

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

            Return If(Me.IsBoolean,
                      Me.BooleanValue.ToString().ToLowerInvariant(),
                      Me.StringValue)
        End Function

#End Region

    End Class

#End Region

End Namespace
