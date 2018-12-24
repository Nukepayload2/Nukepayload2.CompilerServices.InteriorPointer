Imports System.Runtime.CompilerServices

Namespace Unsafe

    ''' <summary>
    ''' The typed transient pointer. 
    ''' This type is an alternate of <typeparamref name="T"/>* in pointer algorithms.
    ''' </summary>
    Public Structure TransientPointer(Of T)
        Friend Value As IntPtr

        Private Sub New(value As IntPtr)
            Me.Value = value
        End Sub

        ''' <summary>
        ''' Invalidates the pointer.
        ''' </summary>
        Friend Sub SetNothing()
            Value = Nothing
        End Sub

        Public Overloads Function Equals(other As TransientPointer(Of T)) As Boolean
            Return Value = other.Value
        End Function

        Public Overloads Function Equals(other As TransientPointer) As Boolean
            Return Value = other.Value
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is TransientPointer(Of T) Then
                Dim other = DirectCast(obj, TransientPointer(Of T))
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
        Public Function DecrementAndGet() As TransientPointer(Of T)
            Value -= SizeOf(Of T)()
            Return Me
        End Function

        ''' <summary>
        ''' This method is used to translate the pointer-- expression.
        ''' </summary>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function GetAndDecrement() As TransientPointer(Of T)
            Dim bak = Me
            Value -= SizeOf(Of T)()
            Return bak
        End Function

        ''' <summary>
        ''' This method is used to translate the ++pointer expression.
        ''' </summary>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function IncrementAndGet() As TransientPointer(Of T)
            Value += SizeOf(Of T)()
            Return Me
        End Function

        ''' <summary>
        ''' This method is used to translate the pointer++ expression.
        ''' </summary>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function GetAndIncrement() As TransientPointer(Of T)
            Dim bak = Me
            Value += SizeOf(Of T)()
            Return bak
        End Function

        ''' <summary>
        ''' This method is used to translate the *target = *source expression.
        ''' </summary>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Sub CopyUnmanagedFrom(other As TransientPointer(Of T))
        End Sub

        ''' <summary>
        ''' This method is used to translate the pointer + offset expression.
        ''' </summary>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Shared Operator +(pointer As TransientPointer(Of T), offset As Integer) As TransientPointer(Of T)
            pointer.Value += offset * SizeOf(Of T)()
            Return pointer
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer - offset expression.
        ''' </summary>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Shared Operator -(pointer As TransientPointer(Of T), offset As Integer) As TransientPointer(Of T)
            pointer.Value -= offset * SizeOf(Of T)()
            Return pointer
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer == other expression.
        ''' </summary>
        Public Shared Operator =(left As TransientPointer(Of T), right As TransientPointer(Of T)) As Boolean
            Return left.Value = right.Value
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer != other expression.
        ''' </summary>
        Public Shared Operator <>(left As TransientPointer(Of T), right As TransientPointer(Of T)) As Boolean
            Return left.Value <> right.Value
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer == other expression.
        ''' </summary>
        Public Shared Operator =(left As TransientPointer(Of T), right As TransientPointer) As Boolean
            Return left.Value = right.Value
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer != other expression.
        ''' </summary>
        Public Shared Operator <>(left As TransientPointer(Of T), right As TransientPointer) As Boolean
            Return left.Value <> right.Value
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer == other expression.
        ''' </summary>
        Public Shared Operator =(left As TransientPointer, right As TransientPointer(Of T)) As Boolean
            Return left.Value = right.Value
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer != other expression.
        ''' </summary>
        Public Shared Operator <>(left As TransientPointer, right As TransientPointer(Of T)) As Boolean
            Return left.Value <> right.Value
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer &gt; other expression.
        ''' </summary>
        Public Shared Operator >(left As TransientPointer(Of T), right As TransientPointer(Of T)) As Boolean
            Return IsGreaterThan(left.Value, right.Value)
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer &lt; other expression.
        ''' </summary>
        Public Shared Operator <(left As TransientPointer(Of T), right As TransientPointer(Of T)) As Boolean
            Return IsLessThan(left.Value, right.Value)
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer &gt;= other expression.
        ''' </summary>
        Public Shared Operator >=(left As TransientPointer(Of T), right As TransientPointer(Of T)) As Boolean
            Return Not IsLessThan(left.Value, right.Value)
        End Operator

        ''' <summary>
        ''' This method is used to translate the pointer &lt;= other expression.
        ''' </summary>
        Public Shared Operator <=(left As TransientPointer(Of T), right As TransientPointer(Of T)) As Boolean
            Return Not IsGreaterThan(left.Value, right.Value)
        End Operator

        ''' <summary>
        ''' This operator is used to translate the (IntPtr)pointer expression.
        ''' </summary>
        Public Shared Widening Operator CType(ptr As TransientPointer(Of T)) As IntPtr
            Return ptr.Value
        End Operator

        ''' <summary>
        ''' This operator is used to translate the (<typeparamref name="T"/>*)pointer expression.
        ''' </summary>
        Public Shared Narrowing Operator CType(ptr As IntPtr) As TransientPointer(Of T)
            Return New TransientPointer(Of T)(ptr)
        End Operator

        ''' <summary>
        ''' This operator is used to translate the (<typeparamref name="T"/>*)pointer expression.
        ''' </summary>
        Public Shared Narrowing Operator CType(ptr As TransientPointer) As TransientPointer(Of T)
            Return New TransientPointer(Of T)(ptr.Value)
        End Operator

        ''' <summary>
        ''' This function is used to translate the (<typeparamref name="T"/>*)pointer expression.
        ''' </summary>
        Public Function StaticCast(Of T2)() As TransientPointer(Of T2)
            Return New TransientPointer(Of T2)(Value)
        End Function

        ''' <summary>
        ''' This operator is used to translate the (<see cref="Void"/>*)pointer expression.
        ''' </summary>
        Public Shared Narrowing Operator CType(ptr As TransientPointer(Of T)) As TransientPointer
            Return New TransientPointer(ptr.Value)
        End Operator
    End Structure

End Namespace
