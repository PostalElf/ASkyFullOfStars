Public Class limbo
    Inherits location

    Public Overrides ReadOnly Property name As String
        Get
            Return "The Void"
        End Get
    End Property
    Public Overrides Property relationships As New List(Of relationship)
End Class
