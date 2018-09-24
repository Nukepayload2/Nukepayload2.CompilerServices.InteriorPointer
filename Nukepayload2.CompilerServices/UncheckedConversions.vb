Imports System.Runtime.CompilerServices

Public Module UncheckedConversions
#Disable Warning BC42353
    ''' <summary>
    ''' Unchecked and devirtualized version of <see cref="ChrW(Integer)"/>.
    ''' This method is used to translate the (<see langword="char"/>)int32Value expression.
    ''' </summary>
    <Extension>
    <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
    Public Function UncheckedToChar(ch As Integer) As Char
    End Function

    ''' <summary>
    ''' Unchecked and devirtualized version of <see cref="AscW(Char)"/>. 
    ''' This method is used to translate the (<see langword="int"/>)charValue expression.
    ''' </summary>
    <Extension>
    <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
    Public Function UncheckedToInt32(ch As Char) As Integer
    End Function
#Enable Warning BC42353
End Module
