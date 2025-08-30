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

    <TestMethod>
    Sub TestSetNothing()
        Dim value As Integer = 42
        Dim ptr = value.UnsafeByRefToTypedPtr
        Assert.IsTrue(ptr.IsNotZero)
        ptr.SetNothing()
        Assert.IsTrue(ptr.IsZero)
    End Sub

    <TestMethod>
    Sub TestUnmanagedItemProperty()
        Dim value As Integer = 123
        Dim ptr = value.UnsafeByRefToTypedPtr
        
        ' Test getter
        Assert.AreEqual(123, ptr.UnmanagedItem)
        
        ' Test setter
        ptr.UnmanagedItem = 456
        Assert.AreEqual(456, ptr.UnmanagedItem)
        Assert.AreEqual(456, value)
    End Sub

    <TestMethod>
    Sub TestSetUnmanagedItem()
        Dim value As Integer = 789
        Dim ptr = value.UnsafeByRefToTypedPtr
        
        ptr.SetUnmanagedItem(999)
        Assert.AreEqual(999, value)
        Assert.AreEqual(999, ptr.UnmanagedItem)
    End Sub

    <TestMethod>
    Sub TestCopyUnmanagedFrom()
        Dim sourceValue As Integer = 111
        Dim targetValue As Integer = 222
        Dim sourcePtr = sourceValue.UnsafeByRefToTypedPtr
        Dim targetPtr = targetValue.UnsafeByRefToTypedPtr
        
        targetPtr.CopyUnmanagedFrom(sourcePtr)
        Assert.AreEqual(111, targetValue)
        Assert.AreEqual(111, targetPtr.UnmanagedItem)
    End Sub

    <TestMethod>
    Sub TestStaticCast()
        Dim value As Integer = 1000
        Dim ptr = value.UnsafeByRefToTypedPtr
        Dim longPtr = ptr.StaticCast(Of UShort)()

        Assert.AreEqual(ptr.Value, longPtr.Value)
        Assert.AreEqual(1000US, longPtr.UnmanagedItem)
    End Sub

    <TestMethod>
    Sub TestToString()
        Dim value As Integer = 12345
        Dim ptr = value.UnsafeByRefToTypedPtr
        Dim str = ptr.ToString()
        Assert.AreEqual(ptr.Value.ToString(), str)
    End Sub

    <TestMethod>
    Sub TestExceptionMethods()
        Dim value As Integer = 42
        Dim ptr = value.UnsafeByRefToTypedPtr
        
        ' Test Equals(obj) throws exception
        Try
            Dim result = ptr.Equals(CObj(Nothing))
            Assert.Fail("Should have thrown NotSupportedException")
        Catch ex As NotSupportedException
            Assert.IsTrue(ex.Message.Contains("interior pointer"))
        End Try
        
        ' Test GetHashCode throws exception
        Try
            Dim hash = ptr.GetHashCode()
            Assert.Fail("Should have thrown NotSupportedException")
        Catch ex As NotSupportedException
            Assert.IsTrue(ex.Message.Contains("interior pointer"))
        End Try
    End Sub

    <TestMethod>
    Sub TestConversionOperators()
        Dim value As Integer = 999
        Dim typedPtr = value.UnsafeByRefToTypedPtr
        Dim rawPtr As InteriorPointer = typedPtr
        Dim backToTyped As InteriorPointer(Of Integer) = rawPtr
        Dim intPtr As IntPtr = typedPtr
        
        Assert.AreEqual(typedPtr.Value, rawPtr.Value)
        Assert.AreEqual(typedPtr.Value, backToTyped.Value)
        Assert.AreEqual(typedPtr.Value, intPtr)
    End Sub

    <TestMethod>
    Sub TestStructureUnmanagedItem()
        Dim color As MyColor = MyColor.Red
        Dim ptr = color.UnsafeByRefToTypedPtr
        
        ' Test getter
        Assert.AreEqual(color.A, ptr.UnmanagedItem.A)
        Assert.AreEqual(color.R, ptr.UnmanagedItem.R)
        Assert.AreEqual(color.G, ptr.UnmanagedItem.G)
        Assert.AreEqual(color.B, ptr.UnmanagedItem.B)
        
        ' Test setter
        Dim newColor As New MyColor With {.A = 10, .R = 20, .G = 30, .B = 40}
        ptr.UnmanagedItem = newColor
        Assert.AreEqual(CByte(10), color.A)
        Assert.AreEqual(CByte(20), color.R)
        Assert.AreEqual(CByte(30), color.G)
        Assert.AreEqual(CByte(40), color.B)
    End Sub

    <TestMethod>
    Sub TestPointerIncrementDecrementMethods()
        ' Test with array to have multiple elements
        Dim array As Integer() = {10, 20, 30, 40}
        Dim ptr = array(0).UnsafeByRefToTypedPtr

        ' Test GetAndIncrement (postfix++) - returns original value, then increments
        Dim resultPtr = ptr.GetAndIncrement()
        Assert.AreEqual(10, resultPtr.UnmanagedItem)
        Assert.AreEqual(20, ptr.UnmanagedItem)  ' ptr should now point to next element

        ' Test IncrementAndGet (prefix++) - increments first, then returns
        ptr = array(0).UnsafeByRefToTypedPtr
        resultPtr = ptr.IncrementAndGet()
        Assert.AreEqual(20, resultPtr.UnmanagedItem)  ' Should return the incremented pointer
        Assert.AreEqual(20, ptr.UnmanagedItem)  ' ptr should now point to next element

        ' Test GetAndDecrement (postfix--) - returns original value, then decrements
        ptr = array(2).UnsafeByRefToTypedPtr
        resultPtr = ptr.GetAndDecrement()
        Assert.AreEqual(30, resultPtr.UnmanagedItem)
        Assert.AreEqual(20, ptr.UnmanagedItem)  ' ptr should now point to previous element

        ' Test DecrementAndGet (prefix--) - decrements first, then returns
        ptr = array(1).UnsafeByRefToTypedPtr
        resultPtr = ptr.DecrementAndGet()
        Assert.AreEqual(10, resultPtr.UnmanagedItem)  ' Should return the decremented pointer
        Assert.AreEqual(10, ptr.UnmanagedItem)  ' ptr should now point to previous element
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
