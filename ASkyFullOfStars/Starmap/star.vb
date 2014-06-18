Public Class star
    Public Property starmap As starmap
    Public Property planets As New List(Of planet)
    Public Property name As String

    Public Overrides Function ToString() As String
        Return name & " (Planets: " & planets.Count & ")"
    End Function

End Class
