
Public Class form_etiquetas
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private base As New base_AFN
    
    Private lineaActual As Integer

    Private _datos As DataTable

    Private _imprimirPD As ImprimirPD

    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub form_etiquetas_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub

    Private Sub form_etiquetas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dgv_resultado.AllowUserToAddRows = False
        dgv_resultado.AllowUserToDeleteRows = False
        dgv_resultado.AllowUserToResizeColumns = True
        dgv_resultado.AllowUserToResizeRows = False
        dgv_resultado.MultiSelect = False
        dgv_resultado.RowHeadersVisible = False

        dgv_resultado.DataSource = base.cabecera_imprimir_etiquetas 'maestro.ejecuta(sql_base)

        dgv_resultado.EditMode = DataGridViewEditMode.EditProgrammatically
        dgv_resultado.Columns(0).Width = 20
        dgv_resultado.Columns(0).Frozen = True
        dgv_resultado.Columns(1).Width = 100
        dgv_resultado.Columns(2).Width = 100
        dgv_resultado.Columns(3).Width = 200
        dgv_resultado.Columns(4).Width = 100
        dgv_resultado.Columns(5).Width = 70
        dgv_resultado.Columns(6).Width = 70
        dgv_resultado.Columns(7).Width = 70
        dgv_resultado.Columns(8).Width = 70
        dgv_resultado.Columns(9).Width = 80
        'txtCant.Text = 1
        btn_imprimir.Text = "Im&primir"

        _imprimirPD = New ImprimirPD(base.fileFontBarcode, base.fileFontLabel)

    End Sub

    Private Sub btn_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_add.Click
        Dim contar As Integer
        contar = 0
        For Each elementos As Control In Me.Controls
            If TypeOf elementos Is multicriterio Then
                contar = contar + 1
            End If
        Next
        If contar < 4 Then
            Dim nuevo_filtro As New multicriterio
            Dim cordY As Integer
            Me.Controls.Add(nuevo_filtro)
            nuevo_filtro.Name = "Multicriterio" + (contar + 1).ToString
            nuevo_filtro.Visible = True
            cordY = btn_add.Location.Y + nuevo_filtro.Height + 5
            nuevo_filtro.Location = New Point(Multicriterio1.Location.X, cordY)
            btn_add.Location = New Point(btn_add.Location.X, cordY)
            btn_less.Location = New Point(btn_less.Location.X, cordY)
            btn_find.Location = New Point(btn_find.Location.X, cordY)
            'Else
            '    MessageBox.Show("ha excedido el maximo de controles")
        End If
    End Sub

    Private Sub btn_less_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_less.Click
        Dim contar As Integer
        contar = 0
        For Each elementos As Control In Me.Controls
            If TypeOf elementos Is multicriterio Then
                contar = contar + 1
            End If
        Next
        If contar > 1 Then
            Dim cordY As Integer
            Dim elemento As Control
            elemento = Me.Controls.Item("Multicriterio" + contar.ToString)
            cordY = btn_add.Location.Y - elemento.Height - 5
            btn_add.Location = New Point(btn_add.Location.X, cordY)
            btn_less.Location = New Point(btn_less.Location.X, cordY)
            btn_find.Location = New Point(btn_find.Location.X, cordY)
            Me.Controls.Remove(elemento)
        Else
            Dim elemento As Control
            elemento = Me.Controls.Item("Multicriterio" + contar.ToString)
            Dim final As multicriterio = CType(elemento, multicriterio)
            final.cbCampo.SelectedIndex = -1
        End If
    End Sub

    Private Sub btn_find_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_find.Click
        Dim myWhere As New List(Of multicriterio)
        For Each elementos As Control In Me.Controls
            If TypeOf elementos Is multicriterio Then
                Dim criterio As multicriterio = CType(elementos, multicriterio)
                If String.IsNullOrEmpty(criterio.filtro) Then
                    MessageBox.Show("Debe completar todos los criterios para realizar la busqueda", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    criterio.Focus()
                    Exit Sub
                Else
                    myWhere.Add(criterio)
                End If
            End If
        Next
        dgv_resultado.DataSource = base.INV_CONSULTA_ETIQUETAS(myWhere)
        dgv_resultado.ClearSelection()
        Label1.Text = Strings.Left(Label1.Text, Strings.InStr(Label1.Text, ":")) + " " + dgv_resultado.Rows.Count.ToString
    End Sub

    Private Sub dgv_resultado_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_resultado.CellClick
        If e.ColumnIndex = 0 Then
            If e.RowIndex > -1 Then
                dgv_resultado.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Not (dgv_resultado.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
            End If
        End If
        dgv_resultado.ClearSelection()
    End Sub

    Private Sub dgv_resultado_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_resultado.CellDoubleClick
        dgv_resultado.ClearSelection()
    End Sub

    Private Sub btn_imprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_imprimir.Click
        'imprimir_etiqueta()
        ' Imprimir
        ' imprimir o mostrar el PrintPreview
        '
        'valido campos
        If dgv_resultado.Rows.Count <= 0 Then
            MessageBox.Show("No se han encontrado valores para la busqueda indicada", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim tabla As DataTable = dgv_resultado.DataSource
        _datos = tabla.Copy
        For i = tabla.Rows.Count - 1 To 0 Step -1
            Dim fila As DataRow = tabla.Rows(i)
            If Not fila(0) Then
                _datos.Rows.RemoveAt(i)
            End If
        Next
        If _datos.Rows.Count = 0 Then
            MessageBox.Show("No se han seleccionado filas de la busqueda para imprimir", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        _imprimirPD.imprimir(_datos, chkSelAntes.Checked, False)
    End Sub

    Private Sub imprimir_etiqueta()
        'valido campos
        If dgv_resultado.Rows.Count <= 0 Then
            MessageBox.Show("No se han encontrado valores para la busqueda indicada", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim contar_imprimir As Integer = 0

        For Each fila As DataGridViewRow In dgv_resultado.Rows
            If fila.Cells(0).Value Then
                contar_imprimir = contar_imprimir + 1
            End If
        Next
        If contar_imprimir = 0 Then
            MessageBox.Show("No se han seleccionado filas de la busqueda para imprimir", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim strMascara, strOld, strDescrip, strLoteA As String
        Dim strFecha As Date
        Dim cantidad As Integer
        cantidad = 1
        For Each fila As DataGridViewRow In dgv_resultado.Rows
            'fila.Cells(0).Value indica si esta checked, por lo tanto se imprime
            If fila.Cells(0).Value Then
                strMascara = fila.Cells(1).Value.ToString       'producto
                strOld = fila.Cells(2).Value.ToString           'codigo_old
                strDescrip = fila.Cells(3).Value.ToString       'descripcion
                strFecha = fila.Cells(4).Value                  'fecha_compra
                strLoteA = fila.Cells(9).Value                  'lote_articulo
                Dim Imprimir As New ImpresionZPL
                For i = 1 To cantidad
                    Imprimir.EtiquetaActivoFijo(strMascara, strDescrip, strFecha.ToString("dd/MM/yyyy"), strLoteA, strOld)
                Next
                'Sleep(500)
            Else
                'no imprimir, no se hace nada
            End If
        Next
    End Sub


    Private Sub form_etiquetas_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        dgv_resultado.Size = New Size(Me.Width - 41, Me.Height - 342)
        Dim ypos As Integer
        ypos = dgv_resultado.Location.Y + dgv_resultado.Size.Height + 26
        With btn_imprimir
            .Location = New Point(.Location.X, ypos)
        End With
        With chkSelAntes
            .Location = New Point(.Location.X, ypos)
        End With
        With btnSelImpresora
            .Location = New Point(.Location.X, ypos)
        End With
    End Sub

    Private Sub dgv_resultado_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_resultado.CellContentClick
        If e.ColumnIndex = 0 And e.RowIndex = -1 Then
            For Each fila As DataGridViewRow In dgv_resultado.Rows
                fila.Cells(0).Value = Not (fila.Cells(0).Value)
            Next
        End If
    End Sub

    
    Private Sub btnSelImpresora_Click(sender As System.Object, e As System.EventArgs) Handles btnSelImpresora.Click
        _imprimirPD.seleccionarImpresora()
    End Sub

End Class