Public Class agent
    Public Property location As location
    Public Property owner As player
    Dim pteTraveLTTL As Integer
    Public Property travelTTL As Integer
        Get
            Return pteTraveLTTL
        End Get
        Set(value As Integer)
            pteTraveLTTL = value
            If travelTTL <= 0 Then
                location = travelDestination
                travelDestination = Nothing
                location.getRelationship(owner).agents.Add(Me)
            End If
        End Set
    End Property
    Private Property travelDestination As location

    Public Function travelTo(destination As location) As feedback
        If TypeOf location Is capital Then
            Return travelFromCapital(destination)
        ElseIf TypeOf location Is city Then
            Return travelFromCity(destination)
        Else
            'error
            bugcatch.alert(Me, "Agent is on an illegal location")
            Return New erroll()
        End If
    End Function
    Private Function travelFromCapital(destination As location) As feedback
        'double check to make sure that destination is a city
        If TypeOf destination Is city = False Then Return New erroll

        Return beginTravel(location, destination, 0)
    End Function
    Private Function travelFromCity(destination As location) As feedback
        If TypeOf destination Is city Then
            'ensure that the destination is on the same planet
            Dim currentDestination As city = CType(destination, city)
            Dim currentLocation As city = CType(destination, city)

            If currentDestination.planet.Equals(currentLocation.planet) Then
                'same planet, begin travel
                Return beginTravel(currentLocation, currentDestination, 1)


            ElseIf currentDestination.planet.star.Equals(currentLocation.planet.star) Then
                'in same star system; check for solar rail
                Const assetName As String = "Solar Rail System"
                If currentDestination.planet.getRelationship(owner).hasAsset(assetName) AndAlso currentLocation.planet.getRelationship(owner).hasAsset(assetName) Then
                    Return beginTravel(currentLocation, currentDestination, 3)
                Else
                    Return New erroll
                End If


            Else
                Return New erroll


            End If


        ElseIf TypeOf destination Is capital Then
            'ensure that the destination is orbiting the planet; otherwise reject
            Dim capital As capital = CType(destination, capital)

            If capital.orbiting.Equals(Me) Then Return beginTravel(location, capital, 0) Else Return New erroll
        Else
            'destination is neither capital nor city
            bugcatch.alert(Me, "Destination is neither capital nor city")
            Return New erroll()
        End If
    End Function

    Private Function beginTravel(source As location, destination As location, ttl As Integer) As feedback
        'move him offworld 
        source.getRelationship(owner).agents.Remove(Me)
        location = Nothing
        travelDestination = destination
        travelTTL = ttl             'setting travelTTL will automatically fire destination code if valid

        Return New report
    End Function
End Class
