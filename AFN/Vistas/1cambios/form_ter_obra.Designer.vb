<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_ter_obra
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_ter_obra))
        Me._Label1 = New System.Windows.Forms.Label()
        Me.Tsaldos = New System.Windows.Forms.DataGridView()
        Me.salidaAF = New System.Windows.Forms.DataGridView()
        Me._Label2 = New System.Windows.Forms.Label()
        Me.Ecod = New System.Windows.Forms.TextBox()
        Me.Edesc = New System.Windows.Forms.TextBox()
        Me.EmontoMax = New System.Windows.Forms.TextBox()
        Me.EmontoSel = New System.Windows.Forms.TextBox()
        Me.btn_adjuntar = New System.Windows.Forms.Button()
        Me._Label3 = New System.Windows.Forms.Label()
        Me._Label4 = New System.Windows.Forms.Label()
        Me._Label5 = New System.Windows.Forms.Label()
        Me._Label6 = New System.Windows.Forms.Label()
        Me.Tcantidad = New System.Windows.Forms.TextBox()
        Me._Label7 = New System.Windows.Forms.Label()
        Me.LvalorAF = New System.Windows.Forms.TextBox()
        Me.btn_guardar = New System.Windows.Forms.Button()
        Me.btn_quitar = New System.Windows.Forms.PictureBox()
        Me._Label30 = New System.Windows.Forms.Label()
        Me.btFindEntrada = New System.Windows.Forms.Button()
        Me.MaskedTextBox1 = New System.Windows.Forms.MaskedTextBox()
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
        Me._Label1.Location = New System.Drawing.Point(17, 9)
        Me._Label1.Name = "_Label1"
        Me._Label1.Size = New System.Drawing.Size(167, 20)
        Me._Label1.TabIndex = 0
        Me._Label1.Text = "Entradas con Saldo"
        '
        'Tsaldos
        '
        Me.Tsaldos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tsaldos.Location = New System.Drawing.Point(21, 32)
        Me.Tsaldos.Name = "Tsaldos"
        Me.Tsaldos.Size = New System.Drawing.Size(769, 227)
        Me.Tsaldos.TabIndex = 1
        '
        'salidaAF
        '
        Me.salidaAF.AllowUserToAddRows = False
        Me.salidaAF.AllowUserToDeleteRows = False
        Me.salidaAF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.salidaAF.Location = New System.Drawing.Point(21, 380)
        Me.salidaAF.Name = "salidaAF"
        Me.salidaAF.ReadOnly = True
        Me.salidaAF.Size = New System.Drawing.Size(769, 134)
        Me.salidaAF.TabIndex = 11
        '
        '_Label2
        '
        Me._Label2.AutoSize = True
        Me._Label2.Location = New System.Drawing.Point(197, 267)
        Me._Label2.Name = "_Label2"
        Me._Label2.Size = New System.Drawing.Size(63, 13)
        Me._Label2.TabIndex = 3
        Me._Label2.Text = "Descripción"
        '
        'Ecod
        '
        Me.Ecod.Location = New System.Drawing.Point(108, 283)
        Me.Ecod.Name = "Ecod"
        Me.Ecod.Size = New System.Drawing.Size(86, 20)
        Me.Ecod.TabIndex = 2
        '
        'Edesc
        '
        Me.Edesc.Location = New System.Drawing.Point(200, 283)
        Me.Edesc.Name = "Edesc"
        Me.Edesc.Size = New System.Drawing.Size(413, 20)
        Me.Edesc.TabIndex = 4
        '
        'EmontoMax
        '
        Me.EmontoMax.Location = New System.Drawing.Point(128, 326)
        Me.EmontoMax.Name = "EmontoMax"
        Me.EmontoMax.Size = New System.Drawing.Size(131, 20)
        Me.EmontoMax.TabIndex = 6
        '
        'EmontoSel
        '
        Me.EmontoSel.Location = New System.Drawing.Point(277, 326)
        Me.EmontoSel.Name = "EmontoSel"
        Me.EmontoSel.Size = New System.Drawing.Size(113, 20)
        Me.EmontoSel.TabIndex = 8
        '
        'btn_adjuntar
        '
        Me.btn_adjuntar.Location = New System.Drawing.Point(559, 324)
        Me.btn_adjuntar.Name = "btn_adjuntar"
        Me.btn_adjuntar.Size = New System.Drawing.Size(54, 22)
        Me.btn_adjuntar.TabIndex = 9
        Me.btn_adjuntar.Text = "Adjuntar"
        Me.btn_adjuntar.UseVisualStyleBackColor = True
        '
        '_Label3
        '
        Me._Label3.AutoSize = True
        Me._Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label3.ForeColor = System.Drawing.Color.Red
        Me._Label3.Location = New System.Drawing.Point(30, 357)
        Me._Label3.Name = "_Label3"
        Me._Label3.Size = New System.Drawing.Size(175, 20)
        Me._Label3.TabIndex = 10
        Me._Label3.Text = "Salidas al Activo Fijo"
        '
        '_Label4
        '
        Me._Label4.AutoSize = True
        Me._Label4.Location = New System.Drawing.Point(125, 310)
        Me._Label4.Name = "_Label4"
        Me._Label4.Size = New System.Drawing.Size(89, 13)
        Me._Label4.TabIndex = 5
        Me._Label4.Text = "Monto Disponible"
        '
        '_Label5
        '
        Me._Label5.AutoSize = True
        Me._Label5.Location = New System.Drawing.Point(274, 310)
        Me._Label5.Name = "_Label5"
        Me._Label5.Size = New System.Drawing.Size(80, 13)
        Me._Label5.TabIndex = 7
        Me._Label5.Text = "Monto Utilizado"
        '
        '_Label6
        '
        Me._Label6.AutoSize = True
        Me._Label6.Location = New System.Drawing.Point(176, 528)
        Me._Label6.Name = "_Label6"
        Me._Label6.Size = New System.Drawing.Size(115, 13)
        Me._Label6.TabIndex = 12
        Me._Label6.Text = "Cantidad de Artículos :"
        '
        'Tcantidad
        '
        Me.Tcantidad.Location = New System.Drawing.Point(327, 525)
        Me.Tcantidad.Name = "Tcantidad"
        Me.Tcantidad.Size = New System.Drawing.Size(80, 20)
        Me.Tcantidad.TabIndex = 13
        '
        '_Label7
        '
        Me._Label7.AutoSize = True
        Me._Label7.Location = New System.Drawing.Point(180, 562)
        Me._Label7.Name = "_Label7"
        Me._Label7.Size = New System.Drawing.Size(110, 13)
        Me._Label7.TabIndex = 14
        Me._Label7.Text = "Valor Total Activo Fijo"
        '
        'LvalorAF
        '
        Me.LvalorAF.Location = New System.Drawing.Point(327, 562)
        Me.LvalorAF.Name = "LvalorAF"
        Me.LvalorAF.Size = New System.Drawing.Size(134, 20)
        Me.LvalorAF.TabIndex = 15
        '
        'btn_guardar
        '
        Me.btn_guardar.Location = New System.Drawing.Point(522, 540)
        Me.btn_guardar.Name = "btn_guardar"
        Me.btn_guardar.Size = New System.Drawing.Size(65, 35)
        Me.btn_guardar.TabIndex = 16
        Me.btn_guardar.Text = "Guardar"
        Me.btn_guardar.UseVisualStyleBackColor = True
        '
        'btn_quitar
        '
        Me.btn_quitar.Image = Global.AFN.My.Resources.Resources.remove1
        Me.btn_quitar.Location = New System.Drawing.Point(765, 354)
        Me.btn_quitar.Name = "btn_quitar"
        Me.btn_quitar.Size = New System.Drawing.Size(25, 23)
        Me.btn_quitar.TabIndex = 12
        Me.btn_quitar.TabStop = False
        '
        '_Label30
        '
        Me._Label30.AutoSize = True
        Me._Label30.Location = New System.Drawing.Point(114, 267)
        Me._Label30.Name = "_Label30"
        Me._Label30.Size = New System.Drawing.Size(40, 13)
        Me._Label30.TabIndex = 17
        Me._Label30.Text = "Codigo"
        '
        'btFindEntrada
        '
        Me.btFindEntrada.Location = New System.Drawing.Point(726, 265)
        Me.btFindEntrada.Name = "btFindEntrada"
        Me.btFindEntrada.Size = New System.Drawing.Size(64, 46)
        Me.btFindEntrada.TabIndex = 18
        Me.btFindEntrada.Text = "Buscar Entrada"
        Me.btFindEntrada.UseVisualStyleBackColor = True
        '
        'MaskedTextBox1
        '
        Me.MaskedTextBox1.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.MaskedTextBox1.HidePromptOnLeave = True
        Me.MaskedTextBox1.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite
        Me.MaskedTextBox1.Location = New System.Drawing.Point(418, 326)
        Me.MaskedTextBox1.Mask = "###,###,###,###,###.00"
        Me.MaskedTextBox1.Name = "MaskedTextBox1"
        Me.MaskedTextBox1.Size = New System.Drawing.Size(110, 20)
        Me.MaskedTextBox1.TabIndex = 19
        Me.MaskedTextBox1.Visible = False
        '
        'form_ter_obra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(813, 600)
        Me.Controls.Add(Me.MaskedTextBox1)
        Me.Controls.Add(Me.btFindEntrada)
        Me.Controls.Add(Me._Label30)
        Me.Controls.Add(Me.btn_guardar)
        Me.Controls.Add(Me.LvalorAF)
        Me.Controls.Add(Me._Label7)
        Me.Controls.Add(Me.Tcantidad)
        Me.Controls.Add(Me._Label6)
        Me.Controls.Add(Me.btn_quitar)
        Me.Controls.Add(Me._Label5)
        Me.Controls.Add(Me._Label4)
        Me.Controls.Add(Me._Label3)
        Me.Controls.Add(Me.btn_adjuntar)
        Me.Controls.Add(Me.EmontoSel)
        Me.Controls.Add(Me.EmontoMax)
        Me.Controls.Add(Me.Edesc)
        Me.Controls.Add(Me.Ecod)
        Me.Controls.Add(Me._Label2)
        Me.Controls.Add(Me.salidaAF)
        Me.Controls.Add(Me.Tsaldos)
        Me.Controls.Add(Me._Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "form_ter_obra"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Termino de Obra en Construcción hacia Activo Fijo"
        CType(Me.Tsaldos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.salidaAF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btn_quitar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents _Label1 As System.Windows.Forms.Label
    Friend WithEvents Tsaldos As System.Windows.Forms.DataGridView
    Friend WithEvents salidaAF As System.Windows.Forms.DataGridView
    Friend WithEvents _Label2 As System.Windows.Forms.Label
    Friend WithEvents Ecod As System.Windows.Forms.TextBox
    Friend WithEvents Edesc As System.Windows.Forms.TextBox
    Friend WithEvents EmontoMax As System.Windows.Forms.TextBox
    Friend WithEvents EmontoSel As System.Windows.Forms.TextBox
    Friend WithEvents btn_adjuntar As System.Windows.Forms.Button
    Friend WithEvents _Label3 As System.Windows.Forms.Label
    Friend WithEvents _Label4 As System.Windows.Forms.Label
    Friend WithEvents _Label5 As System.Windows.Forms.Label
    Friend WithEvents btn_quitar As System.Windows.Forms.PictureBox
    Friend WithEvents _Label6 As System.Windows.Forms.Label
    Friend WithEvents Tcantidad As System.Windows.Forms.TextBox
    Friend WithEvents _Label7 As System.Windows.Forms.Label
    Friend WithEvents LvalorAF As System.Windows.Forms.TextBox
    Friend WithEvents btn_guardar As System.Windows.Forms.Button
    Friend WithEvents _Label30 As System.Windows.Forms.Label
    Friend WithEvents btFindEntrada As System.Windows.Forms.Button
    Friend WithEvents MaskedTextBox1 As System.Windows.Forms.MaskedTextBox
End Class
