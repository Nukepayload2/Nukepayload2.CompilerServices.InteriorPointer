Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports Nukepayload2.CompilerServices.Unsafe

''' <summary>
''' The raw interior pointer. 
''' This type is an alternate of <see cref="Void"/>* in pointer algorithms.
''' <para>
''' Caution: The <see cref="InteriorPointer"/> may lose its target if it's used as 
''' the data type of an array element,
''' field of class,
''' field of structure which is a field of class (directly or indirectly),
''' anonymous type member,
''' generic type argument,
''' <see langword="Async"/> <see langword="Function"/> locals or argument,
''' <see langword="Async"/> <see langword="Sub"/> locals or argument,
''' <see langword="Iterator"/> <see langword="Function"/> locals or argument,
''' lambda expression locals or argument,
''' anonymous delegate locals or argument,
''' locals as <see cref="Object"/> or <see cref="ValueType"/>,
''' <see langword="Static"/> locals,
''' or parameters as <see cref="Object"/> or <see cref="ValueType"/>.
''' </para>
''' </summary>
<IsByRefLike>
Public Structure InteriorPointer
    Friend Value As IntPtr

    Friend Sub New(value As IntPtr)
        Me.Value = value
    End Sub

    Friend Sub SetNothing()
        Value = Nothing
    End Sub

    Public Overloads Function Equals(other As InteriorPointer) As Boolean
        Return Value = other.Value
    End Function

    <EditorBrowsable(EditorBrowsableState.Never)>
    Public Overrides Function Equals(obj As Object) As Boolean
        Throw New NotSupportedException("You can't call this method on interior pointer.")
    End Function

    <EditorBrowsable(EditorBrowsableState.Never)>
    Public Overrides Function GetHashCode() As Integer
        Throw New NotSupportedException("You can't call this method on interior pointer.")
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
