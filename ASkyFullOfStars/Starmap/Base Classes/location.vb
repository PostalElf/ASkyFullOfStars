Public MustInherit Class location
    Public MustOverride ReadOnly Property name As String
    Public MustOverride Property relationships As List(Of relationship)

    Public Function getRelationship(owner As player)
        For Each relationship In relationships
            If relationship.owner.Equals(owner) Then Return relationship
        Next
        Return Nothing
    End Function
End Class
