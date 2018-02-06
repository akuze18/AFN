Public Class form_castigo
    Dim AFNok As Color = ColorTranslator.FromOle(RGB(125, 255, 125))
    Dim AFNfail As Color = ColorTranslator.FromOle(RGB(255, 50, 50))
    Dim AFNprocess As Color = ColorTranslator.FromOle(RGB(255, 255, 125))

    Dim ActualDetalleLote, TotalDetalleLote As DataTable

    'Private maestro As New master_control
    ''' <summary>
    ''' Instancia del forumario que contiene toda la logica del proceso
    ''' </summary>
    ''' <remarks></remarks>
    Private base As New base_AFN

    Private Sub form_castigo_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub
    Private Sub form_castigo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'cargo columnas
        With lista_castigar
            .DataSource = base.lista_por_castigar()
            .ColumnHeadersHeight = lista_castigar.ColumnHeadersHeight * 2
            .RowHeadersVisible = False
            .AllowUserToResizeColumns = False
            For Each columna As DataGridViewColumn In .Columns
                columna.SortMode = DataGridViewColumnSortMode.NotSortable
                'columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            Next
        End With
        'configuro anchos
        With lista_castigar
            .Columns(0).Visible = False
            .Columns(1).Width = 60
            .Columns(2).Width = 90
            .Columns(3).Width = 67
            .Columns(4).Width = 300
            .Columns(5).Visible = False
            .Columns(6).Visible = False
            .Columns(7).Visible = False
            .Columns(8).Visible = False
        End With
        TotalDetalleLote = base.lista_det_total_art
        iniciar_formulario()
        Dcastigo.Value = Now
        Dcastigo.MaxDate = Now
    End Sub

    Private Sub iniciar_formulario()
        'bloqueo
        cboCant.Items.Clear()
        Dcastigo.Enabled = False
        Tarticulo.Enabled = False
        Tarticulo.Text = String.Empty
        Tarticulo.BackColor = Color.LightGray
        Tvalor.Enabled = False
        Tvalor.Text = String.Empty
        CheckF.Enabled = False
        CheckI.Enabled = False
        Tvalor.BackColor = Color.LightGray
        btn_consulta.BackColor = Color.Red
    End Sub
    Public Sub cargar_formulario(ByVal filaRef As Integer)
        iniciar_formulario()
        Dim recordset As DataTable
        Dim cantid As Integer
        recordset = base.BASE_LOCAL(Now, filaRef)
        cantid = recordset.Rows(0).Item("cantidad")
        For i = 1 To cantid
            cboCant.Items.Add(i)
        Next
        cod_art.Text = recordset.Rows(0).Item("cod_articulo")
        Gparte.Text = recordset.Rows(0).Item("parte")
        zona_art.Text = recordset.Rows(0).Item("zona")
        rowindx.Text = filaRef
        cboCant.SelectedIndex = cboCant.Items.Count - 1
        Dcastigo.Enabled = True
        Tarticulo.Text = recordset.Rows(0).Item("dsc_breve")
        Tarticulo.BackColor = System.Drawing.SystemColors.Window
        Tvalor.Text = recordset.Rows(0).Item("val_libro")
        Tvalor.BackColor = System.Drawing.SystemColors.Window
        btn_consulta.BackColor = System.Drawing.SystemColors.Window
    End Sub

    Private Sub btn_consulta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_consulta.Click
        busqueda_articulo.Show()
        busqueda_articulo.actualizar_origen("C", Me)
    End Sub
    Private Sub lista_castigar_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles lista_castigar.CellContentClick
        If e.RowIndex = -1 Then
            lista_castigar.ClearSelection()
        End If
    End Sub

    Private Sub btn_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_add.Click
        If rowindx.Text <> "" Then
            Dim pasa As Boolean
            'busco que no este ingresado
            pasa = True
            For Each fila As DataGridViewRow In lista_castigar.Rows
                If fila.Cells(5).Value = rowindx.Text Then
                    pasa = False
                End If
            Next
            If pasa Then
                Dim recordset As DataTable = lista_castigar.DataSource
                Dim newfila As DataRow = recordset.NewRow
                newfila(1) = cod_art.Text
                newfila(2) = Dcastigo.Value
                newfila(3) = cboCant.Text
                newfila(4) = Tarticulo.Text
                newfila(5) = rowindx.Text
                newfila(6) = zona_art.Text
                newfila(7) = CheckT.Checked
                newfila(8) = Gparte.Text
                recordset.Rows.Add(newfila)

                detalle_add(ActualDetalleLote, cod_art.Text, Gparte.Text, rowindx.Text)

                lista_castigar.ClearSelection()
            Else
                MsgBox("Articulo indicado ya ha sido agregado para el castigo", vbExclamation, "NH FOODS CHILE")
            End If
        End If
    End Sub
    Private Sub btn_remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_remove.Click
        Dim limpiar As Boolean
        limpiar = False
        If lista_castigar.SelectedRows.Count > 0 Then
            For Each fila_sel As DataGridViewRow In lista_castigar.SelectedRows
                Dim elige As DialogResult
                elige = MessageBox.Show("Está seguro que desea eliminar el artículo " + fila_sel.Cells(0).Value.ToString + " de la lista de castigos?", "NH FOODS CHILE", MessageBoxButtons.YesNo)
                If elige = Windows.Forms.DialogResult.Yes Then
                    detalle_less(fila_sel.Cells(5).Value)
                    lista_castigar.Rows.Remove(fila_sel)
                    limpiar = True
                End If
            Next
        Else
            MessageBox.Show("No ha indicado ningun registro para quitar del listado de castigo", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        If limpiar Then
            lista_castigar.ClearSelection()
        End If
    End Sub
    Private Sub btn_fin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_fin.Click
        If lista_castigar.Rows.Count > 0 Then
            Dim elige As DialogResult
            Dim filaindice, codigo_articulo, parte_articulo, newcantidad As String
            Dim newfecha As Date
            Dim procT, codigo_grupo As Integer
            elige = MessageBox.Show("Está seguro que desea procesar los artículos de la lista de castigos?", "NH FOODS CHILE", MessageBoxButtons.YesNo)
            If elige = Windows.Forms.DialogResult.Yes Then
                Dim recordset As New DataTable
                For Each fila As DataGridViewRow In lista_castigar.Rows
                    If fila.DefaultCellStyle.BackColor <> AFNok Then
                        'las celdas que estuvieran ok no se procesan
                        fila.DefaultCellStyle.BackColor = AFNprocess
                        lista_castigar.Refresh()
                        filaindice = fila.Cells(5).Value
                        codigo_articulo = fila.Cells(1).Value
                        parte_articulo = fila.Cells(8).Value
                        newfecha = fila.Cells(2).Value
                        newcantidad = fila.Cells(3).Value
                        procT = fila.Cells(7).Value
                        codigo_grupo = fila.Cells(5).Value
                        recordset = base.CASTIGO(codigo_articulo, parte_articulo, newfecha, newcantidad, procT, form_welcome.GetUsuario, TotalDetalleLote, codigo_grupo)
                        If recordset.Rows(0).Item("cod_sit") < 0 Then
                            'se produjo un error al momento de generar al castigo en la base de datos
                            fila.DefaultCellStyle.BackColor = AFNfail
                            lista_castigar.Refresh()
                            MessageBox.Show(recordset.Rows(0).Item("dsc_sit").ToString + vbCrLf + "Fila: " + fila.Index.ToString, "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else
                            fila.DefaultCellStyle.BackColor = AFNok
                            lista_castigar.Refresh()
                        End If
                    End If
                    Application.DoEvents()
                Next
                recordset = Nothing
                MessageBox.Show("Castigo se ha realizado con exito", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Else
            MessageBox.Show("No ha agregado ningun articulo al listado de castigo", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub cboCant_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboCant.SelectedIndexChanged
        If cboCant.SelectedIndex <> -1 Then
            Dim cantid As Integer
            cantid = cboCant.SelectedItem
            ActualDetalleLote = base.lista_det_articulo(cod_art.Text, Gparte.Text, cantid)
        End If
    End Sub

    Private Sub btn_detalle_cantidad_Click(sender As System.Object, e As System.EventArgs) Handles btn_detalle_cantidad.Click
        'Valido que la información necesaria para activar esta opcion este completa
        If String.IsNullOrEmpty(cod_art.Text) Then
            Exit Sub
        End If
        If cboCant.SelectedIndex = -1 Then
            Exit Sub
        End If

        If IsNothing(ActualDetalleLote) Then
            MessageBox.Show("Se produjo un error al obtener el detalle de los articulos del lote", "NH Foods Chile", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim resultado As DialogResult
        Dim aux As form_detalle_articulo
        aux = New form_detalle_articulo(cod_art.Text, Gparte.Text, form_detalle_articulo.form_accion.castigo, ActualDetalleLote)
        resultado = aux.ShowDialog()
        If resultado = Windows.Forms.DialogResult.OK Then
            ActualDetalleLote = aux.detalle
        End If
    End Sub

#Region "Manejo de TotalDetalleLote"
    Private Sub detalle_add(ByVal detalle_producto As DataTable, ByVal lote As Integer, ByVal parte As Integer, ByVal rowid As Integer)

        For Each producto As DataRow In detalle_producto.Rows
            Dim newFila As DataRow
            newFila = TotalDetalleLote.NewRow
            newFila("producto") = producto.Item("Codigo Producto")
            newFila("lote") = lote
            newFila("parte") = parte
            newFila("procesar") = producto.Item("Procesar")
            newFila("row_id") = rowid
            TotalDetalleLote.Rows.Add(newFila)
        Next

    End Sub
    Private Sub detalle_less(ByVal rowid As Integer)
        Dim fila_eliminada As DataRow()
        fila_eliminada = TotalDetalleLote.Select("row_id=" + rowid.ToString)
        For Each fila As DataRow In fila_eliminada
            TotalDetalleLote.Rows.Remove(fila)
        Next

    End Sub
#End Region
End Class