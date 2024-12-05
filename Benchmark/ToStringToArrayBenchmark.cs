using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Gitan.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class ToStringToArrayBenchmark
{
    static byte[] _buffer = new byte[1024];
    static long _fixedPointValue = -1234__5678_0000;
    static double _doubleValue = -1234.5678;
    static decimal _decimalValue = -1234.5678m;

    static uint Div1000(uint value) => value * 8389 >> 23;
    static uint Div100(uint value) => value * 5243 >> 19;
    static uint Div10(uint value) => value * 6554 >> 16;

    public static bool TryWriteUtf8(long value, Span<byte> destination, out int writtenLength)
    {

        writtenLength = 0;

        int offset = 0;
        int offset2;
        uint num1, num2, num3, num4, num5, num6, num7, num8, div;
        ulong valueA, valueB;

        if (value < 0)
        {
            if (value == long.MinValue)
            {
                ReadOnlySpan<byte> minValue = "-92233720368.54775808"u8;

                writtenLength = minValue.Length;
                return minValue.TryCopyTo(destination);
            }

            if (destination.Length == 0) { return false; }
            destination[offset++] = (byte)'-';
            valueB = (ulong)(unchecked(-value));
        }
        else
        {
            valueB = (ulong)value;
        }

        valueA = valueB / 1__0000_0000;

        uint underPoint = (uint)(valueB - (valueA * 1__0000_0000));

        if (valueA < 10000)
        {
            num1 = (uint)valueA;
            if (num1 < 10)
            {
                if (destination.Length < offset + 1) { return false; }
                goto L1;
            }
            if (num1 < 100)
            {
                if (destination.Length < offset + 2) { return false; }
                goto L2;
            }
            if (num1 < 1000)
            {
                if (destination.Length < offset + 3) { return false; }
                goto L3;
            }
            if (destination.Length < offset + 4) { return false; }
            goto L4;
        }
        else
        {
            valueB = valueA / 10000;
            num1 = (uint)(valueA - valueB * 10000);
            if (valueB < 10000)
            {
                num2 = (uint)valueB;
                if (num2 < 100)
                {
                    if (num2 < 10)
                    {
                        if (destination.Length < offset + 5) { return false; }
                        goto L5;
                    }
                    if (destination.Length < offset + 6) { return false; }
                    goto L6;
                }
                if (num2 < 1000)
                {
                    if (destination.Length < offset + 7) { return false; }
                    goto L7;
                }
                if (destination.Length < offset + 8) { return false; }
                goto L8;
            }
            else
            {
                valueA = valueB / 10000;
                num2 = (uint)(valueB - valueA * 10000);
                num3 = (uint)valueA;
                if (num3 < 100)
                {
                    if (num3 < 10)
                    {
                        if (destination.Length < offset + 9) { return false; }
                        goto L9;
                    }
                    if (destination.Length < offset + 10) { return false; }
                    goto L10;
                }
                if (num3 < 1000)
                {
                    if (destination.Length < offset + 11) { return false; }
                    goto L11;
                }
                if (destination.Length < offset + 12) { return false; }
                goto L12;

            L12:
                destination[offset++] = (byte)('0' + (div = Div1000(num3)));
                num3 -= div * 1000;
            L11:
                destination[offset++] = (byte)('0' + (div = Div100(num3)));
                num3 -= div * 100;
            L10:
                destination[offset++] = (byte)('0' + (div = Div10(num3)));
                num3 -= div * 10;
            L9:
                destination[offset++] = ((byte)('0' + (num3)));
            }
        L8:
            destination[offset++] = (byte)('0' + (div = Div1000(num2)));
            num2 -= div * 1000;
        L7:
            destination[offset++] = (byte)('0' + (div = Div100(num2)));
            num2 -= div * 100;
        L6:
            destination[offset++] = (byte)('0' + (div = Div10(num2)));
            num2 -= div * 10;
        L5:
            destination[offset++] = (byte)('0' + num2);
        }

    L4:
        destination[offset++] = (byte)('0' + (div = Div1000(num1)));
        num1 -= div * 1000;
    L3:
        destination[offset++] = (byte)('0' + (div = Div100(num1)));
        num1 -= div * 100;
    L2:
        destination[offset++] = (byte)('0' + (div = Div10(num1)));
        num1 -= div * 10;
    L1:
        destination[offset++] = (byte)('0' + num1);

        if (underPoint > 0)
        {
            num8 = underPoint;

            num8 -= (num4 = num8 / 10000) * 10000; // 実行後、num4:1～4桁目の値 num8:5～8桁目の値
            num4 -= (num2 = Div100(num4)) * 100; // 実行後、num2:1～2桁目の値 num4:3～4桁目の値
            num2 -= (num1 = Div10(num2)) * 10; // 実行後、num1:1桁目の値 num2:2桁目の値

            if (num8 > 0) // 5～8桁を評価
            {
                // 小数点以下出力は、5桁以上
                num4 -= (num3 = Div10(num4)) * 10; // 実行後、num3:3桁目の値 num4:4桁目の値
                num8 -= (num6 = Div100(num8)) * 100; // 実行後、num6:5～6桁目の値 num8:7～8桁目の値
                num6 -= (num5 = Div10(num6)) * 10; // 実行後、num5:5桁目の値 num6:6桁目の値
                if (num8 > 0) // 7～8桁を評価
                {
                    // 小数点以下出力は、7or8桁
                    num8 -= (num7 = Div10(num8)) * 10; // 実行後、num7:7桁目の値 num8:8桁目の値
                    if (num8 > 0) // 8桁を評価
                    {
                        // 小数点以下出力は、8桁
                        offset2 = offset += 9;
                        if (destination.Length < offset) { return false; }
                        goto LM8;
                    }
                    else
                    {
                        // 小数点以下出力は、7桁
                        offset2 = offset += 8;
                        if (destination.Length < offset) { return false; }
                        goto LM7;
                    }
                }
                else
                {
                    // 小数点以下出力は、5or6桁
                    if (num6 > 0) // 6桁を評価
                    {
                        // 小数点以下出力は、6桁
                        offset2 = offset += 7;
                        if (destination.Length < offset) { return false; }
                        goto LM6;
                    }
                    else
                    {
                        // 小数点以下出力は、5桁
                        offset2 = offset += 6;
                        if (destination.Length < offset) { return false; }
                        goto LM5;
                    }
                }
            }
            else
            {
                // 小数点以下出力は、4桁以下
                if (num4 > 0) // 3～4桁を評価
                {
                    // 小数点以下出力は、3or4桁
                    num4 -= (num3 = Div10(num4)) * 10; // 実行後、num3:3桁目の値 num4:4桁目の値
                    if (num4 > 0) // 4桁を評価
                    {
                        // 小数点以下出力は、4桁
                        offset2 = offset += 5;
                        if (destination.Length < offset) { return false; }
                        goto LM4;
                    }
                    else
                    {
                        // 小数点以下出力は、3桁
                        offset2 = offset += 4;
                        if (destination.Length < offset) { return false; }
                        goto LM3;
                    }
                }
                else
                {
                    // 小数点以下出力は、1or2桁
                    if (num2 > 0) // 2桁を評価
                    {
                        // 小数点以下出力は、2桁
                        offset2 = offset += 3;
                        if (destination.Length < offset) { return false; }
                        goto LM2;
                    }
                    else
                    {
                        // 小数点以下出力は、1桁
                        offset2 = offset += 2;
                        if (destination.Length < offset) { return false; }
                        goto LM1;
                    }
                }
            }

        LM8:
            destination[--offset2] = (byte)('0' + num8);
        LM7:
            destination[--offset2] = (byte)('0' + num7);
        LM6:
            destination[--offset2] = (byte)('0' + num6);
        LM5:
            destination[--offset2] = (byte)('0' + num5);
        LM4:
            destination[--offset2] = (byte)('0' + num4);
        LM3:
            destination[--offset2] = (byte)('0' + num3);
        LM2:
            destination[--offset2] = (byte)('0' + num2);
        LM1:
            destination[--offset2] = (byte)('0' + num1);
            destination[--offset2] = (byte)'.';
        }
        writtenLength = offset;
        return true;
    }

    [Benchmark]
    public string DoubleToString()
    {
        return _doubleValue.ToString();
    }

    [Benchmark]
    public string DecimalToString()
    {
        return _decimalValue.ToString();
    }

    [Benchmark]
    public byte[] FixedPointToByteArray()
    {
        int offset = 0;
        if (TryWriteUtf8(_fixedPointValue, _buffer.AsSpan(offset), out var writtenLength) == false)
        {
            throw new Exception("Buffer不足");
        }
        offset += writtenLength;

        return _buffer.AsSpan(0, offset).ToArray();
    }

    [Benchmark]
    public (byte[], int) FixedPointReturnBuffer()
    {
        int offset = 0;
        if (TryWriteUtf8(_fixedPointValue, _buffer.AsSpan(offset), out var writtenLength) == false)
        {
            throw new Exception("Buffer不足");
        }
        offset += writtenLength;

        return (_buffer, offset);
    }
}

