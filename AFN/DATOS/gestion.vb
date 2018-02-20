Public Class gestion
    Inherits DataTable

    Protected _origen_data As String = "AFN_GESTION"

    Protected Overrides Function GetRowType() As Type
        Return GetType(fila)
    End Function

    Protected Overrides Function NewRowFromBuilder(builder As DataRowBuilder) As DataRow
        Return New fila(builder)
    End Function

    Public Sub New(ByVal datos As DataTable)
        For Each columna As DataColumn In datos.Columns
            Columns.Add(columna.ColumnName, columna.DataType)
        Next
        For Each fila As DataRow In datos.Rows
            ImportRow(fila)
        Next
    End Sub

    Default Public ReadOnly Property Item(idx As Integer) As fila
        Get
            Return DirectCast(Rows(idx), fila)
        End Get
    End Property
    Public Sub Add(row As fila)
        Rows.Add(row)
    End Sub
    Public Sub Remove(row As fila)
        Rows.Remove(row)
    End Sub
    Public Shadows Function NewRow() As fila
        Dim row As fila = CType(NewRow(), fila)

        Return row
    End Function
    Public Function GetNewRow() As fila
        Dim row As fila = CType(NewRow(), fila)

        Return row
    End Function

    Public Function FIND_by_COD(ByVal cod As String) As fila
        Dim seleccionada As fila
        seleccionada = Nothing
        For Each sRow As fila In Me.Rows
            If sRow.CODE = cod Then
                seleccionada = sRow
            End If
        Next
        Return seleccionada
    End Function


    Public Function GetAllArray() As fila()
        Dim contenido As New List(Of fila)
        For Each fil In Me.Rows
            contenido.Add(fil)
        Next
        Return contenido.ToArray
    End Function

    Public Class fila
        Inherits DataRow

        Friend Sub New(builder As DataRowBuilder)
            MyBase.New(builder)
        End Sub

        Public Overrides Function toString() As String
            Return DESCRIP
        End Function

        Public ReadOnly Property CODE As String
            Get
                Return Strings.Trim(CType(MyBase.Item("code"), String))
            End Get
        End Property

        Public ReadOnly Property NOMBRE As String
            Get
                Return Strings.Trim(CType(MyBase.Item("nombre"), String))
            End Get
        End Property
        Public ReadOnly Property ID As Integer
            Get
                Return CType(MyBase.Item("id"), Integer)
            End Get
        End Property

        Public ReadOnly Property DESCRIP As String
            Get
                Return NOMBRE
            End Get
        End Property

    End Class

End Class
