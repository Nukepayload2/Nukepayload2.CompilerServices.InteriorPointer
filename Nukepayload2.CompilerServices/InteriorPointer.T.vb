Imports System.Runtime.CompilerServices

Namespace Unsafe

    ''' <summary>
    ''' The typed interior pointer. 
    ''' This type is an alternate of <typeparamref name="T"/>* in pointer algorithms.
    ''' <para>
    ''' Caution: <see cref="InteriorPointer(Of T)"/> may not work normally when used as 
    ''' the data type of an array element,
    ''' field,
    ''' anonymous type member,
    ''' <see langword="Async"/> <see langword="Function"/> locals or argument,
    ''' <see langword="Async"/> <see langword="Sub"/> locals or argument,
    ''' <see langword="Iterator"/> <see langword="Function"/> locals or argument,
    ''' generic type argument,
    ''' lambda expression locals or argument,
    ''' anonymous delegate locals or argument,
    ''' locals of type (<see cref="Object"/>, <see cref="IEquatable(Of T)"/> or <see cref="ValueTuple"/>),
    ''' or parameter of type (<see cref="Object"/>, <see cref="IEquatable(Of T)"/> or <see cref="ValueTuple"/>).
    ''' </para>
    ''' </summary>
    Public Structure InteriorPointer(Of T)
        Implements IEquatable(Of InteriorPointer), IEquatable(Of InteriorPointer(Of T))

        Friend Value As IntPtr

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
        ''' This property is used to translate the value = *pointer and the *pointer = value expression.
        ''' <para>
        ''' This property won't work when <typeparamref name="T"/> is <see langword="Class"/>, <see langword="Interface"/>, or <see langword="Structure"/> with field of reference type.
        ''' </para>
        ''' </summary>
        Public Property UnmanagedItem As T
            <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
            Get
            End Get
            <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
            Set(value As T)
            End Set
        End Property

        ''' <summary>
        ''' This method is used to translate the *pointer = value expression.
        ''' <para>
        ''' This method won't work when <typeparamref name="T"/> is <see langword="Class"/>, <see langword="Interface"/>, or <see langword="Structure"/> with field of reference type.
        ''' </para>
        ''' </summary>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Sub SetUnmanagedItem(value As T)
        End Sub

        ''' <summary>
        ''' This method is used to translate the --pointer expression.
        ''' </summary>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function DecrementAndGet() As InteriorPointer(Of T)
            Value -= SizeOf(Of T)()
            Return Me
        End Function

        ''' <summary>
        ''' This method is used to translate the pointer-- expression.
        ''' </summary>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function GetAndDecrement() As InteriorPointer(Of T)
            Dim bak = Me
            Value -= SizeOf(Of T)()
            Return bak
        End Function

        ''' <summary>
        ''' This method is used to translate the ++pointer expression.
        ''' </summary>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function IncrementAndGet() As InteriorPointer(Of T)
            Value += SizeOf(Of T)()
            Return Me
        End Function

        ''' <summary>
        ''' This method is used to translate the pointer++ expression.
        ''' </summary>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function GetAndIncrement() As InteriorPointer(Of T)
            Dim bak = Me
            Value += SizeOf(Of T)()
            Return bak
        End Function

        ''' <summary>
        ''' This method is used to translate the *target = *source expression.
        ''' </summary>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Sub CopyUnmanagedFrom(other As InteriorPointer(Of T))
        End Sub

        ''' <summary>
        ''' This method is used to translate the pointer + offset expression.
        ''' </summary>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Shared Operator +(pointer As InteriorPointer(Of T), offset As Integer) As InteriorPointer(Of T)
            pointer.Value += offset * SizeOf(Of T)()
            Return pointer
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer - offset expression.
        ''' </summary>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Shared Operator -(pointer As InteriorPointer(Of T), offset As Integer) As InteriorPointer(Of T)
            pointer.Value -= offset * SizeOf(Of T)()
            Return pointer
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer == other expression.
        ''' </summary>
        Public Shared Operator =(left As InteriorPointer(Of T), right As InteriorPointer(Of T)) As Boolean
            Return left.Value = right.Value
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer != other expression.
        ''' </summary>
        Public Shared Operator <>(left As InteriorPointer(Of T), right As InteriorPointer(Of T)) As Boolean
            Return left.Value <> right.Value
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer == other expression.
        ''' </summary>
        Public Shared Operator =(left As InteriorPointer(Of T), right As InteriorPointer) As Boolean
            Return left.Value = right.Value
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer != other expression.
        ''' </summary>
        Public Shared Operator <>(left As InteriorPointer(Of T), right As InteriorPointer) As Boolean
            Return left.Value <> right.Value
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer == other expression.
        ''' </summary>
        Public Shared Operator =(left As InteriorPointer, right As InteriorPointer(Of T)) As Boolean
            Return left.Value = right.Value
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer != other expression.
        ''' </summary>
        Public Shared Operator <>(left As InteriorPointer, right As InteriorPointer(Of T)) As Boolean
            Return left.Value <> right.Value
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer &gt; other expression.
        ''' </summary>
        Public Shared Operator >(left As InteriorPointer(Of T), right As InteriorPointer(Of T)) As Boolean
            Return IsGreaterThan(left.Value, right.Value)
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer &lt; other expression.
        ''' </summary>
        Public Shared Operator <(left As InteriorPointer(Of T), right As InteriorPointer(Of T)) As Boolean
            Return IsLessThan(left.Value, right.Value)
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer &gt;= other expression.
        ''' </summary>
        Public Shared Operator >=(left As InteriorPointer(Of T), right As InteriorPointer(Of T)) As Boolean
            Return Not IsLessThan(left.Value, right.Value)
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer &lt;= other expression.
        ''' </summary>
        Public Shared Operator <=(left As InteriorPointer(Of T), right As InteriorPointer(Of T)) As Boolean
            Return Not IsGreaterThan(left.Value, right.Value)
        End Operator

        ''' <summary>
        ''' This operator is used to translate the (IntPtr)pointer expression.
        ''' </summary>
        Public Shared Widening Operator CType(ptr As InteriorPointer(Of T)) As IntPtr
            Return ptr.Value
        End Operator

        ''' <summary>
        ''' This operator is used to translate the (<typeparamref name="T"/>*)pointer expression.
        ''' </summary>
        Public Shared Narrowing Operator CType(ptr As IntPtr) As InteriorPointer(Of T)
            Return New InteriorPointer(Of T)(ptr)
        End Operator

        ''' <summary>
        ''' This operator is used to translate the (<typeparamref name="T"/>*)pointer expression.
        ''' </summary>
        Public Shared Narrowing Operator CType(ptr As InteriorPointer) As InteriorPointer(Of T)
            Return New InteriorPointer(Of T)(ptr.Value)
        End Operator

        ''' <summary>
        ''' This operator is used to translate the (<see cref="Void"/>*)pointer expression.
        ''' </summary>
        Public Shared Narrowing Operator CType(ptr As InteriorPointer(Of T)) As InteriorPointer
            Return New InteriorPointer(ptr.Value)
        End Operator
    End Structure
End Namespace