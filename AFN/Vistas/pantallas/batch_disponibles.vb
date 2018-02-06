Public Class batch_disponibles
    Private _elegido As datos

    Private Class datos
        Dim _batch As Integer
        Dim _monto As Integer
        Public Sub New(ByVal batch As Integer, ByVal monto As Decimal)
            _batch = batch
            _monto = monto
        End Sub
        Public Overrides Function ToString() As String
            Return "Borrador " + _batch.ToString + " :" + Space(10 - _batch.ToString.Length) + "$" + _monto.ToString("#,###")
        End Function
        Public ReadOnly Property resultado As Integer
            Get
                Return _batch
            End Get
        End Property

    End Class

    Public Sub New(ByVal origen As DataTable)
        InitializeComponent()
        For Each fila As DataRow In origen.Rows
            Dim _info As New datos(fila("id_batch"), fila("monto"))
            ListBox1.Items.Add(_info)
        Next
    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As System.EventArgs) Handles ListBox1.DoubleClick
        _elegido = ListBox1.SelectedItem
        DialogResult = Windows.Forms.DialogResult.Yes
    End Sub

    Public ReadOnly Property resultado As Integer
        Get
            Return _elegido.resultado
        End Get
    End Property
End Class