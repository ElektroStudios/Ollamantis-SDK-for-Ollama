
#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Text.Json.Serialization

Imports Ollamantis.Core
Imports Ollamantis.Entities

#End Region

Namespace Contracts

#Region " GenerationRequestBase "

    ''' <summary>
    ''' Provides the base implementation for generation request contracts.
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Never)>
    <Browsable(True)>
    <DebuggerStepThrough>
    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Public MustInherit Class GenerationRequestBase : Inherits JsonObjectBase

#Region " Properties "

        ''' <summary>
        ''' Mandatory. Gets or sets the name of the model to use for the generation request (e.g., "<c>llama3.2</c>").
        ''' </summary>
        <JsonPropertyOrder(-1)> ' Forces this property to be serialized first (at top of JSON).
        <JsonPropertyName("model")>
        <DisplayName("Model")>
        <Description("The name of the model to use for the generation request (e.g., ""llama3.2"").")>
        Public Property Model As String

        ''' <summary>
        ''' Optional. Gets or sets additional model parameters for generation.
        ''' <para></para>
        ''' Default value is null (use defaults).
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("options")>
        <DisplayName("Options")>
        <Description("Optional. Additional model parameters for generation.")>
        Public Property Options As GenerationOptions

        ''' <summary>
        ''' Optional. Gets or sets a value indicating how long the model will stay loaded into memory following the request.
        ''' <para></para>
        ''' Default value is null, which defaults to "<c>5m</c>".
        ''' </summary>
        <JsonIgnore(Condition:=JsonIgnoreCondition.WhenWritingNull)>
        <JsonPropertyName("keep_alive")>
        <DisplayName("Keep Alive")>
        <Description("Optional. A value indicating how long the model will stay loaded into memory following the request. Default value is null, which defaults to ""5m"".")>
        Public Property KeepAlive As KeepAliveOption

        ''' <summary>
        ''' Gets the string to display in the debugger variable windows.
        ''' </summary>
        <Browsable(False)>
        <DebuggerBrowsable(DebuggerBrowsableState.Never)>
        Protected Overridable ReadOnly Property DebuggerDisplay As String
            Get
                Return $"Model = {Me.Model}, KeepAlive = {Me.KeepAlive}, Options = {Me.Options}"
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="GenerationRequestBase"/> class.
        ''' </summary>
        Public Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <see cref="GenerationRequestBase"/> class.
        ''' </summary>
        ''' 
        ''' <param name="model">
        ''' Mandatory. The name of the model to use for the generation request (e.g., "<c>llama3.2</c>").
        ''' </param>
        ''' 
        ''' <param name="options">
        ''' Optional. Additional model parameters for generation.
        ''' <para></para>
        ''' Default value is null (use defaults).
        ''' </param>
        ''' 
        ''' <param name="keepAlive">
        ''' Optional. A value indicating how long the model will stay loaded into memory following the request.
        ''' <para></para>
        ''' Default value is null, which defaults to "<c>5m</c>".
        ''' </param>
        Public Sub New(model As String,
                       options As GenerationOptions,
                       keepAlive As KeepAliveOption)

            Me.Model = model
            Me.Options = options
            Me.KeepAlive = keepAlive
        End Sub

#End Region

    End Class

#End Region

End Namespace
