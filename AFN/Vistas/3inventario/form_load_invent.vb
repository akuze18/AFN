Imports Excel = Microsoft.Office.Interop.Excel
Public Class form_load_invent

#Region "Variables de clase"
    ''' <summary>
    ''' Instancia del forumario que contiene toda la logica del proceso
    ''' </summary>
    ''' <remarks></remarks>
    Private base As New base_AFN
#End Region

#Region "Formulario"
    Private Sub form_load_invent_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub
    Private Sub form_load_invent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Cargo combo de fechas de inventario
        With cboFecha
            .DataSource = base.lista_fecha_inv
            .ValueMember = .DataSource.Columns(0).ColumnName
            .DisplayMember = .DataSource.Columns(1).ColumnName
            .SelectedIndex = -1
        End With
    End Sub
#End Region

#Region "Pestaña Descargar"
    Private Sub cboFecha_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboFecha.SelectedIndexChanged
        If cboFecha.SelectedIndex > -1 And TypeOf cboFecha.SelectedValue Is DateTime Then
            Dim fechaI As Date
            fechaI = cboFecha.SelectedValue
            'CARGAR COMBO DE ZONAS
            Dim colchon As DataTable
            colchon = base.lista_zona_inv(fechaI, False)
            With cboZona
                .ValueMember = colchon.Columns(0).ColumnName
                .DisplayMember = colchon.Columns(1).ColumnName
                .DataSource = colchon
                .SelectedIndex = -1
            End With
        Else
            cboZona.DataSource = Nothing
        End If
        cboClase.DataSource = Nothing
    End Sub
    Private Sub cboZona_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboZona.SelectedIndexChanged
        If cboZona.SelectedIndex <> -1 Then
            Dim fechaI As Date = cboFecha.SelectedValue
            Dim zona As String = cboZona.SelectedValue
            'CARGAR COMBO DE CLASES
            Dim colchon As DataTable
            colchon = base.lista_clase_inv(fechaI, zona, False)
            With cboClase
                .ValueMember = colchon.Columns(0).ColumnName
                .DisplayMember = colchon.Columns(1).ColumnName
                .DataSource = colchon
                .SelectedIndex = -1
            End With
        End If
    End Sub
    Private Function validarF() As Boolean
        Dim resultado As Boolean
        resultado = True
        If cboZona.SelectedIndex = -1 And resultado Then
            Mensaje_IT("Debe seleccionar una zona")
            cboZona.Focus()
            resultado = False
        End If
        If cboClase.SelectedIndex = -1 And resultado Then
            Mensaje_IT("Debe seleccionar una clase")
            cboClase.Focus()
            resultado = False
        End If
        If cboFecha.SelectedIndex = -1 And resultado Then
            Mensaje_IT("Debe seleccionar una fecha")
            cboFecha.Focus()
            resultado = False
        End If
        Return resultado
    End Function

    Private Sub btn_download_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_download.Click

        If Not validarF() Then
            Exit Sub
        End If
        'Declarar variables y establecer valores iniciales
        Dim FultimoC As Date
        Dim colchon, cojin As DataTable
        Dim zona, clase, TXTzona, TXTclase As String
        Dim Separador As String
        Dim i, j, hojasN As Integer
        Dim ini_titulo, estadoI, estadoF, subzonaI, subzonaF, ubicacionI, ubicacionF, fila_actual, fila_ini, fila_fin As Integer
        Dim H1name, nombre_total As String
        Dim fecha1, fecha2 As String
        bloquearW(Me)
        zona = Trim(cboZona.SelectedValue)
        clase = Trim(cboClase.SelectedValue)
        TXTzona = cboZona.Text
        TXTclase = cboClase.Text
        FultimoC = cboFecha.SelectedValue
        Separador = getSeparadorList()
        Dim fbp As ProgressShow = cargar_barra(Me)
        fbp.inicializar(1)
        colchon = base.lista_inv_foto(FultimoC, zona, clase)
        fbp.definir_proceso(0, 5 + colchon.Rows.Count)
        'preparo variables Excel
        Dim objExcel As New Excel.Application
        'objExcel.Visible = True
        Dim libro As Excel.Workbook = objExcel.Workbooks.Add
        hojasN = 2      'cantidad de hojas necesarias para el proceso
        i = libro.Sheets.Count
        If i > hojasN Then
            For j = i To hojasN + 1 Step -1
                libro.Sheets(j).Delete()
            Next
        End If
        If i < hojasN Then
            For j = 1 To hojasN - 1 Step 1
                libro.Sheets.Add()
            Next
        End If
        Dim pagina, codifi As Excel.Worksheet
        pagina = libro.Sheets(1)  'pagina del listado
        codifi = libro.Sheets(2)  'pagina con codificacion
        ini_titulo = 1
        'ingresamos tablas de codidificacion
        'ESTADOS
        codifi.Cells(ini_titulo, 1) = "ESTADOS"
        cojin = base.estado_existencia
        estadoI = ini_titulo + 1
        estadoF = estadoI
        For Each fila As DataRow In cojin.Rows
            codifi.Cells(estadoF, 1).Value = fila("cod").ToString
            codifi.Cells(estadoF, 2).Value = fila("dscrpt").ToString
            estadoF = estadoF + 1
        Next
        cojin = Nothing
        estadoF = estadoF - 1
        'SUBZONAS
        codifi.Cells(ini_titulo, 3) = "SUBZONAS"
        cojin = base.SUBZONAS_ACT(zona)
        subzonaI = ini_titulo + 1
        subzonaF = subzonaI
        For Each fila As DataRow In cojin.Rows
            codifi.Cells(subzonaF, 3) = fila("cod").ToString
            codifi.Cells(subzonaF, 4) = fila("descrip").ToString
            subzonaF = subzonaF + 1
        Next
        cojin = Nothing
        subzonaF = subzonaF - 1
        'UBICACIONES
        codifi.Cells(ini_titulo, 5) = "UBICACIONES"
        cojin = base.ubicacion
        ubicacionI = ini_titulo + 1
        ubicacionF = ubicacionI
        For Each fila As DataRow In cojin.Rows
            codifi.Cells(ubicacionF, 5) = fila("cod").ToString
            codifi.Cells(ubicacionF, 6) = fila("descrip").ToString
            ubicacionF = ubicacionF + 1
        Next
        cojin = Nothing
        ubicacionF = ubicacionF - 1
        codifi.Name = "COD"
        codifi.Columns("A:F").EntireColumn.AutoFit()
        codifi.Visible = False
        'fin tablas de codificacion
        fbp.continua_proceso()
        'ingresamos cabecera de pagina principal
        pagina.Cells(ini_titulo, 2) = "Zona:"
        pagina.Cells(ini_titulo, 3) = zona + " - " + TXTzona
        ini_titulo = ini_titulo + 1
        pagina.Cells(ini_titulo, 2) = "Clase:"
        pagina.Cells(ini_titulo, 3) = clase + " - " + TXTclase
        ini_titulo = ini_titulo + 1
        fecha2 = Now.ToString("dd-MM-yyyy")
        fecha1 = DateAdd(DateInterval.Year, -2, Now).ToString("dd-MM-yyyy")
        pagina.Cells(ini_titulo, 2) = "Fecha Inv.:"
        With pagina.Range(lcol(3) + CStr(ini_titulo) + ":" + lcol(4) + CStr(ini_titulo))
            .NumberFormat = "dd-mm-yyyy;@"
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
            With .Interior
                .Pattern = Excel.XlPattern.xlPatternSolid
                .PatternColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic
                .Color = 5296274
                .TintAndShade = 0
                .PatternTintAndShade = 0
            End With
            .Merge()
            .Formula = "=DATE(" + FultimoC.Year.ToString + "," + FultimoC.Month.ToString + "," + FultimoC.Day.ToString + ")"
            .Locked = True
        End With
        'pagina.Cells(ini_titulo, 5) = "(DD-MM-AAAA)"
        ini_titulo = ini_titulo + 2
        'fin cabecera
        Application.DoEvents()
        'inicio titulos
        pagina.Cells(ini_titulo, 1) = "Nº"
        pagina.Cells(ini_titulo, 2) = "Código"
        pagina.Cells(ini_titulo, 3) = "Código" + Chr(10) + "Lote"
        pagina.Cells(ini_titulo, 4) = "Descripción"
        pagina.Cells(ini_titulo, 5) = "Fecha Compra"
        pagina.Cells(ini_titulo, 6) = "Valor" + Chr(10) + "Compra"
        pagina.Cells(ini_titulo, 7) = "Valor" + Chr(10) + "Libro"
        pagina.Cells(ini_titulo, 8) = "Codigo" + Chr(10) + "Estado"
        pagina.Cells(ini_titulo, 9) = "Estado" + Chr(10) + "Actual"
        pagina.Cells(ini_titulo, 10) = "Codigo" + Chr(10) + "Subzona"
        pagina.Cells(ini_titulo, 11) = "Subzona" + Chr(10) + "Actual"
        pagina.Cells(ini_titulo, 12) = "CODIGO" + Chr(10) + "UBICACION"
        pagina.Cells(ini_titulo, 13) = "Ubicacion" + Chr(10) + "Actual"
        pagina.Cells(ini_titulo, 14) = "OBSERVACIÓN (máximo 100 caracteres, incluyendo espacios)"
        'fin titulos
        'formato titulos
        With pagina.Rows(ini_titulo & ":" & ini_titulo)
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
            .WrapText = True                    'equivale a alinear contenido a la celda
            .ShrinkToFit = False
            .RowHeight = 30                     'alto de fila se estable en 30
            .Font.Bold = True
            .AutoFilter()
        End With
        Application.DoEvents()
        'fin todo lo de titulos
        fbp.continua_proceso()
        fila_actual = ini_titulo + 1

        For Each fila As DataRow In colchon.Rows
            Application.DoEvents()
            With pagina.Rows(CStr(fila_actual) + ":" + CStr(fila_actual))
                .RowHeight = .RowHeight * 2
                .VerticalAlignment = Excel.XlVAlign.xlVAlignTop
                .WrapText = True                'equivale a alinear contenido a la celda
                .ShrinkToFit = False
            End With
            pagina.Cells(fila_actual, 1) = CStr(fila("row"))
            pagina.Cells(fila_actual, 2).NumberFormat = "@"
            pagina.Cells(fila_actual, 2) = CStr(fila("producto"))
            pagina.Cells(fila_actual, 3) = CStr(fila("lote_articulo"))
            pagina.Cells(fila_actual, 4) = CStr(fila("descripcion"))
            Dim tmpF As Date = fila("fecha_compra")
            pagina.Cells(fila_actual, 5).FormulaLocal = "=FECHA(" + tmpF.Year.ToString + Separador + tmpF.Month.ToString + Separador + tmpF.Day.ToString + ")"
            pagina.Cells(fila_actual, 6).NumberFormat = "#,##0;[red]-#,##0;0"
            pagina.Cells(fila_actual, 6) = CStr(fila("valor_inicial"))
            pagina.Cells(fila_actual, 7).NumberFormat = "#,##0;[red]-#,##0;0"
            pagina.Cells(fila_actual, 7) = CStr(fila("val_libro"))
            Application.DoEvents()
            pagina.Cells(fila_actual, 9).FormulaLocal = "=SI.ERROR(BUSCARV(" + lcol(8) + CStr(fila_actual) + Separador + "COD!$A$" + CStr(estadoI) + ":$B$" + CStr(estadoF) + Separador + "2" + Separador + "0)" + Separador + """"")"
            pagina.Cells(fila_actual, 11).FormulaLocal = "=SI.ERROR(BUSCARV(" + lcol(10) + CStr(fila_actual) + Separador + "COD!$C$" + CStr(subzonaI) + ":$D$" + CStr(subzonaF) + Separador + "2" + Separador + "0)" + Separador + """"")"
            pagina.Cells(fila_actual, 13).FormulaLocal = "=SI.ERROR(BUSCARV(" + lcol(12) + CStr(fila_actual) + Separador + "COD!$E$" + CStr(ubicacionI) + ":$F$" + CStr(ubicacionF) + Separador + "2" + Separador + "0)" + Separador + """"")"
            fila_actual = fila_actual + 1
            fbp.continua_proceso()
            Application.DoEvents()
        Next
        'agregar validadores
        fila_ini = ini_titulo + 1
        fila_fin = fila_actual - 1
        'validar estado
        With pagina.Range(lcol(8) + CStr(fila_ini) + ":" + lcol(8) + CStr(fila_fin))
            With .Validation
                .Delete()
                .Add(Type:=Excel.XlDVType.xlValidateList, AlertStyle:=Excel.XlDVAlertStyle.xlValidAlertStop, Operator:=Excel.XlFormatConditionOperator.xlBetween, Formula1:="=COD!$A$" + CStr(estadoI) + ":$A$" + CStr(estadoF))
                .IgnoreBlank = True
                .InCellDropdown = True
                .ErrorTitle = "NH Foods Chile"
                .ErrorMessage = "Debe seleccionar un valor del listado"
                .ShowInput = True
                .ShowError = True
            End With
            .Locked = False
        End With
        'validar subzona
        With pagina.Range(lcol(10) + CStr(fila_ini) + ":" + lcol(10) + CStr(fila_fin))
            With .Validation
                .Delete()
                .Add(Type:=Excel.XlDVType.xlValidateList, AlertStyle:=Excel.XlDVAlertStyle.xlValidAlertStop, Operator:=Excel.XlFormatConditionOperator.xlBetween, Formula1:="=COD!$C$" + CStr(subzonaI) + ":$C$" + CStr(subzonaF))
                .IgnoreBlank = True
                .InCellDropdown = True
                .ErrorTitle = "NH Foods Chile"
                .ErrorMessage = "Debe seleccionar un valor del listado"
                .ShowInput = True
                .ShowError = True
            End With
            .Locked = False
        End With
        'validar ubicacion
        With pagina.Range(lcol(12) + CStr(fila_ini) + ":" + lcol(12) + CStr(fila_fin))
            With .Validation
                .Delete()
                .Add(Type:=Excel.XlDVType.xlValidateList, AlertStyle:=Excel.XlDVAlertStyle.xlValidAlertStop, Operator:=Excel.XlFormatConditionOperator.xlBetween, Formula1:="=COD!$E$" + CStr(ubicacionI) + ":$E$" + CStr(ubicacionF))
                .IgnoreBlank = True
                .InCellDropdown = True
                .ErrorTitle = "NH Foods Chile"
                .ErrorMessage = "Debe seleccionar un valor del listado"
                .ShowInput = True
                .ShowError = True
            End With
            .Locked = False
        End With
        'validar largo maximo de observacion
        With pagina.Range(lcol(14) + CStr(fila_ini) + ":" + lcol(14) + CStr(fila_fin))
            With .Validation
                .Delete()
                .Add(Type:=Excel.XlDVType.xlValidateTextLength, AlertStyle:=Excel.XlDVAlertStyle.xlValidAlertStop, Operator:=Excel.XlFormatConditionOperator.xlLessEqual, Formula1:="100")
                .IgnoreBlank = True
                .InCellDropdown = True
                .ErrorTitle = "NH Foods Chile"
                .ErrorMessage = "Celda no puede contener más de 100 caracteres"
                .ShowInput = True
                .ShowError = True
            End With
            .Locked = False
        End With
        fbp.continua_proceso()

        'formato general
        pagina.Columns("A").ColumnWidth = 4
        pagina.Columns("B").ColumnWidth = 14.4
        pagina.Columns("C").ColumnWidth = 8
        pagina.Columns("D").ColumnWidth = 55
        pagina.Columns("E").ColumnWidth = 12.6
        pagina.Columns("F").ColumnWidth = 12.6
        pagina.Columns("G").ColumnWidth = 8.2
        pagina.Columns("H").ColumnWidth = 10.7
        pagina.Columns("I").ColumnWidth = 18
        pagina.Columns("J").ColumnWidth = 10.7
        pagina.Columns("K").ColumnWidth = 18
        pagina.Columns("L").ColumnWidth = 10.7
        pagina.Columns("M").ColumnWidth = 20
        pagina.Columns("N").ColumnWidth = 32

        Application.DoEvents()
        With pagina.Range("A" + CStr(ini_titulo) + ":" + lcol(14) + CStr(fila_actual - 1))
            With .Borders(Excel.XlBordersIndex.xlEdgeLeft)
                .ColorIndex = 0
                .TintAndShade = 1
                .Weight = 2
            End With
            With .Borders(Excel.XlBordersIndex.xlEdgeTop)
                .ColorIndex = 0
                .TintAndShade = 1
                .Weight = 2
            End With
            With .Borders(Excel.XlBordersIndex.xlEdgeBottom)
                .ColorIndex = 0
                .TintAndShade = 1
                .Weight = 2
            End With
            With .Borders(Excel.XlBordersIndex.xlEdgeRight)
                .ColorIndex = 0
                .TintAndShade = 1
                .Weight = 2
            End With
            With .Borders(Excel.XlBordersIndex.xlInsideVertical)
                .ColorIndex = 0
                .TintAndShade = 1
                .Weight = 2
            End With
            With .Borders(Excel.XlBordersIndex.xlInsideHorizontal)
                .ColorIndex = 0
                .TintAndShade = 1
                .Weight = 2
            End With
        End With
        Application.DoEvents()
        'fin formato general
        fbp.continua_proceso()

        'formato hoja
        H1name = "INGRESO" + zona + "-" + clase
        pagina.Name = H1name
        codifi.Cells(1, 7).FormulaLocal = "=CONTAR('" + CStr(H1name) + "'!A:A)"
        nombre_total = base.fileLogo
        pagina.PageSetup.LeftHeaderPicture.Filename = nombre_total
        Application.DoEvents()
        With pagina.PageSetup
            .PrintTitleRows = "$1:$5"
            .Orientation = 2
            .Zoom = 60
            '.FitToPagesWide = 1
            '.FitToPagesTall = 4
            Try
                .PaperSize = 14
            Catch er_ex As Exception

            End Try
            .LeftHeader = "&G" + Chr(13) + "&8NH FOODS CHILE"
            .CenterHeader = "&""-,Negrita""&U&16INGRESO DE TOMA INVENTARIO DE ACTIVO FIJO"
            .RightHeader = "Fecha de Impresion : &D" & Chr(13) & "Hora de Impresion: &T" + Chr(13) + "Página &P de &N"
            '        .LeftFooter = ""
            '        .CenterFooter = "" 
            '        .RightFooter = ""
            '        .TopMargin = objExcel.InchesToPoints(1.01)
            .CenterHorizontally = True
        End With
        'fin formato hoja
        fbp.continua_proceso()
        Dim contraseña As String
        Dim objRnd As New Random(CInt(Now.Millisecond))
        contraseña = objRnd.Next(0, 99999).ToString
        contraseña = "aaa"
        pagina.Protect(DrawingObjects:=False, Contents:=True, Scenarios:=True _
            , AllowFiltering:=True, AllowFormattingCells:=True, AllowFormattingColumns:=True _
            , Password:=contraseña)
        descargar_barra(Me)
        desbloquearW(Me)
        objExcel.Visible = True
        objExcel = Nothing
        'MsgBox("Proceso terminado", vbInformation, "NH FOODS CHILE")

    End Sub
#End Region

#Region "Pestaña Cargar"
    Private Function validarF2() As Boolean
        Dim resultado As Boolean
        resultado = True
        If Tarchivof.Text = "" And resultado Then
            Mensaje_IT("Debe indicar la ubicación del archivo a cargar")
            btn_buscar.Focus()
            resultado = False
        End If
        If Dir(Tarchivof.Text) = "" And resultado Then
            Mensaje_IT("Nombre o Ubicación del archivo no existen")
            Tarchivof.SelectionStart = 0
            Tarchivof.SelectionLength = Len(Tarchivof.Text)
            btn_buscar.Focus()
            resultado = False
        End If
        validarF2 = resultado
    End Function
    Private Sub btn_buscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_buscar.Click
        dialogo.Title = "Elija el archivo"
        dialogo.Filter = "Planillas de Calculo (Excel)|*.xls;*.xlsx"
        dialogo.ShowDialog()
        If dialogo.FileName <> "" Then
            Tarchivof.Text = dialogo.FileName
        End If
    End Sub
    Private Sub btn_upload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_upload.Click
        If Not validarF2() Then
            Exit Sub
        End If
        bloquearW(Me)
        Dim fbp As ProgressShow = cargar_barra(Me)
        fbp.inicializar(1)
        fbp.definir_proceso(0, 10)
        Dim Archivo As String
        Dim Testado, Tsubzona, Tubicacion, Tobs, Tproducto As String
        Dim Titulos(14) As String
        Dim Existan, Detener As Boolean
        Dim Finventario As Date
        Dim Pinventario As Vperiodo
        Dim i, x, total_reg As Long
        Dim colchon As DataTable
        Archivo = Tarchivof.Text
        'validar archivo
        Dim objExcel As New Excel.Application
        Dim libro As Excel.Workbook = objExcel.Workbooks.Open(Archivo)
        Dim hoja_main As Excel.Worksheet
        hoja_main = libro.Sheets(1)
        Detener = False
        'identifico titulos (para asegurar que es el mismo archivo generado por el sistema)
        If hoja_main.Cells(1, 2).Value <> "Zona:" And Not Detener Then
            Mensaje_IT("Archivo ingresado no corresponde con el formato")
            Detener = True
        End If
        If hoja_main.Cells(2, 2).Value <> "Clase:" And Not Detener Then
            Mensaje_IT("Archivo ingresado no corresponde con el formato")
            Detener = True
        End If
        If hoja_main.Cells(3, 2).Value <> "Fecha Inv.:" And Not Detener Then
            Mensaje_IT("Archivo ingresado no corresponde con el formato")
            Detener = True
        End If
        Titulos(1) = "Nº"
        Titulos(2) = "Código"
        Titulos(3) = "Código" + Chr(10) + "Lote"
        Titulos(4) = "Descripción"
        Titulos(5) = "Fecha Compra"
        Titulos(6) = "Valor" + Chr(10) + "Compra"
        Titulos(7) = "Valor" + Chr(10) + "Libro"
        Titulos(8) = "Codigo" + Chr(10) + "Estado"
        Titulos(9) = "Estado" + Chr(10) + "Actual"
        Titulos(10) = "Codigo" + Chr(10) + "Subzona"
        Titulos(11) = "Subzona" + Chr(10) + "Actual"
        Titulos(12) = "CODIGO" + Chr(10) + "UBICACION"
        Titulos(13) = "Ubicacion" + Chr(10) + "Actual"
        Titulos(14) = "OBSERVACIÓN (máximo 100 caracteres, incluyendo espacios)"
        For i = 1 To 14
            If hoja_main.Cells(5, i).Value <> Titulos(i) And Not Detener Then
                Mensaje_IT("Archivo ingresado no corresponde con el formato")
                Detener = True
            End If
        Next
        If Not Detener Then
            total_reg = libro.Sheets(2).Cells(1, 7).Value
            fbp.definir_proceso(0, total_reg * 2 + 1)
        End If
        fbp.continua_proceso()
        'titulos ok, ahora valido contenido de celdas editables
        If hoja_main.Cells(3, 3).Value.ToString = "" And Not Detener Then
            Mensaje_IT("No ha ingresado la fecha del inventario en el archivo")
            Detener = True
            Pinventario = New Vperiodo(Now.Year, Now.Month)
        Else
            Finventario = hoja_main.Cells(3, 3).Value
            Pinventario = New Vperiodo(Finventario.Year, Finventario.Month)
        End If
        Existan = True
        x = 6   'desde esta fila comienzan los datos
        While Existan And Not Detener
            Testado = hoja_main.Cells(x, 9).Value
            Tsubzona = hoja_main.Cells(x, 11).Value
            Tubicacion = hoja_main.Cells(x, 13).Value
            Tobs = hoja_main.Cells(x, 14).Value
            If Testado = "" And Not Detener Then
                Mensaje_IT("No se ha ingresado valor para la Estado Actual en la fila " + CStr(x))
                Detener = True
            End If
           
            If Tsubzona = "" And Not Detener And Testado <> "NO EXISTE" Then
                Mensaje_IT("No se ha ingresado valor para la Subzona Actual en la fila " + CStr(x))
                Detener = True
            End If

            If Detener Then
                'Cerrar excel abierto
                libro.Close()
            End If
            fbp.continua_proceso()
            Threading.Thread.Sleep(10)
            x = x + 1
            If (IsNothing(hoja_main.Cells(x, 1).Value)) Then
                Existan = False
            End If
        End While
        'fin validar archivo
        'inicio ingresar planilla a BD
        i = 1
        While i <= total_reg And Not Detener
            x = i + 5
            Tproducto = hoja_main.Cells(x, 2).Value
            If String.IsNullOrEmpty(hoja_main.Cells(x, 8).Value) Then
                Testado = "0"
            Else
                Testado = hoja_main.Cells(x, 8).Value
            End If
            If String.IsNullOrEmpty(hoja_main.Cells(x, 10).Value) Then
                Tsubzona = "0"
            Else
                Tsubzona = hoja_main.Cells(x, 10).Value
            End If

            If String.IsNullOrEmpty(hoja_main.Cells(x, 12).Value) Then
                Tubicacion = "0"
            Else
                Tubicacion = Val(hoja_main.Cells(x, 12).Value)
            End If
            If String.IsNullOrEmpty(hoja_main.Cells(x, 14).Value) Then
                Tobs = ""
            Else
                Tobs = hoja_main.Cells(x, 14).Value
            End If

            colchon = base.ingresar_toma_inv(Tproducto, Pinventario.lastDB, Testado, Tsubzona, Tubicacion, Tobs)
            If colchon.Rows(0).Item("status") <> 1 Then
                Mensaje_IT(colchon.Rows(0).Item("mensaje") + x.ToString)
                Detener = True
            End If

            fbp.continua_proceso()
            i = i + 1
            Threading.Thread.Sleep(10)
        End While
        'fin ingreso planilla a BD
        descargar_barra(Me)
        'objExcel.Visible = True
        libro.Close()
        desbloquearW(Me)
        If Not Detener Then
            Mensaje_Inf("Carga ha sido completada exitosamente")
        End If
    End Sub
#End Region


    




    



    
End Class