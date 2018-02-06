Public Class listado_basico
    Inherits DataTable

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
            If sRow.COD = cod Then
                seleccionada = sRow
            End If
        Next
        Return seleccionada
    End Function

    Public Class fila
        Inherits DataRow

        Friend Sub New(builder As DataRowBuilder)
            MyBase.New(builder)
        End Sub

        Public Overrides Function toString() As String
            Return DESCRIP
        End Function

        Public ReadOnly Property COD As String
            Get
                Return Strings.Trim(CType(MyBase.Item("COD"), String))
            End Get
        End Property

        Public ReadOnly Property MONEDA As String
            Get
                Return Strings.Trim(CType(MyBase.Item("MONEDA"), String))
            End Get
        End Property
        Public ReadOnly Property AMBIENTE As String
            Get
                Return Strings.Trim(CType(MyBase.Item("AMBIENTE"), String))
            End Get
        End Property

        Public ReadOnly Property DESC_AMBIENTE As String
            Get
                Select Case AMBIENTE
                    Case "FIN"
                        Return "Financiero"
                    Case "TRIB"
                        Return "Tributario"
                    Case "IFRS"
                        Return "IFRS"
                    Case Else
                        Return ""
                End Select
            End Get
        End Property

        Public ReadOnly Property DB_AMBIENTE As String
            Get
                Select Case MONEDA
                    Case "CLP"
                        Select Case AMBIENTE
                            Case "FIN"
                                Return "F"
                            Case "TRIB"
                                Return "T"
                            Case "IFRS"
                                Return "GC"
                            Case Else
                                Return ""
                        End Select
                    Case "YEN"
                        Select Case AMBIENTE
                            Case "FIN"
                                Return "Y"
                            Case "TRIB"
                                Return ""   'NO ESTA DEFINIDO AUN
                            Case "IFRS"
                                Return "GY"
                            Case Else
                                Return ""
                        End Select
                    Case Else
                        Return ""
                End Select

            End Get
        End Property

        Public ReadOnly Property DESCRIP As String
            Get
                Return MONEDA + " - " + DESC_AMBIENTE
            End Get
        End Property

    End Class

End Class
