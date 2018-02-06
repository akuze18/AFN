Public Class form_descripciones

    Private Sub form_descripciones_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        editing = False
    End Sub

    Private Property editing As Boolean
        Get
            Return Not (tbLote.Enabled)
        End Get
        Set(value As Boolean)
            tbDescrip.Enabled = value
            tbLote.Enabled = Not (value)
        End Set
    End Property

    Private Function getInfo() As lote_articulos.fila
        
        If (String.IsNullOrEmpty(tbLote.Text)) Then
            base.mensaje_alerta("Debe ingresar un codigo de lote")
            tbLote.Focus()
            Return Nothing
        End If
        If (Val(tbLote.Text)) <= 0 Then
            base.mensaje_alerta("Debe indicar un valor numerico")
            tbLote.Focus()
            Return Nothing
        End If

        If editing Then
            If (String.IsNullOrEmpty(tbDescrip.Text)) Then
                base.mensaje_alerta("Debe ingresar la descripción del lote")
                tbDescrip.Focus()
                Return Nothing
            End If
        End If


        Dim codigo As Integer = Val(tbLote.Text)
        Dim record = base.getInfoLote(codigo)
        If (IsNothing(record)) Then
            base.mensaje_alerta("Código ingresado no corresponde a ningun artículo registrado")
            Return Nothing
        End If
        Return record
    End Function

   
    Private Sub btn_show_Click(sender As System.Object, e As System.EventArgs) Handles btn_show.Click
        Dim record = getInfo()
        If Not (IsNothing(record)) Then
            editing = True
            With tbDescrip
                .Text = record.descripcion
            End With
        End If
    End Sub


    Private Sub btn_save_Click(sender As System.Object, e As System.EventArgs) Handles btn_save.Click
        If Not editing Then
            Exit Sub
        End If
        Dim record = getInfo()
        If Not (IsNothing(record)) Then
            Dim resultado As Boolean
            resultado = base.actualiza_descripcion_lote(record.cod, tbDescrip.Text)
            If (resultado) Then
                base.mensaje_alerta("Se ha cambiado exitosamente la descripción del código")
                editing = False
                With tbDescrip
                    .Text = String.Empty
                End With
                With tbLote
                    .Text = String.Empty
                    .Focus()
                End With
            Else
                base.mensaje_alerta("Se produjo un error al actualizar la descripción del código")
            End If
        End If
    End Sub

    
    Private Sub btn_cancelar_Click(sender As System.Object, e As System.EventArgs) Handles btn_cancelar.Click
        editing = False
        tbDescrip.Text = String.Empty
        tbLote.Text = String.Empty
    End Sub
End Class
