Imports System.IO

Public Class master_control

#Region "Variables de control de la clase"
    ''' <summary>
    ''' Representa si no se produjeron errores al momento de inicializar la clase
    ''' </summary>
    ''' <remarks>Se establece como resultado del constructor de la clase</remarks>
    Dim var_state As Boolean
    ''' <summary>
    ''' Si se produjera un error al inicializar la clase, el mensaje de error se enseña en esta variable
    ''' </summary>
    ''' <remarks>Si no hay error, esta variable retorna un string vacio</remarks>
    Dim var_state_text As String
    ''' <summary>
    ''' Se utiliza como clave para la encriptación de los valores en el INI
    ''' </summary>
    ''' <remarks>Es recomendable cambiarlo en cada aplicación</remarks>
    Dim clave As String = "activo_fijo"
    ''' <summary>
    ''' Representa la ruta de la aplicación en que es llamado el control
    ''' </summary>
    ''' <remarks>Se obtiene en el constructor de la clase</remarks>
    Dim Path_INI As String
    ''' <summary>
    ''' Instancia la clase que permite manipular un archivo INI
    ''' </summary>
    ''' <remarks></remarks>
    Private mINI As New cIniArray

    Private _ruta_base As String
#End Region

#Region "Funciones privadas"

    ''' <summary>
    ''' Lee un dato del archivo INI. Recibe la sección y la clave a leer
    ''' </summary>
    ''' <param name="ThemeTag">Sección dentro del archivo INI</param>
    ''' <param name="Key">Clave dentro del archivo INI</param>
    ''' <returns>El valor dentro del archivo sin encriptar</returns>
    ''' <remarks></remarks>
    Private Function Leer_Ini(ByVal ThemeTag As String, ByVal Key As String) As String
        Dim Value As String
        Value = mINI.IniGet(Path_INI, ThemeTag, Key)
        If Value.Length > 0 Then
            Return EncryptString(clave, Value, EncrypAction.DECRYPT)
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Escribe un dato encriptado en el INI. Recibe la seccion, la clave a escribir y el valor a añadir en dicha clave  
    ''' </summary>
    ''' <param name="ThemeTag">Sección dentro del archivo INI</param>
    ''' <param name="Key">Clave dentro del archivo INI</param>
    ''' <param name="Valor">Valor para almacenar en la clave</param>
    ''' <returns>La misma clave almacenada sin encriptar</returns>
    ''' <remarks></remarks>
    Private Function Grabar_Ini(ByVal ThemeTag As String, ByVal Key As String, ByVal Valor As String) As String
        Dim newValor As String
        newValor = EncryptString(clave, Valor, EncrypAction.ENCRYPT)
        mINI.IniWrite(Path_INI, ThemeTag, Key, newValor)
        Return Valor
    End Function

    Private Function valida(ByVal cadena As String) As Boolean
        Return Not String.IsNullOrEmpty(cadena)
    End Function
#End Region

#Region "Constructor"
    Public Sub New()

        Dim var_servidor As String
        Dim var_base_dato As String
        Dim var_usuario As String
        Dim var_password As String
        _ruta_base = System.AppDomain.CurrentDomain.BaseDirectory()
        Path_INI = _ruta_base + "config.ini"
        Try
            If File.Exists(Path_INI) Then
                'Obtengo la información primordial desde el archivo de configuración
                var_servidor = Leer_Ini("CONEXION", "SERVIDOR")
                var_base_dato = Leer_Ini("CONEXION", "BASE_DATO")
                var_usuario = Leer_Ini("CONEXION", "USUARIO")
                var_password = Leer_Ini("CONEXION", "PASSWORD")
            Else
                'Creo un archivo nuevo, con la información primordial por defecto
                var_servidor = Grabar_Ini("CONEXION", "SERVIDOR", "SANTIAGO")
                var_base_dato = Grabar_Ini("CONEXION", "BASE_DATO", "NIPPO")
                var_usuario = Grabar_Ini("CONEXION", "USUARIO", "sa")
                var_password = Grabar_Ini("CONEXION", "PASSWORD", "nippon2006")
            End If
        Catch oe As Exception
            var_state = False
            var_state_text = oe.Message
            Exit Sub
        End Try
        'VALIDAR QUE TODAS LAS VARIABLES PRIMORDIALES FUERON ESTABLECIDAS
        If valida(var_servidor) And valida(var_base_dato) And valida(var_usuario) And valida(var_password) Then
            var_state = True
            var_state_text = ""
        Else
            var_state = False
            var_state_text = "La configuración básica esta incompleta"
        End If
    End Sub
#End Region

#Region "Salidas de configuraciones"
    Public Property servidor As String
        Get
            Return Leer_Ini("CONEXION", "SERVIDOR")
        End Get
        Set(value As String)
            Grabar_Ini("CONEXION", "SERVIDOR", value)
        End Set
    End Property
    Public Property base_dato As String
        Get
            Return Leer_Ini("CONEXION", "BASE_DATO")
        End Get
        Set(value As String)
            Grabar_Ini("CONEXION", "BASE_DATO", value)
        End Set
    End Property
    Public Property usuario As String
        Get
            Return Leer_Ini("CONEXION", "USUARIO")
        End Get
        Set(value As String)
            Grabar_Ini("CONEXION", "USUARIO", value)
        End Set
    End Property
    Public Property password As String
        Get
            Return Leer_Ini("CONEXION", "PASSWORD")
        End Get
        Set(value As String)
            Grabar_Ini("CONEXION", "PASSWORD", value)
        End Set
    End Property

    Public ReadOnly Property rutaApp As String
        Get
            Return _ruta_base
        End Get
    End Property
#End Region

#Region "Salidas de configuraciones dinamicas"
    Public Property vars(ByVal seccion As String, ByVal clave As String) As String
        Get
            Dim value As String
            Try
                value = Leer_Ini(seccion, clave)
            Catch eo As Exception
                value = "EMPTY KEY"
            End Try
            Return value
        End Get
        Set(value As String)
            Grabar_Ini(seccion, clave, value)
        End Set
    End Property

    Public ReadOnly Property sections As String()
        Get
            Return mINI.IniGetSections(Path_INI)
        End Get
    End Property

    Public ReadOnly Property keys(ByVal section As String) As String()
        Get
            Dim data_ini As String()
            Dim llave() As String
            data_ini = mINI.IniGetSection(Path_INI, section)
            ReDim llave(data_ini.Length / 2 - 1)
            For i = 0 To data_ini.Length - 1 Step 2
                llave(i / 2) = data_ini(i)
            Next
            Return llave
        End Get
    End Property
#End Region

#Region "Manejo de la conexion"

    Public Function ejecuta(ByVal txt_sql As String) As DataTable
        Dim conex = New coneccion(base_dato, servidor, usuario, password)
        Dim tabla As DataTable
        conex.conectar()
        tabla = conex.ejecuta(txt_sql)
        conex.cerrar()
        Return tabla
    End Function

    Public Function Dval(ByVal campo As String, ByVal origen As String, Optional ByVal criterio As String = "") As String
        Dim conex = New coneccion(base_dato, servidor, usuario, password)
        Dim valor As String
        conex.conectar()
        valor = conex.Dval(campo, origen, criterio)
        conex.cerrar()
        Return valor
    End Function
    Public Function Dcount(ByVal campo As String, ByVal origen As String, Optional ByVal criterio As String = "") As Integer
        Dim conex = New coneccion(base_dato, servidor, usuario, password)
        Dim valor As Integer
        conex.conectar()
        valor = conex.Dcount(campo, origen, criterio)
        conex.cerrar()
        Return valor
    End Function
    Public Function execute(ByVal txt_sql As String) As DataTable()
        Dim conex = New coneccion(base_dato, servidor, usuario, password)
        Dim tabla As DataTable()
        conex.conectar()
        tabla = conex.execute(txt_sql)
        conex.cerrar()
        Return tabla
    End Function

#End Region

#Region "Estado de la instancia"
    Public ReadOnly Property estado As Boolean
        Get
            Return var_state
        End Get
    End Property
    Public ReadOnly Property estado_desc As String
        Get
            Return var_state_text
        End Get
    End Property
#End Region

End Class