.class public auto ansi sealed Nukepayload2.CompilerServices.UncheckedConversions
    extends [mscorlib]System.Object
{
    .custom instance void [netstandard]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
        01 00 00 00
    )
    .custom instance void Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute::.ctor() = (
        01 00 00 00
    )

	.method public static char UncheckedToChar (
        int32 ch
    ) cil managed aggressiveinlining 
    {
        .custom instance void [netstandard]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
            01 00 00 00
        )
		.maxstack 1
        ldarg.0
		conv.u2
        ret
    }

    .method public static int32 UncheckedToInt32 (
        char ch
    ) cil managed aggressiveinlining 
    {
        .custom instance void [netstandard]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
            01 00 00 00
        )
		.maxstack 1
        ldarg.0
        ret
    }
}