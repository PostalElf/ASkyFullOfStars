Imports System.Xml

Public Class filefetch
    Implements IDisposable
    Private Const filePath As String = "data\"
    Private Const playerFilename As String = "player.xml"
    Private Const starmapFilename As String = "starmap.xml"
    Private Const capitalFilename As String = "capital.xml"


    Public Function getPlayer() As player
        Return New player
    End Function


    Public Function getStarmap(player As player) As starmap
        Dim xmlSettings As New XmlReaderSettings
        Dim starmap As New starmap
        Dim currentStar As star = Nothing
        Dim currentPlanet As planet = Nothing
        Dim currentCity As city = Nothing

        Using reader As XmlReader = XmlReader.Create(filePath & starmapFilename, xmlSettings)
            While reader.Read()
                If reader.IsStartElement = True Then
                    Select Case reader.Name
                        Case "star"
                            Dim star As New star(starmap)
                            star.starname = reader.GetAttribute(0)
                            Dim splitString() As String = Split(reader.GetAttribute(1))
                            star.starXY = New xy(splitString(0), splitString(1))
                            starmap.stars.Add(star)
                            currentStar = star

                        Case "planet"
                            Dim starname As String = reader.GetAttribute(0)
                            Dim planet As New planet(currentStar, reader.GetAttribute(1))
                            planet.government = reader.GetAttribute(2)
                            planet.type = reader.GetAttribute(3)
                            currentStar.planets.Add(planet)
                            currentPlanet = planet

                        Case "planetAsset"
                            Dim assetName As String = reader.GetAttribute(0)
                            Dim assetType As eAsset = reader.GetAttribute(1)
                            Dim assetTTL As Integer = reader.GetAttribute(2)
                            Dim assetIncome As Double = reader.GetAttribute(3)
                            currentPlanet.assets.add(New asset(assetName, assetType, currentPlanet, player, assetTTL, assetIncome))

                        Case "city"
                            Dim starname As String = reader.GetAttribute(0)
                            Dim planetnumber As Integer = reader.GetAttribute(1)
                            Dim city As New city(currentPlanet, reader.GetAttribute(2))
                            city.size = reader.GetAttribute(3)
                            currentPlanet.cities.Add(city)
                            currentCity = city

                        Case "supply"
                            currentCity.supply.Add(reader.GetAttribute(0))

                        Case "demand"
                            currentCity.demand.Add(reader.GetAttribute(0))

                        Case "cityAsset"
                            Dim assetName As String = reader.GetAttribute(0)
                            Dim assetType As eAsset = reader.GetAttribute(1)
                            Dim assetTTL As Integer = reader.GetAttribute(2)
                            Dim assetIncome As Double = reader.GetAttribute(3)
                            currentCity.assets.add(New asset(assetName, assetType, currentPlanet, player, assetTTL, assetIncome))

                    End Select
                End If
            End While
        End Using

        Return starmap
    End Function
    Public Sub writeStarmap(ByRef starmap As starmap)
        Dim xmlSettings As New XmlWriterSettings
        xmlSettings.Indent = True

        Using writer As XmlWriter = XmlWriter.Create(filePath & starmapFilename, xmlSettings)
            writer.WriteStartDocument()
            writer.WriteStartElement("starmap")     'root

            For Each star In starmap.stars
                writer.WriteStartElement("star")
                writer.WriteStartAttribute("name")
                writer.WriteString(star.name)
                writer.WriteStartAttribute("xy")
                writer.WriteString(star.starXY.x & " " & star.starXY.y)
                writer.WriteEndAttribute()

                For Each planet In star.planets
                    writer.WriteStartElement("planet")
                    writer.WriteStartAttribute("starname")
                    writer.WriteString(planet.star.name)
                    writer.WriteStartAttribute("number")
                    writer.WriteString(planet.number)
                    writer.WriteStartAttribute("government")
                    writer.WriteString(planet.government)
                    writer.WriteStartAttribute("type")
                    writer.WriteString(planet.type)
                    writer.WriteEndAttribute()

                    For Each asset In planet.assets.getAssets(eAsset.All)
                        writer.WriteStartElement("planetAsset")
                        writer.WriteStartAttribute("name")
                        writer.WriteString(asset.name)
                        writer.WriteStartAttribute("type")
                        writer.WriteString(asset.type)
                        writer.WriteStartAttribute("ttl")
                        writer.WriteString(asset.ttl)
                        writer.WriteStartAttribute("income")
                        writer.WriteString(asset.income)
                        writer.WriteEndAttribute()
                        writer.WriteEndElement()
                    Next

                    For Each city In planet.cities
                        writer.WriteStartElement("city")
                        writer.WriteStartAttribute("starname")
                        writer.WriteString(city.planet.star.name)
                        writer.WriteStartAttribute("planetnumber")
                        writer.WriteString(city.planet.number)
                        writer.WriteStartAttribute("number")
                        writer.WriteString(city.number)
                        writer.WriteStartAttribute("size")
                        writer.WriteString(city.size)
                        writer.WriteEndAttribute()

                        For Each supply In city.supply
                            writer.WriteStartElement("supply")
                            writer.WriteStartAttribute("type")
                            writer.WriteString(supply)
                            writer.WriteEndAttribute()
                            writer.WriteEndElement()
                        Next

                        For Each demand In city.demand
                            writer.WriteStartElement("demand")
                            writer.WriteStartAttribute("type")
                            writer.WriteString(demand)
                            writer.WriteEndAttribute()
                            writer.WriteEndElement()
                        Next

                        For Each asset In city.assets.getAssets(eAsset.All)
                            writer.WriteStartElement("cityAsset")
                            writer.WriteStartAttribute("name")
                            writer.WriteString(asset.name)
                            writer.WriteStartAttribute("type")
                            writer.WriteString(asset.type)
                            writer.WriteStartAttribute("ttl")
                            writer.WriteString(asset.ttl)
                            writer.WriteStartAttribute("income")
                            writer.WriteString(asset.income)
                            writer.WriteEndAttribute()
                            writer.WriteEndElement()
                        Next

                        writer.WriteEndElement()        'closes city
                    Next

                    writer.WriteEndElement()            'closes planet
                Next

                writer.WriteEndElement()                'closes star
            Next

            writer.WriteEndElement()                'closes root
            writer.WriteEndDocument()
        End Using
    End Sub

    Public Function getCapital(player As player, starmap As starmap) As capital
        Dim xmlSettings As New XmlReaderSettings
        Dim capital As New capital(player, starmap)

        Using reader As XmlReader = XmlReader.Create(filePath & capitalFilename, xmlSettings)
            While reader.Read()
                If reader.IsStartElement = True Then
                    Select Case reader.Name
                        Case "good"
                            Dim type As eGood = reader.GetAttribute(0)
                            Dim source As city = starmap.getCity(reader.GetAttribute(1))
                            Dim dest As city = starmap.getCity(reader.GetAttribute(2))
                            capital.goods.Add(New good(type, source, dest))

                        Case "asset"
                            Dim assetName As String = reader.GetAttribute(0)
                            Dim assetType As eAsset = CInt(reader.GetAttribute(1))
                            Dim assetTTL As Integer = CInt(reader.GetAttribute(2))
                            Dim assetIncome As Double = CDbl(reader.GetAttribute(3))
                            capital.assets.add(New asset(assetName, assetType, capital, player, assetTTL, assetIncome))

                    End Select
                End If
            End While
        End Using

        Return capital
    End Function
    Public Sub writeCapital(ByRef capital As capital)
        Dim xmlSettings As New XmlWriterSettings
        xmlSettings.Indent = True

        Using writer As XmlWriter = XmlWriter.Create(filePath & capitalFilename, xmlSettings)
            writer.WriteStartDocument()
            writer.WriteStartElement("capital")     'root

            For Each good In capital.goods
                writer.WriteStartElement("good")
                writer.WriteStartAttribute("type")
                writer.WriteString(good.type)
                writer.WriteStartAttribute("source")
                Dim source As city = CType(good.source, city)
                writer.WriteString(source.id)
                writer.WriteStartAttribute("destination")
                Dim dest As city = CType(good.destination, city)
                writer.WriteString(dest.id)
                writer.WriteEndAttribute()
                writer.WriteEndElement()
            Next

            For Each asset In capital.assets.getAssets(eAsset.All)
                writer.WriteStartElement("asset")
                writer.WriteStartAttribute("name")
                writer.WriteString(asset.name)
                writer.WriteStartAttribute("type")
                writer.WriteString(asset.type)
                writer.WriteStartAttribute("ttl")
                writer.WriteString(asset.ttl)
                writer.WriteStartAttribute("income")
                writer.WriteString(asset.income)
                writer.WriteEndAttribute()
                writer.WriteEndElement()
            Next

            writer.WriteEndElement()              'closes root
            writer.WriteEndDocument()
        End Using
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
