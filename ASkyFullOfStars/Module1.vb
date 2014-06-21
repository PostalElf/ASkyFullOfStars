Module Module1

    Sub Main()
        Console.WriteLine("1. Fetch")
        Console.WriteLine("2. Generate")
        Select Case Console.ReadLine()
            Case "1"
                Dim filefetch As New filefetch
                Dim starmap As starmap = filefetch.getStarmap
                displayStarmap(starmap)

            Case "2"
                generateStarmap()
        End Select
    End Sub

    Private Sub generateStarmap()
        Dim starmap As New starmap(New starmapBuilder(1))

        displayStarmap(starmap)

        Dim filefetch As New filefetch
        filefetch.writeStarmap(starmap)
    End Sub

    Private Sub displayStarmap(starmap As starmap)
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
