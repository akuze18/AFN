'Fuente codigo general: http://www.vb-mundo.com/enviar-e-mail-desde-una-aplicacion-vb-net/
'Fuente error de autenticacion : https://stackoverflow.com/questions/34851484/how-to-send-an-email-in-net-according-to-new-security-policies
'Fuente usar validacion de 2 pasos: https://galleryserverpro.com/use-gmail-as-your-smtp-server-even-when-using-2-factor-authentication-2-step-verification/

Imports System.Net
Imports System.Net.Mail

Public Class sentMail    
    Private Sub sentMail_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        txtPara.Text = "aquispe@nipponchile.cl"
        txtAsunto.Text = "Prueba de envio de mensaje en .NET"
        txtMensaje.Text = "Este es un mensaje de prueba"
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim _Message As New System.Net.Mail.MailMessage()
        Dim _SMTP As New System.Net.Mail.SmtpClient

        'CONFIGURACIÓN DEL STMP
        _SMTP.Credentials = New System.Net.NetworkCredential("arielquispesepulveda@gmail.com", "uimwmrqsdzuryssz")
        _SMTP.Host = "smtp.gmail.com"
        _SMTP.Port = 587
        _SMTP.DeliveryMethod = SmtpDeliveryMethod.Network
        _SMTP.EnableSsl = True

        ' CONFIGURACION DEL MENSAJE
        _Message.[To].Add(Me.txtPara.Text.ToString) 'Cuenta de Correo al que se le quiere enviar el e-mail
        _Message.From = New System.Net.Mail.MailAddress("arielquispesepulveda@gmail.com", "Ariel Quispe", System.Text.Encoding.UTF8) 'Quien lo envía
        _Message.Subject = Me.txtAsunto.Text.ToString 'Sujeto del e-mail
        _Message.SubjectEncoding = System.Text.Encoding.UTF8 'Codificacion
        _Message.Body = Me.txtMensaje.Text.ToString 'contenido del mail
        _Message.BodyEncoding = System.Text.Encoding.UTF8
        _Message.Priority = System.Net.Mail.MailPriority.Normal
        _Message.IsBodyHtml = False

        'ENVIO
        Try
            _SMTP.Send(_Message)
            MessageBox.Show("Mensaje enviado correctamente", "Exito!", MessageBoxButtons.OK)
        Catch ex As System.Net.Mail.SmtpException
            MessageBox.Show(ex.ToString, "Error!", MessageBoxButtons.OK)
        End Try
    End Sub

    'Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
    '    Dim mailClient As System.Net.Mail.SmtpClient = New System.Net.Mail.SmtpClient("smtp.gmail.com", 587)
    '    Dim MyMailMessage As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage("arielquispesepulveda@gmail.cl", Me.txtPara.Text.ToString,
    '    Me.txtAsunto.Text.ToString, Me.txtMensaje.Text.ToString)
    '    MyMailMessage.IsBodyHtml = False

    '    ''Proper Authentication Details need to be passed when sending email from gmail
    '    Dim mailAuthentication As System.Net.NetworkCredential = New System.Net.NetworkCredential("arielquispesepulveda@gmail.cl", "micromante10?")

    '    mailClient.DeliveryMethod = SmtpDeliveryMethod.Network
    '    ''//mailClient.ServicePoint.Address;
    '    mailClient.EnableSsl = True
    '    mailClient.UseDefaultCredentials = False

    '    mailClient.Credentials = mailAuthentication
    '    Try
    '        mailClient.Send(MyMailMessage)
    '        Label1.Text = "OK"
    '    Catch exc As Exception
    '        Label1.Text = exc.Message
    '    End Try


    'End Sub

End Class