
Public Class busqueda_articulo
    Private _origen As String
    Private _Cvolver As Object
    Private _Rfinal As resultado_articulo

    ''' <summary>
    ''' Instancia del forumario que contiene toda la logica del proceso
    ''' </summary>
    ''' <remarks></remarks>
    Private base As New base_AFN


    Public Sub New()
        ' Llamada necesaria para el diseñador.
        InitializeComponent()
    End Sub
    Public Sub New(ByVal origen As String)
        ' Llamada necesaria para el diseñador.
        InitializeComponent()
        _origen = origen
        DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

#Region "Funciones del formulario"
    Private Sub busqueda_articulo_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed
        If Not IsNothing(_Cvolver) Then
            _Cvolver.Show()
        End If
    End Sub
    Private Sub busqueda_articulo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim tabla_zona As DataTable

        With Fdesde
            .ShowCheckBox = True
            .Value = DateAdd(DateInterval.Month, -1, DateSerial(Year(Now), Month(Now), 1))
            .CustomFormat = "dd-MM-yyyy"
            .Checked = False
            .MaxDate = Now
        End With

        With Fhasta
            .ShowCheckBox = True
            .Value = DateAdd(DateInterval.Day, -1, DateSerial(Year(Now), Month(Now), 1))
            .Checked = False
            .CustomFormat = "dd-MM-yyyy"
            .MaxDate = Now
        End With

        With MosResult
            .DataSource = base.lista_articulo

            .Columns("Artículo").Width = 80
            .Columns("Cantidad").Width = 80
            .Columns("Zona").Width = 80
            .Columns("Descripción Artículo").Width = 290

            .Columns("rowindx").Visible = False
            .Columns("parte").Visible = False
            .Columns("estado").Visible = False
        End With

        With cboZona
            tabla_zona = base.ZONAS_GL_T()
            .DataSource = tabla_zona
            .ValueMember = tabla_zona.Columns(0).ColumnName
            .DisplayMember = tabla_zona.Columns(1).ColumnName
            .SelectedIndex = 0
        End With
        
    End Sub
    Public Sub actualizar_origen(ByVal CodOrigen As String, ByRef QalForm As Form)
        _origen = CodOrigen
        _Cvolver = QalForm
    End Sub
#End Region

#Region "Funciones de controles del formulario"
    Private Sub btn_buscar_Click(sender As System.Object, e As System.EventArgs) Handles btn_buscar.Click
        Dim bZona, bOrigen, bVigencia, Bdescrip As String
        Dim fecha_min, fecha_max As DateTime
        Dim Bcodigo As Integer

        If _origen = "Mod" Then
            bOrigen = "NO"
        Else
            bOrigen = "CLOSE"
        End If
        Select Case _origen
            Case "FB"
                bVigencia = "2,3"
            Case "VP"
                bVigencia = "2"
            Case "FC"
                bVigencia = "1,2,3"
            Case Else
                bVigencia = "1"
        End Select
        If cboZona.SelectedIndex = -1 Then
            bZona = "00"
        Else
            bZona = cboZona.SelectedValue
        End If
        If IsNumeric(Tcodigo.Text) Then
            Bcodigo = Val(Tcodigo.Text)
        Else
            Bcodigo = 0
        End If
        Bdescrip = Tdescrip.Text
        fecha_min = DateTime.MinValue
        fecha_max = DateTime.MaxValue
        If Fdesde.Checked Then
            fecha_min = Fdesde.Value
        End If
        If Fhasta.Checked Then
            fecha_max = Fhasta.Value
        End If

        Dim resultado As DataTable
        resultado = base.buscar_Articulo(fecha_min, fecha_max, Bcodigo, Bdescrip, bZona, bVigencia, bOrigen)

        Lresultado.Text = "Resultados : " + CStr(resultado.Rows.Count)
        MosResult.DataSource = resultado
        MosResult.ClearSelection()
        _Rfinal = Nothing
        'proceso especial para precio de venta
        If _origen = "VP" Then
            Dim codigo, parte, procesado As Integer
            For Each fila As DataGridViewRow In MosResult.Rows
                codigo = fila.Cells("Artículo").Value
                parte = fila.Cells("parte").Value
                procesado = base.contar_docVentas(codigo, parte)
                If procesado > 0 Then
                    fila.DefaultCellStyle.BackColor = Color.Gray
                End If
                Application.DoEvents()
            Next
        End If
    End Sub

    Private Sub MosResult_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MosResult.CellClick
        Dim valor_activacion As Boolean
        If e.RowIndex = -1 And e.ColumnIndex = -1 Then
            MosResult.ClearSelection()
            _Rfinal = Nothing
        End If
        If e.RowIndex <> -1 Then
            For Each fila As DataGridViewRow In MosResult.SelectedRows
                If fila.Cells("estado").Value = "OPEN" Then
                    valor_activacion = False
                Else
                    valor_activacion = True
                End If
                _Rfinal = New resultado_articulo(fila.Cells("Artículo").Value, fila.Cells("parte").Value, fila.Cells("rowindx").Value, valor_activacion)
            Next
        End If
    End Sub

    Private Sub btn_marcar_Click(sender As System.Object, e As System.EventArgs) Handles btn_marcar.Click, MosResult.CellDoubleClick
        If Not IsNothing(_Rfinal) Then
            DialogResult = Windows.Forms.DialogResult.OK
            If Not IsNothing(_Cvolver) Then
                Select Case _origen
                    Case "EA"
                        _Cvolver.AF_estado_actual(_Rfinal.articulo)
                    Case "FI"
                        form_welcome.AF_ficha_ingreso(_Rfinal.articulo)
                    Case "FB"
                        form_welcome.AF_ficha_baja(_Rfinal.articulo, _Rfinal.part)
                    Case "Mod"
                        form_ingreso.resultado_busqueda(_Rfinal.articulo, _Rfinal.activo)
                    Case "FC"
                        form_ficha_cambio.cargar_formulario(_Rfinal.articulo)
                    Case Else
                        '"V" "C" "M" "AN"
                        _Cvolver.cargar_formulario(_Rfinal.fila)
                End Select
                Me.Dispose()
            End If
        Else
            'MsgBox("no hay nada seleccionado")
        End If
    End Sub
#End Region

#Region "Variables que muestra el resultado"
    Public ReadOnly Property fila As Integer
        Get
            Return _Rfinal.fila
        End Get
    End Property
    Public ReadOnly Property articulo As Integer
        Get
            Return _Rfinal.articulo
        End Get
    End Property
    Public ReadOnly Property parte As Integer
        Get
            Return _Rfinal.part
        End Get
    End Property
    Public ReadOnly Property activo As Boolean
        Get
            Return _Rfinal.activo
        End Get
    End Property
#End Region

End Class

Public Class resultado_articulo
    Dim id_articulo As Integer
    Dim parte As Integer
    Dim rowid As Integer
    Dim activado As Boolean

    Sub New(ByVal codigo_articulo As Integer, ByVal particion As Integer, ByVal row As Integer, ByVal activar As Boolean)
        id_articulo = codigo_articulo
        parte = particion
        rowid = row
        activado = activar
    End Sub
    Public ReadOnly Property articulo As Integer
        Get
            Return id_articulo
        End Get
    End Property
    Public ReadOnly Property part As Integer
        Get
            Return parte
        End Get
    End Property
    Public ReadOnly Property fila As Integer
        Get
            Return rowid
        End Get
    End Property
    Public ReadOnly Property activo As Boolean
        Get
            Return activado
        End Get
    End Property
End Class