Imports Excel = Microsoft.Office.Interop.Excel

Public Class form_FixAsset
    Protected Friend Structure dato_fixed
        Public Tperiodo As String
        Public Tmoneda As String
        Public Ttipo As String
    End Structure
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private maestro As New master_control

    Private base As New base_AFN

    Dim DFfix As dato_fixed
    Dim colchon As DataTable        'generico para la clase, para ser utilizado en consulta por BGworker
    Dim consulta1 As String         'generico para la clase, para ser utilizado en consulta por BGworker
    Dim oExcel As Excel.Application
    Dim oBook As Excel.Workbook


    'funciones del formulario
    Private Sub form_FixAsset_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub
    Private Sub form_FixAsset_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ''Esta opción se quita de la pantalla, ya que el reporte ha quedado obsoleto por problemas en el
        'manejo de la corrección monetaria en las diferentes columnas que lo componen
        Op1.Visible = False

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
            MessageBox.Show("Debe seleccionar un periodo para mostrar", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cb_fecha.Focus()
            validar_formulario = False
            Exit Function
        End If
        If cboTipo.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar un tipo para mostrar", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboTipo.Focus()
            validar_formulario = False
            Exit Function
        End If
        If Not (Op1.Checked Or Op2.Checked) Then
            MessageBox.Show("Debe indicar una moneda", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            MarcoMoneda.Focus()
            validar_formulario = False
            Exit Function
        End If
        validar_formulario = True
        Exit Function
    End Function


    'generar proceso
    Private Sub btn_calcular_Click(sender As System.Object, e As System.EventArgs) Handles btn_calcular.Click
        If validar_formulario() Then
            DFfix.Tperiodo = cb_fecha.SelectedValue
            DFfix.Ttipo = cboTipo.SelectedValue
            If Op1.Checked Then
                DFfix.Tmoneda = Op1.Tag
            ElseIf Op2.Checked Then
                DFfix.Tmoneda = Op2.Tag
            End If
            generar_cuadro()
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
        avance.definir_proceso(0, 3)  'duración estimada de 3 segundos
        dato_proceso = calcular_AF_fixed(avance)

        'SEGUNDO PROCESO: Imprimir en Excel la información
        avance.cambiar_parte(1, 60)
        avance.definir_proceso(1, dato_proceso.Columns.Count)
        oHoja = imprimir_AF_fixed(dato_proceso, avance, 1)
        avance.continua_proceso()

        'TERCER PROCESO: Formatear hoja de Excel
        formato_pagina(oHoja)
        avance.continua_proceso()

        desbloquearW(Me)
        oExcel.Visible = True
        avance.termina_proceso()
        descargar_barra(Me)
    End Sub
    Private Function calcular_AF_fixed(ByRef avance As ProgressShow) As DataTable
        Dim fecha_inicial, fecha_final As Date
        fecha_inicial = DateSerial(CInt(Strings.Left(DFfix.Tperiodo, 4)), CInt(Strings.Right(DFfix.Tperiodo, 2)), 1)
        fecha_final = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, fecha_inicial))
        'reestablesco fecha inicial al primero del año, ya que el cuadro de movimiento siempre es acumulado
        fecha_inicial = DateSerial(CInt(Strings.Left(DFfix.Tperiodo, 4)), 1, 1)

        If DFfix.Tmoneda = "GC" Or DFfix.Tmoneda = "GY" Then
            consulta1 = "Definir una data para fixed assets para yenes"
        ElseIf DFfix.Tmoneda = "LC" Then
            consulta1 = "SELECT descripcion 'PACKING'," + Chr(13) + _
                        "saldoI_act 'BEGINNING'," + Chr(13) + _
                        "adiciones 'ACQUISITION FROM OTHER'," + Chr(13) + _
                        "Fobra 'FROM CONTRUCTION IN PROGRESS'," + Chr(13) + _
                        "inc_construc 'INCREASE CONSTRUCTION'," + Chr(13) + _
                        "dec_to_activo+dec_to_gasto 'DECREASE OF CONSTRUCTION TO ASSETS'," + Chr(13) + _
                        "cast_act 'DISPOSAL'," + Chr(13) + _
                        "vent_act 'SALES'," + Chr(13) + _
                        "credito 'DECREASE OTHER 4%'," + Chr(13) + _
                        "saldoF_act 'AT END'," + Chr(13) + _
                        "descripcion '*PACKING'," + Chr(13) + _
                        "saldoI_dep '-BEGINNING'," + Chr(13) + _
                        "dep_eje 'INCREASE (DEPRECIATION)'," + Chr(13) + _
                        "cast_dep '-DISPOSAL'," + Chr(13) + _
                        "vent_dep 'DECREASE (SALES) OTHER'," + Chr(13) + _
                        "saldoF_dep '-AT END'," + Chr(13) + _
                        "valor_neto 'BALANCE'" + Chr(13) + _
                        "FROM AFN_cuadro_mov_fix2('" + fecha_inicial.ToString("yyyyMMdd") + "','" + fecha_final.ToString("yyyyMMdd") + "','F') WHERE tipo='" + DFfix.Ttipo + "' ORDER BY CLASE"
        Else
            consulta1 = "SELECT descripcion 'PACKING'," + Chr(13) + _
                        "saldoI_act 'BEGINNING'," + Chr(13) + _
                        "adiciones 'ACQUISITION FROM OTHER'," + Chr(13) + _
                        "desde_cep 'FROM CONTRUCTION IN PROGRESS'," + Chr(13) + _
                        "incre_con 'INCREASE CONSTRUCTION'," + Chr(13) + _
                        "decre_con 'DECREASE OF CONSTRUCTION TO ASSETS'," + Chr(13) + _
                        "cast_act 'DISPOSAL'," + Chr(13) + _
                        "vent_act 'SALES'," + Chr(13) + _
                        "ajusteA 'OTHER'," + Chr(13) + _
                        "credito 'DECREASE OTHER 4%'," + Chr(13) + _
                        "saldoF_act 'AT END'," + Chr(13) + _
                        "descripcion '*PACKING'," + Chr(13) + _
                        "saldoI_dep '-BEGINNING'," + Chr(13) + _
                        "dep_eje 'INCREASE (DEPRECIATION)'," + Chr(13) + _
                        "cast_dep '-DISPOSAL'," + Chr(13) + _
                        "vent_dep 'DECREASE (SALES) OTHER'," + Chr(13) + _
                        "ajusteD '-OTHER'," + Chr(13) + _
                        "saldoF_dep '-AT END'," + Chr(13) + _
                        "valor_neto 'BALANCE'" + Chr(13) + _
                        "FROM AFN_fixed_assets2('" + Strings.Left(DFfix.Tperiodo, 4) + "','" + Strings.Right(DFfix.Tperiodo, 2) + "','" + DFfix.Tmoneda + "') WHERE tipo='" + DFfix.Ttipo + "' ORDER BY CLASE"
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
    Private Function imprimir_AF_fixed(ByVal valores As DataTable, ByRef avance As ProgressShow, ByVal inicio As Integer) As Excel.Worksheet
        Dim oSheet As Excel.Worksheet
        Try
            oExcel = GetObject("Excel.Application")
        Catch ex As Exception
            oExcel = CreateObject("Excel.Application")
        End Try
        oBook = oExcel.Workbooks.Add
        Dim dato_proceso As DataTable
        Dim pagina, fila_actual, columna_actual, last_col, max_col As Integer
        Dim titulo, tipo, letra_col, letra_col2 As String

        For pagina = oBook.Sheets.Count To 2 Step -1
            oBook.Sheets(pagina).Delete()
        Next
        oSheet = oBook.Sheets(pagina)
        dato_proceso = valores
        fila_actual = inicio

        Dim Tmatrix(,) As String
        Dim Nmatrix(,) As Double
        Dim Imatrix(,) As Integer
        Dim DMatrix(,) As Date
        ReDim Tmatrix(dato_proceso.Rows.Count - 1, 0)
        ReDim Nmatrix(dato_proceso.Rows.Count - 1, 0)
        ReDim DMatrix(dato_proceso.Rows.Count - 1, 0)
        ReDim Imatrix(dato_proceso.Rows.Count - 1, 0)
        'oExcel.Visible = True
        letra_col = "A"     'para eliminar advertencia uso de variable sin asignar valor
        letra_col2 = "B"
        last_col = 0
        columna_actual = 1
        For i = 0 To dato_proceso.Columns.Count - 1
            titulo = dato_proceso.Columns(i).ColumnName
            tipo = dato_proceso.Columns(i).DataType.Name
            If Strings.Left(titulo, 1).Equals("*") Then
                'asterisco es el caracter indicador para marcar reinicio de posiciones
                max_col = columna_actual - 1
                last_col = 0
                columna_actual = 1
                fila_actual = fila_actual + dato_proceso.Rows.Count + 2
                titulo = Strings.Mid(titulo, 2)
            End If
            If Strings.Left(titulo, 1).Equals("-") Then
                'signo menos es el caracter para diferenciar campos con igual titulo
                titulo = Strings.Mid(titulo, 2)
            End If
            If dato_proceso.Columns.Count - 1 = i Then
                columna_actual = max_col
            End If
            letra_col = lcol(columna_actual)
            letra_col2 = lcol(last_col + 1)
            For j = 0 To dato_proceso.Rows.Count - 1
                Dim filt As DataRow = dato_proceso.Rows(j)
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
                Application.DoEvents()
            Next
            oSheet.Cells(fila_actual, columna_actual).Value = titulo
            With oSheet.Range(letra_col2 + CStr(fila_actual) + ":" + letra_col + CStr(fila_actual))
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                .WrapText = True                    'equivale a alinear contenido a la celda
                .Font.Bold = True                   'negria la letra
                .RowHeight = 30                     'alto de fila se estable en 30
                With .Borders(Excel.XlBordersIndex.xlEdgeBottom)
                    .ColorIndex = 0
                    .TintAndShade = 0
                    .Weight = 3
                End With
            End With
            Dim oRango, oTotal As Excel.Range
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
            oTotal = oSheet.Range(letra_col2 + CStr(dato_proceso.Rows.Count + fila_actual) + ":" + letra_col + CStr(dato_proceso.Rows.Count + fila_actual))
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
            last_col = columna_actual
            columna_actual = columna_actual + 1
            avance.continua_proceso()
        Next
        oSheet.Columns("B:" + letra_col).NumberFormat = "#,###;[red]-#,###;0"
        oSheet.Columns("A:A").EntireColumn.AutoFit()
        oSheet.Columns("B:" + letra_col).ColumnWidth = 14.7
        Return oSheet
    End Function
    Private Sub formato_pagina(ByRef oSheet As Excel.Worksheet)
        Dim Mostrar, periodo, Titulo1, Titulo2, nom_pag, dir_logo As String
        Dim fecha_cm As Date
        Select Case DFfix.Tmoneda
            Case "LC"
                Titulo1 = "FINANCIERO LOCAL"
                Titulo2 = "(CLP)"
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

        'Case "FA"     'reporte de fixed assets
        fecha_cm = DateSerial(CInt(Strings.Left(DFfix.Tperiodo, 4)), CInt(Strings.Right(DFfix.Tperiodo, 2)), 1)
        Mostrar = "REPORT FIXED ASSETS " + Strings.Left(DFfix.Tperiodo, 4) + " " + Titulo2 + Chr(13)
        periodo = Format(fecha_cm, "MMMM yyyy")
        nom_pag = "FixedAssets_" + DFfix.Tmoneda + Format(fecha_cm, "yyyy-MM")
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
            .Zoom = 80
            '.FitToPagesWide = 1
            '.FitToPagesTall = 1
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