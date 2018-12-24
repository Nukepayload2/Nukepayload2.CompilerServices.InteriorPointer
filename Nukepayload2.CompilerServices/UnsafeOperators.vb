Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Namespace Unsafe

    Public Module UnsafeOperators
#Disable Warning BC42353
        ''' <summary>
        ''' Gets the size of a type. If the type is value type, returns the size of its value.
        ''' If the type is reference type, returns the size of <see cref="IntPtr"/>.
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
        ''' <exception cref="ArgumentException"/>
        ''' <exception cref="ArgumentNullException"/>
        Public Function Fixed(Of T)(targetArray As T()) As PinnedPointer(Of T)
            If targetArray Is Nothing Then
                Throw New ArgumentNullException(NameOf(targetArray))
            End If
            If targetArray.Length = 0 Then
                Throw New ArgumentException("Array is empty.", NameOf(targetArray))
            End If
            Dim hGc = GCHandle.Alloc(targetArray, GCHandleType.Pinned)
            Return New PinnedPointer(Of T)(CType(hGc.AddrOfPinnedObject, TransientPointer(Of T)), hGc)
        End Function

        ''' <summary>
        ''' Pins the specified array and gets a pointer to the element at the specified startIndex.
        ''' </summary>
        ''' <exception cref="ArgumentException"/>
        ''' <exception cref="ArgumentNullException"/>
        ''' <exception cref="ArgumentOutOfRangeException"/>
        Public Function Fixed(Of T)(targetArray As T(), startIndex As Integer) As PinnedPointer(Of T)
            If targetArray Is Nothing Then
                Throw New ArgumentNullException(NameOf(targetArray))
            End If
            If targetArray.Length = 0 Then
                Throw New ArgumentException("Array is empty.", NameOf(targetArray))
            End If
            If startIndex < 0 Then
                Throw New ArgumentOutOfRangeException(NameOf(startIndex))
            End If
            Dim hGc = GCHandle.Alloc(targetArray, GCHandleType.Pinned)
            Return New PinnedPointer(Of T)(CType(hGc.AddrOfPinnedObject, TransientPointer(Of T)), hGc)
        End Function

        ''' <summary>
        ''' Gets the address of an local variable.
        ''' </summary>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function VarPtr(Of T)(ByRef variable As T) As InteriorPointer(Of T)
            Return UnsafeByRefToTypedPtr(variable)
        End Function

        ''' <summary>
        ''' Pins the specified reference type and then gets its reference as <see cref="IntPtr"/>.
        ''' This method is less useful than in classic VB.
        ''' </summary>
        Public Function ObjPtr(Of T As Class)(variable As T) As PinnedPointer
            Dim hGc = GCHandle.Alloc(variable, GCHandleType.Pinned)
            Dim pPtr = variable.UnsafeReinterpretCast(Of IntPtr)
            Return New PinnedPointer(CType(pPtr, InteriorPointer), hGc)
        End Function

        ''' <summary>
        ''' Pins the specified string and gets a pointer to its first character.
        ''' </summary>
        ''' <exception cref="ArgumentNullException"/>
        Public Function StrPtr(str As String) As PinnedPointer(Of Char)
            If str?.Length = 0 Then
                Throw New ArgumentNullException(NameOf(str))
            End If

            Dim hGc = GCHandle.Alloc(str, GCHandleType.Pinned)
            Dim pPtr = StrPtrInternal(str)
            Return New PinnedPointer(Of Char)(pPtr, hGc)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining Or MethodImplOptions.ForwardRef)>
        Friend Function StrPtrInternal(str As String) As TransientPointer(Of Char)
        End Function
    End Module

End Namespace
