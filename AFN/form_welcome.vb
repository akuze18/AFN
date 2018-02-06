Imports ActDir = System.DirectoryServices
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.IO
Imports Enc = System.Environment

Public Class form_welcome
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private base As New base_AFN


#Region "Variables de Clase"
    Dim usuarioActual As String
#End Region

#Region "Formulario"
    Private Sub form_welcome_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed
        Application.Exit()
    End Sub
    Private Sub form_welcome_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim colchon As DataTable

        Dim strUsuarioRed, strNameUsuario As String
        strUsuarioRed = ObtenerUsuarioActual()
        strNameUsuario = ""
        'Try
        '    Dim entry As New ActDir.DirectoryEntry("LDAP://nhfoodschile.cl/CN=ForeignSecurityPrincipals,DC=nhfoodschile,DC=cl")  'CN=Contenedor, OU= Unidad Organizativa
        '    Dim Dsearch As New ActDir.DirectorySearcher(entry)
        '    'Dsearch.Filter = "(samaccountname=" + strUsuarioRed + ")"
        '    For Each sResultSet As ActDir.SearchResult In Dsearch.FindAll()
        '        'Login Name
        '        strNameUsuario = GetProperty(sResultSet, "name")

        '        Dim cn, descrip, nombre As String
        '        cn = GetProperty(sResultSet, "cn")
        '        descrip = GetProperty(sResultSet, "description")
        '        nombre = GetProperty(sResultSet, "name")
        '        MessageBox.Show(strNameUsuario)
        '        'Dim sama As String = GetProperty(sResultSet, "samaccountname")
        '    Next
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
        Try
            Dim entry As New ActDir.DirectoryEntry("LDAP://nhfoodschile.cl/OU=NHFOODSCHILE,DC=nhfoodschile,DC=cl")  'CN=Contenedor, OU= Unidad Organizativa
            Dim Dsearch As New ActDir.DirectorySearcher(entry)
            Dsearch.Filter = "(samaccountname=" + strUsuarioRed + ")"
            For Each sResultSet As ActDir.SearchResult In Dsearch.FindAll()
                'Login Name
                strNameUsuario = GetProperty(sResultSet, "name")
                'Dim sama As String = GetProperty(sResultSet, "samaccountname")
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Panels1.Text = strNameUsuario
        Panels2.Text = Format(Now, "dd MMMM yyyy")
        Panels3.Text = base.servidor
        Panels4.Text = base.base_dato
        usuarioActual = strUsuarioRed

        colchon = base.PERIODO_FISCAL
        For Each row As DataRow In colchon.Rows
            'per.Text = "Periodo " + row("year1").ToString + "-" + CStr(row("year1") + 1)
            Sistema2.Text = "Carga de Saldos Iniciales " + CStr(row("year1") + 1)
            Sistema2.Tag = row("year1").ToString
        Next
        colchon = Nothing
        'conn.cerrar()
        'genero evento Resize para actualizar la posicion de los elementos del panel
        set_MinSize()
        'Checkeo directorios
        For Each directorio As String In base.dirAll
            If Not System.IO.Directory.Exists(directorio) Then
                System.IO.Directory.CreateDirectory(directorio)
            End If
        Next
        'copio en local archivos necesarios
        My.Resources.logo_nippon.Save(base.fileLogo)    'logo Empresa
        System.IO.File.WriteAllBytes(base.fileFontBarcode, My.Resources.FRE3OF9X)   'Fuente de Codigo Barra
        System.IO.File.WriteAllBytes(base.fileFontLabel, My.Resources.BrowalliaUPC)   'Fuente de Letras Etiqueta
        
    End Sub
#End Region

#Region "Funciones Privadas"
    Private Function GetProperty(ByVal searchResult As ActDir.SearchResult, ByVal PropertyName As String) As String
        Dim propertyKey, resultado As String
        Dim caca As String
        resultado = String.Empty
        For Each propertyKey In searchResult.Properties.PropertyNames

            Dim valueCollection As ActDir.ResultPropertyValueCollection = searchResult.Properties(propertyKey)
            Dim propertyValue As Object
            For Each propertyValue In valueCollection
                If propertyKey = PropertyName Then
                    resultado = propertyValue.ToString()
                Else
                    caca = propertyValue.ToString()
                End If
            Next propertyValue

        Next propertyKey
        Return resultado
    End Function
    Private Sub set_MinSize()
        Dim largo_menu, largo_form, largo_cntrls As Integer
        'Dim LUser, Lfecha, LServ, LBD As Integer
        'largo_residuo = 16      'determinado por prueba y error
        largo_form = StatusStrip1.FindForm.Width
        largo_menu = StatusStrip1.Size.Width
        'largo de controles
        largo_cntrls = 0
        For Each Spanel In StatusStrip1.Items
            If Spanel.Tag <> "dinam" Then
                largo_cntrls = largo_cntrls + Spanel.Width
            End If
        Next
        Me.MinimumSize = New System.Drawing.Size((largo_form - largo_menu) + largo_cntrls + 16, 224)
    End Sub

    ''' <summary>
    ''' Genera un archivo de respaldo, con la información de un error por lado del cliente
    ''' </summary>
    ''' <param name="resultado">Variable generada en el error</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function crear_log_error(ByVal resultado As Exception) As Boolean
        Dim archivo_log As String
        Try
            archivo_log = base.dirError + "ERROR " + Format(Now, "dd-MM-YYYY HH.mm.ss") + ".log"
            Dim ArchivoSalida As New StreamWriter(archivo_log)
            ArchivoSalida.WriteLine("Site: " + CStr(resultado.TargetSite.Name))
            ArchivoSalida.WriteLine("Number: " + CStr(resultado.ToString))
            ArchivoSalida.WriteLine("Descripcion: " + CStr(resultado.Message))
            ArchivoSalida.WriteLine("Fuente: " + CStr(resultado.Source))
            ArchivoSalida.WriteLine("Usuario: " + Me.usuarioActual)
            ArchivoSalida.WriteLine("Modulo: " + CStr(Me.Name))
            ArchivoSalida.Close()
            Return True
        Catch Ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Propiedades Publicas"
    Public ReadOnly Property GetUsuario As String
        Get
            Return UCase(usuarioActual)
        End Get
    End Property
#End Region

#Region "Opcion 'Consulta'"
    Private Sub Consulta1_Click(sender As System.Object, e As System.EventArgs) Handles Consulta1.Click
        busqueda_articulo.Show()
        busqueda_articulo.actualizar_origen("FI", Me)   'ficha ingreso
        Me.Hide()
    End Sub
    Private Sub Consulta2_Click(sender As System.Object, e As System.EventArgs) Handles Consulta2.Click
        busqueda_articulo.Show()
        busqueda_articulo.actualizar_origen("FB", Me)   'ficha baja
        Me.Hide()
    End Sub
    Private Sub Consulta5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Consulta5.Click
        form_saldo_obc.Show()
        Me.Hide()
    End Sub
    Private Sub Consulta6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Consulta6.Click
        form_ficha_cambio.Show()
        form_ficha_cambio.Hide()
        Me.Hide()
    End Sub
#End Region

#Region "Opcion 'Cambios'"
    Private Sub Cambio1_Click(sender As System.Object, e As System.EventArgs) Handles Cambio1.Click
        Dim valor As Integer
        valor = base.periodo_abierto_contar
        If valor > 0 Then
            form_ingreso.Show()
            Me.Hide()
        Else
            MessageBox.Show("Todos los periodos se encuentran cerrados, no puede ingresar información", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub
    Private Sub Cambio3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cambio3.Click
        form_venta.Show()
        Me.Hide()
    End Sub
    Private Sub Cambio4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cambio4.Click
        form_castigo.Show()
        Me.Hide()
    End Sub
    Private Sub Cambio5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cambio5.Click
        form_cambio.Show()
        Me.Hide()
    End Sub
    Private Sub Cambio6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cambio6.Click
        form_ing_obra.Show()
        Me.Hide()
    End Sub
    Private Sub Cambio7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cambio7.Click
        form_ter_obra.Show()
        Me.Hide()
    End Sub
    Private Sub Cambio8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cambio8.Click
        form_gasto_obra.Show()
        Me.Hide()
    End Sub
    Private Sub Cambio9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cambio9.Click
        form_precio_venta.Show()
        Me.Hide()
    End Sub
#End Region

#Region "Opcion 'Reportes'"
    Private Sub Reporte1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Reporte1.Click
        form_reporte.Show()
        Me.Hide()
    End Sub
    Private Sub Reporte2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Reporte2.Click
        form_rep_baja.Show()
        Me.Hide()
    End Sub
    Private Sub Reporte3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Reporte3.Click
        form_Cmov.Show()
        Me.Hide()
    End Sub
    Private Sub Reporte4_Click(sender As System.Object, e As System.EventArgs) Handles Reporte4.Click
        form_FixAsset.Show()
        Me.Hide()
    End Sub
    Private Sub Reporte5_Click(sender As System.Object, e As System.EventArgs) Handles Reporte5.Click
        form_contab.Show()
        Me.Hide()
    End Sub
#End Region

#Region "Opcion 'Inventario'"
    Private Sub Inventario1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Inventario1.Click
        form_etiquetas.Show()
        Me.Hide()
    End Sub
    Private Sub Inventario2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Inventario2.Click
        form_toma_invent.Show()
        Me.Hide()
    End Sub
    Private Sub Inventario3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Inventario3.Click
        form_load_invent.Show()
        Me.Hide()
    End Sub
    Private Sub Inventario4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Inventario4.Click
        form_resultado_invent.Show()
        Me.Hide()
    End Sub

    Private Sub Test_Click(sender As System.Object, e As System.EventArgs) Handles Test.Click
        Dim opcion, menu_opcion As String
        menu_opcion = "1) Imprimir Etiqueta en VB.NET" + Chr(13)
        menu_opcion = menu_opcion + "2) Enviar Correo en VB.NET" + Chr(13)
        menu_opcion = menu_opcion + "3) Directorio de la app" + Chr(13)
        opcion = InputBox(menu_opcion, "Opcion de TEST", "")

        'Form2.Show()
        '
        'frmSolicitudInfo.Show()
        Select Case opcion
            Case "1"
                Form3.Show()
            Case "2"
                sentMail.Show()
            Case "3"
                Dim directorio As DirectoryInfo = New DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory())
                MessageBox.Show(directorio.FullName)
                MessageBox.Show(directorio.Name)
                MessageBox.Show(directorio.Parent.Name)
                MessageBox.Show(directorio.Parent.FullName)
            Case Else
                MessageBox.Show("opcion invalida")
        End Select
    End Sub
#End Region

#Region "Opcion 'Sitema'"
    Private Sub Sistema1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Sistema1.Click
        form_CM.Show()
        Me.Hide()
    End Sub
    Private Sub Sistema2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Sistema2.Click
        Dim colchon As DataTable

        colchon = base.CambioAño(Sistema2.Tag, GetUsuario)

        For Each fila As DataRow In colchon.Rows
            Select Case fila("codigo")
                Case 1
                    MessageBox.Show(fila("explica"), "Módulo " + fila("modulo"), MessageBoxButtons.OK, MessageBoxIcon.Information)
                Case 2
                    MessageBox.Show(fila("explica"), "Módulo " + fila("modulo"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Case 0
                    MessageBox.Show("Error (" + fila("descrip") + "):" + Enc.NewLine + Enc.NewLine + fila("explica"), "Módulo " + fila("modulo"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Select
            Application.DoEvents()
        Next

    End Sub
    Private Sub Sistema3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Sistema3.Click
        form_cuenta_cont.Show()
        Me.Hide()
    End Sub
#End Region

    'consultas propias
    ''' <summary>
    ''' Genera un archivo Excel con la información básica que se ingresó el activo al sistema
    ''' </summary>
    ''' <param name="lote_art">Código del lote del artículo que se desea visualizar el reporte</param>
    ''' <remarks></remarks>
    Public Sub AF_ficha_ingreso(ByVal lote_art As Integer)
        Dim config_hoja As Boolean
        Dim Torigen, DetOrigen As String
        Dim colN1, colN2, colN3, filT1, colT1, colT2, colT3 As Integer
        Dim filT0, colG0, colG1 As Integer
        config_hoja = True

        Dim rpt1, rpt2 As DataTable
        'Dim rpt1FC, rpt1TC, rpt1IC, rpt1IY As DataTable
        Dim i, j, bono As Integer
        'carga de datos
        rpt1 = base.REPORTE_INICIO(lote_art)

        'obtenemos segundo reporte, información de especificaciones
        rpt2 = base.REPORTE_INICIO2(lote_art)
        'datos cargados
        'imprimir datos en excel
        Dim oExcel As New Excel.Application
        Dim oBook As Excel.Workbook = oExcel.Workbooks.Add
        Dim oSheet As Excel.Worksheet
        For i = oBook.Sheets.Count To 2 Step -1
            oBook.Sheets(i).Delete()
        Next
        oSheet = oBook.Sheets(i)
        'formamos la grilla blanca y de ancho fijo para trabajar sobre ella
        With oSheet.Cells
            .ColumnWidth = 3
            With .Borders(Excel.XlBordersIndex.xlEdgeLeft)
                .LineStyle = 1
                .ColorIndex = 2
                .TintAndShade = 0
                .Weight = 2
            End With
            With .Borders(Excel.XlBordersIndex.xlEdgeTop)
                .LineStyle = 1
                .ColorIndex = 2
                .TintAndShade = 0
                .Weight = 2
            End With
            With .Borders(Excel.XlBordersIndex.xlEdgeBottom)
                .LineStyle = 1
                .ColorIndex = 2
                .TintAndShade = 0
                .Weight = 2
            End With
            With .Borders(Excel.XlBordersIndex.xlEdgeRight)
                .LineStyle = 1
                .ColorIndex = 2
                .TintAndShade = 0
                .Weight = 2
            End With
            With .Borders(Excel.XlBordersIndex.xlInsideVertical)
                .LineStyle = 1
                .ColorIndex = 2
                .TintAndShade = 0
                .Weight = 2
            End With
            With .Borders(Excel.XlBordersIndex.xlInsideHorizontal)
                .LineStyle = 1
                .ColorIndex = 2
                .TintAndShade = 0
                .Weight = 2
            End With
        End With
        'primera parte del reporte, listado de valores genéricos
        colN1 = 1
        filT1 = 1
        colN2 = colN1 + 8
        colN3 = colN2 + 16
        colT1 = colN1 + 4
        colT2 = colN2 + 3
        colT3 = colN3 + 3
        oSheet.Cells(filT1, colN1).Value = "Código Grupo :"
        With oSheet.Range(lcol(colN1) + CStr(filT1) + ":" + lcol(colT1 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        With oSheet.Range(lcol(colN1) + CStr(filT1) + ":" + lcol(colT1 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        oSheet.Cells(filT1, colT1).Value = rpt1.Rows(0).Item("cod_articulo")
        With oSheet.Range(lcol(colT1) + CStr(filT1) + ":" + lcol(colT1 + 2) + CStr(filT1))
            .MergeCells = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        oSheet.Cells(filT1, colN2).Value = "Descripción :"
        With oSheet.Range(lcol(colN2) + CStr(filT1) + ":" + lcol(colT2) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        oSheet.Cells(filT1, colT2 + 1).Value = rpt1.Rows(0).Item("dscrp")
        filT1 = filT1 + 2
        oSheet.Cells(filT1, colN1).Value = "Cantidad :"
        With oSheet.Range(lcol(colN1) + CStr(filT1) + ":" + lcol(colT1 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        oSheet.Cells(filT1, colT1) = rpt1.Compute("SUM(cantidad)", "fuente='FIN'")
        With oSheet.Range(lcol(colT1) + CStr(filT1) + ":" + lcol(colT1 + 2) + CStr(filT1))
            .MergeCells = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        oSheet.Cells(filT1, colN2) = "Zona :"
        With oSheet.Range(lcol(colN2) + CStr(filT1) + ":" + lcol(colT2 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        oSheet.Cells(filT1, colT2) = rpt1.Rows(0).Item("dscr_zona")
        oSheet.Cells(filT1, colN3) = "Clase :"
        With oSheet.Range(lcol(colN3) + CStr(filT1) + ":" + lcol(colT3 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        oSheet.Cells(filT1, colT3) = rpt1.Rows(0).Item("dscr_clase")
        filT1 = filT1 + 2
        oSheet.Cells(filT1, colN1) = "Orígen :"
        With oSheet.Range(lcol(colN1) + CStr(filT1) + ":" + lcol(colT1 - 2) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        Torigen = rpt1.Rows(0).Item("origen")
        If Torigen = "REG" Then
            DetOrigen = "REGULAR"
        Else
            DetOrigen = "OBRA CONST."
        End If
        oSheet.Cells(filT1, colT1 - 1) = DetOrigen
        With oSheet.Range(lcol(colT1 - 1) + CStr(filT1) + ":" + lcol(colT1 + 2) + CStr(filT1))
            .MergeCells = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        oSheet.Cells(filT1, colN2) = "Sub Zona :"
        With oSheet.Range(lcol(colN2) + CStr(filT1) + ":" + lcol(colT2 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        oSheet.Cells(filT1, colT2) = rpt1.Rows(0).Item("dscr_subz")
        oSheet.Cells(filT1, colN3) = "Sub Clase :"
        With oSheet.Range(lcol(colN3) + CStr(filT1) + ":" + lcol(colT3 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        oSheet.Cells(filT1, colT3) = rpt1.Rows(0).Item("dscr_subc")
        filT1 = filT1 + 2
        oSheet.Cells(filT1, colN1) = "Fecha Compra :"
        With oSheet.Range(lcol(colN1) + CStr(filT1) + ":" + lcol(colT1 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        With oSheet.Range(lcol(colT1) + CStr(filT1) + ":" + lcol(colT1 + 2) + CStr(filT1))
            .MergeCells = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
            .NumberFormat = "@"
        End With
        oSheet.Cells(filT1, colT1) = rpt1.Rows(0).Item("fecha_compra")
        oSheet.Cells(filT1, colN2) = "Categoria :"
        With oSheet.Range(lcol(colN2) + CStr(filT1) + ":" + lcol(colT2 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        oSheet.Cells(filT1, colT2) = rpt1.Rows(0).Item("dscr_categ")
        oSheet.Cells(filT1, colN3) = "Nº Doc. :"
        With oSheet.Range(lcol(colN3) + CStr(filT1) + ":" + lcol(colT3 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        oSheet.Cells(filT1, colT3) = rpt1.Rows(0).Item("num_doc")
        'With oSheet.Range(lcol(colT3) + CStr(filT1) + ":" + lcol(colT3 + 3) + CStr(filT1))
        '    .MergeCells = True
        '    .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        'End With
        filT1 = filT1 + 2

        'saber en que fila está cada uno de los valores
        Dim Ffin, Ftrib, Fifrs, Fifrsy As Integer
        Ffin = -1
        Ftrib = -1
        Fifrs = -1
        Fifrsy = -1
        For i = 0 To rpt1.Rows.Count - 1
            Dim RptRow As DataRow = rpt1.Rows(i)
            Select Case RptRow.Item("fuente")
                Case "FIN"
                    Ffin = i
                Case "TRIB"
                    Ftrib = i
                Case "IFRS"
                    Fifrs = i
                Case "IFRS_Y"
                    Fifrsy = i
            End Select
        Next
        oSheet.Cells(filT1, colN1) = "Derecho Credito :"
        With oSheet.Range(lcol(colN1) + CStr(filT1) + ":" + lcol(colT1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        oSheet.Cells(filT1, colT1 + 1) = rpt1.Rows(0).Item("derecho_credito")
        oSheet.Cells(filT1, colN2) = "Proveedor :"
        With oSheet.Range(lcol(colN2) + CStr(filT1) + ":" + lcol(colT2 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        oSheet.Cells(filT1, colT2) = "'" + rpt1.Rows(0).Item("proveedor").ToString
        bono = 0
        If Trim(rpt1.Rows(0).Item("proveedor")) <> Trim(rpt1.Rows(0).Item("dscr_prov")) Then
            oSheet.Cells(filT1 + 1, colT2) = Trim(rpt1.Rows(0).Item("dscr_prov"))
            bono = 1
        End If
        If Fifrs <> -1 Then
            oSheet.Cells(filT1, colN3) = "Metodo Revaluación :"
            With oSheet.Range(lcol(colN3) + CStr(filT1) + ":" + lcol(colT3 + 2) + CStr(filT1))
                .MergeCells = True
                .Font.Bold = True
                .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
            End With
            Select Case rpt1.Rows(0).Item("metod_val")
                Case 1
                    oSheet.Cells(filT1, colT3 + 3) = "COSTO"
                Case 2
                    oSheet.Cells(filT1, colT3 + 3) = "REVALORIZACION"
            End Select
        End If
        'segunda parte del reporte, cuadro resumen de valores
        'columna 1
        filT0 = filT1 + 3 + bono
        filT1 = filT0
        'objExcel.Visible = True
        colG0 = 2
        colG1 = colG0 + 4
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = "Precio Unitario"
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = "Vida Útil"
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = "Valor Residual"
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = "Dep. Acum."
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        If Fifrs <> -1 Then
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "Preparación"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "Transporte"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "Montaje"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "Desmantelamiento"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "Honorario"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "Revalorización"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        End If
        'aplico formato para la columna
        With oSheet.Range(lcol(colG0) + CStr(filT0 + 1) + ":" + lcol(colG1) + CStr(filT1))
            With .Borders(7)
                .ColorIndex = 1
            End With
            With .Borders(8)
                .ColorIndex = 1
            End With
            With .Borders(9)
                .ColorIndex = 1
            End With
            With .Borders(10)
                .ColorIndex = 1
            End With
            With .Borders(11)
                .ColorIndex = 1
            End With
            With .Borders(12)
                .ColorIndex = 1
            End With
            .Font.Bold = True
        End With

        'columna 2
        'filT0 = filT0   'fila inicio de la sección no cambia
        filT1 = filT0   'vuelvo al inicio el contador de filas

        colG0 = colG1 + 1   '1 columna a la derecha de donde termino la anterior
        colG1 = colG0 + 2   'seteo nuevo rango, en este caso seran 4

        oSheet.Cells(filT1, colG0) = "FINANCIERO"
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = rpt1.Rows(Ffin).Item("precio_base")
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = rpt1.Rows(Ffin).Item("vida_util_base")
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = rpt1.Rows(Ffin).Item("valor_residual")
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = rpt1.Rows(Ffin).Item("depreciacion_acum")
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        If Fifrs <> -1 Then
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        End If
        'aplico formato para la columna
        With oSheet.Range(lcol(colG0) + CStr(filT0) + ":" + lcol(colG1) + CStr(filT1))
            With .Borders(7)
                .ColorIndex = 1
            End With
            With .Borders(8)
                .ColorIndex = 1
            End With
            With .Borders(9)
                .ColorIndex = 1
            End With
            With .Borders(10)
                .ColorIndex = 1
            End With
            With .Borders(11)
                .ColorIndex = 1
            End With
            With .Borders(12)
                .ColorIndex = 1
            End With
            .HorizontalAlignment = -4152    'xlRight
            .VerticalAlignment = -4160      'xlTop
            .NumberFormat = "#,##0"
        End With

        'columna 3
        'filT0 = filT0   'fila inicio de la sección no cambia
        filT1 = filT0   'vuelvo al inicio el contador de filas

        colG0 = colG1 + 1   '1 columna a la derecha de donde termino la anterior
        colG1 = colG0 + 2   'seteo nuevo rango, en este caso seran 4

        oSheet.Cells(filT1, colG0) = "TRIBUTARIO"
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = rpt1.Rows(Ftrib).Item("precio_base")
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = rpt1.Rows(Ftrib).Item("vida_util_base")
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = rpt1.Rows(Ftrib).Item("valor_residual")
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = rpt1.Rows(Ftrib).Item("depreciacion_acum")
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        If Fifrs <> -1 Then
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        End If
        'aplico formato para la columna
        With oSheet.Range(lcol(colG0) + CStr(filT0) + ":" + lcol(colG1) + CStr(filT1))
            With .Borders(7)
                .ColorIndex = 1
            End With
            With .Borders(8)
                .ColorIndex = 1
            End With
            With .Borders(9)
                .ColorIndex = 1
            End With
            With .Borders(10)
                .ColorIndex = 1
            End With
            With .Borders(11)
                .ColorIndex = 1
            End With
            With .Borders(12)
                .ColorIndex = 1
            End With
            .HorizontalAlignment = -4152    'xlRight
            .VerticalAlignment = -4160      'xlTop
            .NumberFormat = "#,##0"
        End With


        If Fifrs <> -1 Then
            'columna 4
            'filT0 = filT0   'fila inicio de la sección no cambia
            filT1 = filT0   'vuelvo al inicio el contador de filas

            colG0 = colG1 + 1   '1 columna a la derecha de donde termino la anterior
            colG1 = colG0 + 2   'seteo nuevo rango, en este caso seran 4


            oSheet.Cells(filT1, colG0) = "IFRS CLP"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = rpt1.Rows(Fifrs).Item("precio_base")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = rpt1.Rows(Fifrs).Item("vida_util_base")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = rpt1.Rows(Fifrs).Item("valor_residual")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = rpt1.Rows(Fifrs).Item("depreciacion_acum")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = rpt1.Rows(Fifrs).Item("preparacion")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = rpt1.Rows(Fifrs).Item("transporte")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = rpt1.Rows(Fifrs).Item("montaje")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = rpt1.Rows(Fifrs).Item("desmantel")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = rpt1.Rows(Fifrs).Item("honorario")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = rpt1.Rows(Fifrs).Item("revalorizacion")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            'aplico formato para la columna
            With oSheet.Range(lcol(colG0) + CStr(filT0) + ":" + lcol(colG1) + CStr(filT1))
                With .Borders(7)
                    .ColorIndex = 1
                End With
                With .Borders(8)
                    .ColorIndex = 1
                End With
                With .Borders(9)
                    .ColorIndex = 1
                End With
                With .Borders(10)
                    .ColorIndex = 1
                End With
                With .Borders(11)
                    .ColorIndex = 1
                End With
                With .Borders(12)
                    .ColorIndex = 1
                End With
                .HorizontalAlignment = -4152    'xlRight
                .VerticalAlignment = -4160      'xlTop
                .NumberFormat = "#,##0"
            End With
        End If
        'aca empieza ifrs yen
        If Fifrsy <> -1 Then
            'columna 5
            'filT0 = filT0   'fila inicio de la sección no cambia
            filT1 = filT0   'vuelvo al inicio el contador de filas

            colG0 = colG1 + 1   '1 columna a la derecha de donde termino la anterior
            colG1 = colG0 + 2   'seteo nuevo rango, en este caso seran 4

            oSheet.Cells(filT1, colG0) = "IFRS YEN"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Math.Round(rpt1.Rows(Fifrsy).Item("precio_base"), 2)
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = rpt1.Rows(Fifrsy).Item("vida_util_base")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Math.Round(rpt1.Rows(Fifrsy).Item("valor_residual"), 2)
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Math.Round(rpt1.Rows(Fifrsy).Item("depreciacion_acum"), 2)
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Math.Round(rpt1.Rows(Fifrsy).Item("preparacion"), 2)
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Math.Round(rpt1.Rows(Fifrsy).Item("transporte"), 2)
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Math.Round(rpt1.Rows(Fifrsy).Item("montaje"), 2)
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Math.Round(rpt1.Rows(Fifrsy).Item("desmantel"), 2)
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Math.Round(rpt1.Rows(Fifrsy).Item("honorario"), 2)
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Math.Round(rpt1.Rows(Fifrsy).Item("revalorizacion"), 2)
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            'aplico formato para la columna
            With oSheet.Range(lcol(colG0) + CStr(filT0) + ":" + lcol(colG1) + CStr(filT1))
                With .Borders(7)
                    .ColorIndex = 1
                End With
                With .Borders(8)
                    .ColorIndex = 1
                End With
                With .Borders(9)
                    .ColorIndex = 1
                End With
                With .Borders(10)
                    .ColorIndex = 1
                End With
                With .Borders(11)
                    .ColorIndex = 1
                End With
                With .Borders(12)
                    .ColorIndex = 1
                End With
                .HorizontalAlignment = -4152    'xlRight
                .VerticalAlignment = -4160      'xlTop
                .NumberFormat = "#,##0.0"
            End With
        End If

        'aca termina la cosa

        With oSheet.Range("A" + CStr(filT0) + ":" + lcol(colG1) + CStr(filT0))
            .HorizontalAlignment = -4108    'xlCenter
            .Font.Bold = True
        End With

        'tercera seccion, ingreso de descripciones, si las hubiera.
        Dim colAtrib, filAtrib, conGen, conEsp As Integer
        Dim Atrib, ver_largo, extra As Integer
        colAtrib = 20 'columna fija'columna donde se ocupo ultima vez
        filAtrib = filT0 - 1 'fila superior desocupada
        'revisamos si hay descripcion generica
        conGen = 0
        conEsp = 0
        For i = 0 To rpt2.Rows.Count - 1
            Atrib = rpt2.Rows(i).Item("cod_atrib")
            If Not (Atrib = 16 Or Atrib = 17 Or Atrib = 18) Then
                If rpt2.Rows(i).Item("codigo") = "" Then
                    conGen = conGen + 1
                Else
                    conEsp = conEsp + 1
                End If
            End If
        Next
        If conGen > 0 Then
            With oSheet.Cells(filAtrib, colAtrib)
                .Value = "DESCRIPCIÓN GENERICA"
                .Font.Bold = True
                .Font.Underline = True
            End With
            filAtrib = filAtrib + 2
            For i = 0 To rpt2.Rows.Count - 1
                Atrib = rpt2.Rows(i).Item("cod_atrib")
                If Not (Atrib = 16 Or Atrib = 17 Or Atrib = 18) Then
                    If rpt2.Rows(i).Item("codigo") = "" Then
                        oSheet.Cells(filAtrib, colAtrib) = rpt2.Rows(i).Item("atributo")
                        oSheet.Cells(filAtrib, colAtrib + 4) = rpt2.Rows(i).Item("dscr_detalle")
                        ver_largo = Len(rpt2.Rows(i).Item("dscr_detalle"))
                        extra = Int(ver_largo / 48)
                        With oSheet.Range(lcol(colAtrib + 4) + CStr(filAtrib) + ":" + lcol(colAtrib + 17) + CStr(filAtrib + extra))
                            .MergeCells = True
                            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
                            .VerticalAlignment = Excel.XlVAlign.xlVAlignTop
                            .WrapText = True
                        End With
                        filAtrib = filAtrib + extra + 1
                    End If
                End If
            Next
            filAtrib = filAtrib + 1
        End If
        If conEsp > 0 Then
            With oSheet.Cells(filAtrib, colAtrib)
                .Value = "DESCRIPCIÓN ESPECÍFICA"
                .Font.Bold = True
                .Font.Underline = True
            End With
            filAtrib = filAtrib + 2
            j = 1
            For i = 0 To rpt2.Rows.Count - 1
                Atrib = rpt2.Rows(i).Item("cod_atrib")
                If Not (Atrib = 16 Or Atrib = 17 Or Atrib = 18) Then
                    If rpt2.Rows(i).Item("codigo") <> "" Then
                        If j = 1 Then
                            oSheet.Cells(filAtrib, colAtrib).Value = rpt2.Rows(i).Item("atributo")
                            oSheet.Cells(filAtrib, colAtrib).Font.Bold = True
                            filAtrib = filAtrib + 1
                            j = j + 1
                        Else
                            If rpt2.Rows(i).Item("cod_atrib") <> rpt2.Rows(i).Item("cod_atrib") Then
                                oSheet.Cells(filAtrib, colAtrib).Value = rpt2.Rows(i).Item("atributo")
                                oSheet.Cells(filAtrib, colAtrib).Font.Bold = True
                                filAtrib = filAtrib + 1
                                j = j + 1
                            End If
                        End If
                        oSheet.Cells(filAtrib, colAtrib).NumberFormat = "@"
                        oSheet.Cells(filAtrib, colAtrib) = rpt2.Rows(i).Item("codigo")
                        oSheet.Cells(filAtrib, colAtrib + 4) = rpt2.Rows(i).Item("dscr_detalle")
                        ver_largo = Len(rpt2.Rows(i).Item("dscr_detalle"))
                        extra = Int(ver_largo / 48)
                        With oSheet.Range(lcol(colAtrib + 4) + CStr(filAtrib) + ":" + lcol(colAtrib + 17) + CStr(filAtrib + extra))
                            .MergeCells = True
                            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
                            .VerticalAlignment = -4160      'xlTop
                            .WrapText = True
                        End With
                        filAtrib = filAtrib + extra + 1
                    End If
                End If
            Next
            filAtrib = filAtrib + 1
        End If
        'cuarta seccion lineas para firma
        Dim largoFirma, filFirma, ColFirma As Integer
        largoFirma = 4      'contendra 5 celdas
        filFirma = filT1 + 7
        ColFirma = 5
        With oSheet.Range(lcol(ColFirma) + CStr(filFirma) + ":" + lcol(ColFirma + largoFirma) + CStr(filFirma))
            .MergeCells = True
            .HorizontalAlignment = -4108    'xlCenter
            .VerticalAlignment = -4108      'xlCenter
            .Borders(8).ColorIndex = 1
            .Value = "Contador General"
        End With
        ColFirma = ColFirma + largoFirma + 3
        With oSheet.Range(lcol(ColFirma) + CStr(filFirma) + ":" + lcol(ColFirma + largoFirma) + CStr(filFirma + 1))
            .MergeCells = True
            .HorizontalAlignment = -4108    'xlCenter
            .VerticalAlignment = -4108      'xlCenter
            .Borders(8).ColorIndex = 1
            .Value = "Gerencia" & vbCrLf & "(Opcional)"
        End With
        'quinta sección añadir fotos si las hubiera
        Dim indx_pag As Integer
        Dim imagen, nom_pag, cabezera, pies As String
        For i = 0 To rpt2.Rows.Count - 1
            Atrib = rpt2.Rows(i).Item("cod_atrib")
            If (Atrib = 16 Or Atrib = 17 Or Atrib = 18) Then
                oBook.Sheets.Add(After:=oBook.Sheets(oBook.Sheets.Count)).Name = "00"
                indx_pag = oBook.Sheets.Count
                imagen = base.dirFotos + rpt2.Rows(i).Item("dscr_detalle")
                nom_pag = rpt2.Rows(i).Item("atributo")
                If rpt2.Rows(i).Item("codigo") <> "" Then
                    nom_pag = nom_pag + "-" + rpt2.Rows(i).Item("codigo")
                End If
                cabezera = nom_pag
                pies = "&P / &N"
                Dim fSheet As Excel.Worksheet = oBook.Sheets(indx_pag)
                With fSheet
                    .Shapes.AddPicture(imagen, False, True, 0, 0, 709, 482)
                    .Name = nom_pag
                    With .PageSetup
                        .Orientation = 2
                        Try
                            If config_hoja Then
                                .PaperSize = Excel.XlPaperSize.xlPaperFolio
                            End If
                        Catch ex As Exception
                            Try
                                If config_hoja Then
                                    .PaperSize = Excel.XlPaperSize.xlPaperLetter
                                End If
                            Catch ex2 As Exception
                                config_hoja = False
                                MessageBox.Show("No se establecio el tamaño del papel la hoja Excel." + Chr(13) + "Debe configurarlo manualmente", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                If Not crear_log_error(ex2) Then
                                    MessageBox.Show("No se guardo log de error", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                            End Try
                        End Try
                        .CenterHeader = cabezera
                        .CenterFooter = pies
                        .CenterHorizontally = True
                        .CenterVertically = True
                    End With
                End With
            End If
        Next

        'configurar pagina
        Dim periodo, nombre_total As String
        nom_pag = "ficha_ingreso"
        'indx_pag = 1
        cabezera = "&""-,Negrita""&20Ficha de Ingreso" & Chr(13) & "Activo Fijo"
        periodo = "&25 " + Format(rpt1.Rows(1).Item("fecha_ing"), "yyyy-MM")
        oSheet.Name = nom_pag
        nombre_total = base.fileLogo
        oSheet.PageSetup.LeftHeaderPicture.Filename = nombre_total
        With oSheet.PageSetup
            .Orientation = 2
            Try
                If config_hoja Then
                    .PaperSize = Excel.XlPaperSize.xlPaperFolio
                End If
            Catch ex As Exception
                Try
                    If config_hoja Then
                        .PaperSize = Excel.XlPaperSize.xlPaperLetter
                    End If
                Catch ex2 As Exception
                    config_hoja = False
                    MessageBox.Show("No se establecio el tamaño del papel la hoja Excel." + Chr(13) + "Debe configurarlo manualmente", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If Not crear_log_error(ex2) Then
                        MessageBox.Show("No se guardo log de error", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Try
            End Try
            .LeftHeader = "&G"
            .CenterHeader = cabezera
            .RightHeader = periodo
            .LeftFooter = "&P / &N"
            '        .CenterFooter = pies
            .RightFooter = "Fecha de Impresion : &D" & Chr(13) & "Hora de Impresion: &T"
            .TopMargin = oExcel.InchesToPoints(1.2)
            .RightMargin = oExcel.InchesToPoints(1.01)
            .LeftMargin = oExcel.InchesToPoints(1.01)
            .CenterHorizontally = True
        End With
        Application.DoEvents()
        oSheet.Select()
        oExcel.Visible = True
    End Sub

    ''' <summary>
    ''' Genera un archivo Excel con la información del activo al momento en que se dio de baja
    ''' </summary>
    ''' <param name="InCodigo">Código de lote del cual se desea obtener el reporte</param>
    ''' <param name="InParte">Parte que indica el sublote de división</param>
    ''' <remarks></remarks>
    Public Sub AF_ficha_baja(ByVal InCodigo As Integer, ByVal InParte As Integer)
        Dim config_hoja As Boolean

        Dim Torigen, DetOrigen As String
        Dim rpt1, rpt2 As DataTable
        Dim i, colN1, colN2, colN3, colT1, colT2, colT3, filT1, bono As Integer
        Dim filT0, colG0, colG1, Atrib, ver_largo, extra, j As Integer
        'Dim tipo_X As Double
        config_hoja = True
        'carga de datos
        rpt1 = base.REPORTE_BAJA(InCodigo, InParte)
        'obtenemos segundo reporte, información de especificaciones
        rpt2 = base.REPORTE_BAJA2(InCodigo, InParte)

        'datos cargados
        'imprimir datos en excel
        Dim oExcel As New Excel.Application
        Dim oBook As Excel.Workbook = oExcel.Workbooks.Add
        Dim oSheet As Excel.Worksheet
        For i = oBook.Sheets.Count To 2 Step -1
            oBook.Sheets(i).Delete()
        Next
        oSheet = oBook.Sheets(i)
        'formamos la grilla blanca y de ancho fijo para trabajar sobre ella
        With oSheet.Cells
            .ColumnWidth = 3
            With .Borders(7)
                .LineStyle = 1
                .ColorIndex = 2
                .TintAndShade = 0
                .Weight = 2
            End With
            With .Borders(8)
                .LineStyle = 1
                .ColorIndex = 2
                .TintAndShade = 0
                .Weight = 2
            End With
            With .Borders(9)
                .LineStyle = 1
                .ColorIndex = 2
                .TintAndShade = 0
                .Weight = 2
            End With
            With .Borders(10)
                .LineStyle = 1
                .ColorIndex = 2
                .TintAndShade = 0
                .Weight = 2
            End With
            With .Borders(11)
                .LineStyle = 1
                .ColorIndex = 2
                .TintAndShade = 0
                .Weight = 2
            End With
            With .Borders(12)
                .LineStyle = 1
                .ColorIndex = 2
                .TintAndShade = 0
                .Weight = 2
            End With
        End With

        'primera parte del reporte, listado de valores genéricos
        colN1 = 1
        filT1 = 1

        colN2 = colN1 + 8
        colN3 = colN2 + 16

        colT1 = colN1 + 4
        colT2 = colN2 + 3
        colT3 = colN3 + 3

        oSheet.Cells(filT1, colN1) = "Código Grupo :"
        With oSheet.Range(lcol(colN1) + CStr(filT1) + ":" + lcol(colT1 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = -4131    'xlLeft
        End With
        oSheet.Cells(filT1, colT1) = rpt1.Rows(0).Item("cod_articulo")
        With oSheet.Range(lcol(colT1) + CStr(filT1) + ":" + lcol(colT1 + 2) + CStr(filT1))
            .MergeCells = True
            .HorizontalAlignment = -4131    'xlLeft
        End With
        oSheet.Cells(filT1, colN2) = "Descripción :"
        With oSheet.Range(lcol(colN2) + CStr(filT1) + ":" + lcol(colT2) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = -4131    'xlLeft
        End With
        oSheet.Cells(filT1, colT2 + 1) = rpt1.Rows(0).Item("dscrp")


        filT1 = filT1 + 2

        oSheet.Cells(filT1, colN1) = "Cantidad :"
        With oSheet.Range(lcol(colN1) + CStr(filT1) + ":" + lcol(colT1 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = -4131    'xlLeft
        End With
        oSheet.Cells(filT1, colT1) = rpt1.Rows(0).Item("cantidad")
        With oSheet.Range(lcol(colT1) + CStr(filT1) + ":" + lcol(colT1 + 2) + CStr(filT1))
            .MergeCells = True
            .HorizontalAlignment = -4131    'xlLeft
        End With
        oSheet.Cells(filT1, colN2) = "Zona :"
        With oSheet.Range(lcol(colN2) + CStr(filT1) + ":" + lcol(colT2 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = -4131    'xlLeft
        End With
        oSheet.Cells(filT1, colT2) = rpt1.Rows(0).Item("dscr_zona")
        oSheet.Cells(filT1, colN3) = "Clase :"
        With oSheet.Range(lcol(colN3) + CStr(filT1) + ":" + lcol(colT3 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = -4131    'xlLeft
        End With
        oSheet.Cells(filT1, colT3) = rpt1.Rows(0).Item("dscr_clase")

        filT1 = filT1 + 2

        oSheet.Cells(filT1, colN1) = "Orígen :"
        With oSheet.Range(lcol(colN1) + CStr(filT1) + ":" + lcol(colT1 - 2) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = -4131    'xlLeft
        End With
        Torigen = rpt1.Rows(0).Item("origen")
        If Torigen = "REG" Then
            DetOrigen = "REGULAR"
        Else
            DetOrigen = "OBRA CONST."
        End If
        oSheet.Cells(filT1, colT1 - 1) = DetOrigen
        With oSheet.Range(lcol(colT1 - 1) + CStr(filT1) + ":" + lcol(colT1 + 2) + CStr(filT1))
            .MergeCells = True
            .HorizontalAlignment = -4131    'xlLeft
        End With
        oSheet.Cells(filT1, colN2) = "Sub Zona :"
        With oSheet.Range(lcol(colN2) + CStr(filT1) + ":" + lcol(colT2 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = -4131    'xlLeft
        End With
        oSheet.Cells(filT1, colT2) = rpt1.Rows(0).Item("dscr_subz")
        oSheet.Cells(filT1, colN3) = "Sub Clase :"
        With oSheet.Range(lcol(colN3) + CStr(filT1) + ":" + lcol(colT3 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = -4131    'xlLeft
        End With
        oSheet.Cells(filT1, colT3) = rpt1.Rows(0).Item("dscr_subc")

        filT1 = filT1 + 2

        oSheet.Cells(filT1, colN1) = "Fecha Compra :"
        With oSheet.Range(lcol(colN1) + CStr(filT1) + ":" + lcol(colT1 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = -4131    'xlLeft
        End With
        '    oSheet.Cells(filT1, colT1) = CStr(txt_dia) + "-" + CStr(txt_mes) + "-" + CStr(txt_año)
        With oSheet.Range(lcol(colT1) + CStr(filT1) + ":" + lcol(colT1 + 2) + CStr(filT1))
            .MergeCells = True
            .HorizontalAlignment = -4131    'xlLeft
            .NumberFormat = "@"
        End With
        oSheet.Cells(filT1, colT1) = Format(rpt1.Rows(0).Item("fecha_compra"), "dd/MM/yyyy")

        oSheet.Cells(filT1, colN2) = "Categoria :"
        With oSheet.Range(lcol(colN2) + CStr(filT1) + ":" + lcol(colT2 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = -4131    'xlLeft
        End With
        oSheet.Cells(filT1, colT2) = rpt1.Rows(0).Item("dscr_categ")
        oSheet.Cells(filT1, colN3) = "Nº Doc. :"
        With oSheet.Range(lcol(colN3) + CStr(filT1) + ":" + lcol(colT3 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = -4131    'xlLeft
        End With
        oSheet.Cells(filT1, colT3) = rpt1.Rows(0).Item("num_doc")
        '    With oSheet.Range(lcol(colT3) + CStr(filT1) + ":" + lcol(colT3 + 3) + CStr(filT1))
        '        .MergeCells = True
        '        .HorizontalAlignment = -4131    'xlLeft
        '    End With

        filT1 = filT1 + 2

        'identificar las filas de cada modulo
        Dim Rfin, Rtrib, Rifrs, RifrsY As DataRow
        Rfin = Nothing
        Rtrib = Nothing
        Rifrs = Nothing
        RifrsY = Nothing
        For Each tfila As DataRow In rpt1.Rows
            Select Case tfila("fuente")
                Case "FIN"
                    Rfin = tfila
                Case "TRIB"
                    Rtrib = tfila
                Case "IFRS"
                    Rifrs = tfila
                Case "IFRS_YEN"
                    RifrsY = tfila
            End Select
        Next

        oSheet.Cells(filT1, colN1) = "Derecho Credito :"
        With oSheet.Range(lcol(colN1) + CStr(filT1) + ":" + lcol(colT1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = -4131    'xlLeft
        End With
        oSheet.Cells(filT1, colT1 + 1) = rpt1.Rows(0).Item("derecho_credito")

        oSheet.Cells(filT1, colN2) = "Proveedor :"
        With oSheet.Range(lcol(colN2) + CStr(filT1) + ":" + lcol(colT2 - 1) + CStr(filT1))
            .MergeCells = True
            .Font.Bold = True
            .HorizontalAlignment = -4131    'xlLeft
        End With
        oSheet.Cells(filT1, colT2) = rpt1.Rows(0).Item("proveedor")
        bono = 0
        If Trim(rpt1.Rows(0).Item("proveedor")) <> Trim(rpt1.Rows(0).Item("dscr_prov")) Then
            oSheet.Cells(filT1 + 1, colT2) = Trim(rpt1.Rows(0).Item("dscr_prov"))
            bono = 1
        End If

        If Not IsNothing(Rifrs) Then
            oSheet.Cells(filT1, colN3) = "Metodo Revaluación :"
            With oSheet.Range(lcol(colN3) + CStr(filT1) + ":" + lcol(colT3 + 2) + CStr(filT1))
                .MergeCells = True
                .Font.Bold = True
                .HorizontalAlignment = -4131    'xlLeft
            End With
            Select Case Rifrs("metod_val")
                Case 1
                    oSheet.Cells(filT1, colT3 + 3) = "COSTO"
                Case 2
                    oSheet.Cells(filT1, colT3 + 3) = "REVALORIZACION"
            End Select
        End If

        'segunda parte del reporte, cuadro resumen de valores
        'columna 1
        filT0 = filT1 + 3 + bono
        filT1 = filT0

        colG0 = 2
        colG1 = colG0 + 4

        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = "Precio Unitario"
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = "Vida Útil"
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = "Valor Residual"
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = "Dep. Acum."
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        If Not IsNothing(Rifrs) Then
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "Preparación"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "Transporte"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "Montaje"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "Desmantelamiento"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "Honorario"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "Revalorización"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        End If
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = "Valor Libro"
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        'aplico formato para la columna
        With oSheet.Range(lcol(colG0) + CStr(filT0 + 1) + ":" + lcol(colG1) + CStr(filT1))
            With .Borders(7)
                .ColorIndex = 1
            End With
            With .Borders(8)
                .ColorIndex = 1
            End With
            With .Borders(9)
                .ColorIndex = 1
            End With
            With .Borders(10)
                .ColorIndex = 1
            End With
            With .Borders(11)
                .ColorIndex = 1
            End With
            With .Borders(12)
                .ColorIndex = 1
            End With
            .Font.Bold = True
        End With

        'columna 2
        'filT0 = filT0   'fila inicio de la sección no cambia
        filT1 = filT0   'vuelvo al inicio el contador de filas

        colG0 = colG1 + 1   '1 columna a la derecha de donde termino la anterior
        colG1 = colG0 + 2   'seteo nuevo rango, en este caso seran 4
        Dim valor_libro As Double
        valor_libro = 0
        oSheet.Cells(filT1, colG0) = "FINANCIERO"
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = Rfin("precio_base")
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        valor_libro = valor_libro + CDbl(Rfin("precio_base"))
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = Rfin("vida_util_base")
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = Rfin("valor_residual")
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        '    valor_libro = valor_libro + CDbl(rpt1.Rows(Ffin).Item("valor_residual")) * -1
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = Rfin("depreciacion_acum")
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        valor_libro = valor_libro + CDbl(Rfin("depreciacion_acum"))
        If Not IsNothing(Rifrs) Then
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        End If
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = valor_libro
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        'aplico formato para la columna
        With oSheet.Range(lcol(colG0) + CStr(filT0) + ":" + lcol(colG1) + CStr(filT1))
            With .Borders(7)
                .ColorIndex = 1
            End With
            With .Borders(8)
                .ColorIndex = 1
            End With
            With .Borders(9)
                .ColorIndex = 1
            End With
            With .Borders(10)
                .ColorIndex = 1
            End With
            With .Borders(11)
                .ColorIndex = 1
            End With
            With .Borders(12)
                .ColorIndex = 1
            End With
            .HorizontalAlignment = -4152    'xlRight
            .VerticalAlignment = -4160      'xlTop
            .NumberFormat = "#,##0"
        End With

        'columna 3
        'filT0 = filT0   'fila inicio de la sección no cambia
        filT1 = filT0   'vuelvo al inicio el contador de filas

        colG0 = colG1 + 1   '1 columna a la derecha de donde termino la anterior
        colG1 = colG0 + 2   'seteo nuevo rango, en este caso seran 4

        valor_libro = 0
        oSheet.Cells(filT1, colG0) = "TRIBUTARIO"
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = Rtrib("precio_base")
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        valor_libro = valor_libro + CDbl(Rtrib("precio_base"))
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = Rtrib("vida_util_base")
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = Rtrib("valor_residual")
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        '    valor_libro = valor_libro + CDbl(rpt1.Rows(Ftrib).Item("valor_residual")) * -1
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = Rtrib("depreciacion_acum")
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        valor_libro = valor_libro + CDbl(Rtrib("depreciacion_acum"))
        If Not IsNothing(Rifrs) Then
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = "-"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        End If
        filT1 = filT1 + 1
        oSheet.Cells(filT1, colG0) = valor_libro
        oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
        'aplico formato para la columna
        With oSheet.Range(lcol(colG0) + CStr(filT0) + ":" + lcol(colG1) + CStr(filT1))
            With .Borders(7)
                .ColorIndex = 1
            End With
            With .Borders(8)
                .ColorIndex = 1
            End With
            With .Borders(9)
                .ColorIndex = 1
            End With
            With .Borders(10)
                .ColorIndex = 1
            End With
            With .Borders(11)
                .ColorIndex = 1
            End With
            With .Borders(12)
                .ColorIndex = 1
            End With
            .HorizontalAlignment = -4152    'xlRight
            .VerticalAlignment = -4160      'xlTop
            .NumberFormat = "#,##0"
        End With


        If Not IsNothing(Rifrs) Then
            'columna 4
            'filT0 = filT0   'fila inicio de la sección no cambia
            filT1 = filT0   'vuelvo al inicio el contador de filas

            colG0 = colG1 + 1   '1 columna a la derecha de donde termino la anterior
            colG1 = colG0 + 2   'seteo nuevo rango, en este caso seran 4

            valor_libro = 0
            oSheet.Cells(filT1, colG0) = "IFRS CLP"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Rifrs("precio_base")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            valor_libro = valor_libro + CDbl(Rifrs("precio_base"))
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Rifrs("vida_util_base")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Rifrs("valor_residual")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            '    valor_libro = valor_libro + CDbl(rpt1.Rows(Fifrs).Item("valor_residual")) * -1
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Rifrs("depreciacion_acum")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            valor_libro = valor_libro + CDbl(Rifrs("depreciacion_acum"))
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Rifrs("preparacion")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            valor_libro = valor_libro + CDbl(Rifrs("preparacion"))
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Rifrs("transporte")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            valor_libro = valor_libro + CDbl(Rifrs("transporte"))
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Rifrs("montaje")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            valor_libro = valor_libro + CDbl(Rifrs("montaje"))
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Rifrs("desmantel")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            valor_libro = valor_libro + CDbl(Rifrs("desmantel"))
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Rifrs("honorario")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            valor_libro = valor_libro + CDbl(Rifrs("honorario"))
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = Rifrs("revalorizacion")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            valor_libro = valor_libro + CDbl(Rifrs("honorario"))
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = valor_libro
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            'aplico formato para la columna
            With oSheet.Range(lcol(colG0) + CStr(filT0) + ":" + lcol(colG1) + CStr(filT1))
                With .Borders(7)
                    .ColorIndex = 1
                End With
                With .Borders(8)
                    .ColorIndex = 1
                End With
                With .Borders(9)
                    .ColorIndex = 1
                End With
                With .Borders(10)
                    .ColorIndex = 1
                End With
                With .Borders(11)
                    .ColorIndex = 1
                End With
                With .Borders(12)
                    .ColorIndex = 1
                End With
                .HorizontalAlignment = -4152    'xlRight
                .VerticalAlignment = -4160      'xlTop
                .NumberFormat = "#,##0"
            End With

            'columna 5
            'filT0 = filT0   'fila inicio de la sección no cambia
            filT1 = filT0   'vuelvo al inicio el contador de filas

            colG0 = colG1 + 1   '1 columna a la derecha de donde termino la anterior
            colG1 = colG0 + 2   'seteo nuevo rango, en este caso seran 4

            valor_libro = 0
            oSheet.Cells(filT1, colG0) = "IFRS YEN"
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            valor_libro = valor_libro + RifrsY("precio_base")
            oSheet.Cells(filT1, colG0) = RifrsY("precio_base")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = RifrsY("vida_util_base")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = RifrsY("valor_residual")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            valor_libro = valor_libro + CDbl(RifrsY("depreciacion_acum"))
            oSheet.Cells(filT1, colG0) = RifrsY("depreciacion_acum")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            valor_libro = valor_libro + CDbl(RifrsY("preparacion"))
            oSheet.Cells(filT1, colG0) = RifrsY("preparacion")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            valor_libro = valor_libro + CDbl(RifrsY("transporte"))
            oSheet.Cells(filT1, colG0) = RifrsY("transporte")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            valor_libro = valor_libro + CDbl(RifrsY("montaje"))
            oSheet.Cells(filT1, colG0) = RifrsY("montaje")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            valor_libro = valor_libro + CDbl(RifrsY("desmantel"))
            oSheet.Cells(filT1, colG0) = RifrsY("desmantel")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            valor_libro = valor_libro + CDbl(RifrsY("honorario"))
            oSheet.Cells(filT1, colG0) = RifrsY("honorario")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            valor_libro = valor_libro + CDbl(RifrsY("revalorizacion"))
            oSheet.Cells(filT1, colG0) = RifrsY("revalorizacion")
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            filT1 = filT1 + 1
            oSheet.Cells(filT1, colG0) = valor_libro
            oSheet.Range(lcol(colG0) + CStr(filT1) + ":" + lcol(colG1) + CStr(filT1)).MergeCells = True
            'aplico formato para la columna
            With oSheet.Range(lcol(colG0) + CStr(filT0) + ":" + lcol(colG1) + CStr(filT1))
                With .Borders(7)
                    .ColorIndex = 1
                End With
                With .Borders(8)
                    .ColorIndex = 1
                End With
                With .Borders(9)
                    .ColorIndex = 1
                End With
                With .Borders(10)
                    .ColorIndex = 1
                End With
                With .Borders(11)
                    .ColorIndex = 1
                End With
                With .Borders(12)
                    .ColorIndex = 1
                End With
                .HorizontalAlignment = -4152    'xlRight
                .VerticalAlignment = -4160      'xlTop
                .NumberFormat = "#,##0"
            End With
        End If

        With oSheet.Range("A" + CStr(filT0) + ":" + lcol(colG1) + CStr(filT0))
            .HorizontalAlignment = -4108    'xlCenter
            .Font.Bold = True
        End With

        'tercera seccion, ingreso de descripciones, si las hubiera.
        Dim colAtrib, filAtrib, conGen, conEsp As Integer
        colAtrib = 20 'columna fija'columna donde se ocupo ultima vez
        filAtrib = filT0 - 1 'fila superior desocupada
        'revisamos si hay descripcion generica
        conGen = 0
        conEsp = 0
        For i = 0 To rpt2.Rows.Count - 1
            Atrib = rpt2.Rows(i).Item("cod_atrib")
            If Not (Atrib = 16 Or Atrib = 17 Or Atrib = 18) Then
                If rpt2.Rows(i).Item("codigo") = "" Then
                    conGen = conGen + 1
                Else
                    conEsp = conEsp + 1
                End If
            End If
        Next
        If conGen <> 0 Then
            With oSheet.Cells(filAtrib, colAtrib)
                .Value = "DESCRIPCIÓN GENERICA"
                .Font.Bold = True
                .Font.Underline = True
            End With
            filAtrib = filAtrib + 2
            For i = 0 To rpt2.Rows.Count - 1
                Atrib = rpt2.Rows(i).Item("cod_atrib")
                If Not (Atrib = 16 Or Atrib = 17 Or Atrib = 18) Then
                    If rpt2.Rows(i).Item("codigo") = "" Then
                        oSheet.Cells(filAtrib, colAtrib) = rpt2.Rows(i).Item("atributo")
                        oSheet.Cells(filAtrib, colAtrib + 4) = "'" + rpt2.Rows(i).Item("dscr_detalle")
                        ver_largo = Len(rpt2.Rows(i).Item("dscr_detalle"))
                        extra = Int(ver_largo / 48)
                        With oSheet.Range(lcol(colAtrib + 4) + CStr(filAtrib) + ":" + lcol(colAtrib + 17) + CStr(filAtrib + extra))
                            .MergeCells = True
                            .HorizontalAlignment = -4131    'xlLeft
                            .VerticalAlignment = -4160      'xlTop
                            .WrapText = True
                        End With
                        filAtrib = filAtrib + extra + 1
                    End If
                End If
            Next
            filAtrib = filAtrib + 1
        End If
        If conEsp <> 0 Then
            With oSheet.Cells(filAtrib, colAtrib)
                .Value = "DESCRIPCIÓN ESPECÍFICA"
                .Font.Bold = True
                .Font.Underline = True
            End With
            filAtrib = filAtrib + 2
            j = 1
            For i = 0 To rpt2.Rows.Count - 1
                Atrib = rpt2.Rows(i).Item("cod_atrib")
                If Not (Atrib = 16 Or Atrib = 17 Or Atrib = 18) Then
                    If rpt2.Rows(i).Item("codigo") <> "" Then
                        If j = 1 Then
                            oSheet.Cells(filAtrib, colAtrib) = rpt2.Rows(i).Item("atributo")
                            oSheet.Cells(filAtrib, colAtrib).Font.Bold = True
                            filAtrib = filAtrib + 1
                            j = j + 1
                        Else
                            If rpt2.Rows(i).Item("cod_atrib") <> rpt2.Rows(i - 1).Item("cod_atrib") Then
                                oSheet.Cells(filAtrib, colAtrib) = rpt2.Rows(i).Item("atributo")
                                oSheet.Cells(filAtrib, colAtrib).Font.Bold = True
                                filAtrib = filAtrib + 1
                                j = j + 1
                            End If
                        End If
                        oSheet.Cells(filAtrib, colAtrib).NumberFormat = "@"
                        oSheet.Cells(filAtrib, colAtrib) = rpt2.Rows(i).Item("codigo")
                        oSheet.Cells(filAtrib, colAtrib + 4) = "'" + rpt2.Rows(i).Item("dscr_detalle")
                        ver_largo = Len(rpt2.Rows(i).Item("dscr_detalle"))
                        extra = Int(ver_largo / 48)
                        With oSheet.Range(lcol(colAtrib + 4) + CStr(filAtrib) + ":" + lcol(colAtrib + 17) + CStr(filAtrib + extra))
                            .MergeCells = True
                            .HorizontalAlignment = -4131    'xlLeft
                            .VerticalAlignment = -4160      'xlTop
                            .WrapText = True
                        End With
                        filAtrib = filAtrib + extra + 1
                    End If
                End If
            Next
            filAtrib = filAtrib + 1
        End If
        'cuarta seccion lineas para firma
        Dim largoFirma, filFirma, ColFirma As Integer
        largoFirma = 4      'contendra 5 celdas
        filFirma = filT1 + 7
        ColFirma = 5
        With oSheet.Range(lcol(ColFirma) + CStr(filFirma) + ":" + lcol(ColFirma + largoFirma) + CStr(filFirma))
            .MergeCells = True
            .HorizontalAlignment = -4108    'xlCenter
            .VerticalAlignment = -4108      'xlCenter
            .Borders(8).ColorIndex = 1
            .Value = "Contador General"
        End With
        ColFirma = ColFirma + largoFirma + 3
        With oSheet.Range(lcol(ColFirma) + CStr(filFirma) + ":" + lcol(ColFirma + largoFirma) + CStr(filFirma + 1))
            .MergeCells = True
            .HorizontalAlignment = -4108    'xlCenter
            .VerticalAlignment = -4108      'xlCenter
            .Borders(8).ColorIndex = 1
            .Value = "Gerencia" & vbCrLf & "(Opcional)"
        End With
        'quinta sección añadir fotos si las hubiera
        Dim indx_pag As Integer
        Dim imagen, nom_pag, cabezera, pies, periodo, nombre_total As String
        For i = 0 To rpt2.Rows.Count - 1
            Atrib = rpt2.Rows(i).Item("cod_atrib")
            If (Atrib = 16 Or Atrib = 17 Or Atrib = 18) Then
                oBook.Sheets.Add(After:=oBook.Sheets(oBook.Sheets.Count)).Name = "00"
                indx_pag = oBook.Sheets.Count
                imagen = base.dirFotos + rpt2.Rows(i).Item("dscr_detalle")
                nom_pag = rpt2.Rows(i).Item("atributo")
                If rpt2.Rows(i).Item("codigo") <> "" Then
                    nom_pag = nom_pag + "-" + rpt2.Rows(i).Item("codigo")
                End If
                cabezera = nom_pag
                pies = "&P / &N"
                Dim fSheet As Excel.Worksheet = oBook.Sheets(indx_pag)
                fSheet.Shapes.AddPicture(imagen, False, True, 0, 0, 709, 482)
                fSheet.Name = nom_pag
                With fSheet.PageSetup
                    .Orientation = 2
                    Try
                        If config_hoja Then
                            .PaperSize = Excel.XlPaperSize.xlPaperFolio
                        End If
                    Catch ex As Exception
                        Try
                            If config_hoja Then
                                .PaperSize = Excel.XlPaperSize.xlPaperLetter
                            End If
                        Catch ex2 As Exception
                            config_hoja = False
                            MessageBox.Show("No se establecio el tamaño del papel la hoja Excel." + Chr(13) + "Debe configurarlo manualmente", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            If Not crear_log_error(ex2) Then
                                MessageBox.Show("No se guardo log de error", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                        End Try
                    End Try
                    .CenterHeader = cabezera
                    .CenterFooter = pies
                    .CenterHorizontally = True
                    .CenterVertically = True
                End With
            End If
        Next

        'configurar pagina
        nom_pag = "ficha_baja"
        Dim Fcausa, causa As String
        Fcausa = rpt1.Rows(0).Item("cod_est")
        Select Case Fcausa
            Case "3"
                causa = "Castigo"
            Case "2"
                causa = "Venta"
            Case Else
                causa = ""
        End Select

        cabezera = "&""-,Negrita""&20Ficha de " + causa + "" & Chr(13) & "Activo Fijo"
        periodo = "&25 " + Format(rpt1.Rows(0).Item("fecha_inicio"), "yyyy-MM")
        oSheet.Name = nom_pag
        nombre_total = base.fileLogo
        oSheet.PageSetup.LeftHeaderPicture.Filename = nombre_total
        With oSheet.PageSetup
            .Orientation = 2
            Try
                If config_hoja Then
                    .PaperSize = Excel.XlPaperSize.xlPaperFolio
                End If
            Catch ex As Exception
                Try
                    If config_hoja Then
                        .PaperSize = Excel.XlPaperSize.xlPaperLetter
                    End If
                Catch ex2 As Exception
                    config_hoja = False
                    MessageBox.Show("No se establecio el tamaño del papel la hoja Excel." + Chr(13) + "Debe configurarlo manualmente", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If Not crear_log_error(ex2) Then
                        MessageBox.Show("No se guardo log de error", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Try
            End Try
            .LeftHeader = "&G"
            .CenterHeader = cabezera
            .RightHeader = periodo
            .LeftFooter = "&P / &N"
            .RightFooter = "Fecha de Impresion : &D" & Chr(13) & "Hora de Impresion: &T"
            .TopMargin = oExcel.InchesToPoints(1.2)
            .RightMargin = oExcel.InchesToPoints(1.01)
            .LeftMargin = oExcel.InchesToPoints(1.01)
            .CenterHorizontally = True
        End With
        oSheet.Select()
        oExcel.Visible = True
    End Sub

    Private Sub Sistema4_Click(sender As System.Object, e As System.EventArgs) Handles Sistema4.Click
        form_config.Show()
        'Me.Hide()
    End Sub

 
    Private Sub Proceso1_Click(sender As System.Object, e As System.EventArgs) Handles Proceso1.Click
        form_activar_lotes.ShowFrom(Me)
    End Sub

    Private Sub Cambio2_Click(sender As System.Object, e As System.EventArgs) Handles Cambio2.Click
        form_descripciones.ShowFrom(Me)
    End Sub

End Class