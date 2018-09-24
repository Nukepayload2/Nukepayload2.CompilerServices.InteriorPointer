Imports System.Globalization

Module Program
    Sub Main(args As String())
        With New Stopwatch
            Const Count = 1000_0000
            Dim values = {0, 0.123456, -0.123456, 234567.89, -234567.89, 1 / 3, -1 / 3, 1.23E+245, 1.23456E-45, -1.23E+45, -1.23E-45, -1.23E-9}
            Dim result As String = Nothing
            For Each value In values
                Console.WriteLine($"===========    {value}    ===========")
                Console.WriteLine("System.Double.ToString")
                .Restart()
                Dim cult = CultureInfo.InvariantCulture
                For i = 1 To Count
                    result = value.ToString(cult)
                Next
                .Stop()
                Console.WriteLine($"Result {result}, Time { .ElapsedMilliseconds}ms.")
                Console.WriteLine("Grisu.ToString")
                .Restart()
                For i = 1 To Count
                    result = Grisu.ToString(value)
                Next
                .Stop()
                Console.WriteLine($"Result {result}, Time { .ElapsedMilliseconds}ms.")
            Next
        End With
    End Sub
End Module
