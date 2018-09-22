// Friend Class UnsafeHelper
.class private auto ansi Nukepayload2.CompilerServices.UnsafeHelper
    extends [netstandard]System.Object
{
    // Public Shared Function SizeOf(Of T)() As Integer
    .method public static int32 SizeOf<T> () cil managed aggressiveinlining
    {
        .maxstack 1
        sizeof !!T
        ret
    } // End Function
} // End Class