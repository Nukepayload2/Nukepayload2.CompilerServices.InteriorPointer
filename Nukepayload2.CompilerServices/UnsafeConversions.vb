Imports System.Runtime.CompilerServices

Namespace Unsafe
    Public Module UnsafeConversions
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
    End Module
End Namespace
