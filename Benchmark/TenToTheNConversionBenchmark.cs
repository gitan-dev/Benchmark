using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Runtime.CompilerServices;

namespace Gitan.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class TenToTheNConversionBenchmark
{
    static long sourceValue = 123;
    static int powerValue = 11;

    [Benchmark]
    public long MathPowBench()
    {
        return Power0(sourceValue, powerValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Power0(long baseNumber, int power)
    {
        return (long)(baseNumber * Math.Pow(10, power));
    }

    [Benchmark]
    public long ForPower10Bench()
    {
        return Power1(sourceValue, powerValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Power1(long baseNumber, int power)
    {
        var value = baseNumber;
        for (int i = 0; i < power; i++)
        {
            value *= 10;
        }
        return value;
    }

    [Benchmark]
    public long WhileMinusPower10Bench()
    {
        return Power2(sourceValue, powerValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Power2(long baseNumber, int power)
    {
        var value = baseNumber;
        int i = power;
        while (i > 0)
        {
            value *= 10;
            i--;
        }
        return value;
    }

    [Benchmark]
    public long WhileMinus4Power10Bench()
    {
        return Power3(sourceValue, powerValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Power3(long baseNumber, int power)
    {
        var value = baseNumber;
        int i = power;
        while (i > 3)
        {
            value *= 10000;
            i -= 4;
        }
        while (i > 0)
        {
            value *= 10;
            i--;
        }
        return value;
    }

    [Benchmark]
    public long SwitchStatementBench()
    {
        return Power4(sourceValue, powerValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Power4(long baseNumber, int power)
    {
        switch (power)
        {
            case 1:
                return baseNumber * 10;
            case 2:
                return baseNumber * 100;
            case 3:
                return baseNumber * 1000;
            case 4:
                return baseNumber * 10_000;
            case 5:
                return baseNumber * 100_000;
            case 6:
                return baseNumber * 1_000_000;
            case 7:
                return baseNumber * 10_000_000;
            case 8:
                return baseNumber * 100_000_000;
            case 9:
                return baseNumber * 1_000_000_000;
            case 10:
                return baseNumber * 10_000_000_000;
            case 11:
                return baseNumber * 100_000_000_000;
            case 12:
                return baseNumber * 1_000_000_000_000;
            case 13:
                return baseNumber * 10_000_000_000_000;
            case 14:
                return baseNumber * 100_000_000_000_000;
            case 15:
                return baseNumber * 1_000_000_000_000_000;
            case 16:
                return baseNumber * 10_000_000_000_000_000;
            case 17:
                return baseNumber * 100_000_000_000_000_000;
            case 18:
                return baseNumber * 1_000_000_000_000_000_000;
            default:
                return -1;
        }
    }

    [Benchmark]
    public long SwitchExpressionBench()
    {
        return Power5(sourceValue, powerValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Power5(long baseNumber, int power)
    {
        return baseNumber * (power switch
        {
            1 => 10,
            2 => 100,
            3 => 1000,
            4 => 10_000,
            5 => 100_000,
            6 => 1000_000,
            7 => 10_000_000,
            8 => 100_000_000,
            9 => 1000_000_000,
            10 => 10_000_000_000,
            11 => 100_000_000_000,
            12 => 1000_000_000_000,
            13 => 10_000_000_000_000,
            14 => 100_000_000_000_000,
            15 => 1000_000_000_000_000,
            16 => 10_000_000_000_000_000,
            17 => 100_000_000_000_000_000,
            18 => 1000_000_000_000_000_000,
            _ => -1
        });
    }

    [Benchmark]
    public long ArrayBench()
    {
        return Power6(sourceValue, powerValue);
    }

    static long[] powerArray = new long[] {
0,
10,
100,
1_000,
10_000,
100_000,
1_000_000,
10_000_000,
100_000_000,
1_000_000_000,
10_000_000_000,
100_000_000_000,
1_000_000_000_000,
10_000_000_000_000,
100_000_000_000_000,
1_000_000_000_000_000,
10_000_000_000_000_000,
100_000_000_000_000_0000,
1_000_000_000_000_000_000
};

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Power6(long baseNumber, int power)
    {
        return baseNumber * powerArray[power];
    }

    [Benchmark]
    public long RosBench()
    {
        return Power7(sourceValue, powerValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Power7(long baseNumber, int power)
    {
        ReadOnlySpan<long> powerSpan = new long[] {
    0,
    10,
    100,
    1_000,
    10_000,
    100_000,
    1_000_000,
    10_000_000,
    100_000_000,
    1_000_000_000,
    10_000_000_000,
    100_000_000_000,
    1_000_000_000_000,
    10_000_000_000_000,
    100_000_000_000_000,
    1_000_000_000_000_000,
    10_000_000_000_000_000,
    100_000_000_000_000_0000,
    1_000_000_000_000_000_000
};

        return baseNumber * powerSpan[power];
    }
}
