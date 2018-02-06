Public Class form_venta
    Dim AFNok, AFNfail, AFNprocess As Color
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private maestro As New master_control

    Private Sub form_venta_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub
    Private Sub form_venta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'agrego columnas
        Dim recordset As New DataTable
        recordset.Columns.Add(" ")
        recordset.Columns.Add("Código Artículo")
        recordset.Columns.Add("Fecha Venta", System.Type.GetType("System.DateTime"))
        recordset.Columns.Add("Cantidad Vendida")
        recordset.Columns.Add("Descripcion Artículo")
        recordset.Columns.Add("Zona")
        recordset.Columns.Add("indice fila")
        recordset.Columns.Add("parte")
        lista_vender.DataSource = recordset
        lista_vender.ColumnHeadersHeight = lista_vender.ColumnHeadersHeight * 2
        lista_vender.RowHeadersVisible = False
        lista_vender.AllowUserToResizeColumns = False
        For Each columna As DataGridViewColumn In lista_vender.Columns
            columna.SortMode = DataGridViewColumnSortMode.NotSortable
            'columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        Next
        lista_vender.Columns(0).Visible = False
        lista_vender.Columns(1).Width = 60
        lista_vender.Columns(2).Width = 90
        lista_vender.Columns(3).Width = 67
        lista_vender.Columns(4).Width = 300
        lista_vender.Columns(5).Width = 45
        lista_vender.Columns(6).Visible = False
        lista_vender.Columns(7).Visible = False
        iniciar_formulario()
        Dventa.Value = Now
        Dventa.MaxDate = Now
        AFNok = ColorTranslator.FromOle(RGB(125, 255, 125))
        AFNfail = ColorTranslator.FromOle(RGB(255, 50, 50))
        AFNprocess = ColorTranslator.FromOle(RGB(255, 255, 125))
    End Sub

    Private Sub iniciar_formulario()
        'bloqueo
        cboCant.Items.Clear()
        Dventa.Enabled = False
        Tarticulo.Enabled = False
        Tarticulo.Text = String.Empty
        Tarticulo.BackColor = Color.LightGray
        Tvalor.Enabled = False
        Tvalor.Text = String.Empty
        Tvalor.BackColor = Color.LightGray
        btn_consulta.BackColor = Color.Red
    End Sub
    Public Sub cargar_formulario(ByVal filaRef As Integer)
        iniciar_formulario()
        Dim recordset As DataTable
        Dim sql_articulo As String
        Dim cantid As Integer
        sql_articulo = "SELECT cod_articulo,parte,dsc_breve,zona,cantidad,val_libro" + Chr(13) + _
        "FROM AFN_base_local2('" + Format(Now, "yyyyMMdd") + "','F')" + Chr(13) + _
        "WHERE fila=" + CStr(filaRef)
        recordset = maestro.ejecuta(sql_articulo)
        cantid = recordset.Rows(0).Item("cantidad")
        For i = 1 To cantid
            cboCant.Items.Add(i)
        Next
        cod_art.Text = recordset.Rows(0).Item("cod_articulo")
        Gparte.Text = recordset.Rows(0).Item("parte")
        zona_art.Text = recordset.Rows(0).Item("zona")
        rowindx.Text = filaRef
        cboCant.SelectedIndex = cboCant.Items.Count - 1
        Dventa.Enabled = True
        'Dventa.MaxDate = Now
        'Dventa.Value = Now
        Tarticulo.Text = recordset.Rows(0).Item("dsc_breve")
        Tarticulo.BackColor = System.Drawing.SystemColors.Window
        Tvalor.Text = recordset.Rows(0).Item("val_libro")
        Tvalor.BackColor = System.Drawing.SystemColors.Window
        btn_consulta.BackColor = System.Drawing.SystemColors.Window


    End Sub

    Private Sub btn_consulta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_consulta.Click
        busqueda_articulo.Show()
        busqueda_articulo.actualizar_origen("V", Me)
    End Sub
    Private Sub lista_vender_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles lista_vender.CellClick
        If e.RowIndex = -1 Then
            lista_vender.ClearSelection()
        End If
    End Sub

    Private Sub btn_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_add.Click
        If rowindx.Text <> "" Then
            Dim pasa As Boolean
            'busco que no este ingresado
            pasa = True
            For Each fila As DataGridViewRow In lista_vender.Rows
                If fila.Cells(6).Value = rowindx.Text Then
                    pasa = False
                End If
            Next
            If pasa Then
                Dim recordset As DataTable = lista_vender.DataSource
                Dim newfila As DataRow = recordset.NewRow
                newfila(1) = cod_art.Text
                newfila(2) = Format(Dventa.Value, "dd-MM-yyyy")
                newfila(3) = cboCant.Text
                newfila(4) = Tarticulo.Text
                newfila(5) = zona_art.Text
                newfila(6) = rowindx.Text
                newfila(7) = Gparte.Text
                recordset.Rows.Add(newfila)
                lista_vender.ClearSelection()
            Else
                MsgBox("Articulo indicado ya ha sido agregado para la venta", vbExclamation, "NH FOODS CHILE")
            End If
        End If
    End Sub
    Private Sub btn_remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_remove.Click
        Dim limpiar As Boolean
        limpiar = False
        If lista_vender.SelectedRows.Count > 0 Then
            For Each fila_sel As DataGridViewRow In lista_vender.SelectedRows
                Dim elige As DialogResult
                elige = MessageBox.Show("Está seguro que desea eliminar el artículo " + fila_sel.Cells(0).Value.ToString + " de la lista de ventas?", "NH FOODS CHILE", MessageBoxButtons.YesNo)
                If elige = Windows.Forms.DialogResult.Yes Then
                    lista_vender.Rows.Remove(fila_sel)
                    limpiar = True
                End If
            Next
        Else
            MessageBox.Show("No ha indicado ningun registro para quitar del listado de venta", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        If limpiar Then
            lista_vender.ClearSelection()
        End If
    End Sub
    Private Sub btn_fin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_fin.Click
        If lista_vender.Rows.Count > 0 Then
            Dim elige As DialogResult
            Dim filaindice, codigo_articulo, parte_articulo, newfecha, newcantidad, sql_castigo As String
            elige = MessageBox.Show("Está seguro que desea procesar los artículos de la lista de ventas?", "NH FOODS CHILE", MessageBoxButtons.YesNo)
            If elige = Windows.Forms.DialogResult.Yes Then
                Dim recordset As New DataTable
                For Each fila As DataGridViewRow In lista_vender.Rows
                    If fila.DefaultCellStyle.BackColor <> AFNok Then
                        'las celdas que estuvieran ok no se procesan
                        fila.DefaultCellStyle.BackColor = AFNprocess
                        lista_vender.Refresh()
                        filaindice = fila.Cells(6).Value
                        codigo_articulo = fila.Cells(1).Value
                        parte_articulo = fila.Cells(7).Value
                        newfecha = Format(fila.Cells(2).Value, "yyyyMMdd")
                        newcantidad = fila.Cells(3).Value
                        sql_castigo = "EXEC AFN_venta_act " + codigo_articulo + "," + parte_articulo + ",'" + newfecha + "'," + newcantidad + ",'" + form_welcome.GetUsuario + "'"
                        recordset = maestro.ejecuta(sql_castigo)
                        If recordset.Rows(0).Item("cod_sit") < 0 Then
                            'se produjo un error al momento de generar al castigo en la base de datos
                            fila.DefaultCellStyle.BackColor = AFNfail
                            lista_vender.Refresh()
                            MessageBox.Show(recordset.Rows(0).Item("dsc_sit").ToString + vbCrLf + "Fila: " + fila.Index.ToString, "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else
                            fila.DefaultCellStyle.BackColor = AFNok
                            lista_vender.Refresh()
                        End If
                    End If
                    Application.DoEvents()
                Next
                recordset = Nothing
                MessageBox.Show("Venta se ha realizado con exito", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Else
            MessageBox.Show("No ha agregado ningun articulo al listado de venta", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

End Class