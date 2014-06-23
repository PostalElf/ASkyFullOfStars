Public Class asset
    Public Property name As String
    Public Property location As location
    Public Property ttl As Integer              'TTL 999 = infinite
    Public Property income As Decimal
    Public Property onExpire As asset

    Public Overrides Function ToString() As String
        Dim str As String = name & " (" & sign(income) & income & ")"
        If ttl <> 999 Then str &= ", TTL " & ttl
        Return str
    End Function
    Public Sub New(_name As String, _location As location, _ttl As Integer, _income As Decimal, Optional ByRef _onExpire As asset = Nothing)
        name = _name
        location = _location
        ttl = _ttl
        income = _income
        onExpire = _onExpire
    End Sub

    Public Function tick() As report
        If ttl <> 999 Then ttl -= 1
        If ttl <= 0 Then
            If onExpire Is Nothing = False Then
                location.assets.construct(onExpire)
                Return New report
            End If
        End If

        Return Nothing
    End Function
End Class