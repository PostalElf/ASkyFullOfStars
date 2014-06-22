Public Class xy
    Public Property x As Integer
    Public Property y As Integer

    Public Overrides Function ToString() As String
        Return x & "," & y
    End Function
    Public Sub New(_x As Integer, _y As Integer)
        x = _x
        y = _y
    End Sub

    Public Function distanceTo(destinationXY As xy) As Integer
        Dim a As Integer = Math.Abs(x - destinationXY.x)
        Dim b As Integer = Math.Abs(y - destinationXY.y)
        Dim c As Integer = Math.Ceiling(Math.Sqrt((a * a) + (b * b)))

        Return c
    End Function
End Class
