<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProgressShow
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
        Me.barra1 = New System.Windows.Forms.ProgressBar()
        Me.texto1 = New System.Windows.Forms.Label()
        Me.porcent = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'barra1
        '
        Me.barra1.BackColor = System.Drawing.Color.White
        Me.barra1.Location = New System.Drawing.Point(13, 24)
        Me.barra1.Name = "barra1"
        Me.barra1.Size = New System.Drawing.Size(217, 23)
        Me.barra1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.barra1.TabIndex = 0
        '
        'texto1
        '
        Me.texto1.AutoSize = True
        Me.texto1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.texto1.Location = New System.Drawing.Point(47, 5)
        Me.texto1.Name = "texto1"
        Me.texto1.Size = New System.Drawing.Size(97, 14)
        Me.texto1.TabIndex = 1
        Me.texto1.Text = "Estado Avance"
        '
        'porcent
        '
        Me.porcent.AutoSize = True
        Me.porcent.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.porcent.Location = New System.Drawing.Point(161, 5)
        Me.porcent.Name = "porcent"
        Me.porcent.Size = New System.Drawing.Size(41, 14)
        Me.porcent.TabIndex = 2
        Me.porcent.Text = "0.0%"
        '
        'ProgressShow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Controls.Add(Me.porcent)
        Me.Controls.Add(Me.texto1)
        Me.Controls.Add(Me.barra1)
        Me.Enabled = False
        Me.Name = "ProgressShow"
        Me.Size = New System.Drawing.Size(241, 56)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents barra1 As System.Windows.Forms.ProgressBar
    Friend WithEvents texto1 As System.Windows.Forms.Label
    Friend WithEvents porcent As System.Windows.Forms.Label

End Class
