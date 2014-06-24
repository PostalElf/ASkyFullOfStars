Public Class army
    Public Property owner As player
    Public Property location As location

    Public Property conscripts As Integer
    Public ReadOnly Property conscriptPower As Double
        Get
            Dim total As Double = 0.8
            For Each upgrade In owner.upgrades

            Next
            Return total
        End Get
    End Property
    Public Property soldiers As Integer
    Public ReadOnly Property soldierPower As Double
        Get
            Dim total As Double = 1
            For Each upgrade In owner.upgrades

            Next
            Return total
        End Get
    End Property
    Public Property armour As Integer
    Public ReadOnly Property armourPower As Double
        Get
            Dim total As Double = 1.5
            For Each upgrade In owner.upgrades

            Next
            Return total
        End Get
    End Property
    Public ReadOnly Property totalPower As Double
        Get
            Return (conscripts * conscriptPower) + (soldiers * soldierPower) + (armour * armourPower)
        End Get
    End Property

    Public Sub takeDamage(totalEnemyPower As Double)
        Dim damage As Double = totalEnemyPower * 0.2

        If damage > 0 Then
            woundUnit(damage, conscripts, conscriptPower)
            If damage <= 0 Then Exit Sub
            woundUnit(damage, soldiers, soldierPower)
            If damage <= 0 Then Exit Sub
            woundUnit(damage, armour, armourPower)
        End If
    End Sub
    Private Sub woundUnit(ByRef damage As Double, ByRef unit As Integer, ByRef unitPower As Double)
        Dim casualties As Integer = constrain(Math.Floor(damage / unitPower), unit)
        unit = constrain(unit - casualties, 0)
        damage = damage - (casualties * unitPower)
    End Sub
End Class
