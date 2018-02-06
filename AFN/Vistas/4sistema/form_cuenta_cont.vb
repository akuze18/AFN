Public Class form_cuenta_cont
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private base As New base_AFN

    Private Sub form_cuenta_cont_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub

    Private Sub form_cuenta_cont_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim Ttcampo, Tclase, Tcuentas As DataTable
        'Cuentas Contables        
        Tcuentas = base.CUENTAS

        'Detalle de la clasificacion
        With dgClasificacion
            .RowHeadersVisible = False
            '.RowHeadersWidth = 25
            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'COLUMNAS DE DATAGRID
            Dim col1, col2, col3, col4, col5, col9 As New DataGridViewTextBoxColumn
            Dim col10 As New DataGridViewComboBoxColumn
            With col1
                .Name = "cod_campo"
                .Visible = False
            End With
            With col2
                .Name = "Tipo Cuenta"
                .Width = 165
                .ReadOnly = True
            End With
            With col3
                .Name = "cod_clase"
                .Visible = False
            End With
            With col4
                .Name = "Clase"
                .Width = 210
                .ReadOnly = True
            End With
            With col9
                .Name = "NUM_CUENTA"
                .Visible = False
            End With
            With col10
                .Name = "Cuenta Contable"
                .DataSource = Tcuentas
                .DisplayMember = Tcuentas.Columns(2).ColumnName
                .ValueMember = Tcuentas.Columns(1).ColumnName
                .Width = 300
            End With
            With col5
                .Name = "ID_CLASIFICA"
                .Visible = False
            End With
            .Columns.AddRange(New DataGridViewColumn() {col1, col2, col3, col4, col9, col10, col5})
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeColumns = True
            .AllowUserToResizeRows = False
            .AllowUserToOrderColumns = False
            '.EditMode = DataGridViewEditMode.
        End With
        
        'Tipo de Cuenta (campo)
        With cbTCuenta
            Ttcampo = base.TIPO_CAMPO
            .ValueMember = Ttcampo.Columns(0).ColumnName
            .DisplayMember = Ttcampo.Columns(1).ColumnName
            .DataSource = Ttcampo
            .SelectedIndex = -1
        End With

        'Clase
        With cbClase
            Tclase = base.CLASE("", True)
            .ValueMember = Tclase.Columns(0).ColumnName
            .DisplayMember = Tclase.Columns(1).ColumnName
            .DataSource = Tclase
            .SelectedIndex = -1
        End With

        cargar_clasificacion()
    End Sub

    Private Sub cargar_clasificacion()
        Dim criterio As String
        Dim contar As Integer
        Dim Tdetalle As DataTable
        'obtengo toda la data necesaria
        Tdetalle = base.CLASIFICA_CUENTAS

        'agrego los criterios segun las especificaciones
        criterio = ""
        If cbTCuenta.SelectedIndex <> -1 Then
            criterio = criterio + "COD_CAMPO=" + cbTCuenta.SelectedValue.ToString
        End If
        If cbClase.SelectedIndex <> -1 Then
            If criterio <> "" Then
                criterio = criterio + " and "
            End If
            criterio = criterio + "COD_CLASE='" + cbClase.SelectedValue + "'"
        End If

        If ckClasif.Checked Then
            If criterio <> "" Then
                criterio = criterio + " and "
            End If
            criterio = criterio + "NUM_CUENTA=''"
        End If

        dgClasificacion.Rows.Clear()
        contar = 0
        For Each fila As DataRow In Tdetalle.Select(criterio)
            Dim indice_fila As Integer
            'agrego filas
            dgClasificacion.Rows.Add()
            indice_fila = dgClasificacion.Rows.Count - 1
            'agrego los datos
            dgClasificacion.Item(0, indice_fila).Value = fila("COD_CAMPO")
            dgClasificacion.Item(1, indice_fila).Value = fila("TXT_CAMPO")
            dgClasificacion.Item(2, indice_fila).Value = fila("COD_CLASE")
            dgClasificacion.Item(3, indice_fila).Value = fila("TXT_CLASE")
            dgClasificacion.Item(4, indice_fila).Value = fila("NUM_CUENTA")
            If fila("NUM_CUENTA") = String.Empty Then
                contar = contar + 1
            Else
                dgClasificacion.Item(5, indice_fila).Value = fila("NUM_CUENTA")
            End If
            dgClasificacion.Item(6, indice_fila).Value = fila("ID_CLASIFICA")
        Next
        Select Case contar
            Case 0
                ckClasif.Text = "Todas las cuentas estan clasificadas"
            Case 1
                ckClasif.Text = "Hay 1 cuenta sin clasificar"
            Case Else
                ckClasif.Text = "Hay " + contar.ToString + " cuentas sin clasificar"
        End Select
    End Sub

    Private Sub cbClase_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles cbClase.KeyUp, cbTCuenta.KeyUp
        Dim elemento As ComboBox = CType(sender, ComboBox)
        If e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
            elemento.SelectedIndex = -1
        End If
    End Sub

    Private Sub combos_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbTCuenta.SelectedIndexChanged, _
        cbClase.SelectedIndexChanged, ckClasif.CheckedChanged

        cargar_clasificacion()
        
    End Sub

    Private Sub btn_guardar_Click(sender As System.Object, e As System.EventArgs) Handles btn_guardar.Click

        Dim contarIns, contarUpd As Integer
        Dim msg_final As String
        contarIns = 0
        contarUpd = 0
        For Each fila As DataGridViewRow In dgClasificacion.Rows
            'Determino si la celda "Cuenta Contable"(5) se modifico
            Dim cuenta_inicial, cuenta_actual As String
            cuenta_inicial = fila.Cells(4).Value
            cuenta_actual = fila.Cells(5).Value
            If cuenta_inicial <> cuenta_actual Then
                'Determino es una Modificacion o una Nueva Clasificacion
                If fila.Cells(6).Value = 0 Then
                    'Corresponde a Nueva Clasificacion
                    base.INGRESO_CLASIFICACION_CUENTA(fila.Cells(0).Value, fila.Cells(2).Value, fila.Cells(5).Value)
                    contarIns = contarIns + 1
                Else
                    'Corresponde a Modificación
                    base.MODIFICA_CLASIFICACION_CUENTA(fila.Cells(6).Value, fila.Cells(0).Value, fila.Cells(2).Value, fila.Cells(5).Value)
                    contarUpd = contarUpd + 1
                End If
            End If
        Next
        msg_final = ""
        If contarIns > 0 Or contarUpd > 0 Then
            msg_final = "Proceso finalizado " + Chr(13)
            If contarIns > 0 Then
                msg_final = msg_final + Chr(7) + "Clasificaciones Nuevas: " + contarIns.ToString + Chr(13)
            End If
            If contarUpd > 0 Then
                msg_final = msg_final + Chr(7) + "Clasificaciones Modificadas: " + contarUpd.ToString + Chr(13)
            End If
        Else
            msg_final = "Proceso finalizado, no se realizo ningun cambio"
        End If
        MessageBox.Show(msg_final, "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
        cargar_clasificacion()
    End Sub
End Class