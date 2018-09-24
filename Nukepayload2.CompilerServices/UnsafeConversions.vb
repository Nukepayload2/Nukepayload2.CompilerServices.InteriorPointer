Imports System.Runtime.CompilerServices

Namespace Unsafe
    Public Module UnsafeConversions
        ''' <summary>
        ''' This method is used to translate the <see langword="static_cast"/>&lt;T&gt;(value) expression.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function UnsafeStaticCast(Of TValue, TResult)(value As TValue) As TResult
        End Function

        ''' <summary>
        ''' Converts a <see langword="ByRef"/> value of <typeparamref name="TValue"/> to <see cref="InteriorPointer"/>.
        ''' This method is used to translate the (<see cref="Void"/>*)refValue expression.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function UnsafeByRefToPtr(Of TValue)(ByRef value As TValue) As InteriorPointer
        End Function

        ''' <summary>
        ''' Converts a <see langword="ByRef"/> value of <typeparamref name="TValue"/> to <see cref="InteriorPointer(Of TValue)"/>.
        ''' This method is used to translate the (<typeparamref name="TValue"/>*)refValue expression.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function UnsafeByRefToTypedPtr(Of TValue)(ByRef value As TValue) As InteriorPointer(Of TValue)
        End Function

        ''' <summary>
        ''' Converts an <see cref="InteriorPointer"/> to <see langword="ByRef"/> value of <typeparamref name="TValue"/>.
        ''' This method is used to translate the (<typeparamref name="TValue"/>&amp;)ptrValue expression.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function UnsafePtrToByRef(Of TValue)(ptr As InteriorPointer) As TValue ' ByRef TValue
        End Function

        ''' <summary>
        ''' Converts an <see cref="InteriorPointer(Of TValue)"/> to <see langword="ByRef"/> value of <typeparamref name="TValue"/>.
        ''' This method is used to translate the (<typeparamref name="TValue"/>&amp;)ptrValue expression.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function UnsafePtrToByRef(Of TValue)(ptr As InteriorPointer(Of TValue)) As TValue ' ByRef TValue
        End Function

#Disable Warning BC42105
        ''' <summary>
        ''' Converts a null-terminated sequence of <see cref="Char"/> to <see cref="String"/>.
        ''' This method is used to translate the <see langword="new"/> <see cref="String.New(Char*)"/>(<see cref="Char"/>*) expression.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function UnsafeToString(ptr As InteriorPointer(Of Char)) As String
        End Function
#Enable Warning BC42105

    End Module
End Namespace
