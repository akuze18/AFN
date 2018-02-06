<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_FixAsset
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_FixAsset))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cb_fecha = New System.Windows.Forms.ComboBox()
        Me.cboTipo = New System.Windows.Forms.ComboBox()
        Me.MarcoMoneda = New System.Windows.Forms.GroupBox()
        Me.Op2 = New System.Windows.Forms.RadioButton()
        Me.Op1 = New System.Windows.Forms.RadioButton()
        Me.btn_calcular = New System.Windows.Forms.Button()
        Me.BGQuery = New System.ComponentModel.BackgroundWorker()
        Me.MarcoMoneda.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(31, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Fecha"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(31, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Tipo"
        '
        'cb_fecha
        '
        Me.cb_fecha.DropDownHeight = 93
        Me.cb_fecha.FormattingEnabled = True
        Me.cb_fecha.IntegralHeight = False
        Me.cb_fecha.Location = New System.Drawing.Point(95, 28)
        Me.cb_fecha.MaxDropDownItems = 7
        Me.cb_fecha.Name = "cb_fecha"
        Me.cb_fecha.Size = New System.Drawing.Size(144, 21)
        Me.cb_fecha.TabIndex = 2
        '
        'cboTipo
        '
        Me.cboTipo.FormattingEnabled = True
        Me.cboTipo.Location = New System.Drawing.Point(95, 64)
        Me.cboTipo.Name = "cboTipo"
        Me.cboTipo.Size = New System.Drawing.Size(144, 21)
        Me.cboTipo.TabIndex = 3
        '
        'MarcoMoneda
        '
        Me.MarcoMoneda.Controls.Add(Me.Op2)
        Me.MarcoMoneda.Controls.Add(Me.Op1)
        Me.MarcoMoneda.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MarcoMoneda.Location = New System.Drawing.Point(57, 113)
        Me.MarcoMoneda.Name = "MarcoMoneda"
        Me.MarcoMoneda.Size = New System.Drawing.Size(149, 95)
        Me.MarcoMoneda.TabIndex = 4
        Me.MarcoMoneda.TabStop = False
        Me.MarcoMoneda.Text = "Presentación"
        '
        'Op2
        '
        Me.Op2.AutoSize = True
        Me.Op2.Checked = True
        Me.Op2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Op2.Location = New System.Drawing.Point(16, 30)
        Me.Op2.Name = "Op2"
        Me.Op2.Size = New System.Drawing.Size(112, 17)
        Me.Op2.TabIndex = 1
        Me.Op2.TabStop = True
        Me.Op2.Tag = "LC"
        Me.Op2.Text = "Local Pesos (CLP)"
        Me.Op2.UseVisualStyleBackColor = True
        '
        'Op1
        '
        Me.Op1.AutoSize = True
        Me.Op1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Op1.Location = New System.Drawing.Point(16, 53)
        Me.Op1.Name = "Op1"
        Me.Op1.Size = New System.Drawing.Size(115, 17)
        Me.Op1.TabIndex = 0
        Me.Op1.Tag = "F"
        Me.Op1.Text = "Japón Pesos (CLP)"
        Me.Op1.UseVisualStyleBackColor = True
        Me.Op1.Visible = False
        '
        'btn_calcular
        '
        Me.btn_calcular.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_calcular.Location = New System.Drawing.Point(95, 242)
        Me.btn_calcular.Name = "btn_calcular"
        Me.btn_calcular.Size = New System.Drawing.Size(75, 33)
        Me.btn_calcular.TabIndex = 5
        Me.btn_calcular.Text = "Mostrar"
        Me.btn_calcular.UseVisualStyleBackColor = True
        '
        'BGQuery
        '
        '
        'form_FixAsset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(282, 292)
        Me.Controls.Add(Me.btn_calcular)
        Me.Controls.Add(Me.MarcoMoneda)
        Me.Controls.Add(Me.cboTipo)
        Me.Controls.Add(Me.cb_fecha)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "form_FixAsset"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report Fixed Assets"
        Me.MarcoMoneda.ResumeLayout(False)
        Me.MarcoMoneda.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cb_fecha As System.Windows.Forms.ComboBox
    Friend WithEvents cboTipo As System.Windows.Forms.ComboBox
    Friend WithEvents MarcoMoneda As System.Windows.Forms.GroupBox
    Friend WithEvents Op2 As System.Windows.Forms.RadioButton
    Friend WithEvents Op1 As System.Windows.Forms.RadioButton
    Friend WithEvents btn_calcular As System.Windows.Forms.Button
    Friend WithEvents BGQuery As System.ComponentModel.BackgroundWorker
End Class
