Public MustInherit Class location
    Public MustOverride ReadOnly Property name As String
    Public MustOverride Property relationships As List(Of relationship)

    Public Function getRelationship(owner As player) As relationship
        For Each relationship In relationships
            If relationship.owner.Equals(owner) Then Return relationship
        Next

        'no relationship found; make a new one
        Dim newRelationship As New relationship(owner, Me)
        relationships.Add(newRelationship)
        Return newRelationship
    End Function
End Class
