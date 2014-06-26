Public Class goods
    Public Property location As location
    Private Property supply As New List(Of good)
    Private Property demand As New List(Of good)

    Public Overrides Function ToString() As String
        Dim str As String = ""

        For Each item In supply
            str &= vbSpace & item.ToString & vbNewLine
        Next
        For Each item In demand
            str &= vbSpace & item.ToString & vbNewLine
        Next

        Return str
    End Function
    Public Sub New(_location As location)
        location = _location
    End Sub


    Public Function getUnusedSupply()
        Return supply.FindAll(Function(a) a.destination Is Nothing)
    End Function
    Public Function getUnfilledDemand()
        Return demand.FindAll(Function(a) a.source Is Nothing)
    End Function
    Public Function getSupply(type As eGood) As List(Of good)
        Dim replist As New List(Of good)

        For Each item In supply
            If item.type = type Then replist.Add(item)
        Next

        Return replist
    End Function
    Public Function getDemand(type As eGood) As List(Of good)
        Dim replist As New List(Of good)

        For Each item In supply
            If item.type = type Then replist.Add(item)
        Next

        Return replist
    End Function

    Public Sub addSupply(ByRef good As good)
        supply.Add(good)
    End Sub
    Public Sub addDemand(ByRef good As good)
        demand.Add(good)
    End Sub
End Class
