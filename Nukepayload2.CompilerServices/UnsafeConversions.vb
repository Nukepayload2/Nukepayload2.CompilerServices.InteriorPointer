Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Namespace Unsafe
    Public Module UnsafeConversions
        ''' <summary>
        ''' This method is used to translate the <see langword="static_cast"/>&lt;T&gt;(value) expression.
        ''' </summary>
        <Obsolete("Use UnsafeReinterpretCast or other overloads instead.")>
        <EditorBrowsable(EditorBrowsableState.Never)>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function UnsafeStaticCast(Of TValue, TResult)(value As TValue) As TResult
        End Function
        ''' <summary>
        ''' This method is used to translate the <see langword="static_cast"/>&lt;T&gt;(value) expression.
        ''' </summary>
        ''' <param name="sourceArray">This array should be pinned before using this method.</param>
        <Extension>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function UnsafeStaticCast(Of TElement)(sourceArray As TElement()) As InteriorPointer(Of TElement)
            Return sourceArray(0).UnsafeByRefToTypedPtr
        End Function
        ''' <summary>
        ''' This method is used to translate the <see langword="reinterpret_cast"/>&lt;T&gt;(value) expression.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function UnsafeReinterpretCast(Of TValue, TResult)(value As TValue) As TResult
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
        ''' <summary>
        ''' Converts a null-terminated sequence of <see cref="Char"/> to <see cref="String"/>.
        ''' This method is used to translate the <see langword="new"/> <see cref="String.New(Char*)"/>(<see cref="Char"/>*) expression.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function UnsafeToString(ptr As TransientPointer(Of Char)) As String
        End Function
#Enable Warning BC42105

        ''' <summary>
        ''' Converts an unmanaged value to <see cref="SByte"/>.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Sub UnsafeStaticCast(Of TSrc As Structure)(value As TSrc, <Out> ByRef result As SByte)
            ' ldarg.1
            ' ldarg.0
            ' conv.i1
            ' stind.i1
        End Sub

        ''' <summary>
        ''' Converts an unmanaged value to <see cref="Byte"/>.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Sub UnsafeStaticCast(Of TSrc As Structure)(value As TSrc, <Out> ByRef result As Byte)
            ' ldarg.1
            ' ldarg.0
            ' conv.u1
            ' stind.i1
        End Sub

        ''' <summary>
        ''' Converts an unmanaged value to <see cref="Short"/>.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Sub UnsafeStaticCast(Of TSrc As Structure)(value As TSrc, <Out> ByRef result As Short)
            ' ldarg.1
            ' ldarg.0
            ' conv.i2
            ' stind.i2
        End Sub

        ''' <summary>
        ''' Converts an unmanaged value to <see cref="UShort"/>.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Sub UnsafeStaticCast(Of TSrc As Structure)(value As TSrc, <Out> ByRef result As UShort)
            ' ldarg.1
            ' ldarg.0
            ' conv.u2
            ' stind.i2
        End Sub

        ''' <summary>
        ''' Converts an unmanaged value to <see cref="Integer"/>.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Sub UnsafeStaticCast(Of TSrc As Structure)(value As TSrc, <Out> ByRef result As Integer)
            ' ldarg.1
            ' ldarg.0
            ' conv.i4
            ' stind.i4
        End Sub

        ''' <summary>
        ''' Converts an unmanaged value to <see cref="UInteger"/>.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Sub UnsafeStaticCast(Of TSrc As Structure)(value As TSrc, <Out> ByRef result As UInteger)
            ' ldarg.1
            ' ldarg.0
            ' conv.u4
            ' stind.i4
        End Sub

        ''' <summary>
        ''' Converts an unmanaged value to <see cref="Long"/>.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Sub UnsafeStaticCast(Of TSrc As Structure)(value As TSrc, <Out> ByRef result As Long)
            ' ldarg.1
            ' ldarg.0
            ' conv.i8
            ' stind.i8
        End Sub

        ''' <summary>
        ''' Converts an unmanaged value to <see cref="ULong"/>.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Sub UnsafeStaticCast(Of TSrc As Structure)(value As TSrc, <Out> ByRef result As ULong)
            ' ldarg.1
            ' ldarg.0
            ' conv.u8
            ' stind.i8
        End Sub

        ''' <summary>
        ''' Converts an unmanaged value or a reference to <see cref="IntPtr"/>.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Sub UnsafeStaticCast(Of TSrc)(value As TSrc, <Out> ByRef result As IntPtr)
            ' ldarg.1
            ' ldarg.0
            ' conv.i
            ' stind.i
        End Sub

        ''' <summary>
        ''' Converts an unmanaged value or a reference to <see cref="UIntPtr"/>.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Sub UnsafeStaticCast(Of TSrc)(value As TSrc, <Out> ByRef result As UIntPtr)
            ' ldarg.1
            ' ldarg.0
            ' conv.u
            ' stind.i
        End Sub

        ''' <summary>
        ''' Converts an unmanaged value to <see cref="Single"/>.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Sub UnsafeStaticCast(Of TSrc As Structure)(value As TSrc, <Out> ByRef result As Single)
            ' ldarg.1
            ' ldarg.0
            ' conv.r4
            ' stind.r4
        End Sub

        ''' <summary>
        ''' Converts an unmanaged value to <see cref="Double"/>.
        ''' </summary>
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Sub UnsafeStaticCast(Of TSrc As Structure)(value As TSrc, <Out> ByRef result As Double)
            ' ldarg.1
            ' ldarg.0
            ' conv.r8
            ' stind.r8
        End Sub
    End Module
End Namespace
