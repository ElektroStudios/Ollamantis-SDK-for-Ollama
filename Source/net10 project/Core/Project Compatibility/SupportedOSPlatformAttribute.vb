
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#If Not NETCOREAPP Then

Namespace Core.ProjectCompatibility

    ''' <summary>
    ''' This attribute class is solely intended to simulate and therefore preserve the 
    ''' 'System.Runtime.Versioning.SupportedOSPlatformAttribute' attribute class when migrating projects to .NET Core.
    ''' <para></para>
    ''' This attribute class marks APIs that are supported for a specified platform or operating system. 
    ''' If a version is specified, the API cannot be called from an earlier version.
    ''' <para></para>
    ''' Multiple attributes can be applied to indicate support for multiple platforms or operating systems.
    ''' </summary>
    '''
    ''' <remarks>
    ''' For more information, 
    ''' see <see href="https://learn.microsoft.com/en-us/dotnet/api/system.runtime.versioning.supportedosplatformattribute">
    ''' SupportedOSPlatformAttribute Class</see>.
    ''' </remarks>
    '''
    ''' <seealso cref="Attribute"/>
    <AttributeUsage(AttributeTargets.Assembly Or
                    AttributeTargets.Class Or
                    AttributeTargets.Constructor Or
                    AttributeTargets.Enum Or
                    AttributeTargets.Event Or
                    AttributeTargets.Field Or
                    AttributeTargets.Interface Or
                    AttributeTargets.Method Or
                    AttributeTargets.Module Or
                    AttributeTargets.Property Or
                    AttributeTargets.Struct,
    AllowMultiple:=True, Inherited:=False)>
    Friend NotInheritable Class SupportedOSPlatformAttribute : Inherits Attribute

        ''' <summary>
        ''' Gets the supported OS platform name that this attribute applies to, 
        ''' optionally including a version (eg. "windows7.0").
        ''' </summary>
        Friend ReadOnly Property PlatformName As String

        ''' <summary>
        ''' Initializes a new instance of the <see cref="SupportedOSPlatformAttribute"/> attribute class 
        ''' for the specified supported OS platform (eg. "windows7.0").
        ''' </summary>
        '''
        ''' <param name="platformName">
        ''' The supported OS platform name that this attribute applies to, 
        ''' optionally including a version (eg. "windows7.0").
        ''' </param>
        Public Sub New(platformName As String)
            Me.PlatformName = platformName
        End Sub

    End Class

End Namespace

#End If
