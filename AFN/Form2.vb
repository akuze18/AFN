Imports CrystalDecisions.CrystalReports.Engine

Public Class Form2

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'Dim periodo1, periodo2 As Vperiodo
        'periodo1 = New Vperiodo(ComboBox1.SelectedValue.Year, ComboBox1.SelectedValue.Month)
        'periodo2 = New Vperiodo(ComboBox2.SelectedValue.Year, ComboBox2.SelectedValue.Month)
        'MessageBox.Show("La diferencia entre los periodos es: " + (periodo1 - periodo2).ToString)

        

    End Sub

    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Dim base As New base_AFN
        'Dim origen As DataTable
        'origen = base.lista_periodo
        'With ComboBox1
        '    .DataSource = origen
        '    .ValueMember = origen.Columns(0).ColumnName
        '    .DisplayMember = origen.Columns(1).ColumnName
        'End With
        'origen = base.lista_periodo
        'With ComboBox2
        '    .DataSource = origen
        '    .ValueMember = origen.Columns(0).ColumnName
        '    .DisplayMember = origen.Columns(1).ColumnName
        'End With
        Me.WindowState = FormWindowState.Maximized

        Dim rpt As ReportDocument = New ReportDocument()

        rpt.Load("D:\Produccion.rpt")
        rpt.SetDatabaseLogon("sa", "NHfc1002")
        rpt.SetParameterValue(0, "20160101")
        rpt.SetParameterValue(1, "20160131")

        CrystalReportViewer1.ReportSource = rpt
        CrystalReportViewer1.Show()
        CrystalReportViewer1.ShowGroupTree()
        CrystalReportViewer1.Refresh()

    End Sub
End Class