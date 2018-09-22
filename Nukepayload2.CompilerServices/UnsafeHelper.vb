Imports System.Runtime.CompilerServices

Friend Class UnsafeHelper
#Disable Warning BC42353
    <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
    Public Shared Function SizeOf(Of T)() As Integer
    End Function
#Enable Warning BC42353
End Class
