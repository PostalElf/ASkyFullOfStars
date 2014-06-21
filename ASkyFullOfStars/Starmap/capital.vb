Public Class capital
    Inherits location
    Public Property starmap As starmap
    Public Overrides ReadOnly Property name As String
        Get
            Return "Capital Ship"
        End Get
    End Property
    Public Property goods As New List(Of good)          'holds all goods that are in the supply chain
    Public Property assets As New List(Of asset)
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

            For Each asset In assets
                assetTotal += asset.income
            Next

            Return assetTotal + goodsTotal
        End Get
    End Property

    Public Overrides Function ToString() As String
        Dim str As String = ""

        For Each asset In assets
            str &= vbSpace & "Asset: " & asset.ToString & vbNewLine
        Next

        For Each good In goods
            str &= vbSpace & "Goods: " & good.ToString & vbNewLine
        Next

        str &= vbNewLine
        str &= vbSpace & "Total Income: " & sign(income) & income & vbNewLine

        Return str
    End Function
    Public Sub New(_starmap As starmap)
        starmap = _starmap
    End Sub


    Private Function countMatchingGoods(city As city) As Integer
        'count the number of goods that match a given city
        'if a good's destination is X, X must have demand

        Dim total As Integer = 0
        For Each good In goods
            If good.destination Is Nothing = False AndAlso good.destination.Equals(city) Then total += 1
        Next
        Return total
    End Function
End Class
