.class public auto ansi Nukepayload2.CompilerServices.Unsafe.UnsafeOperators
    extends [netstandard]System.Object
{
	.method assembly static int32 SizeOf<T> () cil managed aggressiveinlining 
    {
        .maxstack 1
        sizeof !!T
        ret
    } // End Function

    .method assembly static bool IsGreaterThan (
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
	
    .method assembly static bool IsLessThan (
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