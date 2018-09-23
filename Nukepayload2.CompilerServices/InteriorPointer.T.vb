Imports Nukepayload2.CompilerServices.Unsafe

Public Structure InteriorPointer(Of T)
    Implements IEquatable(Of InteriorPointer), IEquatable(Of InteriorPointer(Of T))

    Private Shared ReadOnly SizeOfElement As Integer = UnsafeOperators.SizeOf(Of T)

    Private Value As IntPtr

    Private Sub New(value As IntPtr)
        Me.Value = value
    End Sub

    Public Overloads Function Equals(other As InteriorPointer(Of T)) As Boolean Implements IEquatable(Of InteriorPointer(Of T)).Equals
        Return Value = other.Value
    End Function

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

    Public Shared Operator +(pointer As InteriorPointer(Of T), offset As Integer) As InteriorPointer(Of T)
        pointer.Value += offset * SizeOfElement
        Return pointer
    End Operator

    Public Shared Operator -(pointer As InteriorPointer(Of T), offset As Integer) As InteriorPointer(Of T)
        pointer.Value -= offset * SizeOfElement
        Return pointer
    End Operator

    Public Shared Operator =(left As InteriorPointer(Of T), right As InteriorPointer(Of T)) As Boolean
        Return left.Value = right.Value
    End Operator

    Public Shared Operator <>(left As InteriorPointer(Of T), right As InteriorPointer(Of T)) As Boolean
        Return left.Value <> right.Value
    End Operator

    Public Shared Operator =(left As InteriorPointer(Of T), right As InteriorPointer) As Boolean
        Return left.Value = right.Value
    End Operator

    Public Shared Operator <>(left As InteriorPointer(Of T), right As InteriorPointer) As Boolean
        Return left.Value <> right.Value
    End Operator

    Public Shared Operator =(left As InteriorPointer, right As InteriorPointer(Of T)) As Boolean
        Return left.Value = right.Value
    End Operator

    Public Shared Operator <>(left As InteriorPointer, right As InteriorPointer(Of T)) As Boolean
        Return left.Value <> right.Value
    End Operator

    Public Shared Operator >(left As InteriorPointer(Of T), right As InteriorPointer(Of T)) As Boolean
        Return UnsafeOperators.IsGreaterThan(left.Value, right.Value)
    End Operator

    Public Shared Operator <(left As InteriorPointer(Of T), right As InteriorPointer(Of T)) As Boolean
        Return UnsafeOperators.IsLessThan(left.Value, right.Value)
    End Operator

    Public Shared Widening Operator CType(ptr As InteriorPointer(Of T)) As IntPtr
        Return ptr.Value
    End Operator

    Public Shared Narrowing Operator CType(ptr As IntPtr) As InteriorPointer(Of T)
        Return New InteriorPointer(Of T)(ptr)
    End Operator

    Public Shared Narrowing Operator CType(ptr As InteriorPointer) As InteriorPointer(Of T)
        Return New InteriorPointer(Of T)(ptr.Value)
    End Operator

    Public Shared Narrowing Operator CType(ptr As InteriorPointer(Of T)) As InteriorPointer
        Return New InteriorPointer(ptr.Value)
    End Operator
End Structure
