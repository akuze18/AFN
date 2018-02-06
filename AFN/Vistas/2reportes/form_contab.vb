Imports System.IO
Public Class form_contab
    Dim Tperiodo As Vperiodo
    Dim colchon As DataTable        'generico para la clase, para ser utilizado en consulta por BGworker
    ''' <summary>
    ''' inicializo la conexion a la base de datos con el INI de configuracion 
    ''' </summary>
    ''' <remarks></remarks>
    Private base As New base_AFN

    'funciones del formulario
    Private Sub form_contab_Disposed(sender As Object, e As System.EventArgs) Handles Me.Disposed
        form_welcome.Show()
    End Sub
    Private Sub form_contab_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim Tperiodo As DataTable
        Tperiodo = base.lista_periodo()
        cb_fecha.DataSource = Tperiodo
        cb_fecha.ValueMember = Tperiodo.Columns(0).ColumnName
        cb_fecha.DisplayMember = Tperiodo.Columns(1).ColumnName
        cb_fecha.SelectedIndex = 0
    End Sub
    Private Function validar_formulario() As Boolean
        If cb_fecha.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar un periodo para mostrar", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cb_fecha.Focus()
            validar_formulario = False
            Exit Function
        End If
        If String.IsNullOrEmpty(Trim(Tubicacion.Text)) Then
            MessageBox.Show("Debe indicar una ubicación para guardar la salida", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tubicacion.Focus()
            validar_formulario = False
            Exit Function
        Else
            Dim ini_archivo As Integer
            Dim dir_save As String
            ini_archivo = Strings.InStrRev(Tubicacion.Text, "\")
            dir_save = Strings.Left(Tubicacion.Text, ini_archivo)
            If Not System.IO.Directory.Exists(dir_save) Then
                MessageBox.Show("Directorio ingresado no corresponde", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Tubicacion.Focus()
                validar_formulario = False
                Exit Function
            End If
        End If
        validar_formulario = True
    End Function

    'funciones de controles del formulario
    Private Sub btn_ubicar_Click(sender As System.Object, e As System.EventArgs) Handles btn_ubicar.Click
        Dim nombre_archivo As String
        dialogo.Title = "Elija una ubicación para guardar el archivo"
        dialogo.Filter = "Archivo de Texto|*.txt"
        Dim sel_per As DateTime = cb_fecha.SelectedValue
        nombre_archivo = "CONTABILIZAR" + sel_per.ToString("yyyyMM") + ".txt"
        dialogo.FileName = nombre_archivo
        dialogo.ShowDialog()
        If dialogo.FileName <> "" Then
            Tubicacion.Text = dialogo.FileName
        End If
    End Sub
    Private Sub btn_calcular_Click(sender As System.Object, e As System.EventArgs) Handles btn_calcular.Click
        If validar_formulario() Then
            Dim sel As DateTime = cb_fecha.SelectedValue
            Tperiodo = New Vperiodo(sel.Year, sel.Month)
            generar_contab()
        End If
    End Sub

    'funciones del proceso
    Private Sub generar_contab()
        bloquearW(Me)
        Dim dato_proceso As DataTable
        Dim estado As Boolean
        dato_proceso = calcular_AF_contab()
        estado = imprimir_AF_contab(dato_proceso, 1)
        If estado Then
            MessageBox.Show("Proceso terminado", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.None)
        End If
        desbloquearW(Me)
    End Sub
    Private Function calcular_AF_contab() As DataTable
        If Not IsNothing(colchon) Then
            colchon.Dispose()
        End If
        Dim Tini As Date
        Dim SegTrans, SegActual As Integer
        Tini = Now
        SegTrans = 0
        BGQuery.RunWorkerAsync()
        While BGQuery.IsBusy
            'Proceso principal espera a subproceso hasta que termine
            SegActual = DateDiff(DateInterval.Second, Tini, Now)
            If SegTrans < SegActual Then
                'avance.continua_proceso(0)
                SegTrans = SegActual
            End If
            Application.DoEvents()
        End While
        Return colchon
    End Function
    Private Function imprimir_AF_contab(ByVal valores As DataTable, ByVal inicio As Integer) As Boolean
        Dim dato_proceso As DataTable
        Dim Tlinea, nuevo As String
        Dim Archivo As String
        dato_proceso = valores
        Archivo = Tubicacion.Text
        Try
            Dim sw As New StreamWriter(Archivo, False, System.Text.Encoding.Unicode)
            Dim sTab As String = ControlChars.Tab
            sw.WriteLine("TIPO ASIENTO" + sTab + "REFERENCIA ASIENTO" + sTab + "CUENTA" + sTab + "DESCRIPCION" + sTab + "DIMENSIÓN DEPTO" + sTab + "DIMENSIÓN LUGAR" + sTab + "DIFERENCIA" + sTab + "FECHA")
            For Each fila As DataRow In dato_proceso.Rows
                Tlinea = ""
                For j = 0 To dato_proceso.Columns.Count - 1
                    'If Val(fila.Item(j)) <> 0 And dato_proceso.Columns(j).DataType.Name = "String" Then
                    '    nuevo = "'"
                    'Else
                    nuevo = String.Empty
                    'End If
                    nuevo = nuevo + Strings.Replace(CStr(fila.Item(j)), Chr(13), String.Empty)
                    Tlinea = Tlinea + nuevo + sTab
                    Application.DoEvents()
                Next
                Tlinea = Tlinea + Tperiodo.lastDB
                sw.WriteLine(Tlinea)
            Next
            sw.Close()
            Return True
        Catch EIO As IOException
            MessageBox.Show(EIO.Message, "Error con archivo", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Otro Error No Controlado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try
    End Function

    'ejecucion de consultas en segundo plano
    Private Sub BGQuery_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BGQuery.DoWork

        colchon = base.CONTABILIZAR_GP2013(Tperiodo)

    End Sub
End Class