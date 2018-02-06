<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class busqueda_articulo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(busqueda_articulo))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Lresultado = New System.Windows.Forms.Label()
        Me.Tcodigo = New System.Windows.Forms.TextBox()
        Me.Tdescrip = New System.Windows.Forms.TextBox()
        Me.cboZona = New System.Windows.Forms.ComboBox()
        Me.Marco_fcomp = New System.Windows.Forms.GroupBox()
        Me.Fhasta = New System.Windows.Forms.DateTimePicker()
        Me.Fdesde = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btn_buscar = New System.Windows.Forms.Button()
        Me.btn_marcar = New System.Windows.Forms.Button()
        Me.MosResult = New System.Windows.Forms.DataGridView()
        Me.Marco_fcomp.SuspendLayout()
        CType(Me.MosResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Código Lote"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Descripción"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Zona"
        '
        'Lresultado
        '
        Me.Lresultado.AutoSize = True
        Me.Lresultado.Location = New System.Drawing.Point(47, 130)
        Me.Lresultado.Name = "Lresultado"
        Me.Lresultado.Size = New System.Drawing.Size(66, 13)
        Me.Lresultado.TabIndex = 3
        Me.Lresultado.Text = "Resultados: "
        '
        'Tcodigo
        '
        Me.Tcodigo.Location = New System.Drawing.Point(105, 18)
        Me.Tcodigo.Name = "Tcodigo"
        Me.Tcodigo.Size = New System.Drawing.Size(146, 20)
        Me.Tcodigo.TabIndex = 4
        '
        'Tdescrip
        '
        Me.Tdescrip.Location = New System.Drawing.Point(105, 50)
        Me.Tdescrip.Name = "Tdescrip"
        Me.Tdescrip.Size = New System.Drawing.Size(221, 20)
        Me.Tdescrip.TabIndex = 5
        '
        'cboZona
        '
        Me.cboZona.FormattingEnabled = True
        Me.cboZona.Location = New System.Drawing.Point(105, 78)
        Me.cboZona.Name = "cboZona"
        Me.cboZona.Size = New System.Drawing.Size(146, 21)
        Me.cboZona.TabIndex = 6
        '
        'Marco_fcomp
        '
        Me.Marco_fcomp.Controls.Add(Me.Fhasta)
        Me.Marco_fcomp.Controls.Add(Me.Fdesde)
        Me.Marco_fcomp.Controls.Add(Me.Label6)
        Me.Marco_fcomp.Controls.Add(Me.Label5)
        Me.Marco_fcomp.Location = New System.Drawing.Point(356, 18)
        Me.Marco_fcomp.Name = "Marco_fcomp"
        Me.Marco_fcomp.Size = New System.Drawing.Size(223, 83)
        Me.Marco_fcomp.TabIndex = 7
        Me.Marco_fcomp.TabStop = False
        Me.Marco_fcomp.Text = "Fecha de Compra"
        '
        'Fhasta
        '
        Me.Fhasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Fhasta.Location = New System.Drawing.Point(89, 53)
        Me.Fhasta.Name = "Fhasta"
        Me.Fhasta.Size = New System.Drawing.Size(118, 20)
        Me.Fhasta.TabIndex = 3
        '
        'Fdesde
        '
        Me.Fdesde.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Fdesde.Location = New System.Drawing.Point(89, 26)
        Me.Fdesde.Name = "Fdesde"
        Me.Fdesde.Size = New System.Drawing.Size(118, 20)
        Me.Fdesde.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(18, 53)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Hasta"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Desde"
        '
        'btn_buscar
        '
        Me.btn_buscar.Location = New System.Drawing.Point(303, 107)
        Me.btn_buscar.Name = "btn_buscar"
        Me.btn_buscar.Size = New System.Drawing.Size(64, 25)
        Me.btn_buscar.TabIndex = 8
        Me.btn_buscar.Text = "Buscar"
        Me.btn_buscar.UseVisualStyleBackColor = True
        '
        'btn_marcar
        '
        Me.btn_marcar.Location = New System.Drawing.Point(403, 109)
        Me.btn_marcar.Name = "btn_marcar"
        Me.btn_marcar.Size = New System.Drawing.Size(72, 23)
        Me.btn_marcar.TabIndex = 9
        Me.btn_marcar.Text = "Seleccionar"
        Me.btn_marcar.UseVisualStyleBackColor = True
        '
        'MosResult
        '
        Me.MosResult.AllowUserToAddRows = False
        Me.MosResult.AllowUserToDeleteRows = False
        Me.MosResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MosResult.Location = New System.Drawing.Point(22, 146)
        Me.MosResult.MultiSelect = False
        Me.MosResult.Name = "MosResult"
        Me.MosResult.ReadOnly = True
        Me.MosResult.RowHeadersWidth = 20
        Me.MosResult.RowTemplate.Height = 20
        Me.MosResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.MosResult.Size = New System.Drawing.Size(589, 223)
        Me.MosResult.TabIndex = 10
        '
        'busqueda_articulo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(628, 380)
        Me.Controls.Add(Me.MosResult)
        Me.Controls.Add(Me.btn_marcar)
        Me.Controls.Add(Me.btn_buscar)
        Me.Controls.Add(Me.Marco_fcomp)
        Me.Controls.Add(Me.cboZona)
        Me.Controls.Add(Me.Tdescrip)
        Me.Controls.Add(Me.Tcodigo)
        Me.Controls.Add(Me.Lresultado)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "busqueda_articulo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Buscar un artículo"
        Me.Marco_fcomp.ResumeLayout(False)
        Me.Marco_fcomp.PerformLayout()
        CType(Me.MosResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Lresultado As System.Windows.Forms.Label
    Friend WithEvents Tcodigo As System.Windows.Forms.TextBox
    Friend WithEvents Tdescrip As System.Windows.Forms.TextBox
    Friend WithEvents cboZona As System.Windows.Forms.ComboBox
    Friend WithEvents Marco_fcomp As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btn_buscar As System.Windows.Forms.Button
    Friend WithEvents btn_marcar As System.Windows.Forms.Button
    Friend WithEvents MosResult As System.Windows.Forms.DataGridView
    Friend WithEvents Fhasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Fdesde As System.Windows.Forms.DateTimePicker
End Class
