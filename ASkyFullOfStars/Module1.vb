Module Module1
    Private Const lastStarname As String = "Ananke"
    Private Const lastStarMaxPlanet As Integer = 3
    Private Const lastStarMaxCity As Integer = 3
    Private Const dubbStarname As String = "Lelantos"

    Sub Main()
        Console.WriteLine("1. Starmap")
        Console.WriteLine("2. Capital")
        Console.WriteLine("3. Travel")
        Select Case Console.ReadLine
            Case "1" : testStarmap()
            Case "2" : testCapital()
            Case "3" : testTravel()
        End Select
    End Sub

    Private Sub testCapital()
        Console.WriteLine("1. Fetch")
        Console.WriteLine("2. Generate")
        Select Case Console.ReadLine
            Case "1"
                Dim filefetch As New filefetch
                Dim starmap As starmap = filefetch.getStarmap
                Dim capital As capital = filefetch.getCapital(starmap)
                filefetch.Dispose()

                Console.WriteLine(capital.ToString)
                Console.ReadLine()

            Case "2"
                Dim filefetch As New filefetch
                Dim starmap As starmap = filefetch.getStarmap
                filefetch.Dispose()
                generateCapital(starmap)
        End Select
    End Sub
    Private Sub generateCapital(_starmap As starmap)
        Dim capital As New capital(_starmap)
        capital.assets.Add(New asset("Imperial Bonds", capital, 999, 3))

        'good test
        Dim sourceCity As city = _starmap.getCity(lastStarname, lastStarMaxPlanet, 1)
        sourceCity.supply.Add(eGood.ACompounds)
        Dim destCity As city = _starmap.getCity(lastStarname, lastStarMaxPlanet, lastStarMaxCity)
        destCity.demand.Add(eGood.ACompounds)
        capital.goods.Add(New good(eGood.ACompounds, sourceCity, destCity))


        'asset test
        destCity.assets.add(New asset("Ouroboros Construction", destCity, 3, -0.3))


        'asset tick/TTL test
        For n = 1 To 5
            Console.Clear()
            Console.WriteLine("Turn " & n)
            Console.WriteLine(capital.ToString)
            Console.WriteLine()
            Console.WriteLine(sourceCity.ToString)
            Console.WriteLine()
            Console.WriteLine(destCity.ToString)
            Console.ReadLine()

            capital.starmap.tick()
        Next


        'save changes
        Dim filefetch As New filefetch
        filefetch.writeStarmap(_starmap)
        filefetch.writeCapital(capital)
        filefetch.Dispose()
    End Sub


    Private Sub testStarmap()
        Console.WriteLine("1. Fetch")
        Console.WriteLine("2. Generate")
        Select Case Console.ReadLine()
            Case "1"
                Dim filefetch As New filefetch
                Dim starmap As starmap = filefetch.getStarmap
                filefetch.Dispose()
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
        filefetch.Dispose()
    End Sub
    Private Sub displayStarmap(starmap As starmap)
        For Each star In starmap.stars
            Console.WriteLine(vbTab & vbTab & star.name & " (" & star.starXY.ToString & ")")
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


    Private Sub testTravel()
        Dim filefetch As New filefetch
        Dim starmap As starmap = filefetch.getStarmap
        Dim capital As capital = filefetch.getCapital(starmap)
        filefetch.Dispose()

        Dim dest As planet = starmap.getPlanet(lastStarname, 1)
        Dim origin As planet = starmap.getPlanet(dubbStarname, 2)
        capital.orbiting = origin
        Console.WriteLine("From " & origin.name & " (" & origin.star.starXY.ToString & ") to " & dest.name & " (" & dest.star.starXY.ToString & ")")
        Console.WriteLine("Travel Distance: " & origin.star.starXY.distanceTo(dest.star.starXY))
        Console.WriteLine("Travel Speed: " & capital.travelSpeed)

        capital.travelTo(origin, dest)

        For n = 1 To 10
            Console.WriteLine("Turns Remaining: " & capital.travelTTL)
            Console.WriteLine("Current Location: " & capital.orbiting.name)
            capital.tick()
        Next

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
