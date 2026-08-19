#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Globalization
Imports System.Text.Json
Imports System.Text.Json.Serialization
Imports System.Threading

Imports Ollamantis.Entities

#End Region

Namespace Core

#Region " JsonObjectBase "

    ''' <summary>
    ''' Provides a base implementation for contracts or entities that can be serialized to JSON.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <Browsable(True)>
    <ImmutableObject(False)> ' Inherited = True
    <DebuggerStepThrough>    ' Inherited = False
    Public MustInherit Class JsonObjectBase : Implements IEquatable(Of JsonObjectBase)

#Region " Private Fields "

        ''' <summary>
        ''' A cached, thread-safe instance of <see cref="JsonSerializerOptions"/> configured for compact (non-indented) JSON output.
        ''' </summary>
        Friend Shared ReadOnly JsonOptionsCompact As New Lazy(Of JsonSerializerOptions)(
            Function() JsonObjectBase.CreateJsonOptions(indented:=False))

        ''' <summary>
        ''' A cached, thread-safe instance of <see cref="JsonSerializerOptions"/> configured for indented (pretty-printed) JSON output.
        ''' </summary>
        Friend Shared ReadOnly JsonOptionsIndented As New Lazy(Of JsonSerializerOptions)(
            Function() JsonObjectBase.CreateJsonOptions(indented:=True))

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="JsonObjectBase"/> class.
        ''' </summary>
        Public Sub New()
            MyBase.New()
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

            Return Me.Equals(TryCast(obj, JsonObjectBase))
        End Function

        ''' <summary>
        ''' Determines whether the specified <see cref="JsonObjectBase"/> is equal to the current instance 
        ''' by performing a deep equality check on their serialized JSON representations.
        ''' </summary>
        ''' 
        ''' <param name="other">
        ''' The <see cref="JsonObjectBase"/> to compare with the current instance.
        ''' </param>
        ''' 
        ''' <returns>
        ''' <see langword="True"/> if the specified object is equal to the current instance; 
        ''' otherwise, <see langword="False"/>.
        ''' </returns>
        Public Overloads Function Equals(other As JsonObjectBase) As Boolean Implements IEquatable(Of JsonObjectBase).Equals

            If other Is Nothing Then
                Return False
            End If

            If Object.ReferenceEquals(Me, other) Then
                Return True
            End If

            If Me.GetType() IsNot other.GetType() Then
                Return False
            End If

            ' Deep-equality trick for JSON models: 
            '   Compare their deterministic serialized strings. 
            '   This natively and flawlessly handles nested lists, arrays, and complex objects.
            Dim jsonLeft As String = Me.ToString(writeIndented:=False)
            Dim jsonRight As String = other.ToString(writeIndented:=False)

            Return String.Equals(jsonLeft, jsonRight, StringComparison.Ordinal)
        End Function

        ''' <summary>
        ''' Returns a hash code for this instance.
        ''' </summary>
        ''' 
        ''' <returns>
        ''' A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        ''' </returns>
        Public Overrides Function GetHashCode() As Integer

            Return Me.ToString(writeIndented:=False).GetHashCode()
        End Function

#End Region

#Region " Equality Operators "

        ''' <summary>
        ''' Determines whether two specified instances of <see cref="JsonObjectBase"/> are equal.
        ''' </summary>
        ''' 
        ''' <param name="left">
        ''' The first <see cref="JsonObjectBase"/> to compare.
        ''' </param>
        ''' 
        ''' <param name="right">
        ''' The second <see cref="JsonObjectBase"/> to compare.
        ''' </param>
        ''' 
        ''' <returns>
        ''' <see langword="True"/> if the two instances are equal; otherwise, <see langword="False"/>.
        ''' </returns>
        Public Shared Operator =(left As JsonObjectBase, right As JsonObjectBase) As Boolean

            Return (left Is Nothing AndAlso right Is Nothing) OrElse
                   (
                     left IsNot Nothing AndAlso
                     right IsNot Nothing AndAlso
                     left.Equals(right)
                   )
        End Operator

        ''' <summary>
        ''' Determines whether two specified instances of <see cref="JsonObjectBase"/> are not equal.
        ''' </summary>
        ''' 
        ''' <param name="left">
        ''' The first <see cref="JsonObjectBase"/> to compare.
        ''' </param>
        ''' 
        ''' <param name="right">
        ''' The second <see cref="JsonObjectBase"/> to compare.
        ''' </param>
        ''' 
        ''' <returns>
        ''' <see langword="True"/> if the two instances are not equal; otherwise, <see langword="False"/>.
        ''' </returns>
        Public Shared Operator <>(left As JsonObjectBase, right As JsonObjectBase) As Boolean

            Return Not (left = right)
        End Operator

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Returns a formatted JSON <see cref="String"/> that represents this instance.
        ''' </summary>
        ''' 
        ''' <param name="writeIndented">
        ''' Optional. A <see cref="Boolean"/> value indicating whether the JSON should use pretty printing, which includes: 
        ''' <para></para>
        ''' <list type="bullet">
        '''     <item><description>Indenting nested JSON tokens</description></item>
        '''     <item><description>Adding new lines</description></item>
        '''     <item><description>Adding white space between property names and values.</description></item>
        ''' </list>
        ''' <para></para>
        ''' By default, the JSON is serialized without any extra white space.
        ''' <para></para>
        ''' Set this value to <see langword="True"/> to use pretty printing; otherwise, <see langword="False"/>.
        ''' <para></para>
        ''' Default value is <see langword="False"/>.
        ''' </param>
        ''' 
        ''' <param name="cultureInfo">
        ''' Optional. A <see cref="CultureInfo"/> used to format culture-sensitive properties 
        ''' (such as numeric byte sizes or localized dates) during serialization. 
        ''' <para></para>
        ''' If a value is provided, the current thread's culture is temporarily switched to this culture 
        ''' for the duration of the serialization process. 
        ''' <para></para>
        ''' If null, the current thread's culture remains unchanged.
        ''' <para></para>
        ''' Default value is null.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A formatted JSON <see cref="String"/> that represents this instance.
        ''' </returns>
        Public Overloads Function ToString(Optional writeIndented As Boolean = False,
                                           Optional cultureInfo As CultureInfo = Nothing) As String

            Dim options As JsonSerializerOptions =
                If(writeIndented,
                   JsonObjectBase.JsonOptionsIndented.Value,
                   JsonObjectBase.JsonOptionsCompact.Value)

            ' If no culture is provided, serialize directly without altering the thread context.
            If cultureInfo Is Nothing Then
                Return JsonSerializer.Serialize(Me, Me.GetType(), options)
            End If

            ' A specific culture was provided, so we temporarily switch the thread culture.
            Dim originalCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
            Dim originalUICulture As CultureInfo = Thread.CurrentThread.CurrentUICulture

            Try
                Thread.CurrentThread.CurrentCulture = cultureInfo
                Thread.CurrentThread.CurrentUICulture = cultureInfo

                Return JsonSerializer.Serialize(Me, Me.GetType(), options)

            Finally
                ' Guarantee that the original culture is restored even if serialization fails.
                Thread.CurrentThread.CurrentCulture = originalCulture
                Thread.CurrentThread.CurrentUICulture = originalUICulture
            End Try

        End Function

#End Region

#Region " Private Methods "

        ''' <summary>
        ''' Creates and configures a new instance of <see cref="JsonSerializerOptions"/> 
        ''' with the SDK's default settings.
        ''' </summary>
        ''' 
        ''' <param name="indented">
        ''' A <see cref="Boolean"/> value indicating whether the JSON output should use pretty printing.
        ''' </param>
        ''' 
        ''' <returns>
        ''' A configured <see cref="JsonSerializerOptions"/> instance.
        ''' </returns>
        Private Shared Function CreateJsonOptions(indented As Boolean) As JsonSerializerOptions

            Dim options As New JsonSerializerOptions With {
                .WriteIndented = indented
            }

            Return options
        End Function

#End Region

    End Class

#End Region

End Namespace