Imports System.Runtime.InteropServices
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Nukepayload2.CompilerServices.Unsafe

<TestClass>
Public Class TestUnsafeHelper
    <TestMethod>
    Public Sub TestSizeOf()
        Assert.AreEqual(1, SizeOf(Of Boolean)())
        Assert.AreEqual(1, SizeOf(Of Byte)())
        Assert.AreEqual(1, SizeOf(Of SByte)())
        Assert.AreEqual(2, SizeOf(Of Char)())
        Assert.AreEqual(2, SizeOf(Of Short)())
        Assert.AreEqual(2, SizeOf(Of UShort)())
        Assert.AreEqual(4, SizeOf(Of Single)())
        Assert.AreEqual(4, SizeOf(Of Integer)())
        Assert.AreEqual(4, SizeOf(Of UInteger)())
        Assert.AreEqual(8, SizeOf(Of Double)())
        Assert.AreEqual(8, SizeOf(Of Long)())
        Assert.AreEqual(8, SizeOf(Of ULong)())
        Assert.AreEqual(8, SizeOf(Of Date)())
        Assert.AreEqual(8, SizeOf(Of TimeSpan)())
        Assert.AreEqual(16, SizeOf(Of Decimal)())
        Assert.AreEqual(IntPtr.Size, SizeOf(Of IntPtr)())
        Assert.AreEqual(IntPtr.Size, SizeOf(Of UIntPtr)())
        Assert.AreEqual(IntPtr.Size, SizeOf(Of Object)())
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

    <TestMethod>
    Public Sub TestToString()
        Dim str = "https://github.com/Nukepayload2"
        Dim arr = (str + vbNullChar).ToCharArray
#Disable Warning BC42351
        ' Local variable is read-only and its type is a structure. 
        ' Invoking its members Or passing it ByRef does Not change its content And might lead to unexpected results. 
        ' Consider declaring this variable outside of the 'Using' block.
        Using pinPtr = Fixed(arr)
#Enable Warning
            Dim strPtr = pinPtr.Pointer
            strPtr += 8
            Dim newStr = strPtr.UnsafeToString
            Assert.AreEqual(str.Substring(8), newStr)
        End Using
    End Sub

    <TestMethod>
    Public Sub TestIncrement()
        Dim p1 As New InteriorPointer(Of Char)
        Dim p2 As New InteriorPointer(Of Char)
        p1 = Nothing
        p2 = p1.GetAndIncrement
        Assert.IsFalse(p1.IsZero)
        Assert.IsTrue(p2.IsZero)
        p1 = Nothing
        p2 = p1.IncrementAndGet
        Assert.IsFalse(p1.IsZero)
        Assert.IsFalse(p2.IsZero)
    End Sub

    <TestMethod>
    Public Sub TestRead()
        Dim str = "https://github.com/Nukepayload2"
        Dim arr = (str + vbNullChar).ToCharArray
        Dim strPtr As InteriorPointer(Of Char) = Marshal.UnsafeAddrOfPinnedArrayElement(arr, 0)
        strPtr += 8
        Dim g = strPtr.UnmanagedItem
        Assert.AreEqual("g"c, g)
    End Sub

    <TestMethod>
    Public Sub TestWrite()
        Dim str = "https://github.com/Nukepayload2"
        Dim arr = (str + vbNullChar).ToCharArray
        Dim strPtr As InteriorPointer(Of Char) = Marshal.UnsafeAddrOfPinnedArrayElement(arr, 0)
        strPtr += 8
        strPtr.UnmanagedItem = "G"c
        Assert.AreEqual("G"c, strPtr.UnmanagedItem)
    End Sub

    <TestMethod>
    Public Sub TestStrPtr()
        Dim str = "https://github.com/Nukepayload2"
#Disable Warning BC42351
        ' Local variable is read-only and its type is a structure. 
        ' Invoking its members Or passing it ByRef does Not change its content And might lead to unexpected results. 
        ' Consider declaring this variable outside of the 'Using' block.
        Using hStr = StrPtr(str)
#Enable Warning
            Dim pStr = hStr.Pointer
            Assert.AreEqual("h"c, pStr.GetAndIncrement.UnmanagedItem)
            Assert.AreEqual("t"c, pStr.GetAndIncrement.UnmanagedItem)
            Assert.AreEqual("t"c, pStr.GetAndIncrement.UnmanagedItem)
            Assert.AreEqual("p"c, pStr.GetAndIncrement.UnmanagedItem)
            Assert.AreEqual("s"c, pStr.GetAndIncrement.UnmanagedItem)
            Assert.AreEqual(":"c, pStr.GetAndIncrement.UnmanagedItem)
            Assert.AreEqual("/"c, pStr.GetAndIncrement.UnmanagedItem)
            Assert.AreEqual("/"c, pStr.GetAndIncrement.UnmanagedItem)
            Assert.AreEqual("g"c, pStr.GetAndIncrement.UnmanagedItem)
            Assert.AreEqual("i"c, pStr.GetAndIncrement.UnmanagedItem)
            Assert.AreEqual("t"c, pStr.GetAndIncrement.UnmanagedItem)
            Assert.AreEqual("h"c, pStr.GetAndIncrement.UnmanagedItem)
            Assert.AreEqual("u"c, pStr.GetAndIncrement.UnmanagedItem)
            Assert.AreEqual("b"c, pStr.GetAndIncrement.UnmanagedItem)
        End Using
    End Sub
End Class
