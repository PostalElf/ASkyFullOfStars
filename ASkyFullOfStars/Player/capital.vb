Public Class capital
    Inherits location
    Public Property player As player
    Public Property starmap As starmap
    Public Overrides ReadOnly Property name As String
        Get
            Return "Capital Ship"
        End Get
    End Property
    Public Overrides Property agents As New List(Of agent)
    Public Overrides Property assets As assets = New assets(Me)
    Public ReadOnly Property income As Decimal
        Get
            Dim assetTotal As Decimal = 0
            Dim goodsTotal As Decimal = 0

            For Each star In starmap.stars
                For Each planet In star.planets
                    For Each city In planet.cities
                        assetTotal += city.assets.income
                        goodsTotal += countMatchingGoods(city) * 2
                    Next
                Next
            Next

            assetTotal += assets.income

            Return assetTotal + goodsTotal
        End Get
    End Property
    Public Property orbiting As location = Nothing

    Public Overrides Function ToString() As String
        Dim str As String = ""

        str &= assets.ToString

        For Each good In goods
            str &= vbSpace & "Goods: " & good.ToString & vbNewLine
        Next

        str &= vbNewLine
        str &= vbSpace & "Total Income: " & sign(income) & income & vbNewLine

        Return str
    End Function
    Public Sub New(_player As player, _starmap As starmap)
        player = _player
        starmap = _starmap
    End Sub

#Region "goods"
    Public Property goods As New List(Of good)          'holds all goods that are in the supply chain
    Private Function countMatchingGoods(city As city) As Integer
        'count the number of goods that match a given city
        'if a good's destination is X, X must have demand

        Dim total As Integer = 0
        For Each good In goods
            If good.destination Is Nothing = False AndAlso good.destination.Equals(city) Then total += 1
        Next
        Return total
    End Function
#End Region


#Region "travel"
    Public Property travelSpeed As Decimal = 100
    Private Property origin As planet = Nothing
    Private Property destination As planet = Nothing
    Public Property travelTTL As Integer = 0

    Public Function travelTo(_origin As planet, _destination As planet) As report
        origin = _origin
        destination = _destination
        orbiting = starmap.limbo

        Dim travelDistance As Integer = origin.star.starXY.distanceTo(destination.star.starXY)
        travelTTL = constrain(Math.Ceiling(travelDistance / travelSpeed), 1)

        Return New report()
    End Function
#End Region


    Public Function tick() As List(Of report)
        Dim replist As New List(Of report)

        If travelTTL > 0 Then
            travelTTL -= 1
            If travelTTL <= 0 Then
                replist.Add(New report())
                travelTTL = 0
                orbiting = destination
                origin = Nothing
                destination = Nothing
            End If
        End If

        Return replist
    End Function
End Class
