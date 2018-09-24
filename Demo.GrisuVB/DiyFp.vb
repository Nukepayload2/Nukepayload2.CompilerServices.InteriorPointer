' Copyright 2010 the V8 project authors. All rights reserved.
' Copyright 2011-2012, Kevin Ring. All rights reserved.
' Redistribution and use in source and binary forms, with or without
' modification, are permitted provided that the following conditions are
' met:
'
'     * Redistributions of source code must retain the above copyright
'       notice, this list of conditions and the following disclaimer.
'     * Redistributions in binary form must reproduce the above
'       copyright notice, this list of conditions and the following
'       disclaimer in the documentation and/or other materials provided
'       with the distribution.
'     * Neither the name of Google Inc. nor the names of its
'       contributors may be used to endorse or promote products derived
'       from this software without specific prior written permission.
'
' THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
' "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
' LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
' A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
' OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
' SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
' LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
' DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
' THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
' (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
' OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

' This "Do It Yourself Floating Point" class implements a floating-point number
' with a uint64 significand and an int exponent. Normalized DiyFp numbers will
' have the most significant bit of the significand set.
' Multiplication and Subtraction do not normalize their results.
' DiyFp are not designed to contain special doubles (NaN and Infinity).
Friend Structure DiyFp
    Public Const kSignificandSize As Integer = 64

    Public Sub New(f As ULong, e As Integer)
        Me.F = f
        Me.E = e
    End Sub

    ' this = this - other.
    ' The exponents of both numbers must be the same and the significand of this
    ' must be bigger than the significand of other.
    ' The result will not be normalized.
    Public Sub Subtract(ByRef other As DiyFp)
        Debug.Assert(E = other.E)
        Debug.Assert(F >= other.F)
        F -= other.F
    End Sub

    ' Returns a - b.
    ' The exponents of both numbers must be the same and this must be bigger
    ' than other. The result will not be normalized.
    Public Shared Function Minus(ByRef a As DiyFp, ByRef b As DiyFp) As DiyFp
        Dim result As DiyFp = a
        result.Subtract(b)
        Return result
    End Function

    ' this = this * other.
    Public Sub Multiply(ByRef other As DiyFp)
        ' Simply "emulates" a 128 bit multiplication.
        ' However: the resulting number only contains 64 bits. The least
        ' significant 64 bits are only used for rounding the most significant 64
        ' bits.
        Const kM32 As ULong = &HFFFFFFFFUI
        Dim a As ULong = F >> 32
        Dim b As ULong = F And kM32
        Dim c As ULong = other.F >> 32
        Dim d As ULong = other.F And kM32
        Dim ac As ULong = a * c
        Dim bc As ULong = b * c
        Dim ad As ULong = a * d
        Dim bd As ULong = b * d
        Dim tmp As ULong = (bd >> 32) + (ad And kM32) + (bc And kM32)
        ' By adding 1U << 31 to tmp we round the final result.
        ' Halfway cases will be round up.
        tmp += 1UI << 31
        Dim result_f As ULong = ac + (ad >> 32) + (bc >> 32) + (tmp >> 32)
        E += other.E + 64
        F = result_f
    End Sub

    Public Sub Normalize()
        Debug.Assert(F <> 0)
        Dim f1 As ULong = F
        Dim e1 As Integer = E

        ' This method is mainly called for normalizing boundaries. In general
        ' boundaries need to be shifted by 10 bits. We thus optimize for this case.
        Const k10MSBits As ULong = &HFFC0000000000000UL
        Do While (f1 And k10MSBits) = 0
            f1 <<= 10
            e1 -= 10
        Loop
        Do While (f1 And kUint64MSB) = 0
            f1 <<= 1
            e1 -= 1
        Loop
        F = f1
        E = e1
    End Sub

    Private Const kUint64MSB As ULong = &H8000000000000000UL

    Public F As ULong
    Public E As Integer
End Structure
