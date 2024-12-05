using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Gitan.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class DivShiftBenchmark
{
    // int

    [Benchmark]
    public int Div10IntBench()
    {
        int sum = 0;
        for (int i = 0; i < 10000; i++)
        {
            sum += i / 10;
        }
        return sum;
    }

    [Benchmark]
    public int Div100IntBench()
    {
        int sum = 0;
        for (int i = 0; i < 10000; i++)
        {
            sum += i / 100;
        }
        return sum;
    }

    [Benchmark]
    public int Div1000IntBench()
    {
        int sum = 0;
        for (int i = 0; i < 10000; i++)
        {
            sum += i / 1000;
        }
        return sum;
    }


    [Benchmark]
    public int PowShift10IntBench()
    {
        int sum = 0;
        for (int i = 0; i < 10000; i++)
        {
            sum += (i * 6554) >> 16;
        }
        return sum;
    }

    [Benchmark]
    public int PowShift100IntBench()
    {
        int sum = 0;
        for (int i = 0; i < 10000; i++)
        {
            sum += (i * 5243) >> 19;
        }
        return sum;
    }

    [Benchmark]
    public int PowShift1000IntBench()
    {
        int sum = 0;
        for (int i = 0; i < 10000; i++)
        {
            sum += (i * 8389) >> 23;
        }
        return sum;
    }

    // long

    [Benchmark]
    public long Div10LongBench()
    {
        long sum = 0;
        for (long i = 0; i < 10000; i++)
        {
            sum += i / 10;
        }
        return sum;
    }

    [Benchmark]
    public long Div100LongBench()
    {
        long sum = 0;
        for (long i = 0; i < 10000; i++)
        {
            sum += i / 100;
        }
        return sum;
    }

    [Benchmark]
    public long Div1000LongBench()
    {
        long sum = 0;
        for (long i = 0; i < 10000; i++)
        {
            sum += i / 1000;
        }
        return sum;
    }


    [Benchmark]
    public long PowShift10LongBench()
    {
        long sum = 0;
        for (long i = 0; i < 10000; i++)
        {
            sum += (i * 6554) >> 16;
        }
        return sum;
    }

    [Benchmark]
    public long PowShift100LongBench()
    {
        long sum = 0;
        for (long i = 0; i < 10000; i++)
        {
            sum += (i * 5243) >> 19;
        }
        return sum;
    }

    [Benchmark]
    public long PowShift1000LongBench()
    {
        long sum = 0;
        for (long i = 0; i < 10000; i++)
        {
            sum += (i * 8389) >> 23;
        }
        return sum;
    }

    // uint

    [Benchmark]
    public uint Div10UIntBench()
    {
        uint sum = 0;
        for (uint i = 0; i < 10000; i++)
        {
            sum += i / 10;
        }
        return sum;
    }

    [Benchmark]
    public uint Div100UIntBench()
    {
        uint sum = 0;
        for (uint i = 0; i < 10000; i++)
        {
            sum += i / 100;
        }
        return sum;
    }

    [Benchmark]
    public uint Div1000UIntBench()
    {
        uint sum = 0;
        for (uint i = 0; i < 10000; i++)
        {
            sum += i / 1000;
        }
        return sum;
    }


    [Benchmark]
    public uint PowShift10UIntBench()
    {
        uint sum = 0;
        for (uint i = 0; i < 10000; i++)
        {
            sum += (i * 6554) >> 16;
        }
        return sum;
    }

    [Benchmark]
    public uint PowShift100UIntBench()
    {
        uint sum = 0;
        for (uint i = 0; i < 10000; i++)
        {
            sum += (i * 5243) >> 19;
        }
        return sum;
    }

    [Benchmark]
    public uint PowShift1000UIntBench()
    {
        uint sum = 0;
        for (uint i = 0; i < 10000; i++)
        {
            sum += (i * 8389) >> 23;
        }
        return sum;
    }

    // ulong

    [Benchmark]
    public ulong Div10ULongBench()
    {
        ulong sum = 0;
        for (ulong i = 0; i < 10000; i++)
        {
            sum += i / 10;
        }
        return sum;
    }

    [Benchmark]
    public ulong Div100ULongBench()
    {
        ulong sum = 0;
        for (ulong i = 0; i < 10000; i++)
        {
            sum += i / 100;
        }
        return sum;
    }

    [Benchmark]
    public ulong Div1000ULongBench()
    {
        ulong sum = 0;
        for (ulong i = 0; i < 10000; i++)
        {
            sum += i / 1000;
        }
        return sum;
    }


    [Benchmark]
    public ulong PowShift10ULongBench()
    {
        ulong sum = 0;
        for (ulong i = 0; i < 10000; i++)
        {
            sum += (i * 6554) >> 16;
        }
        return sum;
    }

    [Benchmark]
    public ulong PowShift100ULongBench()
    {
        ulong sum = 0;
        for (ulong i = 0; i < 10000; i++)
        {
            sum += (i * 5243) >> 19;
        }
        return sum;
    }

    [Benchmark]
    public ulong PowShift1000ULongBench()
    {
        ulong sum = 0;
        for (ulong i = 0; i < 10000; i++)
        {
            sum += (i * 8389) >> 23;
        }
        return sum;
    }
}

