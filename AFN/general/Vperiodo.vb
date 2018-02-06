Imports System.Globalization
Imports System.Threading

Public Class Vperiodo

#Region "Variables de la clase"
    ''' <summary>
    ''' La clase TextInfo permite utilizar funciones de manejo de texto contextualizadas en la localizacion
    ''' </summary>
    ''' <remarks></remarks>
    Private tInfo As TextInfo
    ''' <summary>
    ''' Fecha con la que inicia el periodo
    ''' </summary>
    ''' <remarks></remarks>
    Private fecha_inicio As DateTime
    ''' <summary>
    ''' Fecha con la que termina el periodo
    ''' </summary>
    ''' <remarks></remarks>
    Private fecha_final As DateTime
#End Region

#Region "Constructor"

    Public Sub New(ByVal año As Integer, ByVal mes As Integer)
        fecha_inicio = DateSerial(año, mes, 1)
        fecha_final = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, fecha_inicio))
        'Get culture information from current thread.
        Dim curCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
        'Create TextInfo object.
        tInfo = curCulture.TextInfo()
    End Sub

#End Region

#Region "Propiedades y métodos"

    ''' <summary>
    ''' Visualiza el periodo con su formato numerico año - mes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        Return (fecha_inicio.Year * 100 + fecha_inicio.Month).ToString
    End Function

    ''' <summary>
    ''' Indica el primer día del periodo como fecha
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property first As DateTime
        Get
            Return fecha_inicio
        End Get
    End Property
    ''' <summary>
    ''' Indica el último día del periodo como fecha
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property last As DateTime
        Get
            Return fecha_final
        End Get
    End Property
    ''' <summary>
    ''' Indica el último día del periodo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property dia_final As Integer
        Get
            Return fecha_final.Day
        End Get
    End Property
    ''' <summary>
    ''' Visualiza el periodo con su formato escrito año - mes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property mostrar As String
        Get
            Return tInfo.ToTitleCase(fecha_inicio.ToString("yyyy MMMM"))
        End Get
    End Property
    ''' <summary>
    ''' Indica el primer día del periodo en el formato de Base de Datos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property firstDB As String
        Get
            Return fecha_inicio.ToString("yyyyMMdd")
        End Get
    End Property
    ''' <summary>
    ''' Indica el último día del periodo en el formato de Base de Datos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property lastDB As String
        Get
            Return fecha_final.ToString("yyyyMMdd")
        End Get
    End Property

#End Region

#Region "Operadores"

    Public Shared Operator =(ByVal p1 As Vperiodo, ByVal p2 As Vperiodo) As Boolean
        If p1.ToString = p2.ToString Then
            Return True
        Else
            Return False
        End If
    End Operator
    Public Shared Operator <>(ByVal p1 As Vperiodo, ByVal p2 As Vperiodo) As Boolean
        If p1.ToString <> p2.ToString Then
            Return True
        Else
            Return False
        End If
    End Operator

    Public Shared Operator -(ByVal p1 As Vperiodo, ByVal agrega As Integer) As Vperiodo
        Dim nuevo As DateTime
        nuevo = DateAdd(DateInterval.Month, -agrega, p1.first)
        Return New Vperiodo(nuevo.Year, nuevo.Month)
    End Operator
    Public Shared Operator +(ByVal p1 As Vperiodo, ByVal agrega As Integer) As Vperiodo
        Dim nuevo As DateTime
        nuevo = DateAdd(DateInterval.Month, agrega, p1.first)
        Return New Vperiodo(nuevo.Year, nuevo.Month)
    End Operator

    Public Shared Operator -(ByVal p1 As Vperiodo, ByVal p2 As Vperiodo) As Integer
        If p1.first > p2.first Then
            Return DateDiff(DateInterval.Month, p2.first, p1.first)
        Else
            Return DateDiff(DateInterval.Month, p1.first, p2.first)
        End If
    End Operator

#End Region

End Class
