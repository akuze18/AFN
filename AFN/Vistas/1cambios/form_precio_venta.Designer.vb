<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class form_precio_venta
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form_precio_venta))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Tdocumento = New System.Windows.Forms.TextBox()
        Me.DTfecha = New System.Windows.Forms.DateTimePicker()
        Me.Lestado_doc = New System.Windows.Forms.Label()
        Me.detalle_venta = New System.Windows.Forms.DataGridView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TprecioExt = New System.Windows.Forms.TextBox()
        Me.btn_guardar = New System.Windows.Forms.Button()
        CType(Me.detalle_venta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Documento"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Fecha Doc."
        '
        'Tdocumento
        '
        Me.Tdocumento.Location = New System.Drawing.Point(121, 27)
        Me.Tdocumento.Name = "Tdocumento"
        Me.Tdocumento.Size = New System.Drawing.Size(160, 20)
        Me.Tdocumento.TabIndex = 2
        '
        'DTfecha
        '
        Me.DTfecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTfecha.Location = New System.Drawing.Point(122, 62)
        Me.DTfecha.Name = "DTfecha"
        Me.DTfecha.Size = New System.Drawing.Size(159, 20)
        Me.DTfecha.TabIndex = 3
        '
        'Lestado_doc
        '
        Me.Lestado_doc.AutoSize = True
        Me.Lestado_doc.Location = New System.Drawing.Point(310, 30)
        Me.Lestado_doc.Name = "Lestado_doc"
        Me.Lestado_doc.Size = New System.Drawing.Size(39, 13)
        Me.Lestado_doc.TabIndex = 4
        Me.Lestado_doc.Text = "Label3"
        '
        'detalle_venta
        '
        Me.detalle_venta.AllowUserToAddRows = False
        Me.detalle_venta.AllowUserToDeleteRows = False
        Me.detalle_venta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.detalle_venta.Location = New System.Drawing.Point(12, 102)
        Me.detalle_venta.Name = "detalle_venta"
        Me.detalle_venta.ReadOnly = True
        Me.detalle_venta.Size = New System.Drawing.Size(697, 126)
        Me.detalle_venta.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(486, 250)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Total Documento"
        '
        'TprecioExt
        '
        Me.TprecioExt.Location = New System.Drawing.Point(581, 247)
        Me.TprecioExt.Name = "TprecioExt"
        Me.TprecioExt.Size = New System.Drawing.Size(128, 20)
        Me.TprecioExt.TabIndex = 7
        '
        'btn_guardar
        '
        Me.btn_guardar.Location = New System.Drawing.Point(313, 280)
        Me.btn_guardar.Name = "btn_guardar"
        Me.btn_guardar.Size = New System.Drawing.Size(80, 34)
        Me.btn_guardar.TabIndex = 8
        Me.btn_guardar.Text = "Guardar"
        Me.btn_guardar.UseVisualStyleBackColor = True
        '
        'form_precio_venta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(721, 326)
        Me.Controls.Add(Me.btn_guardar)
        Me.Controls.Add(Me.TprecioExt)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.detalle_venta)
        Me.Controls.Add(Me.Lestado_doc)
        Me.Controls.Add(Me.DTfecha)
        Me.Controls.Add(Me.Tdocumento)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "form_precio_venta"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ingreso de factura de venta de Activo Fijo"
        CType(Me.detalle_venta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Tdocumento As System.Windows.Forms.TextBox
    Friend WithEvents DTfecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Lestado_doc As System.Windows.Forms.Label
    Friend WithEvents detalle_venta As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TprecioExt As System.Windows.Forms.TextBox
    Friend WithEvents btn_guardar As System.Windows.Forms.Button
End Class
