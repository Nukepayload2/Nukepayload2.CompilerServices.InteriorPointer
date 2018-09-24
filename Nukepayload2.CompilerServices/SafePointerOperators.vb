Imports System.Runtime.CompilerServices
Imports Nukepayload2.CompilerServices.Unsafe

Public Module SafePointerOperators
    ''' <summary>
    ''' This method is used to translate the c-style assignment expression.
    ''' </summary>
    <Extension>
    Public Function Assign(ByRef this As InteriorPointer, other As InteriorPointer) As InteriorPointer
        this = other
        Return this
    End Function

    ''' <summary>
    ''' This method is used to translate the c-style assignment expression.
    ''' </summary>
    <Extension>
    Public Function Assign(Of T)(ByRef this As InteriorPointer(Of T), other As InteriorPointer(Of T)) As InteriorPointer(Of T)
        this = other
        Return this
    End Function
End Module
