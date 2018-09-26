Imports System.Runtime.CompilerServices

Public Module SafeOperators

    ''' <summary>
    ''' This method is used to translate the c-style assignment expression.
    ''' </summary>
    <Extension>
    Public Function Assign(Of T)(ByRef this As T, other As T) As T
        this = other
        Return this
    End Function

End Module
