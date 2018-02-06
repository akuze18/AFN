Imports System.Data
Imports System.Data.SqlClient

Public Class coneccion
    Private sevrDB, actDB, userDB, passDB As String
    Private cnn As SqlConnection
    Private Sda As SqlDataAdapter
    Private DataRead As SqlDataReader

    Public Enum disponible
        ok = 1
        no = 2
        ocupado = 3
    End Enum

    Public Sub New(Optional ByVal BaseDato As String = "", Optional ByVal Server As String = "", Optional ByVal usuario As String = "", Optional ByVal passw As String = "")
        If Server = "" Then
            sevrDB = "SANTIAGO"
        Else
            sevrDB = Server
        End If
        If BaseDato = "" Then
            actDB = "NIPPO"
        Else
            actDB = BaseDato
        End If
        If usuario = "" Then
            userDB = "sa"
        Else
            userDB = usuario
        End If
        If passw = "" Then
            passDB = "nippon2006"
        Else
            passDB = passw
        End If
    End Sub

    Public Function conectar(Optional ByVal BaseDato As String = "", Optional ByVal Server As String = "", Optional ByVal usuario As String = "", Optional ByVal passw As String = "") As Boolean
        Try
            If Server <> "" Then
                sevrDB = Server
            End If
            If BaseDato <> "" Then
                actDB = BaseDato
            End If
            If usuario <> "" Then
                userDB = usuario
            End If
            If passw <> "" Then
                passDB = passw
            End If
            Dim sCnn, inicio As String
            sCnn = "data source = " + sevrDB + "; initial catalog =" + actDB + "; user id = " + userDB + "; password = " + passDB
            If checkConect() = disponible.no Then
                cnn = New SqlConnection(sCnn)
                cnn.Open()
                inicio = "SET LANGUAGE SPANISH" + Chr(13) + "SET DATEFORMAT MDY"
                Dim config As New SqlCommand(inicio, cnn)
                config.ExecuteNonQuery()
                Return True
            Else
                MessageBox.Show("Conexión ya está abierta", "Operación Inválida", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End If
        Catch Exsql As SqlException
            MessageBox.Show(Exsql.Message, Exsql.Number.ToString, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Catch inOpe As InvalidOperationException
            MessageBox.Show(inOpe.Message, "Operación Inválida", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error al conectarse", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return False
    End Function

    Public Function checkConect() As disponible
        Dim estado As disponible
        If IsNothing(cnn) Then
            Return disponible.no
        End If
        Select Case cnn.State
            Case ConnectionState.Closed
                estado = disponible.no
            Case ConnectionState.Open
                estado = disponible.ok
            Case Else
                estado = disponible.ocupado
        End Select
        Return estado
    End Function

    Public Function ejecuta(ByVal sql As String) As DataTable
        Try
            
            Dim datt As New DataTable
            Sda = New SqlDataAdapter(sql, cnn)
            Sda.SelectCommand.CommandTimeout = 0
            Sda.Fill(datt)
            Return datt
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Critical, "Error " + sqlex.Number.ToString)
            Me.cerrar()
            Return Nothing
        Catch ioEx As InvalidOperationException
            MessageBox.Show("Operación invalida, revise la conexion", "Error de ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.cerrar()
            Return Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error " + ex.Source)
            Me.cerrar()
            Return Nothing
        End Try
    End Function

    Public Function execute(ByVal sql As String) As DataTable()
        Try
            'Dim BaseD As DataTableReader
            Dim seguir As Boolean
            Dim i As Integer
            i = 0
            Dim CMD As New SqlCommand(sql, cnn)
            Dim datt(i) As DataTable

            'datt(i) = New DataTable
            CMD.CommandTimeout = 120
            DataRead = CMD.ExecuteReader
            Do While DataRead.HasRows
                'dTabla.Load(DataRead)      'no funcion correctamente, ya que aplica las restricciones de la base de datos
                Dim dTabla As New DataTable
                For j = 0 To DataRead.FieldCount - 1
                    dTabla.Columns.Add(DataRead.GetName(j), DataRead.GetProviderSpecificFieldType(j))
                Next
                'MessageBox.Show(DataRead.GetName(0) & vbTab & DataRead.GetName(1) & vbTab & DataRead.GetName(1))
                Do While DataRead.Read()

                    Dim dRow As DataRow = dTabla.NewRow
                    For j = 0 To DataRead.FieldCount - 1
                        dRow(j) = DataRead(j)
                    Next
                    dTabla.Rows.Add(dRow)
                    'MessageBox.Show(vbTab & DataRead(0).ToString & vbTab & DataRead(1).ToString)
                Loop
                datt(i) = dTabla
                seguir = DataRead.NextResult
                If seguir Then
                    i = i + 1
                    ReDim Preserve datt(i)
                    'datt(i) = New DataTable
                End If
            Loop
            Return datt
        Catch sqlex As SqlException
            MsgBox(sqlex.Message, MsgBoxStyle.Critical, "Error " + sqlex.Number.ToString)
            Me.cerrar()
            Return Nothing
        Catch ioEx As InvalidOperationException
            MessageBox.Show("Consulta ingresada no es valida", "Error de ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.cerrar()
            Return Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error " + ex.Source)
            Me.cerrar()
            Return Nothing
        End Try
    End Function

    Public Sub cerrar()
        If checkConect() = disponible.ok Then
            cnn.Close()
        End If
    End Sub

    Public Function Dval(ByVal campo As String, ByVal dominio As String, ByVal criterio As String) As String
        Dim sql_armada As String
        Dim datt As New DataTable

        If criterio = "" Then
            sql_armada = "SELECT TOP 1 " + campo + " FROM " + dominio
        Else
            sql_armada = "SELECT TOP 1 " + campo + " FROM " + dominio + " WHERE " + criterio + ""
        End If
        datt = ejecuta(sql_armada)
        Dim filt As DataRow = datt.Rows(0)
        Dval = filt(0).ToString
    End Function

    Public Function Dcount(ByVal campo As String, ByVal dominio As String, ByVal criterio As String) As Integer
        Dim sql_armada As String
        Dim datt As New DataTable

        If criterio = "" Then
            sql_armada = "SELECT count(" + campo + ") FROM " + dominio
        Else
            sql_armada = "SELECT count(" + campo + ") FROM " + dominio + " WHERE " + criterio + ""
        End If
        datt = ejecuta(sql_armada)
        Dim filt As DataRow = datt.Rows(0)
        Return filt(0)
    End Function

End Class
