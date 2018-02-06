Public Class lote_articulos
    Inherits DataTable

    Protected _origen_data As String = "AFN_LOTE_ARTICULOS"

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

    Public Function FIND_by_COD(ByVal cod As Integer) As fila
        Dim seleccionada As fila
        seleccionada = Nothing
        For Each sRow As fila In Me.Rows
            If sRow.cod = cod Then
                seleccionada = sRow
            End If
        Next
        Return seleccionada
    End Function

    Public Class fila
        Inherits DataRow

        Private update As New List(Of String)
        Private variable As String = ""

        Private MyDataTable As lote_articulos

        Friend Sub New(builder As DataRowBuilder)
            MyBase.New(builder)
            MyDataTable = CType(MyBase.Table, lote_articulos)
        End Sub

        Public Overrides Function toString() As String
            Return descripcion
        End Function

        Public ReadOnly Property cod As Integer
            Get
                Return Strings.Trim(CType(MyBase.Item("cod"), Integer))
            End Get
        End Property

        Public Property estado_aprov As String
            Get
                Return Strings.Trim(CType(MyBase.Item("estado_aprov"), String))
            End Get
            Set(value As String)
                MyBase.Item("estado_aprov") = value
                update.Add("estado_aprov")
            End Set
        End Property

        Public ReadOnly Property descripcion As String
            Get
                Return Strings.Trim(CType(MyBase.Item("descripcion"), String))
            End Get
        End Property
        Public ReadOnly Property fecha_compra As Date
            Get
                Return Strings.Trim(CType(MyBase.Item("fecha_compra"), Date))
            End Get
        End Property
        Public ReadOnly Property proveedor As String
            Get
                Return Strings.Trim(CType(MyBase.Item("proveedor"), String))
            End Get
        End Property
        Public ReadOnly Property num_doc As String
            Get
                Return Strings.Trim(CType(MyBase.Item("num_doc"), String))
            End Get
        End Property
        Public ReadOnly Property precio_inicial As Decimal
            Get
                Return Strings.Trim(CType(MyBase.Item("precio_inicial"), Decimal))
            End Get
        End Property
        Public ReadOnly Property vida_util_inicial As Integer
            Get
                Return Strings.Trim(CType(MyBase.Item("vida_util_inicial"), Integer))
            End Get
        End Property
        Public ReadOnly Property derecho_credito As String
            Get
                Return Strings.Trim(CType(MyBase.Item("derecho_credito"), String))
            End Get
        End Property
        Public ReadOnly Property fecha_ing As Date
            Get
                Return Strings.Trim(CType(MyBase.Item("fecha_ing"), Date))
            End Get
        End Property
        Public ReadOnly Property origen As String
            Get
                Return Strings.Trim(CType(MyBase.Item("origen"), String))
            End Get
        End Property
        Public ReadOnly Property consistencia As String
            Get
                Return Strings.Trim(CType(MyBase.Item("consistencia"), String))
            End Get
        End Property


        Public Function activar() As Boolean
            Me.estado_aprov = "CLOSE"
            Return True
        End Function

        Public Function save(ByRef operar As master_control) As Boolean
            If update.Count > 0 Then
                variable = "UPDATE " + MyDataTable._origen_data + " SET "
                For i = 0 To update.Count - 1
                    variable = variable + update(i) + " = '" + MyBase.Item(update(i)) + "'"
                Next
                variable = variable + " WHERE cod='" + Me.cod.ToString + "'"
                operar.ejecuta(variable)
                Return (True)
            Else
                Return False
            End If
        End Function
    End Class

End Class
