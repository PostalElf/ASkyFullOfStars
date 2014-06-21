Public Class city
    Inherits location
    Public Property planet As planet
    Public Property number As Integer
    Public Overrides ReadOnly Property name As String
        Get
            Return planet.name & "-" & alphaNumverter(number)
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
    Public Property supply As New List(Of eGood)
    Public Property demand As New List(Of eGood)
    Public Property assets As New List(Of asset)
    Public Property newAssets As New List(Of asset)
    Public ReadOnly Property assetIncome As Decimal
        Get
            Dim total As Decimal = 0
            For Each asset In assets
                total += asset.income
            Next
            Return total
        End Get
    End Property

    Public Overrides Function ToString() As String
        Dim str As String = name & vbNewLine

        str &= vbSpace & "Size: " & size & vbNewLine
        For Each item In supply
            str &= vbSpace & "Supply: " & good.goodType2String(item) & vbNewLine
        Next
        For Each item In demand
            str &= vbSpace & "Demand: " & good.goodType2String(item) & vbNewLine
        Next
        For Each item In assets
            str &= vbSpace & "Asset: " & item.ToString & vbNewLine
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

        'tick assets and remove all whose ttl = 0
        'note that constructing assets are unfolded within asset.tick and added to newAssets
        'but asset addition and removal must take place outside of asset
        For Each asset In assets
            Dim report As report = asset.tick()
            If report Is Nothing = False Then replist.Add(report)
        Next
        assets.AddRange(newAssets)
        newAssets.Clear()
        assets.RemoveAll(Function(a) a.ttl <= 0)

        Return replist
    End Function
End Class
