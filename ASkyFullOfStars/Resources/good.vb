Public Class good
    Public Property type As eGood
    Public Property source As location
    Public Property destination As location

    Public Overrides Function ToString() As String
        If source Is Nothing AndAlso destination Is Nothing Then
            'something's wrong
            bugcatch.alert(Me, "bug in class good")
            Return Nothing
        ElseIf source Is Nothing Then
            'is a demand
            Return "Demand: " & goodType2String(type)
        ElseIf destination Is Nothing Then
            Return "Supply: " & goodType2String(type)
        Else
            'fulfilled contract
            Return "Contract: " & goodType2String(type) & " from " & source.name & " to " & destination.name
        End If
    End Function
    Public Sub New(_type As eGood, _source As location, _destination As location)
        type = _type
        source = _source
        destination = _destination
    End Sub

    Public Shared Function goodType2String(_type As eGood) As String
        Select Case _type
            Case eGood.HMetals : Return "Heavy Metals"
            Case eGood.PStones : Return "Precious Stones"
            Case eGood.CMaterials : Return "Construction Materials"
            Case eGood.IWaste : Return "Industrial Waste"
            Case eGood.NGas : Return "Natural Gas"

            Case eGood.CElect : Return "Consumer Electronics"
            Case eGood.SArms : Return "Small Arms"
            Case eGood.PLuxes : Return "Petit Luxes"
            Case eGood.HMachinery : Return "Heavy Machinery"
            Case eGood.PBuilding : Return "Prefab Buildings"

            Case eGood.HArt : Return "High Art"
            Case eGood.PCulture : Return "Popular Culture"
            Case eGood.MMedia : Return "Mass Media"
            Case eGood.HSlaves : Return "Human Slaves"
            Case eGood.RDrugs : Return "Recreational Drugs"

            Case eGood.APets : Return "Alien Pets"
            Case eGood.EFoods : Return "Exotic Foods"
            Case eGood.ACompounds : Return "Azothic Compounds"
            Case eGood.XPharm : Return "Xenobiological Pharmaceuticals"
            Case eGood.ETourism : Return "Eden Tourism"

            Case eGood.CPatents : Return "Commercial Patents"
            Case eGood.SSavants : Return "Scientific Savants"
            Case eGood.CSavants : Return "Cultural Savants"
            Case eGood.ADesigns : Return "Architectural Designs"
            Case eGood.AIntel : Return "Artificial Intelligences"

            Case Else : Return Nothing
        End Select
    End Function
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