<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_CM
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_CM))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboPeriodo = New System.Windows.Forms.ComboBox()
        Me.CheckOpen = New System.Windows.Forms.CheckBox()
        Me.Texistia = New System.Windows.Forms.CheckBox()
        Me.btn_guardar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(59, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'cboPeriodo
        '
        Me.cboPeriodo.FormattingEnabled = True
        Me.cboPeriodo.Location = New System.Drawing.Point(121, 34)
        Me.cboPeriodo.Name = "cboPeriodo"
        Me.cboPeriodo.Size = New System.Drawing.Size(197, 21)
        Me.cboPeriodo.TabIndex = 1
        '
        'CheckOpen
        '
        Me.CheckOpen.AutoSize = True
        Me.CheckOpen.Location = New System.Drawing.Point(384, 37)
        Me.CheckOpen.Name = "CheckOpen"
        Me.CheckOpen.Size = New System.Drawing.Size(81, 17)
        Me.CheckOpen.TabIndex = 2
        Me.CheckOpen.Text = "CheckBox1"
        Me.CheckOpen.UseVisualStyleBackColor = True
        '
        'Texistia
        '
        Me.Texistia.AutoSize = True
        Me.Texistia.Location = New System.Drawing.Point(36, 70)
        Me.Texistia.Name = "Texistia"
        Me.Texistia.Size = New System.Drawing.Size(81, 17)
        Me.Texistia.TabIndex = 3
        Me.Texistia.Text = "CheckBox2"
        Me.Texistia.UseVisualStyleBackColor = True
        '
        'btn_guardar
        '
        Me.btn_guardar.Location = New System.Drawing.Point(256, 251)
        Me.btn_guardar.Name = "btn_guardar"
        Me.btn_guardar.Size = New System.Drawing.Size(73, 33)
        Me.btn_guardar.TabIndex = 4
        Me.btn_guardar.Text = "Button1"
        Me.btn_guardar.UseVisualStyleBackColor = True
        '
        'form_CM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 307)
        Me.Controls.Add(Me.btn_guardar)
        Me.Controls.Add(Me.Texistia)
        Me.Controls.Add(Me.CheckOpen)
        Me.Controls.Add(Me.cboPeriodo)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(600, 345)
        Me.Name = "form_CM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mantenedor de Corrección Monetaria"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboPeriodo As System.Windows.Forms.ComboBox
    Friend WithEvents CheckOpen As System.Windows.Forms.CheckBox
    Friend WithEvents Texistia As System.Windows.Forms.CheckBox
    Friend WithEvents btn_guardar As System.Windows.Forms.Button
End Class
