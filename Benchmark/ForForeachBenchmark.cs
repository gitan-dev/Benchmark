using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Gitan.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class ForForeachBenchmark
{
    const int Size = 100;

    static readonly IEnumerable<int> _source = Enumerable.Range(0, Size);

    static readonly int[] _array = _source.ToArray();

    [Benchmark]
    public int ArrayForBench()
    {
        int sum = 0;
        var target = _array;
        for (int i = 0; i < target.Length; i++) { sum += target[i]; }
        return sum;
    }

    [Benchmark]
    public int ArrayForEachBench()
    {
        int sum = 0;
        var target = _array;
        foreach (var item in target) { sum += item; }
        return sum;
    }

    [Benchmark]
    public int ArrayAsSpanForEachBench()
    {
        int sum = 0;
        var target = _array;
        foreach (var item in target.AsSpan()) { sum += item; }
        return sum;
    }

    static readonly List<int> _list = _source.ToList();

    [Benchmark]
    public int ListForBench()
    {
        int sum = 0;
        var target = _list;
        for (int i = 0; i < target.Count; i++) { sum += target[i]; }
        return sum;
    }

    [Benchmark]
    public int ListForEachBench()
    {
        int sum = 0;
        var target = _list;
        foreach (var item in target) { sum += item; }
        return sum;
    }

    [Benchmark]
    public int ListAsSpanForEachBench()
    {
        int sum = 0;
        var target = _list;
        foreach (var item in System.Runtime.InteropServices.CollectionsMarshal.AsSpan(target)) { sum += item; }
        return sum;
    }

    static readonly LinkedList<int> _linkedList = new(_source);


    [Benchmark]
    public int LinkedListForEachBench()
    {
        int sum = 0;
        var target = _linkedList;
        foreach (var item in target) { sum += item; }
        return sum;
    }

    static readonly SortedSet<int> _sortedSet = new(_source);

    [Benchmark]
    public int SortedSetForEachBench()
    {
        int sum = 0;
        var target = _sortedSet;
        foreach (var item in target) { sum += item; }
        return sum;
    }

    static readonly HashSet<int> _hashSet = new(_source);

    [Benchmark]
    public int HashSetForEachBench()
    {
        int sum = 0;
        var target = _hashSet;
        foreach (var item in target) { sum += item; }
        return sum;
    }

    static readonly Stack<int> _stack = new(_source);

    [Benchmark]
    public int StackForEachBench()
    {
        int sum = 0;
        var target = _stack;
        foreach (var item in target) { sum += item; }
        return sum;
    }

    static readonly Queue<int> _queue = new(_source);

    [Benchmark]
    public int QueueForEachBench()
    {
        int sum = 0;
        var target = _queue;
        foreach (var item in target) { sum += item; }
        return sum;
    }
}
