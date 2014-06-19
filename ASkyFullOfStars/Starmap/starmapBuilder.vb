﻿Imports System.IO


Public Class starmapBuilder
    Public Property size As Integer = 0
    Private Const sizeMultiplier As Integer = 3

    Public Sub New(_size As Integer)
        'size governs how many times the planetTypeList must be cleared before the galaxy stops growing
        '+1 to size to account for first pop
        size = _size * sizeMultiplier + 1
    End Sub


    Public Property numPlanets As New range(3, 7)
    Private Const pathStarnamelist As String = "starnames.txt"
    Private Property starnameList As List(Of String) = filegetStarname()
    Public Function rndStarname() As String
        Dim roll As Integer = rng.Next(starnameList.Count)
        rndStarname = starnameList.Item(roll)
        starnameList.RemoveAt(roll)

        If starnameList.Count = 0 Then starnameList = filegetStarname()
    End Function
    Private Function filegetStarname() As List(Of String)
        Dim starnameList As New List(Of String)

        Try
            Dim sr As New StreamReader(pathStarnamelist)
            Do While sr.Peek <> -1
                Dim line As String = sr.ReadLine
                starnameList.Add(line)
            Loop
        Catch ex As Exception
            bugcatch.alert(Me, "pathStarnamelist invalid")
            For count As Integer = 1 To 20
                starnameList.Add("Star " & count)
            Next
        End Try

        Return starnameList
    End Function


    Public Property numCities As New range(2, 5)
    Private Property planetGovernmentList As New List(Of eGovernment)
    Private Property planetTypeList As New List(Of ePlanetType)
    Public Function rndPlanetGovernment() As eGovernment
        If planetGovernmentList.Count = 0 Then popPlanetGovernmentList()

        Dim roll As Integer = rng.Next(planetGovernmentList.Count)
        rndPlanetGovernment = planetGovernmentList.Item(roll)
        planetGovernmentList.RemoveAt(roll)
    End Function
    Private Sub popPlanetGovernmentList()
        planetGovernmentList.Clear()

        Dim items As Array = System.Enum.GetValues(GetType(eGovernment))
        For Each item As eGovernment In items
            planetGovernmentList.Add(item)
        Next
    End Sub
    Public Function rndPlanetType() As ePlanetType
        If planetTypeList.Count = 0 Then
            popPlanetTypeList()
            size -= 1
        End If

        Dim roll As Integer = rng.Next(planetTypeList.Count)
        rndPlanetType = planetTypeList.Item(roll)
        planetTypeList.RemoveAt(roll)
    End Function
    Private Sub popPlanetTypeList()
        planetTypeList.Clear()

        Dim items As Array = System.Enum.GetValues(GetType(ePlanetType))
        For Each item As ePlanetType In items
            planetTypeList.Add(item)
        Next
    End Sub


    Private Property demandList As New List(Of eGood)
    Public Function rndSupply(_type As ePlanetType, ByRef _city As location) As eGood
        Dim roll As Integer = Int(Rnd() * 5 + 1)
        Select Case _type
            Case ePlanetType.Mining 'do nothing
            Case ePlanetType.Industrial : roll += 10
            Case ePlanetType.Sprawl : roll += 20
            Case ePlanetType.Agrarian : roll += 30
            Case ePlanetType.Research : roll += 40
        End Select
        Return roll
    End Function
    Public Function rndDemand(_type As ePlanetType, ByRef _city As location) As eGood
        If demandList.Count = 0 Then popDemandList()


        'generate clone of demandList using stripDemandList
        'and remove all supply generated by current planetType
        'if templist is now empty, pop demandList before stripping again
        Dim tempList As List(Of eGood) = stripDemandList(_type)
        If tempList.Count = 0 Then
            popDemandList()
            tempList = stripDemandList(_type)
        End If

        Dim roll As Integer = rng.Next(tempList.Count)
        rndDemand = tempList.Item(roll)
        demandList.Remove(rndDemand)
    End Function
    Private Sub popDemandList()
        demandList.Clear()

        Dim items As Array = System.Enum.GetValues(GetType(eGood))
        For Each item As eGood In items
            demandList.Add(item)
        Next
    End Sub
    Private Function stripDemandList(_type As ePlanetType) As List(Of eGood)
        Dim templist As New List(Of eGood)(demandList)
        Select Case _type
            Case ePlanetType.Mining : templist.RemoveAll(Function(a) a >= 1 AndAlso a <= 5)
            Case ePlanetType.Industrial : templist.RemoveAll(Function(a) a >= 11 AndAlso a <= 15)
            Case ePlanetType.Sprawl : templist.RemoveAll(Function(a) a >= 21 AndAlso a <= 25)
            Case ePlanetType.Agrarian : templist.RemoveAll(Function(a) a >= 31 AndAlso a <= 35)
            Case ePlanetType.Research : templist.RemoveAll(Function(a) a >= 41 AndAlso a <= 45)
        End Select
        Return templist
    End Function
End Class