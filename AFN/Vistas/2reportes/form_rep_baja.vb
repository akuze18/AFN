Imports System.ComponentModel
Imports System.Threading
Imports Excel = Microsoft.Office.Interop.Excel

Public Class form_rep_baja

#Region "Variables de Clase"

    Dim oExcel As Excel.Application
    Dim oBook As Excel.Workbook
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private base As New base_AFN

    Protected Friend Structure dato_baja
        Public Sdesde As Date
        Public Shasta As Date
        Public Ssitu As String
        Public ScodSitu As Integer
        Public Sreporte As ListBox.SelectedObjectCollection
    End Structure

    Dim Entrada As dato_baja
    Dim colchon As DataTable
    Dim consulta1 As String

#End Region

#Region "Funciones del Formulario"

    Private Sub form_rep_baja_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub
    Private Sub form_rep_baja_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim Tperiodo1, Tperiodo2, Tsituacion As DataTable
        Dim Treporte As listado_basico
        'llenar periodo desde
        With cb_desde
            Tperiodo1 = base.lista_periodo(True, False)
            .DataSource = Tperiodo1
            .ValueMember = Tperiodo1.Columns(0).ColumnName
            .DisplayMember = Tperiodo1.Columns(1).ColumnName
            .SelectedIndex = 0
        End With

        'llenar periodo hasta
        With cb_hasta
            Tperiodo2 = base.lista_periodo(True)
            .DataSource = Tperiodo2
            .ValueMember = Tperiodo2.Columns(0).ColumnName
            .DisplayMember = Tperiodo2.Columns(1).ColumnName
            .SelectedIndex = 0
        End With
        
        'llenar situacion
        With cb_situ
            Tsituacion = base.ESTADOS_BAJA
            .DataSource = Tsituacion
            .ValueMember = Tsituacion.Columns(0).ColumnName
            .DisplayMember = Tsituacion.Columns(1).ColumnName
            .SelectedIndex = 0
        End With

        ''llenar reportes
        With lb_reporte
            Treporte = base.reportes_baja
            For Each reporte As listado_basico.fila In Treporte.Rows
                .Items.Add(reporte)
            Next
        End With

    End Sub
    Private Function validar_form() As Boolean
        If cb_desde.SelectedIndex = -1 Then
            base.mensaje_alerta("Debe ingresar una fecha")
            cb_desde.Focus()
            Return False
        End If
        If cb_hasta.SelectedIndex = -1 Then
            base.mensaje_alerta("Debe ingresar una fecha")
            cb_hasta.Focus()
            Return False
        End If
        If cb_situ.SelectedIndex = -1 Then
            base.mensaje_alerta("Debe indicar la situación")
            cb_situ.Focus()
            Return False
        End If
        If lb_reporte.SelectedIndex = -1 Then
            base.mensaje_alerta("Debe indicar al menos un reporte para generar")
            lb_reporte.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub btn_all_Click(sender As System.Object, e As System.EventArgs) Handles btn_all.Click
        For i = 0 To lb_reporte.Items.Count - 1
            lb_reporte.SelectedIndex = i
        Next
    End Sub

    Private Sub btn_none_Click(sender As System.Object, e As System.EventArgs) Handles btn_none.Click
        lb_reporte.SelectedIndex = -1
    End Sub


#End Region

#Region "Funciones del Menu"
    Private Sub btn_generar_Click(sender As System.Object, e As System.EventArgs) Handles btn_generar.Click
        If validar_form() Then
            Entrada.Sdesde = cb_desde.SelectedValue
            Entrada.Shasta = cb_hasta.SelectedValue
            Entrada.ScodSitu = cb_situ.SelectedValue
            Entrada.Ssitu = cb_situ.Text
            Entrada.Sreporte = lb_reporte.SelectedItems
            generar_baja_det()
            'BGworker.RunWorkerAsync(Entrada)
        End If
    End Sub
#End Region

#Region "Funciones del Proceso"
    'Private Sub BGworker_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BGworker.DoWork
    'End Sub
    Protected Friend Sub generar_baja_det()
        Dim avance As ProgressShow = cargar_barra(Me)
        Dim dato_proceso As DataTable()
        Dim cont_rep As Integer
        bloquearW(Me)
        cont_rep = Entrada.Sreporte.Count

        avance.inicializar(2 + cont_rep)
        avance.cambiar_parte(0, 25)
        dato_proceso = calc_baja_detalle()
        avance.continua_proceso()

        oExcel = New Excel.Application
        oBook = oExcel.Workbooks.Add
        If dato_proceso.Count < oBook.Sheets.Count Then
            For pagina = oBook.Sheets.Count To dato_proceso.Count + 1 Step -1
                oBook.Sheets(pagina).Delete()
            Next
        Else
            For pagina = oBook.Sheets.Count + 1 To dato_proceso.Count Step 1
                oBook.Sheets.Add()
            Next
        End If

        For i = 0 To dato_proceso.Count - 1
            avance.cambiar_parte(i + 1, (60 / cont_rep))
            If dato_proceso(i).Columns.Count > 0 Then
                avance.definir_proceso(i + 1, dato_proceso(i).Columns.Count)
                Dim sRep As listado_basico.fila = Entrada.Sreporte.Item(i)
                imp_baja_detalle(dato_proceso(i), i + 1, avance, sRep.DB_AMBIENTE)
                formato_pagina(i + 1, sRep.DB_AMBIENTE)

            Else
                avance.continua_proceso()
                base.mensaje_alerta("")
            End If
        Next
        avance.continua_proceso()
        
        oExcel.Visible = True
        desbloquearW(Me)
        avance.termina_proceso()
        descargar_barra(Me)
    End Sub

    Private Function calc_baja_detalle() As DataTable()
        Dim Mtp As String
        Dim temp_rs(Entrada.Sreporte.Count - 1) As DataTable
        For i = 0 To Entrada.Sreporte.Count - 1
            Dim reporte As listado_basico.fila
            reporte = Entrada.Sreporte(i)
            Mtp = reporte.DB_AMBIENTE
            
            If Mtp = "GC" Or Mtp = "GY" Then
                colchon = base.REPORTE_BAJA_DETALLE_IFRS(Entrada.Sdesde, Entrada.Shasta, Mtp, Entrada.ScodSitu)
            Else
                colchon = base.REPORTE_BAJA_DETALLE_LOC(Entrada.Sdesde, Entrada.Shasta, Mtp, Entrada.ScodSitu)
            End If
            temp_rs(i) = colchon
        Next
        Return temp_rs
    End Function
    Private Function imp_baja_detalle(ByVal valores As DataTable, ByVal indice As Integer, ByRef avance As ProgressShow, ByVal tipo As String, Optional ByVal inicio As Integer = 1) As Integer
        Dim dato_proceso As DataTable
        Dim ini_titulo, fila_actual As Integer
        Dim mtit As String
        Dim oSheet As Excel.Worksheet

        oSheet = oBook.Sheets(indice)
        dato_proceso = valores
        ini_titulo = inicio
        'ingresamos cabecera
        oSheet.Cells(ini_titulo, 1) = "Desde:"
        oSheet.Cells(ini_titulo, 2).NumberFormat = "dd-MM-yyyy"
        oSheet.Cells(ini_titulo, 2) = DateValue(Entrada.Sdesde)
        Select Case tipo
            Case "F"
                mtit = "Bajas de Financiero"
            Case "T"
                mtit = "Bajas de Tributario"
            Case "Y"
                mtit = "Bajas de Financiero (YEN)"
            Case "GC"
                mtit = "Bajas de IFRS"
            Case "GY"
                mtit = "Bajas de IFRS (YEN)"
            Case Else
                mtit = ""
        End Select
        oSheet.Cells(ini_titulo, 4) = UCase(mtit)
        ini_titulo = ini_titulo + 1
        oSheet.Cells(ini_titulo, 1) = "Hasta:"
        oSheet.Cells(ini_titulo, 2).NumberFormat = "dd-MM-yyyy"
        oSheet.Cells(ini_titulo, 2) = DateValue(Entrada.Shasta)
        ini_titulo = ini_titulo + 1
        oSheet.Cells(ini_titulo, 1) = "Situación:"
        oSheet.Cells(ini_titulo, 2) = Entrada.Ssitu
        ini_titulo = ini_titulo + 2
        'fin cabecera
        Application.DoEvents()
        'formato titulos
        With oSheet.Rows(ini_titulo & ":" & ini_titulo)
            .HorizontalAlignment = -4108        '-4108 equivale a centrar
            .VerticalAlignment = -4108
            .WrapText = True                    'equivale a alinear contenido a la celda
            .Orientation = 0
            .AddIndent = False
            .IndentLevel = 0
            .ShrinkToFit = False
            .RowHeight = 30                     'alto de fila se estable en 30
            .Font.Bold = True
        End With

        Application.DoEvents()
        'fin todo lo de titulos
        'fila_actual = ini_titulo + 1        'fila siguiente a los titulos del reporte

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
                    oSheet.Range(lcol(j + 1) + fila_actual.ToString + ":" + lcol(j + 1) + CStr(fila_actual + dato_proceso.Rows.Count - 1)).Value = Imatrix
                Case "Int64"
                    oSheet.Columns(lcol(j + 1) + ":" + lcol(j + 1)).NumberFormat = "#,##0;[red]-#,##0"
                    oSheet.Range(lcol(j + 1) + fila_actual.ToString + ":" + lcol(j + 1) + CStr(fila_actual + dato_proceso.Rows.Count - 1)).Value = Nmatrix
                Case "Decimal", "Double"
                    oSheet.Columns(lcol(j + 1) + ":" + lcol(j + 1)).NumberFormat = "#,##0;[red]-#,##0"
                    oSheet.Range(lcol(j + 1) + fila_actual.ToString + ":" + lcol(j + 1) + CStr(fila_actual + dato_proceso.Rows.Count - 1)).Value = Nmatrix
                Case "String"
                    oSheet.Columns(lcol(j + 1) + ":" + lcol(j + 1)).NumberFormat = "@"
                    oSheet.Range(lcol(j + 1) + fila_actual.ToString + ":" + lcol(j + 1) + CStr(fila_actual + dato_proceso.Rows.Count - 1)).Value = Tmatrix
                Case "DateTime"
                    oSheet.Columns(lcol(j + 1) + ":" + lcol(j + 1)).NumberFormat = "dd-MM-yyyy"
                    oSheet.Range(lcol(j + 1) + fila_actual.ToString + ":" + lcol(j + 1) + CStr(fila_actual + dato_proceso.Rows.Count - 1)).Value = DMatrix
            End Select
            Application.DoEvents()
            avance.continua_proceso()
        Next
        'formato general
        'oSheet.Columns("C:V").NumberFormat = "#,###;[red]-#,###;0"
        oSheet.Columns("A:A").NumberFormat = "#;"
        oSheet.Columns("A:P").EntireColumn.AutoFit()
        oSheet.Columns("A:D").ColumnWidth = 12
        oSheet.Columns("E:E").ColumnWidth = 55
        '    With oSheet.Columns("H:H")
        '        .ColumnWidth = 40
        '        .WrapText = False
        '        .ShrinkToFit = True
        '    End With
        Application.DoEvents()
        imp_baja_detalle = fila_actual + dato_proceso.Rows.Count
    End Function
    Private Function formato_pagina(ByVal indx_pag As Integer, ByVal Ttipo As String) As Boolean
        Dim nom_pag As String
        Try
            nom_pag = "DETALLE_" + Ttipo + Format(Entrada.Shasta, "yyyy-MM-dd")
            oBook.Sheets(indx_pag).Name = nom_pag
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region


    
   
End Class