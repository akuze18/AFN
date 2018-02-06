Public Class form_CM
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private maestro As New master_control

    Private Sub form_CM_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub

    Private Sub form_CM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Text = "Periodo"
        CheckOpen.Text = "Abierto"
        Texistia.Text = ""
        btn_guardar.Text = "Guardar"
        Me.Controls.Add(New control_cm(0))
        CheckOpen.Enabled = False
        Texistia.Visible = False
        Dim colchon, val_corr As New DataTable
        Dim sql_abierto As String
        sql_abierto = "SELECT DISTINCT YEAR1 'P1', PERIODID 'P2', 1 'STATU' FROM SY40100 WHERE YEAR1>2011 and closed=0 and series=2 and periodid<>0 UNION ALL SELECT DISTINCT left(periodo,4)'P1',right(periodo,2)'P2', 2 'STATU' FROM AFN_CORR_MON WHERE periodo NOT in(SELECT DISTINCT cast(YEAR1*100+PERIODID as varchar) FROM SY40100 WHERE closed=0 and series=2 and periodid<>0) ORDER BY 1 DESC,2 DESC"
        colchon = maestro.ejecuta(sql_abierto)
        val_corr.Columns.Add("mostrar", Type.GetType("System.String"))
        val_corr.Columns.Add("valor", Type.GetType("System.String"))
        For Each fila As DataRow In colchon.Rows
            Dim newfila As DataRow = val_corr.NewRow
            newfila("mostrar") = fila("P1").ToString + " " + UCase(DateSerial(Now.Year, fila("P2"), 1).ToString("MMMM"))
            newfila("valor") = fila("STATU") * 100 + fila("P2")
            val_corr.Rows.Add(newfila)
            Application.DoEvents()
        Next
        cboPeriodo.DisplayMember = "mostrar"
        cboPeriodo.ValueMember = "valor"
        cboPeriodo.DataSource = val_corr
        cboPeriodo.SelectedIndex = -1
    End Sub

    Private Sub cboPeriodo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPeriodo.SelectedIndexChanged
        Dim i, j As Long
        Dim Tperiodo As String

        If cboPeriodo.SelectedIndex <> -1 Then
            'k = cboPeriodo.SelectedIndex
            'txtP(0) = Empty
            j = CLng(cboPeriodo.SelectedValue / 100)
            If j = 1 Then
                CheckOpen.Checked = True
                btn_guardar.Visible = True
                btn_guardar.Enabled = True
            Else
                CheckOpen.Checked = False
                btn_guardar.Visible = False
                btn_guardar.Enabled = False
            End If
            j = cboPeriodo.SelectedValue Mod 100
            'descargo todos los control_cm existentes
            Dim inicio As Integer = 0
            Dim Celiminar(inicio) As Control
            For Each elemento As Control In Me.Controls
                If TypeOf elemento Is control_cm Then
                    Celiminar(inicio) = elemento
                    inicio = inicio + 1
                    ReDim Preserve Celiminar(inicio)
                End If
            Next
            For i = 0 To Celiminar.GetUpperBound(0) - 1
                Me.Controls.Remove(Celiminar(i))
            Next
            btn_guardar.TabIndex = Me.Controls.Count - 1
            For i = 0 To j
                Dim Pcontrol As New control_cm(i)
                Dim fila, columna As Integer
                fila = i Mod 5
                columna = Math.Floor(i / 5)
                Me.Controls.Add(Pcontrol)
                Pcontrol.Location = New Point(Texistia.Location.X + columna * Pcontrol.Width, Texistia.Location.Y + fila * Pcontrol.Height)
                Pcontrol.TabIndex = btn_guardar.TabIndex
                btn_guardar.TabIndex = btn_guardar.TabIndex + 1
            Next
            'verifico si ya esta ingresado el periodo
            Dim colchon As DataTable
            Dim sql_verificar As String
            Tperiodo = Strings.Left(cboPeriodo.Text, 4) + Format(j, "00")
            sql_verificar = "SELECT periodo,aplica,monto FROM AFN_CORR_MON WHERE periodo='" + Tperiodo + "' ORDER BY aplica"
            colchon = maestro.ejecuta(sql_verificar)
            If colchon.Rows.Count <> 0 Then
                Texistia.Checked = True
                btn_guardar.Text = "Modificar"
                For Each Trow As DataRow In colchon.Rows
                    For Each elemento As Control In Me.Controls
                        If TypeOf elemento Is control_cm Then
                            Dim Tcm As control_cm = CType(elemento, control_cm)
                            If Trow("aplica") = Tcm.Tag Then
                                Tcm.valor.Text = Trow("monto")
                            End If
                        End If
                        Application.DoEvents()
                    Next
                Next
            Else
                Texistia.Checked = False
                btn_guardar.Text = "Guardar"
            End If
            colchon = Nothing
        Else
            'descargo todos los control_cm existentes
            Dim inicio As Integer = 0
            Dim Celiminar(inicio) As Control
            For Each elemento As Control In Me.Controls
                If TypeOf elemento Is control_cm Then
                    Celiminar(inicio) = elemento
                    inicio = inicio + 1
                    ReDim Preserve Celiminar(inicio)
                End If
            Next
            For i = 0 To Celiminar.GetUpperBound(0) - 1
                Me.Controls.Remove(Celiminar(i))
            Next
            btn_guardar.TabIndex = Me.Controls.Count - 1
            Dim nuevo As New control_cm(0)
            Me.Controls.Add(nuevo)
            nuevo.Location = Texistia.Location
            nuevo.TabIndex = btn_guardar.TabIndex
            btn_guardar.TabIndex = btn_guardar.TabIndex + 1
        End If
    End Sub

    Private Sub btn_guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_guardar.Click
        'validar campos
        If cboPeriodo.SelectedIndex = -1 Or cboPeriodo.Text = String.Empty Then
            Mensaje_IT("Debe indicar el periodo a aplicar corrección monetaria")
            cboPeriodo.Focus()
            Exit Sub
        End If
        For Each elemento As Control In Me.Controls
            If TypeOf elemento Is control_cm Then
                Dim Pcontrol As control_cm = elemento
                If String.IsNullOrEmpty(Pcontrol.val_actual) Then
                    Mensaje_IT("Debe indicar un valor para P" + Pcontrol.Tag.ToString)
                    Pcontrol.Focus()
                    Exit Sub
                End If
                If Not IsNumeric(Pcontrol.val_actual) Then
                    Mensaje_IT("El valor ingresado para P" + Pcontrol.Tag.ToString + " debe ser un número")
                    Pcontrol.Focus()
                    Exit Sub
                End If
            End If
        Next
        'fin valida campos
        'recolectar campos
        Dim Taño, Tmes As Integer
        Dim Tperiodo As String
        Dim Verif As Boolean
        'j = cboPeriodo.ListIndex
        Taño = CInt(Strings.Left(cboPeriodo.Text, 4))
        Tmes = cboPeriodo.SelectedValue Mod 100
        Tperiodo = Format(Taño, "0000") + Format(Tmes, "00")
        Verif = Texistia.Checked
        'fin recolectar campos
        'ingresamos valores en la base
        Dim colchon As DataTable
        Dim sql_in As String
        For Each elemento As Control In Me.Controls
            If TypeOf elemento Is control_cm Then
                Dim Pcontrol As control_cm = elemento
                If Verif Then
                    sql_in = "UPDATE AFN_CORR_MON SET monto=" + Format(CDbl(Pcontrol.val_actual), "0.0") + " WHERE periodo='" + Tperiodo + "' and aplica=" + Pcontrol.Tag.ToString
                Else
                    sql_in = "INSERT INTO AFN_CORR_MON(periodo,aplica,monto) VALUES('" + Tperiodo + "'," + Pcontrol.Tag.ToString + "," + Format(CDbl(Pcontrol.val_actual), "0.0") + ")"
                End If
                colchon = maestro.ejecuta(sql_in)
            End If
        Next
        Texistia.Checked = True
        btn_guardar.Text = "Modificar"
        Mensaje_Inf("Corrección Monetaria ha sido ingresada correctamente")
    End Sub
End Class