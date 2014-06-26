Public Class relationship
    Public Property owner As player
    Public Property location As location
    Public Property assets As New assets(location)
    Public Property agents As New List(Of agent)

    Public Sub New(_owner As player, _location As location)
        owner = _owner
        location = _location
    End Sub
End Class
