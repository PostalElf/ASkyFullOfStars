Module Module1

    Sub Main()
        testCapital()
    End Sub

    Private Sub testCapital()
        Console.WriteLine("1. Fetch")
        Console.WriteLine("2. Generate")
        Select Case Console.ReadLine
            Case "1"

            Case "2"
                Dim filefetch As New filefetch
                Dim starmap As starmap = filefetch.getStarmap
                generateCapital(starmap)
        End Select
    End Sub
    Private Sub generateCapital(_starmap As starmap)
        Dim capital As New capital(_starmap)
        capital.assets.Add(New asset("Imperial Bonds", capital, 999, 3))

        'good test
        Dim sourceCity As city = _starmap.getCity("Rhea", 7, 3)
        sourceCity.supply.Add(eGood.ACompounds)
        Dim destCity As city = _starmap.getCity("Rhea", 5, 5)
        destCity.demand.Add(eGood.ACompounds)
        capital.goods.Add(New good(eGood.ACompounds, sourceCity, destCity))


        'asset test
        destCity.assets.Add(New asset("Ouroboros Construction", destCity, 3, -0.3))

        For n = 1 To 5
            Console.Clear()
            Console.WriteLine("Turn " & n)
            Console.WriteLine(capital.ToString)
            Console.WriteLine()
            Console.WriteLine(destCity.ToString)
            Console.ReadLine()

            destCity.tick()
        Next
    End Sub


    Private Sub testStarmap()
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
                Console.WriteLine(consoleDotline(str.Length))
                Console.WriteLine(str)
                Console.WriteLine(consoleDotline(str.Length))
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


    Private Function consoleDotline(length As Integer) As String
        Dim str As String = ""
        For n = 1 To length
            str &= "-"
        Next
        Return str
    End Function
End Module
