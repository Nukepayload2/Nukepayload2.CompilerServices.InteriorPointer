// Friend Class UnsafeHelper
.class private auto ansi Nukepayload2.CompilerServices.Unsafe.UnsafeOperators
    extends [netstandard]System.Object
{
    // Public Shared Function SizeOf(Of T)() As Integer
	.method public static int32 SizeOf<T> () cil managed aggressiveinlining 
    {
        .maxstack 1
        sizeof !!T
        ret
    } // End Function

	// Public Shared Function IsGreaterThan(ptr1 As IntPtr, ptr2 As IntPtr) As Boolean
    .method public static bool IsGreaterThan (
            native int ptr1,
            native int ptr2
        ) cil managed aggressiveinlining 
    {
        .maxstack 2
        ldarg.0
        ldarg.1
        cgt.un
        ret
    } // End Function
	
	// Public Shared Function IsLessThan(ptr1 As IntPtr, ptr2 As IntPtr) As Boolean
    .method public static bool IsLessThan (
            native int ptr1,
            native int ptr2
        ) cil managed aggressiveinlining 
    {
        .maxstack 2
        ldarg.0
        ldarg.1
        clt.un
        ret
    } // End Function

} // End Class