Imports System.Xml

Public Class filefetch
    Private Const filePath As String = "data\"
    Private Const starmapFilename As String = "starmap.xml"

    Public Function getStarmap() As starmap
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
                            starmap.stars.Add(star)
                            currentStar = star

                        Case "planet"
                            Dim starname As String = reader.GetAttribute(0)
                            Dim planet As New planet(currentStar, reader.GetAttribute(1))
                            planet.government = reader.GetAttribute(2)
                            planet.type = reader.GetAttribute(3)
                            currentStar.planets.Add(planet)
                            currentPlanet = planet

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

End Class
