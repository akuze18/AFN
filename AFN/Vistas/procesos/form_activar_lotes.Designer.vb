<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_activar_lotes
    Inherits Global.AFN.MainForm

    'Form invalida a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DGLotes = New System.Windows.Forms.DataGridView()
        Me.btn_act_all = New System.Windows.Forms.Button()
        Me.btn_act_sel = New System.Windows.Forms.Button()
        CType(Me.DGLotes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 19)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Lotes por Activar"
        '
        'DGLotes
        '
        Me.DGLotes.AllowUserToAddRows = False
        Me.DGLotes.AllowUserToDeleteRows = False
        Me.DGLotes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGLotes.Location = New System.Drawing.Point(27, 52)
        Me.DGLotes.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DGLotes.Name = "DGLotes"
        Me.DGLotes.ReadOnly = True
        Me.DGLotes.Size = New System.Drawing.Size(752, 220)
        Me.DGLotes.TabIndex = 1
        '
        'btn_act_all
        '
        Me.btn_act_all.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_act_all.Location = New System.Drawing.Point(128, 307)
        Me.btn_act_all.Name = "btn_act_all"
        Me.btn_act_all.Size = New System.Drawing.Size(100, 50)
        Me.btn_act_all.TabIndex = 2
        Me.btn_act_all.Text = "Activar Todo"
        Me.btn_act_all.UseVisualStyleBackColor = True
        '
        'btn_act_sel
        '
        Me.btn_act_sel.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_act_sel.Location = New System.Drawing.Point(265, 307)
        Me.btn_act_sel.Name = "btn_act_sel"
        Me.btn_act_sel.Size = New System.Drawing.Size(94, 50)
        Me.btn_act_sel.TabIndex = 3
        Me.btn_act_sel.Text = "Activar Seleccionado"
        Me.btn_act_sel.UseVisualStyleBackColor = True
        '
        'form_activar_lotes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.ClientSize = New System.Drawing.Size(829, 456)
        Me.Controls.Add(Me.btn_act_sel)
        Me.Controls.Add(Me.btn_act_all)
        Me.Controls.Add(Me.DGLotes)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "form_activar_lotes"
        Me.Text = "Activar Lotes"
        CType(Me.DGLotes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DGLotes As System.Windows.Forms.DataGridView
    Friend WithEvents btn_act_all As System.Windows.Forms.Button
    Friend WithEvents btn_act_sel As System.Windows.Forms.Button

End Class
