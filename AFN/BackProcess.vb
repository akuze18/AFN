Imports System.Threading

'Este es el delegado que se encargará de hacer la magia
Public Delegate Sub ProcesoSecundario(datos As DataTable)

Public Class BackProcess

    Private base As New base_AFN

#Region "Variables de Clase"

    Private _resultado As DataTable
    Private _hilo As Thread
    'Para medir los tiempos en segundos
    Private _tTotal, _tUpdate, _TActual As Integer

    Private _fecha1, _fecha2 As DateTime
    Private _fechaText1 As String
    Private _texto1, _texto2, _texto3 As String
    Private _periodo1 As Vperiodo
    Private _moneda1 As base_AFN.BMoneda
    Private _ambiente1 As base_AFN.BAmbiente

    Private _reporte_datos As form_reporte_dato

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Se utiliza para iniciar el proceso en segundo plano, con los tiempos estimados de ejecución
    ''' </summary>
    ''' <param name="tTotal">Tiempo estimado total que se espera durará todo el proceso. Se mide en segundos</param>
    ''' <param name="tUpdate">Tiempo que indica cada cuanto deberá actualizar la barra de progreso. Se mide en segundos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tTotal As Integer, ByVal tUpdate As Integer)
        _tTotal = tTotal
        _tUpdate = tUpdate
        _TActual = 0
    End Sub
#End Region

#Region "Proceso en segundo plano"
    Private Event capturar As ProcesoSecundario
    Private Sub captura_res(ByVal datos As DataTable)
        _resultado = datos
    End Sub
#End Region

#Region "Salidas y Actualizaciones"
    Public Function resultado() As DataTable
        Return _resultado
    End Function
    Public ReadOnly Property tiempo_total As Integer
        Get
            Return _tTotal
        End Get
    End Property
    Public ReadOnly Property tiempo_cambio As Integer
        Get
            Return _tUpdate
        End Get
    End Property
    Public ReadOnly Property intervalos As Integer
        Get
            Return Math.Ceiling(_tTotal / _tUpdate)
        End Get
    End Property
    Public ReadOnly Property isWorking As Boolean
        Get
            Return _hilo.ThreadState = ThreadState.Running
        End Get
    End Property
    Public Sub Keep()
        Thread.Sleep(1000)
        My.Application.DoEvents()
        _TActual = _TActual + 1
    End Sub
    Public Function UpdateBar() As Boolean
        Return _TActual Mod _tUpdate = 0
    End Function
#End Region

#Region "Form_toma_inventario"
    Public Sub ini_detalle_inventario(ByVal fecha As String, ByVal clase As String, ByVal zona As String)
        _fechaText1 = fecha
        _texto1 = clase
        _texto2 = zona

        'Agregamos el handler del evento (si no lo hacemos no podremos interceptarlo)
        AddHandler capturar, New ProcesoSecundario(AddressOf captura_res)
        'Creamos un delegado para el método ImprimirSuma()
        Dim ts As ThreadStart = New ThreadStart(AddressOf exe_detalle_inventario)
        'Creamos un hilo para ejecutar el delegado...
        _hilo = New Thread(ts)
        'Iniciamos la ejecucion del nuevo hilo
        _hilo.Start()
    End Sub
    Private Sub exe_detalle_inventario()
        Dim tabla As DataTable
        tabla = base.detalle_inventario(_fechaText1, _texto1, _texto2)
        RaiseEvent capturar(tabla)
    End Sub
#End Region
#Region "Form_saldo_obc"
    Public Sub ini_salidas_obc(ByVal fecha_ini As DateTime, ByVal fecha_fin As DateTime, ByVal currency As String)
        _fecha1 = fecha_ini
        _fecha2 = fecha_fin
        _texto1 = currency

        'Agregamos el handler del evento (si no lo hacemos no podremos interceptarlo)
        AddHandler capturar, New ProcesoSecundario(AddressOf captura_res)
        'Creamos un delegado para el método ImprimirSuma()
        Dim ts As ThreadStart = New ThreadStart(AddressOf exe_salidas_obc)
        'Creamos un hilo para ejecutar el delegado...
        _hilo = New Thread(ts)
        'Iniciamos la ejecucion del nuevo hilo
        _hilo.Start()
    End Sub
    Private Sub exe_salidas_obc()
        Dim tabla As DataTable
        tabla = base.salidas_obc(_fecha1, _fecha2, _texto1)
        RaiseEvent capturar(tabla)
    End Sub
#End Region
#Region "form_reporte"
    Public Sub ini_REPORTE_VIG_INV_DET(ByVal periodo As Vperiodo, ByVal moneda As base_AFN.BMoneda, ByVal zona As String, ByVal clase As String)
        _periodo1 = periodo
        _moneda1 = moneda
        _texto1 = zona
        _texto2 = clase

        'Agregamos el handler del evento (si no lo hacemos no podremos interceptarlo)
        AddHandler capturar, New ProcesoSecundario(AddressOf captura_res)
        'Creamos un delegado para el método ImprimirSuma()
        Dim ts As ThreadStart = New ThreadStart(AddressOf exe_REPORTE_VIG_INV_DET)
        'Creamos un hilo para ejecutar el delegado...
        _hilo = New Thread(ts)
        'Iniciamos la ejecucion del nuevo hilo
        _hilo.Start()
    End Sub
    Private Sub exe_REPORTE_VIG_INV_DET()
        Dim tabla As DataTable
        tabla = base.REPORTE_VIG_INV_DET(_periodo1, _moneda1, _texto1, _texto2)
        RaiseEvent capturar(tabla)
    End Sub

    Public Sub ini_REPORTE_VIG_IFRS_DET(ByVal periodo As Vperiodo, ByVal moneda As base_AFN.BMoneda, ByVal zona As String, ByVal clase As String)
        _periodo1 = periodo
        _moneda1 = moneda
        _texto1 = zona
        _texto2 = clase

        'Agregamos el handler del evento (si no lo hacemos no podremos interceptarlo)
        AddHandler capturar, New ProcesoSecundario(AddressOf captura_res)
        'Creamos un delegado para el método ImprimirSuma()
        Dim ts As ThreadStart = New ThreadStart(AddressOf exe_REPORTE_VIG_IFRS_DET)
        'Creamos un hilo para ejecutar el delegado...
        _hilo = New Thread(ts)
        'Iniciamos la ejecucion del nuevo hilo
        _hilo.Start()
    End Sub
    Private Sub exe_REPORTE_VIG_IFRS_DET()
        Dim tabla As DataTable
        tabla = base.REPORTE_VIG_IFRS_DET(_periodo1, _moneda1, _texto1, _texto2)
        RaiseEvent capturar(tabla)
    End Sub

    Public Sub ini_REPORTE_VIG_CONT_DET(ByVal periodo As Vperiodo, ByVal ambiente As base_AFN.BAmbiente, ByVal moneda As base_AFN.BMoneda, ByVal zona As String, ByVal clase As String)
        _ambiente1 = ambiente
        _periodo1 = periodo
        _moneda1 = moneda
        _texto1 = zona
        _texto2 = clase

        'Agregamos el handler del evento (si no lo hacemos no podremos interceptarlo)
        AddHandler capturar, New ProcesoSecundario(AddressOf captura_res)
        'Creamos un delegado para el método ImprimirSuma()
        Dim ts As ThreadStart = New ThreadStart(AddressOf exe_REPORTE_VIG_CONT_DET)
        'Creamos un hilo para ejecutar el delegado...
        _hilo = New Thread(ts)
        'Iniciamos la ejecucion del nuevo hilo
        _hilo.Start()
    End Sub
    Private Sub exe_REPORTE_VIG_CONT_DET()
        Dim tabla As DataTable
        tabla = base.REPORTE_VIG_CONT_DET(_periodo1, _moneda1, _ambiente1, _texto1, _texto2)
        RaiseEvent capturar(tabla)
    End Sub

    Public Sub ini_REPORTE_VIG_RESUMEN(ByVal dato As form_reporte_dato)
        _reporte_datos = dato

        'Agregamos el handler del evento (si no lo hacemos no podremos interceptarlo)
        AddHandler capturar, New ProcesoSecundario(AddressOf captura_res)
        'Creamos un delegado para el método ImprimirSuma()
        Dim ts As ThreadStart = New ThreadStart(AddressOf exe_REPORTE_VIG_RESUMEN)
        'Creamos un hilo para ejecutar el delegado...
        _hilo = New Thread(ts)
        'Iniciamos la ejecucion del nuevo hilo
        _hilo.Start()
    End Sub
    Private Sub exe_REPORTE_VIG_RESUMEN()
        Dim tabla As DataTable
        tabla = base.REPORTE_VIG_RESUMEN(_reporte_datos)
        RaiseEvent capturar(tabla)
    End Sub
#End Region

End Class
