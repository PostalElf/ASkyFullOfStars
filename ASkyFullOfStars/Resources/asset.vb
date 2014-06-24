Public Class asset
    Public Property name As String
    Public Property type As eAsset
    Public Property location As location
    Public Property owner As player
    Public Property ttl As Integer              'TTL 999 = infinite
    Public Property income As Double
    Public Property onExpire As asset

    Public Overrides Function ToString() As String
        Dim str As String = name & " (" & sign(income) & income & ")"
        If ttl <> 999 Then str &= ", TTL " & ttl
        Return str
    End Function
    Public Sub New(_name As String, _type As eAsset, _location As location, _owner As player, _ttl As Integer, _income As Double, Optional ByRef _onExpire As asset = Nothing)
        name = _name
        location = _location
        owner = _owner
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


Public Enum eAsset
    Military = 1            'military boost to units within the city
    Investment = 2          'negative income for short period of time, then positive for longer
    Production = 3          'creates new demand; if demand is filled, adds new supply
    Infrastructure = 4      'unlocks special abilities
    Provocateur = 5         'increases chaosIncome
    Debuff = 6              'straight up penalty, usually in exchange for some things (eg bribes)
    All = 999               'special case
End Enum



Public Class militaryAsset
    Inherits asset
    Public Property unitType As eUnit
    Public Property unitPower As Double

    Public Sub New(_name As String, _type As eAsset, _location As location, _owner As player, _ttl As Integer, _income As Double, _unitType As eUnit, _unitPower As Double, Optional ByRef _onExpire As asset = Nothing)
        MyBase.New(_name, _type, _location, _owner, _ttl, _income, _onExpire)
        unitType = _unitType
        unitPower = _unitPower
    End Sub
End Class