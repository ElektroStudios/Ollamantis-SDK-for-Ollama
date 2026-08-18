
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

#Region " FileSystemHelper "

    ''' <summary>
    ''' Provides internal helper methods for file system operations.
    ''' </summary>
    <DebuggerStepThrough>
    Friend Module FileSystemHelper

#Region " Static Methods "

        ''' <summary>
        ''' Converts a standard file-system path into an extended-length path (prefixed with <c>\\?\</c>).
        ''' <para></para>
        ''' Useful for bypass the traditional 260-character <c>MAX_PATH</c> limitation in Windows APIs.
        ''' </summary>
        ''' 
        ''' <param name="standardPath">
        ''' The standard file-system path to be converted (e.g., "C:\Folder\File.txt" or "\\Server\Share").
        ''' </param>
        ''' 
        ''' <returns>
        ''' The extended-length path string (e.g., "<c>\\?\C:\Folder\File.txt</c>" or "<c>\\?\UNC\Server\Share</c>"), 
        ''' or the original <paramref name="standardPath"/> if it is empty, a relative path, or already extended.
        ''' </returns>
        Friend Function GetExtendedPath(standardPath As String) As String

            If String.IsNullOrWhiteSpace(standardPath) Then
                Return standardPath
            End If

            ' Windows API strictly requires backslashes for extended paths.
            Dim normalizedPath As String = standardPath.Replace("/"c, "\"c)

            ' If it is already an extended path (\\?\) or a device path (\\.\), return as-is.
            If normalizedPath.StartsWith("\\?\", StringComparison.Ordinal) OrElse
               normalizedPath.StartsWith("\\.\", StringComparison.Ordinal) Then

                Return normalizedPath
            End If

            ' Handle UNC paths: "\\Server\Share" -> "\\?\UNC\Server\Share"
            If normalizedPath.StartsWith("\\", StringComparison.Ordinal) Then
                Dim uncCore As String = normalizedPath.Substring(2)
                Return $"\\?\UNC\{uncCore}"
            End If

            ' Handle fully qualified local paths (e.g., "C:\Folder")
            ' Note: We explicitly avoid using 'Path.IsPathRooted()' because it returns True
            '       for partial paths like "\Folder" (rooted to the current working drive).
            '
            '       Appending "\\?\" to that would create an invalid path ("\\?\\Folder"). 
            '       Instead, we check explicitly for the drive letter format (Drive:\).
            If normalizedPath.Length >= 3 AndAlso
               normalizedPath.Chars(1) = ":"c AndAlso
               normalizedPath.Chars(2) = "\"c Then

                Return $"\\?\{normalizedPath}"
            End If

            ' If it is a relative path or an unknown format, return the original untouched.
            Return standardPath
        End Function

        ' ----------------------------------------------------------------------------------
        ' Not required internally by the library, but kept for possible future requirements.
        ' ----------------------------------------------------------------------------------
        '
        '''' <summary>
        '''' Converts a extended-length path (prefixed with <c>\\?\</c>) into a standard file-system path.
        '''' <para></para>
        '''' Useful for UI display, logging, or interoperability with legacy (old) Win32/Shell components.
        '''' </summary>
        '''' 
        '''' <param name="extendedPath">
        '''' The extended-length path to be converted (e.g., "<c>\\?\C:\Folder\File.txt</c>" or "<c>\\?\UNC\Server\Share</c>").
        '''' </param>
        '''' 
        '''' <returns>
        '''' The standard file-system path string without the extended prefix (e.g., "<c>C:\Folder\File.txt</c>" or "<c>\\Server\Share</c>"),
        '''' or the original <paramref name="extendedPath"/> if it is empty, null, or does not contain an extended prefix.
        '''' </returns>
        '<DebuggerStepThrough>
        'Friend Function GetStandardPath(extendedPath As String) As String
        '
        '    If String.IsNullOrWhiteSpace(extendedPath) Then
        '        Return extendedPath
        '    End If
        '
        '    ' Revert extended UNC paths: \\?\UNC\Server\Share -> \\Server\Share
        '    If extendedPath.StartsWith("\\?\UNC\", StringComparison.OrdinalIgnoreCase) Then
        '        Dim uncCore As String = extendedPath.Substring(8)
        '        Return $"\\{uncCore}"
        '    End If
        '
        '    ' Revert extended local paths: \\?\C:\Folder -> C:\Folder
        '    If extendedPath.StartsWith("\\?\", StringComparison.Ordinal) Then
        '        Dim localCore As String = extendedPath.Substring(4)
        '        Return localCore
        '    End If
        '
        '    Return extendedPath
        'End Function

#End Region

    End Module

#End Region

End Namespace
