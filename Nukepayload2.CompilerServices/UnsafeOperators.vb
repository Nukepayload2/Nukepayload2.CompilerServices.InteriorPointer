Imports System.Runtime.CompilerServices

Namespace Unsafe

    Public Module UnsafeOperators
#Disable Warning BC42353
        ''' <summary>
        ''' Gets the size of a type.
        ''' </summary>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Friend Function SizeOf(Of T)() As Integer
        End Function

        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Friend Function IsLessThan(ptr1 As IntPtr, ptr2 As IntPtr) As Boolean
        End Function

        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Friend Function IsGreaterThan(ptr1 As IntPtr, ptr2 As IntPtr) As Boolean
        End Function

#Enable Warning BC42353
    End Module

End Namespace
