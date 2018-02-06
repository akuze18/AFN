<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_cuenta_cont
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_cuenta_cont))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbTCuenta = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbClase = New System.Windows.Forms.ComboBox()
        Me.dgClasificacion = New System.Windows.Forms.DataGridView()
        Me.ckClasif = New System.Windows.Forms.CheckBox()
        Me.btn_guardar = New System.Windows.Forms.Button()
        CType(Me.dgClasificacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Tipo de Cuenta"
        '
        'cbTCuenta
        '
        Me.cbTCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTCuenta.FormattingEnabled = True
        Me.cbTCuenta.Location = New System.Drawing.Point(118, 28)
        Me.cbTCuenta.Name = "cbTCuenta"
        Me.cbTCuenta.Size = New System.Drawing.Size(163, 21)
        Me.cbTCuenta.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(324, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Clase"
        '
        'cbClase
        '
        Me.cbClase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbClase.FormattingEnabled = True
        Me.cbClase.Location = New System.Drawing.Point(396, 28)
        Me.cbClase.Name = "cbClase"
        Me.cbClase.Size = New System.Drawing.Size(184, 21)
        Me.cbClase.TabIndex = 3
        '
        'dgClasificacion
        '
        Me.dgClasificacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgClasificacion.Location = New System.Drawing.Point(12, 115)
        Me.dgClasificacion.Name = "dgClasificacion"
        Me.dgClasificacion.Size = New System.Drawing.Size(812, 314)
        Me.dgClasificacion.TabIndex = 8
        '
        'ckClasif
        '
        Me.ckClasif.AutoSize = True
        Me.ckClasif.Location = New System.Drawing.Point(647, 77)
        Me.ckClasif.Name = "ckClasif"
        Me.ckClasif.Size = New System.Drawing.Size(81, 17)
        Me.ckClasif.TabIndex = 9
        Me.ckClasif.Text = "CheckBox1"
        Me.ckClasif.UseVisualStyleBackColor = True
        '
        'btn_guardar
        '
        Me.btn_guardar.Location = New System.Drawing.Point(637, 28)
        Me.btn_guardar.Name = "btn_guardar"
        Me.btn_guardar.Size = New System.Drawing.Size(107, 23)
        Me.btn_guardar.TabIndex = 10
        Me.btn_guardar.Text = "Guardar"
        Me.btn_guardar.UseVisualStyleBackColor = True
        '
        'form_cuenta_cont
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(839, 472)
        Me.Controls.Add(Me.btn_guardar)
        Me.Controls.Add(Me.ckClasif)
        Me.Controls.Add(Me.dgClasificacion)
        Me.Controls.Add(Me.cbClase)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbTCuenta)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "form_cuenta_cont"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Asignar cuentas contables en el Activo Fijo"
        CType(Me.dgClasificacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbTCuenta As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbClase As System.Windows.Forms.ComboBox
    Friend WithEvents dgClasificacion As System.Windows.Forms.DataGridView
    Friend WithEvents ckClasif As System.Windows.Forms.CheckBox
    Friend WithEvents btn_guardar As System.Windows.Forms.Button
End Class
