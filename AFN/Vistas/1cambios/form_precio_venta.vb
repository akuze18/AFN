Public Class form_precio_venta
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private maestro As New master_control

    Private Sub form_precio_venta_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub

    Private Sub form_precio_venta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim datos_venta As New DataTable
        Tdocumento.Text = ""
        TprecioExt.Text = ""
        Lestado_doc.Text = ""
        TprecioExt.Enabled = False
        DTfecha.Value = Now
        datos_venta.Columns.Add("rowIndex", Type.GetType("System.Int32"))
        datos_venta.Columns.Add("Cod Artículo", Type.GetType("System.Int32"))
        datos_venta.Columns.Add("Parte", Type.GetType("System.Int32"))
        datos_venta.Columns.Add("Descripción Artículo")
        datos_venta.Columns.Add("Cantidad", Type.GetType("System.Int32"))
        datos_venta.Columns.Add("Costo_Unitario", Type.GetType("System.Double"))
        datos_venta.Columns.Add("Costo_Extend", Type.GetType("System.Double"))
        datos_venta.Columns.Add("Precio Unitario", Type.GetType("System.Double"))
        datos_venta.Columns.Add("Precio Total", Type.GetType("System.Double"))
        datos_venta.Columns.Add("Zona")
        datos_venta.Columns.Add("Subzona")
        datos_venta.Columns.Add("Clase")
        datos_venta.Columns.Add("UoE")
        datos_venta.Rows.Add(datos_venta.NewRow)
        With detalle_venta
            .DataSource = datos_venta
            .Columns(0).Visible = False
            .Columns(1).Width = 120 * 0.75
            .Columns(2).Visible = False
            .Columns(3).Width = 450 * 0.75
            .Columns(4).Width = 100 * 0.75
            .Columns(5).Visible = False
            .Columns(6).Visible = False
            .Columns(7).Width = 150 * 0.75
            .Columns(7).DefaultCellStyle.Format = "N0"
            .Columns(8).Width = 180 * 0.75
            .Columns(8).DefaultCellStyle.Format = "N0"
            .Columns(9).Visible = False
            .Columns(10).Visible = False
            .Columns(11).Visible = False
            .Columns(12).Visible = False
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
        End With


    End Sub

    Private Sub Tdocumento_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tdocumento.TextChanged
        If Not String.IsNullOrWhiteSpace(Tdocumento.Text) Then
            'valor del documento fue modificado
            'reviso si documento existe en la base de datos
            Dim esta As Integer
            esta = maestro.Dcount("DOCVENTA", "AFN_RVENTA", "DOCVENTA='" + Tdocumento.Text + "'")
            If esta > 0 Then
                With Lestado_doc
                    .Text = "Documento ya ha sido utilizado"
                    .ForeColor = System.Drawing.ColorTranslator.FromWin32(&HFF&)
                End With
            Else
                With Lestado_doc
                    .Text = "OK"
                    .ForeColor = System.Drawing.ColorTranslator.FromWin32(&HC000&)
                End With
            End If

        Else
            If Tdocumento.Text = "" Then
                With Lestado_doc
                    .Text = ""
                    .ForeColor = System.Drawing.ColorTranslator.FromWin32(&H0&)
                End With
            End If
        End If
    End Sub

    Private Sub detalle_venta_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles detalle_venta.CellClick
        If e.RowIndex >= 0 Then
            'Dim valor As String
            If IsDBNull(detalle_venta.Rows(e.RowIndex).Cells(0).Value) Then
                busqueda_articulo.Show()
                busqueda_articulo.actualizar_origen("VP", Me)
                Me.Hide()
            End If
            'MsgBox(valor)
            'If String.IsNullOrEmpty(valor) Then

            'End If
        End If
    End Sub

    Public Sub cargar_formulario(ByVal filaRef As Integer)

        Dim cod_art, zona, desc_articulo, clase As String
        Dim parte, subzona, esta As Integer
        Dim cantid As Long
        Dim costo_ext, cost_uni As Double
        Dim existe As Boolean
        existe = False
        'reviso si la fila ya fue agregada en la factura
        For i = 0 To detalle_venta.Rows.Count - 2
            If detalle_venta.Rows(i).Cells(0).Value = filaRef Then
                existe = True
            End If
        Next
        If existe Then
            MsgBox("Venta ya ha sido ingresada a la factura actual", vbExclamation, "NH FOODS CHILE")
            Exit Sub
        End If
        'extraer la data asociada a la fila en el sistema
        Dim colchon As DataTable
        Dim sql_articulo As String
        sql_articulo = "SELECT cod_articulo,parte,dsc_breve,zona,cod_subzona,clase,cantidad,val_libro" & Chr(13) & _
        "FROM AFN_base_local2('" + Now.ToString("yyyyMMdd") + "','F')" & Chr(13) & _
        "WHERE fila=" + CStr(filaRef)
        colchon = maestro.ejecuta(sql_articulo)
        cantid = colchon.Rows(0).Item("cantidad")
        parte = colchon.Rows(0).Item("parte")
        cod_art = colchon.Rows(0).Item("cod_articulo")
        zona = colchon.Rows(0).Item("zona")
        desc_articulo = colchon.Rows(0).Item("dsc_breve")
        subzona = colchon.Rows(0).Item("cod_subzona")
        clase = colchon.Rows(0).Item("clase")
        costo_ext = colchon.Rows(0).Item("val_libro")
        cost_uni = costo_ext / cantid
        'reviso si la fila pertene a una otra factura en el sistema
        esta = maestro.Dcount("DOCVENTA", "AFN_DVENTA", "ID_ARTICULO=" + CStr(cod_art) + " and PARTE=" + CStr(parte))
        If esta > 0 Then
            'fila esta asociada al menos a una factura
            MessageBox.Show("Venta ya ha sido ingresada a otra factura en el sistema", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            'fila puede ser procesada para esta factura
            Dim origen_dato As DataTable = detalle_venta.DataSource
            'origen_dato = detalle_venta.DataSource
            Dim newfila As DataRow = origen_dato.NewRow
            newfila(0) = filaRef
            newfila(1) = cod_art
            newfila(2) = parte
            newfila(3) = desc_articulo
            newfila(4) = cantid
            newfila(5) = cost_uni
            newfila(6) = costo_ext
            newfila(7) = 0
            newfila(8) = 0
            newfila(9) = zona
            newfila(10) = subzona
            newfila(11) = clase
            newfila(12) = ""
            origen_dato.Rows.InsertAt(newfila, origen_dato.Rows.Count - 1)
            detalle_venta.DataSource = origen_dato
        End If
    End Sub

    Private Sub detalle_venta_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles detalle_venta.CellContentDoubleClick
        Dim fila, columna As Integer
        Dim valorOLD, valorNEW, descripcion, titulo As String
        Dim cantidad, total_doc As Long
        fila = e.RowIndex
        columna = e.ColumnIndex
        If fila >= 0 Then
            If columna = 7 Or columna = 8 Then
                valorOLD = detalle_venta.Rows(fila).Cells(columna).Value
                Select Case columna
                    Case 7
                        descripcion = "Ingrese precio unitario de la venta"
                        titulo = "Modificando Precio Unitario"
                    Case 8
                        descripcion = "Ingrese precio total de la venta"
                        titulo = "Modificando Precio Total"
                    Case Else
                        descripcion = ""
                        titulo = ""
                End Select
                valorNEW = InputBox(descripcion, titulo, valorOLD)
                If IsNumeric(valorNEW) Then
                    'esta ok
                    detalle_venta.Rows(fila).Cells(columna).Value = valorNEW
                    'calculamos otra columna, dependiendo de cual eligio
                    cantidad = detalle_venta.Rows(fila).Cells(4).Value
                    Select Case columna
                        Case 7      'precio unitario afectado
                            detalle_venta.Rows(fila).Cells(8).Value = CDbl(valorNEW) * cantidad
                            detalle_venta.Rows(fila).Cells(12).Value = "U"
                        Case 8      'precio total afectado
                            detalle_venta.Rows(fila).Cells(7).Value = CDbl(valorNEW) / cantidad
                            detalle_venta.Rows(fila).Cells(12).Value = "E"
                    End Select
                    'recalculo el total del documento
                    total_doc = 0
                    For i = 0 To detalle_venta.Rows.Count - 2
                        total_doc = total_doc + detalle_venta.Rows(i).Cells(8).Value
                    Next
                    TprecioExt.Text = Format(total_doc, "###" + getSeparadorMil() + "###")
                Else
                    If valorNEW <> "" Then
                        'ingresado no cumple con lo esperado
                        MsgBox("Solo pueden ingresarse números")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub detalle_venta_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles detalle_venta.KeyPress
        Dim respuesta As MsgBoxResult
        Dim fila, KeyAscii As Integer
        Dim total_doc As Long
        fila = detalle_venta.CurrentRow.Index
        KeyAscii = AscW(e.KeyChar)
        'MsgBox(KeyAscii)
        If fila >= 0 And fila < detalle_venta.Rows.Count - 1 Then
            If KeyAscii = 8 Then
                respuesta = MsgBox("¿Está seguro que desea eliminar la fila seleccionada?", vbCritical + vbYesNo, "NH FOODS CHILE")
                If respuesta = vbYes Then
                    detalle_venta.Rows.RemoveAt(fila)
                    'recalculo el total del documento
                    total_doc = 0
                    For i = 0 To detalle_venta.Rows.Count - 2
                        total_doc = total_doc + detalle_venta.Rows(i).Cells(8).Value
                    Next
                    TprecioExt.Text = Format(total_doc, "###" + getSeparadorMil() + "###")
                End If
            End If
        End If
    End Sub

    Private Sub btn_guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_guardar.Click
        'validar que el formulario contenga toda la información para guardar
        If Tdocumento.Text = "" Then
            MsgBox("Debe indicar el número del documento", vbExclamation, "NH FOODS CHILE")
            Tdocumento.Focus()
            Exit Sub
        End If
        'volvemos a chequear que el documento no exista en la base de datos
        Dim existe_doc As Integer
        Dim sql_cabecera, sql_detalle1, sql_detalle2 As String
        existe_doc = maestro.Dcount("DOCVENTA", "AFN_DVENTA", "DOCVENTA='" + Tdocumento.Text + "'")
        If existe_doc > 0 Then
            MsgBox("Número de documento ya ha sido utilizado", vbExclamation, "NH FOODS CHILE")
            Tdocumento.Focus()
            Exit Sub
        End If
        If detalle_venta.Rows.Count < 2 Then
            MsgBox("No ha ingresado información en el detalle de la factura", vbExclamation, "NH FOODS CHILE")
            detalle_venta.Focus()
            Exit Sub
        End If
        For i = 0 To detalle_venta.Rows.Count - 2
            If detalle_venta.Rows(i).Cells(8).Value = 0 Then
                MsgBox("Debe ingresar el precio para la fila " + CStr(i + 1) + " del detalle", vbExclamation, "NH FOODS CHILE")
                detalle_venta.Focus()
                Exit Sub
            End If
        Next
        'paso la validación, procedemos a guardar
        Dim codArticu, parte, zona, subzona, clase, tomo As String
        Dim diferencia, costoUnit, costoExtd, precioUni, precioExt, cantidad As Integer
        sql_cabecera = "INSERT INTO AFN_RVENTA(DOCVENTA,COST_EXT,DOCDATE) " + Chr(13) + _
            "VALUES('" + Tdocumento.Text + "'," + Replace(TprecioExt.Text, ",", "") + ",'" + DTfecha.Value.ToString("yyyyMMdd") + "')"
        maestro.ejecuta(sql_cabecera)
        sql_detalle1 = "INSERT INTO AFN_DVENTA(DOCVENTA,ID_ARTICULO,PARTE,CANTIDAD,UNITCOST,EXTCOST,UNITPRICE,EXTPRICE,ZONA,SUBZONA,CLASE,DOCLN)" + Chr(13)
        For i = 0 To detalle_venta.Rows.Count - 2
            'analizamos posibles decimales entre precio unitario y precio total
            Dim tempFila As DataGridViewRow = detalle_venta.Rows(i)
            codArticu = tempFila.Cells(1).Value
            parte = tempFila.Cells(2).Value
            cantidad = tempFila.Cells(4).Value
            costoUnit = tempFila.Cells(5).Value
            costoExtd = tempFila.Cells(6).Value
            precioUni = tempFila.Cells(7).Value
            precioExt = tempFila.Cells(8).Value
            zona = tempFila.Cells(9).Value
            subzona = tempFila.Cells(10).Value
            clase = tempFila.Cells(11).Value
            tomo = tempFila.Cells(12).Value

            diferencia = precioExt - (precioUni * cantidad)
            If diferencia = 0 Then
                'ingreso solo 1 registro
                sql_detalle2 = "VALUES('" + Tdocumento.Text + "'," + CStr(codArticu) + "," + CStr(parte) + "," + CStr(cantidad) + "," + CStr(costoUnit) + _
                "," + CStr(costoExtd) + "," + CStr(precioUni) + "," + CStr(precioExt) + ",'" + zona + "'," + CStr(subzona) + _
                ",'" + clase + "'," + CStr(i) + ")"
                maestro.ejecuta(sql_detalle1 + sql_detalle2)
            Else
                'ingreso 2 registros
                sql_detalle2 = "VALUES('" + Tdocumento.Text + "'," + CStr(codArticu) + "," + CStr(parte) + ",1," + CStr(costoUnit) + _
                "," + CStr(costoUnit) + "," + CStr(precioUni + diferencia) + "," + CStr(precioUni + diferencia) + ",'" + zona + "'," + CStr(subzona) + _
                ",'" + clase + "'," + CStr(i) + ")"
                maestro.ejecuta(sql_detalle1 + sql_detalle2)

                sql_detalle2 = "VALUES('" + Tdocumento.Text + "'," + CStr(codArticu) + "," + CStr(parte) + "," + CStr(cantidad - 1) + "," + CStr(costoUnit) + _
                "," + CStr(costoUnit * (cantidad - 1)) + "," + CStr(precioUni) + "," + CStr(precioUni * (cantidad - 1)) + ",'" + zona + "'," + CStr(subzona) + _
                ",'" + clase + "'," + CStr(i) + ")"
                maestro.ejecuta(sql_detalle1 + sql_detalle2)
            End If
            Application.DoEvents()
        Next
        MsgBox("Documento de venta ingresado correctamente al sistema", vbInformation, "NH FOODS CHILE")
        Me.Dispose()
    End Sub
End Class