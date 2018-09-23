Imports System.Runtime.CompilerServices

Namespace Unsafe

    Friend Class UnsafeOperators
#Disable Warning BC42353
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Shared Function SizeOf(Of T)() As Integer
        End Function

        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Shared Function IsLessThan(ptr1 As IntPtr, ptr2 As IntPtr) As Boolean
        End Function

        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Shared Function IsGreaterThan(ptr1 As IntPtr, ptr2 As IntPtr) As Boolean
        End Function
#Enable Warning BC42353
    End Class

End Namespace
