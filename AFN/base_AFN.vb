''' <summary>
''' Esta clase se diseña para condensar todas las consultas SQL a la base y otros datos de configuraciones, 
''' de forma que los formularios unicamente solicitan los datos puntuales o grupos de datos (datatable) que 
''' se requiera para visualizar,además de contener la logica para realizar insert, update y delete cuando sea necesario
''' </summary>
''' <remarks></remarks>
Public Class base_AFN

#Region "Enumeracion"
    Public Enum BMoneda
        CLP = 1
        YEN = 2
        MIX = 3
    End Enum

    Public Enum BAmbiente
        FIN = 1
        TRIB = 2
        IFRS = 3
    End Enum
#End Region

#Region "Variables de la clase"
    ''' <summary>
    ''' Inicializo la conexion a la base de datos y el archivo INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Public maestro As master_control

    Private _colchon As DataTable
    Private _txt_sql As String

    Private _proyecto As String
    Private _ruta_server As String
#End Region

#Region "Propiedades de configuracion"
    Public ReadOnly Property AFNok As Color
        Get
            Return ColorTranslator.FromOle(RGB(125, 255, 125))
        End Get
    End Property
    Public ReadOnly Property AFNfail As Color
        Get
            Return ColorTranslator.FromOle(RGB(255, 50, 50))
        End Get
    End Property
    Public ReadOnly Property AFNprocess As Color
        Get
            Return ColorTranslator.FromOle(RGB(255, 255, 125))
        End Get
    End Property

    Public ReadOnly Property servidor As String
        Get
            Return maestro.servidor
        End Get
    End Property
    Public ReadOnly Property base_dato As String
        Get
            Return maestro.base_dato
        End Get
    End Property
#End Region

#Region "Directorio de la App"
    Public ReadOnly Property rutaApp As String
        Get
            Return maestro.rutaApp
        End Get
    End Property
    Public ReadOnly Property rutaSrv As String
        Get
            Return _ruta_server
        End Get
    End Property
    Public ReadOnly Property dirAll As String()
        Get
            Return New String() {dirArchivos, dirError, dirFotos}
        End Get
    End Property
    Public ReadOnly Property dirArchivos As String
        Get
            Return rutaApp + "files\"
        End Get
    End Property
    Public ReadOnly Property dirError As String
        Get
            Return rutaApp + "log_error\"
        End Get
    End Property
    Public ReadOnly Property dirFotos As String
        Get
            Return rutaSrv + "fotos_AF\"
        End Get
    End Property

    Public ReadOnly Property fileLogo As String
        Get
            Return dirArchivos + "logo_nippon.jpg"
        End Get
    End Property
    Public ReadOnly Property fileFontBarcode As String
        Get
            Return dirArchivos + "FRE3OF9X.TTF"
        End Get
    End Property
    Public ReadOnly Property fileFontLabel As String
        Get
            Return dirArchivos + "BrowalliaUPC.TTF"
        End Get
    End Property
#End Region

#Region "Constructor"
    Public Sub New()
        maestro = New master_control
        _proyecto = "Activo Fijo NH FOODS CHILE"
        _ruta_server = "\\tokio\E\PROGRAMAS\Activo_Fijo_Programa\"
    End Sub
#End Region

#Region "Mensajes"
    Public Sub mensaje_alerta(ByVal mensaje As String)
        MessageBox.Show(mensaje, _proyecto, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub
#End Region

#Region "Tablas para mostrar en Grillas y Combos"
    Public Function lista_cambio() As DataTable
        Dim recordset As New DataTable
        recordset.Columns.Add(" ")
        recordset.Columns.Add("Código Artículo")
        recordset.Columns.Add("Fecha Traspaso", System.Type.GetType("System.DateTime"))
        recordset.Columns.Add("Cantidad Traspaso")
        recordset.Columns.Add("Zona Destino")
        recordset.Columns.Add("SubZona Destino")
        recordset.Columns.Add("rowindx")
        recordset.Columns.Add("Descripcion Artículo")
        recordset.Columns.Add("parte")
        Return recordset

    End Function
    Public Function lista_articulo() As DataTable
        Dim tabla_result = New DataTable
        tabla_result.Columns.Add("Artículo", System.Type.GetType("System.Int32"))
        tabla_result.Columns.Add("Cantidad", System.Type.GetType("System.Int32"))
        tabla_result.Columns.Add("Zona", System.Type.GetType("System.String"))
        tabla_result.Columns.Add("Descripción Artículo", System.Type.GetType("System.String"))
        tabla_result.Columns.Add("rowindx", System.Type.GetType("System.Int32"))
        tabla_result.Columns.Add("parte", System.Type.GetType("System.Int32"))
        tabla_result.Columns.Add("estado", System.Type.GetType("System.String"))
        Return tabla_result
    End Function
    Public Function lista_det_articulo(ByVal id_articulo As Integer, ByVal parte As Integer, ByVal cantidad As Integer) As DataTable
        Dim colchon As DataTable
        Dim sql_lista As String
        sql_lista = "SELECT producto [Codigo Producto],codigo_old[Codigo Antiguo], cast(0 as bit) [Procesar]  FROM AFN_EXISTENCIA WHERE lote_articulo=" + id_articulo.ToString + _
            " AND parte=" + parte.ToString
        colchon = maestro.ejecuta(sql_lista)
        'Compruebo las cantidades
        If colchon.Rows.Count <= cantidad Then
            For Each fila As DataRow In colchon.Rows
                fila(2) = 1     'columna Procesar
            Next
        Else
            For i = 0 To cantidad - 1
                colchon.Rows(i).Item(2) = 1
            Next
        End If
        Return colchon
    End Function
    Public Function lista_det_total_art() As DataTable
        Dim colchon As New DataTable
        colchon.Columns.Add("producto", Type.GetType("System.String"))
        colchon.Columns.Add("lote", Type.GetType("System.Int32"))
        colchon.Columns.Add("parte", Type.GetType("System.Int32"))
        colchon.Columns.Add("procesar", Type.GetType("System.Byte"))
        colchon.Columns.Add("row_id", Type.GetType("System.Int32"))
        Return colchon
    End Function
    Public Function lista_resultado_inv() As DataTable
        Dim colchon As New DataTable
        colchon.Columns.Add("Zona", Type.GetType("System.String"))
        colchon.Columns.Add("Clase", Type.GetType("System.String"))
        colchon.Columns.Add("Producto", Type.GetType("System.String"))
        colchon.Columns.Add("Lote Articulos")
        colchon.Columns.Add("Descripcion")
        colchon.Columns.Add("Fecha Compra", Type.GetType("System.DateTime"))
        colchon.Columns.Add("Valor Compra", Type.GetType("System.Int32"))
        colchon.Columns.Add("Valor Libro", Type.GetType("System.Int32"))
        colchon.Columns.Add("Estado Inventario")
        colchon.Columns.Add("Subzona Previa")
        colchon.Columns.Add("Subzona Actual")
        colchon.Columns.Add("Ubicacion Previa")
        colchon.Columns.Add("Ubicacion Actual")
        colchon.Columns.Add("parte", Type.GetType("System.Int32"))     'Columna Invisible
        colchon.Columns.Add("cantidad_lote", Type.GetType("System.Int32"))     'Columna Invisible
        colchon.Columns.Add("estado_tomado", Type.GetType("System.Int32"))     'Columna Invisible
        colchon.Columns.Add("subzona_tomada", Type.GetType("System.Int32"))     'Columna Invisible
        colchon.Columns.Add("ubicacion_tomada", Type.GetType("System.Int32"))     'Columna Invisible
        colchon.Columns.Add("cod_ubic", Type.GetType("System.Int32"))     'Columna Invisible
        colchon.Columns.Add("cod_subzona", Type.GetType("System.Int32"))     'Columna Invisible
        Return colchon
    End Function
    Public Function lista_resultado_inv(ByVal datos As DataTable) As DataTable
        Dim colchon As DataTable
        colchon = lista_resultado_inv() 'Para obtener las cabeceras de la grilla
        For Each fila As DataRow In datos.Rows
            Dim main_fila As DataRow = colchon.NewRow
            main_fila(0) = fila("zona")
            main_fila(1) = fila("clase")
            main_fila(2) = fila("producto")
            main_fila(3) = fila("lote_articulo")
            main_fila(4) = fila("descripcion")
            main_fila(5) = fila("fecha_compra")
            main_fila(6) = fila("valor_inicial")
            main_fila(7) = fila("val_libro")
            main_fila(8) = fila("txt_estado")
            main_fila(9) = fila("txt_subzona")
            main_fila(10) = fila("txt_subzona_actual")
            main_fila(11) = fila("txt_ubic")
            main_fila(12) = fila("txt_ubic_actual")
            main_fila(13) = fila("parte")
            main_fila(14) = fila("cantidad_lote")
            main_fila(15) = fila("estado_tomado")
            main_fila(16) = fila("subzona_tomada")
            main_fila(17) = fila("ubicacion_tomada")
            main_fila(18) = fila("cod_ubic")
            main_fila(19) = fila("cod_subzona")
            colchon.Rows.Add(main_fila)
            Application.DoEvents()
        Next
        Return colchon
    End Function
    Public Function cabecera_resultado_inv() As DataGridViewColumn()
        Dim proporcion As Double
        Dim col01, col02, col03, col04, col05, col06, col07, col08, col14, col15 As New DataGridViewTextBoxColumn
        Dim col09, col10, col11, col12, col13 As New DataGridViewComboBoxColumn
        Dim col16 As New DataGridViewCheckBoxColumn
        proporcion = 0.75
        With col01
            .Name = "Zona"
            .Visible = True
            .ReadOnly = True
            .Width = 50 * 0.75
        End With
        With col02
            .Name = "Clase"
            .Visible = True
            .ReadOnly = True
            .Width = 50 * 0.75
        End With
        With col03
            .Name = "Producto"
            .Visible = True
            .ReadOnly = True
            .Width = 150 * proporcion
        End With
        With col04
            .Name = "Lote Articulos"
            .Visible = True
            .ReadOnly = True
            .Width = 85 * proporcion
        End With
        With col05
            .Name = "Descripcion"
            .Visible = True
            .ReadOnly = True
            .Width = 526 * proporcion
        End With
        With col06
            .Name = "Fecha Compra"
            .Visible = True
            .ReadOnly = True
            .Width = 100 * proporcion
        End With
        With col07
            .Name = "Valor Compra"
            .Visible = True
            .ReadOnly = True
            .Width = 125 * proporcion
            .DefaultCellStyle.Format = "N0"
        End With
        With col08
            .Name = "Valor Libro"
            .Visible = True
            .ReadOnly = True
            .Width = 125 * proporcion
            .DefaultCellStyle.Format = "N0"
        End With
        With col09
            .Name = "Estado Inventario"
            .DataSource = estado_existencia()
            .DisplayMember = .DataSource.Columns(1).ColumnName
            .ValueMember = .DataSource.Columns(0).ColumnName
            .Visible = True
            .ReadOnly = False
            .Width = 159 * proporcion
        End With
        With col10
            .Name = "Subzona Previa"
            .DataSource = SUBZONAS_ACT()
            .DisplayMember = .DataSource.Columns(1).ColumnName
            .ValueMember = .DataSource.Columns(0).ColumnName
            .Visible = True
            .ReadOnly = True
            .Width = 135 * proporcion
        End With
        With col11
            .Name = "Subzona Actual"
            .DataSource = SUBZONAS_ACT()
            .DisplayMember = .DataSource.Columns(1).ColumnName
            .ValueMember = .DataSource.Columns(0).ColumnName
            .Visible = True
            .ReadOnly = False
            .Width = 135 * proporcion
        End With
        With col12
            .Name = "Ubicacion Previa"
            .DataSource = ubicacion()
            .DisplayMember = .DataSource.Columns(1).ColumnName
            .ValueMember = .DataSource.Columns(0).ColumnName
            .Visible = True
            .ReadOnly = True
            .Width = 270 * proporcion
        End With
        With col13
            .Name = "Ubicacion Actual"
            .DataSource = ubicacion()
            .DisplayMember = .DataSource.Columns(1).ColumnName
            .ValueMember = .DataSource.Columns(0).ColumnName
            .Visible = True
            .ReadOnly = False
            .Width = 270 * proporcion
        End With
        With col14
            .Name = "parte"
            .Visible = False
            .ReadOnly = True
            .Width = 0
            '.DefaultCellStyle.Format = "N0"
        End With
        With col15
            .Name = "cantidad_lote"
            .Visible = False
            .ReadOnly = True
            .Width = 0
            '.DefaultCellStyle.Format = "N0"
        End With
        With col16
            .Name = "Aplicado"
            .Visible = True
            .ReadOnly = True
            .Width = 70 * 0.75
        End With
        Return New DataGridViewColumn() {col01, col02, col03, col04, col05, col06, col07, col08, col09, col10, col11, col12, col13, col14, col15, col16}
    End Function
    Public Sub detalle_resultado_inv(ByVal datos As DataTable, ByRef grilla As DataGridView)
        grilla.Rows.Clear()
        For Each fila As DataRow In datos.Rows
            Dim indice_fila As Integer
            'agrego filas
            grilla.Rows.Add()
            indice_fila = grilla.Rows.Count - 1
            'agrego los datos
            grilla.Item(0, indice_fila).Value = fila("zona")
            grilla.Item(1, indice_fila).Value = fila("clase")
            grilla.Item(2, indice_fila).Value = fila("producto")
            grilla.Item(3, indice_fila).Value = fila("lote_articulo")
            grilla.Item(4, indice_fila).Value = fila("descripcion")
            grilla.Item(5, indice_fila).Value = fila("fecha_compra")
            grilla.Item(6, indice_fila).Value = fila("valor_inicial")
            grilla.Item(7, indice_fila).Value = fila("val_libro")
            grilla.Item(8, indice_fila).Value = fila("estado_tomado")
            grilla.Item(9, indice_fila).Value = fila("cod_subzona")
            grilla.Item(10, indice_fila).Value = fila("subzona_tomada")
            grilla.Item(11, indice_fila).Value = fila("cod_ubic")
            grilla.Item(12, indice_fila).Value = fila("ubicacion_tomada")
            grilla.Item(13, indice_fila).Value = fila("parte")
            grilla.Item(14, indice_fila).Value = fila("cantidad_lote")
            grilla.Item(15, indice_fila).Value = fila("aplicado")
        Next
    End Sub
    Public Function lista_atributos_paso3() As DataTable
        Dim TGproc As New DataTable
        'agrego columnas a grilla resultado
        TGproc.Columns.Add("Código Atributo", Type.GetType("System.Int32"))
        TGproc.Columns.Add("valor guardado", Type.GetType("System.String"))
        TGproc.Columns.Add("Atributo", Type.GetType("System.String"))
        TGproc.Columns.Add("Valor del atributo", Type.GetType("System.String"))
        TGproc.Columns.Add("Mostrar", Type.GetType("System.Boolean"))
        Return TGproc
    End Function
    Public Function lista_atributos_paso4() As DataTable
        Dim TAproc As New DataTable
        TAproc.Columns.Add("Código Atributo", Type.GetType("System.Int32"))
        TAproc.Columns.Add("valor guardado", Type.GetType("System.String"))
        TAproc.Columns.Add("Artículo", Type.GetType("System.String"))
        TAproc.Columns.Add("Atributo", Type.GetType("System.String"))
        TAproc.Columns.Add("Valor del atributo", Type.GetType("System.String"))
        TAproc.Columns.Add("Mostrar", Type.GetType("System.Boolean"))
        Return TAproc
    End Function

    Public Function cabecera_imprimir_etiquetas() As DataTable
        Dim cabecera As New DataTable
        '[ ], [Código Inventario],[Código Anterior],[Descripcion],[Fecha Compra],[Zona],[Clase],[Subclase],[Cantidad],[Lote Artículo],[Parte]
        With cabecera
            .Columns.Add(" ", Type.GetType("System.Boolean"))

            .Columns.Add("Código Anterior", Type.GetType("System.String"))
            .Columns.Add("Descripcion", Type.GetType("System.String"))
            .Columns.Add("Fecha Compra", Type.GetType("System.DateTime"))
            .Columns.Add("Zona", Type.GetType("System.String"))
            .Columns.Add("Clase", Type.GetType("System.String"))
            .Columns.Add("Subclase", Type.GetType("System.String"))
            .Columns.Add("Cantidad", Type.GetType("System.String"))
            .Columns.Add("Lote Artículo", Type.GetType("System.Int32"))
            .Columns.Add("Parte", Type.GetType("System.Int32"))
        End With
        Return cabecera
    End Function

    Public Function lista_monedas() As DataTable
        Dim Tmoneda As New DataTable
        Tmoneda.Columns.Add("opcion", Type.GetType("System.Int32"))
        Tmoneda.Columns.Add("mostrar")
        For i = 1 To 3
            Dim newFila As DataRow = Tmoneda.NewRow
            newFila("opcion") = i
            Select Case i
                Case 1
                    newFila("mostrar") = "CLP y YEN"
                Case 2
                    newFila("mostrar") = "CLP"
                Case 3
                    newFila("mostrar") = "YEN"
                Case Else
                    newFila("mostrar") = ""
            End Select
            Tmoneda.Rows.Add(newFila)
        Next
        Return Tmoneda
    End Function
    Public Function lista_periodo(Optional ByVal mes_actual As Boolean = False, Optional ByVal valor_final As Boolean = True) As DataTable
        Dim Tperiodo As New DataTable
        Dim Fmin As DateTime
        Dim Lperiodo As Vperiodo
        Fmin = DateSerial(2012, 1, 1)

        Tperiodo.Columns.Add("Codigo", Type.GetType("System.DateTime"))
        Tperiodo.Columns.Add("Descripcion")
        'Boolean -> True = Int -> -1  /   Boolean -> False = Int -> 0
        Lperiodo = New Vperiodo(Now.Year, Now.Month) + CInt(Not (mes_actual))
        While Lperiodo.first >= Fmin
            Dim dr As DataRow = Tperiodo.NewRow
            If valor_final Then
                dr(0) = Lperiodo.last
            Else
                dr(0) = Lperiodo.first
            End If
            dr(1) = Lperiodo.mostrar
            Tperiodo.Rows.Add(dr)
            Lperiodo = Lperiodo - 1
        End While
        Return Tperiodo
    End Function

    Public Function opciones_OBC() As DataTable
        Dim Topcion As New DataTable
        Topcion.Columns.Add("opcion", Type.GetType("System.Int32"))
        Topcion.Columns.Add("mostrar", Type.GetType("System.String"))
        For i = 1 To 3
            Dim newFila As DataRow = Topcion.NewRow
            newFila("opcion") = i
            Select Case i
                Case 1
                    newFila("mostrar") = "Saldo"
                Case 2
                    newFila("mostrar") = "Entradas"
                Case 3
                    newFila("mostrar") = "Salidas"
                Case Else
                    newFila("mostrar") = ""
            End Select
            Topcion.Rows.Add(newFila)
        Next
        Return Topcion
    End Function
    Public Function periodo_contable() As DataTable
        Dim TPcontab As New DataTable
        Dim Ping As Vperiodo
        TPcontab.Columns.Add("CodPeriodo", Type.GetType("System.DateTime"))
        TPcontab.Columns.Add("TxtPeriodo")
        TPcontab.Columns.Add("selected", Type.GetType("System.Int32"))
        Ping = New Vperiodo(Now.Year, Now.Month) + 1
        For i = 0 To 8
            Dim Drow As DataRow = TPcontab.NewRow
            Drow(0) = Ping.last
            Drow(1) = Ping.mostrar
            If Ping = perido_abierto_GP Then
                Drow(2) = 1
            Else
                Drow(2) = 0
            End If
            TPcontab.Rows.Add(Drow)
            Ping = Ping - 1
        Next
        Return TPcontab
    End Function
    Public Function lista_por_castigar() As DataTable
        Dim recordset As New DataTable
        recordset.Columns.Add(" ")
        recordset.Columns.Add("Código Artículo")
        recordset.Columns.Add("Fecha Castigo", System.Type.GetType("System.DateTime"))
        recordset.Columns.Add("Cantidad Castigada")
        recordset.Columns.Add("Descripcion Artículo")
        recordset.Columns.Add("rowindx")
        recordset.Columns.Add("Zona")
        recordset.Columns.Add("Proc-trib", System.Type.GetType("System.Boolean"))
        recordset.Columns.Add("parte")
        Return recordset
    End Function

    Public Function cabecera_saldo_obra_a_gasto() As DataTable
        Dim TBSaldo As New DataTable
        TBSaldo.Columns.Add("Codigo")
        TBSaldo.Columns.Add("Descripción o Referencia")
        TBSaldo.Columns.Add("Fecha", Type.GetType("System.DateTime"))
        TBSaldo.Columns.Add("Zona", Type.GetType("System.String"))
        TBSaldo.Columns.Add("Saldo", Type.GetType("System.Int32"))

        Return TBSaldo
    End Function
    Public Function cabecera_salida_obra_a_gasto() As DataTable
        Dim TBSalida As New DataTable
        TBSalida.Columns.Add("Codigo")
        TBSalida.Columns.Add("Descripción o Referencia")
        TBSalida.Columns.Add("Fecha", Type.GetType("System.DateTime"))
        TBSalida.Columns.Add("Zona")
        TBSalida.Columns.Add("Monto Utilizado", Type.GetType("System.Int32"))
        Return TBSalida
    End Function

    Public Function cabecera_salida_obra_a_af() As DataTable
        Dim TBSaldo As New DataTable
        TBSaldo.Columns.Add("Codigo")
        TBSaldo.Columns.Add("Descripción o Referencia")
        TBSaldo.Columns.Add("Monto Utilizado", Type.GetType("System.Int32"))

        Return TBSaldo
    End Function

    Public Function reportes_baja() As listado_basico
        _colchon = New DataTable
        _colchon.Columns.Add("COD", Type.GetType("System.String"))
        _colchon.Columns.Add("MONEDA", Type.GetType("System.String"))
        _colchon.Columns.Add("AMBIENTE", Type.GetType("System.String"))
        For i = 0 To 4
            Dim nueva_fila As DataRow = _colchon.NewRow
            Select Case i
                Case 0
                    nueva_fila(0) = i
                    nueva_fila(1) = "CLP"
                    nueva_fila(2) = "FIN"
                Case 1
                    nueva_fila(0) = i
                    nueva_fila(1) = "CLP"
                    nueva_fila(2) = "TRIB"
                Case 2
                    nueva_fila(0) = i
                    nueva_fila(1) = "CLP"
                    nueva_fila(2) = "IFRS"
                Case 3
                    nueva_fila(0) = i
                    nueva_fila(1) = "YEN"
                    nueva_fila(2) = "FIN"
                Case 4
                    nueva_fila(0) = i
                    nueva_fila(1) = "YEN"
                    nueva_fila(2) = "IFRS"
            End Select
            _colchon.Rows.Add(nueva_fila)
        Next
        Return New listado_basico(_colchon)
    End Function

#End Region

#Region "Tablas de la base de dato"

#Region "Procedimientos Estructurales"

    Public Function BASE_LOCAL(ByVal fecha As DateTime) As DataTable
        Dim txt_sql As String
        Dim colchon As DataTable
        txt_sql = "SELECT cod_articulo,parte,dsc_breve,zona,cantidad,val_libro,cod_subzona,txt_subzona" + Chr(13) + _
        "FROM AFN_BASE_LOCAL2('" + fecha.ToString("yyyyMMdd") + "','F')"
        colchon = maestro.ejecuta(txt_sql)
        Return colchon
    End Function
    Public Function BASE_LOCAL(ByVal fecha As DateTime, ByVal fila As Integer) As DataTable
        Dim txt_sql As String
        Dim colchon As DataTable
        txt_sql = "SELECT cod_articulo,parte,dsc_breve,zona,cantidad,val_libro,cod_subzona,txt_subzona" + Chr(13) + _
        "FROM AFN_BASE_LOCAL2('" + fecha.ToString("yyyyMMdd") + "','F')" + Chr(13) + _
        "WHERE fila=" + CStr(fila)
        colchon = maestro.ejecuta(txt_sql)
        Return colchon
    End Function

    Public Function DETALLE_FIN(ByVal articulo As String) As DataTable
        Dim colchon As DataTable
        Dim txt_sql As String
        txt_sql = "SELECT * FROM AFN_GET_INVENTARIO('F') WHERE cod_articulo=" + articulo + " ORDER BY precio_base ASC"
        colchon = maestro.ejecuta(txt_sql)
        Return colchon
    End Function
    Public Function DETALLE_IFRS_CLP(ByVal articulo As String, Optional ByVal parte As Integer = -1) As DataTable
        Dim colchon As DataTable
        Dim txt_sql, txt_parte As String
        If parte >= 0 Then
            txt_parte = " AND parte=" + parte.ToString
        Else
            txt_parte = ""
        End If
        txt_sql = "SELECT * FROM AFN_GET_INVENTARIO('GC') WHERE cod_articulo=" + articulo + txt_parte + " ORDER BY precio_base ASC"
        colchon = maestro.ejecuta(txt_sql)
        Return colchon
    End Function

    Public Function ARTICULO_INVENTARIO(ByVal lote As Integer) As DataTable
        Dim colchon As DataTable
        Dim txt_sql As String
        txt_sql = "SELECT producto,codigo_old,parte,ubicacion FROM AFN_EXISTENCIA WHERE lote_articulo=" + lote.ToString + " ORDER BY 1"
        colchon = maestro.ejecuta(txt_sql)
        Return colchon
    End Function

    Public Function buscar_Articulo(ByVal fecha_min As Date,
                                    ByVal fecha_max As Date,
                                    Optional ByVal codigo As Integer = 0,
                                    Optional ByVal nombre As String = "",
                                    Optional ByVal zona As String = "00",
                                    Optional ByVal estado As String = "1",
                                    Optional ByVal status As String = "NO") As DataTable
        '-- empieza funcion

        _txt_sql = "SELECT A.cod_articulo 'Artículo', A.cantidad 'Cantidad', A.zona 'Zona', " + Chr(13) + _
        "A.dscrp+A.dsc_extra 'Descripción Artículo', A.fila 'rowindx', A.parte 'parte', A.status 'estado' " + Chr(13) + _
        "FROM AFN_GET_INVENTARIO('F') A " + Chr(13) + _
        "WHERE " + _
        "A.fecha_inicio<='" + Format(Now, "yyyyMMdd") + "' and " + _
        "A.fecha_fin>'" + Format(Now, "yyyyMMdd") + "' and "
        If codigo <> 0 Then
            _txt_sql = _txt_sql + "(A.cod_articulo =" + codigo.ToString + ") and "
        End If
        If fecha_min <> DateTime.MinValue Then
            _txt_sql = _txt_sql + "A.fecha_compra>='" + Format(fecha_min, "yyyyMMdd") + "' and "
        End If
        If fecha_max <> DateTime.MaxValue Then
            _txt_sql = _txt_sql + "A.fecha_compra<'" + Format(fecha_max, "yyyyMMdd") + "' and "
        End If
        If nombre <> "" Then
            _txt_sql = _txt_sql + "(A.dscrp like '%" + nombre + "%' or A.dsc_extra like '%" + nombre + "%' ) and "
        End If
        If zona <> "00" Then
            _txt_sql = _txt_sql + "(A.zona='" + zona + "')  and "
        End If
        If status <> "NO" Then
            _txt_sql = _txt_sql + "(A.status='" + status + "') and "
        End If

        _txt_sql = _txt_sql + "A.cod_est in( " + estado + ") "

        _colchon = maestro.ejecuta(_txt_sql)
        buscar_Articulo = _colchon
    End Function
#End Region

#Region "Datos Basicos Contabilidad AF"

    Public Function ZONAS_GL() As DataTable
        Dim sql_txt As String
        Dim colchon As DataTable
        sql_txt = "SELECT COD_GL [cod], NOMBRE [es] FROM AFN_ZONA WHERE ACTIVA=1 ORDER BY COD_GL"
        colchon = maestro.ejecuta(sql_txt)
        Return colchon
    End Function
    Public Function ZONAS_GL_T() As DataTable
        Dim colchon As DataTable
        colchon = ZONAS_GL()
        'Agrego la fila "TODAS" al listado
        Dim fila_total As DataRow = colchon.NewRow
        fila_total(0) = "00"
        fila_total(1) = "TODAS"
        colchon.Rows.InsertAt(fila_total, 0)
        Return colchon
    End Function
    Public Function SUBZONAS_ACT() As DataTable
        Dim colchon As DataTable
        Dim txt_sql As String
        txt_sql = "SELECT cod,descrip,zona,GLsubz FROM AFN_SUBZONA WHERE activo=1"
        colchon = maestro.ejecuta(txt_sql)
        Return colchon
    End Function
    Public Function SUBZONAS_ACT(ByVal zona As String) As DataTable
        Dim colchon As DataTable
        Dim sql_subzona As String
        sql_subzona = "SELECT cod,descrip FROM AFN_SUBZONA WHERE (zona='" + zona + "') and activo=1"
        colchon = maestro.ejecuta(sql_subzona)
        Return colchon
    End Function
    Public Function TIPO_AF() As DataTable
        Dim colchon As DataTable
        Dim sql_consist As String
        sql_consist = "SELECT DISTINCT consistencia FROM AFN_CLASE"
        colchon = maestro.ejecuta(sql_consist)
        Return colchon
    End Function
    Public Function CLASE(ByVal tipo_af As String, ByVal mostrar_ing As Boolean) As DataTable
        Dim txt_ing As String
        If mostrar_ing Then
            txt_ing = " AND mostrar_ing=1"
        Else
            txt_ing = ""
        End If
        _txt_sql = "SELECT SysIT.dbo.clear(cod) [cod],SysIT.dbo.clear(descrip) [es] FROM AFN_CLASE WHERE (consistencia='" + tipo_af + "' OR '" + tipo_af + "'='')" + txt_ing + " ORDER BY cod ASC"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function CLASE_consulta() As DataTable
        _txt_sql = "SELECT SysIT.dbo.clear(cod) [cod],SysIT.dbo.clear(descrip) [es] FROM AFN_CLASE WHERE mostrar_con=1 ORDER BY fixed,cod ASC"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function SUBCLASE(ByVal clase As String) As DataTable
        _txt_sql = "SELECT cod,descrip,vu_sug FROM AFN_SUBCLASE WHERE mostrar=1 and clase_def='" + clase + "' ORDER BY descrip ASC"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function SUBCLASE_DET(ByVal subclase As String) As DataRow
        _txt_sql = "SELECT cod,descrip,vu_sug FROM AFN_SUBCLASE WHERE cod='" + subclase + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function
    Public Function IFRS_PREDET(ByVal clase As String) As DataRow
        _txt_sql = "SELECT clase,subclase,preparacion,desmant,transporte,montaje,honorarios,pValRes,metodVal FROM AFN_IFRS_PREDET WHERE clase='" + clase + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function
    Public Function CATEGORIA() As DataTable
        _txt_sql = "SELECT cod,descrip es FROM AFN_CATEGORIA WHERE cod<>'00' ORDER BY cod ASC"
        Return maestro.ejecuta(_txt_sql)
    End Function

    Public Function TIPO_CAMPO() As DataTable
        _txt_sql = "SELECT cod,descrip,sigla FROM AFN_TIPO_CAMPO"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function CLASIFICA_CUENTAS() As DataTable
        _txt_sql = "SELECT COD_CAMPO,TXT_CAMPO,COD_CLASE,TXT_CLASE,NUM_CUENTA,DESC_CUENTA,ID_CLASIFICA FROM AFN_clasif_cuenta()"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function

    Public Function lista_atributo_inicial(ByVal lote As Integer) As DataTable
        _txt_sql = "EXEC AFN_reporte_inicio2 " + lote.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function

    Public Function ESTADOS_BAJA() As DataTable
        _txt_sql = "SELECT A.* FROM(" + _
             "SELECT cod,descripcion FROM AFN_ESTADOS WHERE cod IN(SELECT estado2 FROM AFN_SITUACION WHERE situacion='BAJA')" + _
             " UNION ALL SELECT 0,'TODOS' ) A ORDER BY A.cod"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function

#End Region

#Region "Datos Basicos Inventario"
    Public Function estado_existencia() As DataTable
        Dim colchon As DataTable
        Dim sql_t As String
        sql_t = "SELECT cod,dscrpt FROM AFN_ESTADO_EXISTENCIA"
        colchon = maestro.ejecuta(sql_t)
        Return colchon
    End Function
    Public Function ubicacion() As DataTable
        _txt_sql = "SELECT A.cod,B.descrip+'-'+A.descrip 'descrip' FROM AFN_UBICACION A INNER JOIN AFN_UBICACION B ON A.sigue=B.cod WHERE A.nivel<>1"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function

    Public Function ATRIBUTOS(Optional ByVal activo As Boolean = True) As DataTable
        Dim OpActivo As String
        If activo Then
            OpActivo = "1"
        Else
            OpActivo = "0"
        End If
        _txt_sql = "SELECT cod,atributo,activo,imprimir,tipo FROM AFN_ATRIBUTO WHERE activo=" + OpActivo + " ORDER BY 2"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function DET_ATRIBUTO(atributo As Integer) As DataRow
        _txt_sql = "SELECT cod,atributo,activo,imprimir,tipo FROM AFN_ATRIBUTO WHERE cod=" + atributo.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function
    Public Function busca_nombre_foto(nuevo_nombre_foto As String) As Integer
        _txt_sql = "SELECT lote_art,cod_atrib,detalle FROM AFN_DETALLE_LOTE WHERE cod_atrib IN(SELECT cod FROM AFN_ATRIBUTO WHERE tipo='FOTO') UNION ALL SELECT lote_art,cod_atrib,detalle FROM AFN_DETALLE_ARTICULO WHERE cod_atrib IN(SELECT cod FROM AFN_ATRIBUTO WHERE tipo='FOTO')"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Select("detalle='" + nuevo_nombre_foto + "'").Count
    End Function

    Public Function INV_ATRIBUTOxLOTE(lote_articulo As Integer) As DataTable
        _txt_sql = "SELECT lote_art,cod_atrib,detalle,fech_ini,fech_fin,imprimir FROM AFN_DETALLE_LOTE WHERE lote_art=" + CStr(lote_articulo)
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function INV_ATRIBUTOxLOTE(lote_articulo As Integer, atributo As Integer) As DataRow
        Dim filas As DataRow()
        _colchon = INV_ATRIBUTOxLOTE(lote_articulo)
        filas = _colchon.Select("cod_atrib=" + CStr(atributo) + "")
        If filas.Count > 0 Then
            Return filas(0)
        Else
            Return Nothing
        End If
    End Function

    Public Function INV_ATRIBUTOxITEM(lote_articulo As Integer) As DataTable
        _txt_sql = "SELECT lote_art,codigo,cod_atrib,detalle,fech_ini,fech_fin,imprimir FROM AFN_DETALLE_ARTICULO WHERE lote_art=" + CStr(lote_articulo)
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function INV_ATRIBUTOxITEM(lote_articulo As Integer, codigo_item As String) As DataTable
        _txt_sql = "SELECT lote_art,codigo,cod_atrib,detalle,fech_ini,fech_fin,imprimir " + _
            "FROM AFN_DETALLE_ARTICULO " + _
            "WHERE lote_art=" + CStr(lote_articulo) + " AND codigo='" + codigo_item + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        'Dim algo As New DataTable
        'algo.Rows.Add(_colchon.Select("codigo='" + codigo_item + "'"))
        Return _colchon
    End Function
    Public Function INV_ATRIBUTOxITEM(lote_articulo As Integer, codigo_item As String, atributo As Integer) As DataRow
        Dim filas As DataRow()
        _colchon = INV_ATRIBUTOxITEM(lote_articulo, codigo_item)
        filas = _colchon.Select("cod_atrib=" + CStr(atributo) + "")
        If filas.Count > 0 Then
            Return filas(0)
        Else
            Return Nothing
        End If
    End Function

    Public Function INV_CONSULTA_ETIQUETAS(ByVal criterios As List(Of multicriterio)) As DataTable
        Dim query_where = ""
        For Each criterio In criterios
            query_where = query_where + If(String.IsNullOrEmpty(query_where), "WHERE ", " and ") + criterio.filtro
        Next
        _txt_sql = "SELECT [ ], [Código Inventario],[Código Anterior],[Descripcion],[Fecha Compra],[Zona],[Clase],[Subclase],[Cantidad],[Lote Artículo],[Parte] " + _
            "FROM AFN_listado_etiquetas('F') A " + _
            query_where + _
            "ORDER BY [Zona],[Clase],[Cantidad],[Lote Artículo]"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function

#End Region

#Region "Toma de Inventario"

    Public Function lista_fecha_inv() As DataTable
        _txt_sql = "SELECT fecha_invent FROM AFN_TOMA_INVENTARIO_CAB ORDER BY fecha_invent DESC"
        _colchon = maestro.ejecuta(_txt_sql)
        _colchon.Columns.Add("mostrar")
        For Each fila As DataRow In _colchon.Rows
            Dim lPeriodo As Vperiodo = New Vperiodo(fila("fecha_invent").Year, fila("fecha_invent").Month)
            fila("mostrar") = lPeriodo.mostrar
        Next
        Return _colchon
    End Function
    Public Function lista_zona_inv(ByVal fecha_inventario As DateTime, Optional ByVal tomado As Boolean = True) As DataTable
        _txt_sql = "SELECT DISTINCT " + Chr(13) + _
            "A.zona 'zona', A.txt_zona 'txt_zona'" + Chr(13) + _
            "FROM AFN_TOMA_INVENTARIO_DET A" + Chr(13) + _
            "WHERE A.fecha_invent='" + fecha_inventario.ToString("yyyyMMdd") + "'"
        If tomado Then
            _txt_sql = _txt_sql + " and A.estado_tomado IS NOT NULL"
        End If
        _colchon = maestro.ejecuta(_txt_sql)
        For Each fila As DataRow In _colchon.Rows
            fila(0) = Trim(fila(0))
            fila(1) = Trim(fila(1))
        Next
        Return _colchon
    End Function
    Public Function lista_clase_inv(ByVal fecha_inventario As DateTime, ByVal zona As String, Optional ByVal tomado As Boolean = True) As DataTable
        _txt_sql = "SELECT DISTINCT " + Chr(13) + _
            "A.clase 'clase', A.txt_clase 'txt_clase'" + Chr(13) + _
            "FROM AFN_TOMA_INVENTARIO_DET A" + Chr(10) + _
            "WHERE A.fecha_invent='" + fecha_inventario.ToString("yyyyMMdd") + "' and " + Chr(13) + _
            "A.zona='" + zona + "'"
        If tomado Then
            _txt_sql = _txt_sql + " and A.estado_tomado IS NOT NULL"
        End If
        _txt_sql = _txt_sql + " ORDER BY A.clase"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function lista_inv_foto(ByVal fecha_inventario As DateTime, ByVal zona As String, ByVal clase As String) As DataTable
        _txt_sql = "SELECT ROW_NUMBER() OVER(ORDER BY producto)[row]," + Chr(13) + _
                    "producto,codigo_old,descripcion,fecha_compra,zona,txt_zona,clase,txt_clase,cod_subzona," + Chr(13) + _
                    "txt_subzona,cod_subclase,txt_subclase,cantidad_lote[cantidad],lote_articulo,cod_ubic,txt_ubic,valor_inicial,val_libro" + Chr(13) + _
                    "FROM AFN_TOMA_INVENTARIO_DET" + Chr(13) + _
                    "WHERE zona='" + zona + "' and clase='" + clase + "' and fecha_invent='" + fecha_inventario.ToString("yyyyMMdd") + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function lista_inv_tomado(ByVal fecha_inventario As DateTime, ByVal zona As String, ByVal clase As String) As DataTable
        _txt_sql = "select A.estado_tomado, (select dscrpt from afn_estado_existencia where cod=A.estado_tomado)'txt_estado'," + Chr(13) + _
        "A.subzona_tomada, (select descrip from afn_subzona where cod=A.subzona_tomada)'txt_subzona_actual'," + Chr(13) + _
        "A.ubicacion_tomada, (select G.descrip+'-'+F.descrip from afn_ubicacion F, afn_ubicacion G where F.sigue=G.cod and F.cod=A.ubicacion_tomada)'txt_ubic_actual'," + Chr(13) + _
        "A.producto,A.descripcion,A.lote_articulo,A.valor_inicial,A.val_libro, A.fecha_compra,A.zona,A.clase,A.cod_subzona,A.txt_subzona,A.cantidad_lote,A.parte,A.cod_ubic,A.txt_ubic,A.aplicado" + Chr(13) + _
        "FROM AFN_TOMA_INVENTARIO_DET A" + Chr(13) + _
        "WHERE A.fecha_invent='" + fecha_inventario.ToString("yyyyMMdd") + "' AND A.zona='" + zona + "' AND A.clase='" + clase + "' AND A.estado_tomado IS NOT NULL ORDER BY A.producto"

        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function lista_inv_tomado(ByVal fecha_inventario As DateTime, ByVal zona As String, ByVal clase As String, ByVal dif_subzona As Boolean, Optional ByVal existe As Boolean = True) As DataTable
        _colchon = lista_inv_tomado(fecha_inventario, zona, clase)
        If dif_subzona Then
            Dim nodif As DataRow()
            nodif = _colchon.Select("subzona_tomada=cod_subzona")
            For Each nofila As DataRow In nodif
                _colchon.Rows.Remove(nofila)
            Next
        End If
        Dim filas_existen As DataRow()
        If existe Then
            filas_existen = _colchon.Select("estado_tomado=0")
        Else
            filas_existen = _colchon.Select("estado_tomado<>0")
        End If
        For Each nofila As DataRow In filas_existen
            _colchon.Rows.Remove(nofila)
        Next
        Return _colchon
    End Function
    Public Function detalle_inventario(ByVal fecha As String, ByVal clase As String, ByVal zona As String)
        _txt_sql = "SELECT codigo_inv [producto], codigo_inv_old [codigo_old], dsc_breve [descripcion],fcompra [fecha_compra],Fzona [zona],FtextZona [txt_zona],Fclase [clase],FtextClase [txt_clase],FcodSubzona[cod_subzona],FtxtSubzona [txt_subzona],Qfin [cantidad],codigo [lote_articulo],cod_ubicacion[cod_ubic],ubicacion[txt_ubic]" + Chr(13) + _
            "FROM AFN_DETALLE_INVENTARIO('" + fecha + "','F') A" + Chr(13) + _
            "WHERE A.FcodE=1 and A.Fclase='" + clase + "' and A.Fzona='" + zona + "'" + Chr(13) + _
            "ORDER BY A.codigo_inv"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function

    Public Function ingresar_toma_inv(ByVal producto As String, ByVal fecha_inventario As String, ByVal estado As String, ByVal subzona As String, _
                                      ByVal ubicacion As String, ByVal observacion As String) As DataTable
        _txt_sql = "EXEC AFN_ing_toma '" + producto + "','" + fecha_inventario + "'," + estado + "," + subzona + "," + ubicacion + ",'" + observacion + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function

#End Region

#Region "Desde GP"
    Public Function PROVEEDOR_GP() As DataTable
        Dim colchon As DataTable
        Dim sql_txt As String
        sql_txt = "SELECT COD,TEXTO FROM PM00200 ORDER BY VENDNAME"
        colchon = maestro.ejecuta(sql_txt)
        Return colchon
    End Function

    Public Function CUENTAS() As DataTable
        _txt_sql = "SELECT ACTINDX,ACTNUMBR_1,ACTDESCR FROM GL00105"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function

    Public Function PERIODO_FISCAL() As DataTable
        _txt_sql = "SELECT TOP 1 year1 FROM SY40101 WHERE year1>=2012 and year1<year(GETUTCDATE()) ORDER BY 1 desc"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
#End Region

#Region "Obras en Construccion"

    Public Function saldos_obc(ByVal fecha As DateTime, ByVal currency As String) As DataTable
        Dim colchon As DataTable
        Dim texto As String
        texto = "SELECT codmov 'Codigo Movimiento',descrip 'Descripción',txfecha 'Fecha Documento',glfecha 'Fecha Contable',zona 'Zona',monto 'Monto Documento',ocupado 'Monto Utilizado',saldo 'Saldo Final' " + _
            "FROM AFN_saldo_obras('" + fecha.ToString("yyyyMMdd") + "','" + currency + "')"
        colchon = maestro.ejecuta(texto)
        Return colchon
    End Function

    Public Function entradas_obc(ByVal fechaIni As DateTime, ByVal fechaFin As DateTime, ByVal currency As String) As DataTable
        Dim colchon As DataTable
        Dim texto As String
        texto = "SELECT codmov 'Codigo Movimiento',descrip 'Descripción',txfecha 'Fecha Documento',glfecha 'Fecha Contable',zona 'Zona', documento 'Nº Documento', id_proveedor 'ID Proveedor', nombre_proveedor 'Nombre Proveedor' ,monto 'Monto Entrada' " + _
            "FROM AFN_entrada_obras('" + fechaIni.ToString("yyyyMMdd") + "','" + fechaFin.ToString("yyyyMMdd") + "','" + currency + "')"
        colchon = maestro.ejecuta(texto)
        Return colchon
    End Function

    Public Function salidas_obc(ByVal fechaIni As DateTime, ByVal fechaFin As DateTime, ByVal currency As String) As DataTable
        Dim colchon As DataTable
        Dim texto As String
        texto = "SELECT codmov 'Codigo Movimiento',descrip 'Descripción',txfecha 'Fecha Documento',glfecha 'Fecha Contable',zona 'Zona',monto_salida 'Monto Salida',codigo_entrada 'Código de Movimiento Entrada',codigo_activo 'Cod. Lote Activo', descrip_activo 'Descripcion de Activo', zona_activo 'Zona Activo', clase_activo 'Clase Activo', proveedor 'ID proveedor', num_doc 'Nº Documento', monto_total_activo 'Monto Total Activo' " + _
            "FROM AFN_salida_obras('" + fechaIni.ToString("yyyyMMdd") + "','" + fechaFin.ToString("yyyyMMdd") + "','" + currency + "')"
        colchon = maestro.ejecuta(texto)
        Return colchon
    End Function

    Public Function busca_entrada_obc(ByVal documento As String, ByVal proveedor As String, ByVal zona As String) As Integer
        _txt_sql = "SELECT count(*)[CONTAR] FROM AFN_OBRA_CONS WHERE tipo='E' and zona='" + zona + "' AND proveedor='" + proveedor + "' AND num_doc='" + documento + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)("CONTAR")
    End Function

    Public Function get_last_batch() As Integer
        _txt_sql = "SELECT ISNULL(MAX(grupo_batch),0)[CONT] FROM AFN_TEMP_OBRA_CONS"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0).Item("CONT")
    End Function

    Public Function batch_disponibles() As DataTable
        _txt_sql = "SELECT grupo_batch[id_batch],SUM(monto)[monto] FROM AFN_TEMP_OBRA_CONS WHERE batch_act=1 GROUP BY grupo_batch"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function

    Public Function detalle_batch(ByVal id_batch As Integer) As DataTable
        _txt_sql = "SELECT * FROM AFN_TEMP_OBRA_CONS WHERE grupo_batch=" + id_batch.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
#End Region

#End Region

#Region "Reportes de informacion"
    Public Function CUADRO_INGRESO_IFRS(ByVal lote_articulo As Integer) As DataTable
        _txt_sql = "EXEC AFN_cuadro_ifrs " + lote_articulo.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function REPORTE_VIG_IFRS_DET(ByVal periodo As Vperiodo, ByVal moneda As BMoneda, ByVal zona As String, ByVal clase As String) As DataTable
        Dim sql_tipo As String
        Select Case moneda
            Case BMoneda.CLP
                sql_tipo = "GC"
            Case BMoneda.YEN
                sql_tipo = "GY"
            Case Else
                sql_tipo = ""
        End Select
        _txt_sql = "SELECT fcompra 'Fecha de Compra'," + _
                "codigo 'Codigo del Bien'," + _
                "dsc_breve 'Descripción breve de los bienes'," + _
                "Qfin 'Cantidad'," + _
                "Fzona 'Zona'," + _
                "Fclase 'Clase'," + _
                "valor_inicial_neto 'Valor Inicial'," + _
                "preparacion 'Preparación'," + _
                "desmantelamiento 'Desmantelamiento'," + _
                "transporte 'Transporte'," + _
                "montaje 'Montaje'," + _
                "honorario 'Honorarios'," + _
                "credito 'Credito Adiciones'," + _
                "valor_final_activo 'Valor de Activo Fijo'," + _
                "depreciacion_inicial 'Dep. Acum Anterior'," + _
                "deter 'Deterioro de Activo'," + _
                "valor_residual 'Valor Residual'," + _
                "val_suj_dep 'Valor sujeto a Depreciación'," + _
                "VU_asig 'V. Util Asignada'," + _
                "VU_ocup 'V. util Ocupada'," + _
                "VU_resi 'V. util Residual'," + _
                "depr_ejer 'Depreciación del Ejercicio'," + _
                "depreciacion_final 'Depreciación Acumulada'," + _
                "revaluacion 'Revalorización'," + _
                "valor_libro 'Valor Libro del Activo'," + _
                "FcodSubzona 'Código Subzona'," + _
                "FtxtSubzona 'Descripción Subzona'," + _
                "valorizacion 'Metodo de valorización'," + _
                "fecha_ingreso 'Fecha Contabilizacion'," + _
                "origen 'Origen'" + Chr(13) + _
                "FROM AFN_DETALLE_IFRS('" + periodo.lastDB + "','" + sql_tipo + "') WHERE FcodE=1" + where_clase(clase) + where_zona(zona) + " ORDER BY 2"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function REPORTE_VIG_INV_DET(ByVal periodo As Vperiodo, ByVal moneda As BMoneda, ByVal zona As String, ByVal clase As String) As DataTable
        Dim sql_tipo As String
        Select Case moneda
            Case BMoneda.CLP
                sql_tipo = "F"
            Case BMoneda.YEN
                sql_tipo = "Y"
            Case Else
                sql_tipo = ""
        End Select
        _txt_sql = "SELECT fcompra 'Fecha de Compra'," + _
                "codigo 'Codigo del Bien'," + _
                "dsc_breve 'Descripción breve de los bienes'," + _
                "Qfin 'Cantidad'," + _
                "Fzona 'Zona'," + _
                "Fclase 'Clase'," + _
                "valor_inicial_neto 'Valor Inicial'," + _
                "pmc '%C.M.'," + _
                "cm_activo 'C. Monetaria Activo'," + _
                "val_actlzd 'Valor activo actualizado'," + _
                "credito 'Credito Adiciones'," + _
                "valor_final_activo 'Valor de Activo Fijo'," + _
                "depreciacion_inicial 'Dep. Acum. Anterior'," + _
                "cmDA 'C. Monetaria Dep. Acum.'," + _
                "DA_actlzd 'Dep. Acum. Actualizada'," + _
                "deter 'Deterioro de Activo'," + _
                "valor_residual 'Valor Residual'," + _
                "val_suj_dep 'Valor sujeto a Depreciación'," + _
                "VU_asig 'V. Util Asignada'," + _
                "VU_ocup 'V. Util Ocupada'," + _
                "VU_resi 'V. Util Residual'," + _
                "depr_ejer 'Depreciación del Ejercicio'," + _
                "depreciacion_final 'Depreciación Acumulada'," + _
                "valor_libro 'Valor Libro del Activo'," + _
                "FcodSubzona 'Codigo Subzona'," + _
                "FtxtSubzona 'Descripción Subzona'," + _
                "fecha_ingreso 'Fecha Contabilizacion'," + _
                "origen 'Origen'," + _
                "cod_ubicacion 'Codigo Ubicación'," + _
                "ubicacion 'Ubicación'," + _
                "entregado 'Entregado'," + _
                "cod_ultimo_estado 'Código Ultimo Estado'," + _
                "txt_ultimo_estado 'Último Estado'" + Chr(13) + _
                "FROM AFN_DETALLE_INVENTARIO('" + periodo.lastDB + "','" + sql_tipo + "') WHERE FcodE=1" + where_zona(zona) + where_clase(clase) + " ORDER BY 2"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function REPORTE_VIG_CONT_DET(ByVal periodo As Vperiodo, ByVal moneda As BMoneda, ByVal ambiente As BAmbiente, ByVal zona As String, ByVal clase As String) As DataTable
        Dim sql_tipo As String
        Select Case moneda
            Case BMoneda.CLP
                If ambiente = BAmbiente.TRIB Then
                    sql_tipo = "T"
                Else
                    sql_tipo = "F"
                End If
            Case BMoneda.YEN
                sql_tipo = "Y"
            Case Else
                sql_tipo = ""
        End Select
        _txt_sql = "SELECT fcompra 'Fecha de Compra'," + _
                "codigo 'Codigo del Bien'," + _
                "dsc_breve 'Descripción breve de los bienes'," + _
                "Qfin 'Cantidad'," + _
                "Fzona 'Zona'," + _
                "Fclase 'Clase'," + _
                "valor_inicial_neto 'Valor Inicial'," + _
                "pmc '%C.M.'," + _
                "cm_activo 'C. Monetaria Activo'," + _
                "val_actlzd 'Valor activo actualizado'," + _
                "credito 'Credito Adiciones'," + _
                "valor_final_activo 'Valor de Activo Fijo'," + _
                "depreciacion_inicial 'Dep. Acum. Anterior'," + _
                "cmDA 'C. Monetaria Dep. Acum.'," + _
                "DA_actlzd 'Dep. Acum. Actualizada'," + _
                "deter 'Deterioro de Activo'," + _
                "valor_residual 'Valor Residual'," + _
                "val_suj_dep 'Valor sujeto a Depreciación'," + _
                "VU_asig 'V. Util Asignada'," + _
                "VU_ocup 'V. Util Ocupada'," + _
                "VU_resi 'V. Util Residual'," + _
                "depr_ejer 'Depreciación del Ejercicio'," + _
                "depreciacion_final 'Depreciación Acumulada'," + _
                "valor_libro 'Valor Libro del Activo'," + _
                "FcodSubzona 'Codigo Subzona'," + _
                "FtxtSubzona 'Descripción Subzona'," + _
                "fecha_ingreso 'Fecha Contabilizacion'," + _
                "origen 'Origen'," + _
                "VU_inicial 'Vida Util Inicial'" + Chr(13) + _
                "FROM AFN_DETALLE_ACTIVO('" + periodo.lastDB + "','" + sql_tipo + "') WHERE FcodE=1" + where_zona(zona) + where_clase(clase) + " ORDER BY 2"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function REPORTE_VIG_RESUMEN(ByVal datos As form_reporte_dato) As DataTable
        Dim titulo1, titulo2, titulo3 As String
        If datos.vista = form_reporte_dato.BVista.C Then
            titulo1 = "Clase de Activo"
            titulo2 = "Nombre Departamento"
            titulo3 = "Nombre Lugar"
        Else
            titulo1 = "Nombre Zona"
            titulo2 = "Clase de Activo"
            titulo3 = "Nombre Lugar"
        End If
        If datos.moneda = BMoneda.MIX Then
            Select Case datos.tipo
                Case "ESP1"
                    _txt_sql = "EXEC AFN_resumen_ma '" + datos.fecha + "','" + datos.cod_zona + "','" + datos.cod_clase + "'"
                Case Else
                    _txt_sql = "EXEC AFN_resumen_ma '" + datos.fecha + "','" + datos.cod_zona + "','" + datos.cod_clase + "'"
            End Select
        Else
            If datos.ambiente = base_AFN.BAmbiente.IFRS Then
                _txt_sql = "SELECT Descrip1 '" + titulo1 + "'," + _
                            "Descrip2 '" + titulo2 + "'," + _
                            "Descrip3 '" + titulo3 + "'," + _
                            "valor_anterior 'Valor Inicial'," + _
                            "credito 'Credito adiciones'," + _
                            "valor_actual 'Valor de Activo Fijo'," + _
                            "Dep_Acum_anterior 'Dep. Acum Anterior'," + _
                            "valor_residual 'Valor Residual'," + _
                            "Dep_ejercicio 'Depreciación del Ejercicio'," + _
                            "Dep_acumulada 'Depreciación Acumulada'," + _
                            "revalorizacion 'Revalorización'," + _
                            "valor_libro 'Valor Libro del Activo'," + _
                            "Orden1,Orden2,Orden3 " + _
                            "FROM AFN_resumen_IFRS2('" + datos.fecha + "','" + datos.cod_zona + "','" + datos.cod_clase + "','" + datos.tipo + "','" + datos.vista.ToString + "') ORDER BY Orden1,Orden2"
            Else
                _txt_sql = "SELECT Descrip1 '" + titulo1 + "'," + _
                            "Descrip2 '" + titulo2 + "'," + _
                            "Descrip3 '" + titulo3 + "'," + _
                            "valor_anterior 'Valor Inicial'," + _
                            "CM_activo 'C Monetaria Activo'," + _
                            "credito 'Credito adiciones'," + _
                            "valor_actual 'Valor de Activo Fijo'," + _
                            "Dep_Acum_anterior 'Dep. Acum Anterior'," + _
                            "CM_Dep_Acum 'C. Monetaria Dep. Acum.'," + _
                            "valor_residual 'Valor Residual'," + _
                            "Dep_ejercicio 'Depreciación del Ejercicio'," + _
                            "Dep_acumulada 'Depreciación Acumulada'," + _
                            "valor_libro 'Valor Libro del Activo'," + _
                            "Orden1,Orden2,Orden3 " + _
                            "FROM AFN_resumen2('" + datos.fecha + "','" + datos.cod_zona + "','" + datos.cod_clase + "','" + datos.tipo + "','" + datos.vista.ToString + "')  ORDER BY Orden1,Orden2"
            End If
        End If
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function

    Public Function CONTABILIZAR_GP2013(ByVal periodo As Vperiodo) As DataTable
        _txt_sql = "EXEC AFN_contabilizar_GP2013 " + periodo.last.Year.ToString + "," + periodo.last.Month.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function

    Public Function REPORTE_BAJA_DETALLE_LOC(ByVal sDesde As DateTime, ByVal sHasta As DateTime, ByVal sBase As String, sSituacion As Integer) As DataTable
        Dim Tsituacion As String
        Select Case sSituacion
            Case 0      'Todos
                Tsituacion = "<>1"
            Case Else
                Tsituacion = "=" + sSituacion.ToString
        End Select

        _txt_sql = "SELECT " + _
                        "A.cod_articulo 'Codigo del Bien'," + _
                        "A.fec_compra 'Fecha de Compra'," + _
                        "A.fecha_ini 'Fecha de Baja'," + _
                        "A.estado 'Situación'," + _
                        "A.dsc_breve 'Descripción breve de los bienes'," + _
                        "A.cantidad 'Cantidad'," + _
                        "A.zona 'Zona'," + _
                        "A.clase 'Clase'," + _
                        "A.valor_anterior 'Valor Anterior'," + _
                        "A.cred_adi 'Credito'," + _
                        "A.val_AF 'Valor de Activo Fijo'," + _
                        "dep_eje 'Depreciación Ejercicio'," + _
                        "A.DA_AF 'Depreciación Acumulada'," + _
                        "A.val_libro 'Valor Libro del Activo'," + _
                        "A.cod_subzona 'Codigo Subzona'," + _
                        "A.txt_subzona 'Descripción Subzona' " + _
                "FROM afn_base_local2('" + sHasta.ToString("yyyyMMdd") + "','" + sBase + "') A " + _
                "WHERE A.fecha_ini>='" + sDesde.ToString("yyyyMMdd") + "' and A.cod_est" + Tsituacion + " ORDER BY 2"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function REPORTE_BAJA_DETALLE_IFRS(ByVal sDesde As DateTime, ByVal sHasta As DateTime, ByVal sBase As String, sSituacion As Integer) As DataTable
        Dim Tsituacion As String
        Select Case sSituacion
            Case 0      'Todos
                Tsituacion = "<>1"
            Case Else
                Tsituacion = "=" + sSituacion.ToString
        End Select

        _txt_sql = "SELECT " + _
                "A.cod_articulo 'Codigo del Bien'," + _
                "A.fec_compra 'Fecha de Compra'," + _
                "A.fecha_ini 'Fecha de Baja'," + _
                "A.estado 'Situación'," + _
                "A.dsc_breve 'Descripción breve de los bienes'," + _
                "A.cantidad 'Cantidad'," + _
                "A.zona 'Zona'," + _
                "A.clase 'Clase'," + _
                "A.valor_anterior 'Valor Anterior'," + _
                "A.cred_adi 'Credito'," + _
                "A.preparacion 'Preparacion'," + _
                "A.desmantel 'Desmantelamiento'," + _
                "A.transporte 'Transporte'," + _
                "A.montaje 'Montaje'," + _
                "A.honorario 'Honorarios'," + _
                "A.val_AF 'Valor de Activo Fijo'," + _
                "A.dep_eje 'Depreciación Ejercicio'," + _
                "A.DA_AF 'Depreciación Acumulada'," + _
                "A.val_libro 'Valor Libro del Activo'," + _
                "A.cod_subzona 'Codigo Subzona'," + _
                "A.txt_subzona 'Descripción Subzona' " + _
        "FROM AFN_base_global2('" + sHasta.ToString("yyyyMMdd") + "','" + sBase + "') A " + _
        "WHERE A.fecha_ini>='" + sDesde.ToString("yyyyMMdd") + "' and A.cod_est" + Tsituacion + " ORDER BY 2"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function

    Public Function REPORTE_INICIO(ByVal codigo As Integer) As DataTable
        _txt_sql = "EXEC AFN_reporte_inicio " + codigo.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function

    Public Function REPORTE_INICIO2(ByVal codigo As Integer) As DataTable
        _txt_sql = "EXEC AFN_reporte_inicio2 " + codigo.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function REPORTE_BAJA(ByVal codigo As Integer, ByVal parte As Integer) As DataTable
        _txt_sql = "EXEC AFN_reporte_baja " + codigo.ToString + "," + parte.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function REPORTE_BAJA2(ByVal codigo As Integer, ByVal parte As Integer) As DataTable
        _txt_sql = "EXEC AFN_reporte_baja2 " + codigo.ToString + "," + parte.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
#End Region

#Region "Acomodar WHERE"
    Private Function where_clase(ByVal clase As String) As String
        Dim sql_clase As String
        Select Case clase
            Case "00"
                sql_clase = ""
            Case "10"
                sql_clase = " and tipo='ACTIVO'"
            Case "20"
                sql_clase = " and tipo='INTANGIBLE'"
            Case Else
                sql_clase = " and Fclase='" + clase + "'"
        End Select
        Return sql_clase
    End Function
    Private Function where_zona(ByVal zona As String) As String
        Dim sql_zona As String
        If zona <> "00" Then
            sql_zona = " and Fzona='" + zona + "'"
        Else
            sql_zona = ""
        End If
        Return sql_zona
    End Function
#End Region

#Region "Valores de la base de datos"
    Public ReadOnly Property contar_subzonas(ByVal zona As String) As Integer
        Get
            Return maestro.Dcount("*", "AFN_SUBZONA", "zona='" + zona + "'")
        End Get
    End Property

    Public ReadOnly Property contar_docVentas(ByVal codigo As Integer, ByVal parte As Integer) As Integer
        Get
            Return maestro.Dcount("DOCVENTA", "AFN_DVENTA", "ID_ARTICULO=" + codigo.ToString + " AND PARTE=" + parte.ToString)
        End Get
    End Property

    Public ReadOnly Property articulo_descrip(ByVal id_articulo As Integer) As String
        Get
            Return maestro.Dval("dscrp+dsc_extra", "AFN_GET_inventario('F')", "cod_articulo=" + id_articulo.ToString)
        End Get
    End Property

    Public ReadOnly Property perido_abierto_GP As Vperiodo
        Get
            Dim valor As String
            Dim colchon As DataTable
            valor = "SELECT DISTINCT YEAR1,PERIODID FROM SY40100 WHERE YEAR1 IN(SELECT YEAR1 FROM SY40101 WHERE HISTORYR=0) and closed=0 and series<>0 and periodid<>0 ORDER BY YEAR1 DESC,PERIODID DESC"
            colchon = maestro.ejecuta(valor)
            If colchon.Rows.Count > 0 Then
                Return New Vperiodo(colchon.Rows(0).Item(0), colchon.Rows(0).Item(1))
            Else
                Return New Vperiodo(Now.Year, Now.Month)
            End If
        End Get
    End Property

    Public ReadOnly Property periodo_abierto_contar As Integer
        Get
            Return maestro.Dcount("DISTINCT YEAR1*100+PERIODID", "SY40100", "YEAR1 IN(SELECT YEAR1 FROM SY40101 WHERE HISTORYR=0) and closed=0 and series<>0 and periodid<>0")
        End Get
    End Property

    Public Function TipoCambio(ByVal fecha As DateTime) As Double
        Dim colchon As DataTable
        Dim sql_txt As String
        sql_txt = "SELECT XCHGRATE FROM DYNAMICS..MC00100 WHERE EXGTBLID='YEN OBSERVADO' AND EXCHDATE='" + fecha.ToString("yyyyMMdd") + "'"
        colchon = maestro.ejecuta(sql_txt)
        If colchon.Rows.Count > 0 Then
            Return colchon.Rows(0).Item(0)
        Else
            Return 0
        End If
    End Function
#End Region

#Region "Procedimientos de sistema"

#Region "Formulario de Ingreso"
    Public Function BORRAR_AF(ByVal codigo_lote As Integer) As DataTable
        _txt_sql = "UPDATE AFN_LOTE_ARTICULOS SET estado_aprov='DELETE' WHERE cod=" + codigo_lote.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function ACTIVAR_AF(ByVal codigo_lote As Integer) As DataTable
        _txt_sql = "UPDATE AFN_LOTE_ARTICULOS SET estado_aprov='CLOSE' WHERE cod=" + codigo_lote.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function

    Public Function INGRESO_LOTE(ByVal descrip As String, ByVal fcompra As DateTime, ByVal proveedor As String, _
                                 ByVal documento As String, ByVal total_compra As String, ByVal vutil As Integer, _
                                 ByVal derecho As String, ByVal fecha_contab As DateTime, ByVal origen As String, _
                                 ByVal CtiPo As String) As DataRow
        _txt_sql = "EXEC AFN_ing_lote '" + _
            descrip + "','" + fcompra.ToString("yyyyMMdd") + "','" + proveedor + "','" + documento + "'," + _
            total_compra + "," + vutil.ToString + ",'" + derecho + "','" + fecha_contab.ToString("yyyyMMdd") + "','" + origen + "','" + CtiPo + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function
    Public Function INGRESO_FINANCIERO(ByVal lote_articulo As Integer, ByVal zona As String, ByVal cantidad As Integer, _
                    ByVal clase As String, ByVal categoria As String, ByVal subzona As String, ByVal subclase As String, _
                    ByVal depreciar As String, ByVal usuario As String) As DataRow
        _txt_sql = "EXEC AFN_ing_inv_F " + lote_articulo.ToString + ",'" + zona + "'," + _
            cantidad.ToString + ",'" + clase + "','" + categoria + "'," + subzona + ",'" + subclase + "'," + depreciar + ",'" + usuario + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function
    Public Function INGRESO_TRIBUTARIO(ByVal lote_articulo As Integer, ByVal zona As String, ByVal cantidad As Integer, _
                    ByVal clase As String, ByVal categoria As String, ByVal subzona As String, ByVal subclase As String, _
                    ByVal depreciar As String, ByVal usuario As String) As DataRow
        _txt_sql = "EXEC AFN_ing_inv_T " + lote_articulo.ToString + ",'" + zona + "'," + _
            cantidad.ToString + ",'" + clase + "','" + categoria + "'," + subzona + ",'" + subclase + "'," + depreciar + ",'" + usuario + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function
    Public Function INGREGO_IFRS(ByVal lote_articulo As Integer, ByVal val_res As String, ByVal VUA As Integer, _
                    ByVal metod_val As Integer, ByVal prepa As Integer, ByVal trans As Integer, ByVal monta As Integer, _
                    ByVal desma As Integer, ByVal honor As Integer) As DataRow
        _txt_sql = "EXEC AFN_ing_inv_I " + lote_articulo.ToString + "," + val_res + "," + VUA.ToString + "," + metod_val.ToString + _
            "," + prepa.ToString + "," + trans.ToString + "," + monta.ToString + "," + desma.ToString + "," + honor.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function
    Public Function GENERAR_CODIGO_INV(ByVal lote_articulo As Integer) As DataRow
        _txt_sql = "EXEC AFN_crear_cod " + lote_articulo.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function
    Public Function INGRESO_ATRIB_LOTE(lote_articulo As Integer, cod_atrib As Integer, detalle As String, mostrar As Integer) As DataRow
        _txt_sql = "EXEC AFN_ing_detalle_lote " + lote_articulo.ToString + "," + cod_atrib.ToString + ",'" + _
            detalle + "'," + mostrar.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function
    Public Function INGRESO_ATRIB_ITEM(lote_articulo As Integer, codigo_item As String, cod_atrib As Integer, detalle As String, mostrar As Integer) As DataRow
        _txt_sql = "EXEC AFN_ing_detalle_artic " + lote_articulo.ToString + "," + codigo_item + "," + cod_atrib.ToString + ",'" + _
            detalle + "'," + mostrar.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function

    Public Function MODIFICA_LOTE(ByVal lote_articulo As Integer, ByVal descrip As String, ByVal proveedor As String, _
                    ByVal documento As String, ByVal total_compra As String, ByVal vutil As Integer, _
                    ByVal derecho As String, ByVal fecha_contab As DateTime) As DataRow
        _txt_sql = "EXEC AFN_mod_lote " + lote_articulo.ToString + ", '" + _
            descrip + "','" + proveedor + "','" + documento + "'," + _
            total_compra + "," + vutil.ToString + ",'" + derecho + "','" + fecha_contab.ToString("yyyyMMdd") + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function
    Public Function MODIFICA_FINANCIERO(lote_articulo As Integer, zona As String, categoria As String, _
                                subzona As String, subclase As String, deprecia As String, usuario As String) As DataRow
        _txt_sql = "EXEC AFN_mod_inv_F " + lote_articulo.ToString + ",'" + zona + "','" + _
            categoria + "'," + subzona + ",'" + subclase + "'," + deprecia + ",'" + usuario + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function
    Public Function MODIFICA_TRIBUTARIO(lote_articulo As Integer, zona As String, categoria As String, _
                                subzona As String, subclase As String, deprecia As String, usuario As String) As DataRow
        _txt_sql = "EXEC AFN_mod_inv_T " + lote_articulo.ToString + ",'" + zona + "','" + _
            categoria + "'," + subzona + ",'" + subclase + "'," + deprecia + ",'" + usuario + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function
    Public Function MODIFICA_IFRS(lote_artic As Integer, val_res As String, VUA As Integer, metod_val As Integer,
                                  prepa As Integer, trans As Integer, monta As Integer, desma As Integer, honor As Integer) As DataRow
        _txt_sql = "EXEC AFN_mod_inv_I " + lote_artic.ToString + "," + val_res + "," + VUA.ToString + _
            "," + metod_val.ToString + "," + prepa.ToString + "," + trans.ToString + "," + monta.ToString + _
            "," + desma.ToString + "," + honor.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function
    Public Function ELIMINA_IFRS(ByVal lote_articulo As Integer) As DataRow
        _txt_sql = "EXEC AFN_del_inv_I " + lote_articulo.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function

    Public Sub BORRAR_ATRIBUTOxLOTE(lote_articulo As Integer, atributo As Integer)
        _txt_sql = "DELETE AFN_DETALLE_LOTE WHERE lote_art=" + lote_articulo.ToString + " and cod_atrib=" + CStr(atributo)
        maestro.execute(_txt_sql)
    End Sub
    Public Sub BORRAR_ATRIBUTOxITEM(lote_articulo As Integer, codigo_item As String, atributo As Integer)
        _txt_sql = "DELETE AFN_DETALLE_ARTICULO WHERE lote_art=" + lote_articulo.ToString + " and cod_atrib=" + CStr(atributo) + "and codigo='" + codigo_item + "'"
        maestro.execute(_txt_sql)
    End Sub
#End Region

#Region "Obras en Construccion"
    Public Function INGRESO_OBC(ByVal fechaC As DateTime, ByVal zona As String, ByVal proveedor As String, ByVal documento As String, ByVal descp As String, ByVal credit_amo As String, ByVal fechaGL As DateTime)
        'ingreso en obra_cons
        _txt_sql = "EXEC AFN_ing_obras '" + fechaC.ToString("yyyyMMdd") + "','" + zona + "','" + proveedor + "','" + documento + "','" + descp + "'," + credit_amo + ",'" + fechaGL.ToString("yyyyMMdd") + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function EGRESO_OBC(ByVal cod_entrada As Integer, ByVal cod_lote As Integer, ByVal monto As String, ByVal zona As String) As DataRow
        _txt_sql = "EXEC AFN_egre_obras " + cod_entrada.ToString + "," + cod_lote.ToString + _
            "," + monto + ",'" + zona + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function
    Public Function EGRESO_GST(ByVal cod_entrada As Integer, ByVal fsalida As DateTime, ByVal monto As String) As DataRow
        _txt_sql = "EXEC AFN_egre_gasto " + cod_entrada.ToString + ",'" + fsalida.ToString("yyyyMMdd") + "'," + monto
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function

    Public Function EGRESO_GST_TEMP(ByVal cod_entrada As Integer, ByVal fsalida As DateTime, ByVal monto As String, ByVal batch As Integer) As DataRow
        _txt_sql = "EXEC AFN_egre_gasto_temp " + cod_entrada.ToString + "," + batch.ToString + ",'" + fsalida.ToString("yyyyMMdd") + "'," + monto
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function

    Public Function LIMPIAR_BATCH(ByVal id_batch As Integer) As DataRow
        _txt_sql = "EXEC AFN_limpiar_batch " + id_batch.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function
    Public Function DESESACTIVA_BATCH(ByVal id_batch As Integer) As DataRow
        _txt_sql = "EXEC AFN_inact_batch " + id_batch.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon.Rows(0)
    End Function
#End Region

#Region "Modificaciones"
    Public Function CAMBIO_ZONA(ByVal codigo As Integer, ByVal parte As Integer, ByVal fecha_cambio As DateTime,
                                ByVal zona_destino As String, ByVal subzona_destino As String,
                                ByVal cantidad As Integer, ByVal usuario As String, ByVal DetalleProductos As DataTable, ByVal grupo_fila As Integer) As DataTable
        Dim colchon As DataTable
        Dim sql_movim As String
        sql_movim = "EXEC AFN_cambio_act " + codigo.ToString + "," + parte.ToString + ",'" + fecha_cambio.ToString("yyyyMMdd") + _
            "','" + zona_destino + "'," + subzona_destino + "," + cantidad.ToString + ",'" + usuario + "'"
        colchon = maestro.ejecuta(sql_movim)
        If colchon(0).Item("cod_sit") = 1 And colchon(0).Item("parte2") <> -1 Then
            Dim FilasProductos As DataRow() = DetalleProductos.Select("procesar=1 AND row_id=" + grupo_fila.ToString)
            Dim nueva_parte As Integer = colchon(0).Item("parte2")
            For Each filaP As DataRow In FilasProductos
                CAMBIO_ZONA_DET(filaP("producto"), nueva_parte)
            Next
        End If
        Return colchon
    End Function

    Private Function CAMBIO_ZONA_DET(ByVal producto As String, ByVal parte As Integer) As DataTable
        Dim sql_mov As String
        Dim colchon As DataTable
        sql_mov = "EXEC AFN_cambio_act_detalle'" + producto + "'," + parte.ToString
        colchon = maestro.ejecuta(sql_mov)
        Return colchon
    End Function

    Public Sub CAMBIO_ZONA_INV(ByVal producto As String, ByVal fecha_inventario As DateTime)
        Dim sql_mov As String
        Dim colchon As DataTable
        sql_mov = "UPDATE AFN_TOMA_INVENTARIO_DET SET aplicado=1 WHERE producto='" + producto + "' AND fecha_invent='" + fecha_inventario.ToString("yyyyMMdd") + "'"
        colchon = maestro.ejecuta(sql_mov)
        'Return colchon
    End Sub

    Public Function CASTIGO(ByVal codigo As Integer, ByVal parte As Integer, ByVal fecha_castigo As DateTime,
                            ByVal cantidad As Integer, ByVal tributario As Integer, ByVal usuario As String, ByVal DetalleProductos As DataTable, ByVal grupo_fila As Integer) As DataTable
        Dim colchon As DataTable
        Dim sql_movim As String
        sql_movim = "EXEC AFN_castigo_act " + codigo.ToString + "," + parte.ToString + ",'" + fecha_castigo.ToString("yyyyMMdd") + _
            "'," + cantidad.ToString + "," + tributario.ToString + ",'" + usuario + "'"
        colchon = maestro.ejecuta(sql_movim)
        If colchon(0).Item("cod_sit") = 1 Then
            Dim FilasProductos As DataRow() = DetalleProductos.Select("procesar=1 AND row_id=" + grupo_fila.ToString)
            Dim nueva_parte As Integer = colchon(0).Item("parte2")
            Dim primera_parte As Integer = colchon(0).Item("parte1")
            Dim parte_uso As Integer
            If nueva_parte <> -1 Then
                parte_uso = nueva_parte
            Else
                parte_uso = primera_parte
            End If
            For Each filaP As DataRow In FilasProductos
                CASTIGO_DET(filaP("producto"), parte_uso, fecha_castigo)
            Next
        End If
        Return colchon
    End Function

    Private Function CASTIGO_DET(ByVal producto As String, ByVal parte As Integer, ByVal fecha_baja As DateTime) As DataTable
        Dim sql_mov As String
        Dim colchon As DataTable
        sql_mov = "EXEC AFN_castigo_act_detalle'" + producto + "'," + parte.ToString + ",'" + fecha_baja.ToString("yyyyMMdd") + "'"
        colchon = maestro.ejecuta(sql_mov)
        Return colchon
    End Function

    Public Function ACTUALIZAR_TOMA_INVENTARIO(ByVal fecha_inventario As DateTime, ByVal producto As String, ByVal atributo As String, ByVal valor As Integer) As DataTable
        Dim consulta As String
        Dim colchon As DataTable
        Select Case atributo
            Case "estado"
                consulta = "EXEC AFN_mod_toma '" + producto + "','" + fecha_inventario.ToString("yyyyMMdd") + "'," + valor.ToString
            Case "subzona"
                consulta = "EXEC AFN_mod_toma '" + producto + "','" + fecha_inventario.ToString("yyyyMMdd") + "',null," + valor.ToString
            Case "ubicacion"
                consulta = "EXEC AFN_mod_toma '" + producto + "','" + fecha_inventario.ToString("yyyyMMdd") + "',null,null," + valor.ToString
            Case Else
                consulta = "SELECT '" + producto + "' [producto]"
        End Select
        colchon = maestro.ejecuta(consulta)
        Return colchon
    End Function
#End Region

#Region "Clasificación de Cuentas para Contabilizar"
    Public Function INGRESO_CLASIFICACION_CUENTA(ByVal tipo_cuenta As Integer, ByVal clase As String, ByVal num_cuenta As String) As DataTable
        _txt_sql = "EXEC AFN_NEW_DET_LINEAS_CONTAB " + tipo_cuenta.ToString + ",'" + clase + "','" + num_cuenta + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
    Public Function MODIFICA_CLASIFICACION_CUENTA(ByVal id_clasifica As Integer, ByVal tipo_cuenta As Integer, ByVal clase As String, ByVal num_cuenta As String) As DataTable
        _txt_sql = "EXEC AFN_MOD_DET_LINEAS_CONTAB " + id_clasifica.ToString + "," + tipo_cuenta.ToString + ",'" + clase + "','" + num_cuenta + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
#End Region

#Region "Mantenimiento"
    Public Function CambioAño(ByVal año As String, ByVal usuario As String) As DataTable
        _txt_sql = "EXEC AFN_cambioYEAR " + año + ",'" + usuario + "'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return _colchon
    End Function
#End Region

#Region "Procesos"
    Public Function getLotesAbiertos() As lote_articulos
        _txt_sql = "SELECT cod,estado_aprov,descripcion,fecha_compra,proveedor,num_doc,precio_inicial,vida_util_inicial,derecho_credito,fecha_ing,origen,consistencia FROM AFN_LOTE_ARTICULOS" + _
            " WHERE estado_aprov='OPEN'"
        _colchon = maestro.ejecuta(_txt_sql)
        Return New lote_articulos(_colchon)
    End Function

    Public Function getInfoLote(ByVal codigo As Integer) As lote_articulos.fila
        _txt_sql = "SELECT cod,estado_aprov,descripcion,fecha_compra,proveedor,num_doc,precio_inicial,vida_util_inicial,derecho_credito,fecha_ing,origen,consistencia FROM AFN_LOTE_ARTICULOS" + _
            " WHERE cod=" + codigo.ToString
        _colchon = maestro.ejecuta(_txt_sql)
        Return New lote_articulos(_colchon).FIND_by_COD(codigo)
    End Function

    Public Function actualiza_descripcion_lote(ByVal codigo As Integer, ByVal new_desc As String) As Boolean
        Try
            _txt_sql = "UPDATE AFN_LOTE_ARTICULOS SET descripcion = '" + new_desc + "' WHERE cod=" + codigo.ToString
            _colchon = maestro.ejecuta(_txt_sql)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#End Region

#Region "Tablas en segundo plano"
    Public Function back_salidas_obc() As BackProcess
        Dim tTotal, tUpdate As Integer 'Para medir los tiempos en segundos
        tTotal = 20
        tUpdate = 2
        'Creamos una instancia de la clase multi hilo y seteamos los campos que normalmente pasariamos como parametros
        Dim cmh As New BackProcess(tTotal, tUpdate)
        Return cmh
    End Function
    Public Function back_detalle_inventario() As BackProcess
        Dim tTotal, tUpdate As Integer 'Para medir los tiempos en segundos
        tTotal = 7.5 * 60  '7.5 minutos
        tUpdate = 10
        'Creamos una instancia de la clase multi hilo y seteamos los campos que normalmente pasariamos como parametros
        Dim cmh As New BackProcess(tTotal, tUpdate)
        Return cmh
    End Function
    Public Function back_REPORTE_VIG_INV_DET() As BackProcess
        Dim tTotal, tUpdate As Integer 'Para medir los tiempos en segundos
        tTotal = 15 * 60  '15 minutos
        tUpdate = 10
        'Creamos una instancia de la clase multi hilo y seteamos los campos que normalmente pasariamos como parametros
        Dim cmh As New BackProcess(tTotal, tUpdate)
        Return cmh
    End Function
    Public Function back_REPORTE_VIG_CONT_DET() As BackProcess
        Dim tTotal, tUpdate As Integer 'Para medir los tiempos en segundos
        tTotal = 20  '15 minutos
        tUpdate = 1
        'Creamos una instancia de la clase multi hilo y seteamos los campos que normalmente pasariamos como parametros
        Dim cmh As New BackProcess(tTotal, tUpdate)
        Return cmh
    End Function
    Public Function back_REPORTE_VIG_IFRS_DET() As BackProcess
        Dim tTotal, tUpdate As Integer 'Para medir los tiempos en segundos
        tTotal = 20  '15 minutos
        tUpdate = 1
        'Creamos una instancia de la clase multi hilo y seteamos los campos que normalmente pasariamos como parametros
        Dim cmh As New BackProcess(tTotal, tUpdate)
        Return cmh
    End Function
    Public Function back_REPORTE_VIG_RESUMEN() As BackProcess
        Dim tTotal, tUpdate As Integer 'Para medir los tiempos en segundos
        tTotal = 20
        tUpdate = 1
        'Creamos una instancia de la clase multi hilo y seteamos los campos que normalmente pasariamos como parametros
        Dim cmh As New BackProcess(tTotal, tUpdate)
        Return cmh
    End Function
#End Region

End Class




