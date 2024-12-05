using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Gitan.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class CopyPerformanceBenchmark
{
    static byte[] data = new byte[5] { 15, 16, 17, 18, 19, };

    [Benchmark]
    public byte[] CopyArray()
    {
        var result = new byte[50];
        for (int i = 0; i < 10; i++)
        {
            data.CopyTo(result, i * 5);
        }
        return result;
    }

    [Benchmark]
    public byte[] CopySpan()
    {
        var result = new byte[50];
        for (int i = 0; i < 10; i++)
        {
            data.CopyTo(result.AsSpan(i * 5));
        }
        return result;
    }
}
