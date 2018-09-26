Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Namespace Unsafe

    Public Module UnsafeOperators
#Disable Warning BC42353
        ''' <summary>
        ''' Gets the size of a type.
        ''' </summary>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function SizeOf(Of T)() As Integer
        End Function

        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Friend Function IsLessThan(ptr1 As IntPtr, ptr2 As IntPtr) As Boolean
        End Function

        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Friend Function IsGreaterThan(ptr1 As IntPtr, ptr2 As IntPtr) As Boolean
        End Function

#Enable Warning BC42353

        Public Function Fixed(Of T)(arr As T()) As PinnedPointer(Of T)
            Dim hGc = GCHandle.Alloc(arr, GCHandleType.Pinned)
            Return New PinnedPointer(Of T)(arr(0).UnsafeByRefToTypedPtr, hGc)
        End Function

        Public Function Fixed(Of T)(arr As T(), startIndex As Integer) As PinnedPointer(Of T)
            Dim hGc = GCHandle.Alloc(arr, GCHandleType.Pinned)
            Return New PinnedPointer(Of T)(arr(startIndex).UnsafeByRefToTypedPtr, hGc)
        End Function
    End Module

End Namespace
