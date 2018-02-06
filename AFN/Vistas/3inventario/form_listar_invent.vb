Imports System.Globalization
Imports System.Threading
Public Class form_listar_invent
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private maestro As New master_control

    Private Sub form_listar_invent_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub
    Private Sub form_listar_invent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Get culture information from current thread.
        Dim curCulture As CultureInfo = Thread.CurrentThread.CurrentCulture
        'Create TextInfo object.
        Dim tInfo As TextInfo = curCulture.TextInfo()

        Dim Tdato As DataTable
        Dim sql_text As String
        Label1.Text = "Inventarios Disponibles"
        Label2.Text = "Zonas Disponibles"
        Label3.Text = "Clase Disponibles"
        btn_mostrar.Text = "Mostrar"
        btn_mostrar.Visible = False
        sql_text = "SELECT DISTINCT fecha_inventario FROM AFN_TOMA_EXISTENCIA ORDER BY fecha_inventario DESC"
        Tdato = maestro.ejecuta(sql_text)
        Tdato.Columns.Add("mostrar")
        For Each fila As DataRow In Tdato.Rows
            Dim tfecha As Date = fila("fecha_inventario")
            fila("mostrar") = tInfo.ToTitleCase(tfecha.ToString("yyyy MMMM"))
        Next
        cboFech_Inv.ValueMember = "fecha_inventario"
        cboFech_Inv.DisplayMember = "mostrar"
        cboFech_Inv.DataSource = Tdato
        cboFech_Inv.SelectedIndex = -1
        limpiar_cuadrilla()
        dgv_mostrar.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        Me.MinimumSize = New Size(POpciones.Width, Me.Height)
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub form_listar_invent_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Dim largo_form As Integer
        largo_form = Me.Width - 16
        POpciones.Location = New Point(0, 0)
        POpciones.Width = largo_form
        dgv_mostrar.Location = New Point(20, POpciones.Height)
        dgv_mostrar.Width = largo_form - 40
        dgv_mostrar.Height = Me.Height - POpciones.Height - 40 - 16
    End Sub
    Private Sub POpciones_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles POpciones.Resize
        Dim largo_total, largo_label, largo_combo, largo_lista, largo_boton, C, espacio As Integer
        largo_total = POpciones.Width
        espacio = 10
        'determino largo maximo de cada tipo de control
        largo_label = 0
        largo_combo = 0
        largo_boton = 0
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
                If largo_boton < elemento.Width Then
                    largo_boton = elemento.Width
                End If
            End If
        Next
        largo_lista = largo_label + espacio + largo_combo + espacio + largo_boton
        C = (largo_total - largo_lista) / 2
        For Each elemento As Control In POpciones.Controls
            If TypeOf elemento Is Label Then
                elemento.Location = New Point(C, elemento.Location.Y)
            End If
            If TypeOf elemento Is ComboBox Then
                elemento.Location = New Point(C + largo_label + espacio, elemento.Location.Y)
            End If
            If TypeOf elemento Is Button Then
                elemento.Location = New Point(C + largo_lista - largo_boton, elemento.Location.Y)
            End If
        Next
    End Sub

    Private Sub cboFech_Inv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFech_Inv.SelectedIndexChanged
        If cboFech_Inv.SelectedIndex > -1 Then

            Dim colchon As DataTable
            'Dim periodo As Long
            Dim sql_zonas As String
            Dim fechaI As Date
            fechaI = cboFech_Inv.SelectedValue
            'fechaI = DateAdd("d", -1, DateAdd("M", 1, DateSerial(CInt(periodo / 100), CInt(periodo Mod 100), 1)))
            'CARGAR COMBO DE ZONAS
            sql_zonas = "SELECT DISTINCT " + Chr(10) + _
            "C.Fzona 'zona', C.FtextZona 'txt_zona'" + Chr(10) + _
            "FROM AFN_TOMA_EXISTENCIA A" + Chr(10) + _
            "inner join AFN_EXISTENCIA B on A.producto = B.producto" + Chr(10) + _
            "inner join AFN_DETALLE_ACTIVO('" + fechaI.ToString("yyyyMMdd") + "','F') C on B.lote_articulo=C.codigo and B.parte=C.parte" + Chr(10) + _
            "WHERE A.fecha_inventario='" + fechaI.ToString("yyyyMMdd") + "'"
            colchon = maestro.ejecuta(sql_zonas)
            For Each fila As DataRow In colchon.Rows
                fila(0) = Trim(fila(0))
                fila(1) = Trim(fila(1))
            Next
            cboZona.ValueMember = "zona"
            cboZona.DisplayMember = "txt_zona"
            cboZona.DataSource = colchon
            cboZona.SelectedIndex = -1
        Else
            cboZona.DataSource = Nothing
        End If
        cboClase.DataSource = Nothing
    End Sub
    Private Sub cboZona_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboZona.SelectedIndexChanged
        If cboZona.SelectedIndex <> -1 Then
            Dim colchon As DataTable
            Dim sql_clase, zona As String
            Dim fechaI As Date
            fechaI = cboFech_Inv.SelectedValue
            'fechaI = DateAdd("d", -1, DateAdd("M", 1, DateSerial(CInt(periodo / 100), CInt(periodo Mod 100), 1)))
            zona = cboZona.SelectedValue
            'CARGAR COMBO DE CLASES
            sql_clase = "SELECT DISTINCT " + Chr(10) + _
            "C.Fclase 'clase', C.FtextClase 'txt_clase'" + Chr(10) + _
            "FROM AFN_TOMA_EXISTENCIA A" + Chr(10) + _
            "inner join AFN_EXISTENCIA B on A.producto = B.producto" + Chr(10) + _
            "inner join AFN_DETALLE_ACTIVO('" + fechaI.ToString("yyyyMMdd") + "','F') C on B.lote_articulo=C.codigo and B.parte=C.parte" + Chr(10) + _
            "WHERE A.fecha_inventario='" + fechaI.ToString("yyyyMMdd") + "' and " + Chr(10) + _
            "C.Fzona='" + zona + "'"
            colchon = maestro.ejecuta(sql_clase)
            cboClase.DisplayMember = "txt_clase"
            cboClase.ValueMember = "clase"
            cboClase.DataSource = colchon
            cboClase.SelectedIndex = -1
        End If
    End Sub
    Private Sub cboClase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClase.SelectedIndexChanged
        Dim mostrar As Boolean
        mostrar = cboClase.SelectedIndex > -1
        btn_mostrar.Visible = mostrar

    End Sub
    Private Sub btn_mostrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_mostrar.Click
        Dim colchon, main_data As DataTable
        Dim fechaI As Date
        Dim zona, clase, Tfecha, sql_datos As String
        fechaI = cboFech_Inv.SelectedValue
        Tfecha = fechaI.ToString("yyyyMMdd")
        zona = cboZona.SelectedValue
        clase = cboClase.SelectedValue
        'bloquearC(dgv_mostrar)
        'CARGAR GRILLA
        sql_datos = "select B.estado, (select dscrpt from afn_estado_existencia where cod=B.estado)'txt_estado'," + Chr(10) + _
        "B.subzona, (select descrip from afn_subzona where cod=B.subzona)'txt_subzona'," + Chr(10) + _
        "C.ubicacion, (select G.descrip+'-'+F.descrip from afn_ubicacion F, afn_ubicacion G where F.sigue=G.cod and F.cod=C.ubicacion)'txt_ubicacion'," + Chr(10) + _
        "A.producto,A.descripcion,A.lote_articulo,A.valor_inicial,A.val_libro, A.fecha_compra,A.zona,A.clase from AFN_detalleXarticulo('" + zona + "','" + clase + "','" + Tfecha + "') A" + Chr(10) + _
        "inner join AFN_TOMA_EXISTENCIA B on A.producto = B.producto" + Chr(10) + _
        "inner join AFN_EXISTENCIA C on A.producto = C.producto" + Chr(10) + _
        "WHERE B.fecha_inventario='" + Tfecha + "'"

        colchon = maestro.ejecuta(sql_datos)
        main_data = limpiar_cuadrilla()
        For Each fila As DataRow In colchon.Rows
            Dim principal As DataRow = main_data.NewRow
            principal(0) = fila("zona")
            principal(1) = fila("clase")
            principal(2) = fila("producto")
            principal(3) = fila("lote_articulo")
            principal(4) = fila("descripcion")
            principal(5) = fila("fecha_compra")
            principal(6) = fila("valor_inicial")
            principal(7) = fila("val_libro")
            principal(8) = fila("txt_estado")
            principal(9) = fila("txt_subzona")
            principal(10) = fila("txt_ubicacion")

            main_data.Rows.Add(principal)
            Application.DoEvents()
        Next
        'desbloquearC(dgv_mostrar)
        MessageBox.Show("Se han registrado " + colchon.Rows.Count.ToString + " filas", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'dgv_mostrar.DataSource = main_data
    End Sub

    Private Function limpiar_cuadrilla() As DataTable
        Dim data_grilla As New DataTable
        data_grilla.Columns.Add("Zona", Type.GetType("System.String"))
        data_grilla.Columns.Add("Clase", Type.GetType("System.String"))
        data_grilla.Columns.Add("Producto", Type.GetType("System.String"))
        data_grilla.Columns.Add("Lote Articulos")
        data_grilla.Columns.Add("Descripcion")
        data_grilla.Columns.Add("Fecha Compra", Type.GetType("System.DateTime"))
        data_grilla.Columns.Add("Valor Compra", Type.GetType("System.Int32"))
        data_grilla.Columns.Add("Valor Libro", Type.GetType("System.Int32"))
        data_grilla.Columns.Add("Estado Actual")
        data_grilla.Columns.Add("Subzona Actual")
        data_grilla.Columns.Add("Ubicacion Actual")

        dgv_mostrar.DataSource = data_grilla
        dgv_mostrar.RowHeadersWidth = 20
        dgv_mostrar.AllowUserToAddRows = False
        dgv_mostrar.AllowUserToDeleteRows = False
        dgv_mostrar.AllowUserToResizeRows = False
        dgv_mostrar.AllowUserToResizeColumns = True
        dgv_mostrar.AllowUserToOrderColumns = True
        dgv_mostrar.ColumnHeadersHeight = 240 * 2
        dgv_mostrar.EditMode = DataGridViewEditMode.EditProgrammatically

        dgv_mostrar.Columns("Zona").Width = 50 * 0.75
        dgv_mostrar.Columns("Clase").Width = 50 * 0.75
        dgv_mostrar.Columns("Producto").Width = 150 * 0.75
        dgv_mostrar.Columns("Lote Articulos").Width = 85 * 0.75
        dgv_mostrar.Columns("Descripcion").Width = 526 * 0.75
        dgv_mostrar.Columns("Fecha Compra").Width = 100 * 0.75
        dgv_mostrar.Columns("Valor Compra").Width = 125 * 0.75
        dgv_mostrar.Columns("Valor Compra").DefaultCellStyle.Format = "N0"
        dgv_mostrar.Columns("Valor Libro").Width = 125 * 0.75
        dgv_mostrar.Columns("Valor Libro").DefaultCellStyle.Format = "N0"
        dgv_mostrar.Columns("Estado Actual").Width = 159 * 0.75
        dgv_mostrar.Columns("Subzona Actual").Width = 135 * 0.75
        dgv_mostrar.Columns("Ubicacion Actual").Width = 270 * 0.75

        Return data_grilla
    End Function

End Class