Public Class capital
    Inherits location
    Public Property owner As player
    Public Property starmap As starmap
    Public Overrides ReadOnly Property name As String
        Get
            Return "Capital Ship"
        End Get
    End Property
    Public Overrides Property agents As New List(Of agent)
    Public Overrides Property assets As assets = New assets(Me)
    Public Property orbiting As location = Nothing

    Public Overrides Function ToString() As String
        Dim str As String = ""

        str &= assets.ToString

        For Each good In goods
            str &= vbSpace & "Goods: " & good.ToString & vbNewLine
        Next

        Return str
    End Function
    Public Sub New(_owner As player, _starmap As starmap)
        owner = _owner
        starmap = _starmap
    End Sub

#Region "goods"
    Public Property goods As New List(Of good)          'holds all goods that are in the supply chain
    Public Function countMatchingGoods(city As city) As Integer
        'count the number of goods that match a given city
        'if a good's destination is X, X must have demand

        Dim total As Integer = 0
        For Each good In goods
            If good.destination Is Nothing = False AndAlso good.destination.Equals(city) Then total += 1
        Next
        Return total
    End Function
#End Region


#Region "travel"
    Public Property travelSpeed As Double = 100
    Private Property origin As planet = Nothing
    Private Property destination As planet = Nothing
    Public Property travelTTL As Integer = 0

    Public Function travelTo(_origin As planet, _destination As planet) As report
        origin = _origin
        destination = _destination
        orbiting = starmap.limbo

        Dim travelDistance As Integer = origin.star.starXY.distanceTo(destination.star.starXY)
        travelTTL = constrain(Math.Ceiling(travelDistance / travelSpeed), 1)

        Return New report()
    End Function
#End Region


    Public Function tick() As List(Of report)
        Dim replist As New List(Of report)

        If travelTTL > 0 Then
            travelTTL -= 1
            If travelTTL <= 0 Then
                replist.Add(New report())
                travelTTL = 0
                orbiting = destination
                origin = Nothing
                destination = Nothing
            End If
        End If

        Return replist
    End Function
End Class
