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

        ''' <summary>
        ''' Pins the specified array and gets a pointer to its first element.
        ''' </summary>
        Public Function Fixed(Of T)(arr As T()) As PinnedPointer(Of T)
            Dim hGc = GCHandle.Alloc(arr, GCHandleType.Pinned)
            Return New PinnedPointer(Of T)(arr(0).UnsafeByRefToTypedPtr, hGc)
        End Function

        ''' <summary>
        ''' Pins the specified array and gets a pointer to the element at the specified startIndex.
        ''' </summary>
        Public Function Fixed(Of T)(arr As T(), startIndex As Integer) As PinnedPointer(Of T)
            Dim hGc = GCHandle.Alloc(arr, GCHandleType.Pinned)
            Return New PinnedPointer(Of T)(arr(startIndex).UnsafeByRefToTypedPtr, hGc)
        End Function

        ''' <summary>
        ''' Gets the address of an local variable.
        ''' </summary>
        ''' <remarks>Welcome back, VarPtr! </remarks>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function VarPtr(Of TVariable)(ByRef variable As TVariable) As InteriorPointer(Of TVariable)
            Return UnsafeByRefToTypedPtr(variable)
        End Function

        ''' <summary>
        ''' Pins the specified string and gets a pointer to its first character.
        ''' </summary>
        ''' <remarks>Welcome back, StrPtr! </remarks>
        Public Function StrPtr(str As String) As PinnedPointer(Of Char)
            Dim hGc = GCHandle.Alloc(str, GCHandleType.Pinned)
            Dim pPtr = StrPtrInternal(str)
            Return New PinnedPointer(Of Char)(pPtr, hGc)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining Or MethodImplOptions.ForwardRef)>
        Private Function StrPtrInternal(str As String) As InteriorPointer(Of Char)
        End Function
    End Module

End Namespace
