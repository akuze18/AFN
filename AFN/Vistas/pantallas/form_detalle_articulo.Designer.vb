<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_detalle_articulo
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TB_cod_lote = New System.Windows.Forms.TextBox()
        Me.DG_articulos = New System.Windows.Forms.DataGridView()
        Me.LBdescrip = New System.Windows.Forms.Label()
        Me.btn_guardar = New System.Windows.Forms.Button()
        Me.btn_less = New System.Windows.Forms.Button()
        Me.btn_top = New System.Windows.Forms.Button()
        Me.mark_actual = New System.Windows.Forms.Label()
        Me.mark_total = New System.Windows.Forms.Label()
        Me.RBclear = New System.Windows.Forms.RadioButton()
        Me.btn_clear = New System.Windows.Forms.Button()
        CType(Me.DG_articulos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Código de Lote"
        '
        'TB_cod_lote
        '
        Me.TB_cod_lote.Location = New System.Drawing.Point(110, 6)
        Me.TB_cod_lote.Name = "TB_cod_lote"
        Me.TB_cod_lote.Size = New System.Drawing.Size(95, 20)
        Me.TB_cod_lote.TabIndex = 1
        '
        'DG_articulos
        '
        Me.DG_articulos.AllowUserToAddRows = False
        Me.DG_articulos.AllowUserToDeleteRows = False
        Me.DG_articulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG_articulos.Location = New System.Drawing.Point(28, 114)
        Me.DG_articulos.Name = "DG_articulos"
        Me.DG_articulos.ReadOnly = True
        Me.DG_articulos.Size = New System.Drawing.Size(375, 419)
        Me.DG_articulos.TabIndex = 9
        '
        'LBdescrip
        '
        Me.LBdescrip.Location = New System.Drawing.Point(12, 35)
        Me.LBdescrip.Name = "LBdescrip"
        Me.LBdescrip.Size = New System.Drawing.Size(290, 35)
        Me.LBdescrip.TabIndex = 2
        Me.LBdescrip.Text = "Label2"
        '
        'btn_guardar
        '
        Me.btn_guardar.Location = New System.Drawing.Point(308, 35)
        Me.btn_guardar.Name = "btn_guardar"
        Me.btn_guardar.Size = New System.Drawing.Size(115, 23)
        Me.btn_guardar.TabIndex = 6
        Me.btn_guardar.Text = "Guardar"
        Me.btn_guardar.UseVisualStyleBackColor = True
        '
        'btn_less
        '
        Me.btn_less.Location = New System.Drawing.Point(308, 6)
        Me.btn_less.Name = "btn_less"
        Me.btn_less.Size = New System.Drawing.Size(55, 23)
        Me.btn_less.TabIndex = 4
        Me.btn_less.Text = "Button2"
        Me.btn_less.UseVisualStyleBackColor = True
        '
        'btn_top
        '
        Me.btn_top.Location = New System.Drawing.Point(368, 6)
        Me.btn_top.Name = "btn_top"
        Me.btn_top.Size = New System.Drawing.Size(55, 23)
        Me.btn_top.TabIndex = 5
        Me.btn_top.Text = "Button3"
        Me.btn_top.UseVisualStyleBackColor = True
        '
        'mark_actual
        '
        Me.mark_actual.AutoSize = True
        Me.mark_actual.Location = New System.Drawing.Point(305, 78)
        Me.mark_actual.Name = "mark_actual"
        Me.mark_actual.Size = New System.Drawing.Size(39, 13)
        Me.mark_actual.TabIndex = 7
        Me.mark_actual.Text = "Label2"
        '
        'mark_total
        '
        Me.mark_total.AutoSize = True
        Me.mark_total.Location = New System.Drawing.Point(364, 78)
        Me.mark_total.Name = "mark_total"
        Me.mark_total.Size = New System.Drawing.Size(39, 13)
        Me.mark_total.TabIndex = 8
        Me.mark_total.Text = "Label3"
        '
        'RBclear
        '
        Me.RBclear.AutoSize = True
        Me.RBclear.Location = New System.Drawing.Point(277, 201)
        Me.RBclear.Name = "RBclear"
        Me.RBclear.Size = New System.Drawing.Size(25, 17)
        Me.RBclear.TabIndex = 10
        Me.RBclear.TabStop = True
        Me.RBclear.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.RBclear.UseVisualStyleBackColor = True
        '
        'btn_clear
        '
        Me.btn_clear.Location = New System.Drawing.Point(247, 6)
        Me.btn_clear.Name = "btn_clear"
        Me.btn_clear.Size = New System.Drawing.Size(55, 23)
        Me.btn_clear.TabIndex = 3
        Me.btn_clear.Text = "Button1"
        Me.btn_clear.UseVisualStyleBackColor = True
        '
        'form_detalle_articulo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 555)
        Me.Controls.Add(Me.btn_clear)
        Me.Controls.Add(Me.mark_total)
        Me.Controls.Add(Me.mark_actual)
        Me.Controls.Add(Me.btn_top)
        Me.Controls.Add(Me.btn_less)
        Me.Controls.Add(Me.btn_guardar)
        Me.Controls.Add(Me.LBdescrip)
        Me.Controls.Add(Me.DG_articulos)
        Me.Controls.Add(Me.TB_cod_lote)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RBclear)
        Me.Name = "form_detalle_articulo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "form_detalle_articulo"
        CType(Me.DG_articulos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TB_cod_lote As System.Windows.Forms.TextBox
    Friend WithEvents DG_articulos As System.Windows.Forms.DataGridView
    Friend WithEvents LBdescrip As System.Windows.Forms.Label
    Friend WithEvents btn_guardar As System.Windows.Forms.Button
    Friend WithEvents btn_less As System.Windows.Forms.Button
    Friend WithEvents btn_top As System.Windows.Forms.Button
    Friend WithEvents mark_actual As System.Windows.Forms.Label
    Friend WithEvents mark_total As System.Windows.Forms.Label
    Friend WithEvents RBclear As System.Windows.Forms.RadioButton
    Friend WithEvents btn_clear As System.Windows.Forms.Button
End Class
