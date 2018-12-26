Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Nukepayload2.CompilerServices.Unsafe

<TestClass>
Public Class TestUnsafeConversions

    <TestMethod>
    Sub TestUnsafeReinterpretCast()
        Dim priObj As New ClassWithPrivates(12345)
        Dim pubObj = priObj.UnsafeReinterpretCast(Of ClassWithPublics)
        Assert.AreEqual(12345, pubObj._hiddenValue)
        pubObj._hiddenReadOnlyValue = 678
        Assert.AreEqual(678, priObj.HiddenReadOnlyValue)
    End Sub

    <TestMethod>
    Sub TestCastWithInteriorPtr()
        Dim priObj As New ClassWithPrivates(12345)
        Dim pubObj = CType(priObj.UnsafeByRefToPtr, InteriorPointer(Of ClassWithPublics)).UnsafePtrToByRef
        Assert.AreEqual(12345, pubObj._hiddenValue)
        pubObj._hiddenReadOnlyValue = 678
        Assert.AreEqual(678, priObj.HiddenReadOnlyValue)
    End Sub

    <TestMethod>
    Sub TestStaticCastByte()
        Dim b As Byte = 255
        Dim sb As SByte
        b.UnsafeStaticCast(sb)
        Assert.AreEqual(CSByte(-1), sb)
    End Sub

    <TestMethod>
    Sub TestStaticCastSByte()
        Dim sb As SByte = -1
        Dim b As Byte
        sb.UnsafeStaticCast(b)
        Assert.AreEqual(CByte(255), b)
    End Sub

    <TestMethod>
    Sub TestStaticCastShort()
        Dim us As UShort = &HFFFF
        Dim s As Short
        us.UnsafeStaticCast(s)
        Assert.AreEqual(CShort(-1), s)
    End Sub

    <TestMethod>
    Sub TestStaticCastUShort()
        Dim s As Short = -1
        Dim us As UShort
        s.UnsafeStaticCast(us)
        Assert.AreEqual(CUShort(&HFFFF), us)
    End Sub

    <TestMethod>
    Sub TestStaticCastInteger()
        Dim us As UInteger = &HFFFFFFFFUI
        Dim s As Integer
        us.UnsafeStaticCast(s)
        Assert.AreEqual(-1, s)
    End Sub

    <TestMethod>
    Sub TestStaticCastUInteger()
        Dim s As Integer = -1
        Dim us As UInteger
        s.UnsafeStaticCast(us)
        Assert.AreEqual(&HFFFFFFFFUI, us)
    End Sub

    <TestMethod>
    Sub TestStaticCastLong()
        Dim us As ULong = &HFFFFFFFFFFFFFFFFUL
        Dim s As Long
        us.UnsafeStaticCast(s)
        Assert.AreEqual(-1L, s)
    End Sub

    <TestMethod>
    Sub TestStaticCastULong()
        Dim s As Long = -1
        Dim us As ULong
        s.UnsafeStaticCast(us)
        Assert.AreEqual(&HFFFFFFFFFFFFFFFFUL, us)
    End Sub

    <TestMethod>
    Sub TestStaticCastSingle()
        Dim us As Double = 1.2345
        Dim s As Single
        us.UnsafeStaticCast(s)
        Assert.AreEqual(1.2345F, s, 0.0000001)
    End Sub

    <TestMethod>
    Sub TestStaticCastDouble()
        Dim us As Single = 1.2345F
        Dim s As Double
        us.UnsafeStaticCast(s)
        Assert.AreEqual(1.2345, s, 0.0000001)
    End Sub

    <TestMethod>
    Sub TestStaticCastIntPtr()
        If IntPtr.Size = 8 Then
            Dim uptr As New UIntPtr(&HFFFFFFFFFFFFFFFFUL)
            Dim ptr As IntPtr
            uptr.UnsafeStaticCast(ptr)
            Assert.AreEqual(New IntPtr(-1L), ptr)
        ElseIf IntPtr.Size = 4 Then
            Dim uptr As New UIntPtr(&HFFFFFFFFUI)
            Dim ptr As IntPtr
            uptr.UnsafeStaticCast(ptr)
            Assert.AreEqual(New IntPtr(-1), ptr)
        End If
    End Sub

    <TestMethod>
    Sub TestStaticCastUIntPtr()
        If IntPtr.Size = 8 Then
            Dim ptr As New IntPtr(-1L)
            Dim uptr As UIntPtr
            ptr.UnsafeStaticCast(uptr)
            Assert.AreEqual(New UIntPtr(&HFFFFFFFFFFFFFFFFUL), uptr)
        ElseIf IntPtr.Size = 4 Then
            Dim ptr As New IntPtr(-1)
            Dim uptr As UIntPtr
            ptr.UnsafeStaticCast(uptr)
            Assert.AreEqual(New UIntPtr(&HFFFFFFFFUI), uptr)
        End If
    End Sub

    <TestMethod>
    Sub TestStaticCastEnumHasFlag()
        Dim enumValue = TestEnum.Flag3 Or TestEnum.Flag1 Or TestEnum.Flag6
        Dim enumLongValue As Long
        enumValue.UnsafeStaticCast(enumLongValue)
        Assert.AreEqual(CLng(enumValue), enumLongValue)
        Assert.IsTrue(enumLongValue And TestEnum.Flag1)
        Assert.IsFalse(enumLongValue And TestEnum.Flag2)
        Assert.IsTrue(enumLongValue And TestEnum.Flag3)
        Assert.IsFalse(enumLongValue And TestEnum.Flag4)
        Assert.IsFalse(enumLongValue And TestEnum.Flag5)
        Assert.IsTrue(enumLongValue And TestEnum.Flag6)
    End Sub

    <Flags>
    Enum TestEnum
        None
        Flag1 = 1
        Flag2 = 2
        Flag3 = 4
        Flag4 = 8
        Flag5 = 16
        Flag6 = 32
    End Enum
End Class

Public Class ClassWithPrivates
    Private _hiddenValue As Integer
    Private ReadOnly _hiddenReadOnlyValue As Integer

    Public Sub New(hiddenValue As Integer)
        _hiddenValue = hiddenValue
    End Sub

    Public ReadOnly Property HiddenReadOnlyValue As Integer
        Get
            Return _hiddenReadOnlyValue
        End Get
    End Property
End Class

Public Class ClassWithPublics
    Public _hiddenValue As Integer = 123
    Public _hiddenReadOnlyValue As Integer

    Public ReadOnly Property HiddenReadOnlyValue As Integer
        Get
            Return _hiddenReadOnlyValue
        End Get
    End Property
End Class
