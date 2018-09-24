Imports Nukepayload2.CompilerServices
Imports Nukepayload2.CompilerServices.Unsafe

Friend Structure StackStringBuilder
    Private ReadOnly _backBuffer As InteriorPointer(Of Char)
    Private _currentPosition As InteriorPointer(Of Char)

    Public Sub New(buf As InteriorPointer(Of Char))
        _backBuffer = buf
        _currentPosition = buf
    End Sub

    Public Sub Append(ch As Char)
        _currentPosition.GetAndIncrement.SetUnmanagedItem(ch)
    End Sub

    Public Sub Append(ch As Char, repeatCount As Integer)
        For i = 0 To repeatCount - 1
            _currentPosition.GetAndIncrement.SetUnmanagedItem(ch)
        Next
    End Sub

    Public Sub AppendChar(ch As Integer)
        _currentPosition.GetAndIncrement.SetUnmanagedItem(ch.UncheckedToChar)
    End Sub

    Public Sub Append(buf As InteriorPointer(Of Char), length As Integer)
        For i = 0 To length - 1
            _currentPosition.GetAndIncrement.CopyUnmanagedFrom(buf.GetAndIncrement)
        Next
    End Sub

    Public Sub Append(buf As InteriorPointer(Of Char), startIndex As Integer, length As Integer)
        Dim pCurrent = buf + startIndex
        For i = 0 To length - 1
            _currentPosition.GetAndIncrement.CopyUnmanagedFrom(pCurrent.GetAndIncrement)
        Next
    End Sub

    Public Overloads Function ToString() As String
        Return _backBuffer.UnsafeToString
    End Function
End Structure
