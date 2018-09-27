.class public auto ansi Nukepayload2.CompilerServices.Unsafe.UnsafeOperators
    extends [netstandard]System.Object
{
	.method public static int32 SizeOf<T> () cil managed aggressiveinlining 
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

	.method private static valuetype Nukepayload2.CompilerServices.Unsafe.InteriorPointer`1<char> 
        StrPtrInternal(string str) cil managed aggressiveinlining
	{
	    .maxstack 2
	    .locals init (
			[0] string pinned sp,
			[1] valuetype Nukepayload2.CompilerServices.Unsafe.InteriorPointer`1<char> result
		)

        ldarg.0
		stloc.0
		ldloc.0
        conv.u
        call int32 [netstandard]System.Runtime.CompilerServices.RuntimeHelpers::get_OffsetToStringData()
        add
        stloc.1
        ldloc.1
        ret
	} // End Function

} // End Class