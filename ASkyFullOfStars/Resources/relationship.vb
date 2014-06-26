Public Class relationship
    Public Property owner As player
    Public Property location As location
    Public Property assets As New assets(location, owner)
    Public Property agents As New List(Of agent)

    Public Overrides Function ToString() As String
        Dim str As String = ""

        str &= assets.ToString

        For Each agent In agents
            str &= agent.ToString
        Next

        Return str
    End Function
    Public Sub New(_owner As player, _location As location)
        owner = _owner
        location = _location
    End Sub
End Class
