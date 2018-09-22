Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Nukepayload2.CompilerServices.Unsafe

<TestClass>
Public Class TestUnsafeConversions

    <TestMethod>
    Sub TestUnsafeStaticCast()
        Dim priObj As New ClassWithPrivates(12345)
        Dim pubObj = priObj.UnsafeStaticCast(Of ClassWithPublics)
        Assert.AreEqual(12345, pubObj._hiddenValue)
        pubObj._hiddenReadOnlyValue = 678
        Assert.AreEqual(678, priObj.HiddenReadOnlyValue)
    End Sub

    <TestMethod>
    Sub TestUnsafeStaticCastWithInteriorPtr()
        Dim priObj As New ClassWithPrivates(12345)
        Dim pubObj = CType(priObj.UnsafeByRefToPtr, InteriorPointer(Of ClassWithPublics)).UnsafePtrToByRef
        Assert.AreEqual(12345, pubObj._hiddenValue)
        pubObj._hiddenReadOnlyValue = 678
        Assert.AreEqual(678, priObj.HiddenReadOnlyValue)
    End Sub
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
