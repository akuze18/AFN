Public Class form_ingreso
    Enum cod_situacion As Integer
        nuevo = 0
        editable = 1
        activo = 2
    End Enum
    Private cual_sit As cod_situacion
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private base As New base_AFN

#Region "Funciones del formulario"
    Private Sub form_ingreso_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub
    Private Sub form_ingreso_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim indice As Integer
        Dim TPcontab, Tzona, Ttipo, Tcategoria, Tproveedor, Tatributo As DataTable
        Dim TPgestion As gestion
        'cargar información en los controles que no varian
        'periodo contable
        With cbFecha_ing
            TPcontab = base.periodo_contable
            .ValueMember = TPcontab.Columns(0).ColumnName
            .DisplayMember = TPcontab.Columns(1).ColumnName
            .DataSource = TPcontab
            For Each selected As DataRow In TPcontab.Select("selected=1")
                .SelectedValue = selected(0)
            Next
        End With
        'fecha de compra
        With Tfecha_compra
            .Value = base.perido_abierto_GP.first
            .CustomFormat = "dd-MM-yyyy"
            .MaxDate = Now
            .MinDate = DateSerial(Year(DateAdd(DateInterval.Month, -5, Now)), Month(DateAdd(DateInterval.Month, -5, Now)), 1)
        End With
        'zonas
        With cboZona
            Tzona = base.ZONAS_GL
            .ValueMember = Tzona.Columns(0).ColumnName
            .DisplayMember = Tzona.Columns(1).ColumnName
            .DataSource = Tzona
            .SelectedIndex = -1
        End With
        'consistencia / tipo
        With cboConsist
            Ttipo = base.TIPO_AF
            .DisplayMember = Ttipo.Columns(0).ColumnName
            .ValueMember = Ttipo.Columns(0).ColumnName
            .DataSource = Ttipo
            .SelectedIndex = 0
        End With
        'categoria
        With cboCateg
            Tcategoria = base.CATEGORIA
            .ValueMember = Tcategoria.Columns(0).ColumnName
            .DisplayMember = Tcategoria.Columns(1).ColumnName
            .DataSource = Tcategoria
            .SelectedIndex = -1
        End With
        'proveedor
        With cboProveedor
            Tproveedor = base.PROVEEDOR_GP
            .ValueMember = Tproveedor.Columns(0).ColumnName
            .DisplayMember = Tproveedor.Columns(1).ColumnName
            .DataSource = Tproveedor
            .SelectedIndex = -1
        End With
        'metodo valorizacion
        With cboMetod
            .Items.Clear()
            .Items.Add("COSTO")
            .Items.Add("REVALORIZACION")
        End With

        With cboGestion
            TPgestion = base.GESTIONES
            .Items.AddRange(TPgestion.GetAllArray)
            .SelectedIndex = -1
        End With

        'cargar combo atributos por grupo (paso3)
        With cbGatrib
            Tatributo = base.ATRIBUTOS
            .ValueMember = Tatributo.Columns(0).ColumnName
            .DisplayMember = Tatributo.Columns(1).ColumnName
            .DataSource = Tatributo
        End With
        'cargar combo atributos por articulo (paso4)
        With cbAatrib
            Tatributo = base.ATRIBUTOS
            .ValueMember = Tatributo.Columns(0).ColumnName
            .DisplayMember = Tatributo.Columns(1).ColumnName
            .DataSource = Tatributo
        End With

        'configuracion estética
        For Each pestaña As TabPage In pasos.TabPages
            pestaña.BackColor = Me.BackColor
        Next

        iniciar_formulario()
    End Sub
    Private Sub pasos_Selecting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles pasos.Selecting
        Dim contenedor As TabControl = CType(sender, TabControl)
        Dim pestaña As TabPage = contenedor.SelectedTab
        e.Cancel = Not pestaña.Enabled
    End Sub
#End Region

#Region "funciones de gestion de formulario"
    Private Function situacion() As cod_situacion
        If String.IsNullOrEmpty(artic.Text) Then
            'no hay elemento seleccionado
            Return cod_situacion.nuevo
        Else
            If CkEstado.Checked Then
                Return cod_situacion.activo
            Else
                Return cod_situacion.editable
            End If
        End If
    End Function
    Private Sub carga_superior()
        If Not String.IsNullOrEmpty(artic.Text) Then
            Dim colchon As DataTable
            colchon = base.DETALLE_FIN(artic.Text)
            Dim fila As DataRow = colchon.Rows(0)
            fuente.Text = fila("origen")
            TFulldescrip.Text = fila("dscrp") + fila("dsc_extra")
        Else
            fuente.Text = "REG"
            TFulldescrip.Text = String.Empty
        End If
    End Sub
    Private Sub cargar_base()
        'primer select para habilitar lo que corresponda segun el estado
        Select Case cual_sit
            Case cod_situacion.nuevo
                paso1.Enabled = True
                ActivarF(Tdescrip)
                ActivarF(cboZona)
                ActivarF(cboSubzona)
                ActivarF(cboConsist)
                ActivarF(cboClase)
                ActivarF(cboSubclase)
                ActivarF(cboCateg)

                ActivarF(cboProveedor)
                ActivarF(Tfecha_compra)
                ActivarF(cbFecha_ing)
                ActivarF(Tcantidad)
                ActivarF(Tprecio_compra)
                ActivarF(TvuF)
                ActivarF(Tdoc)
                ActivarF(Fderecho)
                ActivarF(derC1)
                ActivarF(derC2)
                ActivarF(ckIFRS)
                btn_guardar.Image = My.Resources._32_next
                btn_guardar.Text = "Guardar"
                btn_elim.Visible = False
                btn_act.Visible = False
            Case cod_situacion.editable
                paso1.Enabled = True
                ActivarF(Tdescrip)
                ActivarF(cboZona)
                ActivarF(cboSubzona)
                ActivarF(cboConsist, False)      'implica en la Clase, que no puede ser modificada
                ActivarF(cboClase, False)        'implica en los codigos de producto
                ActivarF(cboSubclase)
                ActivarF(cboCateg)

                ActivarF(cboProveedor)
                ActivarF(Tfecha_compra, False)   'implica en los codigos de producto
                ActivarF(cbFecha_ing)
                ActivarF(Tcantidad, False)       'implica en los codigos de producto
                ActivarF(Tprecio_compra, CBool(fuente.Text <> "OBC"))
                ActivarF(TvuF)
                ActivarF(Tdoc)
                ActivarF(Fderecho)
                ActivarF(derC1)
                ActivarF(derC2)
                ActivarF(ckIFRS)

                btn_guardar.Image = My.Resources._32_edit
                btn_guardar.Text = "Editar"
                btn_elim.Visible = True
                btn_act.Visible = True
            Case cod_situacion.activo
                paso1.Enabled = False
        End Select
        'segundo select para completar con los valores que correspondan
        Select Case cual_sit
            Case cod_situacion.editable
                'significa que se debe modificar
                Dim zona As String
                Dim hay, valor_ifrs As Integer
                Dim colchon As DataTable
                colchon = base.DETALLE_FIN(artic.Text)
                hay = colchon.Rows.Count
                If hay = 1 Then
                    'solo hay 1 registro para el codigo
                    Dim registro As DataRow = colchon.Rows(0)
                    residuo.Text = 0
                    Tdescrip.Text = registro("dscrp")
                    Try
                        zona = registro("zona")
                        cboZona.SelectedValue = zona
                        Try
                            cboSubzona.SelectedValue = registro("subzona")
                        Catch ex As Exception
                            If zona <> "30" Then
                                cboSubzona.SelectedIndex = -1
                            Else
                                cboSubzona.SelectedIndex = 0
                            End If
                        End Try
                    Catch ex As Exception
                        cboZona.SelectedIndex = -1
                    End Try

                    cboConsist.SelectedValue = registro("tipo")
                    cboClase.SelectedValue = registro("clase")
                    cboSubclase.SelectedValue = registro("subclase")
                    cboCateg.SelectedValue = registro("categoria")
                    Try
                        cboProveedor.SelectedValue = registro("proveedor")
                    Catch ex As Exception
                        cboProveedor.SelectedIndex = -1
                    End Try
                    Tfecha_compra.Value = registro("fecha_compra")
                    Try
                        cbFecha_ing.SelectedValue = registro("fecha_ing")
                    Catch ex As Exception
                        cbFecha_ing.SelectedIndex = -1
                    End Try
                    Tcantidad.Text = registro("cantidad")
                    Tprecio_compra.Text = Format(registro("precio_base"), "#,##0")
                    TvuF.Text = registro("vida_util_inicial")
                    Tdoc.Text = registro("num_doc")
                    If registro("derecho_credito") = "SI" Then
                        derC1.Checked = True
                    Else
                        derC2.Checked = True
                    End If
                    ckDepre.Checked = registro("se_deprecia")
                    valor_ifrs = base.DETALLE_IFRS_CLP(artic.Text).Rows.Count
                    If valor_ifrs = 0 Then
                        ckIFRS.Checked = False
                        'CkModIFRS.Value = 0
                        paso2.Enabled = False
                        'Call cargar_DxG()
                        'pasos.SelectedTab = paso1
                    Else
                        ckIFRS.Checked = True
                        'CkModIFRS.Value = 1
                        paso2.Enabled = True
                        'Call cargar_ifrs()
                        'pasos.SelectedTab = paso1
                    End If
                ElseIf hay = 2 Then
                    'revisar que los dos registros sean iguales, excepto el valor unitario y las cantidades
                    Dim QLcantidad(1), QLvida_util(1), QLparte(1) As Integer
                    Dim QLfecha_inicio(1) As Date
                    Dim QLzona(1), QLestado(1), QLclase(1) As String
                    Dim QLprecio(1) As Double
                    Dim revisar As Boolean
                    For i = 0 To 1
                        QLparte(i) = colchon.Rows(i).Item("parte")
                        QLfecha_inicio(i) = colchon.Rows(i).Item("fecha_inicio")
                        QLzona(i) = colchon.Rows(i).Item("zona")
                        QLestado(i) = colchon.Rows(i).Item("estado")
                        QLcantidad(i) = colchon.Rows(i).Item("cantidad")
                        QLprecio(i) = colchon.Rows(i).Item("precio_base")
                        QLclase(i) = colchon.Rows(i).Item("clase")
                        QLvida_util(i) = colchon.Rows(i).Item("vida_util_base")
                    Next
                    revisar = True
                    If Not (QLparte(0) = 0 And QLparte(1) = 1) And revisar Then
                        MsgBox("No corresponde a las partes necesarias", vbCritical, "NH FOODS CHILE")
                        revisar = False
                    End If      'partes solo deben ser 0 y 1
                    If QLfecha_inicio(0) <> QLfecha_inicio(1) And revisar Then
                        MsgBox("Fechas de inicio no son iguales", vbCritical, "NH FOODS CHILE")
                        revisar = False
                    End If      'fecha iguales
                    If QLzona(0) <> QLzona(1) And revisar Then
                        MsgBox("Zonas no son iguales", vbCritical, "NH FOODS CHILE")
                        revisar = False
                    End If      'zonas iguales
                    If QLestado(0) <> QLestado(1) And revisar Then
                        MsgBox("Estados no son iguales", vbCritical, "NH FOODS CHILE")
                        revisar = False
                    End If      'estados iguales
                    If Not (Math.Abs(QLprecio(0) - QLprecio(1)) = 1) And revisar Then
                        MsgBox("Precios no corresponden", vbCritical, "NH FOODS CHILE")
                        revisar = False
                    End If      'precios deben tener 1 peso de diferencia
                    If QLclase(0) <> QLclase(1) And revisar Then
                        MsgBox("Clases no son iguales", vbCritical, "NH FOODS CHILE")
                        revisar = False
                    End If      'clases iguales
                    If QLvida_util(0) <> QLvida_util(1) And revisar Then
                        MsgBox("Vida Utiles no son iguales", vbCritical, "NH FOODS CHILE")
                        revisar = False
                    End If      'vida util igual
                    If revisar Then
                        Dim registro As DataRow = colchon.Rows(0)
                        residuo.Text = QLcantidad(1)
                        Tdescrip.Text = registro("dscrp")
                        Try
                            zona = registro("zona")
                            cboZona.SelectedValue = zona
                            Try
                                cboSubzona.SelectedValue = registro("subzona")
                            Catch ex As Exception
                                If zona <> "30" Then
                                    cboSubzona.SelectedIndex = -1
                                Else
                                    cboSubzona.SelectedIndex = 0
                                End If
                            End Try
                        Catch ex As Exception
                            cboZona.SelectedIndex = -1
                            cboSubzona.SelectedIndex = -1
                        End Try
                        cboConsist.SelectedValue = registro("tipo")
                        cboClase.SelectedValue = registro("clase")
                        cboSubclase.SelectedValue = registro("subclase")
                        cboCateg.SelectedValue = registro("categoria")
                        Try
                            cboProveedor.SelectedValue = registro("proveedor")
                        Catch ex As Exception
                            cboProveedor.SelectedIndex = -1
                        End Try
                        Tfecha_compra.Value = registro("fecha_compra")
                        Try
                            cbFecha_ing.SelectedValue = registro("fecha_ing")
                        Catch ex As Exception
                            cbFecha_ing.SelectedIndex = -1
                        End Try
                        Tcantidad.Text = QLcantidad(0) + QLcantidad(1)
                        Tprecio_compra.Text = Format(registro("precio_base"), "#,##0")
                        TvuF.Text = registro("vida_util_inicial")
                        Tdoc.Text = registro("num_doc")
                        If registro("derecho_credito") = "SI" Then
                            derC1.Checked = True
                        Else
                            derC2.Checked = True
                        End If
                        ckDepre.Checked = registro("se_deprecia")
                        valor_ifrs = base.DETALLE_IFRS_CLP(artic.Text).Rows.Count
                        If valor_ifrs = 0 Then
                            ckIFRS.Checked = False
                            'CkModIFRS.Value = 0
                            paso2.Enabled = False
                            'Call cargar_DxG()
                            'pasos.SelectedTab = paso1
                        Else
                            ckIFRS.Checked = True
                            'CkModIFRS.Value = 1
                            paso2.Enabled = True
                            'Call cargar_ifrs()
                            'pasos.SelectedTab = paso1
                        End If
                    End If
                Else
                    MsgBox("La cantidad de registros no corresponde con el proceso", vbCritical, "NH FOODS CHILE")
                End If
            Case cod_situacion.nuevo
                'no se carga nada
            Case cod_situacion.activo
                'la hoja no esta activa, por lo que no es necesario hacerle nada
        End Select
    End Sub
    Private Sub cargar_ifrs()
        Select Case situacion()
            Case cod_situacion.nuevo, cod_situacion.activo
                paso2.Enabled = False
            Case cod_situacion.editable
                'reviso si existe IFRS cargado
                Dim cont_ifrs As Integer
                cont_ifrs = base.DETALLE_IFRS_CLP(artic.Text).Rows.Count
                If cont_ifrs > 0 Then
                    paso2.Enabled = True
                    'habilitar hoja IFRS
                    ActivarF(TvuI)
                    ActivarF(cboMetod)
                    ActivarF(Tval_resI)

                    Dim datos_ifrs, detalle_ifrs As DataTable
                    With DataIFRS
                        datos_ifrs = base.CUADRO_INGRESO_IFRS(artic.Text)
                        .DataSource = datos_ifrs
                        For Each columna As DataGridViewColumn In .Columns
                            columna.SortMode = DataGridViewColumnSortMode.NotSortable
                        Next
                        .Sort(.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
                        .Columns(0).Visible = False
                        .Columns(1).Width = 160
                        .Columns(2).DefaultCellStyle.Format = "C0"
                        .Columns(2).Width = 130
                        .Columns(3).DefaultCellStyle.Format = "C2"
                        .Columns(3).Width = 130
                    End With


                    'tipo cambio
                    xt.Text = base.TipoCambio(Tfecha_compra.Value)

                    detalle_ifrs = base.DETALLE_IFRS_CLP(artic.Text, 0)
                    'valor residual
                    Tval_resI.Text = Format(detalle_ifrs.Rows(0).Item("valor_residual"), "#,#0")
                    'vida util
                    TvuI.Text = detalle_ifrs.Rows(0).Item("vida_util_base")
                    'metodo valotizacion
                    cboMetod.SelectedIndex = detalle_ifrs.Rows(0).Item("metod_val") - 1

                    btn_IFRS.Image = My.Resources._32_next
                    btn_IFRS.Text = "Guardar"
                Else
                    paso2.Enabled = False
                End If
        End Select
    End Sub
    Private Sub cargar_DxG()
        Select Case situacion()
            Case cod_situacion.nuevo
                paso3.Enabled = False
                paso4.Enabled = False

            Case cod_situacion.editable, cod_situacion.activo
                paso3.Enabled = True
                paso4.Enabled = True
                'habilitar hoja detalle por lote
                ActivarF(cbGatrib)
                ActivarF(TGvalor)
                ActivarF(cbGvalor)
                ActivarF(btn_addGA)
                ActivarF(btn_lessGA)
                ActivarF(btn_detallexG)
                'habilitar hoja detalle por articulo
                ActivarF(cblistaArticulo)
                ActivarF(cbAatrib)
                ActivarF(TAvalor)
                ActivarF(cbAvalor)
                ActivarF(btn_addDA)
                ActivarF(btn_lessDA)
                ActivarF(btn_detallexA)
                Dim colchon As DataTable
                Dim TGproc, TAproc As DataTable
                'cargar combo codigos de los artículos (paso4)
                With cblistaArticulo
                    colchon = base.ARTICULO_INVENTARIO(artic.Text)
                    .DisplayMember = colchon.Columns(0).ColumnName
                    .ValueMember = colchon.Columns(0).ColumnName
                    .DataSource = colchon
                End With

                'controles que cambian según sea el atributo que se selecciona
                cbGvalor.Visible = False
                TGvalor.Visible = True
                cbAvalor.Visible = False
                TAvalor.Visible = True
                btn_buscaA.Visible = False      'para atributos de foto
                btn_buscaG.Visible = False      'para atributos de foto
                'agrego columnas a grilla resultado
                TGproc = base.lista_atributos_paso3
                AtribGrupo.DataSource = TGproc
                TAproc = base.lista_atributos_paso4
                AtribArticulo.DataSource = TAproc
                'traer los valores que correspondan para el lote, si los hubiera
                colchon = base.lista_atributo_inicial(artic.Text)
                For Each fila As DataRow In colchon.Rows
                    If fila.Item("codigo") = "" Then
                        Dim newfila As DataRow = TGproc.NewRow
                        newfila("Código Atributo") = fila.Item("cod_atrib")
                        If base.DET_ATRIBUTO(fila("cod_atrib"))("tipo") = "FOTO" Then
                            newfila("valor guardado") = "XX:" + fila.Item("detalle")
                        Else
                            newfila("valor guardado") = fila.Item("detalle")
                        End If
                        newfila("Atributo") = fila.Item("atributo")
                        newfila("Valor del atributo") = fila.Item("dscr_detalle")
                        newfila("Mostrar") = fila.Item("imprimir")
                        TGproc.Rows.Add(newfila)
                    Else
                        Dim newfila As DataRow = TAproc.NewRow
                        newfila("Código Atributo") = fila.Item("cod_atrib")
                        If base.DET_ATRIBUTO(fila("cod_atrib"))("tipo") = "FOTO" Then
                            newfila("valor guardado") = "XX:" + fila.Item("detalle")
                        Else
                            newfila("valor guardado") = fila.Item("detalle")
                        End If
                        newfila("Artículo") = fila.Item("codigo")
                        newfila("Atributo") = fila.Item("atributo")
                        newfila("Valor del atributo") = fila.Item("dscr_detalle")
                        newfila("Mostrar") = fila.Item("imprimir")
                        TAproc.Rows.Add(newfila)
                    End If
                Next
                With AtribGrupo
                    .ClearSelection()
                    'establecer visibilidad o ancho de columnas segun corresponda
                    .Columns(0).Visible = False
                    .Columns(1).Visible = False
                    .Columns(2).Width = 200
                    .Columns(3).Width = 355
                    .Columns(4).Width = 70
                    For Each columna As DataGridViewColumn In .Columns
                        columna.SortMode = DataGridViewColumnSortMode.NotSortable
                    Next
                    .Sort(.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
                    .AllowUserToResizeColumns = False
                    .AllowUserToResizeRows = False
                End With
                With AtribArticulo
                    .ClearSelection()
                    .Columns(0).Visible = False
                    .Columns(1).Visible = False
                    .Columns(2).Width = 110
                    .Columns(3).Width = 200
                    .Columns(4).Width = 325
                    .Columns(5).Width = 50
                    For Each columna As DataGridViewColumn In .Columns
                        columna.SortMode = DataGridViewColumnSortMode.NotSortable
                    Next
                    .Sort(.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
                    .AllowUserToResizeColumns = False
                    .AllowUserToResizeRows = False
                End With
        End Select
    End Sub

    ''' <summary>
    ''' Proceso para limpiar los valores de todo el formulario y dejarlo es estado NUEVO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub iniciar_formulario()
        Dim i As Integer
        Dim contabilizar As Vperiodo
        cual_sit = cod_situacion.nuevo
        'fuera de pasos
        artic.Text = String.Empty
        TFulldescrip.Text = String.Empty
        fuente.Text = "REG"
        CkEstado.Checked = False
        'paso1
        Tdescrip.Text = String.Empty
        cboZona.SelectedIndex = -1          'zonas ya se cargaron en el load del form
        cboConsist.SelectedIndex = 0
        cboClase.SelectedIndex = -1         'clases se carga desde tipo
        cboCateg.SelectedIndex = -1         'categoria ya se cargaron en el load del form
        cboProveedor.SelectedIndex = -1     'proveedor ya se cargaron en el load del form
        contabilizar = base.perido_abierto_GP
        Tfecha_compra.Value = contabilizar.first
        ckDepre.Checked = True
        'periodo contable
        i = 0
        For Each dato As DataRow In cbFecha_ing.DataSource.Rows
            If dato("CodPeriodo") = contabilizar.last Then
                cbFecha_ing.SelectedIndex = i
            End If
            i = i + 1
        Next
        residuo.Text = 0
        Tcantidad.Text = String.Empty
        Tprecio_compra.Text = String.Empty
        TvuF.Text = String.Empty
        Tdoc.Text = String.Empty
        derC1.Checked = True
        ckIFRS.Checked = False
        'paso2
        TvuI.Text = String.Empty
        Tval_resI.Text = String.Empty
        cboMetod.SelectedIndex = -1
        xt.Text = String.Empty
        DataIFRS.DataSource = Nothing
        'paso3
        cbGvalor.DataSource = Nothing   ' Items.Clear()
        cbGvalor.Visible = False
        TGvalor.Text = String.Empty
        TGvalor.Visible = True
        btn_buscaG.Visible = False
        'AtribGrupo.DataSource = Nothing
        'paso4
        cblistaArticulo.DataSource = Nothing ' .Items.Clear()
        cbAvalor.DataSource = Nothing '.Items.Clear()
        cbAvalor.Visible = False
        TAvalor.Text = String.Empty
        TAvalor.Visible = True
        btn_buscaA.Visible = False
        'AtribArticulo.DataSource = Nothing

        cargar_base()
        cargar_ifrs()
        cargar_DxG()
        seleccionar_pestaña(paso1)
    End Sub

    Public Sub resultado_busqueda(ByVal codigo As Integer, ByVal estado As Boolean)
        artic.Text = codigo
        CkEstado.Checked = estado
        cual_sit = situacion()
        carga_superior()
        cargar_base()
        cargar_ifrs()
        cargar_DxG()
        seleccionar_pestaña(paso1, paso3)
    End Sub
#End Region

#Region "funciones extra"
    Private Sub seleccionar_pestaña(ByVal pestaña As TabPage)
        For Each sheet As TabPage In pasos.TabPages
            pasos.SelectedTab = sheet
        Next
        pasos.SelectedTab = pestaña
    End Sub
    Private Sub seleccionar_pestaña(ByVal pestaña As TabPage, ByVal pestaña_op As TabPage)
        For Each sheet As TabPage In pasos.TabPages
            pasos.SelectedTab = sheet
        Next
        pasos.SelectedTab = pestaña_op
        pasos.SelectedTab = pestaña
    End Sub
    Private Function foto_name() As String
        Dim contenido(35), salida As String
        Dim i, j, k As Integer
        Dim valido As Boolean
        j = 0
        'entre 48 a 57: numeros
        'entre 65 a 90: letras mayusculas
        'entre 97 a 122: letras minusculas
        For i = 48 To 90
            If (i >= 48 And i <= 57) Or (i >= 65 And i <= 90) Then
                contenido(j) = Chr(i)
                j = j + 1
            End If
        Next
        valido = False
        Do
            Randomize()
            salida = ""
            For k = 0 To 7
                i = Int((j * Rnd()))
                salida = salida + contenido(i)
            Next
            If base.busca_nombre_foto(salida) = 0 Then
                valido = True
            End If
        Loop While Not valido

        Return salida

    End Function
#End Region

#Region "validaciones del propio formulario, actualizacion de combos en cascada"

    Private Sub cboZona_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboZona.SelectedIndexChanged
        cboSubzona.DataSource = Nothing
        If cboZona.SelectedIndex <> -1 Then
            Dim zona As String
            Dim Tsubzona As DataTable
            zona = cboZona.SelectedValue
            With cboSubzona
                Tsubzona = base.SUBZONAS_ACT(zona)
                .ValueMember = Tsubzona.Columns(0).ColumnName
                .DisplayMember = Tsubzona.Columns(1).ColumnName
                .DataSource = Tsubzona
                If Tsubzona.Rows.Count = 1 Then
                    .SelectedIndex = 0
                Else
                    .SelectedIndex = -1
                End If
                .MaxDropDownItems = 7
                .DropDownHeight = .ItemHeight * 7 + 2
            End With
        Else
            With cboSubzona
                .MaxDropDownItems = 1
                .DropDownHeight = cboSubzona.ItemHeight * 1 + 2
            End With
        End If
    End Sub
    Private Sub cboConsist_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboConsist.SelectedIndexChanged
        cboClase.DataSource = Nothing
        If cboConsist.SelectedIndex <> -1 Then
            Dim Tclase As DataTable
            'clase segun tipo
            With cboClase
                Tclase = base.CLASE(cboConsist.SelectedValue, True)
                .ValueMember = Tclase.Columns(0).ColumnName
                .DisplayMember = Tclase.Columns(1).ColumnName
                .DataSource = Tclase
                .SelectedIndex = -1
            End With
        End If
    End Sub
    Private Sub cboClase_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboClase.SelectedIndexChanged
        cboSubclase.DataSource = Nothing
        If cboClase.SelectedIndex <> -1 Then
            Dim clase As String
            Dim Tsubclase As DataTable
            clase = Trim(cboClase.SelectedValue)
            With cboSubclase
                Tsubclase = base.SUBCLASE(clase)
                .ValueMember = Tsubclase.Columns(0).ColumnName
                .DisplayMember = Tsubclase.Columns(1).ColumnName
                .DataSource = Tsubclase
                .SelectedIndex = -1
                .MaxDropDownItems = 7
                .DropDownHeight = .ItemHeight * 7 + 2
            End With
        Else
            With cboSubclase
                .MaxDropDownItems = 1
                .DropDownHeight = cboClase.ItemHeight * 1 + 2
            End With
        End If
    End Sub
    Private Sub cboSubclase_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSubclase.SelectedIndexChanged
        If cboSubclase.SelectedIndex <> -1 Then
            Dim subclase As String
            subclase = cboSubclase.SelectedValue
            TvuF.Text = base.SUBCLASE_DET(subclase)("vu_sug")
        Else
            TvuF.Text = String.Empty
        End If
    End Sub
    Private Sub Tcantidad_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tcantidad.LostFocus
        If Tcantidad.Text <> String.Empty Then
            If Not IsNumeric(Val(Tcantidad.Text)) Then
                MsgBox("Solo puede ingresar números en la cantidad")
                Tcantidad.Focus()
                Tcantidad.Text = String.Empty
            Else
                Tcantidad.Text = Format(Val(Tcantidad.Text), "#")
            End If
        End If
        'Validar_TxtPrecioTotal()
    End Sub
    Private Sub Tprecio_compra_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tprecio_compra.GotFocus
        Dim procesar As String
        procesar = Tprecio_compra.Text
        procesar = Strings.Replace(procesar, getSeparadorMil, "")
        Tprecio_compra.Text = Format(Val(procesar), "#")
    End Sub
    Private Sub Tprecio_compra_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tprecio_compra.LostFocus
        If Tprecio_compra.Text <> String.Empty Then
            Dim procesar, sepLi As String
            sepLi = getSeparadorMil()
            procesar = Tprecio_compra.Text
            procesar = Strings.Replace(procesar, sepLi, "")
            If Not IsNumeric(Val(procesar)) Then
                MsgBox("Solo puede ingresar números en la cantidad")
                Tprecio_compra.Focus()
                Tprecio_compra.Text = String.Empty
            Else
                Tprecio_compra.Text = Format(Val(procesar), "#,##0")
            End If
        End If
        'Validar_TxtPrecioTotal()
    End Sub
    Private Sub Validar_TxtPrecioTotal() Handles Tprecio_compra.TextChanged, Tcantidad.TextChanged
        If Not String.IsNullOrEmpty(Tcantidad.Text) And Not String.IsNullOrEmpty(Tprecio_compra.Text) Then
            Dim sepList As String
            sepList = getSeparadorMil()
            Dim Q, P, R As Integer
            Q = Val(Strings.Replace(Tcantidad.Text, sepList, ""))
            P = Val(Strings.Replace(Tprecio_compra.Text, sepList, ""))
            R = Val(residuo.Text)
            TxtPrecioTotal.Text = Format(Q * P + R, "#,##0")
        Else
            TxtPrecioTotal.Text = String.Empty
        End If
    End Sub
    Private Sub Tfecha_compra_ValueChanged(sender As System.Object, e As System.EventArgs) Handles Tfecha_compra.ValueChanged
        If Tfecha_compra.Value.Month = 1 And Tfecha_compra.Value.Day = 1 Then
            Tfecha_compra.Value = New Date(Tfecha_compra.Value.Year, 1, 2)
        End If
    End Sub

#End Region

#Region "Botones Paso 1"
    Private Sub btn_modif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_modif.Click
        busqueda_articulo.Show()
        busqueda_articulo.actualizar_origen("Mod", Me)
    End Sub
    Private Sub btn_new_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_new.Click
        iniciar_formulario()
    End Sub
    Private Sub btn_elim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_elim.Click
        Dim toma As DialogResult
        toma = MessageBox.Show("¿Está seguro que desea eliminar este registro?", "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If toma = Windows.Forms.DialogResult.Yes Then
            base.BORRAR_AF(artic.Text)
            MessageBox.Show("Se ha eliminado el registro correctamente", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            iniciar_formulario()
        End If
    End Sub
    Private Sub btn_act_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_act.Click
        Dim toma As DialogResult
        toma = MessageBox.Show("¿Está seguro que desea activar este registro? (ya no podrá ser modificado)", "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If toma = Windows.Forms.DialogResult.Yes Then
            base.ACTIVAR_AF(artic.Text)
            MessageBox.Show("Se ha activado el registro correctamente", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            iniciar_formulario()
        End If
    End Sub
    Private Sub btn_Bprov_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Bprov.Click
        bus_prov.Show()
        bus_prov.actualizar_origen("Mod", Me, Me.cboProveedor)
    End Sub
    Private Sub btn_guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_guardar.Click
        'validar información ingresada
        If Strings.Trim(Tdescrip.Text) = String.Empty Then
            MsgBox("Debe indicar la descripción del Activo Fijo", vbExclamation, "NH FOODS CHILE")
            Tdescrip.Focus()
            Exit Sub
        End If
        If cboZona.SelectedIndex = -1 Then
            MsgBox("Debe indicar la zona del Activo Fijo", vbExclamation, "NH FOODS CHILE")
            cboZona.Focus()
            Exit Sub
        End If
        If cboSubzona.SelectedIndex = -1 Then
            MsgBox("Debe indicar una subzona para el Activo Fijo", vbExclamation, "NH FOODS CHILE")
            cboSubzona.Focus()
            Exit Sub
        End If
        If cboClase.SelectedIndex = -1 Then
            MsgBox("Debe indicar la clase del Activo Fijo", vbExclamation, "NH FOODS CHILE")
            cboClase.Focus()
            Exit Sub
        End If
        If cboSubclase.SelectedIndex = -1 Then
            MsgBox("Debe indicar una subclase para el Activo Fijo", vbExclamation, "NH FOODS CHILE")
            cboSubclase.Focus()
            Exit Sub
        End If
        If cboCateg.SelectedIndex = -1 Then
            MsgBox("Debe indicar el proveedor del Activo Fijo", vbExclamation, "NH FOODS CHILE")
            cboCateg.Focus()
            Exit Sub
        End If
        If cboGestion.SelectedIndex = -1 Then
            MsgBox("Debe indicar la gestion del Activo Fijo", vbExclamation, "NH FOODS CHILE")
            cboGestion.Focus()
            Exit Sub
        End If
        If Tcantidad.Text = "" Then
            MsgBox("Debe indicar la cantidad de artículos", vbExclamation, "NH FOODS CHILE")
            Tcantidad.Focus()
            Exit Sub
        End If
        If Tprecio_compra.Text = "" Then
            MsgBox("Debe indicar el precio de adquisición del Activo Fijo", vbExclamation, "NH FOODS CHILE")
            Tprecio_compra.Focus()
            Exit Sub
        End If
        If TvuF.Text = "" Then
            MsgBox("Debe indicar la vida útil del artículo", vbExclamation, "NH FOODS CHILE")
            TvuF.Focus()
            Exit Sub
        End If
        If Tdoc.Text = "" Then
            Dim eleccion As DialogResult
            eleccion = MessageBox.Show("Desea continuar sin indicar el Nº de documento del Activo Fijo", "NH FOODS CHILE", MessageBoxButtons.YesNo)
            If eleccion <> Windows.Forms.DialogResult.Yes Then   'no marco SI
                Tdoc.Focus()
                Exit Sub
            End If
        End If
        If cbFecha_ing.SelectedIndex = -1 Then
            MessageBox.Show("Debe indicar el periodo contable del Activo Fijo", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cbFecha_ing.Focus()
            Exit Sub
        End If
        'fin validación
        'simplicación de variables
        Dim documento, derecho, origen, descrip, proveedor, pcompra, vutil, zona, cantidad, clase, _
        categoria, subzona, subclase, usuario, total_compra, CtiPo, depreciar As String
        Dim sGestion As gestion.fila
        Dim fcompra, fecha_contab As DateTime
        CtiPo = cboConsist.Text
        descrip = Tdescrip.Text
        fcompra = Tfecha_compra.Value
        pcompra = Format(Tprecio_compra.Text, "General Number")
        cantidad = Tcantidad.Text
        total_compra = Format(TxtPrecioTotal.Text, "General Number")
        vutil = TvuF.Text
        zona = cboZona.SelectedValue
        clase = cboClase.SelectedValue
        categoria = cboCateg.SelectedValue
        subzona = cboSubzona.SelectedValue
        subclase = cboSubclase.SelectedValue
        sGestion = cboGestion.SelectedItem
        usuario = form_welcome.GetUsuario
        If Tdoc.Text = "" Then
            documento = "SIN_DOCUMENTO"
        Else
            documento = Tdoc.Text
        End If
        If cboProveedor.SelectedIndex = -1 Then
            proveedor = "SIN_PROVEED"
        Else
            proveedor = Trim(cboProveedor.SelectedValue)
        End If
        If derC1.Checked Then
            derecho = "SI"
        Else
            derecho = "NO"
        End If
        origen = fuente.Text
        fecha_contab = cbFecha_ing.SelectedValue
        depreciar = Math.Abs(CInt(ckDepre.Checked))
        'fin reduccion de variables
        Dim mRS As DataRow
        Dim mensaje_final As String
        If cual_sit = cod_situacion.nuevo Then
            'ingreso en lote_articulos
            mRS = base.INGRESO_LOTE(descrip, fcompra, proveedor, documento, total_compra, vutil, derecho, fecha_contab, origen, CtiPo)
            If mRS("cod_sit") = -1 Then
                'se produjo un error en el insert, se debe avisar
                MsgBox("Se ha producido un error al momento de guardar el lote", vbCritical, "NH FOODS CHILE")
                Exit Sub
            End If
            Dim codigo As String = mRS("codigo")
            artic.Text = mRS("codigo")
            mRS = Nothing
            'checkear origen del ingreso para hacer match con obra en construccion
            If origen = "OBC" Then
                Dim monto, codEnt As String
                For Each fila As DataGridViewRow In form_ter_obra.salidaAF.Rows
                    monto = Format(fila.Cells(2).Value, "General Number")
                    codEnt = Format(fila.Cells(0).Value, "General Number")
                    mRS = base.EGRESO_OBC(codEnt, artic.Text, monto, zona)
                Next
                form_ter_obra.continuar = True
                form_ter_obra.Close()
            End If
            'ingreso primer registro FINANCIERO
            mRS = base.INGRESO_FINANCIERO(codigo, zona, cantidad, clase, categoria, subzona, subclase, depreciar, usuario, sGestion.ID)
            If mRS("cod_status") < 0 Then
                'se produjo un error en el insert, se debe avisar
                MsgBox(mRS("status"), vbCritical, "NH FOODS CHILE")
                Exit Sub
            End If
            'ingreso primer registro TRIBUTARIO
            mRS = base.INGRESO_TRIBUTARIO(codigo, zona, cantidad, clase, categoria, subzona, subclase, depreciar, usuario, sGestion.ID)
            If mRS("cod_status") < 0 Then
                'se produjo un error en el insert, se debe avisar
                MsgBox(mRS("status"), vbCritical, "NH FOODS CHILE")
                Exit Sub
            End If
            mRS = Nothing
            'analizo si tiene marcada la casilla IFRS
            If ckIFRS.Checked Then
                'entonces ingresamos el módulo ifrs con valores por defecto (despues ya podrá modificarlos si desea)
                Dim val_res, VUA, metod_val As String
                val_res = Math.Round(CLng(pcompra) * CDbl(base.IFRS_PREDET(clase)("pValRes")), 0)
                VUA = Math.Round(CInt(vutil) / 12 * 365)
                metod_val = base.IFRS_PREDET(clase)("metodVal")
                mRS = base.INGREGO_IFRS(codigo, val_res, VUA, metod_val, 0, 0, 0, 0, 0)
                If mRS("cod_status") < 0 Then
                    'se produjo un error en el procedimiento, se debe avisar
                    MsgBox(mRS("status"), vbCritical, "NH FOODS CHILE")
                    Exit Sub
                Else
                    btn_IFRS.Image = My.Resources._32_edit
                    btn_IFRS.Text = "Modificar"
                End If
                mRS = Nothing
            Else
                'como es nuevo, no hay que eliminar nada, puesto que no existe
            End If
            'homologar con los códigos de inventario
            mRS = base.GENERAR_CODIGO_INV(codigo)
            If mRS("cod_status") < 0 Then
                MsgBox(mRS("status"), vbCritical, "NH FOODS CHILE")
            End If
            mensaje_final = "Registro de articulo ingresado correctamente al Activo Fijo"
            cual_sit = cod_situacion.editable
        Else
            'modificacion en lote_articulos (ya que si esta activo, esta pestaña nunca esta disponible)
            mRS = base.MODIFICA_LOTE(artic.Text, descrip, proveedor, documento, total_compra, vutil, derecho, fecha_contab)
            If mRS("cod_sit") = -1 Then
                'se produjo un error en el insert, se debe avisar
                MsgBox("Se ha producido un error al momento de modificar el lote", vbCritical, "NH FOODS CHILE")
                Exit Sub
            End If
            mRS = Nothing
            'origen no cambia, asi que no se requiere este segmento de codigo

            'modifico primer historico FINANCIERO
            mRS = base.MODIFICA_FINANCIERO(artic.Text, zona, categoria, subzona, subclase, depreciar, usuario, sGestion.ID)
            If mRS("cod_status") < 0 Then
                'se produjo un error en el procedimiento, se debe avisar
                MsgBox(mRS("status"), vbCritical, "NH FOODS CHILE")
                Exit Sub
            End If
            mRS = Nothing
            'modifico primer historico TRIBUTARIO
            mRS = base.MODIFICA_TRIBUTARIO(artic.Text, zona, categoria, subzona, subclase, depreciar, usuario)
            If mRS("cod_status") < 0 Then
                'se produjo un error en el procedimiento, se debe avisar
                MsgBox(mRS("status"), vbCritical, "NH FOODS CHILE")
                Exit Sub
            End If
            mRS = Nothing
            'analizo si tiene marcada la casilla IFRS
            Dim cont_ifrs As Integer
            cont_ifrs = base.DETALLE_IFRS_CLP(artic.Text).Rows.Count
            If ckIFRS.Checked Then
                If cont_ifrs = 0 Then
                    'no existe en ifrs y se ha solicitado ingresarlo
                    Dim val_res, VUA, metod_val As String
                    val_res = Math.Round(CLng(pcompra) * base.IFRS_PREDET(clase)("pValRes"), 0)
                    VUA = Math.Round(CInt(vutil) / 12 * 365)
                    metod_val = base.IFRS_PREDET(clase)("metodVal")
                    mRS = base.INGREGO_IFRS(artic.Text, val_res, VUA, metod_val, 0, 0, 0, 0, 0)
                    If mRS("cod_status") < 0 Then
                        'se produjo un error en el procedimiento, se debe avisar
                        MsgBox(mRS("status"), vbCritical, "NH FOODS CHILE")
                        Exit Sub
                    Else
                        btn_IFRS.Image = My.Resources._32_edit
                        btn_IFRS.Text = "Modificar"
                    End If
                    mRS = Nothing
                Else
                    'si esta marcado y creado, no es necesario hacer algo
                End If
            Else
                If cont_ifrs > 0 Then
                    'si esta desmarcado y existe, se debe eliminar el registro ifrs del artículo
                    mRS = base.ELIMINA_IFRS(artic.Text)
                    If mRS("cod_status") < 0 Then
                        'se produjo un error en el procedimiento, se debe avisar
                        MsgBox(mRS("status"), vbCritical, "NH FOODS CHILE")
                        Exit Sub
                    End If
                    mRS = Nothing
                Else
                    'si no esta marcado y no esta creado, no es necesario hacer algo
                End If
            End If
            mensaje_final = "Registro de articulo modificado correctamente al Activo Fijo"
        End If
        carga_superior()
        cargar_ifrs()
        cargar_DxG()
        If ckIFRS.Checked Then
            seleccionar_pestaña(paso2)
        Else
            seleccionar_pestaña(paso3)
        End If
        MessageBox.Show(mensaje_final, "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub TabControl1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles pasos.DrawItem

        'Firstly we'll define some parameters.
        Dim CurrentTab As TabPage = pasos.TabPages(e.Index)
        Dim ItemRect As Rectangle = pasos.GetTabRect(e.Index)
        Dim FillBrush As New SolidBrush(System.Drawing.SystemColors.InactiveCaption)
        Dim TextBrush As New SolidBrush(System.Drawing.SystemColors.InactiveCaptionText)
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        'If we are currently painting the Selected TabItem we'll 
        'change the brush colors and inflate the rectangle.
        If CBool(e.State And DrawItemState.Selected) Then
            FillBrush.Color = Color.ForestGreen
            TextBrush.Color = Color.Black
            ItemRect.Inflate(2, 2)
        ElseIf CurrentTab.Enabled Then
            FillBrush.Color = Color.LightGreen
            TextBrush.Color = Color.Gray
            ItemRect.Inflate(2, 2)
        End If

        'Set up rotation for left and right aligned tabs
        If pasos.Alignment = TabAlignment.Left Or pasos.Alignment = TabAlignment.Right Then
            Dim RotateAngle As Single = 90
            If pasos.Alignment = TabAlignment.Left Then RotateAngle = 270
            Dim cp As New PointF(ItemRect.Left + (ItemRect.Width \ 2), ItemRect.Top + (ItemRect.Height \ 2))
            e.Graphics.TranslateTransform(cp.X, cp.Y)
            e.Graphics.RotateTransform(RotateAngle)
            ItemRect = New Rectangle(-(ItemRect.Height \ 2), -(ItemRect.Width \ 2), ItemRect.Height, ItemRect.Width)
        End If

        'Next we'll paint the TabItem with our Fill Brush
        e.Graphics.FillRectangle(FillBrush, ItemRect)

        'Now draw the text.
        e.Graphics.DrawString(CurrentTab.Text, e.Font, TextBrush, RectangleF.op_Implicit(ItemRect), sf)

        'Reset any Graphics rotation
        e.Graphics.ResetTransform()

        'Finally, we should Dispose of our brushes.
        FillBrush.Dispose()
        TextBrush.Dispose()

    End Sub
#End Region

#Region "Botones Paso 2"
    Private Sub Tval_resI_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tval_resI.GotFocus
        With Tval_resI
            .Text = Format(Tval_resI.Text, "General Number")
            .SelectionStart = 0
            .SelectionLength = Len(.Text)
        End With
    End Sub
    Private Sub Tval_resI_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tval_resI.LostFocus
        Tval_resI.Text = Format(Tval_resI.Text, "Standard")
        Tval_resI.Text = Strings.Left(Tval_resI.Text, Len(Tval_resI.Text) - 3)
    End Sub
    Private Sub btn_IFRS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_IFRS.Click
        'validar campos vacios
        If TvuI.Text = "" Then
            MsgBox("Debe indicar la vida útil en días", vbExclamation, "NH FOODS CHILE")
            TvuI.Focus()
            Exit Sub
        End If
        If Tval_resI.Text = "" Then
            MsgBox("Debe indicar el valor residual", vbExclamation, "NH FOODS CHILE")
            Tval_resI.Focus()
            Exit Sub
        End If
        If cboMetod.SelectedIndex = -1 Then
            MsgBox("Debe indicar el tipo de valorización", vbExclamation, "NH FOODS CHILE")
            cboMetod.Focus()
            Exit Sub
        End If
        'reduccion de campos
        Dim VUA, val_res, metod_val, prepa, trans, monta, desma, honor As String
        prepa = "0"
        desma = "0"
        trans = "0"
        monta = "0"
        honor = "0"
        VUA = TvuI.Text
        val_res = Format(Tval_resI.Text, "General Number")
        metod_val = CStr(cboMetod.SelectedIndex + 1)
        For Each fila As DataGridViewRow In DataIFRS.Rows
            Select Case fila.Cells(0).Value
                Case 1
                    prepa = Format(fila.Cells(2).Value, "General Number")
                Case 2
                    desma = Format(fila.Cells(2).Value, "General Number")
                Case 3
                    trans = Format(fila.Cells(2).Value, "General Number")
                Case 4
                    monta = Format(fila.Cells(2).Value, "General Number")
                Case 5
                    honor = Format(fila.Cells(2).Value, "General Number")
            End Select
        Next
        Dim colchon As DataRow
        'modifico el datos
        colchon = base.MODIFICA_IFRS(artic.Text, val_res, VUA, metod_val, prepa, trans, monta, desma, honor)
        If colchon("cod_status") < 0 Then
            'se produjo un error en el insert, se debe avisar
            MsgBox(colchon("status"), vbCritical, "NH FOODS CHILE")
        Else
            MsgBox("Registro se modificó correctamente al módulo Activo Fijo IFRS", vbInformation, "NH FOODS CHILE")
        End If
        colchon = Nothing
        seleccionar_pestaña(paso3)
    End Sub
    Private Sub DataIFRS_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataIFRS.CellDoubleClick
        Dim fila As Integer
        fila = e.RowIndex
        If fila > -1 Then
            Dim nombre_fila, opcion, valor_actual As String
            nombre_fila = DataIFRS.Rows(fila).Cells(1).Value
            valor_actual = Format(DataIFRS.Rows(fila).Cells(2).Value, "General Number")
            opcion = InputBox("Ingrese nuevo valor para " + nombre_fila, "NH FOODS CHILE", valor_actual)
            If Not String.IsNullOrEmpty(opcion) Then
                If IsNumeric(opcion) Then
                    DataIFRS.Rows(fila).Cells(2).Value = opcion
                    DataIFRS.Rows(fila).Cells(3).Value = Math.Round(Val(opcion) / Val(xt.Text), 0)
                Else
                    MessageBox.Show("Solo puede ingresar números", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
        End If
    End Sub
#End Region

#Region "Botones Paso 3"
    Private Sub cbGatrib_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbGatrib.SelectedIndexChanged
        Dim cod_atrib As String
        If cbGatrib.SelectedIndex <> -1 Then
            cod_atrib = cbGatrib.SelectedValue
            If base.DET_ATRIBUTO(cod_atrib)("tipo") = "COMBO" Then
                Dim colchon As DataTable
                If base.DET_ATRIBUTO(cod_atrib)("atributo") = "SUBZONA" Then
                    colchon = base.SUBZONAS_ACT
                Else
                    colchon = base.ubicacion
                End If
                With cbGvalor
                    .DisplayMember = colchon.Columns(1).ColumnName
                    .ValueMember = colchon.Columns(0).ColumnName
                    .DataSource = colchon
                    .Visible = True
                End With

                TGvalor.Visible = False         'oculto opcion de texto
                btn_buscaG.Visible = False      'oculto boton para buscar archivos
                cbGvalor.Focus()
            Else
                cbGvalor.Visible = False
                TGvalor.Visible = True
                TGvalor.Text = String.Empty
                If base.DET_ATRIBUTO(cod_atrib)("tipo") = "FOTO" Then
                    'opciones de fotos
                    btn_buscaG.Visible = True
                Else
                    btn_buscaG.Visible = False
                End If
                TGvalor.Focus()
            End If
        End If
    End Sub
    Private Sub btn_buscaG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_buscaG.Click
        dialogo.Title = "Elija el archivo"
        dialogo.Filter = "Archivos de Imagen|*.jpg;*.bmp;*.gif;*.png"
        dialogo.ShowDialog()
        If dialogo.FileName <> "" Then
            TGvalor.Text = dialogo.FileName
        End If
    End Sub
    Private Sub btn_addGA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_addGA.Click
        Dim cod_atrib As String
        If cbGatrib.SelectedIndex <> -1 Then
            cod_atrib = cbGatrib.SelectedValue
            'reviso que el atributo tenga valor para ingresarlo
            If base.DET_ATRIBUTO(cod_atrib)("tipo") = "COMBO" Then
                If cbGvalor.SelectedIndex = -1 Then
                    MessageBox.Show("No ha seleccionado un valor para " + LCase(cbGatrib.Text), "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    cbGvalor.Focus()
                    Exit Sub
                End If
            Else
                If String.IsNullOrEmpty(Trim(TGvalor.Text)) Then
                    MessageBox.Show("No ha ingresado un valor para " + LCase(cbGatrib.Text), "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TGvalor.Focus()
                    Exit Sub
                End If
                If base.DET_ATRIBUTO(cod_atrib)("tipo") = "FOTO"" Then" Then
                    'si es foto reviso que sea una direccion valida
                    If IO.File.Exists(TGvalor.Text) Then
                        'existe archivo, esta bien
                    Else
                        MessageBox.Show("Archivo indicado para la foto no existe", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        TGvalor.Focus()
                        With TGvalor
                            .SelectionStart = 0
                            .SelectionLength = Len(.Text)
                        End With
                        Exit Sub
                    End If
                Else
                    TGvalor.Text = UCase(TGvalor.Text)
                End If
            End If
            'reviso que el atributo no este ingresado
            Dim listado_ingreso As DataTable
            listado_ingreso = AtribGrupo.DataSource
            For i = 0 To listado_ingreso.Rows.Count - 1
                If listado_ingreso.Rows(i).Item(0) = cod_atrib Then
                    MessageBox.Show("Atributo ya ha sido establecido", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next
            'ingreso atributo a la lista
            Dim nuevo_atributo As DataRow = listado_ingreso.NewRow
            nuevo_atributo(0) = cod_atrib
            nuevo_atributo(2) = cbGatrib.Text
            nuevo_atributo(4) = ckMostrar.Checked
            If base.DET_ATRIBUTO(cod_atrib)("tipo") = "COMBO" Then
                nuevo_atributo(1) = cbGvalor.SelectedValue
                nuevo_atributo(3) = cbGvalor.SelectedText
            Else
                nuevo_atributo(1) = TGvalor.Text
                If base.DET_ATRIBUTO(cod_atrib)("tipo") = "FOTO" Then
                    Dim inicio, final As Integer
                    inicio = 0
                    final = 1
                    While final <> 0
                        final = InStr(inicio + 1, TGvalor.Text, "\")
                        If final <> 0 Then
                            inicio = final
                        End If
                    End While
                    nuevo_atributo(3) = Mid(TGvalor.Text, inicio + 1)
                Else
                    nuevo_atributo(3) = TGvalor.Text
                End If
            End If
            listado_ingreso.Rows.Add(nuevo_atributo)
            AtribGrupo.ClearSelection()
            cbGatrib.SelectedIndex = -1
            TGvalor.Text = String.Empty
            cbGvalor.SelectedIndex = -1
            ckMostrar.Checked = False
            cbGatrib.Focus()
        End If
    End Sub
    Private Sub btn_lessGA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lessGA.Click
        Dim listado_borrar As DataTable
        listado_borrar = AtribGrupo.DataSource
        For Each fila As DataGridViewRow In AtribGrupo.SelectedRows
            AtribGrupo.Rows.Remove(fila)
        Next
        AtribGrupo.ClearSelection()
        cbGatrib.Focus()
    End Sub
    Private Sub btn_detallexG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_detallexG.Click
        Dim mensaje As String
        Dim colchon As DataTable
        Dim lote_art, atributo, detalle, val_foto, Antig, solo_foto, nueva_foto As String
        Dim nuevo, estaba, malo, elimina, mostrar As Integer
        Dim tengo_atrib, resultado As DataRow
        Dim disponible, Copiar As Boolean
        nuevo = 0
        estaba = 0
        malo = 0
        If AtribGrupo.Rows.Count > 0 Then
            nueva_foto = ""
            detalle = ""
            Antig = ""
            For i = 0 To AtribGrupo.Rows.Count - 1
                lote_art = artic.Text
                atributo = AtribGrupo.Rows(i).Cells(0).Value
                mostrar = AtribGrupo.Rows(i).Cells(4).Value
                tengo_atrib = base.INV_ATRIBUTOxLOTE(lote_art, atributo)
                If IsNothing(tengo_atrib) Then
                    If base.DET_ATRIBUTO(atributo)("tipo") = "FOTO" Then
                        'generamos el nuevo nombre para la foto
                        val_foto = foto_name()
                        'no existe la foto, por lo tanto podemos crearlo
                        solo_foto = val_foto + "." + Strings.Right(AtribGrupo.Rows(i).Cells(1).Value, 3)
                        nueva_foto = base.dirFotos + solo_foto
                        detalle = solo_foto
                        Copiar = True
                    Else
                        detalle = AtribGrupo.Rows(i).Cells(1).Value
                    End If
                Else
                    If base.DET_ATRIBUTO(atributo)("tipo") = "FOTO" Then
                        Antig = tengo_atrib("detalle")
                        If Strings.Left(AtribGrupo.Rows(i).Cells(1).Value, 3) = "XX:" Then
                            'foto viene desde el servidor cargada, no necesito crearla de nuevo
                            detalle = Mid(AtribGrupo.Rows(i).Cells(1).Value, 4)
                            Copiar = False
                        Else
                            'foto nueva ingresada
                            'generamos el nuevo nombre para la foto
                            val_foto = foto_name()
                            'no existe la foto, por lo tanto podemos crearlo
                            solo_foto = val_foto + "." + Strings.Right(AtribGrupo.Rows(i).Cells(1).Value, 3)
                            nueva_foto = base.dirFotos + solo_foto
                            detalle = solo_foto
                            Copiar = True
                        End If
                    Else
                        detalle = AtribGrupo.Rows(i).Cells(1).Value
                    End If
                End If
                
                resultado = base.INGRESO_ATRIB_LOTE(lote_art, atributo, detalle, mostrar)
                Select Case resultado("estado")
                    Case 1      'nuevo
                        nuevo = nuevo + 1
                        If base.DET_ATRIBUTO(atributo)("tipo") = "FOTO" Then
                            'copio la foto al server una vez que ya deje el registro en la BD
                            IO.File.Copy(AtribGrupo.Rows(i).Cells(1).Value, nueva_foto)
                            AtribGrupo.Rows(i).Cells(1).Value = "XX:" + detalle
                        End If
                    Case 0      'existente
                        estaba = estaba + 1
                        If base.DET_ATRIBUTO(atributo)("tipo") = "FOTO" Then
                            If Copiar Then
                                'copio la foto al server una vez que ya deje el registro en la BD
                                IO.File.Copy(AtribGrupo.Rows(i).Cells(1).Value, nueva_foto)
                                AtribGrupo.Rows(i).Cells(1).Value = "XX:" + detalle
                                If Antig <> detalle Then
                                    'borro antigua
                                    Kill(base.dirFotos + Antig)
                                End If
                            End If
                        End If
                    Case -1     'error
                        malo = malo + 1
                End Select
                Application.DoEvents()
            Next
        End If
        'una vez que recorri toda la grilla (o estaba vacia), debo eliminar de la base de datos aquellos que no esten listados
        elimina = 0
        colchon = base.INV_ATRIBUTOxLOTE(artic.Text)
        For Each fila As DataRow In colchon.Rows
            atributo = fila("cod_atrib")
            detalle = fila("detalle")
            disponible = False
            For i = 0 To AtribGrupo.Rows.Count - 1
                If AtribGrupo.Rows(i).Cells(0).Value = atributo Then
                    disponible = True
                End If
                Application.DoEvents()
            Next
            If Not disponible Then
                'ya no esta disponible en la grilla, entonces lo sacamos de la BD tb
                base.BORRAR_ATRIBUTOxLOTE(artic.Text, atributo)
                elimina = elimina + 1
            End If
            Application.DoEvents()
        Next

        If nuevo = 0 And estaba = 0 And elimina = 0 And malo = 0 Then
            MessageBox.Show("No hay registros para ingresar al detalle por grupo", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        mensaje = "Proceso completado :" & vbCrLf & vbCrLf
        If nuevo <> 0 Then
            mensaje = mensaje + "   Nuevos  " + CStr(nuevo) & vbCrLf
        End If
        If estaba <> 0 Then
            mensaje = mensaje + "   Actualizados  " + CStr(estaba) & vbCrLf
        End If
        If elimina <> 0 Then
            mensaje = mensaje + "   Eliminados  " + CStr(elimina) & vbCrLf
        End If
        If malo <> 0 Then
            mensaje = mensaje + "   Fallidos  " + CStr(malo) & vbCrLf
        End If
        MsgBox(mensaje, vbInformation, "NH FOODS CHILE")
        carga_superior()

    End Sub
#End Region

#Region "Botones Paso 4"
    Private Sub cbAatrib_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAatrib.SelectedIndexChanged
        Dim cod_atrib As String
        If cbAatrib.SelectedIndex <> -1 Then
            cod_atrib = cbAatrib.SelectedValue
            If base.DET_ATRIBUTO(cod_atrib)("tipo") = "COMBO" Then
                Dim colchon As DataTable
                If base.DET_ATRIBUTO(cod_atrib)("atributo") = "SUBZONA" Then
                    colchon = base.SUBZONAS_ACT
                Else
                    colchon = base.ubicacion
                End If
                With cbAvalor
                    .DisplayMember = colchon.Columns(1).ColumnName
                    .ValueMember = colchon.Columns(0).ColumnName
                    .DataSource = colchon
                    .Visible = True
                End With
                TAvalor.Visible = False         'oculto opcion de texto
                btn_buscaA.Visible = False      'oculto boton para buscar archivos
                cbAvalor.Focus()
            Else
                cbAvalor.Visible = False
                TAvalor.Visible = True
                TAvalor.Text = String.Empty
                If base.DET_ATRIBUTO(cod_atrib)("tipo") = "FOTO" Then
                    'opciones de fotos
                    btn_buscaA.Visible = True
                Else
                    btn_buscaA.Visible = False
                End If
                TAvalor.Focus()
            End If
        End If
    End Sub
    Private Sub btn_buscaA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_buscaA.Click
        dialogo.Title = "Elija el archivo"
        dialogo.Filter = "Archivos de Imagen|*.jpg;*.bmp;*.gif;*.png"
        dialogo.ShowDialog()
        If dialogo.FileName <> "" Then
            TAvalor.Text = dialogo.FileName
        End If
    End Sub
    Private Sub btn_addDA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_addDA.Click
        Dim cod_atrib, cod_articulo As String
        If cbAatrib.SelectedIndex <> -1 Then
            If cblistaArticulo.SelectedIndex = -1 Then
                MessageBox.Show("No ha seleccionado un articulo específico", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cblistaArticulo.Focus()
                Exit Sub
            End If
            cod_atrib = cbAatrib.SelectedValue
            cod_articulo = cblistaArticulo.SelectedValue
            'reviso que el atributo tenga valor para ingresarlo
            If base.DET_ATRIBUTO(cod_atrib)("tipo") = "COMBO" Then
                If cbAvalor.SelectedIndex = -1 Then
                    MessageBox.Show("No ha seleccionado un valor para " + LCase(cbAatrib.Text), "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    cbAvalor.Focus()
                    Exit Sub
                End If
            Else
                If String.IsNullOrEmpty(Trim(TAvalor.Text)) Then
                    MessageBox.Show("No ha ingresado un valor para " + LCase(cbAatrib.Text), "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TAvalor.Focus()
                    Exit Sub
                End If
                If base.DET_ATRIBUTO(cod_atrib)("tipo") = "FOTO" Then
                    'si es foto reviso que sea una direccion valida
                    If IO.File.Exists(TAvalor.Text) Then
                        'existe archivo, esta bien
                    Else
                        MessageBox.Show("Archivo indicado para la foto no existe", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        With TAvalor
                            .Focus()
                            .SelectionStart = 0
                            .SelectionLength = Len(.Text)
                        End With
                        Exit Sub
                    End If
                Else
                    TAvalor.Text = UCase(TAvalor.Text)
                End If
            End If
            'reviso que el atributo no este ingresado
            Dim listado_ingreso As DataTable
            listado_ingreso = AtribArticulo.DataSource
            For i = 0 To listado_ingreso.Rows.Count - 1
                If listado_ingreso.Rows(i).Item(0) = cod_atrib And listado_ingreso.Rows(i).Item(2) = cod_articulo Then
                    MessageBox.Show("Atributo ya ha sido establecido para el articulo indicado", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next
            'ingreso atributo a la lista
            Dim nuevo_atributo As DataRow = listado_ingreso.NewRow
            nuevo_atributo(0) = cod_atrib
            nuevo_atributo(2) = cod_articulo
            nuevo_atributo(3) = cbAatrib.Text
            nuevo_atributo(5) = ckMostrarA.Checked
            If base.DET_ATRIBUTO(cod_atrib)("tipo") = "COMBO" Then
                nuevo_atributo(1) = cbAvalor.SelectedValue
                nuevo_atributo(4) = cbAvalor.SelectedText
            Else
                nuevo_atributo(1) = TAvalor.Text
                If base.DET_ATRIBUTO(cod_atrib)("tipo") = "FOTO" Then
                    Dim inicio, final As Integer
                    inicio = 0
                    final = 1
                    While final <> 0
                        final = InStr(inicio + 1, TAvalor.Text, "\")
                        If final <> 0 Then
                            inicio = final
                        End If
                    End While
                    nuevo_atributo(4) = Mid(TAvalor.Text, inicio + 1)
                Else
                    nuevo_atributo(4) = TAvalor.Text
                End If
            End If
            listado_ingreso.Rows.Add(nuevo_atributo)
            'restablezco valores
            AtribArticulo.ClearSelection()
            cbAatrib.SelectedIndex = -1
            TAvalor.Text = String.Empty
            cbAvalor.SelectedIndex = -1
            cbAatrib.Focus()
        End If
    End Sub
    Private Sub btn_lessDA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_lessDA.Click
        Dim listado_borrar As DataTable
        listado_borrar = AtribArticulo.DataSource
        For Each fila As DataGridViewRow In AtribArticulo.SelectedRows
            AtribArticulo.Rows.Remove(fila)
        Next
        AtribArticulo.ClearSelection()
        cbAatrib.Focus()
    End Sub
    Private Sub btn_detallexA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_detallexA.Click
        Dim colchon As DataTable
        Dim lote_art, atributo, detalle, val_foto, Antig, _
            solo_foto, nueva_foto, articulo As String
        Dim nuevo, estaba, malo, elimina, mostrar As Integer
        Dim disponible, Copiar As Boolean
        Dim tengo_atributo, resultado As DataRow
        nuevo = 0
        estaba = 0
        malo = 0
        elimina = 0
        If AtribArticulo.Rows.Count > 0 Then
            nueva_foto = ""
            detalle = ""
            Antig = ""
            For i = 0 To AtribArticulo.Rows.Count - 1
                lote_art = artic.Text
                atributo = AtribArticulo.Rows(i).Cells(0).Value
                articulo = AtribArticulo.Rows(i).Cells(2).Value
                mostrar = AtribArticulo.Rows(i).Cells(5).Value

                tengo_atributo = base.INV_ATRIBUTOxITEM(lote_art, articulo, atributo)
                If IsNothing(tengo_atributo) Then
                    If base.DET_ATRIBUTO(atributo)("tipo") = "FOTO" Then
                        'generamos el nuevo nombre para la foto
                        val_foto = foto_name()
                        'no existe la foto, por lo tanto podemos crearlo
                        solo_foto = val_foto + "." + Strings.Right(AtribArticulo.Rows(i).Cells(1).Value, 3)
                        nueva_foto = base.dirFotos + solo_foto
                        detalle = solo_foto
                        Copiar = True
                    Else
                        detalle = AtribArticulo.Rows(i).Cells(1).Value
                    End If
                Else
                    If base.DET_ATRIBUTO(atributo)("tipo") = "FOTO" Then
                        Antig = tengo_atributo("detalle")
                        If Strings.Left(AtribArticulo.Rows(i).Cells(1).Value, 3) = "XX:" Then
                            'foto viene desde el servidor cargada, no necesito crearla de nuevo
                            detalle = Mid(AtribArticulo.Rows(i).Cells(1).Value, 4)
                            Copiar = False
                        Else
                            'foto nueva ingresada
                            'generamos el nuevo nombre para la foto
                            val_foto = foto_name()
                            'no existe la foto, por lo tanto podemos crearlo
                            solo_foto = val_foto + "." + Strings.Right(AtribArticulo.Rows(i).Cells(1).Value, 3)
                            nueva_foto = base.dirFotos + solo_foto
                            detalle = solo_foto
                            Copiar = True
                        End If
                    Else
                        detalle = AtribArticulo.Rows(i).Cells(1).Value
                    End If
                End If
                resultado = base.INGRESO_ATRIB_ITEM(lote_art, articulo, atributo, detalle, mostrar)
                Select Case resultado("estado")
                    Case 1      'nuevo
                        nuevo = nuevo + 1
                        If base.DET_ATRIBUTO(atributo)("tipo") = "FOTO" Then
                            'copio la foto al server una vez que ya deje el registro en la BD
                            IO.File.Copy(AtribArticulo.Rows(i).Cells(1).Value, nueva_foto)
                            AtribArticulo.Rows(i).Cells(1).Value = "XX:" + detalle
                        End If
                    Case 0      'existente
                        estaba = estaba + 1
                        If base.DET_ATRIBUTO(atributo)("tipo") = "FOTO" Then
                            If Copiar Then
                                'copio la foto al server una vez que ya deje el registro en la BD
                                IO.File.Copy(AtribArticulo.Rows(i).Cells(1).Value, nueva_foto)
                                AtribArticulo.Rows(i).Cells(1).Value = "XX:" + detalle
                                If Antig <> detalle Then
                                    'borro antigua
                                    Kill(base.dirFotos + Antig)
                                End If
                            End If
                        End If
                    Case -1     'error
                        malo = malo + 1
                End Select
                Application.DoEvents()
            Next
        End If
        'una vez que recorri toda la grilla (o estaba vacia), debo eliminar de la base de datos aquellos que no esten
        colchon = base.INV_ATRIBUTOxITEM(artic.Text)
        For Each fila As DataRow In colchon.Rows
            atributo = fila("cod_atrib")
            detalle = fila("detalle")
            articulo = fila("codigo")
            disponible = False
            For i = 0 To AtribArticulo.Rows.Count - 1
                If AtribArticulo.Rows(i).Cells(0).Value = atributo And AtribArticulo.Rows(i).Cells(2).Value = articulo Then
                    disponible = True
                End If
                Application.DoEvents()
            Next
            If Not disponible Then
                'ya no esta disponible en la grilla, entonces lo sacamos de la BD tb
                base.BORRAR_ATRIBUTOxITEM(artic.Text, articulo, atributo)
                elimina = elimina + 1
            End If
            Application.DoEvents()
        Next

        If nuevo = 0 And estaba = 0 And elimina = 0 And malo = 0 Then
            MessageBox.Show("No hay registros para ingresar al detalle por grupo", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim mensaje As String
        mensaje = "Proceso completado :" & vbCrLf & vbCrLf
        If nuevo <> 0 Then
            mensaje = mensaje + "   Nuevos  " + CStr(nuevo) & vbCrLf
        End If
        If estaba <> 0 Then
            mensaje = mensaje + "   Actualizados  " + CStr(estaba) & vbCrLf
        End If
        If elimina <> 0 Then
            mensaje = mensaje + "   Eliminados  " + CStr(elimina) & vbCrLf
        End If
        If malo <> 0 Then
            mensaje = mensaje + "   Fallidos  " + CStr(malo) & vbCrLf
        End If
        MsgBox(mensaje, vbInformation, "NH FOODS CHILE")
        carga_superior()
    End Sub
#End Region

#Region "Comunes para Paso 3 y 4"
    Private Sub btn_imprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_imprimir.Click, btn_imprimir1.Click
        form_welcome.AF_ficha_ingreso(artic.Text)
    End Sub
    Private Sub Atributo_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles AtribGrupo.CellDoubleClick, AtribArticulo.CellDoubleClick
        Dim LAtrib As DataGridView = CType(sender, DataGridView)
        Dim fil, col, col_most As Integer
        If LAtrib.Name = "AtribGrupo" Then
            col_most = 4
        Else
            col_most = 5
        End If
        fil = e.RowIndex
        col = e.ColumnIndex
        If fil <> -1 And col = col_most Then
            LAtrib.Rows(fil).Cells(col).Value = Not CBool(LAtrib.Rows(fil).Cells(col).Value)
        End If
    End Sub
    Private Sub Atributo_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles AtribGrupo.CellClick, AtribArticulo.CellClick
        Dim LAtrib As DataGridView = CType(sender, DataGridView)
        If e.RowIndex = -1 Then
            LAtrib.ClearSelection()
        End If
    End Sub
#End Region

End Class