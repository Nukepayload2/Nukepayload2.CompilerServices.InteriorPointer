Imports BenchmarkDotNet.Attributes
Imports BenchmarkDotNet.Running
Imports Nukepayload2.CompilerServices.Unsafe

Module Program
    Sub Main(args As String())
        BenchmarkRunner.Run(GetType(ArrayAccess), GetType(StringAccess))
    End Sub
End Module

<CoreJob>
<RankColumn>
Public Class ArrayAccess
    Private Const LoopCount = 5000_0000
    Private _testData As Char()

    <GlobalSetup>
    Public Sub Initialize()
        ReDim _testData(100)
    End Sub

    <Benchmark>
    Public Sub ForEachArray()
        Dim data = _testData
        Dim ch As Char
        For i = 1 To LoopCount
            For j = 1 To 4
                ch = data(j)
            Next
        Next
    End Sub

    <Benchmark>
    Public Sub ForEachSpan()
        Dim data = _testData.AsSpan
        Dim ch As Char
        For i = 1 To LoopCount
            For j = 1 To 4
                ch = data(j)
            Next
        Next
    End Sub

    <Benchmark>
    Public Sub ForEachVarPtrArray()
        Dim data = VarPtr(_testData(0))
        Dim ch As Char
        For i = 1 To LoopCount
            Dim ptr = data
            For j = 1 To 4
                ch = ptr.IncrementAndGet.UnmanagedItem
            Next
        Next
    End Sub

End Class

<CoreJob>
<RankColumn>
Public Class StringAccess
    Private Const LoopCount = 5000_0000
    Private _testData As String = "123456789012345678901234567890"

    <Benchmark>
    Public Sub ForEachString()
        Dim data = _testData
        Dim ch As Char
        For i = 1 To LoopCount
            For j = 1 To 4
                ch = data(j)
            Next
        Next
    End Sub

    <Benchmark>
    Public Sub ForEachSpan()
        Dim data = _testData.AsSpan
        Dim ch As Char
        ch = CSharpFeatures.SpanHelper.ForEachTest(data, LoopCount)
    End Sub

    <Benchmark>
    Public Sub ForEachStrPtrArray()
        Dim pin = StrPtr(_testData)
        Dim data = pin.Pointer
        Dim ch As Char
        For i = 1 To LoopCount
            Dim ptr = data
            For j = 1 To 4
                ch = ptr.IncrementAndGet.UnmanagedItem
            Next
        Next
        pin.Dispose()
    End Sub

End Class
