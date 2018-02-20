<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_ingreso
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_ingreso))
        Me.pasos = New System.Windows.Forms.TabControl()
        Me.paso1 = New System.Windows.Forms.TabPage()
        Me.ckDepre = New System.Windows.Forms.CheckBox()
        Me.residuo = New System.Windows.Forms.TextBox()
        Me.TxtPrecioTotal = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.cboSubzona = New System.Windows.Forms.ComboBox()
        Me.btn_Bprov = New System.Windows.Forms.PictureBox()
        Me.btn_act = New System.Windows.Forms.Button()
        Me.btn_elim = New System.Windows.Forms.Button()
        Me.btn_guardar = New System.Windows.Forms.Button()
        Me.ckIFRS = New System.Windows.Forms.CheckBox()
        Me.Fderecho = New System.Windows.Forms.GroupBox()
        Me.derC2 = New System.Windows.Forms.RadioButton()
        Me.derC1 = New System.Windows.Forms.RadioButton()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Tdoc = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.TvuF = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Tprecio_compra = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Tcantidad = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cbFecha_ing = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Tfecha_compra = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboProveedor = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboCateg = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboClase = New System.Windows.Forms.ComboBox()
        Me.cboSubclase = New System.Windows.Forms.ComboBox()
        Me.cboConsist = New System.Windows.Forms.ComboBox()
        Me.cboZona = New System.Windows.Forms.ComboBox()
        Me.Tdescrip = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.paso2 = New System.Windows.Forms.TabPage()
        Me.btn_IFRS = New System.Windows.Forms.Button()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.DataIFRS = New System.Windows.Forms.DataGridView()
        Me.xt = New System.Windows.Forms.TextBox()
        Me.cboMetod = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Tval_resI = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.TvuI = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.paso3 = New System.Windows.Forms.TabPage()
        Me.ckMostrar = New System.Windows.Forms.CheckBox()
        Me.btn_buscaG = New System.Windows.Forms.Button()
        Me.AtribGrupo = New System.Windows.Forms.DataGridView()
        Me.btn_imprimir = New System.Windows.Forms.Button()
        Me.btn_detallexG = New System.Windows.Forms.Button()
        Me.cbGvalor = New System.Windows.Forms.ComboBox()
        Me.TGvalor = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.cbGatrib = New System.Windows.Forms.ComboBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.btn_lessGA = New System.Windows.Forms.Button()
        Me.btn_addGA = New System.Windows.Forms.Button()
        Me.paso4 = New System.Windows.Forms.TabPage()
        Me.ckMostrarA = New System.Windows.Forms.CheckBox()
        Me.AtribArticulo = New System.Windows.Forms.DataGridView()
        Me.cbAatrib = New System.Windows.Forms.ComboBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.btn_imprimir1 = New System.Windows.Forms.Button()
        Me.btn_detallexA = New System.Windows.Forms.Button()
        Me.cbAvalor = New System.Windows.Forms.ComboBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.cblistaArticulo = New System.Windows.Forms.ComboBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.TAvalor = New System.Windows.Forms.TextBox()
        Me.btn_lessDA = New System.Windows.Forms.Button()
        Me.btn_addDA = New System.Windows.Forms.Button()
        Me.btn_buscaA = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.artic = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TFulldescrip = New System.Windows.Forms.TextBox()
        Me.CkEstado = New System.Windows.Forms.CheckBox()
        Me.fuente = New System.Windows.Forms.TextBox()
        Me.btn_modif = New System.Windows.Forms.Button()
        Me.btn_new = New System.Windows.Forms.Button()
        Me.dialogo = New System.Windows.Forms.OpenFileDialog()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cboGestion = New System.Windows.Forms.ComboBox()
        Me.pasos.SuspendLayout()
        Me.paso1.SuspendLayout()
        CType(Me.btn_Bprov, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Fderecho.SuspendLayout()
        Me.paso2.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.DataIFRS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.paso3.SuspendLayout()
        CType(Me.AtribGrupo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.paso4.SuspendLayout()
        CType(Me.AtribArticulo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pasos
        '
        Me.pasos.Controls.Add(Me.paso1)
        Me.pasos.Controls.Add(Me.paso2)
        Me.pasos.Controls.Add(Me.paso3)
        Me.pasos.Controls.Add(Me.paso4)
        Me.pasos.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.pasos.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pasos.ItemSize = New System.Drawing.Size(200, 18)
        Me.pasos.Location = New System.Drawing.Point(12, 78)
        Me.pasos.Name = "pasos"
        Me.pasos.SelectedIndex = 0
        Me.pasos.Size = New System.Drawing.Size(834, 412)
        Me.pasos.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.pasos.TabIndex = 0
        '
        'paso1
        '
        Me.paso1.Controls.Add(Me.cboGestion)
        Me.paso1.Controls.Add(Me.Label21)
        Me.paso1.Controls.Add(Me.ckDepre)
        Me.paso1.Controls.Add(Me.residuo)
        Me.paso1.Controls.Add(Me.TxtPrecioTotal)
        Me.paso1.Controls.Add(Me.Label34)
        Me.paso1.Controls.Add(Me.cboSubzona)
        Me.paso1.Controls.Add(Me.btn_Bprov)
        Me.paso1.Controls.Add(Me.btn_act)
        Me.paso1.Controls.Add(Me.btn_elim)
        Me.paso1.Controls.Add(Me.btn_guardar)
        Me.paso1.Controls.Add(Me.ckIFRS)
        Me.paso1.Controls.Add(Me.Fderecho)
        Me.paso1.Controls.Add(Me.Label17)
        Me.paso1.Controls.Add(Me.Tdoc)
        Me.paso1.Controls.Add(Me.Label16)
        Me.paso1.Controls.Add(Me.TvuF)
        Me.paso1.Controls.Add(Me.Label15)
        Me.paso1.Controls.Add(Me.Tprecio_compra)
        Me.paso1.Controls.Add(Me.Label14)
        Me.paso1.Controls.Add(Me.Tcantidad)
        Me.paso1.Controls.Add(Me.Label13)
        Me.paso1.Controls.Add(Me.cbFecha_ing)
        Me.paso1.Controls.Add(Me.Label12)
        Me.paso1.Controls.Add(Me.Tfecha_compra)
        Me.paso1.Controls.Add(Me.Label11)
        Me.paso1.Controls.Add(Me.cboProveedor)
        Me.paso1.Controls.Add(Me.Label10)
        Me.paso1.Controls.Add(Me.cboCateg)
        Me.paso1.Controls.Add(Me.Label9)
        Me.paso1.Controls.Add(Me.Label8)
        Me.paso1.Controls.Add(Me.Label7)
        Me.paso1.Controls.Add(Me.cboClase)
        Me.paso1.Controls.Add(Me.cboSubclase)
        Me.paso1.Controls.Add(Me.cboConsist)
        Me.paso1.Controls.Add(Me.cboZona)
        Me.paso1.Controls.Add(Me.Tdescrip)
        Me.paso1.Controls.Add(Me.Label4)
        Me.paso1.Controls.Add(Me.Label3)
        Me.paso1.Controls.Add(Me.Label2)
        Me.paso1.Controls.Add(Me.Label1)
        Me.paso1.Location = New System.Drawing.Point(4, 22)
        Me.paso1.Name = "paso1"
        Me.paso1.Padding = New System.Windows.Forms.Padding(3)
        Me.paso1.Size = New System.Drawing.Size(826, 386)
        Me.paso1.TabIndex = 0
        Me.paso1.Text = "Ficha Básica"
        Me.paso1.UseVisualStyleBackColor = True
        '
        'ckDepre
        '
        Me.ckDepre.AutoSize = True
        Me.ckDepre.Checked = True
        Me.ckDepre.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckDepre.Location = New System.Drawing.Point(222, 321)
        Me.ckDepre.Name = "ckDepre"
        Me.ckDepre.Size = New System.Drawing.Size(72, 17)
        Me.ckDepre.TabIndex = 36
        Me.ckDepre.Text = "Depreciar"
        Me.ckDepre.UseVisualStyleBackColor = True
        '
        'residuo
        '
        Me.residuo.Enabled = False
        Me.residuo.Location = New System.Drawing.Point(766, 238)
        Me.residuo.Name = "residuo"
        Me.residuo.Size = New System.Drawing.Size(27, 20)
        Me.residuo.TabIndex = 24
        Me.residuo.Text = "0"
        Me.residuo.Visible = False
        '
        'TxtPrecioTotal
        '
        Me.TxtPrecioTotal.Enabled = False
        Me.TxtPrecioTotal.Location = New System.Drawing.Point(620, 238)
        Me.TxtPrecioTotal.Name = "TxtPrecioTotal"
        Me.TxtPrecioTotal.Size = New System.Drawing.Size(140, 20)
        Me.TxtPrecioTotal.TabIndex = 23
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Enabled = False
        Me.Label34.Location = New System.Drawing.Point(550, 241)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(64, 13)
        Me.Label34.TabIndex = 22
        Me.Label34.Text = "Precio Total"
        '
        'cboSubzona
        '
        Me.cboSubzona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSubzona.FormattingEnabled = True
        Me.cboSubzona.Location = New System.Drawing.Point(350, 69)
        Me.cboSubzona.Name = "cboSubzona"
        Me.cboSubzona.Size = New System.Drawing.Size(140, 21)
        Me.cboSubzona.TabIndex = 5
        '
        'btn_Bprov
        '
        Me.btn_Bprov.Image = Global.AFN.My.Resources.Resources.find
        Me.btn_Bprov.Location = New System.Drawing.Point(620, 163)
        Me.btn_Bprov.Name = "btn_Bprov"
        Me.btn_Bprov.Size = New System.Drawing.Size(25, 26)
        Me.btn_Bprov.TabIndex = 35
        Me.btn_Bprov.TabStop = False
        '
        'btn_act
        '
        Me.btn_act.Image = Global.AFN.My.Resources.Resources._32_lock
        Me.btn_act.Location = New System.Drawing.Point(629, 321)
        Me.btn_act.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_act.Name = "btn_act"
        Me.btn_act.Size = New System.Drawing.Size(53, 45)
        Me.btn_act.TabIndex = 39
        Me.btn_act.Text = "Activar"
        Me.btn_act.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_act.UseVisualStyleBackColor = True
        '
        'btn_elim
        '
        Me.btn_elim.Image = Global.AFN.My.Resources.Resources._32_remove
        Me.btn_elim.Location = New System.Drawing.Point(537, 321)
        Me.btn_elim.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_elim.Name = "btn_elim"
        Me.btn_elim.Size = New System.Drawing.Size(53, 45)
        Me.btn_elim.TabIndex = 38
        Me.btn_elim.Text = "Eliminar"
        Me.btn_elim.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_elim.UseVisualStyleBackColor = True
        '
        'btn_guardar
        '
        Me.btn_guardar.Image = Global.AFN.My.Resources.Resources._32_next
        Me.btn_guardar.Location = New System.Drawing.Point(437, 321)
        Me.btn_guardar.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_guardar.Name = "btn_guardar"
        Me.btn_guardar.Size = New System.Drawing.Size(53, 45)
        Me.btn_guardar.TabIndex = 37
        Me.btn_guardar.Text = "Guardar"
        Me.btn_guardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_guardar.UseVisualStyleBackColor = True
        '
        'ckIFRS
        '
        Me.ckIFRS.AutoSize = True
        Me.ckIFRS.Location = New System.Drawing.Point(90, 321)
        Me.ckIFRS.Name = "ckIFRS"
        Me.ckIFRS.Size = New System.Drawing.Size(97, 17)
        Me.ckIFRS.TabIndex = 35
        Me.ckIFRS.Text = "Ingresa a IFRS"
        Me.ckIFRS.UseVisualStyleBackColor = True
        '
        'Fderecho
        '
        Me.Fderecho.Controls.Add(Me.derC2)
        Me.Fderecho.Controls.Add(Me.derC1)
        Me.Fderecho.Location = New System.Drawing.Point(622, 268)
        Me.Fderecho.Margin = New System.Windows.Forms.Padding(0)
        Me.Fderecho.Name = "Fderecho"
        Me.Fderecho.Padding = New System.Windows.Forms.Padding(0)
        Me.Fderecho.Size = New System.Drawing.Size(113, 28)
        Me.Fderecho.TabIndex = 34
        Me.Fderecho.TabStop = False
        '
        'derC2
        '
        Me.derC2.AutoSize = True
        Me.derC2.Location = New System.Drawing.Point(65, 8)
        Me.derC2.Name = "derC2"
        Me.derC2.Size = New System.Drawing.Size(39, 17)
        Me.derC2.TabIndex = 1
        Me.derC2.TabStop = True
        Me.derC2.Text = "No"
        Me.derC2.UseVisualStyleBackColor = True
        '
        'derC1
        '
        Me.derC1.AutoSize = True
        Me.derC1.Location = New System.Drawing.Point(7, 8)
        Me.derC1.Name = "derC1"
        Me.derC1.Size = New System.Drawing.Size(34, 17)
        Me.derC1.TabIndex = 0
        Me.derC1.TabStop = True
        Me.derC1.Text = "Si"
        Me.derC1.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(550, 273)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(56, 27)
        Me.Label17.TabIndex = 33
        Me.Label17.Text = "Derecho Credito"
        '
        'Tdoc
        '
        Me.Tdoc.Location = New System.Drawing.Point(350, 273)
        Me.Tdoc.Name = "Tdoc"
        Me.Tdoc.Size = New System.Drawing.Size(140, 20)
        Me.Tdoc.TabIndex = 32
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(270, 273)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(77, 13)
        Me.Label16.TabIndex = 31
        Me.Label16.Text = "Nº Documento"
        '
        'TvuF
        '
        Me.TvuF.Location = New System.Drawing.Point(90, 273)
        Me.TvuF.Name = "TvuF"
        Me.TvuF.Size = New System.Drawing.Size(140, 20)
        Me.TvuF.TabIndex = 30
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(20, 273)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(72, 35)
        Me.Label15.TabIndex = 29
        Me.Label15.Text = "Vida Util (Meses)"
        '
        'Tprecio_compra
        '
        Me.Tprecio_compra.Location = New System.Drawing.Point(350, 238)
        Me.Tprecio_compra.Name = "Tprecio_compra"
        Me.Tprecio_compra.Size = New System.Drawing.Size(140, 20)
        Me.Tprecio_compra.TabIndex = 28
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(270, 238)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(76, 13)
        Me.Label14.TabIndex = 27
        Me.Label14.Text = "Precio Unitario"
        '
        'Tcantidad
        '
        Me.Tcantidad.Location = New System.Drawing.Point(90, 238)
        Me.Tcantidad.Name = "Tcantidad"
        Me.Tcantidad.Size = New System.Drawing.Size(140, 20)
        Me.Tcantidad.TabIndex = 26
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(20, 238)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(49, 13)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "Cantidad"
        '
        'cbFecha_ing
        '
        Me.cbFecha_ing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFecha_ing.FormattingEnabled = True
        Me.cbFecha_ing.Location = New System.Drawing.Point(350, 203)
        Me.cbFecha_ing.Name = "cbFecha_ing"
        Me.cbFecha_ing.Size = New System.Drawing.Size(140, 21)
        Me.cbFecha_ing.TabIndex = 21
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(270, 203)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(77, 35)
        Me.Label12.TabIndex = 20
        Me.Label12.Text = "Periodo Contable"
        '
        'Tfecha_compra
        '
        Me.Tfecha_compra.CustomFormat = "dd-MM-yyyy"
        Me.Tfecha_compra.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Tfecha_compra.Location = New System.Drawing.Point(90, 203)
        Me.Tfecha_compra.Name = "Tfecha_compra"
        Me.Tfecha_compra.Size = New System.Drawing.Size(130, 20)
        Me.Tfecha_compra.TabIndex = 19
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(20, 203)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 35)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = "Fecha Adquisición"
        '
        'cboProveedor
        '
        Me.cboProveedor.DropDownHeight = 93
        Me.cboProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProveedor.FormattingEnabled = True
        Me.cboProveedor.IntegralHeight = False
        Me.cboProveedor.Location = New System.Drawing.Point(90, 168)
        Me.cboProveedor.MaxDropDownItems = 7
        Me.cboProveedor.Name = "cboProveedor"
        Me.cboProveedor.Size = New System.Drawing.Size(500, 21)
        Me.cboProveedor.TabIndex = 17
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(20, 168)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 13)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Proveedor"
        '
        'cboCateg
        '
        Me.cboCateg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCateg.FormattingEnabled = True
        Me.cboCateg.Location = New System.Drawing.Point(620, 70)
        Me.cboCateg.Name = "cboCateg"
        Me.cboCateg.Size = New System.Drawing.Size(140, 21)
        Me.cboCateg.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(550, 70)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 13)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Categoria"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(270, 103)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 13)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "Clase"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(270, 70)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Subzona"
        '
        'cboClase
        '
        Me.cboClase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboClase.FormattingEnabled = True
        Me.cboClase.Location = New System.Drawing.Point(350, 103)
        Me.cboClase.Name = "cboClase"
        Me.cboClase.Size = New System.Drawing.Size(140, 21)
        Me.cboClase.TabIndex = 11
        '
        'cboSubclase
        '
        Me.cboSubclase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSubclase.FormattingEnabled = True
        Me.cboSubclase.Location = New System.Drawing.Point(620, 103)
        Me.cboSubclase.Name = "cboSubclase"
        Me.cboSubclase.Size = New System.Drawing.Size(140, 21)
        Me.cboSubclase.TabIndex = 13
        '
        'cboConsist
        '
        Me.cboConsist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboConsist.FormattingEnabled = True
        Me.cboConsist.Location = New System.Drawing.Point(90, 103)
        Me.cboConsist.Name = "cboConsist"
        Me.cboConsist.Size = New System.Drawing.Size(140, 21)
        Me.cboConsist.TabIndex = 9
        '
        'cboZona
        '
        Me.cboZona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboZona.FormattingEnabled = True
        Me.cboZona.Location = New System.Drawing.Point(90, 70)
        Me.cboZona.Name = "cboZona"
        Me.cboZona.Size = New System.Drawing.Size(140, 21)
        Me.cboZona.TabIndex = 3
        '
        'Tdescrip
        '
        Me.Tdescrip.Location = New System.Drawing.Point(90, 15)
        Me.Tdescrip.Multiline = True
        Me.Tdescrip.Name = "Tdescrip"
        Me.Tdescrip.Size = New System.Drawing.Size(686, 40)
        Me.Tdescrip.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(550, 103)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Subclase"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 103)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Tipo"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Zona"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(20, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 40)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Descripción Simple"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'paso2
        '
        Me.paso2.Controls.Add(Me.btn_IFRS)
        Me.paso2.Controls.Add(Me.Frame2)
        Me.paso2.Controls.Add(Me.xt)
        Me.paso2.Controls.Add(Me.cboMetod)
        Me.paso2.Controls.Add(Me.Label20)
        Me.paso2.Controls.Add(Me.Tval_resI)
        Me.paso2.Controls.Add(Me.Label19)
        Me.paso2.Controls.Add(Me.TvuI)
        Me.paso2.Controls.Add(Me.Label18)
        Me.paso2.Location = New System.Drawing.Point(4, 22)
        Me.paso2.Name = "paso2"
        Me.paso2.Padding = New System.Windows.Forms.Padding(3)
        Me.paso2.Size = New System.Drawing.Size(796, 356)
        Me.paso2.TabIndex = 1
        Me.paso2.Text = "Ficha IFRS"
        Me.paso2.UseVisualStyleBackColor = True
        '
        'btn_IFRS
        '
        Me.btn_IFRS.Image = Global.AFN.My.Resources.Resources._32_next
        Me.btn_IFRS.Location = New System.Drawing.Point(337, 295)
        Me.btn_IFRS.Name = "btn_IFRS"
        Me.btn_IFRS.Size = New System.Drawing.Size(86, 55)
        Me.btn_IFRS.TabIndex = 8
        Me.btn_IFRS.Text = "Guardar"
        Me.btn_IFRS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_IFRS.UseVisualStyleBackColor = True
        '
        'Frame2
        '
        Me.Frame2.Controls.Add(Me.DataIFRS)
        Me.Frame2.Location = New System.Drawing.Point(85, 65)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Size = New System.Drawing.Size(588, 213)
        Me.Frame2.TabIndex = 7
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Valorización Inicial"
        '
        'DataIFRS
        '
        Me.DataIFRS.AllowUserToAddRows = False
        Me.DataIFRS.AllowUserToDeleteRows = False
        Me.DataIFRS.AllowUserToResizeColumns = False
        Me.DataIFRS.AllowUserToResizeRows = False
        Me.DataIFRS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataIFRS.Location = New System.Drawing.Point(42, 30)
        Me.DataIFRS.Name = "DataIFRS"
        Me.DataIFRS.ReadOnly = True
        Me.DataIFRS.RowHeadersVisible = False
        Me.DataIFRS.Size = New System.Drawing.Size(513, 156)
        Me.DataIFRS.TabIndex = 20
        '
        'xt
        '
        Me.xt.Location = New System.Drawing.Point(702, 149)
        Me.xt.Name = "xt"
        Me.xt.Size = New System.Drawing.Size(14, 20)
        Me.xt.TabIndex = 6
        Me.xt.Text = "xt"
        Me.xt.Visible = False
        '
        'cboMetod
        '
        Me.cboMetod.FormattingEnabled = True
        Me.cboMetod.Location = New System.Drawing.Point(394, 24)
        Me.cboMetod.Name = "cboMetod"
        Me.cboMetod.Size = New System.Drawing.Size(149, 21)
        Me.cboMetod.TabIndex = 3
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(270, 25)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(118, 13)
        Me.Label20.TabIndex = 2
        Me.Label20.Text = "Método de Valorización"
        '
        'Tval_resI
        '
        Me.Tval_resI.Location = New System.Drawing.Point(646, 21)
        Me.Tval_resI.Name = "Tval_resI"
        Me.Tval_resI.Size = New System.Drawing.Size(112, 20)
        Me.Tval_resI.TabIndex = 5
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(565, 24)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(75, 13)
        Me.Label19.TabIndex = 4
        Me.Label19.Text = "Valor Residual"
        '
        'TvuI
        '
        Me.TvuI.Location = New System.Drawing.Point(134, 24)
        Me.TvuI.Name = "TvuI"
        Me.TvuI.Size = New System.Drawing.Size(111, 20)
        Me.TvuI.TabIndex = 1
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(20, 27)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(103, 13)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Vida Util IFRS (días)"
        '
        'paso3
        '
        Me.paso3.Controls.Add(Me.ckMostrar)
        Me.paso3.Controls.Add(Me.btn_buscaG)
        Me.paso3.Controls.Add(Me.AtribGrupo)
        Me.paso3.Controls.Add(Me.btn_imprimir)
        Me.paso3.Controls.Add(Me.btn_detallexG)
        Me.paso3.Controls.Add(Me.cbGvalor)
        Me.paso3.Controls.Add(Me.TGvalor)
        Me.paso3.Controls.Add(Me.Label30)
        Me.paso3.Controls.Add(Me.cbGatrib)
        Me.paso3.Controls.Add(Me.Label29)
        Me.paso3.Controls.Add(Me.btn_lessGA)
        Me.paso3.Controls.Add(Me.btn_addGA)
        Me.paso3.Location = New System.Drawing.Point(4, 22)
        Me.paso3.Name = "paso3"
        Me.paso3.Padding = New System.Windows.Forms.Padding(3)
        Me.paso3.Size = New System.Drawing.Size(796, 356)
        Me.paso3.TabIndex = 2
        Me.paso3.Text = "Descripción por Grupo"
        Me.paso3.UseVisualStyleBackColor = True
        '
        'ckMostrar
        '
        Me.ckMostrar.AutoSize = True
        Me.ckMostrar.Location = New System.Drawing.Point(410, 67)
        Me.ckMostrar.Name = "ckMostrar"
        Me.ckMostrar.Size = New System.Drawing.Size(133, 17)
        Me.ckMostrar.TabIndex = 6
        Me.ckMostrar.Text = "Mostrar en descripción"
        Me.ckMostrar.UseVisualStyleBackColor = True
        '
        'btn_buscaG
        '
        Me.btn_buscaG.Image = Global.AFN.My.Resources.Resources.find
        Me.btn_buscaG.Location = New System.Drawing.Point(612, 25)
        Me.btn_buscaG.Name = "btn_buscaG"
        Me.btn_buscaG.Size = New System.Drawing.Size(31, 31)
        Me.btn_buscaG.TabIndex = 5
        Me.btn_buscaG.UseVisualStyleBackColor = True
        '
        'AtribGrupo
        '
        Me.AtribGrupo.AllowUserToAddRows = False
        Me.AtribGrupo.AllowUserToDeleteRows = False
        Me.AtribGrupo.AllowUserToResizeColumns = False
        Me.AtribGrupo.AllowUserToResizeRows = False
        Me.AtribGrupo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AtribGrupo.Location = New System.Drawing.Point(28, 144)
        Me.AtribGrupo.MultiSelect = False
        Me.AtribGrupo.Name = "AtribGrupo"
        Me.AtribGrupo.ReadOnly = True
        Me.AtribGrupo.RowHeadersWidth = 20
        Me.AtribGrupo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.AtribGrupo.Size = New System.Drawing.Size(752, 195)
        Me.AtribGrupo.TabIndex = 11
        '
        'btn_imprimir
        '
        Me.btn_imprimir.Location = New System.Drawing.Point(507, 99)
        Me.btn_imprimir.Name = "btn_imprimir"
        Me.btn_imprimir.Size = New System.Drawing.Size(75, 31)
        Me.btn_imprimir.TabIndex = 10
        Me.btn_imprimir.Text = "Visualizar"
        Me.btn_imprimir.UseVisualStyleBackColor = True
        '
        'btn_detallexG
        '
        Me.btn_detallexG.Location = New System.Drawing.Point(406, 99)
        Me.btn_detallexG.Name = "btn_detallexG"
        Me.btn_detallexG.Size = New System.Drawing.Size(75, 31)
        Me.btn_detallexG.TabIndex = 9
        Me.btn_detallexG.Text = "Guardar"
        Me.btn_detallexG.UseVisualStyleBackColor = True
        '
        'cbGvalor
        '
        Me.cbGvalor.FormattingEnabled = True
        Me.cbGvalor.Location = New System.Drawing.Point(407, 31)
        Me.cbGvalor.Name = "cbGvalor"
        Me.cbGvalor.Size = New System.Drawing.Size(175, 21)
        Me.cbGvalor.TabIndex = 4
        '
        'TGvalor
        '
        Me.TGvalor.Location = New System.Drawing.Point(407, 31)
        Me.TGvalor.Name = "TGvalor"
        Me.TGvalor.Size = New System.Drawing.Size(175, 20)
        Me.TGvalor.TabIndex = 3
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(361, 31)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(40, 15)
        Me.Label30.TabIndex = 2
        Me.Label30.Text = "Valor"
        '
        'cbGatrib
        '
        Me.cbGatrib.FormattingEnabled = True
        Me.cbGatrib.Location = New System.Drawing.Point(118, 31)
        Me.cbGatrib.Name = "cbGatrib"
        Me.cbGatrib.Size = New System.Drawing.Size(156, 21)
        Me.cbGatrib.TabIndex = 1
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(55, 31)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(56, 15)
        Me.Label29.TabIndex = 0
        Me.Label29.Text = "Atributo"
        '
        'btn_lessGA
        '
        Me.btn_lessGA.Image = Global.AFN.My.Resources.Resources.remove
        Me.btn_lessGA.Location = New System.Drawing.Point(268, 99)
        Me.btn_lessGA.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_lessGA.Name = "btn_lessGA"
        Me.btn_lessGA.Size = New System.Drawing.Size(36, 39)
        Me.btn_lessGA.TabIndex = 8
        Me.btn_lessGA.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btn_lessGA.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_lessGA.UseVisualStyleBackColor = True
        '
        'btn_addGA
        '
        Me.btn_addGA.Image = Global.AFN.My.Resources.Resources.Add
        Me.btn_addGA.Location = New System.Drawing.Point(215, 99)
        Me.btn_addGA.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_addGA.Name = "btn_addGA"
        Me.btn_addGA.Size = New System.Drawing.Size(36, 39)
        Me.btn_addGA.TabIndex = 7
        Me.btn_addGA.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btn_addGA.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_addGA.UseVisualStyleBackColor = True
        '
        'paso4
        '
        Me.paso4.Controls.Add(Me.ckMostrarA)
        Me.paso4.Controls.Add(Me.AtribArticulo)
        Me.paso4.Controls.Add(Me.cbAatrib)
        Me.paso4.Controls.Add(Me.Label33)
        Me.paso4.Controls.Add(Me.btn_imprimir1)
        Me.paso4.Controls.Add(Me.btn_detallexA)
        Me.paso4.Controls.Add(Me.cbAvalor)
        Me.paso4.Controls.Add(Me.Label32)
        Me.paso4.Controls.Add(Me.cblistaArticulo)
        Me.paso4.Controls.Add(Me.Label31)
        Me.paso4.Controls.Add(Me.TAvalor)
        Me.paso4.Controls.Add(Me.btn_lessDA)
        Me.paso4.Controls.Add(Me.btn_addDA)
        Me.paso4.Controls.Add(Me.btn_buscaA)
        Me.paso4.Location = New System.Drawing.Point(4, 22)
        Me.paso4.Name = "paso4"
        Me.paso4.Padding = New System.Windows.Forms.Padding(3)
        Me.paso4.Size = New System.Drawing.Size(796, 356)
        Me.paso4.TabIndex = 3
        Me.paso4.Text = "Descripción por Articulo"
        Me.paso4.UseVisualStyleBackColor = True
        '
        'ckMostrarA
        '
        Me.ckMostrarA.AutoSize = True
        Me.ckMostrarA.Location = New System.Drawing.Point(407, 64)
        Me.ckMostrarA.Name = "ckMostrarA"
        Me.ckMostrarA.Size = New System.Drawing.Size(118, 17)
        Me.ckMostrarA.TabIndex = 8
        Me.ckMostrarA.Text = "Mostrar en Etiqueta"
        Me.ckMostrarA.UseVisualStyleBackColor = True
        '
        'AtribArticulo
        '
        Me.AtribArticulo.AllowUserToAddRows = False
        Me.AtribArticulo.AllowUserToDeleteRows = False
        Me.AtribArticulo.AllowUserToResizeColumns = False
        Me.AtribArticulo.AllowUserToResizeRows = False
        Me.AtribArticulo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AtribArticulo.Location = New System.Drawing.Point(28, 144)
        Me.AtribArticulo.Name = "AtribArticulo"
        Me.AtribArticulo.ReadOnly = True
        Me.AtribArticulo.RowHeadersWidth = 20
        Me.AtribArticulo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.AtribArticulo.Size = New System.Drawing.Size(752, 195)
        Me.AtribArticulo.TabIndex = 13
        '
        'cbAatrib
        '
        Me.cbAatrib.FormattingEnabled = True
        Me.cbAatrib.Location = New System.Drawing.Point(118, 58)
        Me.cbAatrib.Name = "cbAatrib"
        Me.cbAatrib.Size = New System.Drawing.Size(156, 21)
        Me.cbAatrib.TabIndex = 3
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(55, 58)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(56, 15)
        Me.Label33.TabIndex = 2
        Me.Label33.Text = "Atributo"
        '
        'btn_imprimir1
        '
        Me.btn_imprimir1.Location = New System.Drawing.Point(507, 99)
        Me.btn_imprimir1.Name = "btn_imprimir1"
        Me.btn_imprimir1.Size = New System.Drawing.Size(75, 31)
        Me.btn_imprimir1.TabIndex = 12
        Me.btn_imprimir1.Text = "Visualizar"
        Me.btn_imprimir1.UseVisualStyleBackColor = True
        '
        'btn_detallexA
        '
        Me.btn_detallexA.Location = New System.Drawing.Point(406, 99)
        Me.btn_detallexA.Name = "btn_detallexA"
        Me.btn_detallexA.Size = New System.Drawing.Size(75, 31)
        Me.btn_detallexA.TabIndex = 11
        Me.btn_detallexA.Text = "Guardar"
        Me.btn_detallexA.UseVisualStyleBackColor = True
        '
        'cbAvalor
        '
        Me.cbAvalor.FormattingEnabled = True
        Me.cbAvalor.Location = New System.Drawing.Point(406, 28)
        Me.cbAvalor.Name = "cbAvalor"
        Me.cbAvalor.Size = New System.Drawing.Size(175, 21)
        Me.cbAvalor.TabIndex = 6
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(360, 28)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(40, 15)
        Me.Label32.TabIndex = 4
        Me.Label32.Text = "Valor"
        '
        'cblistaArticulo
        '
        Me.cblistaArticulo.FormattingEnabled = True
        Me.cblistaArticulo.Location = New System.Drawing.Point(118, 27)
        Me.cblistaArticulo.Name = "cblistaArticulo"
        Me.cblistaArticulo.Size = New System.Drawing.Size(156, 21)
        Me.cblistaArticulo.TabIndex = 1
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(55, 28)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(55, 15)
        Me.Label31.TabIndex = 0
        Me.Label31.Text = "Articulo"
        '
        'TAvalor
        '
        Me.TAvalor.Location = New System.Drawing.Point(406, 28)
        Me.TAvalor.Name = "TAvalor"
        Me.TAvalor.Size = New System.Drawing.Size(175, 20)
        Me.TAvalor.TabIndex = 5
        '
        'btn_lessDA
        '
        Me.btn_lessDA.Image = Global.AFN.My.Resources.Resources.remove
        Me.btn_lessDA.Location = New System.Drawing.Point(268, 99)
        Me.btn_lessDA.Name = "btn_lessDA"
        Me.btn_lessDA.Size = New System.Drawing.Size(36, 39)
        Me.btn_lessDA.TabIndex = 10
        Me.btn_lessDA.UseVisualStyleBackColor = True
        '
        'btn_addDA
        '
        Me.btn_addDA.Image = Global.AFN.My.Resources.Resources.Add
        Me.btn_addDA.Location = New System.Drawing.Point(215, 99)
        Me.btn_addDA.Name = "btn_addDA"
        Me.btn_addDA.Size = New System.Drawing.Size(36, 39)
        Me.btn_addDA.TabIndex = 9
        Me.btn_addDA.UseVisualStyleBackColor = True
        '
        'btn_buscaA
        '
        Me.btn_buscaA.Image = Global.AFN.My.Resources.Resources.find
        Me.btn_buscaA.Location = New System.Drawing.Point(611, 22)
        Me.btn_buscaA.Name = "btn_buscaA"
        Me.btn_buscaA.Size = New System.Drawing.Size(31, 31)
        Me.btn_buscaA.TabIndex = 7
        Me.btn_buscaA.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Enabled = False
        Me.Label5.Location = New System.Drawing.Point(28, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Código Lote"
        '
        'artic
        '
        Me.artic.Enabled = False
        Me.artic.Location = New System.Drawing.Point(27, 29)
        Me.artic.Name = "artic"
        Me.artic.Size = New System.Drawing.Size(100, 20)
        Me.artic.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Enabled = False
        Me.Label6.Location = New System.Drawing.Point(140, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(110, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Descripción Completa"
        '
        'TFulldescrip
        '
        Me.TFulldescrip.Enabled = False
        Me.TFulldescrip.Location = New System.Drawing.Point(143, 29)
        Me.TFulldescrip.Multiline = True
        Me.TFulldescrip.Name = "TFulldescrip"
        Me.TFulldescrip.Size = New System.Drawing.Size(416, 43)
        Me.TFulldescrip.TabIndex = 3
        '
        'CkEstado
        '
        Me.CkEstado.AutoSize = True
        Me.CkEstado.Enabled = False
        Me.CkEstado.Location = New System.Drawing.Point(600, 32)
        Me.CkEstado.Name = "CkEstado"
        Me.CkEstado.Size = New System.Drawing.Size(68, 17)
        Me.CkEstado.TabIndex = 4
        Me.CkEstado.Text = "Activado"
        Me.CkEstado.UseVisualStyleBackColor = True
        '
        'fuente
        '
        Me.fuente.Enabled = False
        Me.fuente.Location = New System.Drawing.Point(604, 52)
        Me.fuente.Name = "fuente"
        Me.fuente.Size = New System.Drawing.Size(36, 20)
        Me.fuente.TabIndex = 5
        Me.fuente.Text = "REG"
        Me.fuente.Visible = False
        '
        'btn_modif
        '
        Me.btn_modif.Image = Global.AFN.My.Resources.Resources._32_find
        Me.btn_modif.Location = New System.Drawing.Point(775, 16)
        Me.btn_modif.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_modif.Name = "btn_modif"
        Me.btn_modif.Size = New System.Drawing.Size(48, 45)
        Me.btn_modif.TabIndex = 7
        Me.btn_modif.Text = "Buscar"
        Me.btn_modif.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_modif.UseVisualStyleBackColor = True
        '
        'btn_new
        '
        Me.btn_new.Image = Global.AFN.My.Resources.Resources._32_add
        Me.btn_new.Location = New System.Drawing.Point(703, 16)
        Me.btn_new.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_new.Name = "btn_new"
        Me.btn_new.Size = New System.Drawing.Size(48, 45)
        Me.btn_new.TabIndex = 6
        Me.btn_new.Text = "Limpiar"
        Me.btn_new.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_new.UseVisualStyleBackColor = True
        '
        'dialogo
        '
        Me.dialogo.FileName = "foto"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(20, 139)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(43, 13)
        Me.Label21.TabIndex = 14
        Me.Label21.Text = "Gestion"
        '
        'cboGestion
        '
        Me.cboGestion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGestion.FormattingEnabled = True
        Me.cboGestion.Location = New System.Drawing.Point(90, 136)
        Me.cboGestion.Name = "cboGestion"
        Me.cboGestion.Size = New System.Drawing.Size(140, 21)
        Me.cboGestion.TabIndex = 15
        '
        'form_ingreso
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(869, 502)
        Me.Controls.Add(Me.fuente)
        Me.Controls.Add(Me.CkEstado)
        Me.Controls.Add(Me.TFulldescrip)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btn_modif)
        Me.Controls.Add(Me.btn_new)
        Me.Controls.Add(Me.artic)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.pasos)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "form_ingreso"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mantenedor de Bienes de Activo Fijo"
        Me.pasos.ResumeLayout(False)
        Me.paso1.ResumeLayout(False)
        Me.paso1.PerformLayout()
        CType(Me.btn_Bprov, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Fderecho.ResumeLayout(False)
        Me.Fderecho.PerformLayout()
        Me.paso2.ResumeLayout(False)
        Me.paso2.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        CType(Me.DataIFRS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.paso3.ResumeLayout(False)
        Me.paso3.PerformLayout()
        CType(Me.AtribGrupo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.paso4.ResumeLayout(False)
        Me.paso4.PerformLayout()
        CType(Me.AtribArticulo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pasos As System.Windows.Forms.TabControl
    Friend WithEvents paso1 As System.Windows.Forms.TabPage
    Friend WithEvents paso2 As System.Windows.Forms.TabPage
    Friend WithEvents paso3 As System.Windows.Forms.TabPage
    Friend WithEvents btn_act As System.Windows.Forms.Button
    Friend WithEvents btn_elim As System.Windows.Forms.Button
    Friend WithEvents btn_guardar As System.Windows.Forms.Button
    Friend WithEvents ckIFRS As System.Windows.Forms.CheckBox
    Friend WithEvents Fderecho As System.Windows.Forms.GroupBox
    Friend WithEvents derC2 As System.Windows.Forms.RadioButton
    Friend WithEvents derC1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Tdoc As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TvuF As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Tprecio_compra As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Tcantidad As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cbFecha_ing As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Tfecha_compra As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboCateg As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboClase As System.Windows.Forms.ComboBox
    Friend WithEvents cboSubclase As System.Windows.Forms.ComboBox
    Friend WithEvents cboConsist As System.Windows.Forms.ComboBox
    Friend WithEvents cboZona As System.Windows.Forms.ComboBox
    Friend WithEvents Tdescrip As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btn_IFRS As System.Windows.Forms.Button
    Friend WithEvents Frame2 As System.Windows.Forms.GroupBox
    Friend WithEvents xt As System.Windows.Forms.TextBox
    Friend WithEvents cboMetod As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Tval_resI As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TvuI As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents AtribGrupo As System.Windows.Forms.DataGridView
    Friend WithEvents btn_imprimir As System.Windows.Forms.Button
    Friend WithEvents btn_detallexG As System.Windows.Forms.Button
    Friend WithEvents btn_lessGA As System.Windows.Forms.Button
    Friend WithEvents btn_addGA As System.Windows.Forms.Button
    Friend WithEvents cbGvalor As System.Windows.Forms.ComboBox
    Friend WithEvents TGvalor As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents cbGatrib As System.Windows.Forms.ComboBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents paso4 As System.Windows.Forms.TabPage
    Friend WithEvents AtribArticulo As System.Windows.Forms.DataGridView
    Friend WithEvents cbAatrib As System.Windows.Forms.ComboBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents btn_imprimir1 As System.Windows.Forms.Button
    Friend WithEvents btn_detallexA As System.Windows.Forms.Button
    Friend WithEvents btn_lessDA As System.Windows.Forms.Button
    Friend WithEvents btn_addDA As System.Windows.Forms.Button
    Friend WithEvents btn_buscaA As System.Windows.Forms.Button
    Friend WithEvents TAvalor As System.Windows.Forms.TextBox
    Friend WithEvents cbAvalor As System.Windows.Forms.ComboBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents cblistaArticulo As System.Windows.Forms.ComboBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents artic As System.Windows.Forms.TextBox
    Friend WithEvents btn_new As System.Windows.Forms.Button
    Friend WithEvents btn_modif As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TFulldescrip As System.Windows.Forms.TextBox
    Friend WithEvents btn_Bprov As System.Windows.Forms.PictureBox
    Friend WithEvents btn_buscaG As System.Windows.Forms.Button
    Friend WithEvents CkEstado As System.Windows.Forms.CheckBox
    Friend WithEvents cboSubzona As System.Windows.Forms.ComboBox
    Friend WithEvents TxtPrecioTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents fuente As System.Windows.Forms.TextBox
    Friend WithEvents residuo As System.Windows.Forms.TextBox
    Friend WithEvents DataIFRS As System.Windows.Forms.DataGridView
    Friend WithEvents ckMostrar As System.Windows.Forms.CheckBox
    Friend WithEvents dialogo As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ckMostrarA As System.Windows.Forms.CheckBox
    Friend WithEvents ckDepre As System.Windows.Forms.CheckBox
    Friend WithEvents cboGestion As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
End Class
