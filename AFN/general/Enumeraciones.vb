Public Class valores
    Private cod As String
    Private txt As String

    Public Sub New(ByVal codigo As String, ByVal texto As String)
        cod = codigo
        txt = texto
    End Sub

    Public Overrides Function ToString() As String
        Return cod
    End Function

    Public ReadOnly Property codigo As String
        Get
            Return cod
        End Get
    End Property

    Public ReadOnly Property texto As String
        Get
            Return txt
        End Get
    End Property
End Class

Public Class moneda
    Shared vals As valores() = New valores() {
                                New valores("CLP", "Reportes CLP"),
                                New valores("YEN", "Reportes YEN"),
                                New valores("MIX", "Reportes Especiales")
                            }

    Shared ReadOnly Property CLP As String
        Get
            Return vals(0).codigo
        End Get
    End Property
    Shared ReadOnly Property YEN As String
        Get
            Return vals(1).codigo
        End Get
    End Property
    Shared ReadOnly Property MIX As String
        Get
            Return vals(2).codigo
        End Get
    End Property

    Shared Function Items() As valores()
        Return vals
    End Function
End Class

Public Class ambiente

    Shared vals As valores() = New valores() {New valores("FIN", "Financiero"), _
                              New valores("IFRS", "IFRS"), _
                              New valores("TRIB", "Tributario")}
    Shared val_special As valores() = New valores() {
        New valores("ESP1", "Resumen para Package")}

    Shared ReadOnly Property FIN As String
        Get
            Return vals(0).codigo
        End Get
    End Property
    Shared ReadOnly Property IFRS As String
        Get
            Return vals(1).codigo
        End Get
    End Property
    Shared ReadOnly Property TRIB As String
        Get
            Return vals(2).codigo
        End Get
    End Property

    Shared Function Items(ByVal moneda As String) As valores()
        Select Case moneda
            Case Global.AFN.moneda.YEN
                Return New valores() {vals(0), vals(1)}
            Case Global.AFN.moneda.MIX
                Return val_special
            Case Else
                Return vals
        End Select
    End Function
End Class

Public Class vista

    Shared vals As valores() = New valores() {
                                New valores("D", "Detalle"),
                                New valores("C", "Resumen por Clase"),
                                New valores("Z", "Resumen por Zona"),
                                New valores("DI", "Detalle con Inventario")}

    Shared ReadOnly Property D As String
        Get
            Return vals(0).codigo
        End Get
    End Property
    Shared ReadOnly Property C As String
        Get
            Return vals(1).codigo
        End Get
    End Property
    Shared ReadOnly Property Z As String
        Get
            Return vals(2).codigo
        End Get
    End Property
    Shared ReadOnly Property DI As String
        Get
            Return vals(3).codigo
        End Get
    End Property

    Shared Function Items(ByVal ambiente As String, ByVal moneda As String) As valores()
        Select Case moneda
            Case Global.AFN.moneda.MIX
                Return New valores() {}
            Case Global.AFN.moneda.CLP
                Select Case ambiente
                    Case Global.AFN.ambiente.FIN
                        Return vals
                    Case Else
                        Return New valores() {vals(0), vals(1), vals(2)}
                End Select
            Case Else
                Return New valores() {vals(0), vals(1), vals(2)}
        End Select
    End Function
End Class

