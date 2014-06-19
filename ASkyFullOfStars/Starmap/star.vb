Public Class star
    Public Property starmap As starmap
    Public Property planets As New List(Of planet)
    Public Property name As String

    Public Overrides Function ToString() As String
        Return name & " (Planets: " & planets.Count & ")"
    End Function
    Public Sub New(ByRef starmapBuilder As starmapBuilder, ByRef _starmap As starmap)
        starmap = _starmap
        name = starmapBuilder.rndStarname()

        For n = 1 To starmapBuilder.numPlanets.roll
            planets.Add(New planet(starmapBuilder, Me, n))
        Next
    End Sub
    Public Sub New(_starmap As starmap)
        starmap = _starmap
    End Sub
End Class
