using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Text;

namespace Gitan.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class ByteArrayROSSplitBenchmark
{
    public static byte[] SampleBytes = Encoding.UTF8.GetBytes("Command Parameter");

    [Benchmark]
    public int ByteArraySplitBench()
    {
        int sum = 0;

        for (int i = 0; i < 100; i++)
        {
            (var left, var right) = ByteArraySplit(SampleBytes);
            sum += left.Length + right.Length;
        }

        return sum;
    }

    public static (byte[] left, byte[] right) ByteArraySplit(byte[] source)
    {
        byte[] left;
        byte[] right;
        for (int i = 0; i < source.Length; i++)
        {
            if (source[i] == (byte)' ')
            {
                left = source.AsSpan(0, i).ToArray();
                right = source.AsSpan(i + 1).ToArray();

                return (left, right);
            }
        }
        left = source;
        right = Array.Empty<byte>();
        return (left, right);
    }

    [Benchmark]
    public int ReadOnlySpanSplitBench()
    {
        int sum = 0;

        for (int i = 0; i < 100; i++)
        {
            ReadOnlySpanSplit(SampleBytes, out var left, out var right);
            sum += left.Length + right.Length;
        }

        return sum;
    }

    public static void ReadOnlySpanSplit(ReadOnlySpan<byte> source, out ReadOnlySpan<byte> left, out ReadOnlySpan<byte> right)
    {
        for (int i = 0; i < source.Length; i++)
        {
            if (source[i] == (byte)' ')
            {
                left = source[0..i];
                right = source[(i + 1)..];
                return;
            }
        }
        left = source;
        right = ReadOnlySpan<byte>.Empty;
    }
}
