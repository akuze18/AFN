Namespace Vistas.Reportes
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_reporte
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_reporte))
        Me.M0 = New System.Windows.Forms.MenuStrip()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cb_fecha = New System.Windows.Forms.ComboBox()
        Me.cb_clase = New System.Windows.Forms.ComboBox()
        Me.cb_zona = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'M0
        '
        Me.M0.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.M0.Location = New System.Drawing.Point(0, 0)
        Me.M0.Name = "M0"
        Me.M0.Size = New System.Drawing.Size(348, 24)
        Me.M0.TabIndex = 0
        Me.M0.Text = "MenuStrip1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(42, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Fecha"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(42, 126)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Zona"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(42, 95)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 16)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Clase"
        '
        'cb_fecha
        '
        Me.cb_fecha.DropDownHeight = 65
        Me.cb_fecha.FormattingEnabled = True
        Me.cb_fecha.IntegralHeight = False
        Me.cb_fecha.Location = New System.Drawing.Point(120, 60)
        Me.cb_fecha.MaxDropDownItems = 5
        Me.cb_fecha.Name = "cb_fecha"
        Me.cb_fecha.Size = New System.Drawing.Size(190, 21)
        Me.cb_fecha.TabIndex = 4
        '
        'cb_clase
        '
        Me.cb_clase.DropDownHeight = 65
        Me.cb_clase.FormattingEnabled = True
        Me.cb_clase.IntegralHeight = False
        Me.cb_clase.Location = New System.Drawing.Point(120, 95)
        Me.cb_clase.Name = "cb_clase"
        Me.cb_clase.Size = New System.Drawing.Size(190, 21)
        Me.cb_clase.TabIndex = 5
        '
        'cb_zona
        '
        Me.cb_zona.DropDownHeight = 65
        Me.cb_zona.FormattingEnabled = True
        Me.cb_zona.IntegralHeight = False
        Me.cb_zona.Location = New System.Drawing.Point(120, 126)
        Me.cb_zona.Name = "cb_zona"
        Me.cb_zona.Size = New System.Drawing.Size(190, 21)
        Me.cb_zona.TabIndex = 6
        '
        'form_reporte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(348, 189)
        Me.Controls.Add(Me.cb_zona)
        Me.Controls.Add(Me.cb_clase)
        Me.Controls.Add(Me.cb_fecha)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.M0)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.M0
        Me.Name = "form_reporte"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Activo Fijo en Estado Vigente"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents M0 As System.Windows.Forms.MenuStrip
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cb_fecha As System.Windows.Forms.ComboBox
    Friend WithEvents cb_clase As System.Windows.Forms.ComboBox
    Friend WithEvents cb_zona As System.Windows.Forms.ComboBox
End Class
End Namespace