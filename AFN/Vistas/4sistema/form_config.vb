Public Class form_config
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private maestro As New master_control
    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        For Each seccion As String In maestro.sections
            Dim nodo_seccion As New TreeNode(seccion)
            For Each llave As String In maestro.keys(seccion)
                Dim nodo_llave As New TreeNode(llave)
                nodo_seccion.Nodes.Add(nodo_llave)
            Next
            TVconfig.Nodes.Add(nodo_seccion)
        Next
        TVconfig.ExpandAll()
    End Sub

    Private Sub TVConfig_NodeMouseClick(sender As Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TVconfig.NodeMouseClick
        Dim actual, previo As TreeNode
        actual = e.Node
        previo = actual.Parent
        If IsNothing(previo) Then
            With TBseccion
                .Text = actual.Text
                .Enabled = False
            End With
            With TBkey
                .Text = String.Empty
                .Enabled = True
            End With
            TBvalor.Text = String.Empty
        Else
            With TBseccion
                .Text = previo.Text
                .Enabled = False
            End With
            With TBkey
                .Text = actual.Text
                .Enabled = False
            End With
            TBvalor.Text = maestro.vars(TBseccion.Text, TBkey.Text)
        End If

    End Sub

    Private Sub btn_guardar_Click(sender As System.Object, e As System.EventArgs) Handles btn_guardar.Click
        If String.IsNullOrEmpty(TBseccion.Text) Then
            MessageBox.Show("No puede guardar, debe completar la seccion")
            Exit Sub
        End If
        If String.IsNullOrEmpty(TBkey.Text) Then
            MessageBox.Show("No puede guardar, debe completar la variable")
            Exit Sub
        End If
        If String.IsNullOrEmpty(TBvalor.Text) Then
            MessageBox.Show("No puede guardar, debe indicar un valor para la variable")
            Exit Sub
        End If

        maestro.vars(TBseccion.Text, TBkey.Text) = TBvalor.Text

        MessageBox.Show("Ha guardado la variable correctamente")
    End Sub
End Class