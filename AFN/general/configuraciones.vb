Module configuraciones
    Declare Function GetUserName Lib "advapi32.dll" Alias "GetUserNameA" (ByVal lpBuffer As String, ByRef nSize As Integer) As Integer

    Public Function ObtenerUsuarioActual() As String
        Dim iReturn As Integer
        Dim userName As String
        userName = New String(CChar(" "), 50)
        iReturn = GetUserName(userName, 50)
        ObtenerUsuarioActual = userName.Substring(0, userName.IndexOf(Chr(0)))
    End Function

    Public Sub bloquearW(ByVal reporte As form)
        reporte.Cursor = System.Windows.Forms.Cursors.WaitCursor
        reporte.Enabled = False
    End Sub

    Public Sub desbloquearW(reporte As Form)
        reporte.Cursor = System.Windows.Forms.Cursors.Default
        reporte.Enabled = True
    End Sub

    Public Sub bloquearC(ByVal Elemento As Control)
        Elemento.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Elemento.Enabled = False
    End Sub

    Public Sub desbloquearC(Elemento As Control)
        Elemento.Cursor = System.Windows.Forms.Cursors.Default
        Elemento.Enabled = True
    End Sub

    Public Function lcol(ByVal posicion As Integer) As String
        Dim matriz, result As String
        Dim part1, part2, procesar As Integer
        result = ""
        matriz = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        procesar = posicion - 1
        While procesar >= 0
            part1 = Fix(procesar / Len(matriz)) - 1
            part2 = procesar Mod Len(matriz)
            result = Mid(matriz, part2 + 1, 1) + result
            procesar = part1
            Application.DoEvents()
        End While
        lcol = result
    End Function
    Public Sub UncheckOtherTSMItems(selectedMenuItem As ToolStripMenuItem)
        For Each ltoolStripMenuItem As ToolStripMenuItem In selectedMenuItem.Owner.Items
            ltoolStripMenuItem.Checked = False
        Next
        selectedMenuItem.Checked = True
    End Sub
    Public Function GetIDProcces(ByVal nameProcces As String) As Integer
        Try
            Dim asProccess As Process() = Process.GetProcessesByName(nameProcces)
            For Each pProccess As Process In asProccess
                If pProccess.MainWindowTitle = "" Then
                    Return pProccess.Id
                End If
            Next
            Return -1
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Public Sub ActivarF(ByRef elemento As Control, Optional ByVal stat As Boolean = True)
        elemento.Enabled = stat
        'If Not TypeOf elemento Is DTPicker Then
        'If elemento.Enabled Then
        '    elemento.BackColor = Color.White
        'Else
        '    elemento.BackColor = Color.Silver
        'End If
        'End If
    End Sub


    'funciones para Control Personalizado: ProgressShow
    Public Function cargar_barra(ByRef formulario As Form) As ProgressShow
        Dim MyNewObject As Control
        MyNewObject = New ProgressShow
        MyNewObject.Name = "avance"
        formulario.Controls.Add(MyNewObject)
        formulario.Controls("avance").Visible = False
        Dim avance As ProgressShow = formulario.Controls("avance")
        Return avance
    End Function
    Public Sub descargar_barra(ByRef formulario As Form)
        For Each elemento As Control In formulario.Controls
            If TypeOf elemento Is ProgressShow Then
                formulario.Controls.Remove(elemento)
                elemento.Dispose()
            End If
        Next
    End Sub

    '//For Action parameter in EncryptString  
    Enum EncrypAction
        ENCRYPT = 1
        DECRYPT = 2
    End Enum
    '---------------------------------------------------------------------  
    ' EncryptString  
    ' Modificado por Harvey T.  
    '---------------------------------------------------------------------  
    Public Function EncryptString( _
    UserKey As String, Text As String, Action As EncrypAction _
    ) As String
        'Dim UserKeyX As String
        Dim Temp As Integer
        'Dim Times As Integer
        Dim i As Integer
        Dim j As Integer
        Dim n As Integer
        Dim rtn As String

        '//Get UserKey characters  
        n = Len(UserKey)
        Dim UserKeyASCIIS() As Integer
        ReDim UserKeyASCIIS(0 To n)
        For i = 1 To n
            UserKeyASCIIS(i) = Asc(Mid$(UserKey, i, 1))
        Next

        '//Get Text characters  
        Dim TextASCIIS() As Integer
        ReDim TextASCIIS(Len(Text))
        For i = 1 To Len(Text)
            TextASCIIS(i) = Asc(Mid$(Text, i, 1))
        Next
        rtn = ""
        '//Encryption/Decryption  
        If Action = EncrypAction.ENCRYPT Then
            For i = 1 To Len(Text)
                j = IIf(j + 1 >= n, 1, j + 1)
                Temp = TextASCIIS(i) + UserKeyASCIIS(j)
                If Temp > 255 Then
                    Temp = Temp - 255
                End If
                rtn = rtn + Chr(Temp)
            Next
        ElseIf Action = EncrypAction.DECRYPT Then
            For i = 1 To Len(Text)
                j = IIf(j + 1 >= n, 1, j + 1)
                Temp = TextASCIIS(i) - UserKeyASCIIS(j)
                If Temp < 0 Then
                    Temp = Temp + 255
                End If
                rtn = rtn + Chr(Temp)
            Next
        End If

        '//Return  
        EncryptString = rtn
    End Function

    Public Sub Mensaje_IT(ByVal mensaje As String)
        MessageBox.Show(mensaje, "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub
    Public Sub Mensaje_Err(ByVal mensaje As String)
        MessageBox.Show(mensaje, "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub Mensaje_Inf(ByVal mensaje As String)
        MessageBox.Show(mensaje, "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Module
