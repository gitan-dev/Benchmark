using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Gitan.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class UnixTimeBenchmark
{
    private static DateTime _unixTime_BaseTime = new(1970, 1, 1);

    [Benchmark]
    public long A_DateTime_Now_TimeSpan_TotalMilliseconds() =>
        (long)(DateTime.Now.ToUniversalTime().Subtract(_unixTime_BaseTime).TotalMilliseconds);

    [Benchmark]
    public long B_DateTime_UtcNow_TimeSpan_TotalMilliseconds() =>
        (long)(DateTime.UtcNow.Subtract(_unixTime_BaseTime).TotalMilliseconds);

    [Benchmark]
    public long C_DateTimeOffset_UtcNow_ToUnixTimeMilliseconds() =>
        DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();


    private static long _unixTimeMillisecond_baseTicks = new DateTime(1970, 1, 1).Ticks / TimeSpan.TicksPerMillisecond;

    [Benchmark]
    public long D_DateTime_UtcNow_SelfCalc() =>
        DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond - _unixTimeMillisecond_baseTicks;
}
