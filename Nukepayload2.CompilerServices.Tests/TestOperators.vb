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
        Assert.IsTrue(colors.Color1.UnsafeByRefToTypedPtr = colors.Color1.UnsafeByRefToTypedPtr)
        Assert.IsFalse(colors.Color1.UnsafeByRefToTypedPtr <> colors.Color1.UnsafeByRefToTypedPtr)
        Assert.IsTrue(colors.Color2.UnsafeByRefToTypedPtr <> colors.Color1.UnsafeByRefToTypedPtr)
        Assert.IsFalse(colors.Color1.UnsafeByRefToTypedPtr = colors.Color2.UnsafeByRefToTypedPtr)
    End Sub
End Class
