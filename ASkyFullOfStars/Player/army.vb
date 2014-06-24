Public Class army
    Public Property owner As player
    Public Property location As location
    Private Property assets As New assets(Nothing)

    Public units As New List(Of unit)
    Public Sub refreshUnitPower()
        'reset unit power to base values
        For Each unit In units
            unit.resetPower()
        Next

        'populate list with militaryAssets from self and current location
        Dim assetList As New List(Of asset)(assets.getAssets(eAsset.Military))
        If location.assets Is Nothing = False Then assetList.AddRange(location.assets.getAssets(eAsset.Military, owner))


        'start adding bonuses
        For Each asset In assetList
            Dim milAsset As militaryAsset = CType(asset, militaryAsset)
            For Each unit In units
                If unit.type = milAsset.type Then unit.power += milAsset.unitPower
            Next
        Next

        assetList = Nothing
    End Sub
    Public ReadOnly Property totalPower As Double
        Get
            Dim total As Double = 0
            For Each unit In units
                total += unit.totalPower
            Next
            Return total
        End Get
    End Property

    Public Sub takeDamage(totalEnemyPower As Double)
        Dim damage As Double = totalEnemyPower * 0.2

        For n As Integer = 1 To 3
            If damage <= 0 Then Exit Sub
            woundUnit(damage, getUnit(n))
        Next
    End Sub
    Private Sub woundUnit(ByRef damage As Double, ByRef unit As unit)
        Dim casualties As Integer = constrain(Math.Floor(damage / unit.power), unit.qty)
        unit.qty = constrain(unit.qty - casualties, 0)
        damage = damage - (casualties * unit.power)
    End Sub
    Private Function getUnit(index As Integer) As unit
        For Each unit In units
            If unit.type = index Then Return unit
        Next
        Return Nothing
    End Function

    Public Sub addAsset(asset As asset)
        If asset.type = eAsset.Military Then
            assets.add(asset)
            refreshUnitPower()
        Else
            'do nothing
        End If
    End Sub
    Public Sub removeAsset(asset As asset)
        assets.remove(asset)
    End Sub

End Class


Public Class unit
    Public type As eUnit
    Public qty As Integer
    Public power As Double
    Public ReadOnly Property totalPower As Double
        Get
            Return power * qty
        End Get
    End Property


    Public Sub New(_type As eUnit, _qty As Integer, _power As Double)
        type = _type
        qty = _qty
        power = _power
    End Sub

    Public Sub resetPower()
        Select Case type
            Case eUnit.Conscript : power = 0.8
            Case eUnit.Soldier : power = 1.0
            Case eUnit.Armour : power = 1.5
        End Select
    End Sub
End Class

Public Enum eUnit
    Conscript = 1
    Soldier = 2
    Armour = 3
End Enum