Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Threading

Public Class form_saldo_obc
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Protected base As New base_AFN

    Private Sub form_saldo_obc_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub

    Private Sub form_saldo_obc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        With cbPeriodo
            cbPeriodo.DataSource = base.lista_periodo(True)
            cbPeriodo.ValueMember = .DataSource.Columns(0).ColumnName
            cbPeriodo.DisplayMember = .DataSource.Columns(1).ColumnName
            cbPeriodo.SelectedIndex = 0
        End With
        With cbMoneda
            .DataSource = base.lista_monedas
            .ValueMember = .DataSource.Columns(0).ColumnName
            .DisplayMember = .DataSource.Columns(1).ColumnName
            .SelectedIndex = 1
        End With
        With cbResult
            .DataSource = base.opciones_OBC
            .ValueMember = .DataSource.Columns(0).ColumnName
            .DisplayMember = .DataSource.Columns(1).ColumnName
            .SelectedIndex = 0
        End With
        ckAcum.Checked = True
    End Sub

    Private Sub btn_buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_buscar.Click
        'validar combos
        If cbResult.SelectedIndex = -1 Then
            Mensaje_IT("Debe indicar que resultado desea obtener")
            cbResult.Focus()
            Exit Sub
        End If
        If cbPeriodo.SelectedIndex = -1 Then
            Mensaje_IT("Debe indicar un periodo para consultar")
            cbPeriodo.Focus()
            Exit Sub
        End If
        If cbMoneda.SelectedIndex = -1 Then
            Mensaje_IT("Debe indicar una moneda para consultar")
            cbMoneda.Focus()
            Exit Sub
        End If

        Dim currency(2) As String
        Dim moneda, opcion As Integer
        Dim fecha_cons, fecha_ini As Date
        Dim contar_curr As Integer
        fecha_cons = cbPeriodo.SelectedValue
        If ckAcum.Checked Then
            fecha_ini = DateSerial(fecha_cons.Year, 1, 1)
        Else
            fecha_ini = DateSerial(fecha_cons.Year, fecha_cons.Month, 1)
        End If
        moneda = cbMoneda.SelectedValue
        opcion = cbResult.SelectedValue
        Select Case moneda
            Case 1
                currency(0) = "CLP"
                currency(1) = "YEN"
                contar_curr = 2
            Case Else
                currency(0) = cbMoneda.Text
                currency(1) = String.Empty
                contar_curr = 1
        End Select
        Dim oExcel As New Excel.Application
        Dim oBook As Excel.Workbook = oExcel.Workbooks.Add
        If oBook.Sheets.Count < contar_curr Then
            For i = oBook.Sheets.Count + 1 To contar_curr
                oBook.Sheets.Add()
            Next
        End If
        If oBook.Sheets.Count > contar_curr Then
            For i = oBook.Sheets.Count To contar_curr + 1 Step -1
                oBook.Sheets(i).Delete()
            Next
        End If
        Try
            For i = 1 To contar_curr
                Dim colchon As DataTable
                Select Case opcion
                    Case 1
                        colchon = base.saldos_obc(fecha_cons, currency(i - 1))
                    Case 2
                        colchon = base.entradas_obc(fecha_ini, fecha_cons, currency(i - 1))
                    Case Else
                        bloquearW(Me)
                        'Creamos una instancia de la clase multi hilo
                        Dim cmh As BackProcess = base.back_salidas_obc
                        Dim fbp As ProgressShow = cargar_barra(Me)
                        fbp.inicializar(1)
                        fbp.definir_proceso(0, cmh.intervalos)
                        'seteamos los campos que normalmente pasariamos como parametros
                        cmh.ini_salidas_obc(fecha_ini, fecha_cons, currency(i - 1))
                        'Esperamos a que termine la ejecucion del hilo
                        While cmh.isWorking
                            cmh.Keep()
                            If cmh.UpdateBar Then
                                fbp.continua_proceso(0)
                            End If
                        End While
                        colchon = cmh.resultado
                        descargar_barra(Me)
                        desbloquearW(Me)
                End Select
                Dim oSheet As Excel.Worksheet = oBook.Sheets(i)
                oSheet.Name = cbResult.Text + currency(i - 1).ToString
                Dim tipo_col As String
                Dim col_titulo As String
                col_titulo = ""
                Dim Tmatrix(,) As String
                Dim Nmatrix(,) As Double
                Dim Imatrix(,) As Integer
                Dim DMatrix(,) As Date
                ReDim Tmatrix(colchon.Rows.Count - 1, 0)
                ReDim Nmatrix(colchon.Rows.Count - 1, 0)
                ReDim DMatrix(colchon.Rows.Count - 1, 0)
                ReDim Imatrix(colchon.Rows.Count - 1, 0)
                For j = 0 To colchon.Columns.Count - 1
                    col_titulo = colchon.Columns(j).ColumnName
                    tipo_col = colchon.Columns(j).DataType.Name
                    For k = 0 To colchon.Rows.Count - 1
                        Dim filt As DataRow = colchon.Rows(k)
                        Select Case tipo_col
                            Case "Int32", "Int16"
                                Imatrix(k, 0) = filt.Item(j)
                            Case "Decimal", "Double", "Int64"
                                Nmatrix(k, 0) = filt.Item(j)
                            Case "String"
                                Tmatrix(k, 0) = filt.Item(j).ToString
                            Case "DateTime"
                                DMatrix(k, 0) = filt.Item(j)
                            Case "SqlInt32", "SqlInt16"
                                Imatrix(k, 0) = CInt(filt.Item(j).Value)
                            Case "SqlInt64", "SqlDecimal"
                                Nmatrix(k, 0) = filt.Item(j).Value
                            Case "SqlString"
                                Tmatrix(k, 0) = filt.Item(j).Value.ToString
                        End Select
                        Application.DoEvents()
                    Next
                    Dim oCell As Excel.Range = oSheet.Cells(1, j + 1)
                    With oCell
                        .Value = col_titulo
                        .Font.Bold = True
                        .RowHeight = 30
                        .WrapText = True
                        .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                        .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                    End With
                    oCell = Nothing
                    Select Case tipo_col
                        Case "Int32", "Int16", "SqlInt32", "SqlInt16"
                            oSheet.Columns(lcol(j + 1) + ":" + lcol(j + 1)).NumberFormat = "#,##0"
                            oSheet.Range(lcol(j + 1) + "2" + ":" + lcol(j + 1) + CStr(colchon.Rows.Count + 1)).Value = Imatrix
                        Case "Int64", "SqlInt64"
                            oSheet.Columns(lcol(j + 1) + ":" + lcol(j + 1)).NumberFormat = "#,##0"
                            oSheet.Range(lcol(j + 1) + "2" + ":" + lcol(j + 1) + CStr(colchon.Rows.Count + 1)).Value = Nmatrix
                        Case "Decimal", "Double", "SqlDecimal"
                            oSheet.Columns(lcol(j + 1) + ":" + lcol(j + 1)).NumberFormat = "#,##0"
                            oSheet.Range(lcol(j + 1) + "2" + ":" + lcol(j + 1) + CStr(colchon.Rows.Count + 1)).Value = Nmatrix
                        Case "String", "SqlString"
                            oSheet.Columns(lcol(j + 1) + ":" + lcol(j + 1)).NumberFormat = "@"
                            oSheet.Range(lcol(j + 1) + "2" + ":" + lcol(j + 1) + CStr(colchon.Rows.Count + 1)).Value = Tmatrix
                        Case "DateTime"
                            oSheet.Columns(lcol(j + 1) + ":" + lcol(j + 1)).NumberFormat = "dd-MM-yyyy"
                            oSheet.Range(lcol(j + 1) + "2" + ":" + lcol(j + 1) + CStr(colchon.Rows.Count + 1)).Value = DMatrix
                    End Select
                    Application.DoEvents()
                Next
                oSheet.Columns("A:" + lcol(colchon.Columns.Count)).EntireColumn.AutoFit()
                If opcion <> 3 Then
                    oSheet.Columns("B").ColumnWidth = 80
                End If
            Next
        Catch ex As Exception
            Mensaje_Err(ex.Message)
        Finally
            oExcel.Visible = True
        End Try
    End Sub

End Class