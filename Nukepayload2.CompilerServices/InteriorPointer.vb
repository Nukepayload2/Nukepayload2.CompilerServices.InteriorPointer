Imports Nukepayload2.CompilerServices.Unsafe

''' <summary>
''' The non-typed interior pointer.
''' This type is an alternate of <see cref="Void"/>* in pointer algorithms.
''' <para>
''' Caution: <see cref="InteriorPointer"/> may not work normally when used as 
''' the data type of an array element,
''' field,
''' anonymous type member,
''' <see langword="Async"/> <see langword="Function"/> locals or argument,
''' <see langword="Async"/> <see langword="Sub"/> locals or argument,
''' <see langword="Iterator"/> <see langword="Function"/> locals or argument,
''' generic type argument,
''' lambda expression locals or argument,
''' anonymous delegate locals or argument,
''' locals of type (<see cref="Object"/>, <see cref="IEquatable(Of InteriorPointer)"/> or <see cref="ValueTuple"/>),
''' or parameter of type (<see cref="Object"/>, <see cref="IEquatable(Of InteriorPointer)"/> or <see cref="ValueTuple"/>).
''' </para>
''' </summary>
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

    Public Shared Operator >(left As InteriorPointer, right As InteriorPointer) As Boolean
        Return IsGreaterThan(left.Value, right.Value)
    End Operator

    Public Shared Operator <(left As InteriorPointer, right As InteriorPointer) As Boolean
        Return IsLessThan(left.Value, right.Value)
    End Operator

    Public Shared Operator >=(left As InteriorPointer, right As InteriorPointer) As Boolean
        Return Not IsLessThan(left.Value, right.Value)
    End Operator

    Public Shared Operator <=(left As InteriorPointer, right As InteriorPointer) As Boolean
        Return Not IsGreaterThan(left.Value, right.Value)
    End Operator

    Public Shared Widening Operator CType(ptr As InteriorPointer) As IntPtr
        Return ptr.Value
    End Operator

    Public Shared Narrowing Operator CType(ptr As IntPtr) As InteriorPointer
        Return New InteriorPointer(ptr)
    End Operator
End Structure
