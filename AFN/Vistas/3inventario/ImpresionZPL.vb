Imports System
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports Microsoft.Win32.SafeHandles

'Fuente para crear esta clase : http://elfosodelsarlacc.blogspot.cl/2012/10/imprimir-etiquetas-con-codigo-zpl-desde.html

Public Class ImpresionZPL

    Public Const GENERIC_WRITE = &H40000000
    Public Const OPEN_EXISTING = 3
    Public Const CREATE_ALWAYS = 2
    Public Const FILE_SHARE_WRITE = &H2

    Dim LPTPORT As String
    Dim PuertoImpresion As String = "LPT2"

    Friend WithEvents btnImpresion As System.Windows.Forms.Button
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Dim hPort As Integer

    '...
    <DllImport("kernel32.dll", SetLastError:=True)>
    Public Shared Function CreateFile(ByVal lpFileName As String, ByVal dwDesiredAccess As Integer, ByVal dwShareMode As Integer, ByRef lpSecurityAttributes As SECURITY_ATTRIBUTES, ByVal dwCreationDisposition As Integer, ByVal dwFlagsAndAttributes As Integer, ByVal hTemplateFile As Integer) As IntPtr
    End Function

    Public Declare Function CloseHandle Lib "kernel32" Alias "CloseHandle" _
                            (ByVal hObject As Integer) As Integer
    Dim retval As Integer

    Public Structure SECURITY_ATTRIBUTES
        Private nLength As Integer
        Private lpSecurityDescriptor As Integer
        Private bInheritHandle As Integer
    End Structure

    Public Sub Impresion(ByVal Cod_NEW As String, ByVal descrip As String, _
                         ByVal fec_compra As String, ByVal Cod_OLD As String)

        Dim SA As SECURITY_ATTRIBUTES
        'outFile es el archivo stream que contien la etiqueta y su formato
        Dim outFile As FileStream, hPortP As IntPtr, sfHand As SafeFileHandle
        'Imprime por LPT1 en local.
        'Para la impresión por red vía net use.
        'Se ha de crear un bat con: --> net use LPT2 /del 

        ' --> net use LPT2 \\NombrePC\NombreImpresoraCompartida y colocarlo en
        '....\All Users\Menú Inicio\Programas\Inicio 
        'para que cada usr. que inicie tenga 
        LPTPORT = PuertoImpresion
        hPort = CreateFile(LPTPORT, GENERIC_WRITE, FILE_SHARE_WRITE, SA, OPEN_EXISTING, 0, 0)
        'Convertir Integer a IntPtr 
        hPortP = New IntPtr(hPort)
        'Uso SafeHandle para cambiar la constructor obsoleto / FileStream Constructor (IntPtr, FileAccess, Boolean)
        sfHand = New SafeFileHandle(hPortP, False)
        'Crear FileStream usando SafeHandle 
        outFile = New FileStream(sfHand, FileAccess.Write)

        Dim fileWriter As New StreamWriter(outFile)

        'Sustitución de caracteres. El alfabeto que maneja esta impresora en concreto
        'es solo inglés por eso se sustituyen las tildes, eñes, etc.
        'Esta operación se debe realizar con todos los campos susceptibles a contener 

        'carácteres no aceptados 
        descrip = (descrip.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("ñ", "n").Replace("Ñ", "N").Replace("ç", "c").Replace("Ç", "C").Replace("ª", "."))

        Dim Compañia, Dpart1, Dpart2, Dpart3 As String
        Dim largo As Integer = 50
        Compañia = "NH FOODS CHILE"
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

        'Cod_OLD = "200103000350"
        'Cod_NEW = "20110901000004"
        'descrip = "HP COMPAQ 8100 CORE I3-500 GB HD-2GB RAM"
        'fec_compra = "21/09/2011"
        '--------------------------INICIO ETIQUETA---------------------------' 
        fileWriter.Write("^XA")
        'Deshabilita el panel de la impresora, para que el usr no pueda acceder
        fileWriter.Write("^MPS")
        'Posicion del eje de cordenadas, en dots
        fileWriter.Write("^LH0,0")

        'FO para el inicio de escritura, eje X,Y
        'AQ y AS son el formato fuentes de letra, Q es más pequeña de S
        fileWriter.Write("^FO60,20^AR,,17^FD" & Compañia & "^FS")
        fileWriter.Write("^FO70,60^AQ,,10^FD" & "COD ANTIGUO : " & Cod_OLD & "^FS")
        fileWriter.Write("^FO70,100^AQ,,10^FD" & "DESCRIPCION : " & Dpart1 & "^FS")
        fileWriter.Write("^FO70,125^AQ,,10^FD" & "              " & Dpart2 & "^FS")
        fileWriter.Write("^FO70,150^AQ,,10^FD" & "              " & Dpart3 & "^FS")
        fileWriter.Write("^FO70,200^AQ,,12^FD" & "Fecha Compra : " & fec_compra & "^FS")
        fileWriter.Write("^FO480,10^AU,40,20^FD" & Cod_NEW & "^FS")
        'B1 es para codigos de barra (solo numerico)
        fileWriter.Write("^FO400,200^B3,,50^FD" & Cod_NEW & " ^FS")


        ''FB establece un espacio de 900 dots y la C para centrar el texto
        'fileWriter.Write("^FO1,30^AU^FB900,1,0,C^FD" & Evento & "^FS")

        ''GB escribe una linea horizontal 900 largo, 0 grosor y 2 ancho linea
        'fileWriter.Write("^FO50,165^FR^GB900,0,2^FS")

        'Finaliza la etiqueta
        fileWriter.Write("^XZ")
        fileWriter.Flush()
        fileWriter.Close()
        outFile.Close()
        retval = CloseHandle(hPort)
    End Sub

    Public Sub EtiquetaActivoFijo(ByVal Cod_NEW As String, ByVal descrip As String, _
                         ByVal fec_compra As String, ByVal lote_art As String, ByVal Cod_OLD As String)

        Dim SA As SECURITY_ATTRIBUTES
        Dim outFile As FileStream, hPortP As IntPtr, sfHand As SafeFileHandle

        ' --> net use LPT2 \\NombrePC\NombreImpresoraCompartida y colocarlo en
        '....\All Users\Menú Inicio\Programas\Inicio 
        'para que cada usr. que inicie tenga 
        LPTPORT = PuertoImpresion
        'LPTPORT = "\\etiquetas\Datamax_M_4208"
        hPortP = CreateFile(LPTPORT, GENERIC_WRITE, 0, SA, 0, 0, 0)

        'hPortP = New IntPtr(hPort)
        'Crear FileStream usando SafeHandle 
        If (hPortP <> New IntPtr(-1)) Then
            'Uso SafeHandle para cambiar la constructor obsoleto / FileStream Constructor (IntPtr, FileAccess, Boolean)
            sfHand = New SafeFileHandle(hPortP, True)

            outFile = New FileStream(sfHand, FileAccess.ReadWrite)
            'outFile = New FileStream(hPort, FileAccess.ReadWrite)
        Else
            MessageBox.Show(Marshal.GetLastWin32Error())
            Exit Sub
        End If


        Dim fileWriter As New StreamWriter(outFile)

        'carácteres no aceptados 
        descrip = (descrip.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("ñ", "n").Replace("Ñ", "N").Replace("ç", "c").Replace("Ç", "C").Replace("ª", "."))
        Dim Compañia, Dpart1, Dpart2, Dpart3 As String
        Dim largo As Integer = 50
        Compañia = "NH FOODS CHILE"
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

        '--------------------------INICIO ETIQUETA---------------------------' 
        fileWriter.Write("^XA")
        'Deshabilita el panel de la impresora, para que el usr no pueda acceder
        fileWriter.Write("^MPS")
        'Posicion del eje de cordenadas, en dots
        fileWriter.Write("^LH0,0")

        'FO para el inicio de escritura, eje X,Y
        'AQ y AS son el formato fuentes de letra, Q es más pequeña de S
        fileWriter.Write("^FO60,20^AR,,17^FD" & Compañia & "^FS")
        fileWriter.Write("^FO70,60^AQ,,10^FD" & "COD ANTIGUO : " & Cod_OLD & "^FS")
        fileWriter.Write("^FO70,100^AQ,,10^FD" & "DESCRIPCION : " & Dpart1 & "^FS")
        fileWriter.Write("^FO70,125^AQ,,10^FD" & "              " & Dpart2 & "^FS")
        fileWriter.Write("^FO70,150^AQ,,10^FD" & "              " & Dpart3 & "^FS")
        fileWriter.Write("^FO70,180^AQ,,12^FD" & "Fecha Compra : " & fec_compra & "^FS")
        fileWriter.Write("^FO70,205^AQ,,12^FD" & "Lote Artic.  : " & lote_art & "^FS")
        fileWriter.Write("^FO480,10^AU,40,20^FD" & Cod_NEW & "^FS")
        'B1 es para codigos de barra (solo numerico)
        fileWriter.Write("^FO400,200^B1,,50^FD" & Cod_NEW & " ^FS")
        'Finaliza la etiqueta
        fileWriter.Write("^XZ")
        fileWriter.Flush()
        fileWriter.Close()
        outFile.Close()
        retval = CloseHandle(hPort)
    End Sub
End Class
