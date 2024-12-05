using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using System.Text;

namespace Gitan.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class CommandStringUtf8Benchmark
{
    struct Value
    {
        public string stringValue;
        public byte[] utf8Value;

        public Value(string value)
        {
            stringValue = value;
            utf8Value = Encoding.UTF8.GetBytes(value);
        }
    }
    
    static Value _start = new("start") ;
    static Value _stop = new("stop");
    static Value _show = new("show");
    static Value _quit = new("quit");

    [Benchmark]
    public int StringSwitchBench()
    {
        int sum = 0;
        sum += AnalyzeStringSwitch(_start.stringValue);
        sum += AnalyzeStringSwitch(_show.stringValue);
        sum += AnalyzeStringSwitch(_stop.stringValue);
        sum += AnalyzeStringSwitch(_quit.stringValue);

        return sum;
    }

    public int AnalyzeStringSwitch(string command)
    {
        return command switch
        {
            "start" => 1,
            "show" => 2,
            "stop" => 3,
            "quit" => 4,
            _ => 0,
        };
    }

    [Benchmark]
    public int StringIfBench()
    {
        int sum = 0;
        sum += AnalyzeStringIf(_start.stringValue);
        sum += AnalyzeStringIf(_show.stringValue);
        sum += AnalyzeStringIf(_stop.stringValue);
        sum += AnalyzeStringIf(_quit.stringValue);

        return sum;
    }

    public int AnalyzeStringIf(string command)
    {
        if (command == "start") { return 1; }
        if (command == "show") { return 2; }
        if (command == "stop") { return 3; }
        if (command == "quit") { return 4; }
        return 0;
    }

    [Benchmark]
    public int StaticBytesSequenceEqualBench()
    {
        int sum = 0;
        sum += AnalyzeStaticBytesSequenceEqual(_start.utf8Value);
        sum += AnalyzeStaticBytesSequenceEqual(_show.utf8Value);
        sum += AnalyzeStaticBytesSequenceEqual(_stop.utf8Value);
        sum += AnalyzeStaticBytesSequenceEqual(_quit.utf8Value);

        return sum;
    }

    public int AnalyzeStaticBytesSequenceEqual(byte[] command)
    {
        if (command.SequenceEqual(_start.utf8Value)) { return 1; }
        if (command.SequenceEqual(_show.utf8Value)) { return 2; }
        if (command.SequenceEqual(_stop.utf8Value)) { return 3; }
        if (command.SequenceEqual(_quit.utf8Value)) { return 4; }
        return 0;
    }

    [Benchmark]
    public int StaticRosSequenceEqualBench()
    {
        int sum = 0;
        sum += AnalyzeStaticRosSequenceEqual(_start.utf8Value);
        sum += AnalyzeStaticRosSequenceEqual(_show.utf8Value);
        sum += AnalyzeStaticRosSequenceEqual(_stop.utf8Value);
        sum += AnalyzeStaticRosSequenceEqual(_quit.utf8Value);

        return sum;
    }

    public int AnalyzeStaticRosSequenceEqual(byte[] command)
    {
        var command2 = new ReadOnlySpan<byte>(command);
        if (command2.SequenceEqual(_start.utf8Value)) { return 1; }
        if (command2.SequenceEqual(_show.utf8Value)) { return 2; }
        if (command2.SequenceEqual(_stop.utf8Value)) { return 3; }
        if (command2.SequenceEqual(_quit.utf8Value)) { return 4; }
        return 0;
    }

    [Benchmark]
    public int StaticBytesMyEqualsBench()
    {
        int sum = 0;
        sum += AnalyzeStaticBytesMyEquals(_start.utf8Value);
        sum += AnalyzeStaticBytesMyEquals(_show.utf8Value);
        sum += AnalyzeStaticBytesMyEquals(_stop.utf8Value);
        sum += AnalyzeStaticBytesMyEquals(_quit.utf8Value);

        return sum;
    }

    public int AnalyzeStaticBytesMyEquals(byte[] command)
    {
        if (MyEquals1(command, _start.utf8Value)) { return 1; }
        if (MyEquals1(command, _show.utf8Value)) { return 2; }
        if (MyEquals1(command, _stop.utf8Value)) { return 3; }
        if (MyEquals1(command, _quit.utf8Value)) { return 4; }
        return 0;
    }

    public bool MyEquals1(ReadOnlySpan<byte> a, ReadOnlySpan<byte> b)
    {
        if (a.Length != b.Length) { return false; }
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i]) { return false; }
        }
        return true;
    }

    [Benchmark]
    public int U8BytesBench()
    {
        int sum = 0;
        sum += AnalyzeU8Bytes(_start.utf8Value);
        sum += AnalyzeU8Bytes(_show.utf8Value);
        sum += AnalyzeU8Bytes(_stop.utf8Value);
        sum += AnalyzeU8Bytes(_quit.utf8Value);

        return sum;
    }

    public int AnalyzeU8Bytes(byte[] command)
    {
        if ("start"u8.SequenceEqual(command)) { return 1; }
        if ("show"u8.SequenceEqual(command)) { return 2; }
        if ("stop"u8.SequenceEqual(command)) { return 3; }
        if ("quit"u8.SequenceEqual(command)) { return 4; }
        return 0;
    }
}
