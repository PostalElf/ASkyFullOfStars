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

        str &= goods.ToString

        For Each relationship In relationships
            str &= vbSpace & "-> " & relationship.owner.name & vbNewLine
            str &= vbSpace & relationship.ToString()
        Next

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

    Public Function tick() As List(Of report)
        Dim replist As New List(Of report)

        For Each relationship In relationships
            replist.AddRange(relationship.assets.tick)
        Next

        Return replist
    End Function
End Class