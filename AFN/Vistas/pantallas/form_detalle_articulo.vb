Public Class form_detalle_articulo
    Inherits System.Windows.Forms.Form

    Public Enum form_accion
        cambio = 1
        castigo = 2
        venta = 3
    End Enum

#Region "Variables de Clase"
    Private _accion As form_accion
    Private _id As Integer
    Private _parte As Integer
    Private _detalle As DataTable
    Private _cantidad As Integer
    ''' <summary>
    ''' Instancia del forumario que contiene toda la logica del proceso
    ''' </summary>
    ''' <remarks></remarks>
    Private base As New base_AFN
#End Region

#Region "Constructor"
    Public Sub New(ByVal id As Integer, ByVal parte As Integer, ByVal accion As form_accion, ByVal mydetalle As DataTable)
        InitializeComponent()
        _accion = accion
        _id = id
        _parte = parte
        _detalle = mydetalle.Copy()
        _cantidad = cantidad_detalle()

        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
#End Region

#Region "Del Formulario"
    Private Sub form_detalle_articulo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Select Case _accion
            Case form_accion.cambio
                Me.Text = "Detalle de artículos para cambiar de zona/subzona"
            Case form_accion.castigo
                Me.Text = "Detalle de artículos para castigar"
            Case form_accion.venta
                Me.Text = "Detalle de artículos para vender"
        End Select
        TB_cod_lote.Text = _id
        TB_cod_lote.Enabled = False
        LBdescrip.Text = base.articulo_descrip(_id)
        DG_articulos.DataSource = _detalle
        DG_articulos.RowHeadersVisible = False
        DG_articulos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DG_articulos.AllowUserToResizeColumns = False
        For Each columna As DataGridViewColumn In DG_articulos.Columns
            columna.SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        btn_less.Text = "Sel Inf"
        btn_top.Text = "Sel Sup"
        btn_clear.Text = "Borrar"
        mark_total.Text = "/ " + _cantidad.ToString
        mark_actual.Text = cantidad_detalle()
    End Sub
    Private Function cantidad_detalle() As Integer
        Return _detalle.Select("[Procesar]=1").Length
    End Function
#End Region

#Region "De los Controles"

    Private Sub DG_articulos_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DG_articulos.CellClick
        Dim LAtrib As DataGridView = CType(sender, DataGridView)
        Dim fil, col, col_procesar As Integer
        col_procesar = 2
        fil = e.RowIndex
        col = e.ColumnIndex
        If fil <> -1 And col = col_procesar Then
            Dim valor As Boolean = LAtrib.Rows(fil).Cells(col).Value
            If valor Then
                mark_actual.Text = cantidad_detalle() - 1
            Else
                mark_actual.Text = cantidad_detalle() + 1
            End If
            LAtrib.Rows(fil).Cells(col).Value = Not valor
            RBclear.Focus()
        End If
    End Sub

    Private Sub btn_less_Click(sender As System.Object, e As System.EventArgs) Handles btn_less.Click
        Dim cont As Integer
        cont = 0
        For Each fila As DataRow In _detalle.Rows
            If cont < _detalle.Rows.Count - _cantidad Then
                fila(2) = 0
            Else
                fila(2) = 1
            End If
            cont = cont + 1
        Next
        mark_actual.Text = _cantidad
    End Sub

    Private Sub btn_top_Click(sender As System.Object, e As System.EventArgs) Handles btn_top.Click
        Dim cont As Integer
        cont = 0
        For Each fila As DataRow In _detalle.Rows
            If cont < _cantidad Then
                fila(2) = 1
            Else
                fila(2) = 0
            End If
            cont = cont + 1
        Next
        mark_actual.Text = _cantidad
    End Sub

    Private Sub btn_clear_Click(sender As System.Object, e As System.EventArgs) Handles btn_clear.Click
        For Each fila As DataRow In _detalle.Rows
            fila(2) = 0
        Next
        mark_actual.Text = 0
    End Sub

    Private Sub btn_guardar_Click(sender As System.Object, e As System.EventArgs) Handles btn_guardar.Click
        'Valido que los ticket existentes corresponden a la cantidad requerida
        Dim cant_actual As Integer
        cant_actual = cantidad_detalle()
        If cant_actual <> _cantidad Then
            Dim mensaje As String
            mensaje = "No ha indicado la cantidad de registros necesarios para "
            Select Case _accion
                Case form_accion.cambio
                    mensaje = mensaje + "cambiar de zona/subzona"
                Case form_accion.castigo
                    mensaje = mensaje + "castigar"
                Case form_accion.venta
                    mensaje = mensaje + "vender"
                Case Else
                    mensaje = mensaje + "procesar"
            End Select
            MessageBox.Show(mensaje, "NH Foods Chile", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub
#End Region

#Region "Retornos para solicitantes"
    Public ReadOnly Property detalle As DataTable
        Get
            If DialogResult = Windows.Forms.DialogResult.OK Then
                Return _detalle.Copy()
            Else
                Return New DataTable
            End If
        End Get
    End Property

#End Region

End Class