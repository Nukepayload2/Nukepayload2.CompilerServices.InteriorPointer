Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Nukepayload2.CompilerServices.Tests
Imports Nukepayload2.CompilerServices.Unsafe

<TestClass>
Public Class TestInteriorPointer

    <TestMethod>
    Sub TestUnsafeByRefToPtrToByRef()
        Dim red = MyColor.Red
        Dim green = MyColor.Green
        Dim blue = MyColor.Blue
        red.IsPArgb = True
        TransparencyHalfDown(PredefinedColors.Red, red, green, blue)
        Assert.AreEqual(CByte(&H7F), red.A)
        Assert.AreEqual(CByte(&H7F), red.R)
        Assert.AreEqual(New Byte, red.G)
        Assert.AreEqual(New Byte, red.B)
        TransparencyHalfDown(PredefinedColors.Green, red, green, blue)
        Assert.AreEqual(CByte(&H7F), green.A)
        Assert.AreEqual(New Byte, green.R)
        Assert.AreEqual(CByte(&HFF), green.G)
        Assert.AreEqual(New Byte, green.B)
    End Sub

    <TestMethod>
    Sub TestUnsafeByRefToPtrToByRefTyped()
        Dim red = MyColor.Red
        Dim green = MyColor.Green
        Dim blue = MyColor.Blue
        red.IsPArgb = True
        TransparencyHalfDown2(PredefinedColors.Red, red, green, blue)
        Assert.AreEqual(CByte(&H7F), red.A)
        Assert.AreEqual(CByte(&H7F), red.R)
        Assert.AreEqual(New Byte, red.G)
        Assert.AreEqual(New Byte, red.B)
        TransparencyHalfDown2(PredefinedColors.Green, red, green, blue)
        Assert.AreEqual(CByte(&H7F), green.A)
        Assert.AreEqual(New Byte, green.R)
        Assert.AreEqual(CByte(&HFF), green.G)
        Assert.AreEqual(New Byte, green.B)
    End Sub

    <TestMethod>
    Sub TestAddRef()
        Dim red = MyColor.Red
        Dim pArgb = red.A.UnsafeByRefToTypedPtr
        Assert.AreEqual(red.A, pArgb.UnsafePtrToByRef)
        pArgb += 1
        Assert.AreEqual(red.R, pArgb.UnsafePtrToByRef)
        pArgb += 1
        Assert.AreEqual(red.G, pArgb.UnsafePtrToByRef)
        pArgb += 1
        Assert.AreEqual(red.B, pArgb.UnsafePtrToByRef)
    End Sub

    <TestMethod>
    Sub TestAddRef2()
        Dim red = MyColor.Red
        Dim blue = MyColor.Blue
        Dim group As New MyColorGroup With {
            .Color1 = red,
            .Color2 = blue
        }
        Dim pColor = group.Color1.UnsafeByRefToTypedPtr
        Assert.AreEqual(red.A, pColor.UnsafePtrToByRef.A)
        Assert.AreEqual(red.R, pColor.UnsafePtrToByRef.R)
        Assert.AreEqual(red.G, pColor.UnsafePtrToByRef.G)
        Assert.AreEqual(red.B, pColor.UnsafePtrToByRef.B)
        pColor += 1
        Assert.AreEqual(blue.A, pColor.UnsafePtrToByRef.A)
        Assert.AreEqual(blue.R, pColor.UnsafePtrToByRef.R)
        Assert.AreEqual(blue.G, pColor.UnsafePtrToByRef.G)
        Assert.AreEqual(blue.B, pColor.UnsafePtrToByRef.B)
    End Sub

    Private Sub TransparencyHalfDown(colorType As PredefinedColors,
                                     ByRef red As MyColor,
                                     ByRef green As MyColor,
                                     ByRef blue As MyColor)
        Dim pColor As InteriorPointer
        Select Case colorType
            Case PredefinedColors.Red
                pColor = red.UnsafeByRefToPtr
            Case PredefinedColors.Green
                pColor = green.UnsafeByRefToPtr
            Case PredefinedColors.Blue
                pColor = blue.UnsafeByRefToPtr
            Case Else
                Throw New ArgumentOutOfRangeException(NameOf(colorType))
        End Select

        Dim targetValue As MyColor = pColor.UnsafePtrToByRef(Of MyColor)
        targetValue.A \= 2
        If targetValue.IsPArgb Then
            targetValue.R \= 2
            targetValue.G \= 2
            targetValue.B \= 2
        End If
        pColor.UnsafePtrToByRef(Of MyColor) = targetValue
    End Sub

    Private Sub TransparencyHalfDown2(colorType As PredefinedColors,
                                     ByRef red As MyColor,
                                     ByRef green As MyColor,
                                     ByRef blue As MyColor)
        Dim pColor As InteriorPointer(Of MyColor)
        Select Case colorType
            Case PredefinedColors.Red
                pColor = red.UnsafeByRefToTypedPtr
            Case PredefinedColors.Green
                pColor = green.UnsafeByRefToTypedPtr
            Case PredefinedColors.Blue
                pColor = blue.UnsafeByRefToTypedPtr
            Case Else
                Throw New ArgumentOutOfRangeException(NameOf(colorType))
        End Select

        Dim targetValue As MyColor = pColor.UnsafePtrToByRef
        targetValue.A \= 2
        If targetValue.IsPArgb Then
            targetValue.R \= 2
            targetValue.G \= 2
            targetValue.B \= 2
        End If
        pColor.UnsafePtrToByRef = targetValue
    End Sub
End Class

Public Enum PredefinedColors
    Red
    Green
    Blue
End Enum

Public Structure MyColor
    Dim A, R, G, B As Byte
    Dim IsPArgb As Boolean

    Public Shared ReadOnly Red As New MyColor With {.A = &HFF, .R = &HFF}
    Public Shared ReadOnly Green As New MyColor With {.A = &HFF, .G = &HFF}
    Public Shared ReadOnly Blue As New MyColor With {.A = &HFF, .B = &HFF}
End Structure

Public Structure MyColorGroup
    Dim Color1, Color2 As MyColor
End Structure

Public Structure MyColorGroup4
    Dim Color1, Color2, Color3, Color4 As MyColor
End Structure