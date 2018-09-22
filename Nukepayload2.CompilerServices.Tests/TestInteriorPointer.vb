Imports Microsoft.VisualStudio.TestTools.UnitTesting
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
        ' 统一复制回去，不需要再写个 Select Case 语句了。
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
        ' 统一复制回去，不需要再写个 Select Case 语句了。
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