#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports Ollamantis.Entities

Imports Xunit

#End Region

Namespace Ollamantis.Tests.Entities

    ''' <summary>
    ''' Contains unit tests for the <see cref="EntityOptionBase"/> class and its equality operators.
    ''' </summary>
    Public Class EntityOptionBaseTests

#Region " Helper Classes "

        ''' <summary>
        ''' A dummy option class to test the base equality logic.
        ''' </summary>
        Private Class DummyOption : Inherits EntityOptionBase

            Public Property Value As String

            Public Overrides Function ToString() As String
                Return Me.Value
            End Function

        End Class

#End Region

#Region " Tests "

        <Fact>
        Public Sub EqualsOperator_WhenStringRepresentationsAreSame_ShouldReturnTrue()

            ' Arrange
            Dim option1 As New DummyOption() With {.Value = "temperature"}
            Dim option2 As New DummyOption() With {.Value = "temperature"}

            ' Act
            Dim result As Boolean = (option1 = option2)

            ' Assert
            Assert.True(result, "The equality operator should return true for instances with identical string representations.")
        End Sub

        <Fact>
        Public Sub InequalityOperator_WhenStringRepresentationsDiffer_ShouldReturnTrue()

            ' Arrange
            Dim option1 As New DummyOption() With {.Value = "temperature"}
            Dim option2 As New DummyOption() With {.Value = "top_p"}

            ' Act
            Dim result As Boolean = (option1 <> option2)

            ' Assert
            Assert.True(result, "The inequality operator should return true for instances with different string representations.")
        End Sub

        <Fact>
        Public Sub EqualsOperator_WhenOneInstanceIsNull_ShouldReturnFalse()

            ' Arrange
            Dim option1 As New DummyOption() With {.Value = "temperature"}
            Dim option2 As DummyOption = Nothing

            ' Act
            Dim result As Boolean = (option1 = option2)

            ' Assert
            Assert.False(result, "The equality operator should handle null values safely and return false.")
        End Sub

#End Region

    End Class

End Namespace