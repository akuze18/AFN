<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_rep_baja
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_rep_baja))
        Me.cb_desde = New System.Windows.Forms.ComboBox()
        Me.cb_hasta = New System.Windows.Forms.ComboBox()
        Me.cb_situ = New System.Windows.Forms.ComboBox()
        Me.Lbl0 = New System.Windows.Forms.Label()
        Me.Lbl1 = New System.Windows.Forms.Label()
        Me.Lbl2 = New System.Windows.Forms.Label()
        Me.BGworker = New System.ComponentModel.BackgroundWorker()
        Me.lb_reporte = New System.Windows.Forms.ListBox()
        Me.btn_generar = New System.Windows.Forms.Button()
        Me.btn_all = New System.Windows.Forms.Button()
        Me.btn_none = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cb_desde
        '
        Me.cb_desde.DropDownHeight = 65
        Me.cb_desde.FormattingEnabled = True
        Me.cb_desde.IntegralHeight = False
        Me.cb_desde.Location = New System.Drawing.Point(168, 35)
        Me.cb_desde.MaxDropDownItems = 5
        Me.cb_desde.Name = "cb_desde"
        Me.cb_desde.Size = New System.Drawing.Size(230, 21)
        Me.cb_desde.TabIndex = 0
        '
        'cb_hasta
        '
        Me.cb_hasta.DropDownHeight = 65
        Me.cb_hasta.FormattingEnabled = True
        Me.cb_hasta.IntegralHeight = False
        Me.cb_hasta.Location = New System.Drawing.Point(168, 66)
        Me.cb_hasta.MaxDropDownItems = 5
        Me.cb_hasta.Name = "cb_hasta"
        Me.cb_hasta.Size = New System.Drawing.Size(230, 21)
        Me.cb_hasta.TabIndex = 1
        '
        'cb_situ
        '
        Me.cb_situ.DropDownHeight = 65
        Me.cb_situ.FormattingEnabled = True
        Me.cb_situ.IntegralHeight = False
        Me.cb_situ.Location = New System.Drawing.Point(168, 98)
        Me.cb_situ.Name = "cb_situ"
        Me.cb_situ.Size = New System.Drawing.Size(230, 21)
        Me.cb_situ.TabIndex = 2
        '
        'Lbl0
        '
        Me.Lbl0.AutoSize = True
        Me.Lbl0.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl0.Location = New System.Drawing.Point(58, 36)
        Me.Lbl0.Name = "Lbl0"
        Me.Lbl0.Size = New System.Drawing.Size(54, 16)
        Me.Lbl0.TabIndex = 3
        Me.Lbl0.Text = "Desde"
        '
        'Lbl1
        '
        Me.Lbl1.AutoSize = True
        Me.Lbl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl1.Location = New System.Drawing.Point(58, 67)
        Me.Lbl1.Name = "Lbl1"
        Me.Lbl1.Size = New System.Drawing.Size(49, 16)
        Me.Lbl1.TabIndex = 4
        Me.Lbl1.Text = "Hasta"
        '
        'Lbl2
        '
        Me.Lbl2.AutoSize = True
        Me.Lbl2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl2.Location = New System.Drawing.Point(58, 99)
        Me.Lbl2.Name = "Lbl2"
        Me.Lbl2.Size = New System.Drawing.Size(72, 16)
        Me.Lbl2.TabIndex = 5
        Me.Lbl2.Text = "Situación"
        '
        'BGworker
        '
        Me.BGworker.WorkerReportsProgress = True
        '
        'lb_reporte
        '
        Me.lb_reporte.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lb_reporte.FormattingEnabled = True
        Me.lb_reporte.Location = New System.Drawing.Point(14, 30)
        Me.lb_reporte.Name = "lb_reporte"
        Me.lb_reporte.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.lb_reporte.Size = New System.Drawing.Size(334, 95)
        Me.lb_reporte.TabIndex = 7
        '
        'btn_generar
        '
        Me.btn_generar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_generar.Location = New System.Drawing.Point(180, 328)
        Me.btn_generar.Name = "btn_generar"
        Me.btn_generar.Size = New System.Drawing.Size(91, 38)
        Me.btn_generar.TabIndex = 8
        Me.btn_generar.Text = "Visualizar"
        Me.btn_generar.UseVisualStyleBackColor = True
        '
        'btn_all
        '
        Me.btn_all.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_all.Location = New System.Drawing.Point(84, 131)
        Me.btn_all.Name = "btn_all"
        Me.btn_all.Size = New System.Drawing.Size(75, 23)
        Me.btn_all.TabIndex = 9
        Me.btn_all.Text = "Todos"
        Me.btn_all.UseVisualStyleBackColor = True
        '
        'btn_none
        '
        Me.btn_none.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_none.Location = New System.Drawing.Point(180, 132)
        Me.btn_none.Name = "btn_none"
        Me.btn_none.Size = New System.Drawing.Size(75, 23)
        Me.btn_none.TabIndex = 10
        Me.btn_none.Text = "Ninguno"
        Me.btn_none.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btn_none)
        Me.GroupBox1.Controls.Add(Me.btn_all)
        Me.GroupBox1.Controls.Add(Me.lb_reporte)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(50, 134)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(359, 166)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Reportes Disponibles"
        '
        'form_rep_baja
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(467, 381)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btn_generar)
        Me.Controls.Add(Me.Lbl2)
        Me.Controls.Add(Me.Lbl1)
        Me.Controls.Add(Me.Lbl0)
        Me.Controls.Add(Me.cb_situ)
        Me.Controls.Add(Me.cb_hasta)
        Me.Controls.Add(Me.cb_desde)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "form_rep_baja"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Activo Fijo en Estado de Baja"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub
    Friend WithEvents cb_desde As System.Windows.Forms.ComboBox
    Friend WithEvents cb_hasta As System.Windows.Forms.ComboBox
    Friend WithEvents cb_situ As System.Windows.Forms.ComboBox
    Friend WithEvents Lbl0 As System.Windows.Forms.Label
    Friend WithEvents Lbl1 As System.Windows.Forms.Label
    Friend WithEvents Lbl2 As System.Windows.Forms.Label
    Friend WithEvents BGworker As System.ComponentModel.BackgroundWorker
    Friend WithEvents lb_reporte As System.Windows.Forms.ListBox
    Friend WithEvents btn_generar As System.Windows.Forms.Button
    Friend WithEvents btn_all As System.Windows.Forms.Button
    Friend WithEvents btn_none As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
