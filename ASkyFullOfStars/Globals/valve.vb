Option Strict On
Option Explicit On

Module valve
    Public Const vbSpace As String = "   "
    Public rng As New Random

    Public Function constrain(value As Integer, Optional minValue As Integer = 1, Optional maxValue As Integer = 100) As Integer
        Dim total As Integer = value
        If total < minValue Then total = minValue
        If total > maxValue Then total = maxValue
        Return total
    End Function

    Public Function circular(value As Integer, Optional minValue As Integer = 1, Optional maxValue As Integer = 4) As Integer
        Dim total As Integer = value
        While total < minValue OrElse total > maxValue
            If total < minValue Then total += maxValue
            If total > maxValue Then total -= maxValue
        End While
        Return total
    End Function

    Public Function sign(value As Decimal) As String
        If value < 0 Then Return "" Else Return "+"
    End Function

    Public Function signAndValue(value As Decimal) As String
        Return sign(value) & value
    End Function

    Public Function coinFlip() As Boolean
        Randomize()
        If Int(Rnd() * 2) + 1 = 1 Then Return True Else Return False
    End Function

    Public Function getLiteral(value As Integer, Optional sigDigits As Integer = 2) As String
        Dim str As String = ""
        For n = 1 To sigDigits
            str = str & "0"
        Next
        str = str & value

        Dim characters() As Char = StrReverse(str).ToCharArray
        str = Nothing
        For n = 0 To sigDigits - 1
            str = str & characters(n)
        Next

        Return StrReverse(str)
    End Function

    Public Function romanNumverter(number As Integer) As String
        Select Case number
            Case 0 : Return "0"
            Case 1 : Return "I"
            Case 2 : Return "II"
            Case 3 : Return "III"
            Case 4 : Return "IV"
            Case 5 : Return "V"
            Case 6 : Return "VI"
            Case 7 : Return "VII"
            Case 8 : Return "VIII"
            Case 9 : Return "IX"
            Case 10 : Return "X"
            Case 11 : Return "XI"
            Case 12 : Return "XII"
            Case Else : Return "XIII"
        End Select
    End Function

    Public Function alphaNumverter(number As Integer) As String
        Select Case number
            Case 0 : Return "Z"
            Case 1 : Return "A"
            Case 2 : Return "B"
            Case 3 : Return "C"
            Case 4 : Return "D"
            Case 5 : Return "E"
            Case 6 : Return "F"
            Case 7 : Return "G"
            Case 8 : Return "H"
            Case 9 : Return "I"
            Case 10 : Return "J"
            Case 11 : Return "K"
            Case 12 : Return "L"
            Case 13 : Return "M"
            Case Else : Return "N"
        End Select
    End Function
End Module


Public Class range
    Public Property min As Integer
    Public Property max As Integer

    Public Sub New(_min As Integer, _max As Integer)
        min = _min
        max = _max
    End Sub
    Public Overrides Function ToString() As String
        If min = max Then Return CStr(min) Else Return min & "-" & max
    End Function

    Public Function roll() As Integer
        Return rng.Next(min, max + 1)
    End Function
    Public Sub add(newRange As range)
        min += newRange.min
        max += newRange.max
    End Sub
End Class


