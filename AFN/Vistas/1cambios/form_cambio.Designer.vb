<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_cambio
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_cambio))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cod_art = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Tarticulo = New System.Windows.Forms.TextBox()
        Me.cboCant = New System.Windows.Forms.ComboBox()
        Me.Dcambio = New System.Windows.Forms.DateTimePicker()
        Me.Tvalor = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboZona = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboSubzona = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TzonaAct = New System.Windows.Forms.TextBox()
        Me.TsubZact = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lista_cambiar = New System.Windows.Forms.DataGridView()
        Me.btn_fin = New System.Windows.Forms.Button()
        Me.rowindx = New System.Windows.Forms.TextBox()
        Me.zona_art = New System.Windows.Forms.TextBox()
        Me.subzona_art = New System.Windows.Forms.TextBox()
        Me.Gparte = New System.Windows.Forms.TextBox()
        Me.btn_consulta = New System.Windows.Forms.Button()
        Me.btn_detalle_cantidad = New System.Windows.Forms.PictureBox()
        Me.btn_remove = New System.Windows.Forms.PictureBox()
        Me.btn_add = New System.Windows.Forms.PictureBox()
        CType(Me.lista_cambiar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_detalle_cantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_remove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_add, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(159, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Seleccione Artículo a Traspasar"
        '
        'cod_art
        '
        Me.cod_art.Location = New System.Drawing.Point(198, 9)
        Me.cod_art.Name = "cod_art"
        Me.cod_art.Size = New System.Drawing.Size(100, 13)
        Me.cod_art.TabIndex = 1
        Me.cod_art.Text = "Label2"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(31, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Artículo"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(31, 61)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Cantidad"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(31, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Fecha"
        '
        'Tarticulo
        '
        Me.Tarticulo.Location = New System.Drawing.Point(106, 35)
        Me.Tarticulo.Name = "Tarticulo"
        Me.Tarticulo.Size = New System.Drawing.Size(355, 20)
        Me.Tarticulo.TabIndex = 5
        '
        'cboCant
        '
        Me.cboCant.FormattingEnabled = True
        Me.cboCant.Location = New System.Drawing.Point(106, 61)
        Me.cboCant.Name = "cboCant"
        Me.cboCant.Size = New System.Drawing.Size(114, 21)
        Me.cboCant.TabIndex = 6
        '
        'Dcambio
        '
        Me.Dcambio.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Dcambio.Location = New System.Drawing.Point(106, 88)
        Me.Dcambio.Name = "Dcambio"
        Me.Dcambio.Size = New System.Drawing.Size(114, 20)
        Me.Dcambio.TabIndex = 7
        '
        'Tvalor
        '
        Me.Tvalor.Location = New System.Drawing.Point(351, 61)
        Me.Tvalor.Name = "Tvalor"
        Me.Tvalor.Size = New System.Drawing.Size(110, 20)
        Me.Tvalor.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(284, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Valor Libro"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(31, 117)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Zona Destino"
        '
        'cboZona
        '
        Me.cboZona.FormattingEnabled = True
        Me.cboZona.Location = New System.Drawing.Point(106, 114)
        Me.cboZona.Name = "cboZona"
        Me.cboZona.Size = New System.Drawing.Size(146, 21)
        Me.cboZona.TabIndex = 11
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(284, 117)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 13)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "Subzona Destino"
        '
        'cboSubzona
        '
        Me.cboSubzona.FormattingEnabled = True
        Me.cboSubzona.Location = New System.Drawing.Point(386, 114)
        Me.cboSubzona.Name = "cboSubzona"
        Me.cboSubzona.Size = New System.Drawing.Size(151, 21)
        Me.cboSubzona.TabIndex = 13
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(31, 141)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 13)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Zona Actual"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(284, 141)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 13)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "Subzona Actual"
        '
        'TzonaAct
        '
        Me.TzonaAct.Location = New System.Drawing.Point(106, 141)
        Me.TzonaAct.Name = "TzonaAct"
        Me.TzonaAct.Size = New System.Drawing.Size(146, 20)
        Me.TzonaAct.TabIndex = 16
        '
        'TsubZact
        '
        Me.TsubZact.Location = New System.Drawing.Point(386, 141)
        Me.TsubZact.Name = "TsubZact"
        Me.TsubZact.Size = New System.Drawing.Size(151, 20)
        Me.TsubZact.TabIndex = 17
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(23, 204)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(147, 13)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Articulos listos para Traspasar"
        '
        'lista_cambiar
        '
        Me.lista_cambiar.AllowUserToAddRows = False
        Me.lista_cambiar.AllowUserToDeleteRows = False
        Me.lista_cambiar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.lista_cambiar.Location = New System.Drawing.Point(20, 224)
        Me.lista_cambiar.MultiSelect = False
        Me.lista_cambiar.Name = "lista_cambiar"
        Me.lista_cambiar.ReadOnly = True
        Me.lista_cambiar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.lista_cambiar.Size = New System.Drawing.Size(533, 143)
        Me.lista_cambiar.TabIndex = 21
        '
        'btn_fin
        '
        Me.btn_fin.Location = New System.Drawing.Point(258, 373)
        Me.btn_fin.Name = "btn_fin"
        Me.btn_fin.Size = New System.Drawing.Size(56, 31)
        Me.btn_fin.TabIndex = 22
        Me.btn_fin.Text = "Guardar"
        Me.btn_fin.UseVisualStyleBackColor = True
        '
        'rowindx
        '
        Me.rowindx.Location = New System.Drawing.Point(26, 376)
        Me.rowindx.Name = "rowindx"
        Me.rowindx.Size = New System.Drawing.Size(38, 20)
        Me.rowindx.TabIndex = 23
        Me.rowindx.Visible = False
        '
        'zona_art
        '
        Me.zona_art.Location = New System.Drawing.Point(70, 376)
        Me.zona_art.Name = "zona_art"
        Me.zona_art.Size = New System.Drawing.Size(38, 20)
        Me.zona_art.TabIndex = 24
        Me.zona_art.Visible = False
        '
        'subzona_art
        '
        Me.subzona_art.Location = New System.Drawing.Point(114, 376)
        Me.subzona_art.Name = "subzona_art"
        Me.subzona_art.Size = New System.Drawing.Size(38, 20)
        Me.subzona_art.TabIndex = 25
        Me.subzona_art.Visible = False
        '
        'Gparte
        '
        Me.Gparte.Location = New System.Drawing.Point(158, 376)
        Me.Gparte.Name = "Gparte"
        Me.Gparte.Size = New System.Drawing.Size(38, 20)
        Me.Gparte.TabIndex = 26
        Me.Gparte.Visible = False
        '
        'btn_consulta
        '
        Me.btn_consulta.Location = New System.Drawing.Point(481, 27)
        Me.btn_consulta.Name = "btn_consulta"
        Me.btn_consulta.Size = New System.Drawing.Size(56, 34)
        Me.btn_consulta.TabIndex = 27
        Me.btn_consulta.Text = "Buscar"
        Me.btn_consulta.UseVisualStyleBackColor = True
        '
        'btn_detalle_cantidad
        '
        Me.btn_detalle_cantidad.Image = Global.AFN.My.Resources.Resources.eject
        Me.btn_detalle_cantidad.InitialImage = Nothing
        Me.btn_detalle_cantidad.Location = New System.Drawing.Point(228, 61)
        Me.btn_detalle_cantidad.Name = "btn_detalle_cantidad"
        Me.btn_detalle_cantidad.Size = New System.Drawing.Size(24, 24)
        Me.btn_detalle_cantidad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.btn_detalle_cantidad.TabIndex = 28
        Me.btn_detalle_cantidad.TabStop = False
        '
        'btn_remove
        '
        Me.btn_remove.Image = Global.AFN.My.Resources.Resources.remove
        Me.btn_remove.Location = New System.Drawing.Point(454, 182)
        Me.btn_remove.Name = "btn_remove"
        Me.btn_remove.Size = New System.Drawing.Size(35, 35)
        Me.btn_remove.TabIndex = 19
        Me.btn_remove.TabStop = False
        '
        'btn_add
        '
        Me.btn_add.Image = Global.AFN.My.Resources.Resources.Add
        Me.btn_add.Location = New System.Drawing.Point(398, 182)
        Me.btn_add.Name = "btn_add"
        Me.btn_add.Size = New System.Drawing.Size(36, 36)
        Me.btn_add.TabIndex = 18
        Me.btn_add.TabStop = False
        '
        'form_cambio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 416)
        Me.Controls.Add(Me.btn_detalle_cantidad)
        Me.Controls.Add(Me.btn_consulta)
        Me.Controls.Add(Me.Gparte)
        Me.Controls.Add(Me.subzona_art)
        Me.Controls.Add(Me.zona_art)
        Me.Controls.Add(Me.rowindx)
        Me.Controls.Add(Me.btn_fin)
        Me.Controls.Add(Me.lista_cambiar)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.btn_remove)
        Me.Controls.Add(Me.btn_add)
        Me.Controls.Add(Me.TsubZact)
        Me.Controls.Add(Me.TzonaAct)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cboSubzona)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cboZona)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Tvalor)
        Me.Controls.Add(Me.Dcambio)
        Me.Controls.Add(Me.cboCant)
        Me.Controls.Add(Me.Tarticulo)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cod_art)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(589, 454)
        Me.Name = "form_cambio"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Traspaso de Zona Activo Fijo"
        CType(Me.lista_cambiar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_detalle_cantidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_remove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_add, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cod_art As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Tarticulo As System.Windows.Forms.TextBox
    Friend WithEvents cboCant As System.Windows.Forms.ComboBox
    Friend WithEvents Dcambio As System.Windows.Forms.DateTimePicker
    Friend WithEvents Tvalor As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboZona As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboSubzona As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TzonaAct As System.Windows.Forms.TextBox
    Friend WithEvents TsubZact As System.Windows.Forms.TextBox
    Friend WithEvents btn_add As System.Windows.Forms.PictureBox
    Friend WithEvents btn_remove As System.Windows.Forms.PictureBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lista_cambiar As System.Windows.Forms.DataGridView
    Friend WithEvents btn_fin As System.Windows.Forms.Button
    Friend WithEvents rowindx As System.Windows.Forms.TextBox
    Friend WithEvents zona_art As System.Windows.Forms.TextBox
    Friend WithEvents subzona_art As System.Windows.Forms.TextBox
    Friend WithEvents Gparte As System.Windows.Forms.TextBox
    Friend WithEvents btn_consulta As System.Windows.Forms.Button
    Friend WithEvents btn_detalle_cantidad As System.Windows.Forms.PictureBox
End Class
