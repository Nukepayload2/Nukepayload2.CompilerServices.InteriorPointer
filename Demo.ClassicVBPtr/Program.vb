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
        Console.WriteLine($"VarPtr = {addrOfVariable}")
        Console.WriteLine($"StrPtr = {addrOfFirstChar}")
        Console.WriteLine($"ObjPtr = {addrOfPinnedObject}")

        Dim testArray = testString.ToCharArray
        addrOfVariable = VarPtr(testArray)
        Dim firstElement As IntPtr
        Using pinPtr = Fixed(testArray)
            firstElement = pinPtr.Pointer
            addrOfPinnedObject = ObjPtr(testArray).Pointer
        End Using
        Console.WriteLine("Type of the test subject is SZArray")
        Console.WriteLine($"VarPtr = {addrOfVariable}")
        Console.WriteLine($"Fixed  = {firstElement}")
        Console.WriteLine($"ObjPtr = {addrOfPinnedObject}")
    End Sub
End Module