<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_Cmov
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_Cmov))
        Me.cb_fecha = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.M0 = New System.Windows.Forms.MenuStrip()
        Me.MM0 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MMF1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MMT1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MMI1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MMI2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BGQuery = New System.ComponentModel.BackgroundWorker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboTipo = New System.Windows.Forms.ComboBox()
        Me.M0.SuspendLayout()
        Me.SuspendLayout()
        '
        'cb_fecha
        '
        Me.cb_fecha.DropDownHeight = 104
        Me.cb_fecha.FormattingEnabled = True
        Me.cb_fecha.IntegralHeight = False
        Me.cb_fecha.Location = New System.Drawing.Point(119, 56)
        Me.cb_fecha.Name = "cb_fecha"
        Me.cb_fecha.Size = New System.Drawing.Size(198, 21)
        Me.cb_fecha.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(26, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Periodo"
        '
        'M0
        '
        Me.M0.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MM0})
        Me.M0.Location = New System.Drawing.Point(0, 0)
        Me.M0.Name = "M0"
        Me.M0.Size = New System.Drawing.Size(361, 24)
        Me.M0.TabIndex = 3
        Me.M0.Text = "MenuStrip1"
        '
        'MM0
        '
        Me.MM0.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MMF1, Me.MMT1, Me.MMI1, Me.MMI2})
        Me.MM0.Name = "MM0"
        Me.MM0.Size = New System.Drawing.Size(60, 20)
        Me.MM0.Text = "Mostrar"
        '
        'MMF1
        '
        Me.MMF1.Name = "MMF1"
        Me.MMF1.Size = New System.Drawing.Size(152, 22)
        Me.MMF1.Text = "Financiero"
        '
        'MMT1
        '
        Me.MMT1.Name = "MMT1"
        Me.MMT1.Size = New System.Drawing.Size(152, 22)
        Me.MMT1.Text = "Tributario"
        '
        'MMI1
        '
        Me.MMI1.Name = "MMI1"
        Me.MMI1.Size = New System.Drawing.Size(152, 22)
        Me.MMI1.Text = "IFRS (CLP)"
        '
        'MMI2
        '
        Me.MMI2.Name = "MMI2"
        Me.MMI2.Size = New System.Drawing.Size(152, 22)
        Me.MMI2.Text = "IFRS (YEN)"
        '
        'BGQuery
        '
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(29, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Tipo"
        '
        'cboTipo
        '
        Me.cboTipo.FormattingEnabled = True
        Me.cboTipo.Location = New System.Drawing.Point(119, 96)
        Me.cboTipo.Name = "cboTipo"
        Me.cboTipo.Size = New System.Drawing.Size(198, 21)
        Me.cboTipo.TabIndex = 5
        '
        'form_Cmov
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(361, 167)
        Me.Controls.Add(Me.cboTipo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cb_fecha)
        Me.Controls.Add(Me.M0)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.M0
        Me.Name = "form_Cmov"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cuadro de Movimiento"
        Me.M0.ResumeLayout(False)
        Me.M0.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cb_fecha As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents M0 As System.Windows.Forms.MenuStrip
    Friend WithEvents MM0 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MMF1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MMT1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MMI1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BGQuery As System.ComponentModel.BackgroundWorker
    Friend WithEvents MMI2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboTipo As System.Windows.Forms.ComboBox
End Class
