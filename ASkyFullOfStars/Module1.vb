Module Module1

    Sub Main()
        Dim starmap As New starmap(New starmapBuilder(1))

        For Each star In starmap.stars
            For Each planet In star.planets
                Dim str As String = " " & planet.name & ", " & planet.type.ToString & " " & planet.government.ToString & " "
                consoleDotline(str.Length)
                Console.WriteLine(str)
                consoleDotline(str.Length)
                Console.WriteLine()

                For Each city In planet.cities
                    Console.WriteLine(city.ToString)
                Next
            Next
        Next
        Console.WriteLine()
        Console.WriteLine("Total Stars: " & starmap.stars.Count)
        Console.WriteLine("Total Planets: " & starmap.numberOfPlanets)
        Console.WriteLine("Total Cities: " & starmap.numberOfCities)
        Console.ReadLine()
    End Sub

    Private Sub consoleDotline(length As Integer)
        For n = 1 To length
            Console.Write("-")
        Next
        Console.Write(vbNewLine)
    End Sub

End Module
