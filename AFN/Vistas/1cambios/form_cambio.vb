Public Class form_cambio
    Dim ActualDetalleLote, TotalDetalleLote As DataTable
    ''' <summary>
    ''' Instancia del forumario que contiene toda la logica del proceso
    ''' </summary>
    ''' <remarks></remarks>
    Private base As New base_AFN

#Region "Del Formulario"
    Private Sub form_cambio_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub

    Private Sub form_cambio_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim recordset As DataTable
        recordset = base.lista_cambio
        TotalDetalleLote = base.lista_det_total_art
        lista_cambiar.DataSource = recordset
        lista_cambiar.ColumnHeadersHeight = lista_cambiar.ColumnHeadersHeight * 2
        lista_cambiar.RowHeadersVisible = False
        'lista_cambiar.AllowUserToResizeColumns = False
        For Each columna As DataGridViewColumn In lista_cambiar.Columns
            columna.SortMode = DataGridViewColumnSortMode.NotSortable
            'columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        Next
        lista_cambiar.Columns(0).Visible = False
        lista_cambiar.Columns(1).Width = 60
        lista_cambiar.Columns(2).Width = 90
        lista_cambiar.Columns(3).Width = 67
        lista_cambiar.Columns(4).Width = 50
        lista_cambiar.Columns(5).Width = 60
        lista_cambiar.Columns(6).Visible = False
        lista_cambiar.Columns(7).Width = 300
        lista_cambiar.Columns(8).Visible = False

        Dcambio.Value = Now
        Dcambio.MaxDate = Now

        iniciar_formulario()

    End Sub

    Private Sub form_cambio_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        lista_cambiar.Size = New Size(Me.Size.Width - 16 - 40, Me.Size.Height - 38 - 273)
        btn_fin.Location = New Point(Math.Round(lista_cambiar.Size.Width / 2, 0) - 8, lista_cambiar.Location.Y + lista_cambiar.Size.Height + 6)
    End Sub

    Private Sub iniciar_formulario()
        'bloqueo
        cboCant.Items.Clear()
        cod_art.Text = String.Empty
        Dcambio.Enabled = False
        Tarticulo.Enabled = False
        Tarticulo.Text = String.Empty
        Tarticulo.BackColor = Color.LightGray
        ActivarF(TzonaAct, False)
        TzonaAct.Text = String.Empty
        ActivarF(TsubZact, False)
        TsubZact.Text = String.Empty
        Tvalor.Enabled = False
        Tvalor.Text = String.Empty
        Tvalor.BackColor = Color.LightGray
        btn_consulta.BackColor = Color.Red
    End Sub
#End Region

#Region "De los Controles"
    Private Sub cargar_formulario(ByVal filaRef As Integer)
        Me.Enabled = False
        iniciar_formulario()
        Dim recordset, TBzonas As DataTable
        Dim cantid, cont_subz As Integer
        Dim zona As String
        recordset = base.BASE_LOCAL(Now, filaRef)
        zona = recordset.Rows(0).Item("zona")
        TBzonas = base.ZONAS_GL()
        'Determino si es necesario quitar la zona del combo
        Dim remove_fila As DataRow = Nothing
        For Each fila As DataRow In TBzonas.Rows
            If fila.Item("cod") = zona Then
                TzonaAct.Text = fila.Item("es")
                cont_subz = base.contar_subzonas(zona)
                If cont_subz = 0 Then
                    'zona no tiene subzonas, por lo que no puede ser cambiada dentro de la misma zona
                    remove_fila = fila
                End If
                If cont_subz = 1 Then
                    If recordset.Rows(0).Item("cod_subzona") <> 0 Then
                        'solo tiene una subzona y no puede cambiarse a si misma
                        remove_fila = fila
                    Else
                        'si bien tiene solo 1 subzona, actualmente no tiene indicada una (valor 0) por lo que
                        'se puede hacer el cambio, desde "sin subzonas" a la subzona disponible
                    End If
                End If
            End If
        Next
        If Not IsNothing(remove_fila) Then
            TBzonas.Rows.Remove(remove_fila)
        End If

        With cboZona
            .DisplayMember = TBzonas.Columns(1).ColumnName
            .ValueMember = TBzonas.Columns(0).ColumnName
            .DataSource = TBzonas
            .SelectedIndex = -1
        End With

        cod_art.Text = recordset.Rows(0).Item("cod_articulo")
        Gparte.Text = recordset.Rows(0).Item("parte")

        cantid = recordset.Rows(0).Item("cantidad")
        For i = 1 To cantid
            cboCant.Items.Add(i)
        Next
        cboCant.SelectedIndex = cboCant.Items.Count - 1

        zona_art.Text = recordset.Rows(0).Item("zona")
        'TzonaAct.Text = recordset.Rows(0).Item("")

        subzona_art.Text = recordset.Rows(0).Item("cod_subzona")
        TsubZact.Text = recordset.Rows(0).Item("txt_subzona")

        Dcambio.Enabled = True
        Dcambio.MinDate = DateSerial(Year(DateAdd(DateInterval.Month, -1, Now)), Month(DateAdd(DateInterval.Month, -1, Now)), 1)
        rowindx.Text = filaRef

        Tarticulo.Text = recordset.Rows(0).Item("dsc_breve")
        Tvalor.Text = Format(recordset.Rows(0).Item("val_libro"), "General Number")
        btn_consulta.BackColor = System.Drawing.SystemColors.Window
        Me.Enabled = True
    End Sub

    Private Sub cboZona_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboZona.SelectedIndexChanged
        If cboZona.SelectedIndex <> -1 Then
            Dim colchon As DataTable
            colchon = base.SUBZONAS_ACT(cboZona.SelectedValue)
            cboSubzona.DisplayMember = colchon.Columns(1).ColumnName
            cboSubzona.ValueMember = colchon.Columns(0).ColumnName
            cboSubzona.DataSource = colchon
            cboSubzona.SelectedIndex = -1
        End If
    End Sub
    Private Sub cboCant_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboCant.SelectedIndexChanged
        If cboCant.SelectedIndex <> -1 Then
            Dim cantid As Integer
            cantid = cboCant.SelectedItem
            ActualDetalleLote = base.lista_det_articulo(cod_art.Text, Gparte.Text, cantid)
        End If

    End Sub

    Private Sub btn_consulta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_consulta.Click
        Dim auxiliar As New busqueda_articulo()
        Dim resultado As DialogResult
        resultado = auxiliar.ShowDialog()
        If resultado = Windows.Forms.DialogResult.OK Then
            cargar_formulario(auxiliar.fila)
        End If
        auxiliar.Dispose()
    End Sub
    Private Sub btn_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_add.Click
        If rowindx.Text <> "" Then
            Dim pasa As Boolean
            'valido combos
            If cboZona.SelectedIndex = -1 Then
                MessageBox.Show("Debe indicar la zona de destino del Activo Fijo", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cboZona.Focus()
                Exit Sub
            End If
            If cboSubzona.SelectedIndex = -1 And cboSubzona.Items.Count > 0 Then
                MessageBox.Show("Debe indicar la subzona de destino del Activo Fijo", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cboSubzona.Focus()
                Exit Sub
            End If
            'busco que no este ingresado
            pasa = True
            For Each fila As DataGridViewRow In lista_cambiar.Rows
                If fila.Cells(6).Value = rowindx.Text Then
                    pasa = False
                End If
            Next
            If pasa Then
                Dim recordset As DataTable = lista_cambiar.DataSource
                Dim newfila As DataRow = recordset.NewRow
                Dim Vsubzona As Integer
                If cboSubzona.SelectedIndex = -1 Then
                    Vsubzona = 0
                Else
                    Vsubzona = cboSubzona.SelectedValue
                End If
                newfila(1) = cod_art.Text
                newfila(2) = Format(Dcambio.Value, "dd-MM-yyyy")
                newfila(3) = cboCant.Text
                newfila(4) = cboZona.SelectedValue
                newfila(5) = Vsubzona
                newfila(6) = rowindx.Text
                newfila(7) = Tarticulo.Text
                newfila(8) = Gparte.Text
                recordset.Rows.Add(newfila)

                detalle_add(ActualDetalleLote, cod_art.Text, Gparte.Text, rowindx.Text)

                lista_cambiar.ClearSelection()
            Else
                MessageBox.Show("Articulo indicado ya ha sido agregado para ser traspasado", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
    End Sub
    Private Sub btn_remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_remove.Click
        Dim limpiar As Boolean
        limpiar = False
        If lista_cambiar.SelectedRows.Count > 0 Then
            For Each fila_sel As DataGridViewRow In lista_cambiar.SelectedRows
                Dim elige As DialogResult
                elige = MessageBox.Show("Está seguro que desea eliminar el lote artículo " + fila_sel.Cells(1).Value.ToString + " de la lista de traspasos?", "NH FOODS CHILE", MessageBoxButtons.YesNo)
                If elige = Windows.Forms.DialogResult.Yes Then
                    detalle_less(fila_sel.Cells(6).Value)
                    lista_cambiar.Rows.Remove(fila_sel)
                    limpiar = True
                End If
            Next
        Else
            MessageBox.Show("No ha indicado ningun registro para quitar del listado de traspaso", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        If limpiar Then
            lista_cambiar.ClearSelection()
        End If
    End Sub
    Private Sub btn_fin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_fin.Click
        If lista_cambiar.Rows.Count > 0 Then
            Dim elige As DialogResult
            Dim newzona, newsubzona As String
            Dim newfecha As DateTime
            Dim codigo_articulo, parte_articulo, newcantidad, codigo_grupo As Integer
            elige = MessageBox.Show("Está seguro que desea procesar los artículos de la lista de traspasos?", "NH FOODS CHILE", MessageBoxButtons.YesNo)
            If elige = Windows.Forms.DialogResult.Yes Then
                Dim recordset As New DataTable
                For Each fila As DataGridViewRow In lista_cambiar.Rows
                    If fila.DefaultCellStyle.BackColor <> base.AFNok Then
                        'las celdas que estuvieran ok no se procesan
                        fila.DefaultCellStyle.BackColor = base.AFNprocess
                        lista_cambiar.Refresh()
                        codigo_articulo = fila.Cells(1).Value
                        parte_articulo = fila.Cells(8).Value
                        newfecha = fila.Cells(2).Value
                        newcantidad = fila.Cells(3).Value
                        newzona = fila.Cells(4).Value
                        newsubzona = fila.Cells(5).Value
                        codigo_grupo = fila.Cells(6).Value
                        recordset = base.CAMBIO_ZONA(codigo_articulo, parte_articulo, newfecha, newzona, newsubzona, newcantidad, form_welcome.GetUsuario, TotalDetalleLote, codigo_grupo)
                        If recordset.Rows(0).Item("cod_sit") < 0 Then
                            'se produjo un error al momento de generar al castigo en la base de datos
                            fila.DefaultCellStyle.BackColor = base.AFNfail
                            lista_cambiar.Refresh()
                            MessageBox.Show(recordset.Rows(0).Item("dsc_sit").ToString + vbCrLf + "Fila: " + fila.Index.ToString, "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        Else
                            fila.DefaultCellStyle.BackColor = base.AFNok
                            lista_cambiar.Refresh()
                        End If
                    End If
                    Application.DoEvents()
                Next
                recordset = Nothing
                MessageBox.Show("Traspaso se ha realizado exitosamente", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Else
            MessageBox.Show("No ha agregado ningun articulo al listado de traspaso", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        Dim aux As New form_detalle_articulo(cod_art.Text, Gparte.Text, form_detalle_articulo.form_accion.cambio, ActualDetalleLote)
        resultado = aux.ShowDialog()
        If resultado = Windows.Forms.DialogResult.OK Then
            ActualDetalleLote = aux.detalle
        End If
        aux = Nothing
    End Sub
#End Region

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