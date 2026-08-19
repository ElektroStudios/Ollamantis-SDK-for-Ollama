
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Globalization
Imports System.Runtime.InteropServices
Imports System.Text

#If Not NETCOREAPP Then

Imports Ollamantis.Core.Win32
Imports Ollamantis.Core.Win32.Enums

#End If

#End Region

Namespace Core

#Region " JsonObjectBaseImmutable "

    ''' <summary>
    ''' Provides a base implementation for classes that can be serialized to JSON and are immutable (read-only).
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <Browsable(True)>
    <ImmutableObject(True)> ' Inherited = True
    <DebuggerStepThrough>   ' Inherited = False
    Public MustInherit Class JsonObjectBaseImmutable : Inherits JsonObjectBase

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="JsonObjectBaseImmutable"/> class.
        ''' </summary>
        Protected Sub New()
            MyBase.New()
        End Sub

#End Region

#Region " Protected Methods "

        ''' <summary>
        ''' Helper method to convert a nullable <see cref="Long"/> expressed in bytes 
        ''' to a human-readable format (e.g., KB, MB, GB).
        ''' </summary>
        ''' 
        ''' <param name="byteSize">
        ''' The nullable <see cref="Long"/> expressed in bytes.
        ''' <para></para>
        ''' If <see langword="Nothing"/>, returns <see langword="Nothing"/>, 
        ''' rather than throwing an <see cref="Exception"/>.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A formatted <see cref="String"/>, or <see langword="Nothing"/> if the size is unknown.
        ''' </returns>
        Protected Shared Function FormatByteSize(byteSize As Long?) As String

            If Not byteSize.HasValue Then
                Return Nothing
            End If

#If Not NETCOREAPP Then
            Dim buffer As New StringBuilder(32)
            Dim flags As StrFormatByteSizeFlags = StrFormatByteSizeFlags.RoundToNearest
            Dim hr As Integer = NativeMethods.StrFormatByteSizeEx(CULng(byteSize.Value), flags, buffer, CUInt(buffer.Capacity))

            If hr <> 0 Then
                Marshal.ThrowExceptionForHR(hr)
            End If

            Return buffer.ToString()
#Else
            Dim size As Double = byteSize.Value

            If size = 0 Then
                Return "0 bytes"
            End If

            Dim suffixes() As String = {"bytes", "KB", "MB", "GB", "TB", "PB", "EB"}
            Dim suffixIndex As Integer = 0

            ' Determine the correct magnitude (base 1024).
            While size >= 1024.0 AndAlso suffixIndex < (suffixes.Length - 1)
                size /= 1024.0
                suffixIndex += 1
            End While

            ' Format with up to two decimal places ("0.##") and append the correct suffix.
            ' Explicitly using CurrentCulture to ensure regional decimal separators (comma vs dot).
            Dim formattedNumber As String = size.ToString("0.##", CultureInfo.CurrentCulture)

            Return $"{formattedNumber} {suffixes(suffixIndex)}"
#End If
        End Function

        ''' <summary>
        ''' Helper method to convert a nullable <see cref="DateTimeOffset"/> to local time as a human-readable 24-hour string.
        ''' </summary>
        ''' 
        ''' <param name="dateTime">
        ''' The nullable <see cref="DateTimeOffset"/> value to format. 
        ''' <para></para>
        ''' If <see langword="Nothing"/>, returns <see langword="Nothing"/>, 
        ''' rather than throwing an <see cref="Exception"/>.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A formatted <see cref="String"/> representing the local date and time, 
        ''' or <see langword="Nothing"/> if the input value is not provided.
        ''' </returns>
        Protected Shared Function FormatDateTimeOffset(dateTime As DateTimeOffset?) As String

            If Not dateTime.HasValue Then
                Return Nothing
            End If

            ' "F" automatically translates and formats to the OS language and region preferences.
            ' EN-US: "Tuesday, August 18, 2026 1:18:49 PM"
            ' ES-ES: "martes, 18 de agosto de 2026 13:18:49"
            Return dateTime.Value.ToLocalTime().ToString("F", CultureInfo.CurrentCulture)
        End Function

#End Region

    End Class

#End Region

End Namespace
