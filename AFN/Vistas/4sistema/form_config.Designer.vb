<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_config
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_config))
        Me.TVconfig = New System.Windows.Forms.TreeView()
        Me.TBseccion = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TBkey = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TBvalor = New System.Windows.Forms.TextBox()
        Me.btn_guardar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TVconfig
        '
        Me.TVconfig.Location = New System.Drawing.Point(12, 12)
        Me.TVconfig.Name = "TVconfig"
        Me.TVconfig.Size = New System.Drawing.Size(119, 183)
        Me.TVconfig.TabIndex = 0
        '
        'TBseccion
        '
        Me.TBseccion.Location = New System.Drawing.Point(152, 32)
        Me.TBseccion.Name = "TBseccion"
        Me.TBseccion.Size = New System.Drawing.Size(228, 20)
        Me.TBseccion.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(149, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Seccion"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(149, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Variable"
        '
        'TBkey
        '
        Me.TBkey.Location = New System.Drawing.Point(152, 82)
        Me.TBkey.Name = "TBkey"
        Me.TBkey.Size = New System.Drawing.Size(228, 20)
        Me.TBkey.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(149, 122)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Valor"
        '
        'TBvalor
        '
        Me.TBvalor.Location = New System.Drawing.Point(152, 138)
        Me.TBvalor.Name = "TBvalor"
        Me.TBvalor.Size = New System.Drawing.Size(228, 20)
        Me.TBvalor.TabIndex = 5
        '
        'btn_guardar
        '
        Me.btn_guardar.Location = New System.Drawing.Point(223, 169)
        Me.btn_guardar.Name = "btn_guardar"
        Me.btn_guardar.Size = New System.Drawing.Size(71, 26)
        Me.btn_guardar.TabIndex = 7
        Me.btn_guardar.Text = "Guardar"
        Me.btn_guardar.UseVisualStyleBackColor = True
        '
        'form_config
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 214)
        Me.Controls.Add(Me.btn_guardar)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TBvalor)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TBkey)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TBseccion)
        Me.Controls.Add(Me.TVconfig)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "form_config"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuracion"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TVconfig As System.Windows.Forms.TreeView
    Friend WithEvents TBseccion As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TBkey As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TBvalor As System.Windows.Forms.TextBox
    Friend WithEvents btn_guardar As System.Windows.Forms.Button
End Class
