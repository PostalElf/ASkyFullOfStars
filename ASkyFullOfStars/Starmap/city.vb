Public Class city
    Inherits location
    Public Property planet As planet
    Public Property number As Integer
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
    Public Property supply As New List(Of eGood)
    Public Property demand As New List(Of eGood)

    Public ReadOnly Property name As String
        Get
            Return planet.name & "-" & alphaNumverter(number)
        End Get
    End Property

    Public Overrides Function ToString() As String
        Dim str As String = name & vbNewLine

        str &= vbSpace & "Supply: "
        For Each item In supply
            str &= good.goodType2String(item) & " "
        Next
        str &= vbNewLine
        str &= vbSpace & "Demand: "
        For Each item In demand
            str &= good.goodType2String(item) & " "
        Next
        str &= vbNewLine
        str &= vbSpace & "Agents: " & agents.Count
        str &= vbNewLine

        Return str
    End Function
    Public Sub New(ByRef starmapBuilder As starmapBuilder, ByRef _planet As planet, _number As Integer)
        planet = _planet
        number = _number
        supply.Add(starmapBuilder.rndSupply(_planet.type, Me))
        demand.Add(starmapBuilder.rndDemand(_planet.type, Me))
    End Sub
    Public Sub New(_planet As planet, _number As Integer)
        planet = _planet
        number = _number
    End Sub
End Class
