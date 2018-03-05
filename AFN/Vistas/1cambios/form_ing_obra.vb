Public Class form_ing_obra
    Dim color_btn As Color
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private base As New base_AFN

    Private Sub form_ing_obra_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub
    Private Sub form_ing_obra_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim tmp_fecha As Date
        tmp_fecha = DateAdd(DateInterval.Month, -6, Now)
        With Tfecha_compra
            .Value = Now
            .CustomFormat = "dd-MM-yyyy"
            .MaxDate = Now
            .MinDate = DateSerial(Year(tmp_fecha), Month(tmp_fecha), 1)
        End With
        With Tfecha_conta
            .Value = Now
            .CustomFormat = "dd-MM-yyyy"
            .MaxDate = Now
            .MinDate = DateSerial(Year(tmp_fecha), Month(tmp_fecha), 1)
        End With
        'zonas
        With cboZona
            .DataSource = base.ZONAS_GL
            .ValueMember = .DataSource.Columns(0).ColumnName
            .DisplayMember = .DataSource.Columns(1).ColumnName
            .SelectedIndex = -1
        End With
        'proveedor
        With cboProveedor
            .DataSource = base.PROVEEDOR_GP(base_AFN.tipo_proveedor.SoloNuevos)
            .ValueMember = .DataSource.Columns(0).ColumnName
            .DisplayMember = .DataSource.Columns(1).ColumnName
            .SelectedIndex = -1
        End With
        color_btn = btn_guardar.BackColor
    End Sub

    Private Sub btn_Bprov_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Bprov.Click
        bus_prov.Show()
        bus_prov.actualizar_origen("obc", Me, Me.cboProveedor)
    End Sub

    Private Sub Tcredito_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tcredito.GotFocus
        Dim procesar As String
        procesar = Tcredito.Text
        procesar = Strings.Replace(procesar, ",", "")
        Tcredito.Text = Format(Val(procesar), "#")
    End Sub
    Private Sub Tcredito_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tcredito.LostFocus
        If Tcredito.Text <> String.Empty Then
            Dim procesar As String
            procesar = Tcredito.Text
            procesar = Strings.Replace(procesar, ",", "")
            If Not IsNumeric(Val(procesar)) Then
                MessageBox.Show("Solo puede ingresar números en la cantidad", "AVISO")
                Tcredito.Focus()
                Tcredito.Text = String.Empty
            Else
                Tcredito.Text = Format(Val(procesar), "#,##0")
            End If
        End If
    End Sub

    Private Sub btn_limpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_limpiar.Click
        cboZona.SelectedIndex = -1
        Tcredito.Text = String.Empty
        Tdoc.Text = String.Empty
        cboProveedor.SelectedIndex = -1
        Tdescrip.Text = String.Empty
        btn_guardar.BackColor = color_btn
    End Sub

    Private Sub btn_guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_guardar.Click
        'validar información ingresada
        If Tfecha_compra.Value.ToString = "" Then
            MessageBox.Show("Debe indicar la fecha de ingreso por Obra en Construcción", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tfecha_compra.Focus()
            Exit Sub
        End If
        If Tfecha_conta.Value.ToString = "" Then
            MessageBox.Show("Debe indicar la fecha de contabilizacion de Obra en Construcción", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tfecha_conta.Focus()
            Exit Sub
        End If
        If cboZona.SelectedIndex = -1 Then
            MessageBox.Show("Debe indicar la zona de la Obra en Construcción", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboZona.Focus()
            Exit Sub
        End If
        If Tcredito.Text = "" Then
            MessageBox.Show("Debe indicar el monto de Obra en Construcción", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tcredito.Focus()
            Exit Sub
        End If
        If Tdoc.Text = "" Then
            Dim eleccion As DialogResult
            eleccion = MessageBox.Show("Desea continuar sin indicar el Nº de documento de Obra en Construcción", "NH FOODS CHILE", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If eleccion <> Windows.Forms.DialogResult.Yes Then   'no marco SI
                Tdoc.Focus()
                Exit Sub
            End If
        End If
        If Tdescrip.Text = "" Then
            MessageBox.Show("Debe indicar la descripción o referencia de la Obra en Construcción", "NH FOODS CHILE", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            Tdescrip.Focus()
            Exit Sub
        End If
        'fin validación
        'inicio preparar datos para ingresar
        Dim documento, proveedor, zona, descp, credit_amo As String
        Dim fechaC, fechaGL As DateTime
        fechaC = Tfecha_compra.Value
        fechaGL = Tfecha_conta.Value
        zona = cboZona.SelectedValue
        descp = Tdescrip.Text
        credit_amo = Format(Tcredito.Text, "General Number")
        If Tdoc.Text = "" Then
            documento = "SIN_DOCUMENTO"
        Else
            documento = Tdoc.Text
        End If
        If cboProveedor.SelectedIndex = -1 Then
            proveedor = "SIN_PROVEED"
        Else
            proveedor = cboProveedor.SelectedValue
        End If
        'Valido que no se haya ingresado duplicado comprobando el documento, proveedor y zona
        Dim contar As Integer
        contar = base.busca_entrada_obc(documento, proveedor, zona)
        If contar > 0 Then
            Mensaje_Err("La entrada indicada ya existe en el sistema")
            Exit Sub
        End If
        'ingreso en obra_cons
        Dim mRS As DataTable
        mRS = base.INGRESO_OBC(fechaC, zona, proveedor, documento, descp, credit_amo, fechaGL)
        'fin ingreso en obra_cons
        MessageBox.Show("Registro de Obra en Construccion ingresado correctamente", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btn_guardar.BackColor = Color.OrangeRed
    End Sub

    'si produce un cambio en cualquier control, el boton vuelve a su color por defecto
    Private Sub cboZona_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboZona.SelectedIndexChanged
        btn_guardar.BackColor = color_btn
    End Sub
    Private Sub Tcredito_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tcredito.TextChanged
        btn_guardar.BackColor = color_btn
    End Sub
    Private Sub Tdoc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tdoc.TextChanged
        btn_guardar.BackColor = color_btn
    End Sub
    Private Sub cboProveedor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboProveedor.SelectedIndexChanged
        btn_guardar.BackColor = color_btn
    End Sub
    Private Sub Tdescrip_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tdescrip.TextChanged
        btn_guardar.BackColor = color_btn
    End Sub
    Private Sub campo_fecha_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tfecha_compra.ValueChanged, Tfecha_conta.ValueChanged
        Dim campo_fecha As DateTimePicker
        campo_fecha = CType(sender, DateTimePicker)
        btn_guardar.BackColor = color_btn
        If campo_fecha.Value.Month = 1 And campo_fecha.Value.Day = 1 Then
            campo_fecha.Value = DateSerial(campo_fecha.Value.Year, 1, 2)
        End If
    End Sub

End Class