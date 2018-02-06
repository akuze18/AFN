Public Class multicriterio
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private maestro As New master_control

    Private Inicializado As Boolean = False
    Dim Dcampo As New DataCampos
    ''' <summary>
    ''' Almacena la información del campo seleccionado en combo 'cbCampo'
    ''' </summary>
    ''' <remarks></remarks>
    Dim SelCampo As cbCampoValue

    Private Structure cbCampoValue
        Dim tipo As TipoCampo
        Dim nombre_combo As String
        Dim nombre_sql As String
        Dim origen_sql As String
    End Structure

    Public Enum TipoCampo
        combo = 1
        text = 2
        fecha = 3
    End Enum

    Public ReadOnly Property filtro As String
        Get
            Dim cadena_resultado As String = String.Empty
            If cbCampo.SelectedIndex = -1 Then
                Return String.Empty
            End If
            If cbFiltro.SelectedIndex = -1 Then
                Return String.Empty
            End If
            Dim fila_filtro As DataRowView = cbFiltro.SelectedItem
            Dim valor_filtro As Integer
            Dim valor_operador, valor1, valor2 As String
            Dim valor_doble As Boolean
            valor_filtro = fila_filtro("criterio")
            valor_operador = fila_filtro("operador")
            valor_doble = fila_filtro("doble")

            Select Case SelCampo.tipo
                Case TipoCampo.text
                    If String.IsNullOrEmpty(Valor1txt.Text) Then
                        valor1 = String.Empty
                    Else
                        valor1 = Valor1txt.Text
                    End If
                    If (Not valor_doble) Or (String.IsNullOrEmpty(Valor2txt.Text)) Then
                        valor2 = String.Empty
                    Else
                        valor2 = Valor2txt.Text
                    End If
                Case TipoCampo.fecha
                    If String.IsNullOrEmpty(Valor1dat.Text) Then
                        valor1 = String.Empty
                    Else
                        valor1 = Valor1dat.Value.ToString("yyyyMMdd")
                    End If
                    If (Not valor_doble) Or (String.IsNullOrEmpty(Valor2dat.Text)) Then
                        valor2 = String.Empty
                    Else
                        valor2 = Valor2dat.Value.ToString("yyyyMMdd")
                    End If
                Case TipoCampo.combo
                    If Valor1cbo.SelectedIndex = -1 Then
                        valor1 = String.Empty
                    Else
                        valor1 = Valor1cbo.SelectedValue
                    End If
                    'Combos nunca debe tener 2 valores
                    valor2 = String.Empty
                Case Else
                    valor1 = String.Empty
                    valor2 = String.Empty
            End Select
            If valor_doble Then
                'Es doble, por lo tanto requiero de ambos valores para operar
                If Not (String.IsNullOrEmpty(valor1) Or String.IsNullOrEmpty(valor2)) Then
                    valor_operador = valor_operador.Replace("val1", valor1).Replace("val2", valor2)
                    cadena_resultado = SelCampo.nombre_sql + valor_operador
                End If
            Else
                'No es doble, por lo ranto requiero solo 1 valor para operar
                If Not (String.IsNullOrEmpty(valor1)) Then
                    valor_operador = valor_operador.Replace("val1", valor1)
                    cadena_resultado = SelCampo.nombre_sql + valor_operador
                End If
            End If

            Return cadena_resultado
        End Get
    End Property
    Public ReadOnly Property val_combo As String
        Get
            Return SelCampo.nombre_combo
        End Get
    End Property

    Private Sub multicriterio_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dcampo.add_combo("Zona", "A.[Zona]", "SELECT COD_GL 'cod',NOMBRE 'txt' FROM AFN_ZONA WHERE ACTIVA=1 ORDER BY COD_GL")
        Dcampo.add_combo("Clase", "A.[Clase]", "SELECT cod 'cod',descrip 'txt' FROM AFN_CLASE WHERE mostrar_ing=1 ORDER BY cod ASC")
        Dcampo.add_fecha("Fecha Compra", "A.[Fecha Compra]")
        Dcampo.add_text("Código de Inventario Articulo", "A.[Código Inventario]")
        Dcampo.add_text("Descripcion Artículo", "A.[Descripcion]")
        Dcampo.add_text("Código de Lote Articulo", "A.[Lote Artículo]")
        Dcampo.add_text("Cantidad en el Lote Articulo", "A.[Cantidad]")

        'Para cargar la subzona debe existir otro multicriterio en el formulario padre, que ya tenga una zona seleccionada
        If Not IsNothing(Me.ParentForm) Then
            Dim FormPadre As Form
            FormPadre = Me.ParentForm
            Dim cont As Integer = 0
            For Each elemento As Control In FormPadre.Controls
                cont = cont + 1
                If TypeOf elemento Is multicriterio Then
                    Dim otro_multic As multicriterio
                    otro_multic = CType(elemento, multicriterio)
                    If Not IsNothing(otro_multic.val_combo) Then
                        If otro_multic.val_combo = "Zona" Then
                            Dim fil_zona As String = otro_multic.filtro
                            If Not String.IsNullOrEmpty(fil_zona) Then
                                Dcampo.add_combo("Subzona", "A.[Subzona]", "SELECT cod 'cod',rtrim(descrip) 'txt' FROM AFN_SUBZONA A WHERE activo=1 AND " + fil_zona + " ORDER BY descrip ASC")
                            End If
                        End If
                    End If

                End If
            Next
        End If


        cargar_combo_campo()
        Inicializado = True
    End Sub

    Private Sub cargar_combo_campo()
        cbCampo.DisplayMember = Dcampo.display
        cbCampo.ValueMember = Dcampo.value
        cbCampo.DataSource = Dcampo.tabla
        cbCampo.SelectedIndex = -1
        cbFiltro.Enabled = False
        mostrar_valor(TipoCampo.text, False)
        Valor1txt.Enabled = False
    End Sub

    Private Sub cbCampo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCampo.LostFocus
        If Not Inicializado Then
            Exit Sub
        End If
        If cbCampo.Text <> "" Then
            Dim existe As Boolean
            existe = Dcampo.existe_display(cbCampo.Text)
            If Not existe Then
                cargar_combo_campo()
            End If
        Else
            cargar_combo_campo()
        End If
    End Sub

    Private Sub cbCampo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCampo.SelectedIndexChanged
        If Not Inicializado Then
            Exit Sub
        End If
        If cbCampo.SelectedIndex > -1 Then
            Dim fila As DataRowView = cbCampo.SelectedItem
            'Asigno las variables del campo seleccionado a las variables de la clase
            SelCampo.nombre_sql = fila("codigo")
            SelCampo.tipo = DirectCast([Enum].Parse(GetType(TipoCampo), fila("tipo")), TipoCampo)
            SelCampo.origen_sql = fila("origen_sql")
            SelCampo.nombre_combo = fila("nombre")

            Dim Dfiltro As New DataFiltro
            Dfiltro.add("es igual que", 0, False, " = 'val1'")
            Dfiltro.add("empieza por", 2, False, " LIKE 'val1%'")
            Dfiltro.add("no es igual que", 1, False, " <> 'val1'")
            Dfiltro.add("contiene", 3, False, " LIKE '%val1%'")
            Dfiltro.add("es mayor que", 4, False, " > 'val1'")
            Dfiltro.add("es menor que", 5, False, " < 'val1'")
            If (SelCampo.tipo <> TipoCampo.combo) Then
                'Los campos de tipo combo, no pueden determinarse como entre
                Dfiltro.add("está entre", 6, True, " BETWEEN 'val1' AND 'val2'")
            End If
            cbFiltro.DisplayMember = Dfiltro.display
            cbFiltro.ValueMember = Dfiltro.value
            cbFiltro.DataSource = Dfiltro.tabla
            cbFiltro.Enabled = True
        Else
            SelCampo.nombre_sql = String.Empty
            SelCampo.tipo = 0
            SelCampo.origen_sql = String.Empty

            cbFiltro.DataSource = Nothing
            cbFiltro.Enabled = False
        End If
    End Sub

    Private Sub cbFiltro_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFiltro.SelectedIndexChanged
        If cbFiltro.SelectedIndex > -1 Then
            Dim doble As Boolean
            Dim fila_filtro As DataRowView = cbFiltro.SelectedItem
            doble = fila_filtro("doble")
            mostrar_valor(SelCampo.tipo, doble)
            If SelCampo.tipo = TipoCampo.combo Then
                Dim valores1 As DataTable
                Dim sql_dato As String
                sql_dato = SelCampo.origen_sql
                valores1 = maestro.ejecuta(sql_dato)
                With Valor1cbo
                    .DataSource = valores1
                    .SelectedIndex = -1
                End With
            End If
        Else
            mostrar_valor(TipoCampo.text, False)
            Valor1txt.Enabled = False
        End If
    End Sub

    Private Sub mostrar_valor(ByVal tipo_campo As TipoCampo, ByVal doble As Boolean)
        Select Case tipo_campo
            Case TipoCampo.text
                With Valor1txt
                    .Visible = True
                    .Enabled = True
                    .Text = String.Empty
                End With
                Valor1cbo.Visible = False
                Valor1dat.Visible = False
                With Valor2txt
                    .Visible = doble
                    .Enabled = doble
                    .Text = String.Empty
                End With
                Valor2dat.Visible = False
            Case TipoCampo.combo
                Valor1txt.Visible = False
                Valor1cbo.Visible = True
                Valor1dat.Visible = False
                Valor2txt.Visible = False
                Valor2dat.Visible = False
            Case TipoCampo.fecha
                Valor1txt.Visible = False
                Valor1cbo.Visible = False
                With Valor1dat
                    .Visible = True
                    .Enabled = True
                    .Value = Now
                End With
                Valor2txt.Visible = False
                With Valor2dat
                    .Visible = doble
                    .Enabled = doble
                    .Value = Now
                End With
        End Select

    End Sub

End Class

#Region "Clases para encapsular datos"
Public Class DataCampos
    Private Tcampo As New DataTable
    Public Sub New()
        Tcampo.Columns.Add("nombre", Type.GetType("System.String"))
        Tcampo.Columns.Add("tipo", Type.GetType("System.String"))
        Tcampo.Columns.Add("codigo", Type.GetType("System.String"))
        Tcampo.Columns.Add("origen_sql", Type.GetType("System.String"))
    End Sub
    Public Sub add_text(ByVal nombre As String, ByVal codigo_sql As String)
        Dim newfila As DataRow = Tcampo.NewRow
        newfila("nombre") = nombre
        newfila("tipo") = multicriterio.TipoCampo.text.ToString
        newfila("codigo") = codigo_sql
        newfila("origen_sql") = String.Empty
        Tcampo.Rows.Add(newfila)
    End Sub
    Public Sub add_combo(ByVal nombre As String, ByVal codigo_sql As String)
        Dim newfila As DataRow = Tcampo.NewRow
        newfila("nombre") = nombre
        newfila("tipo") = multicriterio.TipoCampo.combo.ToString
        newfila("codigo") = codigo_sql
        newfila("origen_sql") = String.Empty
        Tcampo.Rows.Add(newfila)

    End Sub
    Public Sub add_combo(ByVal nombre As String, ByVal codigo_sql As String, ByVal origen_sql As String)
        Dim newfila As DataRow = Tcampo.NewRow
        newfila("nombre") = nombre
        newfila("tipo") = multicriterio.TipoCampo.combo.ToString
        newfila("codigo") = codigo_sql
        newfila("origen_sql") = origen_sql
        Tcampo.Rows.Add(newfila)
    End Sub
    Public Sub add_fecha(ByVal nombre As String, ByVal codigo_sql As String)
        Dim newfila As DataRow = Tcampo.NewRow
        newfila("nombre") = nombre
        newfila("tipo") = multicriterio.TipoCampo.fecha.ToString
        newfila("codigo") = codigo_sql
        newfila("origen_sql") = String.Empty
        Tcampo.Rows.Add(newfila)
    End Sub
    Public ReadOnly Property tabla As DataTable
        Get
            Return Tcampo
        End Get
    End Property
    Public ReadOnly Property display As String
        Get
            Return "nombre"
        End Get
    End Property
    Public ReadOnly Property value As String
        Get
            Return "codigo"
        End Get
    End Property
    Public Function existe_display(ByVal mostrar As String) As Boolean
        Dim filas_buscadas() As DataRow
        filas_buscadas = Tcampo.Select(Me.display + "='" + mostrar + "'")
        If filas_buscadas.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class

Public Class DataFiltro
    Private tvalores As New DataTable
    Public Sub New()
        tvalores.Columns.Add("mostrar", Type.GetType("System.String"))
        tvalores.Columns.Add("criterio", Type.GetType("System.Int32"))
        tvalores.Columns.Add("doble", Type.GetType("System.Boolean"))
        tvalores.Columns.Add("operador", Type.GetType("System.String"))
    End Sub
    Public Sub add(ByVal mostar As String, ByVal criterio As Integer, ByVal doble As Boolean, ByVal operador As String)
        Dim newfila As DataRow = tvalores.NewRow
        newfila("mostrar") = mostar
        newfila("criterio") = criterio
        newfila("doble") = doble
        newfila("operador") = operador
        tvalores.Rows.Add(newfila)
    End Sub
    Public ReadOnly Property tabla As DataTable
        Get
            Return tvalores
        End Get
    End Property
    Public ReadOnly Property display As String
        Get
            Return "mostrar"
        End Get
    End Property
    Public ReadOnly Property value As String
        Get
            Return "criterio"
        End Get
    End Property
End Class

#End Region