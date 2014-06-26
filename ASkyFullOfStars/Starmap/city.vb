Public Class city
    Inherits location
    Public Property planet As planet
    Public Property number As Integer
    Public Overrides ReadOnly Property name As String
        Get
            Return planet.name & "-" & romanNumverter(number)
        End Get
    End Property
    Public ReadOnly Property id As String
        Get
            'only used for filefetch
            Return planet.star.name & " " & planet.number & " " & number
        End Get
    End Property
    Public Overrides Property relationships As New List(Of relationship)
    Public Property goods As New goods(Me)
    Public ReadOnly Property government
        Get
            Return planet.government
        End Get
    End Property
    Public ReadOnly Property type
        Get
            Return planet.type
        End Get
    End Property
    Public Property size As Integer

    Public Overloads Function ToString() As String
        Dim str As String = name & vbNewLine

        str &= vbSpace & "Size: " & size & vbNewLine

        For Each relationship In relationships
            str &= relationship.ToString() & vbNewLine
        Next

        str &= goods.ToString

        Return str
    End Function
    Public Sub New(ByRef starmapBuilder As starmapBuilder, ByRef _planet As planet, _number As Integer)
        planet = _planet
        number = _number
        size = starmapBuilder.rndCitySize

        goods.addSupply(starmapBuilder.rndSupply(_planet.type, Me))
        goods.addDemand(starmapBuilder.rndDemand(_planet.type, Me))
    End Sub
    Public Sub New(_planet As planet, _number As Integer)
        planet = _planet
        number = _number
    End Sub

    Public Function landAgent(owner As player, agent As agent) As report
        Dim relationship As relationship = getRelationship(owner)
        If relationship Is Nothing Then
            'no relationship exists yet
            'add one and move on
            relationship = New relationship(owner, Me)
            relationships.Add(relationship)
        End If

        'land the agent
        relationship.agents.Add(agent)
        agent.location = Me


        'return report
        Return New report()
    End Function
    Public Function disembarkAgent(owner As player, agent As agent, capital As capital) As report
        Dim relationship As relationship = getRelationship(owner)
        If relationship Is Nothing Then
            'relationship is empty
            'therefore, definitely no agents
            Return Nothing
        End If

        For Each currentagent In relationship.agents
            If currentagent.Equals(agent) Then
                'found agent
                'move him offworld and into the capital
                relationship.agents.Remove(currentagent)
                currentagent.location = capital


                'return report
                Return New report()
            End If
        Next
        Return Nothing
    End Function

    Public Function tick() As List(Of report)
        Dim replist As New List(Of report)

        For Each relationship In relationships
            replist.AddRange(relationship.assets.tick)
        Next

        Return replist
    End Function
End Class