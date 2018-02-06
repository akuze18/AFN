<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_gasto_obra
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_gasto_obra))
        Me._Label1 = New System.Windows.Forms.Label()
        Me.Tsaldos = New System.Windows.Forms.DataGridView()
        Me.salidaAF = New System.Windows.Forms.DataGridView()
        Me.Ecod = New System.Windows.Forms.TextBox()
        Me.Ezona = New System.Windows.Forms.TextBox()
        Me._Label2 = New System.Windows.Forms.Label()
        Me.Edesc = New System.Windows.Forms.TextBox()
        Me._Label3 = New System.Windows.Forms.Label()
        Me.EmontoMax = New System.Windows.Forms.TextBox()
        Me._Label4 = New System.Windows.Forms.Label()
        Me.EmontoSel = New System.Windows.Forms.TextBox()
        Me._Label5 = New System.Windows.Forms.Label()
        Me.Efecha = New System.Windows.Forms.DateTimePicker()
        Me.btn_adjuntar = New System.Windows.Forms.Button()
        Me._Label6 = New System.Windows.Forms.Label()
        Me.btn_guardar = New System.Windows.Forms.Button()
        Me.btn_in_temp = New System.Windows.Forms.Button()
        Me.btn_out_temp = New System.Windows.Forms.Button()
        Me.btn_quitar = New System.Windows.Forms.PictureBox()
        Me.btn_clear_temp = New System.Windows.Forms.Button()
        Me.btFindEntrada = New System.Windows.Forms.Button()
        Me._Label7 = New System.Windows.Forms.Label()
        CType(Me.Tsaldos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.salidaAF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btn_quitar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        '_Label1
        '
        Me._Label1.AutoSize = True
        Me._Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1.ForeColor = System.Drawing.Color.Green
        Me._Label1.Location = New System.Drawing.Point(28, 14)
        Me._Label1.Name = "_Label1"
        Me._Label1.Size = New System.Drawing.Size(167, 20)
        Me._Label1.TabIndex = 0
        Me._Label1.Text = "Entradas con Saldo"
        '
        'Tsaldos
        '
        Me.Tsaldos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tsaldos.Location = New System.Drawing.Point(32, 37)
        Me.Tsaldos.Name = "Tsaldos"
        Me.Tsaldos.Size = New System.Drawing.Size(633, 129)
        Me.Tsaldos.TabIndex = 1
        '
        'salidaAF
        '
        Me.salidaAF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.salidaAF.Location = New System.Drawing.Point(32, 302)
        Me.salidaAF.Name = "salidaAF"
        Me.salidaAF.Size = New System.Drawing.Size(633, 125)
        Me.salidaAF.TabIndex = 2
        '
        'Ecod
        '
        Me.Ecod.Location = New System.Drawing.Point(32, 196)
        Me.Ecod.Name = "Ecod"
        Me.Ecod.Size = New System.Drawing.Size(88, 20)
        Me.Ecod.TabIndex = 3
        '
        'Ezona
        '
        Me.Ezona.Location = New System.Drawing.Point(12, 243)
        Me.Ezona.Name = "Ezona"
        Me.Ezona.Size = New System.Drawing.Size(18, 20)
        Me.Ezona.TabIndex = 4
        Me.Ezona.Visible = False
        '
        '_Label2
        '
        Me._Label2.AutoSize = True
        Me._Label2.Location = New System.Drawing.Point(151, 180)
        Me._Label2.Name = "_Label2"
        Me._Label2.Size = New System.Drawing.Size(63, 13)
        Me._Label2.TabIndex = 5
        Me._Label2.Text = "Descripción"
        '
        'Edesc
        '
        Me.Edesc.Location = New System.Drawing.Point(133, 196)
        Me.Edesc.Name = "Edesc"
        Me.Edesc.Size = New System.Drawing.Size(425, 20)
        Me.Edesc.TabIndex = 6
        '
        '_Label3
        '
        Me._Label3.AutoSize = True
        Me._Label3.Location = New System.Drawing.Point(56, 226)
        Me._Label3.Name = "_Label3"
        Me._Label3.Size = New System.Drawing.Size(89, 13)
        Me._Label3.TabIndex = 7
        Me._Label3.Text = "Monto Disponible"
        '
        'EmontoMax
        '
        Me.EmontoMax.Location = New System.Drawing.Point(48, 243)
        Me.EmontoMax.Name = "EmontoMax"
        Me.EmontoMax.Size = New System.Drawing.Size(135, 20)
        Me.EmontoMax.TabIndex = 8
        '
        '_Label4
        '
        Me._Label4.AutoSize = True
        Me._Label4.Location = New System.Drawing.Point(195, 226)
        Me._Label4.Name = "_Label4"
        Me._Label4.Size = New System.Drawing.Size(80, 13)
        Me._Label4.TabIndex = 9
        Me._Label4.Text = "Monto Utilizado"
        '
        'EmontoSel
        '
        Me.EmontoSel.Location = New System.Drawing.Point(198, 242)
        Me.EmontoSel.Name = "EmontoSel"
        Me.EmontoSel.Size = New System.Drawing.Size(142, 20)
        Me.EmontoSel.TabIndex = 10
        '
        '_Label5
        '
        Me._Label5.AutoSize = True
        Me._Label5.Location = New System.Drawing.Point(355, 226)
        Me._Label5.Name = "_Label5"
        Me._Label5.Size = New System.Drawing.Size(84, 13)
        Me._Label5.TabIndex = 11
        Me._Label5.Text = "Fecha de Salida"
        '
        'Efecha
        '
        Me.Efecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.Efecha.Location = New System.Drawing.Point(358, 242)
        Me.Efecha.Name = "Efecha"
        Me.Efecha.Size = New System.Drawing.Size(115, 20)
        Me.Efecha.TabIndex = 12
        '
        'btn_adjuntar
        '
        Me.btn_adjuntar.Location = New System.Drawing.Point(491, 243)
        Me.btn_adjuntar.Name = "btn_adjuntar"
        Me.btn_adjuntar.Size = New System.Drawing.Size(54, 22)
        Me.btn_adjuntar.TabIndex = 13
        Me.btn_adjuntar.Text = "Adjuntar"
        Me.btn_adjuntar.UseVisualStyleBackColor = True
        '
        '_Label6
        '
        Me._Label6.AutoSize = True
        Me._Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label6.ForeColor = System.Drawing.Color.Red
        Me._Label6.Location = New System.Drawing.Point(28, 282)
        Me._Label6.Name = "_Label6"
        Me._Label6.Size = New System.Drawing.Size(137, 20)
        Me._Label6.TabIndex = 15
        Me._Label6.Text = "Salida a Gastos"
        '
        'btn_guardar
        '
        Me.btn_guardar.Location = New System.Drawing.Point(171, 433)
        Me.btn_guardar.Name = "btn_guardar"
        Me.btn_guardar.Size = New System.Drawing.Size(67, 45)
        Me.btn_guardar.TabIndex = 16
        Me.btn_guardar.Text = "Guardar"
        Me.btn_guardar.UseVisualStyleBackColor = True
        '
        'btn_in_temp
        '
        Me.btn_in_temp.Location = New System.Drawing.Point(324, 433)
        Me.btn_in_temp.Name = "btn_in_temp"
        Me.btn_in_temp.Size = New System.Drawing.Size(67, 45)
        Me.btn_in_temp.TabIndex = 17
        Me.btn_in_temp.Text = "Guardar Borrador"
        Me.btn_in_temp.UseVisualStyleBackColor = True
        '
        'btn_out_temp
        '
        Me.btn_out_temp.Location = New System.Drawing.Point(406, 433)
        Me.btn_out_temp.Name = "btn_out_temp"
        Me.btn_out_temp.Size = New System.Drawing.Size(67, 45)
        Me.btn_out_temp.TabIndex = 18
        Me.btn_out_temp.Text = "Cargar Borrador"
        Me.btn_out_temp.UseVisualStyleBackColor = True
        '
        'btn_quitar
        '
        Me.btn_quitar.Image = Global.AFN.My.Resources.Resources.remove1
        Me.btn_quitar.Location = New System.Drawing.Point(630, 278)
        Me.btn_quitar.Name = "btn_quitar"
        Me.btn_quitar.Size = New System.Drawing.Size(25, 23)
        Me.btn_quitar.TabIndex = 14
        Me.btn_quitar.TabStop = False
        '
        'btn_clear_temp
        '
        Me.btn_clear_temp.Location = New System.Drawing.Point(491, 433)
        Me.btn_clear_temp.Name = "btn_clear_temp"
        Me.btn_clear_temp.Size = New System.Drawing.Size(67, 45)
        Me.btn_clear_temp.TabIndex = 19
        Me.btn_clear_temp.Text = "Borrar Borrador"
        Me.btn_clear_temp.UseVisualStyleBackColor = True
        '
        'btFindEntrada
        '
        Me.btFindEntrada.Location = New System.Drawing.Point(609, 172)
        Me.btFindEntrada.Name = "btFindEntrada"
        Me.btFindEntrada.Size = New System.Drawing.Size(56, 44)
        Me.btFindEntrada.TabIndex = 20
        Me.btFindEntrada.Text = "Buscar Entrada"
        Me.btFindEntrada.UseVisualStyleBackColor = True
        '
        '_Label7
        '
        Me._Label7.AutoSize = True
        Me._Label7.Location = New System.Drawing.Point(36, 180)
        Me._Label7.Name = "_Label7"
        Me._Label7.Size = New System.Drawing.Size(40, 13)
        Me._Label7.TabIndex = 21
        Me._Label7.Text = "Codigo"
        '
        'form_gasto_obra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(699, 490)
        Me.Controls.Add(Me._Label7)
        Me.Controls.Add(Me.btFindEntrada)
        Me.Controls.Add(Me.btn_clear_temp)
        Me.Controls.Add(Me.btn_out_temp)
        Me.Controls.Add(Me.btn_in_temp)
        Me.Controls.Add(Me.btn_guardar)
        Me.Controls.Add(Me._Label6)
        Me.Controls.Add(Me.btn_quitar)
        Me.Controls.Add(Me.btn_adjuntar)
        Me.Controls.Add(Me.Efecha)
        Me.Controls.Add(Me._Label5)
        Me.Controls.Add(Me.EmontoSel)
        Me.Controls.Add(Me._Label4)
        Me.Controls.Add(Me.EmontoMax)
        Me.Controls.Add(Me._Label3)
        Me.Controls.Add(Me.Edesc)
        Me.Controls.Add(Me._Label2)
        Me.Controls.Add(Me.Ezona)
        Me.Controls.Add(Me.Ecod)
        Me.Controls.Add(Me.salidaAF)
        Me.Controls.Add(Me.Tsaldos)
        Me.Controls.Add(Me._Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "form_gasto_obra"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Obra en Construcción hacia Gastos"
        CType(Me.Tsaldos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.salidaAF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_quitar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents _Label1 As System.Windows.Forms.Label
    Friend WithEvents Tsaldos As System.Windows.Forms.DataGridView
    Friend WithEvents salidaAF As System.Windows.Forms.DataGridView
    Friend WithEvents Ecod As System.Windows.Forms.TextBox
    Friend WithEvents Ezona As System.Windows.Forms.TextBox
    Friend WithEvents _Label2 As System.Windows.Forms.Label
    Friend WithEvents Edesc As System.Windows.Forms.TextBox
    Friend WithEvents _Label3 As System.Windows.Forms.Label
    Friend WithEvents EmontoMax As System.Windows.Forms.TextBox
    Friend WithEvents _Label4 As System.Windows.Forms.Label
    Friend WithEvents EmontoSel As System.Windows.Forms.TextBox
    Friend WithEvents Efecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents btn_adjuntar As System.Windows.Forms.Button
    Friend WithEvents btn_quitar As System.Windows.Forms.PictureBox
    Friend WithEvents _Label6 As System.Windows.Forms.Label
    Friend WithEvents btn_guardar As System.Windows.Forms.Button
    Private WithEvents _Label5 As System.Windows.Forms.Label
    Friend WithEvents btn_in_temp As System.Windows.Forms.Button
    Friend WithEvents btn_out_temp As System.Windows.Forms.Button
    Friend WithEvents btn_clear_temp As System.Windows.Forms.Button
    Friend WithEvents btFindEntrada As System.Windows.Forms.Button
    Friend WithEvents _Label7 As System.Windows.Forms.Label
End Class
