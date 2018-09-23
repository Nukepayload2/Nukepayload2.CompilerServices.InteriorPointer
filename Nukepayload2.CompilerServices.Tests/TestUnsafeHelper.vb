Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class TestUnsafeHelper
    <TestMethod>
    Public Sub TestSizeOf()
        Assert.AreEqual(1, Unsafe.UnsafeHelper.SizeOf(Of Boolean)())
        Assert.AreEqual(1, Unsafe.UnsafeHelper.SizeOf(Of Byte)())
        Assert.AreEqual(1, Unsafe.UnsafeHelper.SizeOf(Of SByte)())
        Assert.AreEqual(2, Unsafe.UnsafeHelper.SizeOf(Of Char)())
        Assert.AreEqual(2, Unsafe.UnsafeHelper.SizeOf(Of Short)())
        Assert.AreEqual(2, Unsafe.UnsafeHelper.SizeOf(Of UShort)())
        Assert.AreEqual(4, Unsafe.UnsafeHelper.SizeOf(Of Single)())
        Assert.AreEqual(4, Unsafe.UnsafeHelper.SizeOf(Of Integer)())
        Assert.AreEqual(4, Unsafe.UnsafeHelper.SizeOf(Of UInteger)())
        Assert.AreEqual(8, Unsafe.UnsafeHelper.SizeOf(Of Double)())
        Assert.AreEqual(8, Unsafe.UnsafeHelper.SizeOf(Of Long)())
        Assert.AreEqual(8, Unsafe.UnsafeHelper.SizeOf(Of ULong)())
        Assert.AreEqual(8, Unsafe.UnsafeHelper.SizeOf(Of Date)())
        Assert.AreEqual(8, Unsafe.UnsafeHelper.SizeOf(Of TimeSpan)())
        Assert.AreEqual(16, Unsafe.UnsafeHelper.SizeOf(Of Decimal)())
        Assert.AreEqual(IntPtr.Size, Unsafe.UnsafeHelper.SizeOf(Of IntPtr)())
        Assert.AreEqual(IntPtr.Size, Unsafe.UnsafeHelper.SizeOf(Of UIntPtr)())
        Assert.AreEqual(IntPtr.Size, Unsafe.UnsafeHelper.SizeOf(Of Object)())
    End Sub
End Class
