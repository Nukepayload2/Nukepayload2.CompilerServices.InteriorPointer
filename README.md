# Nukepayload2.CompilerServices.InteriorPointer
Provides functionality for manipulating interior pointers with VB.

[Get on nuget](https://www.nuget.org/packages/Nukepayload2.CompilerServices.InteriorPointer)

- Convert ByRef to interior pointer, or convert interior pointer back to ByRef.
- Load or store unmanaged values indirectly.
- No extra dependencies.
- Supports addition, subtraction, increment, decrement, equality, inequality, greater than, greater than or equal, less than, and less than or equal operator.
- Provides helper functions for easier code conversion from C#, C++, or Classic VB to VB.NET.
- Runtime-check for InteriorPointer(Of T) is enabled.
 
## Scenarios
- You don't want to see small c# projects among your huge VB solution.
- You prefer VB syntax and want to implement pointer algorithms.
- You want to convert c# codes that contain `Span(Of T)` , without marking everything `<Obsolete>`.
- You want to bring Classic VB codes which have `VarPtr` and `StrPtr` to .NET.

## Guides

### Commonly used types
Nukepayload2.CompilerServices.InteriorPointer
Nukepayload2.CompilerServices.Unsafe.InteriorPointer(Of T)
Nukepayload2.CompilerServices.Unsafe.UnsafeOperators
Nukepayload2.CompilerServices.Unsafe.PinnedPointer(Of T)
Nukepayload2.CompilerServices.Unsafe.TransientPointer(Of T)

### C-style operators vs InteriorPointer members

|C# sample code|Remarks|InteriorPointer with VB|
|-|-|-|
|`var value = *p;`|p is a pointer of an unmanaged type|`Dim value = p.UnmanagedItem`|
|`var value = *p++;`|p is a pointer of an unmanaged type|`Dim value = p.GetAndIncrement.UnmanagedItem`|
|`var value = *++p;`|p is a pointer of an unmanaged type|`Dim value = p.IncrementAndGet.UnmanagedItem`|
|`var value = *p--;`|p is a pointer of an unmanaged type|`Dim value = p.GetAndDecrement.UnmanagedItem`|
|`var value = *--p;`|p is a pointer of an unmanaged type|`Dim value = p.DecrementAndGet.UnmanagedItem`|
|`*p = value;`|p is a pointer of an unmanaged type|`p.UnmanagedItem = value`|
|`*p++ = value;`|p is a pointer of an unmanaged type|`p.GetAndIncrement.SetUnmanagedItem(value)`|
|`var p = (T*)Unsafe.AsPointer(ref r);`|Converts ref to raw pointer|`Dim p = r.UnsafeByRefToTypedPtr`|
|`ref T r = Unsafe.AsRef<T>((void*)p);`|p is IntPtr|`Dim r = CType(p, InteriorPointer(Of T))`|
|`T r = Unsafe.AsRef<T>((void*)p);`|p is IntPtr|`Dim r = p.UnsafePtrToByRef(Of T)`|
|`T* r = &p;`|p is structure or T is pinned|`Dim r = VarPtr(p)`|
|`*p1 = *p2;`|p1 is a pointer of an unmanaged type|`p1.CopyUnmanagedFrom(p2)`|
|`p+=2`|p is void*|`p+=2`|
|`p-=2`|p is void*|`p-=2`|
|`p+=2`|p is T*|`p+=2`|
|`p-=2`|p is T*|`p-=2`|
|`p1 < p2`|p1 is pointer|`p1 < p2`|
|`p1 > p2`|p1 is pointer|`p1 > p2`|
|`p1 <= p2`|p1 is pointer|`p1 <= p2`|
|`p1 >= p2`|p1 is pointer|`p1 >= p2`|
|`p1 = (void*)p2`|p1 is pointer|`p1 = CType(p2, InteriorPointer)`|
|`p1 = (T*)p2`|p1 is pointer|`p1 = CType(p2, InteriorPointer(Of T))`|

|C++ sample code|Remarks|InteriorPointer with VB|
|-|-|-|
|`auto u = static_cast<unsigned int>(signedValue)`|Converts objects that has the same size and memory layout.|Dim u = signedValue.UnsafeStaticCast(Of UInteger)|
|`auto u = reinterpret_cast<cow*>(wind)`|Converts between irrelevant types.|Dim u = wind.UnsafeReinterpretCast(Of Cow)|

### C# pointer keywords to VB with InteriorPointer
#### stackalloc
__C#__
```C#
char* chrBuf = stackalloc char[50];
```
__VB__
```VB
<StructLayout(LayoutKind.Sequential, Size:=50 * 2)>
Structure StackAllocChar50
End Structure

Dim chrBuf As InteriorPointer(Of Char) = resultBufValue.UnsafeByRefToPtr
```

#### fixed
##### Fixed array

__C#__
```C#
char[] values = new char[4096];
fixed(char* chrBuf = &values[0])
{
    // use chrBuf
}
```
__VB__
```VB
Dim values(4095) As Char
Using pinPtr = Fixed(values)
    Dim chrBuf = pinPtr.Pointer
    ' use chrBuf
End Using
```

##### Fixed string

__C#__
```C#
var values = new string('0', 4096);
fixed(char* chrBuf = values)
{
    // use chrBuf
}
```

__VB__
```VB
Dim values As New String("0"c, 4096)
Using pinPtr = StrPtr(values)
    Dim chrBuf = pinPtr.Pointer
    ' use chrBuf
End Using
```

##### ref struct

__C#__
```C#
public ref struct StackStringBuilder {}
```

__VB__
```VB
Namespace Global.System.Runtime.CompilerServices
    <CompilerGenerated>
    <Embedded>
    Friend NotInheritable Class IsByRefLikeAttribute
        Inherits Attribute
        Public Sub New()
            MyBase.New()
        End Sub
    End Class
End Namespace

<IsByRefLike>
Public Structure StackStringBuilder
End Structure
```

##### readonly ref struct

__C#__
```C#
public readonly ref struct StackString {}
```

__VB__
```VB
Namespace Global.System.Runtime.CompilerServices
    <CompilerGenerated>
    <Embedded>
    Friend NotInheritable Class IsByRefLikeAttribute
        Inherits Attribute
        Public Sub New()
            MyBase.New()
        End Sub
    End Class

    <CompilerGenerated>
    <Embedded>
    Friend NotInheritable Class IsReadOnlyAttribute
        Inherits Attribute
        Public Sub New()
            MyBase.New()
        End Sub
    End Class
End Namespace

<IsByRefLike, IsReadOnly>
Public Structure StackString
End Structure
```

### Restrictions
The `InteriorPointer` and the `InteriorPointer(Of T)` may lose their targets if they're used as 
the data type of:
- An array element
- Field of class
- Field of structure which is a field of class (directly or indirectly)
- Anonymous type member
- Generic type argument
- `Async Function` locals or argument
- `Async Sub` locals or argument
- `Iterator Function` locals or argument
- Lambda expression locals or argument
- Anonymous delegate locals or argument
- Locals as `Object` or `ValueType`
- Parameters as `Object` or `ValueType`
- `Static` locals

## Known issues
- The compile-time checking for the unsafe use of `InteriorPointer` and `InteriorPointer(Of T)` is incomplete. For more information, see [VBRefStructHelper](https://github.com/Nukepayload2/VBRefStructHelper).
