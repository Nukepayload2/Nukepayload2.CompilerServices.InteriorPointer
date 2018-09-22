Public Structure InteriorPointer
    Implements IEquatable(Of InteriorPointer)

    Friend Value As IntPtr

    Friend Sub New(value As IntPtr)
        Me.Value = value
    End Sub

    Public Overloads Function Equals(other As InteriorPointer) As Boolean Implements IEquatable(Of InteriorPointer).Equals
        Return Value = other.Value
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return TypeOf obj Is InteriorPointer AndAlso DirectCast(obj, InteriorPointer).Equals(Me)
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return Value.GetHashCode
    End Function

    Public ReadOnly Property IsZero As Boolean
        Get
            Return Value = IntPtr.Zero
        End Get
    End Property

    Public Shared Operator +(pointer As InteriorPointer, offset As Integer) As InteriorPointer
        pointer.Value += offset
        Return pointer
    End Operator

    Public Shared Operator -(pointer As InteriorPointer, offset As Integer) As InteriorPointer
        pointer.Value -= offset
        Return pointer
    End Operator

    Public Shared Operator =(left As InteriorPointer, right As InteriorPointer) As Boolean
        Return left.Value = right.Value
    End Operator

    Public Shared Operator <>(left As InteriorPointer, right As InteriorPointer) As Boolean
        Return left.Value <> right.Value
    End Operator

    Public Shared Widening Operator CType(ptr As InteriorPointer) As IntPtr
        Return ptr.Value
    End Operator

    Public Shared Narrowing Operator CType(ptr As IntPtr) As InteriorPointer
        Return New InteriorPointer(ptr)
    End Operator
End Structure
