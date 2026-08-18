#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics

#End Region

Namespace Core.Helpers

#Region " ArgumentValidator "

    ''' <summary>
    ''' Provides internal centralized helper methods for argument validation.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Module ArgumentValidator

#Region " Methods "

        ''' <summary>
        ''' Throws an exception if the specified object reference is null.
        ''' </summary>
        ''' 
        ''' <typeparam name="T">
        ''' The type of the object being evaluated. Must be a reference type.
        ''' </typeparam>
        ''' 
        ''' <param name="value">
        ''' The object instance to evaluate.
        ''' </param>
        ''' 
        ''' <param name="paramName">
        ''' The name of the parameter being evaluated.
        ''' </param>
        Friend Sub ThrowIfNull(Of T As Class)(value As T, paramName As String)

#If Not NETCOREAPP Then
            If value Is Nothing Then
                Throw New ArgumentNullException(paramName)
            End If
#Else
            ArgumentNullException.ThrowIfNull(value, paramName)
#End If
        End Sub

        ''' <summary>
        ''' Throws an exception if the specified object reference is null.
        ''' </summary>
        ''' 
        ''' <typeparam name="T">
        ''' The type of the object being evaluated. Must be a reference type.
        ''' </typeparam>
        ''' 
        ''' <param name="value">
        ''' The object instance to evaluate.
        ''' </param>
        ''' 
        ''' <param name="paramName">
        ''' The name of the parameter being evaluated.
        ''' </param>
        ''' 
        ''' <param name="propertyName">
        ''' The name of the property, used to format the fallback error message.
        ''' </param>
        Friend Sub ThrowIfNull(Of T As Class)(value As T, paramName As String, propertyName As String)

#If Not NETCOREAPP Then
            If value Is Nothing Then
                Throw New ArgumentNullException(paramName, $"{propertyName} cannot be null.")
            End If
#Else
            ArgumentNullException.ThrowIfNull(value, paramName)
#End If
        End Sub

        ''' <summary>
        ''' Throws an exception if the specified string is null, empty, or consists only of white-space characters.
        ''' </summary>
        ''' 
        ''' <param name="value">
        ''' The string value to evaluate.
        ''' </param>
        ''' 
        ''' <param name="paramName">
        ''' The name of the parameter being evaluated.
        ''' </param>
        Friend Sub ThrowIfNullOrWhiteSpace(value As String, paramName As String)

#If Not NETCOREAPP Then
            If String.IsNullOrWhiteSpace(value) Then
                Throw New ArgumentNullException(paramName)
            End If
#Else
            ArgumentException.ThrowIfNullOrWhiteSpace(value, paramName)
#End If
        End Sub

        ''' <summary>
        ''' Throws an exception if the specified string is null, empty, or consists only of white-space characters.
        ''' </summary>
        ''' 
        ''' <param name="value">
        ''' The string value to evaluate.
        ''' </param>
        ''' 
        ''' <param name="paramName">
        ''' The name of the parameter being evaluated.
        ''' </param>
        ''' 
        ''' <param name="propertyName">
        ''' The name of the property, used to format the fallback error message.
        ''' </param>
        Friend Sub ThrowIfNullOrWhiteSpace(value As String, paramName As String, propertyName As String)

#If Not NETCOREAPP Then
            If String.IsNullOrWhiteSpace(value) Then
                Throw New ArgumentNullException(paramName, $"{propertyName} cannot be null or empty.")
            End If
#Else
            ArgumentException.ThrowIfNullOrWhiteSpace(value, paramName)
#End If
        End Sub

#End Region

    End Module

#End Region

End Namespace