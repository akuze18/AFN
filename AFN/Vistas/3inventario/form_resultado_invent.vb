
Public Class form_resultado_invent

#Region "Variables de clase"
    ''' <summary>
    ''' Instancia del forumario que contiene toda la logica del proceso
    ''' </summary>
    ''' <remarks></remarks>
    Private base As New base_AFN
#End Region

#Region "del Formulario"
    Private Sub form_resultado_invent_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub

    Private Sub form_resultado_invent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Text = "Inventarios Disponibles"
        Label2.Text = "Zonas Disponibles"
        Label3.Text = "Clase Disponibles"
        With btn_mostrar
            .Text = "Mostrar Todos"
            .Visible = False
        End With
        With btn_dif
            .Text = "Cambios de Subzona"
            .Visible = False
        End With
        With btn_castigo
            .Text = "Bajas"
            .Visible = False
        End With
        With btn_cambios
            .Text = "Confirmar Cambios Subzona"
            .Visible = False
        End With
        
        'Cargo combo de fechas de inventario
        With cboFech_Inv
            .DataSource = base.lista_fecha_inv
            .ValueMember = .DataSource.Columns(0).ColumnName
            .DisplayMember = .DataSource.Columns(1).ColumnName
            .SelectedIndex = -1
        End With

        'Formato de Grilla
        With dgv_mostrar
            .Columns.AddRange(base.cabecera_resultado_inv)
            .RowHeadersWidth = 20
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            .AllowUserToOrderColumns = True
            .ColumnHeadersHeight = 240 * 2
            '.EditMode = DataGridViewEditMode.EditProgrammatically
            .ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        End With

        Me.MinimumSize = New Size(POpciones.Width, Me.Height)
        Me.WindowState = FormWindowState.Maximized
    End Sub
#End Region

#Region "Resize"
    Private Sub form_resultado_invent_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Dim largo_form As Integer
        largo_form = Me.Width - 16
        POpciones.Location = New Point(0, 0)
        POpciones.Width = largo_form
        dgv_mostrar.Location = New Point(20, POpciones.Height)
        dgv_mostrar.Width = largo_form - 40
        dgv_mostrar.Height = Me.Height - POpciones.Height - 40 - 16
    End Sub
    Private Sub POpciones_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles POpciones.Resize
        Dim largo_total, largo_label, largo_combo, largo_lista, largo_boton, contar_boton, C, espacio As Integer
        largo_total = POpciones.Width
        espacio = 10
        'determino largo maximo de cada tipo de control
        largo_label = 0
        largo_combo = 0
        largo_boton = 0
        contar_boton = 0
        For Each elemento As Control In POpciones.Controls
            If TypeOf elemento Is Label Then
                If largo_label < elemento.Width Then
                    largo_label = elemento.Width
                End If
            End If
            If TypeOf elemento Is ComboBox Then
                If largo_combo < elemento.Width Then
                    largo_combo = elemento.Width
                End If
            End If
            If TypeOf elemento Is Button Then
                contar_boton = contar_boton + 1
                If largo_boton < elemento.Width Then
                    largo_boton = elemento.Width
                End If
            End If
        Next
        'Alineo primera fila de controles (Label y Combo)
        largo_lista = largo_label + espacio + largo_combo
        C = (largo_total - largo_lista) / 2
        For Each elemento As Control In POpciones.Controls
            If TypeOf elemento Is Label Then
                elemento.Location = New Point(C, elemento.Location.Y)
            End If
            If TypeOf elemento Is ComboBox Then
                elemento.Location = New Point(C + largo_label + espacio, elemento.Location.Y)
            End If
        Next
        'Alineo segundo fila de controles (Botones)
        largo_lista = (largo_boton + espacio) * contar_boton - espacio
        C = (largo_total - largo_lista) / 2
        'contar_boton = 0
        For Each elemento As Control In POpciones.Controls
            If TypeOf elemento Is Button Then
                Dim posicion As Integer = elemento.Tag
                elemento.Location = New Point(C + posicion * (largo_boton + espacio) - espacio, elemento.Location.Y)
                'contar_boton = contar_boton + 1
            End If
        Next
    End Sub
#End Region

#Region "De los Controles"
    Private Sub cboFech_Inv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFech_Inv.SelectedIndexChanged
        If cboFech_Inv.SelectedIndex > -1 And TypeOf cboFech_Inv.SelectedValue Is DateTime Then
            Dim fechaI As Date
            fechaI = cboFech_Inv.SelectedValue
            'CARGAR COMBO DE ZONAS
            Dim colchon As DataTable
            colchon = base.lista_zona_inv(fechaI)
            With cboZona
                .ValueMember = colchon.Columns(0).ColumnName
                .DisplayMember = colchon.Columns(1).ColumnName
                .DataSource = colchon
                .SelectedIndex = -1
            End With
        Else
            cboZona.DataSource = Nothing
        End If
        cboClase.DataSource = Nothing
    End Sub
    Private Sub cboZona_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboZona.SelectedIndexChanged
        If cboZona.SelectedIndex <> -1 Then
            Dim fechaI As Date = cboFech_Inv.SelectedValue
            Dim zona As String = cboZona.SelectedValue
            'CARGAR COMBO DE CLASES
            Dim colchon As DataTable
            colchon = base.lista_clase_inv(fechaI, zona)
            With cboClase
                .ValueMember = colchon.Columns(0).ColumnName
                .DisplayMember = colchon.Columns(1).ColumnName
                .DataSource = colchon
                .SelectedIndex = -1
            End With
        End If
    End Sub
    Private Sub cboClase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClase.SelectedIndexChanged
        Dim mostrar As Boolean
        mostrar = cboClase.SelectedIndex > -1
        For Each elemento As Control In POpciones.Controls
            If TypeOf elemento Is Button Then
                elemento.Visible = mostrar
            End If
        Next
        'btn_mostrar.Visible = mostrar
        'btn_dif.Visible = mostrar
        'btn_castigo.Visible = mostrar
        'btn_cambios.Visible = mostrar
    End Sub

    Private Sub Buttons_Click(sender As System.Object, e As System.EventArgs) _
        Handles btn_dif.Click, btn_mostrar.Click, btn_castigo.Click, btn_cambios.Click
        If TypeOf sender Is Button Then
            Dim boton As Button = CType(sender, Button)

            Dim fechaI As Date = cboFech_Inv.SelectedValue
            Dim zona As String = cboZona.SelectedValue
            Dim clase As String = cboClase.SelectedValue

            'Cargar grilla según el boton que solicita
            Dim colchon As DataTable
            Select Case boton.Name
                Case "btn_dif", "btn_cambios"
                    colchon = base.lista_inv_tomado(fechaI, zona, clase, True)
                Case "btn_castigo"
                    colchon = base.lista_inv_tomado(fechaI, zona, clase, False, False)
                Case Else       'btn_mostrar
                    colchon = base.lista_inv_tomado(fechaI, zona, clase)
            End Select

            Select Case boton.Name
                Case "btn_cambios"
                    'Modificar
                    Dim text_fecha_camb As String = InputBox("Ingrese la fecha para efectuar los cambios de subzona", "NH Foods", Now.ToString("dd-MM-yyyy"))
                    Dim fecha_camb As DateTime
                    Try
                        fecha_camb = CType(text_fecha_camb, DateTime)
                    Catch ex As Exception
                        MessageBox.Show("No ha ingresado una fecha valida")
                        Exit Sub
                    End Try

                    Dim TotalDetalleLote As DataTable
                    TotalDetalleLote = base.lista_det_total_art
                    Dim avance As ProgressShow = cargar_barra(Me)
                    avance.inicializar(1, colchon.Rows.Count)
                    For i = 0 To colchon.Rows.Count - 1
                        'Reviso si el cambio ya ha sido aplicado
                        If colchon(i).Item("aplicado") = 0 Then
                            'obtengo los valores de la fila
                            Dim lote_art As Integer = colchon(i).Item("lote_articulo")
                            Dim parte As Integer = colchon(i).Item("parte")
                            Dim subzona As Integer = colchon(i).Item("subzona_tomada")
                            Dim filtro As String = "lote_articulo=" + lote_art.ToString + " AND parte=" + parte.ToString + " AND subzona_tomada=" + subzona.ToString
                            Dim cont_lote As DataRow() = colchon.Select(filtro)
                            TotalDetalleLote.Rows.Clear()
                            For Each fila As DataRow In cont_lote
                                Dim newFila As DataRow = TotalDetalleLote.NewRow
                                newFila("producto") = fila("producto")
                                newFila("lote") = fila("lote_articulo")
                                newFila("parte") = fila("parte")
                                newFila("procesar") = 1
                                newFila("row_id") = 1100
                                TotalDetalleLote.Rows.Add(newFila)
                            Next
                            Dim cantidad As Integer = TotalDetalleLote.Rows.Count
                            Dim recordset As DataTable
                            recordset = base.CAMBIO_ZONA(lote_art, parte, fecha_camb, zona, subzona, cantidad, form_welcome.GetUsuario, TotalDetalleLote, 1100)
                            If recordset.Rows(0).Item("cod_sit") < 0 Then
                                MessageBox.Show(recordset.Rows(0).Item("dsc_sit").ToString + vbCrLf + "Codigo: " + lote_art.ToString, "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            Else
                                'ya he cambiado el codigo, por lo que tengo que actualizar la toma de inventario para indicar que ya fue procesado
                                For Each fila As DataRow In colchon.Rows
                                    If fila("lote_articulo") = lote_art And fila("parte") = parte And fila("subzona_tomada") = subzona Then
                                        fila("aplicado") = 1
                                        base.CAMBIO_ZONA_INV(fila("producto"), fechaI)
                                    End If
                                Next
                            End If
                        End If
                        avance.continua_proceso()
                    Next
                    descargar_barra(Me)
                    MessageBox.Show("Proceso de cambio de subzona ha finalizado")
                Case Else
                    'Visualizar
                    base.detalle_resultado_inv(colchon, dgv_mostrar)
                    MessageBox.Show("Se han registrado " + colchon.Rows.Count.ToString + " filas", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Select
        End If

    End Sub

#End Region

#Region "Edicion de combobox de la grilla"
    'Fuente : http://ltuttini.blogspot.cl/2010/03/datagridview-parte-6-combobox-y-evento.html?showComment=1453990834691#c3576649024603939355
    Private Sub dgv_mostrar_EditingControlShowing(sender As Object, e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgv_mostrar.EditingControlShowing
        ' Ignoramos otros tipos de controles existentes en las celdas
        If Not TypeOf e.Control Is  _
        DataGridViewComboBoxEditingControl Then Return

        ' Referenciamos el control TextBox subyacente en la celda actual.
        Dim _cellComboBox As DataGridViewComboBoxEditingControl = TryCast(e.Control, DataGridViewComboBoxEditingControl)
        'MessageBox.Show(dgv_mostrar.CurrentCellAddress.X)
        Dim fila As DataGridViewRow = dgv_mostrar.CurrentRow
        Dim valor_anterior As Integer
        If dgv_mostrar.CurrentCellAddress.X = 10 Then
            With _cellComboBox
                valor_anterior = .SelectedValue
                .DataSource = base.SUBZONAS_ACT(fila.Cells(0).Value.ToString)
                .ValueMember = .DataSource.Columns(0).ColumnName
                .DisplayMember = .DataSource.Columns(1).ColumnName
                .SelectedValue = valor_anterior
                .Tag = dgv_mostrar.CurrentCellAddress.X
            End With
        Else
            With _cellComboBox
                .Tag = dgv_mostrar.CurrentCellAddress.X
            End With
        End If


        ' Eliminamos el controlador de eventos
        RemoveHandler _cellComboBox.SelectedValueChanged, AddressOf SelectedValueChanged

        ' Añadimos un controlador de evento.
        AddHandler _cellComboBox.SelectedValueChanged, AddressOf SelectedValueChanged

    End Sub
    Private Sub SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ' Referenciamos el control ComboBox
        Dim combo As ComboBox = DirectCast(sender, ComboBox)

        If combo.SelectedIndex > -1 And TypeOf combo.SelectedValue Is Integer Then
            ' se accede a la fila actual, para trabajar con otro de sus campos
            Dim fila As DataGridViewRow = dgv_mostrar.CurrentRow
            Dim fecha_inv As DateTime = cboFech_Inv.SelectedValue
            Dim atributo As String
            Select Case combo.Tag
                Case 8
                    atributo = "estado"
                Case 10
                    atributo = "subzona"
                Case 12
                    atributo = "ubicacion"
                Case Else
                    atributo = ""
            End Select
            Dim resultado As DataTable = base.ACTUALIZAR_TOMA_INVENTARIO(fecha_inv, fila.Cells(2).Value.ToString, atributo, combo.SelectedValue.ToString)
            If resultado.Rows(0).Item("status") <> 1 Then
                MessageBox.Show(resultado.Rows(0).Item(1).ToString)
            Else
                'MessageBox.Show("Se ha modificado correctamente")
            End If
            'MessageBox.Show("Valor Combo: " + combo.SelectedValue.ToString + Chr(13) + "Codigo Producto: " + fila.Cells(2).Value.ToString)
        End If
    End Sub
#End Region
End Class