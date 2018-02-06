<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_contab
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_contab))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dialogo = New System.Windows.Forms.SaveFileDialog()
        Me.Tubicacion = New System.Windows.Forms.TextBox()
        Me.cb_fecha = New System.Windows.Forms.ComboBox()
        Me.btn_calcular = New System.Windows.Forms.Button()
        Me.btn_ubicar = New System.Windows.Forms.Button()
        Me.BGQuery = New System.ComponentModel.BackgroundWorker()
        Me.SuspendLayout
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.Location = New System.Drawing.Point(31, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Fecha"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.Location = New System.Drawing.Point(31, 99)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Ubicacion"
        '
        'Tubicacion
        '
        Me.Tubicacion.Location = New System.Drawing.Point(106, 99)
        Me.Tubicacion.Name = "Tubicacion"
        Me.Tubicacion.Size = New System.Drawing.Size(215, 20)
        Me.Tubicacion.TabIndex = 2
        '
        'cb_fecha
        '
        Me.cb_fecha.DropDownHeight = 93
        Me.cb_fecha.FormattingEnabled = true
        Me.cb_fecha.IntegralHeight = false
        Me.cb_fecha.Location = New System.Drawing.Point(106, 52)
        Me.cb_fecha.MaxDropDownItems = 7
        Me.cb_fecha.Name = "cb_fecha"
        Me.cb_fecha.Size = New System.Drawing.Size(189, 21)
        Me.cb_fecha.TabIndex = 3
        '
        'btn_calcular
        '
        Me.btn_calcular.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btn_calcular.Location = New System.Drawing.Point(154, 150)
        Me.btn_calcular.Name = "btn_calcular"
        Me.btn_calcular.Size = New System.Drawing.Size(72, 45)
        Me.btn_calcular.TabIndex = 5
        Me.btn_calcular.Text = "Generar"
        Me.btn_calcular.UseVisualStyleBackColor = true
        '
        'btn_ubicar
        '
        Me.btn_ubicar.Image = Global.AFN.My.Resources.Resources.find
        Me.btn_ubicar.Location = New System.Drawing.Point(327, 92)
        Me.btn_ubicar.Name = "btn_ubicar"
        Me.btn_ubicar.Size = New System.Drawing.Size(34, 32)
        Me.btn_ubicar.TabIndex = 4
        Me.btn_ubicar.UseVisualStyleBackColor = true
        '
        'BGQuery
        '
        '
        'form_contab
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(395, 218)
        Me.Controls.Add(Me.btn_calcular)
        Me.Controls.Add(Me.btn_ubicar)
        Me.Controls.Add(Me.cb_fecha)
        Me.Controls.Add(Me.Tubicacion)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "form_contab"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Contabilizar Activo Fijo"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dialogo As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Tubicacion As System.Windows.Forms.TextBox
    Friend WithEvents cb_fecha As System.Windows.Forms.ComboBox
    Friend WithEvents btn_ubicar As System.Windows.Forms.Button
    Friend WithEvents btn_calcular As System.Windows.Forms.Button
    Friend WithEvents BGQuery As System.ComponentModel.BackgroundWorker
End Class
