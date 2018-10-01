Imports System.Runtime.InteropServices

Namespace Unsafe
    ''' <summary>
    ''' Contains a <see cref="IntPtr"/> and a <see cref="GCHandle"/> that pins the pointer's target object. 
    ''' <para>
    ''' Please use this type with <see cref="UnsafeOperators"/> and the <see langword="Using"/> statement.
    ''' </para>
    ''' </summary>
    Public Structure PinnedPointer
        Implements IDisposable
        Public Pointer As IntPtr
        Friend Handle As GCHandle

        Friend Sub New(pointer As IntPtr, handle As GCHandle)
            Me.Pointer = pointer
            Me.Handle = handle
        End Sub

        Public ReadOnly Property IsDisposed As Boolean
            Get
                Return Handle = Nothing
            End Get
        End Property

        Public Sub Dispose() Implements IDisposable.Dispose
            If Pointer = IntPtr.Zero Then
                Return
            End If
            Handle.Free()
            Pointer = Nothing
        End Sub

        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is PinnedPointer Then
                Dim other = DirectCast(obj, PinnedPointer)
                Return Pointer = other.Pointer AndAlso Handle = other.Handle
            End If
            Return False
        End Function

        Public Overloads Function Equals(other As PinnedPointer) As Boolean
            Return Pointer = other.Pointer AndAlso Handle = other.Handle
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Pointer.GetHashCode
        End Function

        Public Shared Operator =(left As PinnedPointer, right As PinnedPointer) As Boolean
            Return left.Equals(right)
        End Operator

        Public Shared Operator <>(left As PinnedPointer, right As PinnedPointer) As Boolean
            Return Not left = right
        End Operator
    End Structure

End Namespace
