Imports Excel = Microsoft.Office.Interop.Excel
Public Class form_ficha_cambio
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private maestro As New master_control
    Private base As New base_AFN

    Private Sub form_ficha_cambio_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub

    Private Sub form_ficha_cambio_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'agrego columnas
        Dim recordset As New DataTable
        recordset.Columns.Add("Código Artículo", Type.GetType("System.Int32"))
        recordset.Columns.Add("parte", Type.GetType("System.Int32"))
        recordset.Columns.Add("Fecha Cambio", Type.GetType("System.DateTime"))
        recordset.Columns.Add("Cantidad Cambio", Type.GetType("System.Int32"))
        recordset.Columns.Add("Zona Actual")
        recordset.Columns.Add("Subzona Actual")
        recordset.Columns.Add("Zona Anterior")
        recordset.Columns.Add("Subzona Anterior")
        recordset.Columns.Add("zona_actual")
        recordset.Columns.Add("zona_anterior")
        recordset.Columns.Add("subzona_actual")
        recordset.Columns.Add("subzona_anterior")
        Tarticulo.Enabled = False
        With lista_cambios
            .DataSource = recordset
            .ColumnHeadersHeight = .ColumnHeadersHeight * 2
            .RowHeadersVisible = False
            .AllowUserToResizeColumns = False
            For Each columna As DataGridViewColumn In .Columns
                columna.SortMode = DataGridViewColumnSortMode.NotSortable
                'columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            Next
            .Columns(0).Width = 60
            .Columns(1).Visible = False
            .Columns(2).Width = 67
            .Columns(3).Width = 60
            .Columns(4).Width = 300
            .Columns(5).Width = 300
            .Columns(6).Width = 300
            .Columns(7).Width = 300
            .Columns(8).Visible = False
            .Columns(9).Visible = False
            .Columns(10).Visible = False
            .Columns(11).Visible = False
        End With
        iniciar_fomulario()
        btn_consulta_Click(Me, System.EventArgs.Empty)

    End Sub

    Private Sub iniciar_fomulario()
        Dim datos As DataTable
        cod_art.Text = String.Empty
        Tarticulo.Text = String.Empty
        datos = lista_cambios.DataSource
        datos.Rows.Clear()
    End Sub

    Private Sub btn_consulta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_consulta.Click
        busqueda_articulo.Show()
        busqueda_articulo.actualizar_origen("FC", Me)
    End Sub

    Private Sub btn_imprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_imprimir.Click
        If lista_cambios.SelectedRows.Count > 0 Then
            For Each fila As DataGridViewRow In lista_cambios.SelectedRows
                imprimir_cambio(fila.Cells("Código Artículo").Value, fila.Cells("parte").Value, fila.Cells("Fecha Cambio").Value)
            Next
        End If
    End Sub

    'funciones publicas
    Public Sub cargar_formulario(ByVal cod_articulo As Integer)
        Dim resultado, datos As DataTable
        Dim sql_cambio As String
        sql_cambio = "SELECT * FROM AFN_listar_cambios('F') WHERE cod_articulo=" + cod_articulo.ToString
        resultado = maestro.ejecuta(sql_cambio)
        datos = lista_cambios.DataSource
        datos.Rows.Clear()
        If resultado.Rows.Count > 0 Then
            For Each fila As DataRow In resultado.Rows
                Dim newfila As DataRow = datos.NewRow
                newfila("Código Artículo") = fila("cod_articulo")
                newfila("parte") = fila("parte")
                newfila("Fecha Cambio") = fila("fecha_inicio")
                newfila("Cantidad Cambio") = fila("cantidad")
                newfila("Zona Actual") = fila("txt_zona_act")
                newfila("Subzona Actual") = fila("txt_subz_act")
                newfila("Zona Anterior") = fila("txt_zona_ant")
                newfila("Subzona Anterior") = fila("txt_subz_ant")
                newfila("zona_actual") = fila("zona_actual")
                newfila("zona_anterior") = fila("zona_anterior")
                newfila("subzona_actual") = fila("subzona_actual")
                newfila("subzona_anterior") = fila("subzona_anterior")
                datos.Rows.Add(newfila)
            Next
            lista_cambios.ClearSelection()
            Tarticulo.Text = resultado.Rows(0).Item("descrip_artic")
            cod_art.Text = cod_articulo
        Else
            lista_cambios.ClearSelection()
            Tarticulo.Text = String.Empty
            cod_art.Text = String.Empty
            Mensaje_IT("Código indicado no registra cambios de zona")
        End If
    End Sub

    Public Sub imprimir_cambio(ByVal cod_articulo As Integer, ByVal parte As Integer, ByVal fecha_inicio As Date)
        Dim fin_clp, tributario, ifrs_clp, ifrs_yen, info_articulos As DataTable
        Dim inf_art_grupo, inf_art_solo, inf_art_foto As DataRow()
        Dim sql_datos, dir_archivos, PArchivo, Narchivo As String
        Dim DocAbierto As Boolean
        sql_datos = "SELECT * FROM AFN_listar_cambios('F') WHERE cod_articulo=" + cod_articulo.ToString + " and parte=" + parte.ToString + " and fecha_inicio='" + fecha_inicio.ToString("yyyyMMdd") + "'"
        fin_clp = maestro.ejecuta(sql_datos)
        sql_datos = "SELECT * FROM AFN_listar_cambios('T') WHERE cod_articulo=" + cod_articulo.ToString + " and parte=" + parte.ToString + " and fecha_inicio='" + fecha_inicio.ToString("yyyyMMdd") + "'"
        tributario = maestro.ejecuta(sql_datos)
        sql_datos = "SELECT * FROM AFN_listar_cambios('GC') WHERE cod_articulo=" + cod_articulo.ToString + " and parte=" + parte.ToString + " and fecha_inicio='" + fecha_inicio.ToString("yyyyMMdd") + "'"
        ifrs_clp = maestro.ejecuta(sql_datos)
        sql_datos = "SELECT * FROM AFN_listar_cambios('GY') WHERE cod_articulo=" + cod_articulo.ToString + " and parte=" + parte.ToString + " and fecha_inicio='" + fecha_inicio.ToString("yyyyMMdd") + "'"
        ifrs_yen = maestro.ejecuta(sql_datos)
        sql_datos = "AFN_reporte_inicio2 " + cod_articulo.ToString
        info_articulos = maestro.ejecuta(sql_datos)
        dir_archivos = base.dirArchivos
        PArchivo = ""
        DocAbierto = True      'asumo que archivo está abierto
        Do
            Narchivo = "ficha_cambio" + PArchivo + ".xlsx"
            Try
                IO.File.WriteAllBytes(dir_archivos + Narchivo, My.Resources.ficha_cambio)
                DocAbierto = False
            Catch
                'archivo está abierto al intentar sobreescribir, cambio la parte
                If PArchivo = "" Then
                    PArchivo = "(1)"
                Else
                    Dim numero As Integer = CInt(PArchivo.Substring(1, PArchivo.Length - 2))
                    numero = numero + 1
                    PArchivo = "(" + CStr(numero) + ")"
                End If
            End Try
        Loop While DocAbierto
        Dim oExcel As New Excel.Application
        Dim oBook As Excel.Workbook = oExcel.Workbooks.Open(dir_archivos + Narchivo)
        Dim HojaMain As Excel.Worksheet
        Dim TmpFecha As Date
        Dim inicio2 As Integer
        inicio2 = 14
        HojaMain = oBook.Sheets(1)
        inf_art_grupo = info_articulos.Select("codigo='' and cod_atrib not in(16,17,18)")
        inf_art_solo = info_articulos.Select("codigo<>'' and cod_atrib not in(16,17,18)")
        inf_art_foto = info_articulos.Select("cod_atrib in(16,17,18)")
        With HojaMain
            .Cells(1, 5).Value = fin_clp.Rows(0).Item("cod_articulo")
            .Cells(1, 13).Value = fin_clp.Rows(0).Item("descrip_artic")
            .Cells(3, 5).Value = fin_clp.Rows(0).Item("cantidad")
            .Cells(3, 14).Value = fin_clp.Rows(0).Item("txt_clase")
            .Cells(3, 28).Value = fin_clp.Rows(0).Item("txt_subclase")
            .Cells(5, 4).Value = fin_clp.Rows(0).Item("origen")
            .Cells(5, 14).Value = fin_clp.Rows(0).Item("txt_zona_act")
            .Cells(5, 28).Value = fin_clp.Rows(0).Item("txt_subz_act")
            TmpFecha = fin_clp.Rows(0).Item("fecha_inicio")
            .Cells(7, 5).Value = TmpFecha.ToString("dd-MM-yyyy")
            .Cells(7, 14).Value = fin_clp.Rows(0).Item("txt_zona_ant")
            .Cells(7, 28).Value = fin_clp.Rows(0).Item("txt_subz_ant")
            TmpFecha = fin_clp.Rows(0).Item("fecha_compra")
            .Cells(9, 5).Value = TmpFecha.ToString("dd-MM-yyyy")
            .Cells(9, 14).Value = fin_clp.Rows(0).Item("txt_categoria")
            .Cells(9, 28).Value = fin_clp.Rows(0).Item("num_doc")
            .Cells(11, 6).Value = fin_clp.Rows(0).Item("credito")
            .Cells(11, 14).Value = fin_clp.Rows(0).Item("id_proveedor")
            If fin_clp.Rows(0).Item("id_proveedor") <> Trim(fin_clp.Rows(0).Item("txt_proveedor")) Then
                .Cells(12, 14).Value = Trim(fin_clp.Rows(0).Item("txt_proveedor"))
            Else
                Dim fila_extra As Excel.Range
                fila_extra = .Rows(12)
                fila_extra.Delete()
                fila_extra = Nothing
                inicio2 = inicio2 - 1
            End If
            If ifrs_clp.Rows.Count > 0 Then
                'valor para metodo de valorizacion
                .Cells(11, 29).Value = ifrs_clp.Rows(0).Item("metod_val")
            Else
                'limpio el titulo de metodo valorizacion
                .Cells(11, 23).Value = String.Empty
                .Cells(11, 29).Value = String.Empty
            End If
            'financiero
            .Cells(inicio2 + 2, 7).Value = fin_clp.Rows(0).Item("precio_base")
            .Cells(inicio2 + 3, 7).Value = fin_clp.Rows(0).Item("vida_util")
            .Cells(inicio2 + 4, 7).Value = fin_clp.Rows(0).Item("val_residual")
            .Cells(inicio2 + 5, 7).Value = fin_clp.Rows(0).Item("depreciacion_acum")
            'tributario
            .Cells(inicio2 + 2, 10).Value = tributario.Rows(0).Item("precio_base")
            .Cells(inicio2 + 3, 10).Value = tributario.Rows(0).Item("vida_util")
            .Cells(inicio2 + 4, 10).Value = tributario.Rows(0).Item("val_residual")
            .Cells(inicio2 + 5, 10).Value = tributario.Rows(0).Item("depreciacion_acum")
            'analizo si existe ifrs o no
            If ifrs_clp.Rows.Count > 0 And ifrs_yen.Rows.Count > 0 Then
                'ingreso ifrs clp
                .Cells(inicio2 + 2, 13).Value = ifrs_clp.Rows(0).Item("precio_base")
                .Cells(inicio2 + 3, 13).Value = ifrs_clp.Rows(0).Item("vida_util")
                .Cells(inicio2 + 4, 13).Value = ifrs_clp.Rows(0).Item("val_residual")
                .Cells(inicio2 + 5, 13).Value = ifrs_clp.Rows(0).Item("depreciacion_acum")
                .Cells(inicio2 + 6, 13).Value = ifrs_clp.Rows(0).Item("preparacion")
                .Cells(inicio2 + 7, 13).Value = ifrs_clp.Rows(0).Item("transporte")
                .Cells(inicio2 + 8, 13).Value = ifrs_clp.Rows(0).Item("montaje")
                .Cells(inicio2 + 9, 13).Value = ifrs_clp.Rows(0).Item("desmantel")
                .Cells(inicio2 + 10, 13).Value = ifrs_clp.Rows(0).Item("honorario")
                .Cells(inicio2 + 11, 13).Value = ifrs_clp.Rows(0).Item("revalorizacion")
                'ingreso ifrs yen
                .Cells(inicio2 + 2, 16).Value = ifrs_yen.Rows(0).Item("precio_base")
                .Cells(inicio2 + 3, 16).Value = ifrs_yen.Rows(0).Item("vida_util")
                .Cells(inicio2 + 4, 16).Value = ifrs_yen.Rows(0).Item("val_residual")
                .Cells(inicio2 + 5, 16).Value = ifrs_yen.Rows(0).Item("depreciacion_acum")
                .Cells(inicio2 + 6, 16).Value = ifrs_yen.Rows(0).Item("preparacion")
                .Cells(inicio2 + 7, 16).Value = ifrs_yen.Rows(0).Item("transporte")
                .Cells(inicio2 + 8, 16).Value = ifrs_yen.Rows(0).Item("montaje")
                .Cells(inicio2 + 9, 16).Value = ifrs_yen.Rows(0).Item("desmantel")
                .Cells(inicio2 + 10, 16).Value = ifrs_yen.Rows(0).Item("honorario")
                .Cells(inicio2 + 11, 16).Value = ifrs_yen.Rows(0).Item("revalorizacion")
            Else
                'limpio celdas que eran para ifrs
                Dim zona1, zona2 As Excel.Range
                zona1 = .Range("B" + (inicio2 + 6).ToString + ":R" + (inicio2 + 11).ToString + "")
                zona2 = .Range("M" + (inicio2 + 1).ToString + ":R" + (inicio2 + 11).ToString + "")
                With zona1
                    .Value = ""
                    .Borders(Excel.XlBordersIndex.xlEdgeRight).ColorIndex = 2
                    .Borders(Excel.XlBordersIndex.xlEdgeLeft).ColorIndex = 2
                    .Borders(Excel.XlBordersIndex.xlEdgeBottom).ColorIndex = 2
                    .Borders(Excel.XlBordersIndex.xlInsideHorizontal).ColorIndex = 2
                    .Borders(Excel.XlBordersIndex.xlInsideVertical).ColorIndex = 2
                End With
                With zona2
                    .Value = ""
                    .Borders(Excel.XlBordersIndex.xlEdgeTop).ColorIndex = 2
                    .Borders(Excel.XlBordersIndex.xlEdgeRight).ColorIndex = 2
                    .Borders(Excel.XlBordersIndex.xlEdgeBottom).ColorIndex = 2
                    .Borders(Excel.XlBordersIndex.xlInsideHorizontal).ColorIndex = 2
                    .Borders(Excel.XlBordersIndex.xlInsideVertical).ColorIndex = 2
                End With
                zona1 = Nothing
                zona2 = Nothing
            End If
            'revisar detalle de articulos
            Dim pos1 As Integer
            pos1 = inicio2
            'oExcel.Visible = True
            If inf_art_grupo.Count > 0 Then
                'agrego descripcion de grupo
                Dim Rtitulo As Excel.Range = .Cells(pos1, 20)
                With Rtitulo
                    .Value = "DESCRIPCIÓN GENERICA"
                    .Font.Bold = True
                    .Font.Underline = True
                End With
                Rtitulo = Nothing
                pos1 = pos1 + 2
                For Each info_fila As DataRow In inf_art_grupo
                    .Cells(pos1, 20).Value = info_fila("atributo")
                    .Cells(pos1, 24).Value = info_fila("dscr_detalle")
                    .Range("X" + pos1.ToString + ":AK" + pos1.ToString).Merge()
                    pos1 = pos1 + 1
                Next
                pos1 = pos1 + 1
            End If
            If inf_art_solo.Count > 0 Then
                'agrego descripcion de articulos individuales
                Dim Rtitulo As Excel.Range = .Cells(pos1, 20)
                With Rtitulo
                    .Value = "DESCRIPCIÓN ESPECIFICA"
                    .Font.Bold = True
                    .Font.Underline = True
                End With
                Rtitulo = Nothing
                pos1 = pos1 + 2
                For Each info_fila As DataRow In inf_art_solo
                    .Cells(pos1, 20).Value = info_fila("codigo")
                    .Cells(pos1, 24).Value = info_fila("atributo")
                    .Cells(pos1, 28).Value = info_fila("dscr_detalle")
                    .Range("X" + pos1.ToString + ":AA" + pos1.ToString).Merge()
                    .Range("AB" + pos1.ToString + ":AK" + pos1.ToString).Merge()
                    pos1 = pos1 + 1
                Next
            End If
        End With
        'fin de oSheet(1)

        'reviso fotos
        Dim HojaFoto As Excel.Worksheet
        HojaFoto = oBook.Sheets(2)
        If inf_art_foto.Count > 0 Then
            'determino si ocupare solo la hoja que existe o si debo copiar mas
            If inf_art_foto.Count > 1 Then
                'copio la hoja las veces que necesite
                For i = 2 To inf_art_foto.Count
                    HojaFoto.Copy(After:=HojaFoto)
                Next
            End If
            'declaro variables necesarias
            Dim indice_hoja As Integer
            Dim imagen, nom_pagina As String
            indice_hoja = 2
            For Each fila_foto In inf_art_foto
                Dim HTfoto As Excel.Worksheet = oBook.Sheets(indice_hoja)
                If fila_foto("codigo") = "" Then
                    nom_pagina = fila_foto("atributo")
                Else
                    nom_pagina = fila_foto("atributo") + "-"
                    nom_pagina = nom_pagina + Strings.Right(fila_foto("codigo"), 31 - nom_pagina.Length)
                End If
                imagen = base.dirFotos + fila_foto("detalle")
                With HTfoto
                    .Name = nom_pagina
                    .Shapes.AddPicture(imagen, False, True, 0, 0, 709, 482)
                End With

                indice_hoja = indice_hoja + 1
            Next
        Else
            'borro hoja de fotos ya que no se usara
            HojaFoto.Delete()
        End If
        HojaMain.Select()

        HojaMain = Nothing
        HojaFoto = Nothing

        oExcel.Visible = True
    End Sub
End Class