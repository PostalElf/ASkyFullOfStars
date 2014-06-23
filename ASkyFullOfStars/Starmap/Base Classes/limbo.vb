Public Class limbo
    Inherits location
    Public Overrides Property agents As New List(Of agent)
    Public Overrides Property assets As New assets(Me)

    Public Overrides ReadOnly Property name As String
        Get
            Return "The Void"
        End Get
    End Property
End Class
