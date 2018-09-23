Imports System.Runtime.CompilerServices

Public Module SafePointerOperators
    <Extension>
    Public Function Assign(ByRef this As InteriorPointer, other As InteriorPointer) As InteriorPointer
        this = other
        Return this
    End Function

    <Extension>
    Public Function Assign(Of T)(ByRef this As InteriorPointer(Of T), other As InteriorPointer(Of T)) As InteriorPointer(Of T)
        this = other
        Return this
    End Function
End Module
