Imports Excel = Microsoft.Office.Interop.Excel

Public Class form_Cmov
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private maestro As New master_control

    Private base As New base_AFN

    Protected Friend Structure dato_mov
        Public Tperiodo As String
        Public Topcion As String
        Public Ttipo As String
    End Structure
    Dim DFmov As dato_mov
    Dim colchon As DataTable        'generico para la clase, para ser utilizado en consulta por BGworker
    Dim consulta1 As String         'generico para la clase, para ser utilizado en consulta por BGworker
    Dim oExcel As Excel.Application
    Dim oBook As Excel.Workbook


    'funciones del formulario
    Private Sub form_Cmov_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub
    Private Sub form_Cmov_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim Ftope, Fmin As Date
        Dim Tperiodo As New DataTable
        Tperiodo.Columns.Add("periodo")
        Tperiodo.Columns.Add("descrip")
        Fmin = DateSerial(2012, 1, 1)
        Ftope = DateAdd(DateInterval.Month, -1, DateSerial(Year(Now), Month(Now), 1))
        While Ftope >= Fmin
            Dim dr As DataRow = Tperiodo.NewRow
            dr(0) = (Year(Ftope) * 100 + Month(Ftope)).ToString
            dr(1) = Strings.StrConv(Format(Ftope, "MMMM yyyy"), vbProperCase)
            Tperiodo.Rows.Add(dr)
            Ftope = DateAdd(DateInterval.Month, -1, Ftope)
        End While
        cb_fecha.DataSource = Tperiodo
        cb_fecha.ValueMember = "periodo"
        cb_fecha.DisplayMember = "descrip"
        cb_fecha.SelectedIndex = 0

        Dim sql_consist As String
        Dim TTipo As New DataTable
        cboTipo.Items.Clear()
        sql_consist = "SELECT DISTINCT consistencia FROM AFN_CLASE"
        TTipo = maestro.ejecuta(sql_consist)
        cboTipo.DataSource = TTipo
        cboTipo.DisplayMember = "consistencia"
        cboTipo.ValueMember = "consistencia"
        cboTipo.SelectedIndex = 0
    End Sub
    Private Function validar_formulario() As Boolean
        If cb_fecha.SelectedIndex = -1 Then
            MsgBox("Debe seleccionar un periodo para mostrar", vbExclamation, "NH FOODS CHILE")
            cb_fecha.Focus()
            validar_formulario = False
            Exit Function
        End If
        If cboTipo.SelectedIndex = -1 Then
            MsgBox("Debe seleccionar un tipo para mostrar", vbExclamation, "NH FOODS CHILE")
            cboTipo.Focus()
            validar_formulario = False
            Exit Function
        End If
        validar_formulario = True
        Exit Function
    End Function

    'funciones del menu
    Private Sub MMF1_Click(sender As System.Object, e As System.EventArgs) Handles MMF1.Click
        If validar_formulario() Then
            DFmov.Tperiodo = cb_fecha.SelectedValue
            DFmov.Ttipo = cboTipo.SelectedValue
            DFmov.Topcion = "F"
            Call generar_cuadro()
        End If
    End Sub
    Private Sub MMT1_Click(sender As System.Object, e As System.EventArgs) Handles MMT1.Click
        If validar_formulario() Then
            DFmov.Tperiodo = cb_fecha.SelectedValue
            DFmov.Ttipo = cboTipo.SelectedValue
            DFmov.Topcion = "T"
            Call generar_cuadro()
        End If
    End Sub
    Private Sub MMI1_Click(sender As System.Object, e As System.EventArgs) Handles MMI1.Click
        If validar_formulario() Then
            DFmov.Tperiodo = cb_fecha.SelectedValue
            DFmov.Ttipo = cboTipo.SelectedValue
            DFmov.Topcion = "GC"
            Call generar_cuadro()
        End If
    End Sub
    Private Sub MMI2_Click(sender As System.Object, e As System.EventArgs) Handles MMI2.Click
        If validar_formulario() Then
            DFmov.Tperiodo = cb_fecha.SelectedValue
            DFmov.Ttipo = cboTipo.SelectedValue
            DFmov.Topcion = "GY"
            Call generar_cuadro()
        End If
    End Sub

    'funciones del proceso
    Private Sub generar_cuadro()
        Dim dato_proceso As DataTable
        Dim oHoja As Excel.Worksheet
        bloquearW(Me)
        Dim avance As ProgressShow = cargar_barra(Me)
        avance.inicializar(3)
        'PRIMER PROCESO: Calcular Data
        avance.cambiar_parte(0, 15)
        avance.definir_proceso(0, 1)  'duración estimada de 1 segundos
        dato_proceso = calcular_AF_cuadro(avance)

        'SEGUNDO PROCESO: Imprimir en Excel la información
        avance.cambiar_parte(1, 60)
        avance.definir_proceso(1, dato_proceso.Columns.Count)
        oHoja = imprimir_AF_cuadro(dato_proceso, avance, 1)
        avance.continua_proceso()

        'TERCER PROCESO: Formatear hoja de Excel
        formato_pagina(oHoja)
        avance.continua_proceso()

        desbloquearW(Me)
        oExcel.Visible = True
        avance.termina_proceso()
        descargar_barra(Me)
    End Sub
    Private Function calcular_AF_cuadro(ByRef avance As ProgressShow) As DataTable
        Dim fecha_inicial, fecha_final As Date
        fecha_inicial = DateSerial(CInt(Strings.Left(DFmov.Tperiodo, 4)), CInt(Strings.Right(DFmov.Tperiodo, 2)), 1)
        fecha_final = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, fecha_inicial))
        'reestablesco fecha inicial al primero del año, ya que el cuadro de movimiento siempre es acumulado
        fecha_inicial = DateSerial(CInt(Strings.Left(DFmov.Tperiodo, 4)), 1, 1)

        If DFmov.Topcion = "GC" Or DFmov.Topcion = "GY" Then
            consulta1 = "SELECT descripcion 'Cuenta'," + Chr(13) + _
                        "saldoI_act 'Saldo Inicial'," + Chr(13) + _
                        "adiciones+inc_construc 'Adiciones'," + Chr(13) + _
                        "Fobra 'Desde Obras en Construccion'," + Chr(13) + _
                        "dec_to_activo 'Hacia Activo Fijo'," + Chr(13) + _
                        "credito 'Credito'," + Chr(13) + _
                        "cast_act+dec_to_gasto 'Castigos'," + Chr(13) + _
                        "vent_act 'Ventas'," + Chr(13) + _
                        "revaloriza 'Revalorizacion'," + Chr(13) + _
                        "saldoF_act 'Saldo Final'," + Chr(13) + _
                        "descripcion '*Cuenta'," + Chr(13) + _
                        "saldoI_dep '-Saldo Inicial'," + Chr(13) + _
                        "dep_eje 'Dep. Ejercicio'," + Chr(13) + _
                        "cast_dep '-Castigos'," + Chr(13) + _
                        "vent_dep '-Ventas'," + Chr(13) + _
                        "deterioro 'Deterioro'," + Chr(13) + _
                        "saldoF_dep '-Saldo Final'," + Chr(13) + _
                        "'' '_blank1'," + Chr(13) + _
                        "'' '_blank2'," + Chr(13) + _
                        "valor_neto 'Valor Neto'" + Chr(13) + _
                        "FROM AFN_CUADRO_MOV_IFRS2('" + Format(fecha_inicial, "yyyyMMdd") + "','" + Format(fecha_final, "yyyyMMdd") + "','" + DFmov.Topcion + "') WHERE tipo='" + DFmov.Ttipo + "' ORDER BY CLASE"
        Else
            consulta1 = "SELECT descripcion 'Cuenta'," + Chr(13) + _
                        "saldoI_act 'Saldo Inicial'," + Chr(13) + _
                        "adiciones 'Adiciones'," + Chr(13) + _
                        "Fobra 'Desde Obras en Construccion'," + Chr(13) + _
                        "BajaObra 'Hacia Activo Fijo'," + Chr(13) + _
                        "corr_mon_act 'C Mon'," + Chr(13) + _
                        "credito 'Credito'," + Chr(13) + _
                        "cast_act 'Castigos'," + Chr(13) + _
                        "vent_act 'Ventas'," + Chr(13) + _
                        "saldoF_act 'Saldo Final'," + Chr(13) + _
                        "descripcion '*Cuenta'," + Chr(13) + _
                        "saldoI_dep '-Saldo Inicial'," + Chr(13) + _
                        "dep_eje 'Dep. Ejercicio'," + Chr(13) + _
                        "corr_mon_dep '-C Mon'," + Chr(13) + _
                        "cast_dep '-Castigos'," + Chr(13) + _
                        "vent_dep '-Ventas'," + Chr(13) + _
                        "saldoF_dep '-Saldo Final'," + Chr(13) + _
                        "'' '_blank1'," + Chr(13) + _
                        "'' '_blank2'," + Chr(13) + _
                        "valor_neto 'Valor Neto'" + Chr(13) + _
                        "FROM AFN_CUADRO_MOV2('" + Format(fecha_inicial, "yyyyMMdd") + "','" + Format(fecha_final, "yyyyMMdd") + "','" + DFmov.Topcion + "') WHERE tipo='" + DFmov.Ttipo + "' ORDER BY CLASE"
        End If
        If Not IsNothing(colchon) Then
            colchon.Dispose()
        End If
        Dim Tini As Date
        Dim SegTrans, SegActual As Integer
        Tini = Now
        SegTrans = 0
        BGQuery.RunWorkerAsync()
        While BGQuery.IsBusy
            'Proceso principal espera a subproceso hasta que termine
            SegActual = DateDiff(DateInterval.Second, Tini, Now)
            If SegTrans < SegActual Then
                avance.continua_proceso(0)
                SegTrans = SegActual
            End If
            Application.DoEvents()
        End While
        Return colchon
    End Function
    Private Function imprimir_AF_cuadro(ByVal valores As DataTable, ByRef avance As ProgressShow, ByVal inicio As Integer) As Excel.Worksheet
        Dim oSheet As Excel.Worksheet
        oExcel = New Excel.Application
        oBook = oExcel.Workbooks.Add
        Dim dato_proceso As DataTable
        Dim pagina, fila_actual, columna_actual, columna_max As Integer
        Dim titulo, tipo, letra_col As String

        For pagina = oBook.Sheets.Count To 2 Step -1
            oBook.Sheets(pagina).Delete()
        Next
        oSheet = oBook.Sheets(pagina)
        dato_proceso = valores
        Dim Tmatrix(,) As String
        Dim Nmatrix(,) As Double
        Dim Imatrix(,) As Integer
        Dim DMatrix(,) As Date
        ReDim Tmatrix(dato_proceso.Rows.Count - 1, 0)
        ReDim Nmatrix(dato_proceso.Rows.Count - 1, 0)
        ReDim DMatrix(dato_proceso.Rows.Count - 1, 0)
        ReDim Imatrix(dato_proceso.Rows.Count - 1, 0)
        'oExcel.Visible = True
        fila_actual = inicio
        columna_actual = 1
        columna_max = columna_actual
        letra_col = "A"     'para eliminar advertencia uso de variable sin asignar valor
        For i = 0 To dato_proceso.Columns.Count - 1
            titulo = dato_proceso.Columns(i).ColumnName
            tipo = dato_proceso.Columns(i).DataType.Name
            'asterisco es el caracter indicador para marcar reinicio de posiciones
            If Strings.Left(titulo, 1).Equals("*") Then
                columna_actual = 1
                fila_actual = fila_actual + dato_proceso.Rows.Count + 2
                titulo = Strings.Mid(titulo, 2)
            End If
            'actualizo columna maxima utilizada
            If columna_actual > columna_max Then
                columna_max = columna_actual
            End If
            'signo menos es el caracter para diferenciar campos con igual titulo
            If Strings.Left(titulo, 1).Equals("-") Then
                titulo = Strings.Mid(titulo, 2)
            End If
            'titulos _blank no se muestran
            If Strings.Left(titulo, 6).Equals("_blank") Then
                titulo = ""
            End If
            letra_col = lcol(columna_actual)
            For j = 0 To dato_proceso.Rows.Count - 1
                Dim filt As DataRow = dato_proceso.Rows(j)
                'relleno matris con información segun el tipo de dato
                Select Case tipo
                    Case "Int32", "Int16"
                        Imatrix(j, 0) = filt.Item(i)
                    Case "Decimal", "Double", "Int64"
                        Nmatrix(j, 0) = filt.Item(i)
                    Case "String"
                        Tmatrix(j, 0) = filt.Item(i).ToString
                    Case "DateTime"
                        DMatrix(j, 0) = filt.Item(i)
                End Select
            Next
            'Defino Cabera de Columna
            Dim tRango As Excel.Range = oSheet.Cells(fila_actual, columna_actual)
            With tRango
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .Value = titulo
                .Font.Bold = True                   'negria la letra
                .RowHeight = 30                     'alto de fila se estable en 30
                .WrapText = True
                With .Borders(Excel.XlBordersIndex.xlEdgeBottom)
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = 3
                End With
            End With
            Dim oRango, oTotal As Excel.Range
            'Establesco valores y formato para los datos
            oRango = oSheet.Range(letra_col + CStr(fila_actual + 1) + ":" + letra_col + CStr(dato_proceso.Rows.Count + fila_actual))
            Select Case tipo
                Case "Int32", "Int16"
                    oRango.NumberFormat = "#,##0;[red]-#,##0"
                    oRango.Value = Imatrix
                Case "Int64"
                    oRango.NumberFormat = "#,##0;[red]-#,##0"
                    oRango.Value = Nmatrix
                Case "Decimal", "Double"
                    oRango.NumberFormat = "#,##0;[red]-#,##0"
                    oRango.Value = Nmatrix
                Case "String"
                    oRango.NumberFormat = "@"
                    oRango.Value = Tmatrix
                Case "DateTime"
                    oRango.NumberFormat = "dd-MM-yyyy"
                    oRango.Value = DMatrix
            End Select
            'Establesco formato para total de columna
            oTotal = oSheet.Range(letra_col + CStr(dato_proceso.Rows.Count + fila_actual))
            With oTotal
                With .Borders(Excel.XlBordersIndex.xlEdgeBottom)
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = Excel.XlBorderWeight.xlThin
                    .LineStyle = Excel.XlLineStyle.xlDouble
                End With
                With .Borders(Excel.XlBordersIndex.xlEdgeTop)
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = Excel.XlBorderWeight.xlHairline
                End With
            End With
            columna_actual = columna_actual + 1
            avance.continua_proceso()
        Next
        oSheet.Columns("B:" + lcol(columna_max)).NumberFormat = "#,###;[red]-#,###;0"
        oSheet.Columns("A:A").EntireColumn.AutoFit()
        oSheet.Columns("B:" + lcol(columna_max)).ColumnWidth = 14
        Return oSheet
    End Function
    Private Sub formato_pagina(ByRef oSheet As Excel.Worksheet)
        Dim Mostrar, periodo, Titulo1, Titulo2, nom_pag, dir_logo As String
        Dim fecha_cm As Date
        Select Case DFmov.Topcion
            Case "F"
                Titulo1 = "FINANCIERO"
                Titulo2 = "(CLP)"
            Case "T"
                Titulo1 = "TRIBUTARIO"
                Titulo2 = "(CLP)"
            Case "I", "GC"
                Titulo1 = "IFRS"
                Titulo2 = "(CLP)"
            Case "Y"
                Titulo1 = "FINANCIERO EN YENES"
                Titulo2 = "(YEN)"
            Case "W", "GY"
                Titulo1 = "IFRS EN YENES"
                Titulo2 = "(YEN)"
            Case Else
                Titulo1 = "N/N"
                Titulo2 = "N/N"
        End Select

        'Case "CM"     'cuadro de movimiento
        fecha_cm = DateSerial(CInt(Strings.Left(DFmov.Tperiodo, 4)), CInt(Strings.Right(DFmov.Tperiodo, 2)), 1)
        Mostrar = "RESUMEN ANALITICO " + Titulo1 + " " + Titulo2 + Chr(13)
        periodo = Format(fecha_cm, "MMMM yyyy")
        nom_pag = "RCM_" + DFmov.Topcion + Format(fecha_cm, "yyyy-MM")
        'pies = ""

        oSheet.Name = nom_pag
        oExcel.ScreenUpdating = False
        oExcel.Calculation = Excel.XlCalculation.xlCalculationManual
        dir_logo = base.fileLogo
        oSheet.PageSetup.LeftHeaderPicture.Filename = dir_logo
        With oSheet.PageSetup
            '.PrintTitleRows = "$1:$2"
            .Orientation = Excel.XlPageOrientation.xlLandscape
            '.BlackAndWhite = False
            '.Zoom = 110
            .FitToPagesWide = 1
            .FitToPagesTall = 1
            Try
                .PaperSize = Excel.XlPaperSize.xlPaperFolio
            Catch ex As Exception
                'solo pasar el error, buscar un manejo mas correcto para este caso
            End Try

            .LeftHeader = "&G"
            .CenterHeader = Mostrar
            .RightHeader = periodo
            .LeftFooter = form_welcome.GetUsuario + Chr(13) + "&P / &N"
            '.CenterFooter = pies
            .RightFooter = "Fecha de Impresion : &D" & Chr(13) & "Hora de Impresion: &T"
            .TopMargin = oExcel.InchesToPoints(1.01)
            .CenterHorizontally = True
        End With
        oExcel.Calculation = Excel.XlCalculation.xlCalculationAutomatic
        oExcel.ScreenUpdating = True
    End Sub

    'ejecucion de consultas en segundo plano
    Private Sub BGQuery_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BGQuery.DoWork
        colchon = maestro.ejecuta(consulta1)
    End Sub
End Class