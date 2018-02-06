Public Class bus_prov
    Dim origen As String
    Dim Cvolver As Object
    Dim cboAfect As ComboBox

    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private maestro As New master_control

    Public Sub actualizar_origen(ByVal CodOrigen As String, ByRef QalForm As Form, ByRef QalCombo As ComboBox)
        'origen = "V"   / form_venta
        'origen = "C"   / form_castigo
        'origen = "M"   / form_cambio
        'origen = "EA"  / form_welcome
        'origen = "FI"  / form_welcome
        'origen = "Mod" / form_ingreso
        'origen = "obc" / form_ing_obra
        origen = CodOrigen
        Cvolver = QalForm
        cboAfect = QalCombo
    End Sub

    Private Sub busqueda_articulo_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Cvolver.Show()
    End Sub

    Private Sub bus_prov_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim TB_zona As New DataTable
        Dim sql_periodo As String
        'agrego columnas para datagridviewer
        MosResult.DataSource = dato_resultado()
        MosResult.RowHeadersWidth = 10
        MosResult.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        MosResult.AllowUserToResizeColumns = False
        MosResult.AllowUserToResizeRows = False
        MosResult.Columns(0).Width = 100
        MosResult.Columns(1).Width = 310
        MosResult.Columns(2).Width = 50
        MosResult.Columns(3).Visible = False
        MosResult.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        'zonas
        sql_periodo = "SELECT COD_GL [cod],NOMBRE [es] FROM AFN_ZONA WHERE ACTIVA=1 ORDER BY COD_GL"
        TB_zona = maestro.ejecuta(sql_periodo)
        cboZona.DisplayMember = "es"
        cboZona.ValueMember = "cod"
        cboZona.DataSource = TB_zona
    End Sub

    Private Sub btn_buscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_buscar.Click
        'todos los valores son opcionales, asi que no es necesario validar blancos
        Dim resultado As DataTable
        Dim bZona, Bcodigo As String
        Dim Tresult As Integer
        bloquearW(Me)
        If cboZona.SelectedIndex = -1 Or cboZona.Text = "" Then
            bZona = ""
        Else
            bZona = cboZona.SelectedValue
        End If
        'If IsNumeric(Tcodigo.Text) Then
        Bcodigo = onlynumRUT(Tcodigo.Text)
        'Else
        'Bcodigo = "%"
        'End If

        resultado = buscar_Proveedor(Bcodigo, Tdescrip.Text, bZona)
        Tresult = resultado.Rows.Count
        Lresultado.Text = "Resultados : " + CStr(Tresult)

        Dim colchon As New DataTable

        MosResult.DataSource = dato_resultado()
        colchon = dato_resultado()
        MosResult.Refresh()
        'Application.DoEvents()

        Dim fbp As ProgressShow = AFN.cargar_barra(Me)
        fbp.inicializar(1, Tresult)
        For i = 0 To Tresult - 1
            Dim newfila As DataRow = colchon.NewRow
            Dim cod_rut As String = Trim(resultado.Rows(i).Item(0))
            If Len(cod_rut) < 2 Then
                newfila(0) = cod_rut
            Else
                newfila(0) = Strings.Left(cod_rut, Len(cod_rut) - 1) + "-" + Strings.Right(cod_rut, 1)
            End If
            newfila(1) = resultado.Rows(i).Item(1)
            newfila(2) = Strings.Left(resultado.Rows(i).Item(2), 2)
            newfila(3) = resultado.Rows(i).Item(3)
            colchon.Rows.Add(newfila)
            fbp.continua_proceso()
        Next

        MosResult.DataSource = colchon
        MosResult.ClearSelection()
        fbp.termina_proceso()
        descargar_barra(Me)
        desbloquearW(Me)
    End Sub
    Private Sub MosResult_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MosResult.CellClick
        If e.RowIndex = -1 Then
            MosResult.ClearSelection()
        End If
    End Sub
    Private Sub btn_marcar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_marcar.Click, MosResult.DoubleClick
        Dim TablaCombo As New DataTable
        Dim valor_seleccionado As String = ""
        Dim salir As Boolean = False
        If MosResult.SelectedRows.Count = 1 Then
            TablaCombo = cboAfect.DataSource
            For Each fila As DataGridViewRow In MosResult.SelectedRows
                valor_seleccionado = fila.Cells(3).Value
            Next
            For i = 0 To TablaCombo.Rows.Count - 1
                If TablaCombo.Rows(i).Item("cod").Equals(valor_seleccionado) Then
                    cboAfect.SelectedIndex = i
                    salir = True
                    Exit For
                End If
            Next
            If salir Then
                Me.Dispose() 'busqueda
            Else
                MessageBox.Show("Busqueda no corresponde, revisar", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Function buscar_Proveedor(Optional ByVal codigo As String = "", Optional ByVal nombre As String = "", Optional ByVal zona As String = "") As DataTable
        Dim colchon As DataTable
        Dim rut, fondo, sql_conteo As String
        rut = onlynumRUT(codigo)
        fondo = "FROM PM00200 A" + Chr(13) + "WHERE " + _
        "VNDCLSID LIKE '" + zona + "%' and " + _
        "VENDNAME like '%" + nombre + "%' and " + _
        "PARVENDID like '%" + rut + "%' ORDER BY 2"
        sql_conteo = "SELECT A.PARVENDID, A.VENDNAME, A.VNDCLSID, A.COD[VENDORID] " + fondo
        colchon = maestro.ejecuta(sql_conteo)
        Return colchon
    End Function
    Private Function onlynumRUT(ByVal texto As String) As String
        Dim posi As Integer
        Dim salida, revis, patron, Pat As String
        Dim pasa As Boolean
        salida = ""
        patron = "1234567890K"
        For posi = 1 To Len(texto)
            revis = Mid(texto, posi, 1)
            pasa = False
            For i = 1 To Len(patron)
                Pat = Mid(patron, i, 1)
                If revis = Pat Then
                    'caracter esta ok
                    pasa = True
                    i = Len(patron)
                End If
                Application.DoEvents()
            Next
            If pasa Then
                salida = salida + revis
            End If
        Next
        onlynumRUT = salida
    End Function
    Private Function dato_resultado() As DataTable
        Dim formato_resultado As New DataTable
        formato_resultado.Columns.Add("RUT", Type.GetType("System.String"))
        formato_resultado.Columns.Add("NOMBRE", Type.GetType("System.String"))
        formato_resultado.Columns.Add("ZONA", Type.GetType("System.String"))
        formato_resultado.Columns.Add("ID", Type.GetType("System.String"))
        Return formato_resultado
    End Function


End Class