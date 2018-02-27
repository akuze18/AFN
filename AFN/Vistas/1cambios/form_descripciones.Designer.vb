
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_descripciones
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
        Me.tbLote = New System.Windows.Forms.TextBox()
        Me.btn_show = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbDescrip = New System.Windows.Forms.TextBox()
        Me.btn_save = New System.Windows.Forms.Button()
        Me.btn_cancelar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 19)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Codigo Lote"
        '
        'tbLote
        '
        Me.tbLote.Location = New System.Drawing.Point(137, 20)
        Me.tbLote.Name = "tbLote"
        Me.tbLote.Size = New System.Drawing.Size(237, 27)
        Me.tbLote.TabIndex = 1
        '
        'btn_show
        '
        Me.btn_show.Location = New System.Drawing.Point(406, 18)
        Me.btn_show.Name = "btn_show"
        Me.btn_show.Size = New System.Drawing.Size(75, 29)
        Me.btn_show.TabIndex = 3
        Me.btn_show.Text = "Mostrar"
        Me.btn_show.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 19)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Descripción"
        '
        'tbDescrip
        '
        Me.tbDescrip.Location = New System.Drawing.Point(137, 79)
        Me.tbDescrip.Multiline = True
        Me.tbDescrip.Name = "tbDescrip"
        Me.tbDescrip.Size = New System.Drawing.Size(443, 59)
        Me.tbDescrip.TabIndex = 5
        '
        'btn_save
        '
        Me.btn_save.Location = New System.Drawing.Point(188, 167)
        Me.btn_save.Name = "btn_save"
        Me.btn_save.Size = New System.Drawing.Size(75, 47)
        Me.btn_save.TabIndex = 6
        Me.btn_save.Text = "Guardar"
        Me.btn_save.UseVisualStyleBackColor = True
        '
        'btn_cancelar
        '
        Me.btn_cancelar.Location = New System.Drawing.Point(299, 167)
        Me.btn_cancelar.Name = "btn_cancelar"
        Me.btn_cancelar.Size = New System.Drawing.Size(75, 47)
        Me.btn_cancelar.TabIndex = 7
        Me.btn_cancelar.Text = "Cancelar"
        Me.btn_cancelar.UseVisualStyleBackColor = True
        '
        'form_descripciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.ClientSize = New System.Drawing.Size(605, 239)
        Me.Controls.Add(Me.btn_cancelar)
        Me.Controls.Add(Me.btn_save)
        Me.Controls.Add(Me.tbDescrip)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btn_show)
        Me.Controls.Add(Me.tbLote)
        Me.Controls.Add(Me.Label1)
        Me.Name = "form_descripciones"
        Me.Text = "Actualizar Descripciones"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbLote As System.Windows.Forms.TextBox
    Friend WithEvents btn_show As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbDescrip As System.Windows.Forms.TextBox
    Friend WithEvents btn_save As System.Windows.Forms.Button
    Friend WithEvents btn_cancelar As System.Windows.Forms.Button

End Class
