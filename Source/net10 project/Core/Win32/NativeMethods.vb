#If not NETCOREAPP

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Runtime.InteropServices
Imports System.Security
Imports System.Text

Imports Ollamantis.Core.Win32.Enums

#End Region

Namespace Core.Win32

#Region " NativeMethods "

    ''' <summary>
    ''' Provides platform Invocation methods (P/Invoke), access unmanaged code.
    ''' </summary>
    <SuppressUnmanagedCodeSecurity>
    Friend Module NativeMethods

#Region " shlwapi.dll "

        ''' <summary>
        ''' Converts a numeric value into a string that represents the number expressed as a size value in bytes,
        ''' kilobytes, megabytes, gigabytes, petabytes or exabytes, depending on the size.
        ''' <para></para>
        ''' Extends StrFormatByteSizeW by offering the option to round to the nearest displayed digit or to discard undisplayed digits.
        ''' </summary>
        '''
        ''' <remarks>
        ''' <see href="https://docs.microsoft.com/en-us/windows/desktop/api/shlwapi/nf-shlwapi-strformatbytesizeex"/>
        ''' </remarks>
        '''
        ''' <param name="size">
        ''' The numeric value to be converted, expressed in bytes.
        ''' </param>
        '''
        ''' <param name="flags">
        ''' Specifies whether to round or truncate undisplayed digits.
        ''' </param>
        '''
        ''' <param name="buffer">
        ''' A buffer that, when this function returns successfully, receives the converted number.
        ''' </param>
        '''
        ''' <param name="bufferSize">
        ''' The size of <paramref name="buffer"/>, in characters.
        ''' </param>
        '''
        ''' <returns>
        ''' If this function succeeds, it returns HRESULT.S_OK (zero).
        ''' Otherwise, it returns an HRESULT error code.
        ''' </returns>
        <DllImport(Win32LibNames.ShlwApi, SetLastError:=False, ExactSpelling:=True, CharSet:=CharSet.Unicode)>
        Friend Function StrFormatByteSizeEx(size As ULong,
                                            flags As StrFormatByteSizeFlags,
                                            buffer As StringBuilder,
                                            bufferSize As UInteger
                                           ) As Integer ' HRESULT
        End Function

#End Region

    End Module

#End Region

End Namespace

#End If
