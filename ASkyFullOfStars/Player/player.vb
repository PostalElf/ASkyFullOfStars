Public Class player
    Public Property name As String
    Public Property capital As capital
    Public Property starmap As starmap

    Public Overrides Function ToString() As String
        Return name
    End Function
    Public Sub New(_name As String)
        name = _name
    End Sub


    Public ReadOnly Property totalIncome(starmap As starmap, capital As capital) As Double
        Get
            Dim assetTotal As Double = 0
            Dim goodsTotal As Double = 0

            For Each star In starmap.stars
                For Each planet In star.planets
                    For Each city In planet.cities
                        '?????
                    Next
                Next
            Next

            Return assetTotal + goodsTotal
        End Get
    End Property
End Class
