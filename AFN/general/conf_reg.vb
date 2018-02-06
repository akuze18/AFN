Module conf_reg

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    'Función que retorna la cadena con el resultado
    '************************************************
    Public Function getSeparadorList() As String

        'Dim oldDecimalSeparator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        'Dim oldGroupSeparator As String = Application.CurrentCulture.NumberFormat.NumberGroupSeparator()
        Dim oldListSeparator As String = Application.CurrentCulture.TextInfo.ListSeparator

        'Dim o1, o2, o3, o4, o5, o6 As String
        'o1 = Application.CurrentCulture.NumberFormat.NumberGroupSeparator()
        'o2 = System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        'o3 = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator

        'o4 = Application.CurrentCulture.TextInfo.ListSeparator
        'o5 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator
        'o6 = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator

        Return oldListSeparator
    End Function

    Public Function getSeparadorDecim() As String

        Dim oldDecimalSeparator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        'Dim oldGroupSeparator As String = Application.CurrentCulture.NumberFormat.NumberGroupSeparator()
        'Dim oldListSeparator As String = Application.CurrentCulture.TextInfo.ListSeparator

        'Dim o1, o2, o3, o4, o5, o6 As String
        'o1 = Application.CurrentCulture.NumberFormat.NumberGroupSeparator()
        'o2 = System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        'o3 = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator

        'o4 = Application.CurrentCulture.TextInfo.ListSeparator
        'o5 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator
        'o6 = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator

        Return oldDecimalSeparator
    End Function

    Public Function getSeparadorMil() As String

        'Dim oldDecimalSeparator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        Dim oldGroupSeparator As String = Application.CurrentCulture.NumberFormat.NumberGroupSeparator()
        'Dim oldListSeparator As String = Application.CurrentCulture.TextInfo.ListSeparator

        'Dim o1, o2, o3, o4, o5, o6 As String
        'o1 = Application.CurrentCulture.NumberFormat.NumberGroupSeparator()
        'o2 = System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        'o3 = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator

        'o4 = Application.CurrentCulture.TextInfo.ListSeparator
        'o5 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator
        'o6 = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator

        Return oldGroupSeparator
    End Function

    '    'cambio la configuracion del separador de listas
    '    Call SetLocaleInfo(GetSystemDefaultLCID(), LOCALE_SLIST, ";")

End Module
