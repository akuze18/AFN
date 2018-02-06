<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class multicriterio
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
        Me._Label1 = New System.Windows.Forms.Label()
        Me.cbCampo = New System.Windows.Forms.ComboBox()
        Me._Label2 = New System.Windows.Forms.Label()
        Me.cbFiltro = New System.Windows.Forms.ComboBox()
        Me._Label3 = New System.Windows.Forms.Label()
        Me.Valor1txt = New System.Windows.Forms.TextBox()
        Me.Valor1dat = New System.Windows.Forms.DateTimePicker()
        Me.Valor1cbo = New System.Windows.Forms.ComboBox()
        Me.Valor2txt = New System.Windows.Forms.TextBox()
        Me.Valor2dat = New System.Windows.Forms.DateTimePicker()
        Me.SuspendLayout()
        '
        '_Label1
        '
        Me._Label1.AutoSize = True
        Me._Label1.Location = New System.Drawing.Point(3, 0)
        Me._Label1.Name = "_Label1"
        Me._Label1.Size = New System.Drawing.Size(95, 13)
        Me._Label1.TabIndex = 0
        Me._Label1.Text = "Nombre de Campo"
        '
        'cbCampo
        '
        Me.cbCampo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCampo.FormattingEnabled = True
        Me.cbCampo.Location = New System.Drawing.Point(3, 16)
        Me.cbCampo.Name = "cbCampo"
        Me.cbCampo.Size = New System.Drawing.Size(165, 21)
        Me.cbCampo.TabIndex = 1
        '
        '_Label2
        '
        Me._Label2.AutoSize = True
        Me._Label2.Location = New System.Drawing.Point(186, 0)
        Me._Label2.Name = "_Label2"
        Me._Label2.Size = New System.Drawing.Size(29, 13)
        Me._Label2.TabIndex = 2
        Me._Label2.Text = "Filtro"
        '
        'cbFiltro
        '
        Me.cbFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFiltro.FormattingEnabled = True
        Me.cbFiltro.Location = New System.Drawing.Point(189, 16)
        Me.cbFiltro.Name = "cbFiltro"
        Me.cbFiltro.Size = New System.Drawing.Size(143, 21)
        Me.cbFiltro.TabIndex = 3
        '
        '_Label3
        '
        Me._Label3.AutoSize = True
        Me._Label3.Location = New System.Drawing.Point(353, 1)
        Me._Label3.Name = "_Label3"
        Me._Label3.Size = New System.Drawing.Size(31, 13)
        Me._Label3.TabIndex = 4
        Me._Label3.Text = "Valor"
        '
        'Valor1txt
        '
        Me.Valor1txt.Location = New System.Drawing.Point(355, 16)
        Me.Valor1txt.Name = "Valor1txt"
        Me.Valor1txt.Size = New System.Drawing.Size(119, 20)
        Me.Valor1txt.TabIndex = 5
        '
        'Valor1dat
        '
        Me.Valor1dat.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Valor1dat.Location = New System.Drawing.Point(355, 16)
        Me.Valor1dat.Name = "Valor1dat"
        Me.Valor1dat.Size = New System.Drawing.Size(118, 20)
        Me.Valor1dat.TabIndex = 7
        Me.Valor1dat.Visible = False
        '
        'Valor1cbo
        '
        Me.Valor1cbo.DisplayMember = "txt"
        Me.Valor1cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Valor1cbo.FormattingEnabled = True
        Me.Valor1cbo.Location = New System.Drawing.Point(355, 16)
        Me.Valor1cbo.Name = "Valor1cbo"
        Me.Valor1cbo.Size = New System.Drawing.Size(265, 21)
        Me.Valor1cbo.TabIndex = 6
        Me.Valor1cbo.ValueMember = "cod"
        Me.Valor1cbo.Visible = False
        '
        'Valor2txt
        '
        Me.Valor2txt.Location = New System.Drawing.Point(502, 16)
        Me.Valor2txt.Name = "Valor2txt"
        Me.Valor2txt.Size = New System.Drawing.Size(119, 20)
        Me.Valor2txt.TabIndex = 8
        '
        'Valor2dat
        '
        Me.Valor2dat.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Valor2dat.Location = New System.Drawing.Point(502, 16)
        Me.Valor2dat.Name = "Valor2dat"
        Me.Valor2dat.Size = New System.Drawing.Size(118, 20)
        Me.Valor2dat.TabIndex = 10
        Me.Valor2dat.Visible = False
        '
        'multicriterio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Valor2dat)
        Me.Controls.Add(Me.Valor2txt)
        Me.Controls.Add(Me.Valor1dat)
        Me.Controls.Add(Me.Valor1cbo)
        Me.Controls.Add(Me.Valor1txt)
        Me.Controls.Add(Me._Label3)
        Me.Controls.Add(Me.cbFiltro)
        Me.Controls.Add(Me._Label2)
        Me.Controls.Add(Me.cbCampo)
        Me.Controls.Add(Me._Label1)
        Me.Name = "multicriterio"
        Me.Size = New System.Drawing.Size(630, 43)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents _Label1 As System.Windows.Forms.Label
    Friend WithEvents cbCampo As System.Windows.Forms.ComboBox
    Friend WithEvents _Label2 As System.Windows.Forms.Label
    Friend WithEvents cbFiltro As System.Windows.Forms.ComboBox
    Friend WithEvents _Label3 As System.Windows.Forms.Label
    Friend WithEvents Valor1txt As System.Windows.Forms.TextBox
    Friend WithEvents Valor1dat As System.Windows.Forms.DateTimePicker
    Friend WithEvents Valor1cbo As System.Windows.Forms.ComboBox
    Friend WithEvents Valor2txt As System.Windows.Forms.TextBox
    Friend WithEvents Valor2dat As System.Windows.Forms.DateTimePicker

End Class
