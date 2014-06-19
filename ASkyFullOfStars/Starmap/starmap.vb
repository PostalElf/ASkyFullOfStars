﻿Public Class starmap
    Public Property stars As New List(Of star)
    Public ReadOnly Property numberOfPlanets
        Get
            Dim total As Integer = 0
            For Each star In stars
                total += star.planets.Count
            Next
            Return total
        End Get
    End Property
    Public ReadOnly Property numberOfCities
        Get
            Dim total As Integer = 0
            For Each star In stars
                For Each planet In star.planets
                    total += planet.cities.Count
                Next
            Next
            Return total
        End Get
    End Property

    Public Sub New(ByRef starmapBuilder As starmapBuilder)
        While starmapBuilder.size > 0
            stars.Add(New star(starmapBuilder, Me))
        End While
    End Sub
End Class
