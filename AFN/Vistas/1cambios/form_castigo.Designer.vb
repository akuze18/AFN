<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_castigo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_castigo))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cod_art = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tarticulo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboCant = New System.Windows.Forms.ComboBox()
        Me.Dcastigo = New System.Windows.Forms.DateTimePicker()
        Me.Tvalor = New System.Windows.Forms.TextBox()
        Me.btn_consulta = New System.Windows.Forms.Button()
        Me.CheckF = New System.Windows.Forms.CheckBox()
        Me.CheckT = New System.Windows.Forms.CheckBox()
        Me.CheckI = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lista_castigar = New System.Windows.Forms.DataGridView()
        Me.btn_remove = New System.Windows.Forms.PictureBox()
        Me.btn_add = New System.Windows.Forms.PictureBox()
        Me.btn_fin = New System.Windows.Forms.Button()
        Me.rowindx = New System.Windows.Forms.TextBox()
        Me.zona_art = New System.Windows.Forms.TextBox()
        Me.Gparte = New System.Windows.Forms.TextBox()
        Me.btn_detalle_cantidad = New System.Windows.Forms.PictureBox()
        CType(Me.lista_castigar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_remove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_add, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_detalle_cantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(150, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Seleccione Artículo a Castigar"
        '
        'cod_art
        '
        Me.cod_art.Location = New System.Drawing.Point(186, 9)
        Me.cod_art.Name = "cod_art"
        Me.cod_art.Size = New System.Drawing.Size(84, 17)
        Me.cod_art.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(29, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Artículo"
        '
        'Tarticulo
        '
        Me.Tarticulo.Location = New System.Drawing.Point(98, 42)
        Me.Tarticulo.Name = "Tarticulo"
        Me.Tarticulo.Size = New System.Drawing.Size(349, 20)
        Me.Tarticulo.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(29, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Cantidad"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(29, 108)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Fecha"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(284, 79)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Valor Libro"
        '
        'cboCant
        '
        Me.cboCant.FormattingEnabled = True
        Me.cboCant.Location = New System.Drawing.Point(98, 73)
        Me.cboCant.Name = "cboCant"
        Me.cboCant.Size = New System.Drawing.Size(121, 21)
        Me.cboCant.TabIndex = 7
        '
        'Dcastigo
        '
        Me.Dcastigo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Dcastigo.Location = New System.Drawing.Point(98, 108)
        Me.Dcastigo.Name = "Dcastigo"
        Me.Dcastigo.Size = New System.Drawing.Size(121, 20)
        Me.Dcastigo.TabIndex = 8
        '
        'Tvalor
        '
        Me.Tvalor.Location = New System.Drawing.Point(347, 76)
        Me.Tvalor.Name = "Tvalor"
        Me.Tvalor.Size = New System.Drawing.Size(100, 20)
        Me.Tvalor.TabIndex = 9
        '
        'btn_consulta
        '
        Me.btn_consulta.Location = New System.Drawing.Point(499, 26)
        Me.btn_consulta.Name = "btn_consulta"
        Me.btn_consulta.Size = New System.Drawing.Size(61, 32)
        Me.btn_consulta.TabIndex = 12
        Me.btn_consulta.Text = "Buscar"
        Me.btn_consulta.UseVisualStyleBackColor = True
        '
        'CheckF
        '
        Me.CheckF.AutoSize = True
        Me.CheckF.Checked = True
        Me.CheckF.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckF.Enabled = False
        Me.CheckF.Location = New System.Drawing.Point(484, 79)
        Me.CheckF.Name = "CheckF"
        Me.CheckF.Size = New System.Drawing.Size(61, 17)
        Me.CheckF.TabIndex = 13
        Me.CheckF.Text = "Financ."
        Me.CheckF.UseVisualStyleBackColor = True
        '
        'CheckT
        '
        Me.CheckT.AutoSize = True
        Me.CheckT.Checked = True
        Me.CheckT.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckT.Location = New System.Drawing.Point(484, 102)
        Me.CheckT.Name = "CheckT"
        Me.CheckT.Size = New System.Drawing.Size(56, 17)
        Me.CheckT.TabIndex = 14
        Me.CheckT.Text = "Tribut."
        Me.CheckT.UseVisualStyleBackColor = True
        '
        'CheckI
        '
        Me.CheckI.AutoSize = True
        Me.CheckI.Checked = True
        Me.CheckI.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckI.Enabled = False
        Me.CheckI.Location = New System.Drawing.Point(484, 125)
        Me.CheckI.Name = "CheckI"
        Me.CheckI.Size = New System.Drawing.Size(50, 17)
        Me.CheckI.TabIndex = 15
        Me.CheckI.Text = "IFRS"
        Me.CheckI.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(29, 147)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(138, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Articulos listos para Castigar"
        '
        'lista_castigar
        '
        Me.lista_castigar.AllowUserToAddRows = False
        Me.lista_castigar.AllowUserToDeleteRows = False
        Me.lista_castigar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.lista_castigar.Location = New System.Drawing.Point(15, 163)
        Me.lista_castigar.MultiSelect = False
        Me.lista_castigar.Name = "lista_castigar"
        Me.lista_castigar.ReadOnly = True
        Me.lista_castigar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.lista_castigar.Size = New System.Drawing.Size(545, 124)
        Me.lista_castigar.TabIndex = 17
        '
        'btn_remove
        '
        Me.btn_remove.Image = Global.AFN.My.Resources.Resources.remove
        Me.btn_remove.Location = New System.Drawing.Point(374, 108)
        Me.btn_remove.Name = "btn_remove"
        Me.btn_remove.Size = New System.Drawing.Size(32, 36)
        Me.btn_remove.TabIndex = 11
        Me.btn_remove.TabStop = False
        '
        'btn_add
        '
        Me.btn_add.Image = Global.AFN.My.Resources.Resources.Add
        Me.btn_add.Location = New System.Drawing.Point(320, 108)
        Me.btn_add.Name = "btn_add"
        Me.btn_add.Size = New System.Drawing.Size(35, 36)
        Me.btn_add.TabIndex = 10
        Me.btn_add.TabStop = False
        '
        'btn_fin
        '
        Me.btn_fin.Location = New System.Drawing.Point(252, 300)
        Me.btn_fin.Name = "btn_fin"
        Me.btn_fin.Size = New System.Drawing.Size(76, 31)
        Me.btn_fin.TabIndex = 18
        Me.btn_fin.Text = "Guardar"
        Me.btn_fin.UseVisualStyleBackColor = True
        '
        'rowindx
        '
        Me.rowindx.Location = New System.Drawing.Point(47, 300)
        Me.rowindx.Name = "rowindx"
        Me.rowindx.Size = New System.Drawing.Size(23, 20)
        Me.rowindx.TabIndex = 19
        Me.rowindx.Visible = False
        '
        'zona_art
        '
        Me.zona_art.Location = New System.Drawing.Point(76, 300)
        Me.zona_art.Name = "zona_art"
        Me.zona_art.Size = New System.Drawing.Size(25, 20)
        Me.zona_art.TabIndex = 20
        Me.zona_art.Visible = False
        '
        'Gparte
        '
        Me.Gparte.Location = New System.Drawing.Point(107, 300)
        Me.Gparte.Name = "Gparte"
        Me.Gparte.Size = New System.Drawing.Size(25, 20)
        Me.Gparte.TabIndex = 21
        Me.Gparte.Visible = False
        '
        'btn_detalle_cantidad
        '
        Me.btn_detalle_cantidad.Image = Global.AFN.My.Resources.Resources.eject
        Me.btn_detalle_cantidad.Location = New System.Drawing.Point(225, 72)
        Me.btn_detalle_cantidad.Name = "btn_detalle_cantidad"
        Me.btn_detalle_cantidad.Size = New System.Drawing.Size(25, 24)
        Me.btn_detalle_cantidad.TabIndex = 22
        Me.btn_detalle_cantidad.TabStop = False
        '
        'form_castigo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(575, 339)
        Me.Controls.Add(Me.btn_detalle_cantidad)
        Me.Controls.Add(Me.Gparte)
        Me.Controls.Add(Me.zona_art)
        Me.Controls.Add(Me.rowindx)
        Me.Controls.Add(Me.btn_fin)
        Me.Controls.Add(Me.lista_castigar)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.CheckI)
        Me.Controls.Add(Me.CheckT)
        Me.Controls.Add(Me.CheckF)
        Me.Controls.Add(Me.btn_consulta)
        Me.Controls.Add(Me.btn_remove)
        Me.Controls.Add(Me.btn_add)
        Me.Controls.Add(Me.Tvalor)
        Me.Controls.Add(Me.Dcastigo)
        Me.Controls.Add(Me.cboCant)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Tarticulo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cod_art)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "form_castigo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Castigo de Activo Fijo"
        CType(Me.lista_castigar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_remove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_add, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_detalle_cantidad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cod_art As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Tarticulo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboCant As System.Windows.Forms.ComboBox
    Friend WithEvents Dcastigo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Tvalor As System.Windows.Forms.TextBox
    Friend WithEvents btn_add As System.Windows.Forms.PictureBox
    Friend WithEvents btn_remove As System.Windows.Forms.PictureBox
    Friend WithEvents btn_consulta As System.Windows.Forms.Button
    Friend WithEvents CheckF As System.Windows.Forms.CheckBox
    Friend WithEvents CheckT As System.Windows.Forms.CheckBox
    Friend WithEvents CheckI As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lista_castigar As System.Windows.Forms.DataGridView
    Friend WithEvents btn_fin As System.Windows.Forms.Button
    Friend WithEvents rowindx As System.Windows.Forms.TextBox
    Friend WithEvents zona_art As System.Windows.Forms.TextBox
    Friend WithEvents Gparte As System.Windows.Forms.TextBox
    Friend WithEvents btn_detalle_cantidad As System.Windows.Forms.PictureBox
End Class
