<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_ing_obra
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_ing_obra))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Tfecha_compra = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboProveedor = New System.Windows.Forms.ComboBox()
        Me.Tcredito = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboZona = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Tdoc = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Tdescrip = New System.Windows.Forms.TextBox()
        Me.btn_guardar = New System.Windows.Forms.Button()
        Me.btn_limpiar = New System.Windows.Forms.Button()
        Me.btn_Bprov = New System.Windows.Forms.PictureBox()
        Me.Tfecha_conta = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.btn_Bprov, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Fecha Factura"
        '
        'Tfecha_compra
        '
        Me.Tfecha_compra.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Tfecha_compra.Location = New System.Drawing.Point(122, 25)
        Me.Tfecha_compra.Name = "Tfecha_compra"
        Me.Tfecha_compra.Size = New System.Drawing.Size(143, 20)
        Me.Tfecha_compra.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 99)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Monto"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 140)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Proveedor"
        '
        'cboProveedor
        '
        Me.cboProveedor.FormattingEnabled = True
        Me.cboProveedor.Location = New System.Drawing.Point(122, 137)
        Me.cboProveedor.Name = "cboProveedor"
        Me.cboProveedor.Size = New System.Drawing.Size(428, 21)
        Me.cboProveedor.TabIndex = 11
        '
        'Tcredito
        '
        Me.Tcredito.Location = New System.Drawing.Point(121, 96)
        Me.Tcredito.Name = "Tcredito"
        Me.Tcredito.Size = New System.Drawing.Size(144, 20)
        Me.Tcredito.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(326, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Zona"
        '
        'cboZona
        '
        Me.cboZona.FormattingEnabled = True
        Me.cboZona.Location = New System.Drawing.Point(411, 25)
        Me.cboZona.Name = "cboZona"
        Me.cboZona.Size = New System.Drawing.Size(171, 21)
        Me.cboZona.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(326, 99)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Nº Documento"
        '
        'Tdoc
        '
        Me.Tdoc.Location = New System.Drawing.Point(411, 96)
        Me.Tdoc.Name = "Tdoc"
        Me.Tdoc.Size = New System.Drawing.Size(171, 20)
        Me.Tdoc.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(27, 179)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Descripcion"
        '
        'Tdescrip
        '
        Me.Tdescrip.Location = New System.Drawing.Point(122, 179)
        Me.Tdescrip.Multiline = True
        Me.Tdescrip.Name = "Tdescrip"
        Me.Tdescrip.Size = New System.Drawing.Size(460, 59)
        Me.Tdescrip.TabIndex = 13
        '
        'btn_guardar
        '
        Me.btn_guardar.Location = New System.Drawing.Point(227, 254)
        Me.btn_guardar.Name = "btn_guardar"
        Me.btn_guardar.Size = New System.Drawing.Size(75, 33)
        Me.btn_guardar.TabIndex = 14
        Me.btn_guardar.Text = "Guardar"
        Me.btn_guardar.UseVisualStyleBackColor = True
        '
        'btn_limpiar
        '
        Me.btn_limpiar.Location = New System.Drawing.Point(319, 255)
        Me.btn_limpiar.Name = "btn_limpiar"
        Me.btn_limpiar.Size = New System.Drawing.Size(75, 33)
        Me.btn_limpiar.TabIndex = 15
        Me.btn_limpiar.Text = "Nuevo"
        Me.btn_limpiar.UseVisualStyleBackColor = True
        '
        'btn_Bprov
        '
        Me.btn_Bprov.Image = Global.AFN.My.Resources.Resources.find
        Me.btn_Bprov.Location = New System.Drawing.Point(556, 137)
        Me.btn_Bprov.Name = "btn_Bprov"
        Me.btn_Bprov.Size = New System.Drawing.Size(26, 24)
        Me.btn_Bprov.TabIndex = 10
        Me.btn_Bprov.TabStop = False
        '
        'Tfecha_conta
        '
        Me.Tfecha_conta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Tfecha_conta.Location = New System.Drawing.Point(122, 61)
        Me.Tfecha_conta.Name = "Tfecha_conta"
        Me.Tfecha_conta.Size = New System.Drawing.Size(143, 20)
        Me.Tfecha_conta.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(27, 61)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Fecha Contab"
        '
        'form_ing_obra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(620, 314)
        Me.Controls.Add(Me.Tfecha_conta)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btn_limpiar)
        Me.Controls.Add(Me.btn_guardar)
        Me.Controls.Add(Me.Tdescrip)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btn_Bprov)
        Me.Controls.Add(Me.Tdoc)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboZona)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Tcredito)
        Me.Controls.Add(Me.cboProveedor)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Tfecha_compra)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "form_ing_obra"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Entrada de Obra en Construccion"
        CType(Me.btn_Bprov, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Tfecha_compra As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents Tcredito As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboZona As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Tdoc As System.Windows.Forms.TextBox
    Friend WithEvents btn_Bprov As System.Windows.Forms.PictureBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Tdescrip As System.Windows.Forms.TextBox
    Friend WithEvents btn_guardar As System.Windows.Forms.Button
    Friend WithEvents btn_limpiar As System.Windows.Forms.Button
    Friend WithEvents Tfecha_conta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
