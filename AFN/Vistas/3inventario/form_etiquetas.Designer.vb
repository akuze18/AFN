<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_etiquetas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_etiquetas))
        Me.btn_imprimir = New System.Windows.Forms.Button()
        Me.dgv_resultado = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSelImpresora = New System.Windows.Forms.Button()
        Me.btn_find = New System.Windows.Forms.Button()
        Me.btn_less = New System.Windows.Forms.Button()
        Me.btn_add = New System.Windows.Forms.Button()
        Me.chkSelAntes = New System.Windows.Forms.CheckBox()
        Me.Multicriterio1 = New Global.AFN.multicriterio()
        CType(Me.dgv_resultado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_imprimir
        '
        Me.btn_imprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_imprimir.Location = New System.Drawing.Point(266, 470)
        Me.btn_imprimir.Name = "btn_imprimir"
        Me.btn_imprimir.Size = New System.Drawing.Size(93, 35)
        Me.btn_imprimir.TabIndex = 19
        Me.btn_imprimir.Text = "Imprimir"
        Me.btn_imprimir.UseVisualStyleBackColor = True
        '
        'dgv_resultado
        '
        Me.dgv_resultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_resultado.Location = New System.Drawing.Point(12, 226)
        Me.dgv_resultado.Name = "dgv_resultado"
        Me.dgv_resultado.Size = New System.Drawing.Size(783, 218)
        Me.dgv_resultado.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(59, 208)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(158, 15)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Resultado de la Busqueda :"
        '
        'btnSelImpresora
        '
        Me.btnSelImpresora.BackgroundImage = Global.AFN.My.Resources.Resources.imprimir
        Me.btnSelImpresora.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSelImpresora.Location = New System.Drawing.Point(584, 465)
        Me.btnSelImpresora.Name = "btnSelImpresora"
        Me.btnSelImpresora.Size = New System.Drawing.Size(42, 40)
        Me.btnSelImpresora.TabIndex = 28
        Me.btnSelImpresora.UseVisualStyleBackColor = True
        '
        'btn_find
        '
        Me.btn_find.Image = Global.AFN.My.Resources.Resources.find
        Me.btn_find.Location = New System.Drawing.Point(675, 12)
        Me.btn_find.Name = "btn_find"
        Me.btn_find.Size = New System.Drawing.Size(33, 43)
        Me.btn_find.TabIndex = 25
        Me.btn_find.UseVisualStyleBackColor = True
        '
        'btn_less
        '
        Me.btn_less.Image = Global.AFN.My.Resources.Resources._16__Minus_
        Me.btn_less.Location = New System.Drawing.Point(745, 12)
        Me.btn_less.Name = "btn_less"
        Me.btn_less.Size = New System.Drawing.Size(25, 43)
        Me.btn_less.TabIndex = 24
        Me.btn_less.UseVisualStyleBackColor = True
        '
        'btn_add
        '
        Me.btn_add.Image = Global.AFN.My.Resources.Resources._16__Plus_
        Me.btn_add.Location = New System.Drawing.Point(714, 12)
        Me.btn_add.Name = "btn_add"
        Me.btn_add.Size = New System.Drawing.Size(25, 43)
        Me.btn_add.TabIndex = 23
        Me.btn_add.UseVisualStyleBackColor = True
        '
        'chkSelAntes
        '
        Me.chkSelAntes.Checked = True
        Me.chkSelAntes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSelAntes.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkSelAntes.Location = New System.Drawing.Point(386, 485)
        Me.chkSelAntes.Name = "chkSelAntes"
        Me.chkSelAntes.Size = New System.Drawing.Size(176, 20)
        Me.chkSelAntes.TabIndex = 29
        Me.chkSelAntes.Text = "Seleccionar antes de imprimir"
        '
        'Multicriterio1
        '
        Me.Multicriterio1.Location = New System.Drawing.Point(35, 12)
        Me.Multicriterio1.Name = "Multicriterio1"
        Me.Multicriterio1.Size = New System.Drawing.Size(630, 43)
        Me.Multicriterio1.TabIndex = 22
        '
        'form_etiquetas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(808, 522)
        Me.Controls.Add(Me.chkSelAntes)
        Me.Controls.Add(Me.btnSelImpresora)
        Me.Controls.Add(Me.btn_find)
        Me.Controls.Add(Me.btn_less)
        Me.Controls.Add(Me.btn_add)
        Me.Controls.Add(Me.Multicriterio1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgv_resultado)
        Me.Controls.Add(Me.btn_imprimir)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.KeyPreview = true
        Me.MinimizeBox = false
        Me.MinimumSize = New System.Drawing.Size(824, 560)
        Me.Name = "form_etiquetas"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Imprimir Etiquetas"
        CType(Me.dgv_resultado,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents btn_imprimir As System.Windows.Forms.Button
    Friend WithEvents dgv_resultado As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Multicriterio1 As Global.AFN.multicriterio
    Friend WithEvents btn_add As System.Windows.Forms.Button
    Friend WithEvents btn_less As System.Windows.Forms.Button
    Friend WithEvents btn_find As System.Windows.Forms.Button
    Friend WithEvents btnSelImpresora As System.Windows.Forms.Button
    Friend WithEvents chkSelAntes As System.Windows.Forms.CheckBox
End Class
