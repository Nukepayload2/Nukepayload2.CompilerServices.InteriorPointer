Imports System.Diagnostics.CodeAnalysis
Imports Nukepayload2.CompilerServices.Unsafe

Module Program

    <SuppressMessage("", "BC42351")>
    Sub Main(args As String())
        Dim testString = "Hello World!"
        Dim addrOfVariable As IntPtr = VarPtr(testString)
        Dim addrOfFirstChar, addrOfPinnedObject As IntPtr
        Using pinPtr = StrPtr(testString)
            addrOfFirstChar = pinPtr.Pointer
        End Using
        Using pinPtr = ObjPtr(testString)
            addrOfPinnedObject = pinPtr.Pointer
        End Using
        Console.WriteLine("Type of the test subject is String")
        Console.WriteLine($"VarPtr = &H{addrOfVariable.ToInt64:X}")
        Console.WriteLine($"StrPtr = &H{addrOfFirstChar.ToInt64:X}")
        Console.WriteLine($"ObjPtr = &H{addrOfPinnedObject.ToInt64:X}")

        Dim testArray = testString.ToCharArray
        addrOfVariable = VarPtr(testArray)
        Dim firstElementRef = VarPtr(testArray(0))
        Dim firstElement As IntPtr
        Using pinPtr = Fixed(testArray)
            firstElement = pinPtr.Pointer
            addrOfPinnedObject = ObjPtr(testArray).Pointer
        End Using
        Console.WriteLine("Type of the test subject is SZArray")
        Console.WriteLine($"VarPtr = &H{addrOfVariable.ToInt64:X}")
        Console.WriteLine($"Fixed  = &H{firstElement.ToInt64:X}")
        Console.WriteLine($"ByRef array(0)  = &H{CType(firstElementRef, IntPtr).ToInt64:X}")
        Console.WriteLine($"ObjPtr = &H{addrOfPinnedObject.ToInt64:X}")
    End Sub
End Module