Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Nukepayload2.CompilerServices.Unsafe

<TestClass>
Public Class TestUnsafeHelper
    <TestMethod>
    Public Sub TestSizeOf()
        Assert.AreEqual(1, UnsafeOperators.SizeOf(Of Boolean)())
        Assert.AreEqual(1, UnsafeOperators.SizeOf(Of Byte)())
        Assert.AreEqual(1, UnsafeOperators.SizeOf(Of SByte)())
        Assert.AreEqual(2, UnsafeOperators.SizeOf(Of Char)())
        Assert.AreEqual(2, UnsafeOperators.SizeOf(Of Short)())
        Assert.AreEqual(2, UnsafeOperators.SizeOf(Of UShort)())
        Assert.AreEqual(4, UnsafeOperators.SizeOf(Of Single)())
        Assert.AreEqual(4, UnsafeOperators.SizeOf(Of Integer)())
        Assert.AreEqual(4, UnsafeOperators.SizeOf(Of UInteger)())
        Assert.AreEqual(8, UnsafeOperators.SizeOf(Of Double)())
        Assert.AreEqual(8, UnsafeOperators.SizeOf(Of Long)())
        Assert.AreEqual(8, UnsafeOperators.SizeOf(Of ULong)())
        Assert.AreEqual(8, UnsafeOperators.SizeOf(Of Date)())
        Assert.AreEqual(8, UnsafeOperators.SizeOf(Of TimeSpan)())
        Assert.AreEqual(16, UnsafeOperators.SizeOf(Of Decimal)())
        Assert.AreEqual(IntPtr.Size, UnsafeOperators.SizeOf(Of IntPtr)())
        Assert.AreEqual(IntPtr.Size, UnsafeOperators.SizeOf(Of UIntPtr)())
        Assert.AreEqual(IntPtr.Size, UnsafeOperators.SizeOf(Of Object)())
    End Sub

    <TestMethod>
    Public Sub TestCompareOperators()
        Dim colors As New MyColorGroup
        AssertLtGt(colors)
        AssertEqNe(colors)
        AssertLeGe(colors)
    End Sub

    Private Shared Sub AssertEqNe(colors As MyColorGroup)
        Assert.IsTrue(colors.Color1.UnsafeByRefToTypedPtr = colors.Color1.UnsafeByRefToTypedPtr)
        Assert.IsFalse(colors.Color1.UnsafeByRefToTypedPtr <> colors.Color1.UnsafeByRefToTypedPtr)
        Assert.IsTrue(colors.Color2.UnsafeByRefToTypedPtr <> colors.Color1.UnsafeByRefToTypedPtr)
        Assert.IsFalse(colors.Color1.UnsafeByRefToTypedPtr = colors.Color2.UnsafeByRefToTypedPtr)
    End Sub

    Private Shared Sub AssertLtGt(colors As MyColorGroup)
        Dim v1 = colors.Color1.UnsafeByRefToPtr < colors.Color2.UnsafeByRefToPtr
        Dim v2 = colors.Color2.UnsafeByRefToPtr > colors.Color1.UnsafeByRefToPtr
        Dim v3 = colors.Color1.UnsafeByRefToTypedPtr < colors.Color2.UnsafeByRefToTypedPtr
        Dim v4 = colors.Color2.UnsafeByRefToTypedPtr > colors.Color1.UnsafeByRefToTypedPtr
        Dim v1x = colors.Color1.UnsafeByRefToPtr > colors.Color2.UnsafeByRefToPtr
        Dim v2x = colors.Color2.UnsafeByRefToPtr < colors.Color1.UnsafeByRefToPtr
        Dim v3x = colors.Color1.UnsafeByRefToTypedPtr > colors.Color2.UnsafeByRefToTypedPtr
        Dim v4x = colors.Color2.UnsafeByRefToTypedPtr < colors.Color1.UnsafeByRefToTypedPtr
        Assert.IsTrue(v1 And v2 And v3 And v4)
        Assert.IsFalse(v1x Or v2x Or v3x Or v4x)
    End Sub

    Private Shared Sub AssertLeGe(colors As MyColorGroup)
        Dim v1 = colors.Color1.UnsafeByRefToPtr <= colors.Color2.UnsafeByRefToPtr
        Dim v2 = colors.Color2.UnsafeByRefToPtr >= colors.Color1.UnsafeByRefToPtr
        Dim v3 = colors.Color1.UnsafeByRefToTypedPtr <= colors.Color2.UnsafeByRefToTypedPtr
        Dim v4 = colors.Color2.UnsafeByRefToTypedPtr >= colors.Color1.UnsafeByRefToTypedPtr
        Dim v1x = colors.Color1.UnsafeByRefToPtr >= colors.Color2.UnsafeByRefToPtr
        Dim v2x = colors.Color2.UnsafeByRefToPtr <= colors.Color1.UnsafeByRefToPtr
        Dim v3x = colors.Color1.UnsafeByRefToTypedPtr >= colors.Color2.UnsafeByRefToTypedPtr
        Dim v4x = colors.Color2.UnsafeByRefToTypedPtr <= colors.Color1.UnsafeByRefToTypedPtr
        Assert.IsTrue(v1 And v2 And v3 And v4)
        Assert.IsFalse(v1x Or v2x Or v3x Or v4x)
        Assert.IsTrue(colors.Color1.UnsafeByRefToTypedPtr <= colors.Color1.UnsafeByRefToTypedPtr)
        Assert.IsTrue(colors.Color1.UnsafeByRefToTypedPtr >= colors.Color1.UnsafeByRefToTypedPtr)
        Assert.IsFalse(colors.Color2.UnsafeByRefToTypedPtr <= colors.Color1.UnsafeByRefToTypedPtr)
        Assert.IsFalse(colors.Color1.UnsafeByRefToTypedPtr >= colors.Color2.UnsafeByRefToTypedPtr)
    End Sub

    <TestMethod>
    Public Sub TestAssign()
        Dim red = MyColor.Red
        Dim green = MyColor.Green
        Dim blue = MyColor.Blue
        Dim group As New MyColorGroup4 With {
            .Color1 = blue,
            .Color2 = blue,
            .Color3 = blue,
            .Color4 = red
        }
        Dim pStart = group.Color1.UnsafeByRefToTypedPtr
        Dim pEnd = group.Color4.UnsafeByRefToTypedPtr
        Dim pCurrent = pStart
        Do
            pCurrent.UnsafePtrToByRef = green
        Loop While pCurrent.Assign(pCurrent + 1) < pEnd
        Assert.AreEqual(green, group.Color1)
        Assert.AreEqual(green, group.Color2)
        Assert.AreEqual(green, group.Color3)
        Assert.AreEqual(red, group.Color4)
    End Sub

End Class
