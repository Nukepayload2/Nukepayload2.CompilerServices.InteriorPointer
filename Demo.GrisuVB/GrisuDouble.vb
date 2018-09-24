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

Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

<StructLayout(LayoutKind.Explicit)>
Friend Structure GrisuDouble
    Private Const kSignMask As ULong = &H8000000000000000UL
    Private Const kExponentMask As ULong = &H7FF0000000000000
    Private Const kSignificandMask As ULong = &HFFFFFFFFFFFFFL
    Private Const kHiddenBit As ULong = &H10000000000000
    Private Const kPhysicalSignificandSize As Integer = 52 ' Excludes the hidden bit.
    Private Const kSignificandSize As Integer = 53

    Private Const kExponentBias As Integer = &H3FF + kPhysicalSignificandSize
    Private Const kDenormalExponent As Integer = -kExponentBias + 1
    Private Const kMaxExponent As Integer = &H7FF - kExponentBias
    Private Const kInfinity As ULong = &H7FF0000000000000

    Public Sub New(d As Double)
        Value = d
    End Sub

    ' The value encoded by this Double must be strictly greater than 0.
    Public Function AsNormalizedDiyFp() As DiyFp
        Debug.Assert(Value > 0.0)

        Dim d64 As ULong = UInt64Value
        Dim f As ULong
        Dim e As Integer
        If IsDenormal Then
            f = d64 And kSignificandMask
            e = kDenormalExponent
        Else
            f = (d64 And kSignificandMask) + kHiddenBit
            e = CInt((d64 And kExponentMask) >> kPhysicalSignificandSize) - kExponentBias
        End If

        ' The current double could be a denormal.
        Do While (f And kHiddenBit) = 0UL
            f <<= 1
            e -= 1
        Loop
        ' Do the final shifts in one go.
        f <<= DiyFp.kSignificandSize - kSignificandSize
        e -= DiyFp.kSignificandSize - kSignificandSize
        Return New DiyFp(f, e)
    End Function

    ' Returns true if the double is a denormal.
    Public ReadOnly Property IsDenormal As Boolean
        Get
            Return (UInt64Value And kExponentMask) = 0UL
        End Get
    End Property

    ' We consider denormals not to be special.
    ' Hence only Infinity and NaN are special.
    Public ReadOnly Property IsSpecial As Boolean
        Get
            Return (UInt64Value And kExponentMask) = kExponentMask
        End Get
    End Property

    ' Computes the two boundaries of this.
    ' The bigger boundary (m_plus) is normalized. The lower boundary has the same
    ' exponent as m_plus.
    ' Precondition: the value encoded by this Double must be greater than 0.
    Public Sub NormalizedBoundaries(<Out> ByRef out_m_minus As DiyFp, <Out> ByRef out_m_plus As DiyFp)
        Debug.Assert(Value > 0.0)

        Dim d64 As ULong = UInt64Value
        Dim vF As ULong
        Dim vE As Integer
        If IsDenormal Then
            vF = d64 And kSignificandMask
            vE = kDenormalExponent
        Else
            vF = (d64 And kSignificandMask) + kHiddenBit
            vE = CInt((d64 And kExponentMask) >> kPhysicalSignificandSize) - kExponentBias
        End If

        Dim plusF As ULong = (vF << 1UL) + 1UL
        Dim plusE As Integer = vE - 1

        Normalize(out_m_minus, out_m_plus, vF, vE, plusF, plusE)
    End Sub

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Private Shared Sub Normalize(ByRef out_m_minus As DiyFp, ByRef out_m_plus As DiyFp, vF As ULong, vE As Integer, ByRef plusF As ULong, ByRef plusE As Integer)
        Const k10MSBits As ULong = &HFFC0000000000000UL
        Const kUint64MSB As ULong = &H8000000000000000UL
        Do While (plusF And k10MSBits) = 0UL
            plusF <<= 10
            plusE -= 10
        Loop
        Do While (plusF And kUint64MSB) = 0UL
            plusF <<= 1
            plusE -= 1
        Loop

        Dim minusF As ULong
        Dim minusE As Integer
        Dim significand_is_zero As Boolean = vF = kHiddenBit
        If significand_is_zero AndAlso vE <> kDenormalExponent Then
            ' The boundary is closer. Think of v = 1000e10 and v- = 9999e9.
            ' Then the boundary (== (v - v-)/2) is not just at a distance of 1e9 but
            ' at a distance of 1e8.
            ' The only exception is for the smallest normal: the largest denormal is
            ' at the same distance as its successor.
            ' Note: denormals have the same exponent as the smallest normals.
            minusF = (vF << 2UL) - 1UL
            minusE = vE - 2
        Else
            minusF = (vF << 1UL) - 1UL
            minusE = vE - 1
        End If
        out_m_minus = New DiyFp(minusF << (minusE - plusE), plusE)
        out_m_plus = New DiyFp(plusF, plusE)
    End Sub

    <FieldOffset(0)>
    Public UInt64Value As ULong
    <FieldOffset(0)>
    Public Value As Double
End Structure
