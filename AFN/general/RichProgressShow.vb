Imports System.Threading
Imports System.ComponentModel
Imports System.Drawing

''' <summary>
''' Crea una barra de progreso mas compleja, que ademas permite definir diferentes partes de un proceso.
''' </summary>
''' <remarks></remarks>
Public Class ProgressShow
    Dim grupos As Integer
    Dim elementos() As Long
    Dim Total_maximo As Long
    Dim valor_actual As Double
    Dim partes() As Integer
    Dim Total_grupo As Integer

    ''' <summary>
    ''' Función base para comenzar a utilizar la barra de proceso optimizada, establece los valores necesarios para ejecutarse. Por defecto establece que todos los subproceso tienen igual duración
    ''' </summary>
    ''' <param name="subprocesos">Indica cuantos subproceso componen al proceso principal que representa la barra</param>
    ''' <param name="totalProceso">Indica cuantos pasos entre todos los subprocesos conforman el total, no usar para proceso con pasos dinamicos</param>
    ''' <remarks>En general no se recomienda especificar el parámetro opcional totalProceso, ya que cada subproceso suele tener su propia duración, solo es recomendable usar ese campo cuando todos los subproceso son iguales</remarks>
    Public Sub inicializar(ByVal subprocesos As Integer, Optional ByVal totalProceso As Long = 0)
        Total_grupo = 100
        Try
            Me.BringToFront()
            If subprocesos <= 0 Then
                Throw New ArgumentException("Debe inicializar 1 o mas procesos")
            End If
            If subprocesos > Total_grupo Then
                Throw New ArgumentException("Se han definido una cantidad mayor de subproceso de los permitidos")
            End If
            Me.Size = New System.Drawing.Size(243, 58)
            Me.Left = (Me.Parent.ClientSize.Width - Me.Width) / 2
            Me.Top = (Me.Parent.ClientSize.Height - Me.Height) / 2
            Me.Visible = True
            Dim i, Difer, Difer2 As Long
            If totalProceso = 0 Then
                totalProceso = subprocesos
            End If
            valor_actual = 0
            grupos = subprocesos
            Total_maximo = totalProceso
            ReDim partes(grupos - 1)
            ReDim elementos(grupos - 1)
            For i = 0 To grupos - 1
                partes(i) = Math.Round(Total_grupo / grupos, 0)
                elementos(i) = Math.Round(totalProceso / grupos, 0)
                If i = grupos - 1 Then
                    'asigno la diferencia al ultimo subproceso
                    Difer = Total_grupo - (Math.Round(Total_grupo / grupos, 0) * grupos)
                    Difer2 = totalProceso - (Math.Round(totalProceso / grupos, 0) * grupos)
                    partes(i) = partes(i) + Difer
                    elementos(i) = elementos(i) + Difer2
                End If
                System.Windows.Forms.Application.DoEvents()
            Next
            barra1.Maximum = Total_grupo
            barra1.Minimum = 0
            barra1.Value = 0
            porcent.Text = Format(barra1.Value / barra1.Maximum, "##0.00%")
            System.Windows.Forms.Application.DoEvents()
            Thread.Sleep(100)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "ERROR")
        End Try
    End Sub

    ''' <summary>
    ''' Cambia la proporción de la barra asignada a un subproceso, por defecto el remanente lo agrega o quita al último subproceso
    ''' </summary>
    ''' <param name="id_proceso_afec">Indica el índice del subproceso, el primer proceso tiene índice 0</param>
    ''' <param name="nueva_porcion">Indica la proporción que le corresponde de la barra, representa un porcentaje, pero solo puede usarse enteros</param>
    ''' <remarks></remarks>
    Public Overloads Sub cambiar_parte(ByVal id_proceso_afec As Integer, ByVal nueva_porcion As Integer)
        Try
            Dim old_parte, difer As Integer
            old_parte = partes(id_proceso_afec)
            difer = old_parte - nueva_porcion
            If (old_parte + partes(grupos - 1)) <= nueva_porcion Then
                Throw New ArgumentException("Parte de contrapartida no tiene espacio suficiente para recibir diferencia")
            End If
            partes(id_proceso_afec) = partes(id_proceso_afec) - difer
            partes(grupos - 1) = partes(grupos - 1) + difer
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "ERROR")
        End Try
    End Sub
    ''' <summary>
    ''' Cambia la proporción de la barra asignada a un subproceso, el remanente lo agrega o quita al subproceso id_proceso_cont
    ''' </summary>
    ''' <param name="id_proceso_afec">Indica el índice del subproceso, el primer proceso tiene índice 0</param>
    ''' <param name="nueva_porcion">Indica la proporción que le corresponde de la barra, representa un porcentaje, pero solo puede usarse enteros</param>
    ''' <param name="id_proceso_cont">Indica el índice del subproceso que recibirá la contrapartida que se produzca con el proceso principal</param>
    ''' <remarks></remarks>
    Public Overloads Sub cambiar_parte(ByVal id_proceso_afec As Integer, ByVal nueva_porcion As Integer, ByVal id_proceso_cont As Integer)
        Dim old_parte, difer As Integer
        Try
            old_parte = partes(id_proceso_afec)
            difer = old_parte - nueva_porcion
            If (old_parte + partes(id_proceso_cont)) >= nueva_porcion Then
                Throw New ArgumentException("Parte de contrapartida no tiene espacio suficiente para recibir diferencia")
            End If
            partes(id_proceso_afec) = partes(id_proceso_afec) - difer
            partes(id_proceso_cont) = partes(id_proceso_cont) + difer
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "ERROR")
        End Try

    End Sub

    ''' <summary>
    ''' Especifica cuantos elementos tiene el proceso, sin variar su proporción de la barra
    ''' </summary>
    ''' <param name="id_proceso">Indica el proceso que se verá afectado, el primer proceso tiene indice 0</param>
    ''' <param name="nuevo_max">Especifica cuantos elementos tendrá el proceso especificado</param>
    ''' <remarks></remarks>
    Public Sub definir_proceso(ByVal id_proceso As Integer, ByVal nuevo_max As Long)
        Dim difer As Long
        difer = nuevo_max - elementos(id_proceso)
        elementos(id_proceso) = nuevo_max
        Total_maximo = Total_maximo + difer
        dibujar_status()
    End Sub

    ''' <summary>
    ''' Incrementa en uno el elemento actual, para el proceso que se esté ejecutando
    ''' </summary>
    ''' <remarks>Se utiliza cuando hemos definido el proceso con una cantidad de elemento fijas o que se puede determinar en tiempo de diseño</remarks>
    Public Overloads Sub continua_proceso()
        valor_actual = valor_actual + 1
        dibujar_status()
    End Sub

    ''' <summary>
    ''' Incrementa en uno el elemento actual, al proceso que se especifique, sin excederse del máximo del mismo
    ''' </summary>
    ''' <param name="subproceso">Indica el proceso que se verá afectado, el primer proceso tiene indice 0</param>
    ''' <remarks>Se utiliza cuando se ha establecido el proceso como dinámico, es decir no se puede determinar en tiempo de diseño, como por ejemplo el espera por una consulta a una base de datos</remarks>
    Public Overloads Sub continua_proceso(ByVal subproceso As Integer)
        'modificacion de continua_proceso, utilizanda cuando se realiza un proceso de duración estimada, como por ejemplo una consulta a base de datos
        Dim i, Tbase As Long
        If subproceso < grupos Then
            Tbase = 0
            'determino cual es el valor maximo que debiera soportar este subproceso
            For i = 0 To subproceso
                Tbase = Tbase + elementos(i)
            Next
            If valor_actual + 1 >= Tbase Then
                'significa que ya ha alcanzado el valor maximo para este subproceso, por lo que no sigo aumentando
            Else
                valor_actual = valor_actual + 1
                dibujar_status()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Establece el contador de la barra en el mínimo del proceso indicado
    ''' </summary>
    ''' <param name="subproceso">Indica el proceso que se verá afectado, el primer proceso tiene indice 0</param>
    ''' <remarks>Solo debe utilizarse cuando el proceso anterior fue definido como dinámico, puesto que es posible que el proceso dinámico termine antes de lo esperado</remarks>
    Public Sub inicia_proceso(ByVal subproceso As Integer)
        'se agrega función para restablecer el valor del proceso, en caso de que un proceso de duración estimada finalice antes de lo esperado
        Dim i, Tbase As Long
        If subproceso < grupos Then
            For i = 0 To subproceso - 1
                Tbase = Tbase + elementos(i)
            Next
            valor_actual = Tbase
        End If
    End Sub

    ''' <summary>
    ''' Actuliza los elementos gráficos del control, para reflejar los valores que éste contenga
    ''' </summary>
    ''' <remarks>Debe utilizarse luego de haber hecho un cambio en algún valor, de modo que el efecto sea visible al usuario</remarks>
    Private Sub dibujar_status()
        Dim i, Tbase, MaxProce, MinProce As Long
        Dim avance, MaxPart, MinPart As Integer
        Dim valor_mostrar As Decimal
        'determino a que proceso pertenece el valor actual
        Tbase = 0
        avance = 0
        For i = 0 To grupos - 1
            If (elementos(i) + Tbase >= valor_actual And Tbase < valor_actual) Then
                'pertenece al grupo i
                MaxProce = elementos(i) + Tbase
                MinProce = Tbase
                MaxPart = partes(i) + avance
                MinPart = avance
            End If
            Tbase = Tbase + elementos(i)
            avance = avance + partes(i)
        Next
        If valor_actual = 0 Then
            valor_mostrar = 0
        Else
            valor_mostrar = (MaxPart - MinPart) / (MaxProce - MinProce) * (valor_actual - MinProce) + MinPart
        End If

        porcent.Text = Format(valor_mostrar / 100, "##0.00%")
        System.Windows.Forms.Application.DoEvents()
        barra1.Value = CInt(valor_mostrar)
        barra1.Refresh()

    End Sub

    ''' <summary>
    ''' Desaparece la barra de proceso de la pantalla
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub termina_proceso()
        Me.Visible = False
    End Sub
End Class
