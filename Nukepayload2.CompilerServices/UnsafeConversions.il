// Public Module UnsafeConversions
.class public auto ansi sealed Nukepayload2.CompilerServices.Unsafe.UnsafeConversions
       extends [netstandard]System.Object
{
  .custom instance void [netstandard]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
      01 00 00 00
  )
  .custom instance void Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute::.ctor() = ( 01 00 00 00 ) 
  
  // Public Function UnsafeStaticCast(Of TValue, TResult)(value As TValue) As TResult
  .method public static !!TResult UnsafeStaticCast<TValue,TResult>(
    !!TValue 'value'
  ) cil managed aggressiveinlining
  {
    .custom instance void [netstandard]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
        01 00 00 00
    )
    .maxstack 1
    .locals init (
      [0] !!TResult V_0
    )
    ldarg.0
    // Roundtrip via a local to avoid type mismatch on return that the JIT inliner chokes on.
    stloc.0
    ldloc.0
    ret // Return value
  } // End Function
    
  // Public Function UnsafeReinterpretCast(Of TValue, TResult)(value As TValue) As TResult
  .method public static !!TResult UnsafeReinterpretCast<TValue,TResult>(
    !!TValue 'value'
  ) cil managed aggressiveinlining
  {
    .custom instance void [netstandard]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
        01 00 00 00
    )
    .maxstack 1
    .locals init (
      [0] !!TResult V_0
    )
    ldarg.0
    // Roundtrip via a local to avoid type mismatch on return that the JIT inliner chokes on.
    stloc.0
    ldloc.0
    ret // Return value
  } // End Function

  // Public Function UnsafeByRefToPtr(Of TValue)(ByRef value As TValue) As InteriorPointer
  .method public static valuetype Nukepayload2.CompilerServices.InteriorPointer UnsafeByRefToPtr<TValue> (
      !!TValue& 'value'
  ) cil managed aggressiveinlining 
  {
    .custom instance void [netstandard]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
        01 00 00 00
    )
    .maxstack 1
    .locals init (
      [0] valuetype Nukepayload2.CompilerServices.InteriorPointer V_0
    )
    ldarg.0
    // Roundtrip via a local to avoid type mismatch on return that the JIT inliner chokes on.
    stloc.0
    ldloc.0
    ret
  } // End Function
  
  // Public Function UnsafeByRefToPtr(Of TValue)(ByRef value As TValue) As InteriorPointer(Of TValue)
  .method public static valuetype Nukepayload2.CompilerServices.Unsafe.InteriorPointer`1<!!TValue> UnsafeByRefToTypedPtr<TValue> (
      !!TValue& 'value'
  ) cil managed aggressiveinlining 
  {
    .custom instance void [netstandard]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
        01 00 00 00
    )
    .maxstack 1
    .locals init (
      [0] valuetype Nukepayload2.CompilerServices.Unsafe.InteriorPointer`1<!!TValue> V_0
    )
    ldarg.0
    // Roundtrip via a local to avoid type mismatch on return that the JIT inliner chokes on.
    stloc.0
    ldloc.0
    ret
  } // End Function

  // ByRef Public Function UnsafePtrToByRef(Of TValue)(ptr As InteriorPointer) As TValue
  .method public static !!TValue& UnsafePtrToByRef<TValue> (
      valuetype Nukepayload2.CompilerServices.InteriorPointer ptr
  ) cil managed aggressiveinlining 
  {
    .custom instance void [netstandard]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
        01 00 00 00
    )
    .maxstack 1
    .locals init (
      [0] !!TValue& V_0
    )
    ldarg.0
    // Roundtrip via a local to avoid type mismatch on return that the JIT inliner chokes on.
    stloc.0
    ldloc.0
    ret
  } // End Function

  // ByRef Public Function UnsafePtrToByRef(Of TValue)(ptr As InteriorPointer(Of T)) As TValue
  .method public static !!TValue& UnsafePtrToByRef<TValue> (
      valuetype Nukepayload2.CompilerServices.Unsafe.InteriorPointer`1<!!TValue> ptr
  ) cil managed aggressiveinlining 
  {
    .custom instance void [netstandard]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
        01 00 00 00
    )
    .maxstack 1
    .locals init (
      [0] !!TValue& V_0
    )
    ldarg.0
    // Roundtrip via a local to avoid type mismatch on return that the JIT inliner chokes on.
    stloc.0
    ldloc.0
    ret
  } // End Function

  .method public static string UnsafeToString (
      valuetype Nukepayload2.CompilerServices.Unsafe.InteriorPointer`1<char> ptr
  ) cil managed aggressiveinlining 
  {
    .custom instance void [netstandard]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
        01 00 00 00
    )
    .locals init (
        [0] void* addr
    )
	.maxstack 1
    ldarg.0
	ldfld native int valuetype Nukepayload2.CompilerServices.Unsafe.InteriorPointer`1<char>::Value
	stloc.0
	ldloc.0
	newobj instance void [netstandard]System.String::.ctor(char*)
	ret
  }
} // End Module