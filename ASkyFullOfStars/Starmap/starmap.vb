﻿Public Class starmap
    Public Property stars As New List(Of star)
    Public Property limbo As New limbo
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
    Public Sub New()
        'do nothing
    End Sub

    Public Function getStar(name As String) As star
        For Each star In stars
            If star.name = name Then Return star
        Next
        Return Nothing
    End Function
    Public Function getPlanet(starName As String, planetNumber As Integer) As planet
        Return getStar(starName).getPlanet(planetNumber)
    End Function
    Public Function getCity(starName As String, planetNumber As Integer, cityNumber As Integer) As city
        Return getStar(starName).getPlanet(planetNumber).getCity(cityNumber)
    End Function
    Public Function getCity(id As String) As city
        Dim splitString() As String = Split(id)
        Dim starName As String = splitString(0)
        Dim planetNumber As Integer = CInt(splitString(1))
        Dim cityNumber As Integer = CInt(splitString(2))
        Return getCity(starName, planetNumber, cityNumber)
    End Function


    Public Function tick() As List(Of report)
        Dim replist As New List(Of report)

        For Each star In stars
            replist.AddRange(star.tick())
        Next

        Return replist
    End Function
End Class

