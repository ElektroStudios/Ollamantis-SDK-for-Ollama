#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics

#End Region

Namespace Entities

#Region " EntityOptionBase "

    ''' <summary>
    ''' Provides a base implementation for all entity options.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <Browsable(False)>
    <DebuggerStepThrough>
    Public MustInherit Class EntityOptionBase : Implements IEquatable(Of EntityOptionBase)

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="EntityOptionBase"/> class.
        ''' </summary>
        Protected Sub New()
        End Sub

#End Region

#Region " Equality Methods "

        ''' <summary>
        ''' Determines whether the specified <see cref="Object"/> is equal to the current instance.
        ''' </summary>
        ''' 
        ''' <param name="obj">
        ''' The object to compare with the current instance.
        ''' </param>
        ''' 
        ''' <returns>
        ''' <see langword="True"/> if the specified object is equal to the current instance; 
        ''' otherwise, <see langword="False"/>.
        ''' </returns>
        Public Overrides Function Equals(obj As Object) As Boolean

            Return Me.Equals(TryCast(obj, EntityOptionBase))
        End Function

        ''' <summary>
        ''' Determines whether the specified <see cref="EntityOptionBase"/> is equal to the current instance 
        ''' by performing an exact string comparison of their <see cref="Object.ToString"/> representations.
        ''' </summary>
        ''' 
        ''' <param name="other">
        ''' The <see cref="EntityOptionBase"/> to compare with the current instance.
        ''' </param>
        ''' 
        ''' <returns>
        ''' <see langword="True"/> if the specified object is equal to the current instance; 
        ''' otherwise, <see langword="False"/>.
        ''' </returns>
        Public Overloads Function Equals(other As EntityOptionBase) As Boolean Implements IEquatable(Of EntityOptionBase).Equals

            Return other IsNot Nothing AndAlso
                   (
                     Object.ReferenceEquals(Me, other) OrElse
                     (
                       Me.GetType() Is other.GetType() AndAlso
                       String.Equals(Me.ToString(), other.ToString(), StringComparison.Ordinal)
                     )
                   )

        End Function

        ''' <summary>
        ''' Returns the hash code for this instance based on its string representation.
        ''' </summary>
        ''' 
        ''' <returns>
        ''' A 32-bit signed integer hash code.
        ''' </returns>
        Public Overrides Function GetHashCode() As Integer

            Dim stringRepresentation As String = Me.ToString()

            Return If(stringRepresentation IsNot Nothing,
                      stringRepresentation.GetHashCode(),
                      0)
        End Function

#End Region

#Region " Equality Operators "

        ''' <summary>
        ''' Determines whether two specified instances of <see cref="EntityOptionBase"/> are equal.
        ''' </summary>
        ''' 
        ''' <param name="left">
        ''' The first <see cref="EntityOptionBase"/> to compare.
        ''' </param>
        ''' 
        ''' <param name="right">
        ''' The second <see cref="EntityOptionBase"/> to compare.
        ''' </param>
        ''' 
        ''' <returns>
        ''' <see langword="True"/> if the two instances are equal; otherwise, <see langword="False"/>.
        ''' </returns>
        Public Shared Operator =(left As EntityOptionBase, right As EntityOptionBase) As Boolean

            Return (left Is Nothing AndAlso right Is Nothing) OrElse
                   (
                     left IsNot Nothing AndAlso
                     right IsNot Nothing AndAlso
                     left.Equals(right)
                   )
        End Operator

        ''' <summary>
        ''' Determines whether two specified instances of <see cref="EntityOptionBase"/> are not equal.
        ''' </summary>
        ''' 
        ''' <param name="left">
        ''' The first <see cref="EntityOptionBase"/> to compare.
        ''' </param>
        ''' 
        ''' <param name="right">
        ''' The second <see cref="EntityOptionBase"/> to compare.
        ''' </param>
        ''' 
        ''' <returns>
        ''' <see langword="True"/> if the two instances are not equal; otherwise, <see langword="False"/>.
        ''' </returns>
        Public Shared Operator <>(left As EntityOptionBase, right As EntityOptionBase) As Boolean

            Return Not (left = right)
        End Operator

#End Region

    End Class

#End Region

End Namespace