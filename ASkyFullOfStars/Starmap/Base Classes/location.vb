Public MustInherit Class location
    Public MustOverride Property agents As List(Of agent)
    Public MustOverride Property assets As assets
    Public MustOverride ReadOnly Property name As String
End Class
