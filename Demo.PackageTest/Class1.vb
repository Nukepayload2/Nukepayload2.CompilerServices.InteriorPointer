Imports Nukepayload2.CompilerServices.Unsafe

Public Class Class1
    Function Fill(ptr As InteriorPointer(Of Integer), count As Integer, value As Integer) As InteriorPointer(Of Integer)
        For i = 1 To count
            ptr.GetAndIncrement.SetUnmanagedItem(value)
        Next
        Return ptr
    End Function
End Class
