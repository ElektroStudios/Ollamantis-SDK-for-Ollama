#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

#If Not NETCOREAPP Then
Imports System.Drawing
#End If

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO
Imports System.Text.Json.Serialization

Imports Ollamantis.Core.Helpers

#End Region

Namespace Entities

#Region " ImageOption "

#Disable Warning BC40000 ' Type or member is obsolete (Allow ref structs: Utf8JsonReader)

    ''' <summary>
    ''' Represents an image consumable by an Ollama model.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Browsable(True)>
    <JsonConverter(GetType(ImageOptionJsonConverter))>
    <DebuggerStepThrough>
    <DebuggerDisplay("Base64Data = {Me.Base64Data}")>
    Public Class ImageOption : Inherits EntityOptionBase

#Enable Warning BC40000 ' Type or member is obsolete (Allow ref structs: Utf8JsonReader)

#Region " Properties "

        ''' <summary>
        ''' Gets the raw base64-encoded string representation of the image.
        ''' </summary>
        Public ReadOnly Property Base64Data As String

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="ImageOption"/> class from being created.
        ''' </summary>
        Private Sub New()

            Me.Base64Data = String.Empty
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ImageOption"/> class with a base64-encoded string of an image.
        ''' </summary>
        ''' 
        ''' <param name="base64String">
        ''' The base64-encoded string of the image.
        ''' </param>
        Private Sub New(base64String As String)

            Me.Base64Data = base64String
        End Sub

#End Region

#Region " Static Factory Methods "

        ''' <summary>
        ''' Creates a new <see cref="ImageOption"/> directly from a pre-encoded base64 string.
        ''' </summary>
        ''' 
        ''' <param name="base64String">
        ''' The base64 string.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A new <see cref="ImageOption"/> instance.
        ''' </returns>
        Public Shared Function FromBase64(base64String As String) As ImageOption

            Return New ImageOption(base64String)
        End Function

        ''' <summary>
        ''' Reads an image file from the disk, converts it to a byte array, and creates a new <see cref="ImageOption"/>.
        ''' </summary>
        ''' 
        ''' <param name="filePath">
        ''' The absolute or relative path to the image file.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A new <see cref="ImageOption"/> instance containing the base64 data.
        ''' </returns>
        Public Shared Function FromFile(filePath As String) As ImageOption

            ' Convert the path to an extended-length path to bypass MAX_PATH limitations.
            Dim extendedPath As String = FileSystemHelper.GetExtendedPath(filePath)

            Dim bytes As Byte() = File.ReadAllBytes(extendedPath)

            Return New ImageOption(Convert.ToBase64String(bytes))
        End Function

        ''' <summary>
        ''' Creates a new <see cref="ImageOption"/> from a raw byte array.
        ''' </summary>
        ''' 
        ''' <param name="bytes">
        ''' The byte array containing the image data.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A new <see cref="ImageOption"/> instance.
        ''' </returns>
        Public Shared Function FromByteArray(bytes As Byte()) As ImageOption

            Return New ImageOption(Convert.ToBase64String(bytes))
        End Function

#End Region

#Region " Implicit Operators "

        ''' <summary>
        ''' Performs an implicit conversion from <see cref="Byte()"/> to <see cref="ImageOption"/>.
        ''' </summary>
        ''' 
        ''' <param name="bytes">
        ''' The <see cref="Byte()"/> value to convert.
        ''' </param>
        ''' 
        ''' <returns>
        ''' The result of the conversion.
        ''' </returns>
        Public Shared Widening Operator CType(bytes As Byte()) As ImageOption

            Return ImageOption.FromByteArray(bytes)
        End Operator

#If Not NETCOREAPP Then
        ' DELETE THIS NETCOREAPP CONDITION IF YOU DON'T CARE TO ADD AND DEPEND ON NUGET PACKAGE 'System.Drawing.Common'.

        ''' <summary>
        ''' Performs an implicit conversion from <see cref="System.Drawing.Image"/> to <see cref="ImageOption"/>.
        ''' <para></para>
        ''' Note: This operation saves the image to a memory stream as PNG format before encoding.
        ''' </summary>
        ''' 
        ''' <param name="img">
        ''' The <see cref="System.Drawing.Image"/> to convert.
        ''' </param>
        ''' 
        ''' <returns>
        ''' The result of the conversion.
        ''' </returns>
        Public Shared Widening Operator CType(img As System.Drawing.Image) As ImageOption
        
            If img Is Nothing Then
                Return Nothing
            End If
        
            Dim estimatedCapacity As Integer = (img.Width * img.Height * 4)
        
            Using ms As New MemoryStream(capacity:=estimatedCapacity)
                img.Save(ms, Imaging.ImageFormat.Png)
                Return New ImageOption(Convert.ToBase64String(ms.ToArray()))
            End Using
        End Operator
        
#End If

        ''' <summary>
        ''' Performs an implicit conversion from <see cref="ImageOption"/> to <see cref="String"/>.
        ''' </summary>
        ''' 
        ''' <param name="image">
        ''' The <see cref="ImageOption"/> to convert.
        ''' </param>
        ''' 
        ''' <returns>
        ''' The base64-encoded string of the image.
        ''' </returns>
        Public Shared Widening Operator CType(image As ImageOption) As String

            Return image?.ToString()
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

            Return Me.Base64Data
        End Function

#End Region

    End Class

#End Region

End Namespace