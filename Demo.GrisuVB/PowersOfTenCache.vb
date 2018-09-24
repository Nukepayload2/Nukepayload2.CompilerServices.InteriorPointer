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

Friend Class PowersOfTenCache
    ' Not all powers of ten are cached. The decimal exponent of two neighboring
    ' cached numbers will differ by kDecimalExponentDistance.
    Private Const kDecimalExponentDistance As Integer = 8

    Private Const kMinDecimalExponent As Integer = -348
    Private Const kMaxDecimalExponent As Integer = 340

    ' Returns a cached power-of-ten with a binary exponent in the range
    ' [min_exponent; max_exponent] (boundaries included).
    Public Shared Sub GetCachedPowerForBinaryExponentRange(min_exponent As Integer, max_exponent As Integer, <System.Runtime.InteropServices.Out()> ByRef power As DiyFp, <System.Runtime.InteropServices.Out()> ByRef decimal_exponent As Integer)
        Dim kQ As Integer = DiyFp.kSignificandSize
        Dim k As Double = Math.Ceiling((min_exponent + kQ - 1) * kD_1_LOG2_10)
        Dim foo As Integer = kCachedPowersOffset
        Dim index As Integer = (foo + CInt(Math.Truncate(k)) - 1) \ kDecimalExponentDistance + 1
        Debug.Assert(0 <= index AndAlso index < kCachedPowers.Length)
        Dim cached_power As CachedPower = kCachedPowers(index)
        Debug.Assert(min_exponent <= cached_power.binary_exponent)
        Debug.Assert(cached_power.binary_exponent <= max_exponent)
        decimal_exponent = cached_power.decimal_exponent
        power = New DiyFp(cached_power.significand, cached_power.binary_exponent)
    End Sub

    ' Returns a cached power of ten x ~= 10^k such that
    '   k <= decimal_exponent < k + kCachedPowersDecimalDistance.
    ' The given decimal_exponent must satisfy
    '   kMinDecimalExponent <= requested_exponent, and
    '   requested_exponent < kMaxDecimalExponent + kDecimalExponentDistance.
    Public Shared Sub GetCachedPowerForDecimalExponent(requested_exponent As Integer, <System.Runtime.InteropServices.Out()> ByRef power As DiyFp, <System.Runtime.InteropServices.Out()> ByRef found_exponent As Integer)
        Debug.Assert(kMinDecimalExponent <= requested_exponent)
        Debug.Assert(requested_exponent < kMaxDecimalExponent + kDecimalExponentDistance)
        Dim index As Integer = (requested_exponent + kCachedPowersOffset) \ kDecimalExponentDistance
        Dim cached_power As CachedPower = kCachedPowers(index)
        power = New DiyFp(cached_power.significand, cached_power.binary_exponent)
        found_exponent = cached_power.decimal_exponent
        Debug.Assert(found_exponent <= requested_exponent)
        Debug.Assert(requested_exponent < found_exponent + kDecimalExponentDistance)
    End Sub

    Private Structure CachedPower
        Public significand As ULong
        Public binary_exponent As Short
        Public decimal_exponent As Short
    End Structure

    Private Shared ReadOnly kCachedPowers() As CachedPower = {
        New CachedPower() With {
            .significand = &HFA8FD5A0081C0288UL,
            .binary_exponent = -1220,
            .decimal_exponent = -348
        },
        New CachedPower() With {
            .significand = &HBAAEE17FA23EBF76UL,
            .binary_exponent = -1193,
            .decimal_exponent = -340
        },
        New CachedPower() With {
            .significand = &H8B16FB203055AC76UL,
            .binary_exponent = -1166,
            .decimal_exponent = -332
        },
        New CachedPower() With {
            .significand = &HCF42894A5DCE35EAUL,
            .binary_exponent = -1140,
            .decimal_exponent = -324
        },
        New CachedPower() With {
            .significand = &H9A6BB0AA55653B2DUL,
            .binary_exponent = -1113,
            .decimal_exponent = -316
        },
        New CachedPower() With {
            .significand = &HE61ACF033D1A45DFUL,
            .binary_exponent = -1087,
            .decimal_exponent = -308
        },
        New CachedPower() With {
            .significand = &HAB70FE17C79AC6CAUL,
            .binary_exponent = -1060,
            .decimal_exponent = -300
        },
        New CachedPower() With {
            .significand = &HFF77B1FCBEBCDC4FUL,
            .binary_exponent = -1034,
            .decimal_exponent = -292
        },
        New CachedPower() With {
            .significand = &HBE5691EF416BD60CUL,
            .binary_exponent = -1007,
            .decimal_exponent = -284
        },
        New CachedPower() With {
            .significand = &H8DD01FAD907FFC3CUL,
            .binary_exponent = -980,
            .decimal_exponent = -276
        },
        New CachedPower() With {
            .significand = &HD3515C2831559A83UL,
            .binary_exponent = -954,
            .decimal_exponent = -268
        },
        New CachedPower() With {
            .significand = &H9D71AC8FADA6C9B5UL,
            .binary_exponent = -927,
            .decimal_exponent = -260
        },
        New CachedPower() With {
            .significand = &HEA9C227723EE8BCBUL,
            .binary_exponent = -901,
            .decimal_exponent = -252
        },
        New CachedPower() With {
            .significand = &HAECC49914078536DUL,
            .binary_exponent = -874,
            .decimal_exponent = -244
        },
        New CachedPower() With {
            .significand = &H823C12795DB6CE57UL,
            .binary_exponent = -847,
            .decimal_exponent = -236
        },
        New CachedPower() With {
            .significand = &HC21094364DFB5637UL,
            .binary_exponent = -821,
            .decimal_exponent = -228
        },
        New CachedPower() With {
            .significand = &H9096EA6F3848984FUL,
            .binary_exponent = -794,
            .decimal_exponent = -220
        },
        New CachedPower() With {
            .significand = &HD77485CB25823AC7UL,
            .binary_exponent = -768,
            .decimal_exponent = -212
        },
        New CachedPower() With {
            .significand = &HA086CFCD97BF97F4UL,
            .binary_exponent = -741,
            .decimal_exponent = -204
        },
        New CachedPower() With {
            .significand = &HEF340A98172AACE5UL,
            .binary_exponent = -715,
            .decimal_exponent = -196
        },
        New CachedPower() With {
            .significand = &HB23867FB2A35B28EUL,
            .binary_exponent = -688,
            .decimal_exponent = -188
        },
        New CachedPower() With {
            .significand = &H84C8D4DFD2C63F3BUL,
            .binary_exponent = -661,
            .decimal_exponent = -180
        },
        New CachedPower() With {
            .significand = &HC5DD44271AD3CDBAUL,
            .binary_exponent = -635,
            .decimal_exponent = -172
        },
        New CachedPower() With {
            .significand = &H936B9FCEBB25C996UL,
            .binary_exponent = -608,
            .decimal_exponent = -164
        },
        New CachedPower() With {
            .significand = &HDBAC6C247D62A584UL,
            .binary_exponent = -582,
            .decimal_exponent = -156
        },
        New CachedPower() With {
            .significand = &HA3AB66580D5FDAF6UL,
            .binary_exponent = -555,
            .decimal_exponent = -148
        },
        New CachedPower() With {
            .significand = &HF3E2F893DEC3F126UL,
            .binary_exponent = -529,
            .decimal_exponent = -140
        },
        New CachedPower() With {
            .significand = &HB5B5ADA8AAFF80B8UL,
            .binary_exponent = -502,
            .decimal_exponent = -132
        },
        New CachedPower() With {
            .significand = &H87625F056C7C4A8BUL,
            .binary_exponent = -475,
            .decimal_exponent = -124
        },
        New CachedPower() With {
            .significand = &HC9BCFF6034C13053UL,
            .binary_exponent = -449,
            .decimal_exponent = -116
        },
        New CachedPower() With {
            .significand = &H964E858C91BA2655UL,
            .binary_exponent = -422,
            .decimal_exponent = -108
        },
        New CachedPower() With {
            .significand = &HDFF9772470297EBDUL,
            .binary_exponent = -396,
            .decimal_exponent = -100
        },
        New CachedPower() With {
            .significand = &HA6DFBD9FB8E5B88FUL,
            .binary_exponent = -369,
            .decimal_exponent = -92
        },
        New CachedPower() With {
            .significand = &HF8A95FCF88747D94UL,
            .binary_exponent = -343,
            .decimal_exponent = -84
        },
        New CachedPower() With {
            .significand = &HB94470938FA89BCFUL,
            .binary_exponent = -316,
            .decimal_exponent = -76
        },
        New CachedPower() With {
            .significand = &H8A08F0F8BF0F156BUL,
            .binary_exponent = -289,
            .decimal_exponent = -68
        },
        New CachedPower() With {
            .significand = &HCDB02555653131B6UL,
            .binary_exponent = -263,
            .decimal_exponent = -60
        },
        New CachedPower() With {
            .significand = &H993FE2C6D07B7FACUL,
            .binary_exponent = -236,
            .decimal_exponent = -52
        },
        New CachedPower() With {
            .significand = &HE45C10C42A2B3B06UL,
            .binary_exponent = -210,
            .decimal_exponent = -44
        },
        New CachedPower() With {
            .significand = &HAA242499697392D3UL,
            .binary_exponent = -183,
            .decimal_exponent = -36
        },
        New CachedPower() With {
            .significand = &HFD87B5F28300CA0EUL,
            .binary_exponent = -157,
            .decimal_exponent = -28
        },
        New CachedPower() With {
            .significand = &HBCE5086492111AEBUL,
            .binary_exponent = -130,
            .decimal_exponent = -20
        },
        New CachedPower() With {
            .significand = &H8CBCCC096F5088CCUL,
            .binary_exponent = -103,
            .decimal_exponent = -12
        },
        New CachedPower() With {
            .significand = &HD1B71758E219652CUL,
            .binary_exponent = -77,
            .decimal_exponent = -4
        },
        New CachedPower() With {
            .significand = &H9C40000000000000UL,
            .binary_exponent = -50,
            .decimal_exponent = 4
        },
        New CachedPower() With {
            .significand = &HE8D4A51000000000UL,
            .binary_exponent = -24,
            .decimal_exponent = 12
        },
        New CachedPower() With {
            .significand = &HAD78EBC5AC620000UL,
            .binary_exponent = 3,
            .decimal_exponent = 20
        },
        New CachedPower() With {
            .significand = &H813F3978F8940984UL,
            .binary_exponent = 30,
            .decimal_exponent = 28
        },
        New CachedPower() With {
            .significand = &HC097CE7BC90715B3UL,
            .binary_exponent = 56,
            .decimal_exponent = 36
        },
        New CachedPower() With {
            .significand = &H8F7E32CE7BEA5C70UL,
            .binary_exponent = 83,
            .decimal_exponent = 44
        },
        New CachedPower() With {
            .significand = &HD5D238A4ABE98068UL,
            .binary_exponent = 109,
            .decimal_exponent = 52
        },
        New CachedPower() With {
            .significand = &H9F4F2726179A2245UL,
            .binary_exponent = 136,
            .decimal_exponent = 60
        },
        New CachedPower() With {
            .significand = &HED63A231D4C4FB27UL,
            .binary_exponent = 162,
            .decimal_exponent = 68
        },
        New CachedPower() With {
            .significand = &HB0DE65388CC8ADA8UL,
            .binary_exponent = 189,
            .decimal_exponent = 76
        },
        New CachedPower() With {
            .significand = &H83C7088E1AAB65DBUL,
            .binary_exponent = 216,
            .decimal_exponent = 84
        },
        New CachedPower() With {
            .significand = &HC45D1DF942711D9AUL,
            .binary_exponent = 242,
            .decimal_exponent = 92
        },
        New CachedPower() With {
            .significand = &H924D692CA61BE758UL,
            .binary_exponent = 269,
            .decimal_exponent = 100
        },
        New CachedPower() With {
            .significand = &HDA01EE641A708DEAUL,
            .binary_exponent = 295,
            .decimal_exponent = 108
        },
        New CachedPower() With {
            .significand = &HA26DA3999AEF774AUL,
            .binary_exponent = 322,
            .decimal_exponent = 116
        },
        New CachedPower() With {
            .significand = &HF209787BB47D6B85UL,
            .binary_exponent = 348,
            .decimal_exponent = 124
        },
        New CachedPower() With {
            .significand = &HB454E4A179DD1877UL,
            .binary_exponent = 375,
            .decimal_exponent = 132
        },
        New CachedPower() With {
            .significand = &H865B86925B9BC5C2UL,
            .binary_exponent = 402,
            .decimal_exponent = 140
        },
        New CachedPower() With {
            .significand = &HC83553C5C8965D3DUL,
            .binary_exponent = 428,
            .decimal_exponent = 148
        },
        New CachedPower() With {
            .significand = &H952AB45CFA97A0B3UL,
            .binary_exponent = 455,
            .decimal_exponent = 156
        },
        New CachedPower() With {
            .significand = &HDE469FBD99A05FE3UL,
            .binary_exponent = 481,
            .decimal_exponent = 164
        },
        New CachedPower() With {
            .significand = &HA59BC234DB398C25UL,
            .binary_exponent = 508,
            .decimal_exponent = 172
        },
        New CachedPower() With {
            .significand = &HF6C69A72A3989F5CUL,
            .binary_exponent = 534,
            .decimal_exponent = 180
        },
        New CachedPower() With {
            .significand = &HB7DCBF5354E9BECEUL,
            .binary_exponent = 561,
            .decimal_exponent = 188
        },
        New CachedPower() With {
            .significand = &H88FCF317F22241E2UL,
            .binary_exponent = 588,
            .decimal_exponent = 196
        },
        New CachedPower() With {
            .significand = &HCC20CE9BD35C78A5UL,
            .binary_exponent = 614,
            .decimal_exponent = 204
        },
        New CachedPower() With {
            .significand = &H98165AF37B2153DFUL,
            .binary_exponent = 641,
            .decimal_exponent = 212
        },
        New CachedPower() With {
            .significand = &HE2A0B5DC971F303AUL,
            .binary_exponent = 667,
            .decimal_exponent = 220
        },
        New CachedPower() With {
            .significand = &HA8D9D1535CE3B396UL,
            .binary_exponent = 694,
            .decimal_exponent = 228
        },
        New CachedPower() With {
            .significand = &HFB9B7CD9A4A7443CUL,
            .binary_exponent = 720,
            .decimal_exponent = 236
        },
        New CachedPower() With {
            .significand = &HBB764C4CA7A44410UL,
            .binary_exponent = 747,
            .decimal_exponent = 244
        },
        New CachedPower() With {
            .significand = &H8BAB8EEFB6409C1AUL,
            .binary_exponent = 774,
            .decimal_exponent = 252
        },
        New CachedPower() With {
            .significand = &HD01FEF10A657842CUL,
            .binary_exponent = 800,
            .decimal_exponent = 260
        },
        New CachedPower() With {
            .significand = &H9B10A4E5E9913129UL,
            .binary_exponent = 827,
            .decimal_exponent = 268
        },
        New CachedPower() With {
            .significand = &HE7109BFBA19C0C9DUL,
            .binary_exponent = 853,
            .decimal_exponent = 276
        },
        New CachedPower() With {
            .significand = &HAC2820D9623BF429UL,
            .binary_exponent = 880,
            .decimal_exponent = 284
        },
        New CachedPower() With {
            .significand = &H80444B5E7AA7CF85UL,
            .binary_exponent = 907,
            .decimal_exponent = 292
        },
        New CachedPower() With {
            .significand = &HBF21E44003ACDD2DUL,
            .binary_exponent = 933,
            .decimal_exponent = 300
        },
        New CachedPower() With {
            .significand = &H8E679C2F5E44FF8FUL,
            .binary_exponent = 960,
            .decimal_exponent = 308
        },
        New CachedPower() With {
            .significand = &HD433179D9C8CB841UL,
            .binary_exponent = 986,
            .decimal_exponent = 316
        },
        New CachedPower() With {
            .significand = &H9E19DB92B4E31BA9UL,
            .binary_exponent = 1013,
            .decimal_exponent = 324
        },
        New CachedPower() With {
            .significand = &HEB96BF6EBADF77D9UL,
            .binary_exponent = 1039,
            .decimal_exponent = 332
        },
        New CachedPower() With {
            .significand = &HAF87023B9BF0EE6BUL,
            .binary_exponent = 1066,
            .decimal_exponent = 340
        }
    }

    Private Const kCachedPowersOffset As Integer = 348 ' -1 * the first decimal_exponent.
    Private Const kD_1_LOG2_10 As Double = 0.30102999566398114 '  1 / lg(10)
End Class
