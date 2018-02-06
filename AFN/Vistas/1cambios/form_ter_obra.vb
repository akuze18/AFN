Public Class form_ter_obra
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private base As New base_AFN

    Public continuar As Boolean
    Private Sub form_ter_obra_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If Not continuar Then form_welcome.Show()
    End Sub
    Private Sub form_ter_obra_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'agrego columnas al datagridview de las entradas con saldo
        Dim TBSalida As New DataTable

        With Tsaldos
            .DataSource = base.cabecera_saldo_obra_a_gasto
            .RowHeadersWidth = 25
            .Columns(0).Width = 50 'Deja de estar oculta
            .Columns(1).Width = 450
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
            .DataSource = base.cabecera_salida_obra_a_af
            .RowHeadersWidth = 25

            .Columns(0).Width = 50 'Deja de estar oculta
            .Columns(1).Width = 450
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            .Columns(2).Width = 90
            .Columns(2).DefaultCellStyle.Format = "N0"

            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeColumns = False
            .AllowUserToOrderColumns = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
        End With

        Ecod.Enabled = False
        Edesc.Enabled = False
        EmontoMax.Enabled = False

        LvalorAF.Enabled = False
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
                    ocupado = ocupado + fila_salida.Cells(2).Value
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
        Edesc.Text = String.Empty
        EmontoMax.Text = String.Empty
        EmontoSel.Text = String.Empty
    End Sub

    Private Sub Tsaldos_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Tsaldos.CellClick
        If e.RowIndex = -1 Then
            limpiar_saldo()
        End If
        For Each fila As DataGridViewRow In Tsaldos.SelectedRows

            Dim numero = fila.Cells(4).Value
            Dim largo = numero.ToString.Length.ToString


            Ecod.Text = fila.Cells(0).Value
            Edesc.Text = fila.Cells(1).Value
            EmontoMax.Text = Format(Val(numero), "#,##0")
            EmontoSel.Text = fila.Cells(4).Value
            'MaskedTextBox1.Text = Format(Val(numero), MaskedTextBox1.Mask) '(numero).ToString("D" + largo)
            'MaskedTextBox1.Text = (numero).ToString("D" + largo)
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
            'ingreso valores en segundo flexgrid
            'h = Tsaldos.Row
            'i = salidaAF.Rows
            Dim resultado_salida As DataTable
            resultado_salida = salidaAF.DataSource
            Dim newfila As DataRow = resultado_salida.NewRow
            Dim ocupado As Integer
            newfila(0) = Ecod.Text
            newfila(1) = Edesc.Text
            newfila(2) = Val(Replace(EmontoSel.Text, ",", ""))
            resultado_salida.Rows.Add(newfila)
            EmontoMax.Text = Format(Val(Replace(EmontoMax.Text, ",", "")) - Val(Replace(EmontoSel.Text, ",", "")), "#,##0")
            EmontoSel.Text = 0
            Call cargar_saldos()
            ocupado = 0
            For Each fila As DataGridViewRow In salidaAF.Rows
                ocupado = ocupado + fila.Cells(2).Value
            Next
            LvalorAF.Text = Format(ocupado, "#,##0")
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
            Dim ocupado As Integer
            salidaAF.Rows.Remove(dgvr)
            cargar_saldos()
            'busco el codigo dentro la salida para restar los saldos
            ocupado = 0
            For Each fila As DataGridViewRow In salidaAF.Rows
                ocupado = ocupado + fila.Cells(2).Value
            Next
            LvalorAF.Text = Format(ocupado, "#,##0")
            salidaAF.ClearSelection()
        End If
    End Sub

    Private Sub btn_guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_guardar.Click
        'validamos campos
        If Tcantidad.Text = String.Empty Then
            MsgBox("Ingrese un monto para cantidad de articulos", vbExclamation, "NH FOODS CHILE")
            Tcantidad.Focus()
            Exit Sub
        Else
            If Not IsNumeric(Tcantidad.Text) Then
                MsgBox("Valor ingresado no corresponde a un número", vbExclamation, "NH FOODS CHILE")
                Tcantidad.Focus()
                Exit Sub
            End If
        End If
        If LvalorAF.Text = 0 Or LvalorAF.Text = String.Empty Then
            MsgBox("Seleccione entradas para formar el valor del Activo Fijo", vbExclamation, "NH FOODS CHILE")
            Tsaldos.Focus()
            Exit Sub
        End If
        'fin validacion
        Dim valor_total, cantidad, valor_uni, diferencia As Integer
        valor_total = Val(Format(LvalorAF.Text, "General Number"))
        cantidad = Val(Format(Tcantidad.Text, "General Number"))
        valor_uni = Int(valor_total / cantidad)
        diferencia = valor_total - (valor_uni * cantidad)
        form_ingreso.Show()
        Me.Hide()
        form_ingreso.fuente.Text = "OBC"
        form_ingreso.residuo.Text = Format(diferencia, "0")
        form_ingreso.Tcantidad.Text = Format(cantidad, "#,##0")
        ActivarF(form_ingreso.Tcantidad, False)
        form_ingreso.Tprecio_compra.Text = Format(valor_uni, "#,##0")
        ActivarF(form_ingreso.Tprecio_compra, False)
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