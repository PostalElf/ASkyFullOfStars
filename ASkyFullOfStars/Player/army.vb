Public Class army
    Public Property owner As player
    Public Property location As location
    Private Property assets As New assets(Nothing)
    Public units As New List(Of unit)
    Public ReadOnly Property totalPower As Double
        Get
            Dim total As Double = 0
            For Each unit In units
                total += unit.totalPower
            Next
            Return total
        End Get
    End Property

    Public Overrides Function ToString() As String
        Dim str As String = ""
        For Each unit In units
            str &= unit.ToString()
        Next
        Return str
    End Function
    Public Sub New(_owner As player, _location As location)
        owner = _owner
        location = _location
    End Sub

    Public Sub takeDamage(totalEnemyPower As Double)
        'deal 20% of damage per turn
        Dim damage As Double = totalEnemyPower * 0.2


        'damage each unit, starting from conscript
        For n As Integer = 1 To 3
            If damage <= 0.8 Then Exit Sub
            Dim activeUnit As unit = getUnit(n)
            If activeUnit Is Nothing = False Then woundUnit(damage, activeUnit)
        Next


        'remove all units with 0 qty
        units.RemoveAll(Function(a) a.qty <= 0)
    End Sub
    Private Sub woundUnit(ByRef damage As Double, ByRef unit As unit)
        Dim casualties As Integer = constrain(Math.Floor(damage / unit.power), 0, unit.qty)
        unit.qty = constrain(unit.qty - casualties, 0, unit.qty)
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
    Public Sub refreshUnitPower()
        'reset unit power to base values
        For Each unit In units
            unit.resetPower()
        Next

        'start adding bonuses
        For Each asset In assets.getAssets(eAsset.Military)
            Dim milAsset As militaryAsset = CType(asset, militaryAsset)
            For Each unit In units
                If unit.type = milAsset.type Then unit.power += milAsset.unitPower
            Next
        Next
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

    Public Overrides Function ToString() As String
        Return vbSpace & qty & " " & type.ToString & " (" & power & ")" & vbNewLine
    End Function
    Public Sub New(_type As eUnit, _qty As Integer)
        type = _type
        qty = _qty
        resetPower()
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