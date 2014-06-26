Module Module1
    Sub Main()
        Dim starmapBuilder As New starmapBuilder(3)
        Dim player As New player("Tubby")
        Dim starmap As New starmap(starmapBuilder)
        Dim capital As New capital(player, starmap)

        Console.WriteLine(starmap.ToString)
        Console.ReadLine()
    End Sub



    Public Function consoleDotline(length As Integer) As String
        Dim str As String = ""
        For n = 1 To length
            str &= "-"
        Next
        Return str
    End Function
End Module
