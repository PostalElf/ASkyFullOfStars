Public Class city
    Public Property planet As planet
    Public Property number As Integer
    Public Property supply As New List(Of eGood)
    Public Property demand As New List(Of eGood)

    Public ReadOnly Property name As String
        Get
            Return planet.name & "-" & alphaNumverter(number)
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return name
    End Function
End Class
