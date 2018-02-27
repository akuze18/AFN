<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_gestion
    Inherits Global.AFN.MainForm

    'Form invalida a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnBuscar = New System.Windows.Forms.Button()
        Me.TBcod = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Nparte = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CBgestion = New System.Windows.Forms.ComboBox()
        Me.BtnGuardar = New System.Windows.Forms.Button()
        Me.BtnLimpiar = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LbFechas = New System.Windows.Forms.ListBox()
        CType(Me.Nparte, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 19)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Codigo Lote"
        '
        'BtnBuscar
        '
        Me.BtnBuscar.Location = New System.Drawing.Point(410, 15)
        Me.BtnBuscar.Name = "BtnBuscar"
        Me.BtnBuscar.Size = New System.Drawing.Size(75, 32)
        Me.BtnBuscar.TabIndex = 1
        Me.BtnBuscar.Text = "Buscar"
        Me.BtnBuscar.UseVisualStyleBackColor = True
        '
        'TBcod
        '
        Me.TBcod.AccessibleName = "Codigo Lote"
        Me.TBcod.Location = New System.Drawing.Point(144, 22)
        Me.TBcod.Name = "TBcod"
        Me.TBcod.Size = New System.Drawing.Size(131, 27)
        Me.TBcod.TabIndex = 2
        Me.TBcod.Tag = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 19)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Parte"
        '
        'Nparte
        '
        Me.Nparte.AccessibleName = "Parte"
        Me.Nparte.Location = New System.Drawing.Point(144, 73)
        Me.Nparte.Name = "Nparte"
        Me.Nparte.Size = New System.Drawing.Size(128, 27)
        Me.Nparte.TabIndex = 6
        Me.Nparte.Tag = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 246)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 19)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Gestion Nueva"
        '
        'CBgestion
        '
        Me.CBgestion.AccessibleName = "Nueva Gestion"
        Me.CBgestion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBgestion.FormattingEnabled = True
        Me.CBgestion.Location = New System.Drawing.Point(144, 243)
        Me.CBgestion.Name = "CBgestion"
        Me.CBgestion.Size = New System.Drawing.Size(249, 27)
        Me.CBgestion.TabIndex = 10
        Me.CBgestion.Tag = ""
        '
        'BtnGuardar
        '
        Me.BtnGuardar.Location = New System.Drawing.Point(163, 308)
        Me.BtnGuardar.Name = "BtnGuardar"
        Me.BtnGuardar.Size = New System.Drawing.Size(116, 35)
        Me.BtnGuardar.TabIndex = 11
        Me.BtnGuardar.Text = "Guardar"
        Me.BtnGuardar.UseVisualStyleBackColor = True
        '
        'BtnLimpiar
        '
        Me.BtnLimpiar.Location = New System.Drawing.Point(335, 308)
        Me.BtnLimpiar.Name = "BtnLimpiar"
        Me.BtnLimpiar.Size = New System.Drawing.Size(101, 35)
        Me.BtnLimpiar.TabIndex = 12
        Me.BtnLimpiar.Text = "Limpiar"
        Me.BtnLimpiar.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 129)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 19)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Fechas"
        '
        'LbFechas
        '
        Me.LbFechas.AccessibleName = "Fechas"
        Me.LbFechas.ColumnWidth = 2
        Me.LbFechas.FormattingEnabled = True
        Me.LbFechas.HorizontalScrollbar = True
        Me.LbFechas.ItemHeight = 19
        Me.LbFechas.Location = New System.Drawing.Point(144, 125)
        Me.LbFechas.MultiColumn = True
        Me.LbFechas.Name = "LbFechas"
        Me.LbFechas.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.LbFechas.Size = New System.Drawing.Size(419, 99)
        Me.LbFechas.TabIndex = 14
        Me.LbFechas.Tag = ""
        '
        'form_gestion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.ClientSize = New System.Drawing.Size(612, 379)
        Me.Controls.Add(Me.LbFechas)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.BtnLimpiar)
        Me.Controls.Add(Me.BtnGuardar)
        Me.Controls.Add(Me.CBgestion)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Nparte)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TBcod)
        Me.Controls.Add(Me.BtnBuscar)
        Me.Controls.Add(Me.Label1)
        Me.Name = "form_gestion"
        Me.Text = "Actualizar Gestión"
        CType(Me.Nparte, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnBuscar As System.Windows.Forms.Button
    Friend WithEvents TBcod As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Nparte As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CBgestion As System.Windows.Forms.ComboBox
    Friend WithEvents BtnGuardar As System.Windows.Forms.Button
    Friend WithEvents BtnLimpiar As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LbFechas As System.Windows.Forms.ListBox

End Class
