Public Class asset
    Public Property name As String
    Public Property location As location
    Public Property ttl As Integer              'TTL 999 = infinite
    Public Property income As Decimal

    Public Overrides Function ToString() As String
        Dim str As String = name & " (" & sign(income) & income & ")"
        If ttl <> 999 Then str &= ", TTL " & ttl
        Return str
    End Function
    Public Sub New(_name As String, _location As location, _ttl As Integer, _income As Decimal)
        name = _name
        location = _location
        ttl = _ttl
        income = _income
    End Sub

    Public Function tick() As report
        If ttl <> 999 Then ttl -= 1
        If ttl <= 0 Then
            'KEYPOINT: run whatever checks need to be done based on special asset names
            Select Case name
                Case "Ouroboros Construction"
                    Dim city As city = CType(location, city)
                    city.assets.constructAsset(New asset("Ouroboros", city, 999, -0.1))
                    Return New report

                Case Else
                    Return New report
            End Select
        End If

        Return Nothing
    End Function
End Class