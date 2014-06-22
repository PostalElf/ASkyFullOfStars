Public Class city
    Inherits location
    Public Property planet As planet
    Public Property number As Integer
    Public Overrides ReadOnly Property name As String
        Get
            Return planet.name & "-" & alphaNumverter(number)
        End Get
    End Property
    Public ReadOnly Property id As String
        Get
            Return planet.star.name & " " & planet.number & " " & number
        End Get
    End Property
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
    Public Property assets As New assets(Me)
    Public Property supply As New List(Of eGood)
    Public Property demand As New List(Of eGood)

    Public Overrides Function ToString() As String
        Dim str As String = name & vbNewLine

        str &= vbSpace & "Size: " & size & vbNewLine
        str &= assets.ToString
        For Each item In supply
            str &= vbSpace & "Supply: " & good.goodType2String(item) & vbNewLine
        Next
        For Each item In demand
            str &= vbSpace & "Demand: " & good.goodType2String(item) & vbNewLine
        Next

        'agents are not read from XML at starmap level
        str &= vbSpace & "Agents: " & agents.Count
        str &= vbNewLine

        Return str
    End Function
    Public Sub New(ByRef starmapBuilder As starmapBuilder, ByRef _planet As planet, _number As Integer)
        planet = _planet
        number = _number
        size = starmapBuilder.rndCitySize
        supply.Add(starmapBuilder.rndSupply(_planet.type, Me))
        demand.Add(starmapBuilder.rndDemand(_planet.type, Me))
    End Sub
    Public Sub New(_planet As planet, _number As Integer)
        planet = _planet
        number = _number
    End Sub


    Public Function tick() As List(Of report)
        Dim replist As New List(Of report)

        replist.AddRange(assets.tick)

        Return replist
    End Function
End Class