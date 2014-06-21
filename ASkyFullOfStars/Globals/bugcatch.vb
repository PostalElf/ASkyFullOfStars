NotInheritable Class bugcatch
    Shared Sub alert(sender As Object, Optional moreInfo As String = "")
        Dim errormsg As String = sender.GetType.FullName
        If moreInfo <> "" Then errormsg = errormsg & " (" & moreInfo & ")"
        Console.WriteLine("Error " & errormsg, MsgBoxStyle.Critical)
        Console.ReadKey()
    End Sub
End Class
