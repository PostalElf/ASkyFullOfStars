Public Class city
    Inherits location
    Public Property planet As planet
    Public Property number As Integer
    Public Property supply As New List(Of eGood)
    Public Property demand As New List(Of eGood)

    Public ReadOnly Property name As String
        Get
            Return planet.name & "-" & alphaNumverter(number)
        End Get
    End Property

    Public Overrides Function ToString() As String
        'Return name
        Return planet.type.ToString & " " & supply.Item(0) & "|" & demand.Item(0)
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
