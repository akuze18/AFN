Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6

Public Class frmSolicitudInfo

    Private Sub btnImprimir_Click(sender As System.Object, e As System.EventArgs) Handles btnImprimir.Click
        Try
            BarCodeEAN13("NH FOOD", "1234", "ADSFASDF", 10)
            MsgBox("SI")
        Catch ex As Exception
            MsgBox("NO")
        End Try

    End Sub

    Private Sub BarCodeEAN13(ByVal Cliente As String, ByVal Codigo As String, ByVal Descripcion As String, ByVal NumCopias As Integer)
        Dim Columna As Integer = 0

        Dim Printer As New Printer
        Dim prt As Printer
        Dim encontro As Boolean = False
        For Each prt In Printers
            If prt.DeviceName = cbImpresora.Text Then
                Printer = prt
                encontro = True
                Exit For
            End If
        Next
        If Not encontro Then
            Exit Sub
        End If

        For i As Integer = 1 To NumCopias
            Columna = Columna + 1
            'Columna 1
            If Columna = 1 Then
                'Codigo de barra
                Printer.Print("N")
                Printer.Print("A50,0,0,1,1,1,N,'Example 1'")
                Printer.Print("^BEN,50,Y,N")
                Printer.Print("^FD" & Trim(Codigo) & "^FS")
                'Descripcion del producto
                Printer.Print("^FO20,35")
                Printer.Print("^FB230,4.5,,")
                Printer.Print("^ADN, 11, 7^FD" & Descripcion & "^FS")
                'Tienda
                Printer.Print("^FO25,10")
                Printer.Print("^A0N,30,30^FD" & Cliente & "^FS")
            End If
            'Columna 2
            If Columna = 2 Then
                'Codigo de barra
                'Printer.Print("^FO300,90^BY2")
                'Printer.Print("^BEN,50,Y,N")
                'Printer.Print("^FD" & Trim(Codigo) & "^FS")
                ''Descripcion del producto
                'Printer.Print("^FO285,35")
                'Printer.Print("^FB230,4.5,,")
                'Printer.Print("^ADN, 11, 7^FD" & Descripcion & "^FS")
                ''Tienda
                'Printer.Print("^FO290,10")
                'Printer.Print("^A0N,30,30^FD" & Cliente & "^FS")
            End If

            'Columna 3
            If Columna = 3 Then
                ''Codigo de barra
                'Printer.Print("^FO570,90^BY2")
                'Printer.Print("^BEN,50,Y,N")
                'Printer.Print("^FD" & Trim(Codigo) & "^FS")
                ''Descripcion del producto
                'Printer.Print("^FO555,35")
                'Printer.Print("^FB230,4.5,,")
                'Printer.Print("^ADN, 11, 7^FD" & Descripcion & "^FS")
                ''Tienda
                'Printer.Print("^FO560,10")
                'Printer.Print("^A0N,30,30^FD" & Cliente & "^FS")
            End If
            If Columna = 3 Then
                Printer.Print("P1")
                'Printer.EndDoc()
                Columna = 0
            End If
            If i = NumCopias Then
                Printer.Print("P1")
            End If
        Next
        Printer.EndDoc()
    End Sub

    Private Sub btnSalir_Click_1(sender As System.Object, e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub frmSolicitudInfo_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        For Each prt In Printers
            cbImpresora.Items.Add(prt.DeviceName)
        Next
    End Sub
End Class