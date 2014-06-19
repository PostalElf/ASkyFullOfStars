﻿Public Class planet
    Public Property star As star
    Public Property type As ePlanetType
    Public Property government As eGovernment
    Public Property cities As New List(Of city)
    Public Property number As Integer
    Public ReadOnly Property name As String
        Get
            Return star.name & " " & romanNumverter(number)
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return name & " (Cities: " & cities.Count & ")"
    End Function
    Public Sub New(ByRef starmapBuilder As starmapBuilder, ByRef _star As star, _number As Integer)
        star = _star
        number = _number
        government = starmapBuilder.rndPlanetGovernment()
        type = starmapBuilder.rndPlanetType()

        For n = 1 To starmapBuilder.numCities.roll
            cities.Add(New city(starmapBuilder, Me, n))
        Next
    End Sub
    Public Sub New(_star As star, _number As Integer)
        star = _star
        number = _number
    End Sub
End Class

Public Enum ePlanetType
    Mining = 1
    Industrial = 2
    Sprawl = 3
    Agrarian = 4
    Research = 5
End Enum

Public Enum eGovernment
    Monarchy = 1
    Theocracy = 2
    Democracy = 3
    Oligarchy = 4
    Anocracy = 5
End Enum