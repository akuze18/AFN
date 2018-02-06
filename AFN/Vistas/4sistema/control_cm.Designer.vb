<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class control_cm
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
        Me.display = New System.Windows.Forms.Label()
        Me.valor = New System.Windows.Forms.TextBox()
        Me.icono = New System.Windows.Forms.PictureBox()
        CType(Me.icono, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'display
        '
        Me.display.AutoSize = True
        Me.display.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.display.Location = New System.Drawing.Point(115, 3)
        Me.display.Name = "display"
        Me.display.Size = New System.Drawing.Size(20, 16)
        Me.display.TabIndex = 10
        Me.display.Text = "%"
        '
        'valor
        '
        Me.valor.Location = New System.Drawing.Point(35, 2)
        Me.valor.Name = "valor"
        Me.valor.Size = New System.Drawing.Size(80, 20)
        Me.valor.TabIndex = 9
        '
        'icono
        '
        Me.icono.Location = New System.Drawing.Point(2, 2)
        Me.icono.Name = "icono"
        Me.icono.Size = New System.Drawing.Size(26, 20)
        Me.icono.TabIndex = 8
        Me.icono.TabStop = False
        '
        'control_cm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.display)
        Me.Controls.Add(Me.valor)
        Me.Controls.Add(Me.icono)
        Me.Name = "control_cm"
        Me.Size = New System.Drawing.Size(180, 34)
        CType(Me.icono, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents display As System.Windows.Forms.Label
    Friend WithEvents valor As System.Windows.Forms.TextBox
    Friend WithEvents icono As System.Windows.Forms.PictureBox

End Class
