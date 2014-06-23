Public Class planet
    Inherits location
    Public Overrides ReadOnly Property name As String
        Get
            Return star.name & " " & romanNumverter(number)
        End Get
    End Property
    Public Overrides Property assets As New assets(Me)
    Public Overrides Property agents As New List(Of agent)
    Public Property star As star
    Public Property number As Integer
    Public Property government As eGovernment
    Public Property type As ePlanetType
    Public Property cities As New List(Of city)

    Public Overrides Function ToString() As String
        Return name & " (Cities: " & cities.Count & ")"
    End Function
    Public Sub New(ByRef starmapBuilder As starmapBuilder, ByRef _star As star, _number As Integer)
        star = _star
        number = _number
        government = starmapBuilder.rndPlanetGovernment()
        type = starmapBuilder.rndPlanetType()

        For n = 1 To starmapBuilder.numCities.roll
            cities.Add(New city(starmapBuilder, Me, n))
        Next

        'repopulate citysizeList because it's a new planet
        starmapBuilder.popCitySizeList()
    End Sub
    Public Sub New(_star As star, _number As Integer)
        star = _star
        number = _number
    End Sub


    Public Function getCity(_number As Integer) As city
        For Each city In cities
            If city.number = _number Then Return city
        Next
        Return Nothing
    End Function


    Public Function tick() As List(Of report)
        Dim replist As New List(Of report)

        For Each city In cities
            replist.AddRange(city.tick)
        Next

        replist.AddRange(assets.tick)

        Return replist
    End Function
End Class

Public Enum ePlanetType
    Mining = 1
    Industrial = 2
    Sprawl = 3
    Agrarian = 4
    Research = 5
End Enum

Public Enum eGovernment
    Monarchy = 1
    Theocracy = 2
    Democracy = 3
    Oligarchy = 4
    Anocracy = 5
End Enum