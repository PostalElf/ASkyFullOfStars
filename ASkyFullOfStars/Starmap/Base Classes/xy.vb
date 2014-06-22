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
End Class
