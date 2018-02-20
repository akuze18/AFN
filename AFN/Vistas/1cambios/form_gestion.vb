Public Class form_gestion
    Private Sub preparar_form(Optional ByVal lote As Nullable(Of Integer) = Nothing)
        Dim inicial As Boolean
        inicial = IsNothing(lote)

        Nparte.Enabled = Not (inicial)
        TBcod.Enabled = inicial

        'base.lista_cambio.Select()

        Dim info As gestion = base.GESTIONES
        With CBgestion
            .Items.Clear()
            .Items.AddRange(info.GetAllArray)
            '.Items.AddRange(info.Select())
            .SelectedIndex = -1
        End With
        LbFechas.Items.Clear()

        If (inicial) Then
            TBcod.Text = String.Empty
            With Nparte
                .Minimum = 0
                .Maximum = 0
                .Value = 0
            End With
        Else
            Dim articulo As lote_articulos.fila = base.getInfoLote(lote)
            For i = 0 To 20
                LbFechas.Items.Add(articulo.fecha_ing)
            Next

        End If
    End Sub

    Private Function validar_grupo(ByVal id_grupo As Integer) As Boolean
        For Each cntl As Control In Me.Controls
            If cntl.Tag <= id_grupo Then
                If TypeOf cntl Is TextBox Then
                    Dim TbActual As TextBox = cntl
                    If String.IsNullOrEmpty(TbActual.Text) Then
                        Mensaje_Err("Debe indicar un valor para " + TbActual.AccessibleName)
                        TbActual.Focus()
                        Return False
                    End If
                End If
            End If
        Next
        Return True
    End Function

    Private Sub form_gestion_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load, BtnLimpiar.Click
        TBcod.Tag = 1
        Nparte.Tag = 2
        With LbFechas
            .Tag = 2
            .ColumnWidth = .Width / 4
        End With

        CBgestion.Tag = 2
        preparar_form()
    End Sub

    Private Sub BtnBuscar_Click(sender As System.Object, e As System.EventArgs) Handles BtnBuscar.Click
        If Not (validar_grupo(1)) Then
            Exit Sub
        End If
        preparar_form(TBcod.Text)
    End Sub
End Class
