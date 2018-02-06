Public Class control_cm
    Public Sub New()

        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    Public Sub New(ByVal indice As Integer)
        ' Llamada necesaria para el diseñador.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Me.Tag = indice
        Me.Name = "control_cm" + indice.ToString
        Select Case indice
            Case 1
                icono.Image = My.Resources.p1
            Case 2
                icono.Image = My.Resources.p2
            Case 3
                icono.Image = My.Resources.p3
            Case 4
                icono.Image = My.Resources.p4
            Case 5
                icono.Image = My.Resources.p5
            Case 6
                icono.Image = My.Resources.p6
            Case 7
                icono.Image = My.Resources.p7
            Case 8
                icono.Image = My.Resources.p8
            Case 9
                icono.Image = My.Resources.p9
            Case 10
                icono.Image = My.Resources.p10
            Case 11
                icono.Image = My.Resources.p11
            Case 12
                icono.Image = My.Resources.p12
            Case Else
                icono.Image = My.Resources.p0
                Me.Tag = 0
        End Select
        valor.Text = String.Empty
    End Sub
    Public Sub New(ByVal indice As Integer, ByVal predetermindado As String)
        ' Llamada necesaria para el diseñador.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Me.Tag = indice
        Me.Name = "control_cm" + indice.ToString
        Select Case indice
            Case 1
                icono.Image = My.Resources.p1
            Case 2
                icono.Image = My.Resources.p2
            Case 3
                icono.Image = My.Resources.p3
            Case 4
                icono.Image = My.Resources.p4
            Case 5
                icono.Image = My.Resources.p5
            Case 6
                icono.Image = My.Resources.p6
            Case 7
                icono.Image = My.Resources.p7
            Case 8
                icono.Image = My.Resources.p8
            Case 9
                icono.Image = My.Resources.p9
            Case 10
                icono.Image = My.Resources.p10
            Case 11
                icono.Image = My.Resources.p11
            Case 12
                icono.Image = My.Resources.p12
            Case Else
                icono.Image = My.Resources.p0
                Me.Tag = 0
        End Select
        valor.Text = predetermindado
    End Sub

    Protected Friend ReadOnly Property val_actual As String
        Get
            Return valor.Text
        End Get
    End Property
    Protected Friend Sub fijar_icono(ByVal indice As Integer)
        Me.Tag = indice
        Select Case indice
            Case 1
                icono.Image = My.Resources.p1
            Case 2
                icono.Image = My.Resources.p2
            Case 3
                icono.Image = My.Resources.p3
            Case 4
                icono.Image = My.Resources.p4
            Case 5
                icono.Image = My.Resources.p5
            Case 6
                icono.Image = My.Resources.p6
            Case 7
                icono.Image = My.Resources.p7
            Case 8
                icono.Image = My.Resources.p8
            Case 9
                icono.Image = My.Resources.p9
            Case 10
                icono.Image = My.Resources.p10
            Case 11
                icono.Image = My.Resources.p11
            Case 12
                icono.Image = My.Resources.p12
            Case Else
                icono.Image = My.Resources.p0
                Me.Tag = 0
        End Select
    End Sub

    Private Sub valor_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles valor.GotFocus, Me.GotFocus
        valor.SelectionStart = 0
        valor.SelectionLength = Len(valor.Text)
    End Sub
End Class
