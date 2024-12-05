using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Text;

namespace Gitan.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class HighPerformanceStringBenchmark
{
    readonly byte[] _hash = Enumerable.Range(0, 32).Select(i => (byte)i).ToArray();

    [Benchmark]
    public string ToHexStringBench() => ToHexString(_hash);

    static string ToHexString(byte[] bytes)
    {
        var sb = new StringBuilder(bytes.Length * 2);
        foreach (var b in bytes)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }


    [Benchmark]
    public string ToHexString_SpanBench() => ToHexString_Span(_hash);

    public static string ToHexString_Span(ReadOnlySpan<byte> bytes)
    {
        const string HexValues = "0123456789abcdef";

        Span<char> chars = stackalloc char[bytes.Length * 2];

        for (int i = 0; i < bytes.Length; i++)
        {
            var b = bytes[i];

            var i2 = i * 2;
            chars[i2] = HexValues[b >> 4];
            chars[i2 + 1] = HexValues[b & 0xF];
        }

        return new string(chars);
    }


    [Benchmark]
    public string ToHexString_CreateBench() => ToHexString_Create(_hash);

    public static string ToHexString_Create(byte[] hash)
    {
        const string HexValues = "0123456789abcdef";

        var st = String.Create<byte[]>(64, hash,
            (Span<char> chars, byte[] state) =>
            {
                for (int i = 0; i < state.Length; i++)
                {
                    var b = state[i];
                    var i2 = i * 2;
                    chars[i2] = HexValues[b >> 4];
                    chars[i2 + 1] = HexValues[b & 0xF];
                }
            }
            );

        return st;
    }


    [Benchmark]
    public string ToHexString_CreateUnsafeBench() => ToHexString_CreateUnsafe(_hash);

    static readonly char[] _toHexChars = GetToTexChars();

    static char[] GetToTexChars()
    {
        const string HexValues = "0123456789abcdef";

        var chars = new char[256 * 2];

        for (int i = 0; i < 256; i++)
        {
            chars[i * 2] = HexValues[i >> 4];
            chars[i * 2 + 1] = HexValues[i & 0xF];
        }

        return chars;
    }

    public static string ToHexString_CreateUnsafe(byte[] hash)
    {
        var st = String.Create<byte[]>(hash.Length * 2, hash,
            (Span<char> chars, byte[] state) =>
            {
                unsafe
                {
                    fixed (char* from = _toHexChars)
                    fixed (char* to = chars)
                    {
                        var to2 = to;
                        for (int i = 0; i < state.Length; i++)
                        {
                            *(int*)(to2) = *(int*)(from + state[i] * 2);
                            to2 += 2;
                        }
                    }
                }
            }
            );

        return st;
    }
}
