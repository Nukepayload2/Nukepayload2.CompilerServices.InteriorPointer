.class public sequential ansi sealed beforefieldinit Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<T>
    extends [mscorlib]System.ValueType
    implements class [netstandard]System.IEquatable`1<valuetype Nukepayload2.CompilerServices.TransientPointer>,
               class [netstandard]System.IEquatable`1<valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T>>
{
    .method public specialname static valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T> op_Addition (
            valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T> pointer,
            int32 offset
        ) cil managed aggressiveinlining 
    {
		.maxstack 3
        ldarg.0
		ldfld native int valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T>::Value
		ldarg.1
		sizeof !T
		conv.i
		mul
		add
		ret
	}

	.method public specialname static valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T> op_Subtraction (
        valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T> pointer,
        int32 offset
    ) cil managed aggressiveinlining 
    {
		.maxstack 3
        ldarg.0
		ldfld native int valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T>::Value
		ldarg.1
		sizeof !T
		conv.i
		mul
		sub
		ret
	}

    .method public instance valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T> GetAndIncrement (
	    ) cil managed aggressiveinlining
    {
		.maxstack 4
        ldarg.0 // Me^
        ldobj valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T> // Me
        ldarg.0 // Me, Me^
        ldflda native int valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T>::Value // Me, ByRef Me.Value
        dup // Me, ByRef Me.Value, ByRef Me.Value
        ldind.i // Me, ByRef Me.Value, Me.Value
        sizeof !T // Me, ByRef Me.Value, Me.Value, size
        call native int [netstandard]System.IntPtr::op_Addition(native int,  int32) // Me, ByRef Me.Value, Me.Value + size
        stind.i // Me
        ret
    }

	.method public instance valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T> IncrementAndGet (
	    ) cil managed aggressiveinlining
    {
		.maxstack 3
        ldarg.0 // Me^
        ldflda native int valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T>::Value // ByRef Me.Value
        dup // ByRef Me.Value, ByRef Me.Value
        ldind.i // ByRef Me.Value, Me.Value
        sizeof !T // ByRef Me.Value, Me.Value, size
        call native int [netstandard]System.IntPtr::op_Addition(native int,  int32) // ByRef Me.Value, Me.Value + size
        stind.i //
        ldarg.0 // Me^
        ldobj valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T> // Me
        ret
    }
	
    .method public instance valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T> GetAndDecrement (
	    ) cil managed aggressiveinlining
    {
		.maxstack 4
        ldarg.0 // Me^
        ldobj valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T> // Me
        ldarg.0 // Me, Me^
        ldflda native int valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T>::Value // Me, ByRef Me.Value
        dup // Me, ByRef Me.Value, ByRef Me.Value
        ldind.i // Me, ByRef Me.Value, Me.Value
        sizeof !T // Me, ByRef Me.Value, Me.Value, size
        call native int [netstandard]System.IntPtr::op_Subtraction(native int,  int32) // Me, ByRef Me.Value, Me.Value - size
        stind.i // Me
        ret
    }

	.method public instance valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T> DecrementAndGet (
	    ) cil managed aggressiveinlining
    {
		.maxstack 3
        ldarg.0 // Me^
        ldflda native int valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T>::Value // ByRef Me.Value
        dup // ByRef Me.Value, ByRef Me.Value
        ldind.i // ByRef Me.Value, Me.Value
        sizeof !T // ByRef Me.Value, Me.Value, size
        call native int [netstandard]System.IntPtr::op_Subtraction(native int,  int32) // ByRef Me.Value, Me.Value - size
        stind.i //
        ldarg.0 // Me^
        ldobj valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T> // Me
        ret
    }

	.method public instance void CopyUnmanagedFrom (
        valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T> other
    ) cil managed aggressiveinlining 
    {
		.maxstack 2
        ldarg.0 // Me^
        ldfld native int valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T>::Value // Me.Value
		ldarg.1 // Me.Value, other
        ldfld native int valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T>::Value // Me.Value, other.Value
		ldobj !T // Me.Value, value
		stobj !T
		ret
    }

    .method public specialname instance !T 
          get_UnmanagedItem() cil managed aggressiveinlining
    {
        .maxstack 1
		.locals (void*)
        ldarg.0 // Me^
        ldfld native int valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T>::Value // Me.Value
		stloc.0
		ldloc.0
		ldobj !T
		ret
    }
	
    .method public specialname instance void 
          set_UnmanagedItem(!T 'value') cil managed aggressiveinlining
    {
		.maxstack 2
		.locals (void*)
        ldarg.0 // Me^
        ldfld native int valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T>::Value // Me.Value
		stloc.0
		ldloc.0
		ldarg.1 // pValue, other
        stobj !T
		ret
    }

    .method public instance void 
          SetUnmanagedItem(!T 'value') cil managed aggressiveinlining
    {
		.maxstack 2
		.locals (void*)
        ldarg.0 // Me^
        ldfld native int valuetype Nukepayload2.CompilerServices.Unsafe.TransientPointer`1<!T>::Value // Me.Value
		stloc.0
		ldloc.0
		ldarg.1 // pValue, other
        stobj !T
		ret
    }
}