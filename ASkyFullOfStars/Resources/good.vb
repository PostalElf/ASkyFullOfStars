Public Class good
    Public Property type As eGood
    Public Property source As location
    Public Property destination As location

    Public Overrides Function ToString() As String
        Return type.ToString
    End Function
    Public Sub New(_type As eGood, _source As location, _destination As location)
        type = _type
    End Sub
End Class

Public Enum eGood
    HMetals = 1
    PStones = 2
    CMaterials = 3
    IWaste = 4
    NGas = 5

    CElect = 11
    SArms = 12
    PLuxes = 13
    HMachinery = 14
    PBuilding = 15

    HArt = 21
    PCulture = 22
    MMedia = 23
    HSlaves = 24
    RDrugs = 25

    APets = 31
    EFoods = 32
    ACompounds = 33
    XPharm = 34
    ETourism = 35

    CPatents = 41
    SSavants = 42
    CSavants = 43
    ADesigns = 44
    AIntel = 45
End Enum