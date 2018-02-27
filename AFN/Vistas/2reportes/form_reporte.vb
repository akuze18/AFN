Imports System.Threading
Imports Excel = Microsoft.Office.Interop.Excel

Namespace Vistas.Reportes
    Public Class form_reporte


        ''' <summary>
        ''' inicializo la conexion a la base de datos con el INI de configuracion 
        ''' </summary>
        ''' <remarks></remarks>
        Private base As New base_AFN

        Private datos As form_reporte_dato

        Dim oExcel As Excel.Application
        Dim oBook As Excel.Workbook

#Region "Funciones del Formulario"
        Private Sub form_reporte_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
            form_welcome.Show()
        End Sub
        Private Sub form_reporte_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Dim datos As DataTable
            datos = base.lista_periodo(True)
            With cb_fecha
                .DataSource = datos
                .ValueMember = datos.Columns(0).ColumnName
                .DisplayMember = datos.Columns(1).ColumnName
                .SelectedIndex = 0
            End With
            datos = base.ZONAS_GL_T()
            With cb_zona
                .DataSource = datos
                .ValueMember = datos.Columns(0).ColumnName
                .DisplayMember = datos.Columns(1).ColumnName
                .SelectedIndex = 0
            End With
            datos = base.CLASE_consulta
            With cb_clase
                .DataSource = datos
                .ValueMember = datos.Columns(0).ColumnName
                .DisplayMember = datos.Columns(1).ColumnName
                .SelectedIndex = 0
            End With

            For Each primero As valores In moneda.Items
                Dim menu1 As New ToolStripMenuItem
                With menu1
                    .Text = primero.texto
                    .Tag = primero.codigo
                    For Each segundo As valores In ambiente.Items(primero.codigo)
                        Dim menu2 As New ToolStripMenuItem
                        With menu2
                            .Text = segundo.texto
                            .Tag = segundo.codigo
                            If primero.codigo = "MIX" Then
                                AddHandler .Click, AddressOf menu_especial_Click
                            End If
                            For Each tercero As valores In vista.Items(segundo.codigo, primero.codigo)
                                Dim menu3 As New ToolStripMenuItem
                                With menu3
                                    .Text = tercero.texto
                                    .Tag = tercero.codigo
                                    AddHandler .Click, AddressOf menus_Click
                                End With
                                .DropDownItems.Add(menu3)
                            Next
                        End With
                        .DropDownItems.Add(menu2)
                    Next
                End With
                M0.Items.Add(menu1)
            Next
        End Sub
        Private Function validar_form() As Boolean
            If cb_fecha.SelectedIndex = -1 Then
                MsgBox("Debe ingresar una fecha", vbCritical, "Activo Fijo NH FOODS CHILE")
                cb_fecha.Focus()
                validar_form = False
                Exit Function
            End If
            If cb_zona.SelectedIndex = -1 Then
                MsgBox("Debe indicar la zona", vbCritical, "Activo Fijo NH FOODS CHILE")
                cb_zona.Focus()
                validar_form = False
                Exit Function
            End If
            If cb_clase.SelectedIndex = -1 Then
                MsgBox("Debe indicar la clase", vbCritical, "Activo Fijo NH FOODS CHILE")
                cb_clase.Focus()
                validar_form = False
                Exit Function
            End If
            validar_form = True
        End Function
#End Region

#Region "funciones de la barra de menus"
        Private Sub menus_Click(sender As System.Object, e As System.EventArgs)
            If TypeOf sender Is ToolStripMenuItem Then
                Dim primero, segundo, tercero As ToolStripMenuItem
                tercero = CType(sender, ToolStripMenuItem)
                segundo = tercero.OwnerItem
                primero = segundo.OwnerItem

                Dim ambiente As base_AFN.BAmbiente
                Dim moneda As base_AFN.BMoneda
                Dim vista As form_reporte_dato.BVista
                Dim per_sol As Vperiodo
                Dim fe_sol As Date

                moneda = [Enum].Parse(GetType(base_AFN.BMoneda), primero.Tag)
                ambiente = [Enum].Parse(GetType(base_AFN.BAmbiente), segundo.Tag)
                vista = [Enum].Parse(GetType(form_reporte_dato.BVista), tercero.Tag)
                fe_sol = cb_fecha.SelectedValue
                per_sol = New Vperiodo(fe_sol.Year, fe_sol.Month)

                datos = New form_reporte_dato(per_sol, cb_clase.SelectedItem, cb_zona.SelectedItem, ambiente, moneda, vista)
                If datos.vista = form_reporte_dato.BVista.D Or datos.vista = form_reporte_dato.BVista.DI Then
                    generar_detalle()
                ElseIf datos.vista = form_reporte_dato.BVista.C Or datos.vista = form_reporte_dato.BVista.Z Then
                    generar_resumen()
                End If


            End If
        End Sub
        Private Sub menu_especial_Click(sender As System.Object, e As System.EventArgs)
            If TypeOf sender Is ToolStripMenuItem Then
                Dim primero, segundo As ToolStripMenuItem
                segundo = CType(sender, ToolStripMenuItem)
                primero = segundo.OwnerItem

                Dim moneda As base_AFN.BMoneda
                Dim reporte As String
                Dim per_sol As Vperiodo
                Dim fe_sol As Date

                moneda = [Enum].Parse(GetType(base_AFN.BMoneda), primero.Tag)
                reporte = segundo.Tag
                fe_sol = cb_fecha.SelectedValue
                per_sol = New Vperiodo(fe_sol.Year, fe_sol.Month)

                datos = New form_reporte_dato(per_sol, cb_clase.SelectedItem, cb_zona.SelectedItem, moneda, reporte)
                If datos.vista = form_reporte_dato.BVista.D Or datos.vista = form_reporte_dato.BVista.DI Then
                    generar_detalle()
                ElseIf datos.vista = form_reporte_dato.BVista.C Or datos.vista = form_reporte_dato.BVista.Z Then
                    generar_resumen()
                End If


            End If
        End Sub

#End Region

#Region "Funciones del Proceso"
        Protected Friend Sub generar_detalle()
            Dim dato_proceso As DataTable
            Dim ER As Integer
            bloquearW(Me)
            Dim avance As ProgressShow = cargar_barra(Me)
            avance.inicializar(3)
            'PRIMER PROCESO: Calcular Data
            avance.cambiar_parte(0, 20)
            avance.definir_proceso(0, 120)  'duración estimada de 120 segundos/ 2 minutos
            dato_proceso = calcular_AF_detalle(avance)
            'SEGUNDO PROCESO: Imprimir Excel
            avance.inicia_proceso(1)       'restablece inicio de segundo proceso
            avance.cambiar_parte(1, 70)
            avance.definir_proceso(1, dato_proceso.Columns.Count)
            ER = imprimir_AF_detalle(dato_proceso, avance, 1)
            'TERCER PROCESO: Formatear Hoja de Excel para imprimir
            formato_pagina(1)
            avance.continua_proceso()
            oExcel.Visible = True
            desbloquearW(Me)
            avance.termina_proceso()
            descargar_barra(Me)
        End Sub
        Protected Friend Sub generar_resumen()
            Dim dato_proceso As DataTable
            Dim ER As Integer
            bloquearW(Me)
            Dim avance As ProgressShow = cargar_barra(Me)
            avance.inicializar(3)
            'PRIMER PROCESO: Calcular Data
            avance.cambiar_parte(0, 20)
            avance.definir_proceso(0, 2)  'duración estimada de 2 segundos
            dato_proceso = calcular_AF_resumen(avance)
            'SEGUNDO PROCESO: Imprimir en Excel la información
            avance.inicia_proceso(1)
            avance.cambiar_parte(1, 70)
            avance.definir_proceso(1, dato_proceso.Rows.Count)
            ER = imprimir_AF_resumen(dato_proceso, avance, 1)
            'avance.continua_proceso()
            'TERCER PROCESO: Formatear hoja de Excel
            formato_pagina(1)
            avance.continua_proceso()
            oExcel.Visible = True
            desbloquearW(Me)
            avance.termina_proceso()
            descargar_barra(Me)
        End Sub
        Protected Friend Function calcular_AF_detalle(ByRef avance As ProgressShow) As DataTable
            'Creamos una instancia de la clase multi hilo
            Dim cmh As BackProcess
            Dim colchon As DataTable

            If datos.ambiente = base_AFN.BAmbiente.IFRS Then
                cmh = base.back_REPORTE_VIG_IFRS_DET
            Else
                If datos.vista = form_reporte_dato.BVista.DI Then
                    cmh = base.back_REPORTE_VIG_INV_DET
                Else
                    cmh = base.back_REPORTE_VIG_CONT_DET
                End If
            End If

            avance.definir_proceso(0, cmh.intervalos)

            'seteamos los campos que normalmente pasariamos como parametros

            If datos.ambiente = base_AFN.BAmbiente.IFRS Then
                cmh.ini_REPORTE_VIG_IFRS_DET(datos.periodo, datos.moneda, datos.cod_zona, datos.cod_clase)
            Else
                If datos.vista = form_reporte_dato.BVista.DI Then
                    cmh.ini_REPORTE_VIG_INV_DET(datos.periodo, datos.moneda, datos.cod_zona, datos.cod_clase)
                Else
                    cmh.ini_REPORTE_VIG_CONT_DET(datos.periodo, datos.ambiente, datos.moneda, datos.cod_zona, datos.cod_clase)
                End If
            End If

            'Esperamos a que termine la ejecucion del hilo
            While cmh.isWorking
                cmh.Keep()
                If cmh.UpdateBar Then
                    avance.continua_proceso(0)
                End If
            End While
            colchon = cmh.resultado

            Return colchon
        End Function
        Protected Friend Function calcular_AF_resumen(ByRef avance As ProgressShow) As DataTable
            'Creamos una instancia de la clase multi hilo
            Dim cmh As BackProcess
            Dim colchon As DataTable
            cmh = base.back_REPORTE_VIG_RESUMEN
            avance.definir_proceso(0, cmh.intervalos)

            'seteamos los campos que normalmente pasariamos como parametros
            cmh.ini_REPORTE_VIG_RESUMEN(datos)

            'Esperamos a que termine la ejecucion del hilo
            While cmh.isWorking
                cmh.Keep()
                If cmh.UpdateBar Then
                    avance.continua_proceso(0)
                End If
            End While
            colchon = cmh.resultado

            Return colchon
        End Function
        Protected Friend Function imprimir_AF_detalle(ByRef valores As DataTable, ByRef avance As ProgressShow, ByVal inicio As Integer) As Integer
            'Dim oExcelBack As Excel.Application
            Dim oSheet As Excel.Worksheet
            'oExcelBack = New Excel.Application
            Try
                oExcel = GetObject("Excel.Application")
            Catch ex As Exception
                oExcel = CreateObject("Excel.Application")
            End Try
            oBook = oExcel.Workbooks.Add
            Dim dato_proceso As DataTable
            Dim pagina, ini_titulo, fila_actual As Integer
            Dim mtit As String
            For pagina = oBook.Sheets.Count To 2 Step -1
                oBook.Sheets(pagina).Delete()
            Next
            oSheet = oBook.Sheets(pagina)
            dato_proceso = valores
            ini_titulo = inicio
            'ingresamos cabecera
            Select Case datos.tipo
                Case "F"
                    mtit = "Financiero"
                Case "T"
                    mtit = "Tributario"
                Case "Y"
                    mtit = "Financiero (YEN)"
                Case "GC"
                    mtit = "IFRS"
                Case "GY"
                    mtit = "IFRS(YEN)"
                Case Else
                    mtit = ""
            End Select

            oSheet.Cells(ini_titulo, 4) = UCase(mtit)
            oSheet.Cells(ini_titulo, 1) = "Fecha Cierre:"
            oSheet.Cells(ini_titulo, 2) = datos.fecha_corta
            ini_titulo = ini_titulo + 1
            oSheet.Cells(ini_titulo, 1) = "Zona:"
            oSheet.Cells(ini_titulo, 2) = datos.dsc_zona
            ini_titulo = ini_titulo + 1
            oSheet.Cells(ini_titulo, 1) = "Clase:"
            oSheet.Cells(ini_titulo, 2) = datos.dsc_clase
            ini_titulo = ini_titulo + 2
            'fin cabecera
            Application.DoEvents()
            'formato titulos
            With oSheet.Rows(ini_titulo & ":" & ini_titulo)
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter        '-4108 equivale a centrar
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .WrapText = True                    'equivale a alinear contenido a la celda
                .Orientation = 0
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .RowHeight = 30                     'alto de fila se estable en 30
                .Font.Bold = True
            End With
            'fin formato titulos

            Dim tipo_col(dato_proceso.Columns.Count - 1) As String
            Dim col_titulo As String
            Dim Tmatrix(,) As String
            Dim Nmatrix(,) As Double
            Dim Imatrix(,) As Integer
            Dim DMatrix(,) As Date
            ReDim Tmatrix(dato_proceso.Rows.Count - 1, 0)
            ReDim Nmatrix(dato_proceso.Rows.Count - 1, 0)
            ReDim DMatrix(dato_proceso.Rows.Count - 1, 0)
            ReDim Imatrix(dato_proceso.Rows.Count - 1, 0)
            fila_actual = ini_titulo + 1        'fila siguiente a los titulos del reporte
            For j = 0 To dato_proceso.Columns.Count - 1
                col_titulo = dato_proceso.Columns(j).ColumnName
                tipo_col(j) = dato_proceso.Columns(j).DataType.Name
                For i = 0 To dato_proceso.Rows.Count - 1
                    Dim filt As DataRow = dato_proceso.Rows(i)
                    Select Case tipo_col(j)
                        Case "Int32", "Int16"
                            Imatrix(i, 0) = filt.Item(j)
                        Case "Decimal", "Double", "Int64"
                            Nmatrix(i, 0) = filt.Item(j)
                        Case "String"
                            Tmatrix(i, 0) = filt.Item(j).ToString
                        Case "DateTime"
                            DMatrix(i, 0) = filt.Item(j)
                    End Select
                    Application.DoEvents()
                Next
                oSheet.Cells(ini_titulo, j + 1).Value = col_titulo
                Select Case tipo_col(j)
                    Case "Int32", "Int16"
                        oSheet.Columns(lcol(j + 1) + ":" + lcol(j + 1)).NumberFormat = "#,##0;[red]-#,##0"
                        oSheet.Range(lcol(j + 1) + fila_actual.ToString + ":" + lcol(j + 1) + CStr(dato_proceso.Rows.Count + fila_actual - 1)).Value = Imatrix
                    Case "Int64"
                        oSheet.Columns(lcol(j + 1) + ":" + lcol(j + 1)).NumberFormat = "#,##0;[red]-#,##0"
                        oSheet.Range(lcol(j + 1) + fila_actual.ToString + ":" + lcol(j + 1) + CStr(dato_proceso.Rows.Count + fila_actual - 1)).Value = Nmatrix
                    Case "Decimal", "Double"
                        oSheet.Columns(lcol(j + 1) + ":" + lcol(j + 1)).NumberFormat = "#,##0;[red]-#,##0"
                        oSheet.Range(lcol(j + 1) + fila_actual.ToString + ":" + lcol(j + 1) + CStr(dato_proceso.Rows.Count + fila_actual - 1)).Value = Nmatrix
                    Case "String"
                        oSheet.Columns(lcol(j + 1) + ":" + lcol(j + 1)).NumberFormat = "@"
                        oSheet.Range(lcol(j + 1) + fila_actual.ToString + ":" + lcol(j + 1) + CStr(dato_proceso.Rows.Count + fila_actual - 1)).Value = Tmatrix
                    Case "DateTime"
                        oSheet.Columns(lcol(j + 1) + ":" + lcol(j + 1)).NumberFormat = "dd-MM-yyyy"
                        oSheet.Range(lcol(j + 1) + fila_actual.ToString + ":" + lcol(j + 1) + CStr(dato_proceso.Rows.Count + fila_actual - 1)).Value = DMatrix
                End Select
                Application.DoEvents()
                avance.continua_proceso()
            Next

            'formato general
            oSheet.Columns("B:B").NumberFormat = "#;"
            Select Case datos.tipo
                Case "F", "T", "Y"
                    oSheet.Columns("H:H").NumberFormat = "0.00;[red]-0.00"
            End Select
            oSheet.Columns("A:" + lcol(dato_proceso.Columns.Count)).EntireColumn.AutoFit()
            oSheet.Columns("C:C").ColumnWidth = 55
            'With oSheet.Columns("H:H")
            '    .ColumnWidth = 40
            '    .WrapText = False
            '    .ShrinkToFit = True
            'End With
            Application.DoEvents()
            'fin formato general
            imprimir_AF_detalle = fila_actual + 1
        End Function
        Protected Friend Function imprimir_AF_resumen(ByRef valores As DataTable, ByRef avance As ProgressShow, ByVal inicio As Integer) As Integer
            Dim oSheet As Excel.Worksheet
            oExcel = New Excel.Application
            oBook = oExcel.Workbooks.Add
            Dim dato_proceso As DataTable
            Dim pagina, ini_titulo, fila_actual, col_fija As Integer
            For pagina = oBook.Sheets.Count To 2 Step -1
                oBook.Sheets(pagina).Delete()
            Next
            oSheet = oBook.Sheets(pagina)
            dato_proceso = valores
            ini_titulo = inicio
            'inicio titulos
            If datos.vista = form_reporte_dato.BVista.C Then
                oSheet.Cells(ini_titulo, 1) = "Clase de " + Environment.NewLine + "Activo"
                oSheet.Cells(ini_titulo, 2) = "Nombre" + Environment.NewLine + "Zona"
            Else    'aca es Z
                oSheet.Cells(ini_titulo, 1) = "Nombre" + Environment.NewLine + "Zona"
                oSheet.Cells(ini_titulo, 2) = "Clase de " + Environment.NewLine + "Activo"
            End If
            oSheet.Cells(ini_titulo, 3) = "Nombre" + Environment.NewLine + "Lugar"
            col_fija = 3
            If datos.moneda = base_AFN.BMoneda.MIX Then
                oSheet.Cells(ini_titulo, col_fija + 1) = "Valor Activo" + Environment.NewLine + "Financiero"
                oSheet.Cells(ini_titulo, col_fija + 2) = "Depreciación" + Environment.NewLine + "Ejercicio" + Environment.NewLine + "Financiero"
                oSheet.Cells(ini_titulo, col_fija + 3) = "Depreciación" + Environment.NewLine + "Acumulada" + Environment.NewLine + "Financiero"
                oSheet.Cells(ini_titulo, col_fija + 4) = "Valor Activo" + Environment.NewLine + "IFRS CLP"
                oSheet.Cells(ini_titulo, col_fija + 5) = "Depreciación" + Environment.NewLine + "Ejercicio" + Environment.NewLine + "IFRS CLP"
                oSheet.Cells(ini_titulo, col_fija + 6) = "Depreciación" + Environment.NewLine + "Acumulada" + Environment.NewLine + "IFRS CLP"
                oSheet.Cells(ini_titulo, col_fija + 7) = "Valor Activo" + Environment.NewLine + "IFRS YEN"
                oSheet.Cells(ini_titulo, col_fija + 8) = "Depreciación" + Environment.NewLine + "Ejercicio" + Environment.NewLine + "IFRS YEN"
                oSheet.Cells(ini_titulo, col_fija + 9) = "Depreciación" + Environment.NewLine + "Acumulada" + Environment.NewLine + "IFRS YEN"
            Else
                If datos.ambiente = base_AFN.BAmbiente.IFRS Then
                    oSheet.Cells(ini_titulo, col_fija + 1) = "Valor Inicial"
                    oSheet.Cells(ini_titulo, col_fija + 2) = "Credito" + Environment.NewLine + "adiciones"
                    oSheet.Cells(ini_titulo, col_fija + 3) = "Valor de " + Environment.NewLine + "Activo Fijo"
                    oSheet.Cells(ini_titulo, col_fija + 4) = "Dep. Acum" + Environment.NewLine + "Anterior"
                    oSheet.Cells(ini_titulo, col_fija + 5) = "Valor" + Environment.NewLine + "Residual"
                    oSheet.Cells(ini_titulo, col_fija + 6) = "Depreciación" + Environment.NewLine + "del Ejercicio"
                    oSheet.Cells(ini_titulo, col_fija + 7) = "Depreciación" + Environment.NewLine + "Acumulada"
                    oSheet.Cells(ini_titulo, col_fija + 8) = "Revalorización"
                    oSheet.Cells(ini_titulo, col_fija + 9) = "Valor Libro" + Environment.NewLine + "del Activo"
                Else
                    oSheet.Cells(ini_titulo, col_fija + 1) = "Valor Inicial"
                    oSheet.Cells(ini_titulo, col_fija + 2) = "C Monetaria Activo"
                    oSheet.Cells(ini_titulo, col_fija + 3) = "Credito" + Environment.NewLine + "adiciones"
                    oSheet.Cells(ini_titulo, col_fija + 4) = "Valor de " + Environment.NewLine + "Activo Fijo"
                    oSheet.Cells(ini_titulo, col_fija + 5) = "Dep. Acum" + Environment.NewLine + "Anterior"
                    oSheet.Cells(ini_titulo, col_fija + 6) = "C. Monetaria" + Environment.NewLine + "Dep. Acum."
                    oSheet.Cells(ini_titulo, col_fija + 7) = "Valor" + Environment.NewLine + "Residual"
                    oSheet.Cells(ini_titulo, col_fija + 8) = "Depreciación" + Environment.NewLine + "del Ejercicio"
                    oSheet.Cells(ini_titulo, col_fija + 9) = "Depreciación" + Environment.NewLine + "Acumulada"
                    oSheet.Cells(ini_titulo, col_fija + 10) = "Valor Libro" + Environment.NewLine + "del Activo"
                End If
            End If
            'fin titulos
            'formato titulos
            Dim alto As Integer
            If datos.moneda = base_AFN.BMoneda.MIX Then
                alto = 45
            Else
                alto = 30
            End If
            With oSheet.Rows(ini_titulo & ":" & ini_titulo)
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter        '-4108 equivale a centrar
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .WrapText = True                    'equivale a alinear contenido a la celda
                .Orientation = 0
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .RowHeight = alto                     'alto de fila se estable en 30
                .Font.Bold = True
            End With
            Application.DoEvents()
            'fin todo lo de titulos
            fila_actual = ini_titulo + 2        'fila siguiente a los titulos del reporte
            For i = 0 To dato_proceso.Rows.Count - 1
                Application.DoEvents()
                If dato_proceso.Rows(i).Item(dato_proceso.Columns.Count - 2) = "99999" Then
                    fila_actual = fila_actual + 1
                    If dato_proceso.Rows(i).Item(dato_proceso.Columns.Count - 3) = "99999" Then
                        With oSheet.Range("A" & fila_actual & ":" & lcol(dato_proceso.Columns.Count - 3) & fila_actual)
                            .Interior.Color = 16764057
                            With .Borders(Excel.XlBordersIndex.xlEdgeTop)            'xlEdgeTop
                                .ColorIndex = 0
                                .TintAndShade = 0
                                .Weight = 2
                            End With
                            With .Borders(Excel.XlBordersIndex.xlEdgeBottom)            'xlEdgeBottom
                                .ColorIndex = 0
                                .TintAndShade = 0
                                .Weight = 3
                            End With
                        End With
                    Else
                        With oSheet.Range("A" & fila_actual & ":" & lcol(dato_proceso.Columns.Count - 3) & fila_actual)
                            .Interior.Color = 5296274
                            With .Borders(Excel.XlBordersIndex.xlEdgeTop)            'xlEdgeTop
                                .ColorIndex = 0
                                .TintAndShade = 0
                                .Weight = 2
                            End With
                            With .Borders(Excel.XlBordersIndex.xlEdgeBottom)            'xlEdgeBottom
                                .ColorIndex = 0
                                .TintAndShade = 0
                                .Weight = 3
                            End With
                        End With
                    End If
                    '       fila_actual = fila_actual + 1
                End If
                For j = 1 To dato_proceso.Columns.Count - 3
                    oSheet.Cells(fila_actual, j) = Trim(dato_proceso.Rows(i).Item(j - 1)) 'Format(, "0")
                    Application.DoEvents()
                Next
                If dato_proceso.Rows(i).Item(dato_proceso.Columns.Count - 2) = "99999" Then
                    fila_actual = fila_actual + 1
                End If
                fila_actual = fila_actual + 1
                avance.continua_proceso()
            Next
            'formato general
            oSheet.Columns("D:M").NumberFormat = "#" + getSeparadorMil() + "###" + getSeparadorList() + "[red]-#" + getSeparadorMil() + "###" + getSeparadorList() + "0"
            'oSheet.Columns("D:D").ColumnWidth = 55
            oSheet.Columns("A:M").EntireColumn.AutoFit()
            Application.DoEvents()
            With oSheet.Range("A" + CStr(ini_titulo) + ":" + lcol(dato_proceso.Columns.Count - 3) + CStr(ini_titulo))
                With .Borders(Excel.XlBordersIndex.xlEdgeLeft)            'xlEdgeLeft
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = 3
                End With
                With .Borders(Excel.XlBordersIndex.xlEdgeTop)            'xlEdgeTop
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = 3
                End With
                With .Borders(Excel.XlBordersIndex.xlEdgeBottom)            'xlEdgeBottom
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = 3
                End With
                With .Borders(Excel.XlBordersIndex.xlEdgeRight)           'xlEdgeRight
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = 3
                End With
                With .Borders(Excel.XlBordersIndex.xlInsideVertical)           'xlInsideVertical
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = 3
                End With
                With .Borders(Excel.XlBordersIndex.xlInsideHorizontal)           'xlInsideHorizontal
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = 3
                End With
            End With
            Application.DoEvents()
            'fin formato general
            imprimir_AF_resumen = fila_actual + 1
        End Function

        Public Sub formato_pagina(indx_pag As Integer)
            Dim Mostrar, periodo, Tipo, tipo2, nom_pag, pies As String
            Select Case datos.tipo
                Case "F"
                    Tipo = "FINANCIERO"
                    tipo2 = "(CLP)"
                Case "T"
                    Tipo = "TRIBUTARIO"
                    tipo2 = "(CLP)"
                Case "I", "GC"
                    Tipo = "IFRS"
                    tipo2 = "(CLP)"
                Case "Y"
                    Tipo = "FINANCIERO EN YENES"
                    tipo2 = "(YEN)"
                Case "W", "GY"
                    Tipo = "IFRS EN YENES"
                    tipo2 = "(YEN)"
                Case "ESP1"
                    Tipo = "PARA PACKAGE"
                    tipo2 = ""
                Case Else
                    Tipo = "N/N"
                    tipo2 = "N/N"
            End Select
            Select Case datos.vista
                Case form_reporte_dato.BVista.D      'detalle
                    Mostrar = "DETALLE ACTIVO FIJO " + Tipo
                    periodo = "HASTA " + datos.fecha_corta
                    nom_pag = "DETALLE_" + datos.tipo + datos.fecha_eng
                    pies = ""
                Case form_reporte_dato.BVista.C     'resumen clase
                    Mostrar = "RESUMEN ACTIVO FIJO " + Tipo & Chr(13) & "AGRUPADO POR CLASES"
                    periodo = "HASTA " + datos.fecha_corta
                    nom_pag = "RES_CLA_" + datos.tipo + datos.fecha_eng
                    pies = ""
                Case form_reporte_dato.BVista.Z      'resumen zonas
                    Mostrar = "RESUMEN ACTIVO FIJO " + Tipo & Chr(13) & "AGRUPADO POR ZONAS"
                    periodo = "HASTA " + datos.fecha_corta
                    nom_pag = "RES_ZON_" + datos.tipo + datos.fecha_eng
                    pies = ""
                Case form_reporte_dato.BVista.CM     'cuadro de movimiento
                    Mostrar = "RESUMEN ANALITICO " + Tipo & Chr(13)
                    periodo = datos.periodo.mostrar
                    nom_pag = "RCM_" + datos.tipo + datos.periodo.ToString
                    pies = ""
                Case form_reporte_dato.BVista.FA     'reporte de fixed assets
                    Mostrar = "REPORT FIXED ASSETS " + datos.periodo.last.Year.ToString + tipo2 & Chr(13)
                    periodo = datos.periodo.mostrar
                    nom_pag = "FixedAssets_" + datos.tipo + datos.periodo.ToString
                    pies = ""
                Case Else       'por defecto
                    Mostrar = "ACTIVO FIJO " + Tipo
                    periodo = "HASTA " + datos.fecha_corta
                    nom_pag = "AF_" + datos.fecha_eng
                    pies = ""
            End Select
            Application.DoEvents()
            oBook.Sheets(indx_pag).Name = nom_pag

            If datos.vista <> form_reporte_dato.BVista.D Then


                oBook.Sheets(indx_pag).PageSetup.LeftHeaderPicture.FileName = base.fileLogo

                Application.DoEvents()
                With oBook.Sheets(indx_pag).PageSetup
                    .PrintTitleRows = "$1:$2"
                    .Orientation = 2
                    '.BlackAndWhite = False
                    .Zoom = False
                    .FitToPagesWide = 1
                    .FitToPagesTall = 4
                    Try
                        .PaperSize = 14
                    Catch ex As Exception
                        'de momento dejamos pasar error hasta saber como controlarlo bien
                    End Try
                    .LeftHeader = "&G"
                    .CenterHeader = Mostrar
                    .RightHeader = periodo
                    .LeftFooter = form_welcome.GetUsuario + Chr(13) + "&P / &N"
                    .CenterFooter = pies
                    .RightFooter = "Fecha de Impresion : &D" & Chr(13) & "Hora de Impresion: &T"
                    .TopMargin = oExcel.InchesToPoints(1.01)
                    .CenterHorizontally = True
                End With
            End If
            Application.DoEvents()
        End Sub
#End Region

    End Class

    Public Class form_reporte_dato
        Public Enum BVista
            D = 1       'Detalle Contabilidad
            Z = 2       'Resumen por Zona
            C = 3       'Resumen por Clase
            DI = 4      'Detalle Inventario
            CM = 5      'Cuadro Movimiento
            FA = 6      'Fixed Assets
        End Enum

        Private _periodo As Vperiodo
        Private _cod_clase, _dsc_clase As String
        Private _cod_zona, _dsc_zona As String
        Private _ambiente As base_AFN.BAmbiente
        Private _moneda As base_AFN.BMoneda
        Private _tipo As String
        Private _vista As BVista

        Public Sub New(ByVal periodo As Vperiodo, ByVal clase As DataRowView, ByVal zona As DataRowView, ByVal ambiente As base_AFN.BAmbiente, ByVal moneda As base_AFN.BMoneda, ByVal vista As BVista)
            _periodo = periodo
            _cod_clase = clase(0)
            _dsc_clase = clase(1)
            _cod_zona = zona(0)
            _dsc_zona = zona(1)
            _ambiente = ambiente
            _moneda = moneda
            _vista = vista
            Select Case _ambiente
                Case base_AFN.BAmbiente.FIN
                    Select Case _moneda
                        Case base_AFN.BMoneda.CLP
                            _tipo = "F"
                        Case base_AFN.BMoneda.YEN
                            _tipo = "Y"
                        Case Else
                            _tipo = ""
                    End Select
                Case base_AFN.BAmbiente.IFRS
                    Select Case _moneda
                        Case base_AFN.BMoneda.CLP
                            _tipo = "GC"
                        Case base_AFN.BMoneda.YEN
                            _tipo = "GY"
                        Case Else
                            _tipo = ""
                    End Select
                Case base_AFN.BAmbiente.TRIB
                    Select Case _moneda
                        Case base_AFN.BMoneda.CLP
                            _tipo = "T"
                        Case base_AFN.BMoneda.YEN
                            'Aun no esta disponible
                            _tipo = ""
                        Case Else
                            _tipo = ""
                    End Select
            End Select
        End Sub
        Public Sub New(ByVal periodo As Vperiodo, ByVal clase As DataRowView, ByVal zona As DataRowView, ByVal moneda As base_AFN.BMoneda, ByVal especial As String)
            _periodo = periodo
            _cod_clase = clase(0)
            _dsc_clase = clase(1)
            _cod_zona = zona(0)
            _dsc_zona = zona(1)
            _ambiente = base_AFN.BAmbiente.FIN
            _moneda = moneda
            _vista = BVista.C
            _tipo = especial
        End Sub

        Public ReadOnly Property cod_clase As String
            Get
                Return _cod_clase
            End Get
        End Property
        Public ReadOnly Property dsc_clase As String
            Get
                Return _dsc_clase
            End Get
        End Property
        Public ReadOnly Property cod_zona As String
            Get
                Return _cod_zona
            End Get
        End Property
        Public ReadOnly Property dsc_zona As String
            Get
                Return _dsc_zona
            End Get
        End Property
        Public ReadOnly Property tipo As String
            Get
                Return _tipo
            End Get
        End Property
        Public ReadOnly Property fecha As String
            Get
                Return _periodo.lastDB
            End Get
        End Property

        Public ReadOnly Property fecha_eng As String
            Get
                Return _periodo.last.ToString("yyyy-MM-dd")
            End Get
        End Property

        Public ReadOnly Property fecha_corta As String
            Get
                Return _periodo.last.ToString("dd/MM/yyyy")
            End Get
        End Property

        Public ReadOnly Property ambiente As base_AFN.BAmbiente
            Get
                Return _ambiente
            End Get
        End Property

        Public ReadOnly Property vista As BVista
            Get
                Return _vista
            End Get
        End Property

        Public ReadOnly Property periodo As Vperiodo
            Get
                Return _periodo
            End Get
        End Property

        Public ReadOnly Property moneda As base_AFN.BMoneda
            Get
                Return _moneda
            End Get
        End Property

    End Class
End Namespace

