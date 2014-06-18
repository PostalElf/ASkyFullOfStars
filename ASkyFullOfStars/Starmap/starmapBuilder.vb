Imports System.IO


Public Class starmapBuilder
    Private Property size As Integer = 0
    Private Const sizeMultiplier As Integer = 3
    Public Function newStarmap(_size As Integer) As starmap
        'size governs how many times the planetTypeList must be cleared before the galaxy stops growing
        '+1 to size to account for first pop
        size = _size * sizeMultiplier + 1

        Dim starmap As New starmap

        While size > 0
            starmap.stars.Add(newStar(starmap))
        End While

        Return starmap
    End Function


    Private Property numPlanets As New range(3, 7)
    Private Const pathStarnamelist As String = "starnames.txt"
    Private Property starnameList As List(Of String) = filegetStarname()
    Public Function newStar(ByRef _starmap As starmap) As star
        Dim star As New star

        With star
            .starmap = _starmap
            .name = rndStarname()

            For n = 1 To numPlanets.roll
                .planets.Add(newPlanet(star, n))
            Next
        End With

        Return star
    End Function
    Private Function rndStarname() As String
        Dim roll As Integer = rng.Next(starnameList.Count)
        rndStarname = starnameList.Item(roll)
        starnameList.RemoveAt(roll)

        If starnameList.Count = 0 Then starnameList = filegetStarname()
    End Function
    Private Function filegetStarname() As List(Of String)
        Dim starnameList As New List(Of String)

        Try
            Dim sr As New StreamReader(pathStarnamelist)
            Do While sr.Peek <> -1
                Dim line As String = sr.ReadLine
                starnameList.Add(line)
            Loop
        Catch ex As Exception
            bugcatch.alert(Me, "pathStarnamelist invalid")
            For count As Integer = 1 To 20
                starnameList.Add("Star " & count)
            Next
        End Try

        Return starnameList
    End Function


    Private Property numCities As New range(2, 5)
    Private Property planetGovernmentList As New List(Of eGovernment)
    Private Property planetTypeList As New List(Of ePlanetType)
    Private Function newPlanet(ByRef _star As star, _number As Integer) As planet
        Dim planet As New planet

        With planet
            .star = _star
            .number = _number
            .government = rndPlanetGovernment()
            .type = rndPlanetType()

            For n = 1 To numCities.roll
                .cities.Add(newCity(planet, n))
            Next
        End With

        Return planet
    End Function
    Private Function rndPlanetGovernment() As eGovernment
        If planetGovernmentList.Count = 0 Then popPlanetGovernmentList()

        Dim roll As Integer = rng.Next(planetGovernmentList.Count)
        rndPlanetGovernment = planetGovernmentList.Item(roll)
        planetGovernmentList.RemoveAt(roll)
    End Function
    Private Sub popPlanetGovernmentList()
        planetGovernmentList.Clear()

        Dim items As Array = System.Enum.GetValues(GetType(eGovernment))
        For Each item As eGovernment In items
            planetGovernmentList.Add(item)
        Next
    End Sub
    Private Function rndPlanetType() As ePlanetType
        If planetTypeList.Count = 0 Then
            popPlanetTypeList()
            size -= 1
        End If

        Dim roll As Integer = rng.Next(planetTypeList.Count)
        rndPlanetType = planetTypeList.Item(roll)
        planetTypeList.RemoveAt(roll)
    End Function
    Private Sub popPlanetTypeList()
        planetTypeList.Clear()

        Dim items As Array = System.Enum.GetValues(GetType(ePlanetType))
        For Each item As ePlanetType In items
            planetTypeList.Add(item)
        Next
    End Sub


    Private Property demandList As New List(Of eGood)
    Private Function newCity(ByRef _planet As planet, _number As Integer) As city
        Dim city As New city

        With city
            .planet = _planet
            .number = _number
            .supply.Add(rndSupply(_planet.type))
            .demand.Add(rndDemand)
        End With

        Return city
    End Function
    Private Function rndSupply(_type As ePlanetType) As eGood
        Dim roll As Integer = Int(Rnd() * 5 + 1)
        Select Case _type
            Case ePlanetType.Mining 'do nothing
            Case ePlanetType.Industrial : roll += 10
            Case ePlanetType.Sprawl : roll += 20
            Case ePlanetType.Agrarian : roll += 30
            Case ePlanetType.Research : roll += 40
        End Select
        Return roll
    End Function
    Private Function rndDemand() As eGood

    End Function
End Class