Imports System.Xml

Public Class filefetch
    Implements IDisposable
    Private Const filePath As String = "data\"
    Private Const playerFilename As String = "player.xml"
    Private Const starmapFilename As String = "starmap.xml"
    Private Const capitalFilename As String = "capital.xml"


    Public Function getPlayer() As player
        Return New player("Not Tubby")
    End Function


    Public Function getStarmap(player As player) As starmap
        Dim xmlSettings As New XmlReaderSettings
        Dim starmap As New starmap
        Dim currentStar As star = Nothing
        Dim currentPlanet As planet = Nothing
        Dim currentCity As city = Nothing
        Dim currentAsset As asset = Nothing

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
                            currentAsset = getAsset(reader, currentPlanet, player)
                            currentPlanet.assets.add(currentAsset)

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
                            currentAsset = getAsset(reader, currentCity, player)
                            currentCity.assets.add(currentAsset)

                        Case "assetOnExpire"
                            Dim onExpire As asset = getAsset(reader, currentAsset.location, currentAsset.owner)
                            currentAsset.onExpire = onExpire
                            currentAsset = onExpire

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
                        writeAsset(writer, asset, "planetAsset")
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
                            writeAsset(writer, asset, "cityAsset")
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
                            capital.assets.add(getAsset(reader, capital, player))

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
                writeAsset(writer, asset, "asset")
            Next

            writer.WriteEndElement()              'closes root
            writer.WriteEndDocument()
        End Using
    End Sub

    Private Function getAsset(ByRef reader As XmlReader, ByRef location As location, ByRef player As player) As asset
        Dim assetName As String = reader.GetAttribute(seq)
        Dim assetType As eAsset = CInt(reader.GetAttribute(seq))
        Dim assetTTL As Integer = CInt(reader.GetAttribute(seq))
        Dim assetIncome As Double = CDbl(reader.GetAttribute(seq))

        Select Case assetType
            Case eAsset.Debuff
                getAsset = New debuffAsset(assetName, location, player, assetTTL, assetIncome)
            Case eAsset.Infrastructure
                getAsset = New infrastructureAsset(assetName, location, player, assetTTL, assetIncome)
            Case eAsset.Investment
                Dim requiredSupply As eGood = reader.GetAttribute(seq)
                getAsset = New investmentAsset(assetName, location, player, assetTTL, assetIncome, requiredSupply, Nothing)
            Case eAsset.Military
                Dim unitType As eUnit = reader.GetAttribute(seq)
                Dim unitPower As Double = CDbl(reader.GetAttribute(seq))
                getAsset = New militaryAsset(assetName, location, player, assetTTL, assetIncome, unitType, unitPower)
            Case eAsset.Production
                Dim supply As eGood = reader.GetAttribute(seq)
                Dim demand As eGood = reader.GetAttribute(seq)
                getAsset = New productionAsset(assetName, location, player, assetIncome, demand, supply)
            Case eAsset.Provocateur
                Dim min As Integer = reader.GetAttribute(seq)
                Dim max As Integer = reader.GetAttribute(seq)
                getAsset = New provocateurAsset(assetName, location, player, assetTTL, assetIncome, New range(min, max))
            Case Else : getAsset = Nothing
        End Select

        seqValue = 0
    End Function
    Private Sub writeAsset(ByRef writer As XmlWriter, ByRef asset As asset, assetName As String)
        writer.WriteStartElement(assetName)
        writer.WriteStartAttribute("name")
        writer.WriteString(asset.name)
        writer.WriteStartAttribute("type")
        writer.WriteString(asset.type)
        writer.WriteStartAttribute("ttl")
        writer.WriteString(asset.ttl)
        writer.WriteStartAttribute("income")
        writer.WriteString(asset.income)

        Select Case asset.type
            Case eAsset.Debuff                  'do nothing
            Case eAsset.Infrastructure          'do nothing
            Case eAsset.Investment
                Dim invAsset As investmentAsset = CType(asset, investmentAsset)
                writer.WriteStartAttribute("requiredSupply")
                writer.WriteString(invAsset.requiredSupply)
            Case eAsset.Military
                Dim milasset As militaryAsset = CType(asset, militaryAsset)
                writer.WriteStartAttribute("unitType")
                writer.WriteString(milasset.unitType)
                writer.WriteStartAttribute("unitPower")
                writer.WriteString(milasset.unitPower)
            Case eAsset.Production
                Dim prodAsset As productionAsset = CType(asset, productionAsset)
                writer.WriteStartAttribute("supply")
                writer.WriteString(prodAsset.supply)
                writer.WriteStartAttribute("demand")
                writer.WriteString(prodAsset.demand)
            Case eAsset.Provocateur
                Dim provAsset As provocateurAsset = CType(asset, provocateurAsset)
                writer.WriteStartAttribute("min")
                writer.WriteString(provAsset.chaosIncome.min)
                writer.WriteStartAttribute("max")
                writer.WriteString(provAsset.chaosIncome.max)
        End Select

        writer.WriteEndAttribute()

        If asset.onExpire Is Nothing = False Then writeAsset(writer, asset.onExpire, "assetOnExpire")

        writer.WriteEndElement()
    End Sub
    Private Property seqValue As Integer = 0
    Private Function seq() As Integer
        seq = seqValue
        seqValue += 1
    End Function

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
