<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class bus_prov
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(bus_prov))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tdescrip = New System.Windows.Forms.TextBox()
        Me.Tcodigo = New System.Windows.Forms.TextBox()
        Me.cboZona = New System.Windows.Forms.ComboBox()
        Me.btn_buscar = New System.Windows.Forms.Button()
        Me.btn_marcar = New System.Windows.Forms.Button()
        Me.Lresultado = New System.Windows.Forms.Label()
        Me.MosResult = New System.Windows.Forms.DataGridView()
        CType(Me.MosResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nombre :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "RUT :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Zona :"
        '
        'Tdescrip
        '
        Me.Tdescrip.Location = New System.Drawing.Point(85, 9)
        Me.Tdescrip.Name = "Tdescrip"
        Me.Tdescrip.Size = New System.Drawing.Size(290, 20)
        Me.Tdescrip.TabIndex = 3
        '
        'Tcodigo
        '
        Me.Tcodigo.Location = New System.Drawing.Point(85, 35)
        Me.Tcodigo.Name = "Tcodigo"
        Me.Tcodigo.Size = New System.Drawing.Size(177, 20)
        Me.Tcodigo.TabIndex = 4
        '
        'cboZona
        '
        Me.cboZona.Enabled = False
        Me.cboZona.FormattingEnabled = True
        Me.cboZona.Location = New System.Drawing.Point(85, 61)
        Me.cboZona.Name = "cboZona"
        Me.cboZona.Size = New System.Drawing.Size(177, 21)
        Me.cboZona.TabIndex = 5
        '
        'btn_buscar
        '
        Me.btn_buscar.Location = New System.Drawing.Point(300, 59)
        Me.btn_buscar.Name = "btn_buscar"
        Me.btn_buscar.Size = New System.Drawing.Size(75, 23)
        Me.btn_buscar.TabIndex = 6
        Me.btn_buscar.Text = "Buscar"
        Me.btn_buscar.UseVisualStyleBackColor = True
        '
        'btn_marcar
        '
        Me.btn_marcar.Location = New System.Drawing.Point(431, 88)
        Me.btn_marcar.Name = "btn_marcar"
        Me.btn_marcar.Size = New System.Drawing.Size(75, 23)
        Me.btn_marcar.TabIndex = 7
        Me.btn_marcar.Text = "Seleccionar"
        Me.btn_marcar.UseVisualStyleBackColor = True
        '
        'Lresultado
        '
        Me.Lresultado.AutoSize = True
        Me.Lresultado.Location = New System.Drawing.Point(48, 98)
        Me.Lresultado.Name = "Lresultado"
        Me.Lresultado.Size = New System.Drawing.Size(66, 13)
        Me.Lresultado.TabIndex = 8
        Me.Lresultado.Text = "Resultados :"
        '
        'MosResult
        '
        Me.MosResult.AllowUserToAddRows = False
        Me.MosResult.AllowUserToDeleteRows = False
        Me.MosResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MosResult.Location = New System.Drawing.Point(15, 114)
        Me.MosResult.MaximumSize = New System.Drawing.Size(520, 150)
        Me.MosResult.MinimumSize = New System.Drawing.Size(520, 150)
        Me.MosResult.Name = "MosResult"
        Me.MosResult.ReadOnly = True
        Me.MosResult.Size = New System.Drawing.Size(520, 150)
        Me.MosResult.TabIndex = 9
        '
        'bus_prov
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(554, 277)
        Me.Controls.Add(Me.MosResult)
        Me.Controls.Add(Me.Lresultado)
        Me.Controls.Add(Me.btn_marcar)
        Me.Controls.Add(Me.btn_buscar)
        Me.Controls.Add(Me.cboZona)
        Me.Controls.Add(Me.Tcodigo)
        Me.Controls.Add(Me.Tdescrip)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(570, 315)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(570, 315)
        Me.Name = "bus_prov"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Buscar un proveedor"
        CType(Me.MosResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Tdescrip As System.Windows.Forms.TextBox
    Friend WithEvents Tcodigo As System.Windows.Forms.TextBox
    Friend WithEvents cboZona As System.Windows.Forms.ComboBox
    Friend WithEvents btn_buscar As System.Windows.Forms.Button
    Friend WithEvents btn_marcar As System.Windows.Forms.Button
    Friend WithEvents Lresultado As System.Windows.Forms.Label
    Friend WithEvents MosResult As System.Windows.Forms.DataGridView
End Class
