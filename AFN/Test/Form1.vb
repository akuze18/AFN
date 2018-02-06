

Public Class Form1


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim Dpart1, Dpart2, Dpart3, descrip As String
        Dim largo As Integer
        largo = NumericUpDown1.Value
        descrip = txtTexto.Text
        If descrip.Length <= largo Then
            Dpart1 = descrip
            Dpart2 = ""
            Dpart3 = ""
        Else
            If descrip.Length <= largo * 2 Then
                Dpart1 = descrip.Substring(0, largo)
                Dpart2 = descrip.Substring(largo)
                Dpart3 = ""
            Else
                Dpart1 = descrip.Substring(0, largo)
                Dpart2 = descrip.Substring(largo, largo)
                Dpart3 = descrip.Substring(largo * 2)
            End If
        End If
        Label1.Text = Dpart1
        Label2.Text = Dpart2
        Label3.Text = Dpart3
    End Sub

    Private Sub txtTexto_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtTexto.TextChanged
        Label4.Text = "Caracteres usados : " + txtTexto.Text.Length.ToString
    End Sub

    Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        txtTexto.Text = "0"
        txtTexto.Text = ""
    End Sub
End Class