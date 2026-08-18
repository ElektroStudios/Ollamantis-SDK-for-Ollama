
#If not NETCOREAPP

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " StrFormatByteSizeFlags "

Namespace Core.Win32.Enums

    ''' <summary>
    ''' Specifies whether to round or truncate undisplayed digits when calling 
    ''' <see cref="NativeMethods.StrFormatByteSizeEx"/> function.
    ''' </summary>
    '''
    ''' <remarks>
    ''' <see href="https://docs.microsoft.com/en-us/windows/desktop/api/shlwapi/ne-shlwapi-tagsfbs_flags"/>
    ''' </remarks>
    Friend Enum StrFormatByteSizeFlags

        ''' <summary>
        ''' Round to the nearest displayed digit.
        ''' </summary>
        RoundToNearest = 1

        ''' <summary>
        ''' Discard undisplayed digits.
        ''' </summary>
        Truncate = 2

    End Enum

End Namespace

#End Region

#End If
