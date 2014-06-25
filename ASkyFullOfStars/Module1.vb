Module Module1
    Sub Main()
        Console.WriteLine("1. Tick")
        Console.WriteLine("2. Generate Starmap")
        Console.WriteLine("3. Travel")
        Console.WriteLine("4. Battle")
        Select Case Console.ReadLine
            Case "1" : testTick()
            Case "2" : generateStarmap()
            Case "3" : testTravel()
            Case "4" : testBattle()
        End Select
    End Sub


    Private Sub testTick()
        Dim filefetch As New filefetch
        Dim player As player = filefetch.getPlayer
        Dim starmap As starmap = filefetch.getStarmap(player)
        Dim capital As capital = filefetch.getCapital(player, starmap)

        Console.Write("How many turns? ")
        Dim n As Integer = Console.ReadLine
        Dim firstCity As city = starmap.stars(0).planets(0).cities(0)
        Dim nextCity As city = starmap.stars(0).planets(1).cities(0)

        For count = 1 To n
            Console.Clear()
            Console.WriteLine("Tick " & count)
            Console.WriteLine(vbSpace & "Total Income: " & player.totalIncome(starmap, capital))
            Console.WriteLine(capital.ToString)
            Console.WriteLine(firstCity.ToString(player))
            Console.WriteLine(nextCity.ToString(player))

            starmap.tick()

            Console.ReadLine()
        Next

        filefetch.writeCapital(capital)
        filefetch.writeStarmap(starmap)
        filefetch.Dispose()
    End Sub
    Private Sub displayTick()

    End Sub


    Private Sub generateStarmap()
        Dim player As New player
        Dim starmap As New starmap(New starmapBuilder(1))
        Dim capital As capital = generateCapital(player, starmap)


        'asset test
        Dim currentCity As city = starmap.stars(0).planets(0).cities(0)
        currentCity.assets.add(New productionAsset("Gravy Train", currentCity, New player, 999, -0.1, eGood.EFoods, eGood.CMaterials))
        currentCity.assets.add(New investmentAsset("Chewy Farm", currentCity, New player, 3, -0.1, _
                                New investmentAsset("Chewed-Up Farm", currentCity, New player, 10, 0.2, Nothing)))


        'show starmap
        displayStarmap(player, starmap)


        'save
        Dim filefetch As New filefetch
        filefetch.writeStarmap(starmap)
        filefetch.writeCapital(capital)
        filefetch.Dispose()
    End Sub
    Private Sub displayStarmap(ByRef player As player, starmap As starmap)
        For Each star In starmap.stars
            Console.WriteLine(vbTab & vbTab & star.name & " (" & star.starXY.ToString & ")")
            For Each planet In star.planets
                Dim str As String = " " & planet.name & ", " & planet.type.ToString & " " & planet.government.ToString & " "
                Console.WriteLine(consoleDotline(str.Length))
                Console.WriteLine(str)
                Console.WriteLine(consoleDotline(str.Length))
                Console.WriteLine()

                For Each city In planet.cities
                    Console.WriteLine(city.ToString(player))
                Next
            Next
        Next
        Console.WriteLine()
        Console.WriteLine("Total Stars: " & starmap.stars.Count)
        Console.WriteLine("Total Planets: " & starmap.numberOfPlanets)
        Console.WriteLine("Total Cities: " & starmap.numberOfCities)
        Console.ReadLine()
    End Sub


    Private Function generateCapital(player As player, starmap As starmap) As capital
        Dim capital As New capital(player, starmap)
        capital.assets.add(New asset("Imperial Bonds", eAsset.Investment, capital, player, 999, 3))

        'good test
        Dim sourceCity As city = starmap.stars(0).planets(0).cities(0)
        sourceCity.supply.Add(eGood.ACompounds)
        Dim destCity As city = starmap.stars(0).planets(1).cities(0)
        destCity.demand.Add(eGood.ACompounds)
        capital.goods.Add(New good(eGood.ACompounds, sourceCity, destCity))


        'asset test
        destCity.assets.add(New infrastructureAsset("Ouroboros Construction", destCity, player, 3, -0.3, _
                            New infrastructureAsset("Ouroboros", destCity, player, 999, -0.1)))

        Return capital
    End Function


    Private Sub testTravel()
        Dim filefetch As New filefetch
        Dim player As player = filefetch.getPlayer()
        Dim starmap As starmap = filefetch.getStarmap(player)
        Dim capital As capital = filefetch.getCapital(player, starmap)
        filefetch.Dispose()

        Dim dest As planet = starmap.stars(0).planets(0)
        Dim origin As planet = starmap.stars(0).planets(1)
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


    Private Sub testBattle()
        Dim filefetch As New filefetch
        Dim player As player = filefetch.getPlayer
        Dim starmap As starmap = filefetch.getStarmap(player)
        Dim capital As capital = filefetch.getCapital(player, starmap)
        filefetch.Dispose()

        Dim army As New army(player, capital)
        With army
            .units.Add(New unit(eUnit.Conscript, 100))
            .units.Add(New unit(eUnit.Soldier, 20))
            .addAsset(New militaryAsset("Basic Training", Nothing, player, 999, -0.1, eUnit.Conscript, 0.1))
        End With
        Console.WriteLine(army.ToString)
        Console.WriteLine("Total Power: " & army.totalPower)
        Console.ReadLine()

        While army.units.Count > 0
            army.takeDamage(120)
            Console.WriteLine(army.ToString)
            Console.WriteLine("Total Power: " & army.totalPower)
            Console.ReadLine()
        End While
    End Sub

    Private Function consoleDotline(length As Integer) As String
        Dim str As String = ""
        For n = 1 To length
            str &= "-"
        Next
        Return str
    End Function
End Module
