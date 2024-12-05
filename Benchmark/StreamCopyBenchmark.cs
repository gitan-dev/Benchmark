using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Text;

namespace Gitan.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class StreamCopyBenchmark
{
    public static byte[] Source { get; set; } = Enumerable.Range(32, 80).Select(x => (byte)x).ToArray();
    public static byte[] Buffer { get; set; } = new byte[8192];

    public static MemoryStream ShareMemoryStream { get; set; } = new MemoryStream(Buffer);

    [Benchmark]
    public byte[] ToArray()
    {
        return Source.ToArray();
    }

    [Benchmark]
    public byte[] MemoryStreamCopyUseBuffer()
    {
        var mem1 = new MemoryStream(Source);
        var mem2 = new MemoryStream(Buffer);
        mem1.CopyTo(mem2);

        var result = Buffer.AsSpan(0, (int)mem2.Position).ToArray();

        return result;
    }

    [Benchmark]
    public byte[] MemoryStreamCopyUseShare()
    {
        var mem1 = new MemoryStream(Source);
        var mem2 = ShareMemoryStream;
        mem2.Position = 0;
        mem1.CopyTo(mem2);

        var result = Buffer.AsSpan(0, (int)mem2.Position).ToArray();

        return result;
    }

    [Benchmark]
    public byte[] MemoryStreamCopyNoBuffer()
    {
        var mem1 = new MemoryStream(Source);
        var mem2 = new MemoryStream();
        mem1.CopyTo(mem2);

        var result = mem2.ToArray();

        return result;
    }

    [Benchmark]
    public byte[] StringStreamCopy()
    {
        var mem1 = new MemoryStream(Source);
        System.IO.StreamReader reader = new System.IO.StreamReader(mem1, Encoding.UTF8);
        string content = reader.ReadToEnd();

        var result = Encoding.UTF8.GetBytes(content);
        return result;
    }
}
