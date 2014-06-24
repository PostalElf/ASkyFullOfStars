Public Class upgrade
    Public Property name As String
    Public Property type As eUpgrade
    Public Property owner As player
End Class

Public Enum eUpgrade
    Conscript = 1
    Soldier = 2
    Armour = 3

    Building = 10
End Enum
