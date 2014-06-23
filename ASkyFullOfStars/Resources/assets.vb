Public Class assets
    Public Property location As location
    Public ReadOnly Property income As Decimal
        Get
            Dim total As Decimal = 0
            For Each asset In assetList
                total += asset.income
            Next
            Return total
        End Get
    End Property
    Public Property assetList As New List(Of asset)
    Private Property newAssetList As New List(Of asset)

    Public Overrides Function ToString() As String
        If assetList.Count = 0 Then
            Return Nothing
        Else
            Dim str As String = ""
            For Each asset In assetList
                str &= vbSpace & "Asset: " & asset.ToString & vbNewLine
            Next
            Return str
        End If
    End Function
    Public Sub New(_location As location)
        location = _location
    End Sub

    Public Sub add(asset As asset)
        assetList.Add(asset)
    End Sub
    Public Sub remove(asset As asset)
        assetList.Remove(asset)
    End Sub
    Public Sub construct(asset As asset)
        newAssetList.Add(asset)
    End Sub

    Public Function tick() As List(Of report)
        Dim replist As New List(Of report)

        'tick assets and remove all whose ttl = 0
        'note that constructing assets are unfolded within asset.tick and added to newAssets
        'but asset addition and removal must take place outside of asset

        For Each asset In assetList
            Dim report As report = asset.tick()
            If report Is Nothing = False Then replist.Add(report)
        Next
        assetList.AddRange(newAssetList)
        newAssetList.Clear()
        assetList.RemoveAll(Function(a) a.ttl <= 0)

        Return replist
    End Function
End Class