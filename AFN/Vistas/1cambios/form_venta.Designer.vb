<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_venta
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_venta))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cod_art = New System.Windows.Forms.Label()
        Me.Tarticulo = New System.Windows.Forms.TextBox()
        Me.cboCant = New System.Windows.Forms.ComboBox()
        Me.Dventa = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Tvalor = New System.Windows.Forms.TextBox()
        Me.btn_consulta = New System.Windows.Forms.Button()
        Me.btn_add = New System.Windows.Forms.PictureBox()
        Me.btn_remove = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lista_vender = New System.Windows.Forms.DataGridView()
        Me.btn_fin = New System.Windows.Forms.Button()
        Me.zona_art = New System.Windows.Forms.TextBox()
        Me.rowindx = New System.Windows.Forms.TextBox()
        Me.Gparte = New System.Windows.Forms.TextBox()
        CType(Me.btn_add, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_remove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lista_vender, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Seleccione Artículo a Vender"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(46, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Artículo"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(47, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Cantidad"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(47, 111)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Fecha"
        '
        'cod_art
        '
        Me.cod_art.Location = New System.Drawing.Point(192, 9)
        Me.cod_art.Name = "cod_art"
        Me.cod_art.Size = New System.Drawing.Size(108, 13)
        Me.cod_art.TabIndex = 4
        '
        'Tarticulo
        '
        Me.Tarticulo.Location = New System.Drawing.Point(122, 47)
        Me.Tarticulo.Name = "Tarticulo"
        Me.Tarticulo.Size = New System.Drawing.Size(340, 20)
        Me.Tarticulo.TabIndex = 5
        '
        'cboCant
        '
        Me.cboCant.FormattingEnabled = True
        Me.cboCant.Location = New System.Drawing.Point(122, 77)
        Me.cboCant.Name = "cboCant"
        Me.cboCant.Size = New System.Drawing.Size(121, 21)
        Me.cboCant.TabIndex = 6
        '
        'Dventa
        '
        Me.Dventa.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Dventa.Location = New System.Drawing.Point(122, 111)
        Me.Dventa.Name = "Dventa"
        Me.Dventa.Size = New System.Drawing.Size(121, 20)
        Me.Dventa.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(289, 77)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Valor Libro"
        '
        'Tvalor
        '
        Me.Tvalor.Location = New System.Drawing.Point(362, 77)
        Me.Tvalor.Name = "Tvalor"
        Me.Tvalor.Size = New System.Drawing.Size(100, 20)
        Me.Tvalor.TabIndex = 9
        '
        'btn_consulta
        '
        Me.btn_consulta.Location = New System.Drawing.Point(514, 30)
        Me.btn_consulta.Name = "btn_consulta"
        Me.btn_consulta.Size = New System.Drawing.Size(58, 30)
        Me.btn_consulta.TabIndex = 10
        Me.btn_consulta.Text = "Buscar"
        Me.btn_consulta.UseVisualStyleBackColor = True
        '
        'btn_add
        '
        Me.btn_add.Image = Global.AFN.My.Resources.Resources.Add
        Me.btn_add.Location = New System.Drawing.Point(410, 111)
        Me.btn_add.Name = "btn_add"
        Me.btn_add.Size = New System.Drawing.Size(34, 38)
        Me.btn_add.TabIndex = 11
        Me.btn_add.TabStop = False
        '
        'btn_remove
        '
        Me.btn_remove.Image = Global.AFN.My.Resources.Resources.remove
        Me.btn_remove.Location = New System.Drawing.Point(464, 111)
        Me.btn_remove.Name = "btn_remove"
        Me.btn_remove.Size = New System.Drawing.Size(34, 38)
        Me.btn_remove.TabIndex = 12
        Me.btn_remove.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(23, 145)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(135, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Artículos listos para vender"
        '
        'lista_vender
        '
        Me.lista_vender.AllowUserToAddRows = False
        Me.lista_vender.AllowUserToDeleteRows = False
        Me.lista_vender.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.lista_vender.Location = New System.Drawing.Point(15, 161)
        Me.lista_vender.MultiSelect = False
        Me.lista_vender.Name = "lista_vender"
        Me.lista_vender.ReadOnly = True
        Me.lista_vender.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.lista_vender.Size = New System.Drawing.Size(582, 142)
        Me.lista_vender.TabIndex = 14
        '
        'btn_fin
        '
        Me.btn_fin.Location = New System.Drawing.Point(271, 315)
        Me.btn_fin.Name = "btn_fin"
        Me.btn_fin.Size = New System.Drawing.Size(64, 36)
        Me.btn_fin.TabIndex = 15
        Me.btn_fin.Text = "Guardar"
        Me.btn_fin.UseVisualStyleBackColor = True
        '
        'zona_art
        '
        Me.zona_art.Location = New System.Drawing.Point(90, 331)
        Me.zona_art.Name = "zona_art"
        Me.zona_art.Size = New System.Drawing.Size(10, 20)
        Me.zona_art.TabIndex = 16
        Me.zona_art.Visible = False
        '
        'rowindx
        '
        Me.rowindx.Location = New System.Drawing.Point(106, 331)
        Me.rowindx.Name = "rowindx"
        Me.rowindx.Size = New System.Drawing.Size(10, 20)
        Me.rowindx.TabIndex = 17
        Me.rowindx.Visible = False
        '
        'Gparte
        '
        Me.Gparte.Location = New System.Drawing.Point(122, 331)
        Me.Gparte.Name = "Gparte"
        Me.Gparte.Size = New System.Drawing.Size(10, 20)
        Me.Gparte.TabIndex = 18
        Me.Gparte.Visible = False
        '
        'form_venta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(609, 363)
        Me.Controls.Add(Me.Gparte)
        Me.Controls.Add(Me.rowindx)
        Me.Controls.Add(Me.zona_art)
        Me.Controls.Add(Me.btn_fin)
        Me.Controls.Add(Me.lista_vender)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btn_remove)
        Me.Controls.Add(Me.btn_add)
        Me.Controls.Add(Me.btn_consulta)
        Me.Controls.Add(Me.Tvalor)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Dventa)
        Me.Controls.Add(Me.cboCant)
        Me.Controls.Add(Me.Tarticulo)
        Me.Controls.Add(Me.cod_art)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "form_venta"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Venta de Activo Fijo"
        CType(Me.btn_add, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_remove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lista_vender, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cod_art As System.Windows.Forms.Label
    Friend WithEvents Tarticulo As System.Windows.Forms.TextBox
    Friend WithEvents cboCant As System.Windows.Forms.ComboBox
    Friend WithEvents Dventa As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Tvalor As System.Windows.Forms.TextBox
    Friend WithEvents btn_consulta As System.Windows.Forms.Button
    Friend WithEvents btn_add As System.Windows.Forms.PictureBox
    Friend WithEvents btn_remove As System.Windows.Forms.PictureBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lista_vender As System.Windows.Forms.DataGridView
    Friend WithEvents btn_fin As System.Windows.Forms.Button
    Friend WithEvents zona_art As System.Windows.Forms.TextBox
    Friend WithEvents rowindx As System.Windows.Forms.TextBox
    Friend WithEvents Gparte As System.Windows.Forms.TextBox
End Class
