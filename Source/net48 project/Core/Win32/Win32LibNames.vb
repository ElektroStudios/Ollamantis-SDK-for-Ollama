
#If not NETCOREAPP

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Runtime.InteropServices

#End Region

#Region " Win32LibNames "

Namespace Core.Win32

    ''' <summary>
    ''' Contains the filenames to specify in the <see cref="DllImportAttribute.Value"/> for all used Win32 API libraries.
    ''' </summary>
    Friend Module Win32LibNames

        ''' <summary>
        ''' shlwapi.dll
        ''' </summary>
        Friend Const ShlwApi As String = "shlwapi.dll"

    End Module

End Namespace

#End Region

#End If
