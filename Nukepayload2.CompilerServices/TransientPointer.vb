Imports Nukepayload2.CompilerServices.Unsafe

''' <summary>
''' A raw pointer to unmanaged memory or pinned mananged memory.
''' This type is an alternate of <see cref="Void"/>* in pointer algorithms.
''' </summary>
Public Structure TransientPointer
    Friend Value As IntPtr

    Friend Sub New(value As IntPtr)
        Me.Value = value
    End Sub

    Friend Sub SetNothing()
        Value = Nothing
    End Sub

    Public Overloads Function Equals(other As TransientPointer) As Boolean
        Return Value = other.Value
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj Is TransientPointer Then
            Dim other = DirectCast(obj, TransientPointer)
            Return Value = other.Value
        End If
        Return False
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return Value.GetHashCode
    End Function

    ''' <summary>
    ''' This method is used to translate the pointer == <see langword="nullptr"/> expression.
    ''' </summary>
    Public ReadOnly Property IsZero As Boolean
        Get
            Return Value = IntPtr.Zero
        End Get
    End Property

    ''' <summary>
    ''' This method is used to translate the pointer != <see langword="nullptr"/> expression.
    ''' </summary>
    Public ReadOnly Property IsNotZero As Boolean
        Get
            Return Value <> IntPtr.Zero
        End Get
    End Property

    ''' <summary>
    ''' This method is used to translate the pointer + offset expression.
    ''' </summary>
    Public Shared Operator +(pointer As TransientPointer, offset As Integer) As TransientPointer
        pointer.Value += offset
        Return pointer
    End Operator

    Public Shared Operator -(pointer As TransientPointer, offset As Integer) As TransientPointer
        pointer.Value -= offset
        Return pointer
    End Operator

    Public Shared Operator =(left As TransientPointer, right As TransientPointer) As Boolean
        Return left.Value = right.Value
    End Operator

    Public Shared Operator <>(left As TransientPointer, right As TransientPointer) As Boolean
        Return left.Value <> right.Value
    End Operator

    Public Shared Operator >(left As TransientPointer, right As TransientPointer) As Boolean
        Return IsGreaterThan(left.Value, right.Value)
    End Operator

    Public Shared Operator <(left As TransientPointer, right As TransientPointer) As Boolean
        Return IsLessThan(left.Value, right.Value)
    End Operator

    Public Shared Operator >=(left As TransientPointer, right As TransientPointer) As Boolean
        Return Not IsLessThan(left.Value, right.Value)
    End Operator

    Public Shared Operator <=(left As TransientPointer, right As TransientPointer) As Boolean
        Return Not IsGreaterThan(left.Value, right.Value)
    End Operator

    Public Shared Widening Operator CType(ptr As TransientPointer) As IntPtr
        Return ptr.Value
    End Operator

    Public Shared Narrowing Operator CType(ptr As IntPtr) As TransientPointer
        Return New TransientPointer(ptr)
    End Operator
End Structure
