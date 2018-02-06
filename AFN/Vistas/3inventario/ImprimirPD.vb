Imports System.Drawing.Printing
Imports System.Drawing.Text
Imports System.Threading.Thread

Public Class ImprimirPD

    Private prtSettings As PrinterSettings
    Private prtDoc As PrintDocument
    'Private prtFont As Font
    'Private barCodeFont As Font
    'Private CodeFont As Font
    'Private CompanyFont As Font
    '
    Private lineaActual As Integer

    Private _datos As DataTable
    Private _filaP As DataRow

    Private _fileFontBarcode, _fileFontLabel As String

    Public Sub New(ByVal fileFontBarcode As String, ByVal fileFontLabel As String)
        _fileFontBarcode = fileFontBarcode
        _fileFontLabel = fileFontLabel
        'cargar_fuentes()
        ' La configuración actual de la impresora predeterminada
        prtSettings = New PrinterSettings
    End Sub

#Region "Acciones"
    Public Sub imprimir(ByVal datos As DataTable, ByVal selectAntes As Boolean, ByVal esPreview As Boolean)

        _datos = datos

        If prtSettings Is Nothing Then
            prtSettings = New PrinterSettings
            'prtSettings.Collate = False
        End If
        '
        If selectAntes Then
            If seleccionarImpresora() = False Then Return
        End If
        '
        If prtDoc Is Nothing Then
            prtDoc = New PrintDocument
            AddHandler prtDoc.PrintPage, AddressOf prt_PrintPage
        End If
        '
        ' resetear la línea actual
        lineaActual = 0
        '
        'configuramos la hoja para imprimir
        Dim AFPaperSize As New PaperSize("ACTIVO_FIJO", 410, 118)
        Dim AFMargins As New Margins(100, 100, 100, 100)
        Dim AFPrinterResolution As New PrinterResolution()
        AFPrinterResolution.X = 203
        AFPrinterResolution.Y = 203
        With prtSettings
            With .DefaultPageSettings
                .PaperSize = AFPaperSize
                .Margins = AFMargins
                .PrinterResolution = AFPrinterResolution
                .Landscape = False
            End With
        End With
        '
        ' la configuración a usar en la impresión
        prtDoc.PrinterSettings = prtSettings

        If esPreview Then
            Dim prtPrev As New PrintPreviewDialog
            prtPrev.Document = prtDoc
            prtPrev.Text = "Previsualizar etiquetas"
            prtPrev.ShowDialog()
        Else
            prtDoc.Print()
        End If
    End Sub

    ''' <summary>
    ''' Seleccionar la impresora. (cuadro de diálogos para imprimir)
    ''' </summary>
    ''' <returns>
    ''' Devuelve True si todo fue bien o false si se canceló
    ''' </returns>
    Public Function seleccionarImpresora() As Boolean
        Dim prtDialog As New PrintDialog
        If prtSettings Is Nothing Then
            prtSettings = New PrinterSettings
        End If


        With prtDialog
            .AllowPrintToFile = False
            .AllowSelection = False
            .AllowSomePages = False
            .PrintToFile = False
            .ShowHelp = False
            .ShowNetwork = True

            .PrinterSettings = prtSettings

            If .ShowDialog() = DialogResult.OK Then
                prtSettings = .PrinterSettings
            Else
                Return False
            End If

        End With

        Return True
    End Function
#End Region

#Region "Eventos"
    Private Sub prt_PrintPage(ByVal sender As Object, _
                              ByVal e As PrintPageEventArgs)
        ' Este evento se produce cada vez que se va a imprimir una página
        Dim fontHeight As Single
        Dim yPos As Single = e.MarginBounds.Top
        Dim leftMargin As Single = e.MarginBounds.Left
        Dim secBorder As Single
        Dim fila As DataRow
        fila = _datos.Rows(lineaActual)
        If fila(0) Then
            Dim strMascara, strOld, strDescrip, strLoteA As String
            Dim strFecha As Date
            strMascara = fila(1).ToString       'producto
            strOld = fila(2).ToString           'codigo_old
            strDescrip = fila(3).ToString       'descripcion
            strFecha = fila(4)                  'fecha_compra
            strLoteA = fila(9)                  'lote articulo
            leftMargin = 10
            secBorder = 80
            yPos = 10
            Dim Compañia, Dpart1, Dpart2, Dpart3 As String
            Dim largo As Integer = 50
            Compañia = "NH FOODS CHILE"
            If strDescrip.Length <= largo Then
                Dpart1 = strDescrip
                Dpart2 = ""
                Dpart3 = ""
            Else
                If strDescrip.Length <= largo * 2 Then
                    Dpart1 = strDescrip.Substring(0, largo)
                    Dpart2 = strDescrip.Substring(largo)
                    Dpart3 = ""
                Else
                    Dpart1 = strDescrip.Substring(0, largo)
                    Dpart2 = strDescrip.Substring(largo, largo)
                    Dpart3 = strDescrip.Substring(largo * 2)
                End If
            End If

            ' Asignar el tipo de letra
            fontHeight = 28 'CodeFont.GetHeight(e.Graphics) '27.76693

            ' imprimir cada una de las líneas de esta página
            Dim fontLabel = getFontLabel()
            Dim fontCBar = getFontCBar()
            Using CompanyFont As New Font(fontLabel, 14, FontStyle.Bold)
                Using CodeFont As New Font(fontLabel, 16, FontStyle.Bold)
                    Using prtFont As New Font(fontLabel, 9, FontStyle.Bold)
                        Try
                            e.Graphics.DrawString(Compañia, CompanyFont, Brushes.Black, leftMargin, yPos)
                            e.Graphics.DrawString(strLoteA, CodeFont, Brushes.Black, 170, yPos)
                            e.Graphics.DrawString(strMascara, CodeFont, Brushes.Black, 260, yPos)
                            yPos += fontHeight

                            fontHeight = 16 'prtFont.GetHeight(e.Graphics)  '15.61889

                            e.Graphics.DrawString("DESCRIPCION : ", prtFont, Brushes.Black, leftMargin, yPos)
                            e.Graphics.DrawString(Dpart1, prtFont, Brushes.Black, secBorder, yPos)
                            yPos += fontHeight
                            e.Graphics.DrawString("             ", prtFont, Brushes.Black, leftMargin, yPos)
                            e.Graphics.DrawString(Dpart2, prtFont, Brushes.Black, secBorder, yPos)
                            yPos += fontHeight
                            e.Graphics.DrawString("             ", prtFont, Brushes.Black, leftMargin, yPos)
                            e.Graphics.DrawString(Dpart3, prtFont, Brushes.Black, secBorder, yPos)
                            yPos += fontHeight

                            e.Graphics.DrawString("Fecha Compra : ", prtFont, Brushes.Black, leftMargin, yPos)
                            e.Graphics.DrawString(strFecha.ToString("dd/MM/yyyy"), prtFont, Brushes.Black, secBorder, yPos)
                            yPos += fontHeight
                            e.Graphics.DrawString("Lote Artic. : ", prtFont, Brushes.Black, leftMargin, yPos)
                            e.Graphics.DrawString(strLoteA, prtFont, Brushes.Black, secBorder, yPos)
                            yPos += fontHeight

                            Using barCodeFont As New Font(fontCBar, 30, FontStyle.Regular)
                                e.Graphics.DrawString(FormatBarCode(strMascara), barCodeFont, Brushes.Black, 130, 85)
                            End Using
                        Catch ex As Exception
                            MessageBox.Show("Se produjo un error al imprimir" + ex.Message + Chr(13) + _
                                            "Alcanzó a imprimir " + lineaActual.ToString + " de " + _datos.Rows.Count.ToString + " etiquetas")
                        End Try

                    End Using
                End Using
            End Using
        End If
        lineaActual += 1
        If lineaActual < _datos.Rows.Count Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If
    End Sub
#End Region

#Region "Auxiliares"
    Private Function FormatBarCode(ByVal code As String) As String
        Dim barcode As String = String.Empty
        barcode = String.Format("*{0}*", code)
        Return barcode
    End Function

    Private Function getFontLabel() As FontFamily
        Dim pfc As New PrivateFontCollection()
        pfc.AddFontFile(_fileFontLabel)
        Dim fontLabel As FontFamily = pfc.Families(0)
        Return fontLabel
    End Function

    Private Function getFontCBar() As FontFamily
        Dim pfc As PrivateFontCollection = New PrivateFontCollection()
        pfc.AddFontFile(_fileFontBarcode)
        Dim fontCBar As FontFamily = pfc.Families(0)
        Return fontCBar
    End Function

#End Region


End Class
