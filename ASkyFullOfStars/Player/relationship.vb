Public Class relationship
    Public Property location As location
    Public Property owner As player
    Public Property casus As eCasus
    Public Property siegePressure As Integer

    Public Sub New(_owner As player, _location As location)
        owner = _owner
        location = _location
    End Sub
End Class

Public Enum eCasus
    Neutral
    Import
    Export
    Auxilia
    Belli
End Enum
