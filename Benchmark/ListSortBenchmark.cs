using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Gitan.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class ListSortBenchmark
{
    static int _loopCount = 100_000;

    static Random _random = new();

    static List<int> _originalList = GetList();

    public static List<int> GetList()
    {
        var list = new List<int>(_loopCount);

        for (int i = 0; i < _loopCount; i++)
        {
            list.Add(_random.Next(_loopCount));
        }
        return list;
    }

    public sealed class SortIntReverse : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x > y) return -1;
            if (x == y) return 0;
            return 1;
        }
    }

    [Benchmark]
    public List<int> ListSort()
    {
        var list = _originalList.ToList();

        list.Sort();

        return list;
    }

    [Benchmark]
    public List<int> ListSortReverse()
    {
        var list = _originalList.ToList();

        list.Sort();
        list.Reverse();

        return list;
    }

    [Benchmark]
    public List<int> ListSortReverseComparer()
    {
        var list = _originalList.ToList();

        list.Sort(new SortIntReverse());

        return list;
    }
}

