<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_ficha_cambio
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_ficha_cambio))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cod_art = New System.Windows.Forms.Label()
        Me.btn_consulta = New System.Windows.Forms.Button()
        Me.Tarticulo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lista_cambios = New System.Windows.Forms.DataGridView()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btn_imprimir = New System.Windows.Forms.Button()
        CType(Me.lista_cambios, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Seleccione Artículo"
        '
        'cod_art
        '
        Me.cod_art.Location = New System.Drawing.Point(160, 26)
        Me.cod_art.Name = "cod_art"
        Me.cod_art.Size = New System.Drawing.Size(100, 13)
        Me.cod_art.TabIndex = 2
        Me.cod_art.Text = "Label2"
        '
        'btn_consulta
        '
        Me.btn_consulta.Location = New System.Drawing.Point(463, 34)
        Me.btn_consulta.Name = "btn_consulta"
        Me.btn_consulta.Size = New System.Drawing.Size(56, 34)
        Me.btn_consulta.TabIndex = 28
        Me.btn_consulta.Text = "Buscar"
        Me.btn_consulta.UseVisualStyleBackColor = True
        '
        'Tarticulo
        '
        Me.Tarticulo.Location = New System.Drawing.Point(84, 48)
        Me.Tarticulo.Name = "Tarticulo"
        Me.Tarticulo.Size = New System.Drawing.Size(355, 20)
        Me.Tarticulo.TabIndex = 30
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(34, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Nombre"
        '
        'lista_cambios
        '
        Me.lista_cambios.AllowUserToAddRows = False
        Me.lista_cambios.AllowUserToDeleteRows = False
        Me.lista_cambios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.lista_cambios.Location = New System.Drawing.Point(15, 108)
        Me.lista_cambios.MultiSelect = False
        Me.lista_cambios.Name = "lista_cambios"
        Me.lista_cambios.ReadOnly = True
        Me.lista_cambios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.lista_cambios.Size = New System.Drawing.Size(762, 131)
        Me.lista_cambios.TabIndex = 32
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(15, 88)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(104, 13)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "Cambios del Artículo"
        '
        'btn_imprimir
        '
        Me.btn_imprimir.Location = New System.Drawing.Point(545, 34)
        Me.btn_imprimir.Name = "btn_imprimir"
        Me.btn_imprimir.Size = New System.Drawing.Size(56, 34)
        Me.btn_imprimir.TabIndex = 33
        Me.btn_imprimir.Text = "Imprimir"
        Me.btn_imprimir.UseVisualStyleBackColor = True
        '
        'form_ficha_cambio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(789, 266)
        Me.Controls.Add(Me.btn_imprimir)
        Me.Controls.Add(Me.lista_cambios)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Tarticulo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btn_consulta)
        Me.Controls.Add(Me.cod_art)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "form_ficha_cambio"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ficha de Cambio de Zona"
        CType(Me.lista_cambios, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cod_art As System.Windows.Forms.Label
    Friend WithEvents btn_consulta As System.Windows.Forms.Button
    Friend WithEvents Tarticulo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lista_cambios As System.Windows.Forms.DataGridView
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btn_imprimir As System.Windows.Forms.Button
End Class
