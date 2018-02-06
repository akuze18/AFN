Public Class form_activar_lotes

    Private Sub form_activar_lotes_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        With DGLotes
            .DataSource = cabecera()
            .RowHeadersWidth = 25
            .Columns(0).Width = 120
            .Columns(1).Width = 600
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
            .MultiSelect = True
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeColumns = True
            .AllowUserToOrderColumns = False
            .EditMode = DataGridViewEditMode.EditProgrammatically
        End With
        cargar_lotes()
    End Sub

    Public Function cabecera() As DataTable
        Dim tabla As New DataTable
        tabla.Columns.Add("Codigo Lote")
        tabla.Columns.Add("Descripción")
        'tabla.Columns.Add("Activo", Type.GetType("System.Int32"))
        Return tabla
    End Function

    Private Sub cargar_lotes()
        Dim datos As DataTable
        Dim abiertos As lote_articulos
        abiertos = base.getLotesAbiertos
        datos = DGLotes.DataSource
        datos.Rows.Clear()
        For Each fila As lote_articulos.fila In abiertos.Rows
            'agrego filas
            Dim newdato As DataRow = datos.NewRow
            newdato(0) = fila.cod
            newdato(1) = fila.descripcion
            'newdato(2) = True
            datos.Rows.Add(newdato)
        Next
    End Sub

    Private Sub DGLotes_ColumnWidthChanged(sender As Object, e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles DGLotes.ColumnWidthChanged
        'base.mensaje_alerta(e.Column.Width)
    End Sub

    Private Sub btn_act_all_Click(sender As System.Object, e As System.EventArgs) Handles btn_act_all.Click
        Dim todas = DGLotes.Rows
        If todas.Count = 0 Then
            base.mensaje_alerta("No hay registros para activar")
        Else
            Dim AllLotes = base.getLotesAbiertos
            For Each fila As DataGridViewRow In todas
                Dim OLote = AllLotes.FIND_by_COD(fila.Cells(0).Value)
                OLote.activar()
                OLote.save(base.maestro)
            Next
            base.mensaje_alerta("Todos los Lotes fueron activados")
            cargar_lotes()
        End If
    End Sub

    Private Sub btn_act_sel_Click(sender As System.Object, e As System.EventArgs) Handles btn_act_sel.Click
        Dim todas = DGLotes.SelectedRows
        If todas.Count = 0 Then
            base.mensaje_alerta("No hay registros para activar")
        Else
            Dim AllLotes = base.getLotesAbiertos
            For Each fila As DataGridViewRow In todas
                Dim OLote = AllLotes.FIND_by_COD(fila.Cells(0).Value)
                OLote.activar()
                OLote.save(base.maestro)
            Next
            base.mensaje_alerta("Los Lotes seleccionados fueron activados")
            cargar_lotes()
        End If
    End Sub
End Class
