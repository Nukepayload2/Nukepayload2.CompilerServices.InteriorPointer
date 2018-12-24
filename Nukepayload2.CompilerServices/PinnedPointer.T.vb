Imports System.Runtime.InteropServices

Namespace Unsafe
    ''' <summary>
    ''' Contains a <see cref="TransientPointer(Of T)"/> and a <see cref="GCHandle"/> that pins the pointer's target object. 
    ''' This type is used to translate the <see langword="fixed"/> block.
    ''' <para>
    ''' Please use this type with <see cref="UnsafeOperators"/> and the <see langword="Using"/> statement.
    ''' </para>
    ''' </summary>
    Public Structure PinnedPointer(Of T)
        Implements IDisposable

        Public ReadOnly Pointer As TransientPointer(Of T)
        Friend Handle As GCHandle

        Friend Sub New(pointer As TransientPointer(Of T), handle As GCHandle)
            Me.Pointer = pointer
            Me.Handle = handle
        End Sub

        Public ReadOnly Property IsDisposed As Boolean
            Get
                Return Handle = Nothing
            End Get
        End Property

        Public Sub Dispose() Implements IDisposable.Dispose
            If Pointer.IsZero Then
                Return
            End If
            Handle.Free()
            Pointer.SetNothing()
        End Sub

        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is PinnedPointer(Of T) Then
                Dim other = DirectCast(obj, PinnedPointer(Of T))
                Return Pointer = other.Pointer AndAlso Handle = other.Handle
            End If
            Return False
        End Function

        Public Overloads Function Equals(other As PinnedPointer(Of T)) As Boolean
            Return Pointer = other.Pointer AndAlso Handle = other.Handle
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return Pointer.GetHashCode
        End Function

        Public Shared Operator =(left As PinnedPointer(Of T), right As PinnedPointer(Of T)) As Boolean
            Return left.Equals(right)
        End Operator

        Public Shared Operator <>(left As PinnedPointer(Of T), right As PinnedPointer(Of T)) As Boolean
            Return Not left = right
        End Operator
    End Structure

End Namespace
