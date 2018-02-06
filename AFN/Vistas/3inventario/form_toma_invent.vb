Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Threading

Public Class form_toma_invent
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private maestro As New master_control

    Protected base As New base_AFN

    Private Sub form_toma_invent_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub

    Private Sub form_toma_invent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'periodo
        Dim datos As DataTable
        datos = base.lista_periodo(True)
        With cbPeriodo
            .DataSource = datos
            .ValueMember = datos.Columns(0).ColumnName
            .DisplayMember = datos.Columns(1).ColumnName
            .SelectedIndex = 0
        End With
        'zonas
        datos = base.ZONAS_GL()
        With cboZona
            .DataSource = datos
            .ValueMember = datos.Columns(0).ColumnName
            .DisplayMember = datos.Columns(1).ColumnName
            .SelectedIndex = 0
        End With
        'clases
        datos = base.CLASE("ACTIVO", True)
        With cboClase
            .DataSource = datos
            .ValueMember = datos.Columns(0).ColumnName
            .DisplayMember = datos.Columns(1).ColumnName
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        ' revisar funcion, puede que funcione incorrectamente
        'declarar variables
        Dim zona, clase As String
        Dim strPaso As String
        Dim i As Double
        Dim registros As Long
        Dim colchon, cojin As DataTable
        Dim Separador, sql_estado, nombre_total As String
        Dim pagina, ini_titulo, fila_actual As Integer
        'inicio validar
        If cboZona.SelectedIndex = -1 Then
            Mensaje_IT("Debe indicar la zona para imprimir")
            cboZona.Focus()
            Exit Sub
        End If
        If cboClase.SelectedIndex = -1 Then
            Mensaje_IT("Debe indicar la clase para imprimir")
            cboClase.Focus()
            Exit Sub
        End If
        If cbPeriodo.SelectedIndex = -1 Then
            Mensaje_IT("Debe indicar el periodo para imprimir")
            cbPeriodo.Focus()
            Exit Sub
        End If
        'fin validar
        zona = Trim(cboZona.SelectedValue)
        clase = Trim(cboClase.SelectedValue)
        Dim Dfecha As Date = cbPeriodo.SelectedValue
        Dim Tperiodo As New Vperiodo(Dfecha.Year, Dfecha.Month)
        bloquearW(Me)
        Dim fbp As ProgressShow = cargar_barra(Me)
        Dim cmh As BackProcess = base.back_detalle_inventario
        fbp.inicializar(2)
        fbp.definir_proceso(0, cmh.intervalos)
        fbp.cambiar_parte(0, 70)

        cmh.ini_detalle_inventario(Tperiodo.lastDB, clase, zona)
        'Esperamos a que termine la ejecucion del hilo
        While cmh.isWorking
            cmh.Keep()
            If cmh.UpdateBar Then
                fbp.continua_proceso(0)
            End If
        End While
        fbp.inicia_proceso(1)
        colchon = cmh.resultado
        registros = colchon.Rows.Count
        If registros = 0 Then
            MsgBox("No se han encontrado valores para la busqueda indicada", vbExclamation, "NH FOODS CHILE")
            colchon = Nothing
        Else
            fbp.definir_proceso(1, registros + 5)
            Separador = getSeparadorList()
            'MsgBox(Separador)
            Dim objExcel As New Excel.Application
            Dim libro As Excel.Workbook = objExcel.Workbooks.Add
            For pagina = libro.Sheets.Count To 2 Step -1
                libro.Sheets(pagina).Delete()
            Next
            Dim oSheet As Excel.Worksheet = libro.Sheets(pagina)
            Dim oSheet2 As Excel.Worksheet = libro.Sheets.Add

            'Creamos hoja con codigos para el inventario
            ini_titulo = 1
            'ingresamos tablas de codidificacion
            'ESTADOS
            oSheet2.Cells(ini_titulo, 1).Value = "ESTADOS"
            sql_estado = "SELECT cod,dscrpt FROM afn_estado_existencia"
            cojin = maestro.ejecuta(sql_estado)

            For indice = 0 To cojin.Rows.Count - 1
                Dim linea As DataRow
                linea = cojin.Rows(indice)
                oSheet2.Cells(ini_titulo + indice + 1, 2).Value = linea("cod").ToString
                oSheet2.Cells(ini_titulo + indice + 1, 3).Value = linea("dscrpt").ToString
            Next

            cojin = Nothing
            'SUBZONAS
            oSheet2.Cells(ini_titulo, 5).Value = "SUBZONAS"
            If zona <> "30" Then
                strPaso = " or cod=0"
            Else
                strPaso = ""
            End If
            sql_estado = "SELECT cod,descrip,activo FROM AFN_SUBZONA WHERE zona='" + zona + "'" + strPaso
            cojin = maestro.ejecuta(sql_estado)

            For indice = 0 To cojin.Rows.Count - 1
                Dim linea As DataRow
                linea = cojin.Rows(indice)
                'strPaso = strPaso + linea("cod").ToString + " " + linea("descrip")
                oSheet2.Cells(ini_titulo + indice + 1, 6).Value = linea("cod").ToString
                oSheet2.Cells(ini_titulo + indice + 1, 7).Value = linea("descrip").ToString
            Next
            cojin = Nothing
            'UBICACIONES
            oSheet2.Cells(ini_titulo, 9) = "UBICACIONES"
            sql_estado = "SELECT B.cod,A.descrip+'-'+B.descrip 'descrip'" & Chr(13) & "FROM AFN_UBICACION A, AFN_UBICACION B" & Chr(13) & "WHERE A.cod=B.sigue And B.nivel=2 AND B.activo=1 ORDER BY 1"
            cojin = maestro.ejecuta(sql_estado)
            For indice = 0 To cojin.Rows.Count - 1
                Dim linea As DataRow
                linea = cojin.Rows(indice)
                oSheet2.Cells(ini_titulo + indice + 1, 10).Value = linea("cod").ToString
                oSheet2.Cells(ini_titulo + indice + 1, 11).Value = linea("descrip").ToString
                'If indice < 7 Then
                '    strPaso = strPaso + linea("cod").ToString + " " + linea("descrip")
                'Else
                '    If indice < 14 Then
                '        strPaso2 = strPaso2 + linea("cod").ToString + " " + linea("descrip")
                '    Else
                '        strPaso3 = strPaso3 + linea("cod").ToString + " " + linea("descrip")
                '    End If
                'End If
                'If indice <> cojin.Rows.Count - 1 Then
                '    If (indice + 1) Mod 7 <> 0 Then
                '        If indice < 7 Then
                '            strPaso = strPaso + Chr(10)
                '        Else
                '            If indice < 14 Then
                '                strPaso2 = strPaso2 + Chr(10)
                '            Else
                '                strPaso3 = strPaso3 + Chr(10)
                '            End If
                '        End If
                '    End If
                'End If
            Next
            'fin tablas de codificacion
            fbp.continua_proceso()

            'ingresamos cabecera
            oSheet.Cells(ini_titulo, 2) = "Zona:"
            oSheet.Cells(ini_titulo, 3) = zona + " - " + colchon.Rows(0).Item("txt_zona")    'despues pondremos los filtros por zona aca

            oSheet.Cells(ini_titulo, 6) = "Clase:"
            oSheet.Cells(ini_titulo, 7) = clase + " - " + colchon.Rows(0).Item("txt_clase")    'despues pondremos los filtros por clase aca

            'With oSheet.Cells(ini_titulo, 6)
            '    .Value = "   Responsables"
            '    .Font.Bold = True
            'End With
            'oSheet.Cells(ini_titulo + 1, 6) = "Conteo Físico:"
            'With oSheet.Range(lcol(7) + CStr(ini_titulo + 1) + ":" + lcol(9) + CStr(ini_titulo + 1))
            '    With .Borders(9)            'xlEdgeBottom
            '        .ColorIndex = 0
            '        .TintAndShade = 1
            '        .Weight = 3
            '    End With
            'End With
            ini_titulo = ini_titulo + 2
            'oSheet.Cells(ini_titulo, 6) = "Inventario:"
            'With oSheet.Range(lcol(7) + CStr(ini_titulo) + ":" + lcol(9) + CStr(ini_titulo + 2))
            '    With .Borders(9)            'xlEdgeBottom
            '        .ColorIndex = 0
            '        .TintAndShade = 1
            '        .Weight = 3
            '    End With
            '    With .Borders(12)           'xlInsideHorizontal
            '        .ColorIndex = 0
            '        .TintAndShade = 1
            '        .Weight = 3
            '    End With
            'End With
            'ini_titulo = ini_titulo + 2
            'ini_titulo = ini_titulo + 3
            'fin cabecera
            fbp.continua_proceso()
            Application.DoEvents()
            'inicio titulos
            oSheet.Cells(ini_titulo, 1) = "Nº"
            oSheet.Cells(ini_titulo, 2) = "Código"
            oSheet.Cells(ini_titulo, 3) = "Código" & vbCrLf & "Lote"
            oSheet.Cells(ini_titulo, 4) = "Código" & vbCrLf & "Antiguo"
            oSheet.Cells(ini_titulo, 5) = "Descripción"
            With oSheet.Range(lcol(5) + CStr(ini_titulo) + ":" + lcol(7) + CStr(ini_titulo))
                .MergeCells = True
            End With
            oSheet.Cells(ini_titulo, 8) = "Fecha Compra"
            oSheet.Cells(ini_titulo, 9) = "Cantidad"
            oSheet.Cells(ini_titulo, 10) = "Subzona" & vbCrLf & "Anterior"
            oSheet.Cells(ini_titulo, 11) = "Ubicacion" & vbCrLf & "Anterior"
            oSheet.Cells(ini_titulo, 12) = "Estado" & vbCrLf & "Actual"
            oSheet.Cells(ini_titulo, 13) = "Subzona" & vbCrLf & "Actual"
            oSheet.Cells(ini_titulo, 14) = "Ubicacion" & vbCrLf & "Actual"
            oSheet.Cells(ini_titulo, 15) = "Check"
            oSheet.Cells(ini_titulo, 16) = "Observación"
            With oSheet.Range(lcol(16) + CStr(ini_titulo) + ":" + lcol(18) + CStr(ini_titulo))
                .MergeCells = True
            End With
            'fin titulos
            'formato titulos
            With oSheet.Rows(ini_titulo & ":" & ini_titulo)
                .HorizontalAlignment = -4108        '-4108 equivale a centrar
                .VerticalAlignment = -4108
                .WrapText = True                    'equivale a alinear contenido a la celda
                '.Orientation = 0
                '.AddIndent = False
                '.IndentLevel = 0
                .ShrinkToFit = False
                .RowHeight = 30                     'alto de fila se estable en 30
                .Font.Bold = True
            End With
            Application.DoEvents()
            'fin todo lo de titulos
            fbp.continua_proceso()
            i = 1
            fila_actual = ini_titulo + 1
            For indice = 0 To colchon.Rows.Count - 1
                Dim fila As DataRow = colchon.Rows(indice)
                With oSheet.Rows(CStr(fila_actual) + ":" + CStr(fila_actual))
                    .RowHeight = .RowHeight * 4
                    .VerticalAlignment = -4160     'equivale a top
                    .WrapText = True                    'equivale a alinear contenido a la celda
                    .ShrinkToFit = False
                End With
                oSheet.Cells(fila_actual, 1) = i
                oSheet.Cells(fila_actual, 2).NumberFormat = "@"
                oSheet.Cells(fila_actual, 2) = fila("producto")
                oSheet.Cells(fila_actual, 3) = fila("lote_articulo")
                oSheet.Cells(fila_actual, 4).NumberFormat = "@"
                oSheet.Cells(fila_actual, 4) = fila("codigo_old")
                oSheet.Cells(fila_actual, 5) = fila("descripcion")
                With oSheet.Range(lcol(5) + CStr(fila_actual) + ":" + lcol(7) + CStr(fila_actual))
                    .MergeCells = True
                End With
                Dim tmpF As Date = fila("fecha_compra")
                oSheet.Cells(fila_actual, 8).FormulaLocal = "=FECHA(" + CStr(tmpF.Year) + Separador + CStr(tmpF.Month) + Separador + CStr(tmpF.Day) + ")"

                oSheet.Cells(fila_actual, 9) = fila("cantidad")
                oSheet.Cells(fila_actual, 10) = fila("cod_subzona")
                oSheet.Cells(fila_actual, 11) = fila("cod_ubic")
                With oSheet.Range(lcol(16) + CStr(fila_actual) + ":" + lcol(18) + CStr(fila_actual))
                    .MergeCells = True
                End With
                fila_actual = fila_actual + 1
                i = i + 1
                fbp.continua_proceso()
                Application.DoEvents()
            Next

            'formato general (hoja principal)
            oSheet.Columns("A").ColumnWidth = 4
            oSheet.Columns("B").ColumnWidth = 14.4
            oSheet.Columns("C").ColumnWidth = 8
            oSheet.Columns("D").ColumnWidth = 14.4
            oSheet.Columns("E").ColumnWidth = 30
            oSheet.Columns("F").ColumnWidth = 15
            oSheet.Columns("G").ColumnWidth = 10
            oSheet.Columns("H").ColumnWidth = 12.6
            oSheet.Columns("I").ColumnWidth = 8.2
            oSheet.Columns("J").ColumnWidth = 8
            oSheet.Columns("K").ColumnWidth = 10
            oSheet.Columns("L").ColumnWidth = 8
            oSheet.Columns("M").ColumnWidth = 8
            oSheet.Columns("N").ColumnWidth = 10
            oSheet.Columns("O").ColumnWidth = 5.57
            oSheet.Columns("P").ColumnWidth = 15
            oSheet.Columns("Q").ColumnWidth = 21
            oSheet.Columns("R").ColumnWidth = 26
            'formato general (hoja codigos)
            oSheet2.Columns("A").ColumnWidth = 1
            oSheet2.Columns("B").ColumnWidth = 5
            oSheet2.Columns("C").ColumnWidth = 30
            oSheet2.Columns("D").ColumnWidth = 3
            oSheet2.Columns("E").ColumnWidth = 1
            oSheet2.Columns("F").ColumnWidth = 5
            oSheet2.Columns("G").ColumnWidth = 30
            oSheet2.Columns("H").ColumnWidth = 3
            oSheet2.Columns("I").ColumnWidth = 1
            oSheet2.Columns("J").ColumnWidth = 5
            oSheet2.Columns("K").ColumnWidth = 30
            Application.DoEvents()
            With oSheet.Range("A" + CStr(ini_titulo) + ":" + lcol(18) + CStr(fila_actual - 1))
                With .Borders(7)            'xlEdgeLeft
                    .ColorIndex = 0
                    .TintAndShade = 1
                    .Weight = 2
                End With
                With .Borders(8)            'xlEdgeTop
                    .ColorIndex = 0
                    .TintAndShade = 1
                    .Weight = 2
                End With
                With .Borders(9)            'xlEdgeBottom
                    .ColorIndex = 0
                    .TintAndShade = 1
                    .Weight = 2
                End With
                With .Borders(10)           'xlEdgeRight
                    .ColorIndex = 0
                    .TintAndShade = 1
                    .Weight = 2
                End With
                With .Borders(11)           'xlInsideVertical
                    .ColorIndex = 0
                    .TintAndShade = 1
                    .Weight = 2
                End With
                With .Borders(12)           'xlInsideHorizontal
                    .ColorIndex = 0
                    .TintAndShade = 1
                    .Weight = 2
                End With
            End With
            Application.DoEvents()
            'fin formato general
            'formato hoja
            fbp.continua_proceso()
            oSheet.Name = "CONTEO" + zona + "-" + clase
            nombre_total = base.fileLogo
            oSheet.PageSetup.LeftHeaderPicture.Filename = nombre_total
            Application.DoEvents()
            With oSheet.PageSetup
                .PrintTitleRows = "$1:$3"
                .Orientation = 2
                '.BlackAndWhite = False
                .Zoom = 60
                '.FitToPagesWide = 1
                '.FitToPagesTall = 4
                Try
                    .PaperSize = 14
                Catch ex As Exception
                    .PaperSize = Excel.XlPaperSize.xlPaperA4
                End Try
                .LeftHeader = "&G" + Chr(13) + "&8NH FOODS CHILE"
                .CenterHeader = "&""-,Negrita""&U&16LISTADO DE CONTEO ACTIVO FIJO"
                .RightHeader = "Fecha de Impresion : &D" & Chr(13) & "Hora de Impresion: &T" + Chr(13) + "Página &P de &N"
                .LeftFooter = "_____________________" + Chr(13) + "    Responsable Conteo"
                .CenterFooter = "_____________________" + Chr(13) + "Responsable Inventario"
                .RightFooter = "_____________________" + Chr(13) + "Gerente Area    "
                '            .TopMargin = objExcel.InchesToPoints(1.01)
                .CenterHorizontally = True
            End With

            oSheet2.Name = "CODIGOS" + zona + "-" + clase
            oSheet2.PageSetup.LeftHeaderPicture.Filename = nombre_total
            Application.DoEvents()
            With oSheet2.PageSetup
                .Orientation = 2
                .Zoom = 90
                Try
                    .PaperSize = 14
                Catch ex As Exception
                    .PaperSize = Excel.XlPaperSize.xlPaperA4
                End Try
                .LeftHeader = "&G" + Chr(13) + "&8NH FOODS CHILE"
                .CenterHeader = "&""-,Negrita""&U&16LISTADO DE CODIGOS PARA CONTEO ACTIVO FIJO"
                .RightHeader = "Fecha de Impresion : &D" & Chr(13) & "Hora de Impresion: &T" + Chr(13) + "Página &P de &N"
                '.LeftFooter = "_____________________" + Chr(13) + "    Responsable Conteo"
                '.CenterFooter = "_____________________" + Chr(13) + "Responsable Inventario"
                '.RightFooter = "_____________________" + Chr(13) + "Gerente Area    "
                .TopMargin = objExcel.InchesToPoints(1.01)
                .CenterHorizontally = True
            End With
            'fin formato hoja
            objExcel.Visible = True
            objExcel = Nothing
        End If
        fbp.continua_proceso()
        descargar_barra(Me)
        desbloquearW(Me)
    End Sub
End Class

'Public Class BackProcess_toma_invent
'    Inherits form_toma_invent

'    Private _fecha As String
'    Private _clase As String
'    Private _zona As String
'    Private _resultado As DataTable
'    Private _hilo As Thread

'    Private _tTotal, _tUpdate, _TActual As Integer 'Para medir los tiempos en segundos

'    Public Sub New(ByVal fecha As String, ByVal clase As String, ByVal zona As String)
'        '_base = base
'        _fecha = fecha
'        _clase = clase
'        _zona = zona

'        'Agregamos el handler del evento (si no lo hacemos no podremos interceptarlo)
'        AddHandler ejecutar, New ProcesoSecundario(AddressOf captura_res)

'        'Creamos un delegado para el método ImprimirSuma()
'        Dim ts As ThreadStart = New ThreadStart(AddressOf resultado)

'        'Creamos un hilo para ejecutar el delegado...
'        _hilo = New Thread(ts)


'    End Sub

'    Public Event ejecutar As ProcesoSecundario

'    ''' <summary>
'    ''' Se utiliza para iniciar el proceso en segundo plano, con los tiempos estimados de ejecución
'    ''' </summary>
'    ''' <param name="tTotal">Tiempo estimado total que se espera durará todo el proceso. Se mide en segundos</param>
'    ''' <param name="tUpdate">Tiempo que indica cada cuanto deberá actualizar la barra de progreso. Se mide en segundos</param>
'    ''' <remarks></remarks>
'    Public Sub iniciar(ByVal tTotal As Integer, ByVal tUpdate As Integer)
'        _tTotal = tTotal
'        _tUpdate = tUpdate
'        _TActual = 0

'        'Iniciamos la ejecucion del nuevo hilo
'        _hilo.Start()
'    End Sub

'    Public Sub resultado()
'        Dim tabla As DataTable
'        tabla = base.detalle_inventario(_fecha, _clase, _zona)
'        RaiseEvent ejecutar(tabla)
'    End Sub

'    Public Sub captura_res(ByVal datos As DataTable)
'        _resultado = datos
'    End Sub

'    Public Function datos() As DataTable
'        Return _resultado
'    End Function

'    Public ReadOnly Property hilo As Thread
'        Get
'            Return _hilo
'        End Get
'    End Property

'    Public ReadOnly Property isWorking As Boolean
'        Get
'            Return _hilo.ThreadState = ThreadState.Running
'        End Get
'    End Property

'    Public Sub Keep()
'        Thread.Sleep(1000)
'        My.Application.DoEvents()
'        _TActual = _TActual + 1
'    End Sub

'    Public Function UpdateBar() As Boolean
'        Return _TActual Mod _tUpdate = 0
'    End Function
'End Class