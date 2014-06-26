Module Module1
    Sub Main()
        Dim starmapBuilder As New starmapBuilder(3)
        Dim player As New player("Tubby")
        Dim starmap As New starmap(starmapBuilder)
        Dim capital As New capital(player, starmap)

        Dim firstCity As city = starmap.stars(0).planets(0).cities(0)
        Dim lastStarIndex As Integer = starmap.stars.Count - 1
        Dim lastPlanetIndex As Integer = starmap.stars(lastStarIndex).planets.Count - 1
        Dim lastCityIndex As Integer = starmap.stars(lastStarIndex).planets(lastPlanetIndex).cities.Count - 1
        Dim lastCity As city = starmap.stars(lastStarIndex).planets(lastPlanetIndex).cities(lastCityIndex)

        Dim activeRship As relationship = lastCity.getRelationship(player)
        activeRship.assets.add(New infrastructureAsset("Savvy Investments", activeRship.location, player, 10, 0.1))

        Dim enemyPlayer As New player("Johnson")
        Dim enemyRship As relationship = lastCity.getRelationship(enemyPlayer)
        enemyRship.assets.add(New infrastructureAsset("Hostile Takeover", enemyRship.location, enemyPlayer, 10, 0.1))

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
