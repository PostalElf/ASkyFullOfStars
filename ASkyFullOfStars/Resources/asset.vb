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
End Class
