Public Class form_gasto_obra
    Public continuar As Boolean
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private base As New base_AFN

    Private act_batch As Integer

    Private Sub form_gasto_obra_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If Not continuar Then form_welcome.Show()
    End Sub
    Private Sub form_gasto_obra_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        With Tsaldos
            .DataSource = base.cabecera_saldo_obra_a_gasto
            .RowHeadersWidth = 25
            .Columns(0).Width = 50 'Deja de estar oculta
            .Columns(1).Width = 300
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .Columns(2).Width = 75
            .Columns(3).Width = 50
            .Columns(4).Width = 90
            .Columns(4).DefaultCellStyle.Format = "N0"
            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeColumns = False
            .AllowUserToOrderColumns = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
        End With
        cargar_saldos()
        continuar = False
        With salidaAF
            .DataSource = base.cabecera_salida_obra_a_gasto
            .RowHeadersWidth = 25

            .Columns(0).Width = 50 'Deja de estar oculta
            .Columns(1).Width = 300
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            .Columns(2).Width = 75
            .Columns(3).Width = 50
            .Columns(4).Width = 90
            .Columns(4).DefaultCellStyle.Format = "N0"

            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeColumns = False
            .AllowUserToOrderColumns = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
        End With

        Ecod.Enabled = False
        Ezona.Visible = False
        Edesc.Enabled = False
        EmontoMax.Enabled = False

        Efecha.Value = Now
        Efecha.MaxDate = Now
    End Sub

    Private Sub cargar_saldos()
        Dim colchon, datos As New DataTable
        Dim ocupado As Integer
        colchon = base.saldos_obc(Now, "CLP")
        datos = Tsaldos.DataSource
        datos.Rows.Clear()

        For Each fila As DataRow In colchon.Rows
            'agrego filas
            Dim newdato As DataRow = datos.NewRow
            newdato(0) = fila("Codigo Movimiento")
            newdato(1) = Trim(fila("Descripción"))
            newdato(2) = fila("Fecha Documento")
            newdato(3) = fila("Zona")
            'busco el codigo dentro la salida para restar los saldos
            ocupado = 0
            For Each fila_salida As DataGridViewRow In salidaAF.Rows
                If fila_salida.Cells(0).Value = fila("Codigo Movimiento") Then
                    ocupado = ocupado + fila_salida.Cells(4).Value
                End If
            Next
            newdato(4) = fila("Saldo Final") - ocupado
            datos.Rows.Add(newdato)
        Next
        colchon = Nothing
        limpiar_saldo()
    End Sub
    Private Sub limpiar_saldo()
        Tsaldos.ClearSelection()
        Ecod.Text = String.Empty
        Ezona.Text = String.Empty
        Edesc.Text = String.Empty
        EmontoMax.Text = String.Empty
        EmontoSel.Text = String.Empty
    End Sub

    Private Sub Tsaldos_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Tsaldos.CellClick
        If e.RowIndex = -1 Then
            limpiar_saldo()
        End If
        For Each fila As DataGridViewRow In Tsaldos.SelectedRows
            Ecod.Text = fila.Cells(0).Value
            Edesc.Text = fila.Cells(1).Value
            EmontoMax.Text = Format(fila.Cells(4).Value, "#,##0")
            EmontoSel.Text = Format(fila.Cells(4).Value, "#,##0")
            Ezona.Text = fila.Cells(3).Value
        Next
    End Sub
    Private Sub EmontoSel_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles EmontoSel.GotFocus
        Dim procesar As String
        procesar = EmontoSel.Text
        procesar = Strings.Replace(procesar, ",", "")
        EmontoSel.Text = Format(Val(procesar), "#")
    End Sub
    Private Sub EmontoSel_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles EmontoSel.LostFocus
        If EmontoSel.Text <> String.Empty Then
            Dim procesar, pmaximo As String
            procesar = EmontoSel.Text
            pmaximo = EmontoMax.Text
            procesar = Strings.Replace(procesar, ",", "")
            pmaximo = Strings.Replace(pmaximo, ",", "")
            If Not IsNumeric(Val(procesar)) Then
                MessageBox.Show("Solo puede ingresar números en la cantidad", "AVISO")
                EmontoSel.Focus()
                EmontoSel.Text = String.Empty
            Else
                If Val(pmaximo) < Val(procesar) Then
                    MessageBox.Show("No puede exceder el monto máximo disponible para la entrada", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    EmontoSel.Text = EmontoMax.Text
                Else
                    EmontoSel.Text = Format(Val(procesar), "#,##0")
                End If
            End If
        End If
    End Sub

    Private Sub btn_adjuntar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_adjuntar.Click
        'reviso que haya valores para adjuntar
        Dim ver1, ver2, ver3, ver4 As Boolean
        If Ecod.Text <> String.Empty And IsNumeric(Ecod.Text) Then
            ver1 = True
        Else
            ver1 = False
        End If
        If Edesc.Text <> String.Empty Then
            ver2 = True
        Else
            ver2 = False
        End If
        If EmontoMax.Text <> String.Empty And IsNumeric(EmontoMax.Text) Then
            ver3 = True
        Else
            ver3 = False
        End If
        If EmontoSel.Text <> String.Empty And IsNumeric(EmontoSel.Text) Then
            If EmontoSel.Text <> 0 Then
                ver4 = True
            Else
                ver4 = False
            End If
        Else
            ver4 = False
        End If
        If ver1 And ver2 And ver3 And ver4 Then
            'ingreso valores en segundo datagrid
            Dim resultado_salida As DataTable
            resultado_salida = salidaAF.DataSource
            Dim newfila As DataRow = resultado_salida.NewRow
            'Dim ocupado As Integer
            newfila(0) = Ecod.Text
            newfila(1) = Edesc.Text
            newfila(2) = Efecha.Value
            newfila(3) = Ezona.Text
            newfila(4) = Val(Replace(EmontoSel.Text, ",", ""))
            resultado_salida.Rows.Add(newfila)
            EmontoMax.Text = Format(Val(Replace(EmontoMax.Text, ",", "")) - Val(Replace(EmontoSel.Text, ",", "")), "#,##0")
            EmontoSel.Text = 0
            Call cargar_saldos()
            salidaAF.ClearSelection()
        Else
            'hacer nada
        End If
    End Sub
    Private Sub btn_quitar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_quitar.Click
        Dim dgvr As DataGridViewRow
        dgvr = Nothing
        For Each fila As DataGridViewRow In salidaAF.SelectedRows
            dgvr = fila
        Next
        If Not IsNothing(dgvr) Then
            'Dim ocupado As Integer
            salidaAF.Rows.Remove(dgvr)
            cargar_saldos()
            salidaAF.ClearSelection()
        End If
    End Sub


    Private Sub btn_guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_guardar.Click
        'validamos campos
        If salidaAF.Rows.Count = 0 Then
            MessageBox.Show("Debe indicar los valores de salida a gastos", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tsaldos.Focus()
            Exit Sub
        End If
        'fin validacion
        Dim mRS As DataRow
        Dim monto As String
        Dim codEnt As Integer
        Dim fsalida As DateTime
        For Each fila As DataGridViewRow In salidaAF.Rows
            monto = fila.Cells(4).Value
            fsalida = fila.Cells(2).Value
            codEnt = fila.Cells(0).Value
            mRS = base.EGRESO_GST(codEnt, fsalida, monto)
        Next
        If act_batch <> 0 Then
            base.DESESACTIVA_BATCH(act_batch)
            act_batch = 0
        End If
        cargar_saldos()
        salidaAF.DataSource.Rows.Clear()
    End Sub
   
    Private Sub btn_in_temp_Click(sender As System.Object, e As System.EventArgs) Handles btn_in_temp.Click
        'validamos campos
        If salidaAF.Rows.Count = 0 Then
            MessageBox.Show("Debe indicar los valores de salida a gastos", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tsaldos.Focus()
            Exit Sub
        End If
        'fin validacion
        Dim mRS As DataRow
        Dim monto As String
        Dim codEnt, id_batch As Integer
        Dim fsalida As DateTime
        If act_batch = 0 Then
            id_batch = base.get_last_batch() + 1
        Else
            Dim respuesta As DialogResult
            respuesta = MessageBox.Show("Desea adjuntar los cambios al último batch cargado?", "NH FOODS CHILE", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
            If respuesta = Windows.Forms.DialogResult.Yes Then
                base.LIMPIAR_BATCH(act_batch)
                id_batch = act_batch
            ElseIf respuesta = Windows.Forms.DialogResult.No Then
                id_batch = base.get_last_batch() + 1
            Else
                Exit Sub
            End If
        End If

        For Each fila As DataGridViewRow In salidaAF.Rows
            monto = fila.Cells(4).Value
            fsalida = fila.Cells(2).Value
            codEnt = fila.Cells(0).Value
            mRS = base.EGRESO_GST_TEMP(codEnt, fsalida, monto, id_batch)
        Next
        cargar_saldos()
        act_batch = id_batch
        MessageBox.Show("Se han guardado los registros en borrador con el código: " + act_batch.ToString, "NH FOODS Chile", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub btn_out_temp_Click(sender As System.Object, e As System.EventArgs) Handles btn_out_temp.Click
        'obtenemos batch's disponibles
        Dim disponible As DataTable = base.batch_disponibles
        If disponible.Rows.Count > 0 Then
            Dim ventana As New batch_disponibles(disponible)
            Dim elegir As DialogResult
            elegir = ventana.ShowDialog()
            If elegir = Windows.Forms.DialogResult.Yes Then
                Dim batch As Integer = ventana.resultado
                'quito todas las filas existentes
                If salidaAF.Rows.Count > 0 Then
                    salidaAF.DataSource.Rows.Clear()
                End If
                'ingreso valores en segundo datagrid
                Dim resultado_salida As DataTable = salidaAF.DataSource
                For Each guardado As DataRow In base.detalle_batch(batch).Rows
                    Dim newfila As DataRow = resultado_salida.NewRow
                    'Dim ocupado As Integer
                    newfila(0) = guardado("cod_entrada")
                    newfila(1) = guardado("descrip")
                    newfila(2) = guardado("txfecha")
                    newfila(3) = guardado("zona")
                    newfila(4) = guardado("monto")
                    resultado_salida.Rows.Add(newfila)
                Next
                'salidaAF.DataSource = resultado_salida
                EmontoMax.Text = Format(Val(Replace(EmontoMax.Text, ",", "")) - Val(Replace(EmontoSel.Text, ",", "")), "#,##0")
                EmontoSel.Text = 0
                Call cargar_saldos()
                salidaAF.ClearSelection()
                act_batch = batch
            End If
        Else
            MessageBox.Show("No hay resultados disponibles para cargar")
        End If
    End Sub

    Private Sub btn_clear_temp_Click(sender As System.Object, e As System.EventArgs) Handles btn_clear_temp.Click
        'obtenemos batch's disponibles
        Dim disponible As DataTable = base.batch_disponibles
        If disponible.Rows.Count > 0 Then
            Dim ventana As New batch_disponibles(disponible)
            Dim elegir As DialogResult
            elegir = ventana.ShowDialog()
            If elegir = Windows.Forms.DialogResult.Yes Then
                Dim batch As Integer = ventana.resultado

                If act_batch = batch Then
                    'quito todas las filas existentes si pertenecen al batch que se desea borrar
                    If salidaAF.Rows.Count > 0 Then
                        salidaAF.DataSource.Rows.Clear()
                    End If
                    EmontoMax.Text = Format(Val(Replace(EmontoMax.Text, ",", "")) - Val(Replace(EmontoSel.Text, ",", "")), "#,##0")
                    EmontoSel.Text = 0
                    Call cargar_saldos()
                    salidaAF.ClearSelection()
                    act_batch = 0
                End If

                base.DESESACTIVA_BATCH(batch)
                MessageBox.Show("Se ha desactivado el borrador con el código :" + batch.ToString, "NH FOODS Chile", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show("No hay resultados disponibles para cargar", "NH FOODS Chile", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    
    Private Sub btFindEntrada_Click(sender As System.Object, e As System.EventArgs) Handles btFindEntrada.Click
        Dim find_entrada As String
        find_entrada = InputBox("Ingrese el código que desea buscar", "NH FOODS CHILE")
        Dim cod_entrada As Integer
        Try
            cod_entrada = CType(find_entrada, Integer)
        Catch ex As Exception
            Mensaje_Err("El valor ingresado es valido")
            Exit Sub
        End Try
        Dim encontrar As Boolean = False
        For Each registro As DataGridViewRow In Tsaldos.Rows
            If registro.Cells(0).Value = cod_entrada Then
                encontrar = True
                Tsaldos.FirstDisplayedScrollingRowIndex = registro.Index
                registro.Selected = True
                Tsaldos_CellClick(sender, New DataGridViewCellEventArgs(0, registro.Index))
            End If
        Next
        If encontrar Then
            'Mensaje_Inf("Se ha encontrado un registro")
        Else
            Mensaje_IT("No se encontró ningun registro con el codigo indicado")
        End If
    End Sub
End Class