Public Class star
    Inherits location
    Public Property starmap As starmap
    Public Property planets As New List(Of planet)
    Public Property starname As String
    Public Overrides ReadOnly Property name As String
        Get
            Return starname
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return name & " (Planets: " & planets.Count & ")"
    End Function
    Public Sub New(ByRef starmapBuilder As starmapBuilder, ByRef _starmap As starmap)
        starmap = _starmap
        starname = starmapBuilder.rndStarname()

        For n = 1 To starmapBuilder.numPlanets.roll
            planets.Add(New planet(starmapBuilder, Me, n))
        Next
    End Sub
    Public Sub New(_starmap As starmap)
        starmap = _starmap
    End Sub

    Public Function getPlanet(number As Integer) As planet
        For Each planet In planets
            If planet.number = number Then Return planet
        Next
        Return Nothing
    End Function
End Class
