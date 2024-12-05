using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Gitan.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class IntLongUtf8FormatBenchmark
{
    static uint Div1000(uint value) => value * 8389 >> 23;
    static uint Div100(uint value) => value * 5243 >> 19;
    static uint Div10(uint value) => value * 6554 >> 16;

    public static int IntWriteUtf8(int value, Span<byte> destination)
    {

        int offset = 0;
        int charsWritten;

        uint num1, num2, num3, div;
        int valueB;

        if (value < 0)
        {
            if (value == int.MinValue)
            {
                ReadOnlySpan<byte> minValue = "-2147483648"u8;

                charsWritten = minValue.Length;
                minValue.CopyTo(destination);
                return charsWritten;
            }

            destination[offset++] = (byte)'-';
            valueB = unchecked(-value);
        }
        //else
        //{
        //    valueB = value;
        //}

        if (value < 10000)
        {
            num1 = (uint)value;
            if (num1 < 10) { goto L1; }
            if (num1 < 100) { goto L2; }
            if (num1 < 1000) { goto L3; }
            goto L4;
        }
        else
        {
            valueB = value / 10000;
            num1 = (uint)(value - valueB * 10000);
            if (valueB < 10000)
            {
                num2 = (uint)valueB;
                if (num2 < 100)
                {
                    if (num2 < 10) { goto L5; }
                    goto L6;
                }
                if (num2 < 1000) { goto L7; }
                goto L8;
            }
            else
            {
                value = valueB / 10000;
                num2 = (uint)(valueB - value * 10000);

                num3 = (uint)value;
                if (num3 < 100)
                {
                    if (num3 < 10) { goto L9; }
                    goto L10;
                }
                if (num3 < 1000) { goto L11; }
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
                destination[offset++] = (byte)('0' + num3);

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

        charsWritten = offset;
        return charsWritten;
    }

    public static long LongWriteUtf8(long value, Span<byte> destination)
    {

        int offset = 0;
        int charsWritten;

        uint num1, num2, num3, div;
        long valueB;

        if (value < 0)
        {
            if (value == long.MinValue)
            {
                ReadOnlySpan<byte> minValue = "-9223372036854775808"u8;

                charsWritten = minValue.Length;
                minValue.CopyTo(destination);
                return charsWritten;
            }

            destination[offset++] = (byte)'-';
            valueB = unchecked(-value);
        }
        //else
        //{
        //    valueB = value;
        //}

        if (value < 10000)
        {
            num1 = (uint)value;
            if (num1 < 10) { goto L1; }
            if (num1 < 100) { goto L2; }
            if (num1 < 1000) { goto L3; }
            goto L4;
        }
        else
        {
            valueB = value / 10000;
            num1 = (uint)(value - valueB * 10000);
            if (valueB < 10000)
            {
                num2 = (uint)valueB;
                if (num2 < 100)
                {
                    if (num2 < 10) { goto L5; }
                    goto L6;
                }
                if (num2 < 1000) { goto L7; }
                goto L8;
            }
            else
            {
                value = valueB / 10000;
                num2 = (uint)(valueB - value * 10000);

                num3 = (uint)value;
                if (num3 < 100)
                {
                    if (num3 < 10) { goto L9; }
                    goto L10;
                }
                if (num3 < 1000) { goto L11; }
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
                destination[offset++] = (byte)('0' + num3);

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

        charsWritten = offset;
        return charsWritten;
    }


    int i = 1234567890;
    long l = 1234567890;
    //int imin = int.MinValue;
    //int imin2 = int.MinValue + 1;
    //long lmin = long.MinValue;
    //long lmin2 = long.MinValue + 1;

//#if NET8_0_OR_GREATER
    [Benchmark]
    public bool IntTryFormat()
    {
        Span<byte> buffer = stackalloc byte[20];

        var result = i.TryFormat(buffer, out int charWriten);

        return result;
    }

    [Benchmark]
    public bool LongTryFormat()
    {
        Span<byte> buffer = stackalloc byte[20];

        var result = l.TryFormat(buffer, out int charWriten);

        return result;
    }

//#endif

    [Benchmark]
    public int IntUtf8Write()
    {
        Span<byte> buffer = stackalloc byte[20];

        var result = IntWriteUtf8(i, buffer);

        return result;
    }  
    
    [Benchmark]
    public long LongUtf8Write()
    {
        Span<byte> buffer = stackalloc byte[20];

        var result = LongWriteUtf8(l, buffer);

        return result;
    }
}

