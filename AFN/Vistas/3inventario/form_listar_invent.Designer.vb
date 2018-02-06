<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_listar_invent
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_listar_invent))
        Me.POpciones = New System.Windows.Forms.Panel()
        Me.btn_mostrar = New System.Windows.Forms.Button()
        Me.cboClase = New System.Windows.Forms.ComboBox()
        Me.cboZona = New System.Windows.Forms.ComboBox()
        Me.cboFech_Inv = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgv_mostrar = New System.Windows.Forms.DataGridView()
        Me.POpciones.SuspendLayout()
        CType(Me.dgv_mostrar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'POpciones
        '
        Me.POpciones.Controls.Add(Me.btn_mostrar)
        Me.POpciones.Controls.Add(Me.cboClase)
        Me.POpciones.Controls.Add(Me.cboZona)
        Me.POpciones.Controls.Add(Me.cboFech_Inv)
        Me.POpciones.Controls.Add(Me.Label3)
        Me.POpciones.Controls.Add(Me.Label2)
        Me.POpciones.Controls.Add(Me.Label1)
        Me.POpciones.Location = New System.Drawing.Point(41, 1)
        Me.POpciones.Name = "POpciones"
        Me.POpciones.Size = New System.Drawing.Size(662, 113)
        Me.POpciones.TabIndex = 0
        '
        'btn_mostrar
        '
        Me.btn_mostrar.Location = New System.Drawing.Point(530, 68)
        Me.btn_mostrar.Name = "btn_mostrar"
        Me.btn_mostrar.Size = New System.Drawing.Size(75, 23)
        Me.btn_mostrar.TabIndex = 6
        Me.btn_mostrar.Text = "Button1"
        Me.btn_mostrar.UseVisualStyleBackColor = True
        '
        'cboClase
        '
        Me.cboClase.FormattingEnabled = True
        Me.cboClase.Location = New System.Drawing.Point(245, 73)
        Me.cboClase.Name = "cboClase"
        Me.cboClase.Size = New System.Drawing.Size(150, 21)
        Me.cboClase.TabIndex = 5
        '
        'cboZona
        '
        Me.cboZona.FormattingEnabled = True
        Me.cboZona.Location = New System.Drawing.Point(243, 46)
        Me.cboZona.Name = "cboZona"
        Me.cboZona.Size = New System.Drawing.Size(152, 21)
        Me.cboZona.TabIndex = 4
        '
        'cboFech_Inv
        '
        Me.cboFech_Inv.FormattingEnabled = True
        Me.cboFech_Inv.Location = New System.Drawing.Point(243, 19)
        Me.cboFech_Inv.Name = "cboFech_Inv"
        Me.cboFech_Inv.Size = New System.Drawing.Size(152, 21)
        Me.cboFech_Inv.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(119, 73)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Label3"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(119, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Label2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(119, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'dgv_mostrar
        '
        Me.dgv_mostrar.AllowUserToAddRows = False
        Me.dgv_mostrar.AllowUserToDeleteRows = False
        Me.dgv_mostrar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_mostrar.Location = New System.Drawing.Point(17, 165)
        Me.dgv_mostrar.Name = "dgv_mostrar"
        Me.dgv_mostrar.ReadOnly = True
        Me.dgv_mostrar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_mostrar.Size = New System.Drawing.Size(747, 174)
        Me.dgv_mostrar.TabIndex = 1
        '
        'form_listar_invent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(776, 370)
        Me.Controls.Add(Me.dgv_mostrar)
        Me.Controls.Add(Me.POpciones)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "form_listar_invent"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Vista para revisar Tomas de Inventario cargadas"
        Me.POpciones.ResumeLayout(False)
        Me.POpciones.PerformLayout()
        CType(Me.dgv_mostrar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents POpciones As System.Windows.Forms.Panel
    Friend WithEvents btn_mostrar As System.Windows.Forms.Button
    Friend WithEvents cboClase As System.Windows.Forms.ComboBox
    Friend WithEvents cboZona As System.Windows.Forms.ComboBox
    Friend WithEvents cboFech_Inv As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgv_mostrar As System.Windows.Forms.DataGridView
End Class
