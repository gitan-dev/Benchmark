using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Gitan.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class StringDollerBenchmark
{
    const int _loopCount = 1_000_000;

    [Benchmark]
    public int StringPlus4()
    {
        int length = 0;
        string a = "yamada";
        string b = "taro";

        for (int i = 0; i < _loopCount; i++)
        {
            string value = a + "." + b + ".";
            length += value.Length;
        }

        return length;
    }

    [Benchmark]
    public int StringPlus5()
    {
        int length = 0;
        string a = "yamada";
        string b = "taro";
        string c = "japan";

        for (int i = 0; i < _loopCount; i++)
        {
            string value = a + "." + b + "." + c;
            length += value.Length;
        }

        return length;
    }

    [Benchmark]
    public int DollarFormat4()
    {
        int length = 0;
        string a = "yamada";
        string b = "taro";

        for (int i = 0; i < _loopCount; i++)
        {
            string value = $"{a}.{b}.";
            length += value.Length;
        }

        return length;
    }

    [Benchmark]
    public int DollarFormat5()
    {
        int length = 0;
        string a = "yamada";
        string b = "taro";
        string c = "japan";

        for (int i = 0; i < _loopCount; i++)
        {
            string value = $"{a}.{b}.{c}";
            length += value.Length;
        }

        return length;
    }
}
