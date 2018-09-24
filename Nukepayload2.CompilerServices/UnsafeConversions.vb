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

        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function UnsafeByRefToPtr(Of TValue)(ByRef value As TValue) As InteriorPointer
        End Function

        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function UnsafeByRefToTypedPtr(Of TValue)(ByRef value As TValue) As InteriorPointer(Of TValue)
        End Function

        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function UnsafePtrToByRef(Of TValue)(ptr As InteriorPointer) As TValue ' ByRef TValue
        End Function

        ' Do not use it in the current project.
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function UnsafePtrToByRef(Of TValue)(ptr As InteriorPointer(Of TValue)) As TValue ' ByRef TValue
        End Function

#Disable Warning BC42105
        <Extension>
        <MethodImpl(MethodImplOptions.ForwardRef Or MethodImplOptions.AggressiveInlining)>
        Public Function UnsafeToString(ptr As InteriorPointer(Of Char)) As String
        End Function
#Enable Warning BC42105

    End Module
End Namespace
