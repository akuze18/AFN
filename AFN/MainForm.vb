Public Class MainForm
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Protected base As New base_AFN
    Private _origen As Form

    Public Sub New()
        ' Llamada necesaria para el diseñador.
        InitializeComponent()
        _origen = Nothing
    End Sub

    Public Sub ShowFrom(ByVal origen As Form)
        _origen = origen
        Me.Show()
        origen.Hide()
    End Sub

    Private Sub MainForm_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If Not IsNothing(_origen) Then
            _origen.Show()
        End If
    End Sub


End Class