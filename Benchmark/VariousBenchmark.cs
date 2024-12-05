using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Runtime.CompilerServices;
using System.Text;

namespace Gitan.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class VariousBenchmark
{
    const int _loopCount = 10_000_000;

    [Benchmark]
    public long IntBench()
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            int a = 1;
            int b = 2;

            sum += SumInt(a, b);
        }

        return sum;
    }

    public int SumInt(int a, int b)
    {
        return a + b;
    }

    [Benchmark]
    public double DoubleBench()
    {
        double sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            double a = 1;
            double b = 2;

            sum += SumDouble(a, b);
        }

        return sum;
    }

    public double SumDouble(double a, double b)
    {
        return a + b;
    }

    [Benchmark]
    public long IntNoInlineBench()
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            int a = 1;
            int b = 2;

            sum += SumIntNoInline(a, b);
        }

        return sum;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public int SumIntNoInline(int a, int b)
    {
        return a + b;
    }

    [Benchmark]
    public long IntStructBench()
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            var a = new IntStruct() { Value = 1 };
            var b = new IntStruct() { Value = 2 };

            sum += SumIntStruct(a, b);
        }

        return sum;
    }

    public int SumIntStruct(IntStruct a, IntStruct b)
    {
        return a.Value + b.Value;
    }

    public struct IntStruct
    {
        public int Value { get; set; }
    }

    [Benchmark]
    public long IntClassBench()
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            var a = new IntClass() { Value = 1 };
            var b = new IntClass() { Value = 2 };

            sum += SumIntClass(a, b);
        }

        return sum;
    }

    public int SumIntClass(IntClass a, IntClass b)
    {
        return a.Value + b.Value;
    }

    public class IntClass
    {
        public int Value { get; set; }
    }

    [Benchmark]
    public long IntBoxingBench()
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            var a = 1;
            var b = 2;

            sum += SumIntBoxing(a, b);
        }

        return sum;
    }

    public int SumIntBoxing(object a, object b)
    {
        return (int)a + (int)b;
    }

    [Benchmark]
    public long IntSubClassVirtualBench()
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            ParentClass1 a = new ChildClass1() { Value = 1 };
            ParentClass1 b = new ChildClass1() { Value = 2 };

            sum += SumIntSubClassVirtual(a, b);
        }

        return sum;
    }

    public int SumIntSubClassVirtual(ParentClass1 a, ParentClass1 b)
    {
        return a.Value + b.Value;
    }

    public class ParentClass1
    {
        public virtual int Value { get; set; }
    }

    public class ChildClass1 : ParentClass1
    {
        public override int Value { get; set; }
    }

    [Benchmark]
    public long IntSubClassVirtualBench2()
    {
        long sum = 0;

        ParentClass1 a = new ChildClass1();
        ParentClass1 b = new ChildClass1();

        for (int i = 0; i < _loopCount; i++)
        {
            a.Value = 1;
            b.Value = 2;

            sum += SumIntSubClassVirtual(a, b);
        }

        return sum;
    }

    [Benchmark]
    public long IntSubClassSealedVirtualBench()
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            ParentClass2 a = new ChildClassSealed2() { Value = 1 };
            ParentClass2 b = new ChildClassSealed2() { Value = 2 };

            sum += SumIntSubClassSealedVirtual(a, b);
        }

        return sum;
    }

    public int SumIntSubClassSealedVirtual(ParentClass2 a, ParentClass2 b)
    {
        return a.Value + b.Value;
    }

    public class ParentClass2
    {
        public virtual int Value { get; set; }
    }

    public sealed class ChildClassSealed2 : ParentClass2
    {
        public override int Value { get; set; }
    }

    [Benchmark]
    public long IntInterfaceVirtualBench()
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            IHasValue a = new ChildClassWithInterface() { Value = 1 };
            IHasValue b = new ChildClassWithInterface() { Value = 2 };

            sum += SumIntInterfaceVirtual(a, b);
        }

        return sum;
    }

    public int SumIntInterfaceVirtual(IHasValue a, IHasValue b)
    {
        return a.Value + b.Value;
    }

    public interface IHasValue
    {
        public int Value { get; set; }
    }

    public class ChildClassWithInterface : IHasValue
    {
        public int Value { get; set; }
    }

    readonly object lockObject = new();

    [Benchmark]
    public long IntObjectLockBench() // object型を使う
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            int a = 1;
            int b = 2;

            lock (lockObject)
            {
                sum += SumInt(a, b);
            }
        }

        return sum;
    }

#if NET9_0_OR_GREATER
    readonly Lock _lock = new Lock();
#endif

    [Benchmark]
    public long IntLockClassBench() // Lockクラスを使う
    {
#if NET9_0_OR_GREATER
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            int a = 1;
            int b = 2;

            lock (_lock)
            {
                sum += SumInt(a, b);
            }
        }

        return sum;

#else
        throw new Exception();
#endif
    }


    [Benchmark]
    public async Task<long> AsyncAwaitIntBench()
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            int a = 1;
            int b = 2;

            sum += await SumAsyncInt1(a, b);
        }

        return sum;
    }

    public async Task<int> SumAsyncInt1(int a, int b)
    {
        return a + b;
    }

    [Benchmark]
    public async Task<long> AwaitFromResultIntBench()
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            int a = 1;
            int b = 2;

            sum += await SumFromResultInt(a, b);
        }

        return sum;
    }

    public Task<int> SumFromResultInt(int a, int b)
    {
        return Task.FromResult(a + b);
    }

    [Benchmark]
    public async Task<long> AsyncAwaitValueTaskIntBench()
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            int a = 1;
            int b = 2;

            sum += await SumAsyncAwaitValueTaskInt(a, b);
        }

        return sum;
    }

    public async ValueTask<int> SumAsyncAwaitValueTaskInt(int a, int b)
    {
        return a + b;
    }

    [Benchmark]
    public async Task<long> AwaitFromResultValueTaskIntBench()
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            int a = 1;
            int b = 2;

            sum += await SumFromResultValueTaskInt(a, b);
        }

        return sum;
    }

    public ValueTask<int> SumFromResultValueTaskInt(int a, int b)
    {
        return ValueTask.FromResult(a + b);
    }

    [Benchmark]
    public long MakeUtf8Abcde_StringBench()
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            string a = "a";
            string b = "b";
            string c = "c";
            string d = "d";
            string e = "e";

            var result = MakeUtf8Abcde_String(a, b, c, d, e);

            sum += result.Length;
        }

        return sum;
    }

    public byte[] MakeUtf8Abcde_String(string a, string b, string c, string d, string e)
    {
        return Encoding.UTF8.GetBytes(a + b + c + d + e);
    }

    static string _a = "a";
    static string _b = "b";
    static string _c = "c";
    static string _d = "d";
    static string _e = "e";

    [Benchmark]
    public long MakeUtf8Abcde_StaticStringBench()
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {

            var result = MakeUtf8Abcde_String(_a, _b, _c, _d, _e);

            sum += result.Length;
        }

        return sum;
    }

    public byte[] MakeUtf8Abcde_StaticString(string a, string b, string c, string d, string e)
    {
        return Encoding.UTF8.GetBytes(a + b + c + d + e);
    }

    [Benchmark]
    public long MakeUtf8Abcde_StaticByteArrayBench()
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {

            var result = MakeUtf8Abcde_StaticByteArray(b_a, b_b, b_c, b_d, b_e);

            sum += result.Length;
        }

        return sum;
    }

    static byte[] b_a = Encoding.UTF8.GetBytes("a");
    static byte[] b_b = Encoding.UTF8.GetBytes("b");
    static byte[] b_c = Encoding.UTF8.GetBytes("c");
    static byte[] b_d = Encoding.UTF8.GetBytes("d");
    static byte[] b_e = Encoding.UTF8.GetBytes("e");

    public byte[] MakeUtf8Abcde_StaticByteArray(byte[] a, byte[] b, byte[] c, byte[] d, byte[] e)
    {
        byte[] result = new byte[a.Length + b.Length + c.Length + d.Length + e.Length];
        int index = 0;
        a.CopyTo(result, index);
        index += a.Length;
        b.CopyTo(result, index);
        index += a.Length;
        c.CopyTo(result, index);
        index += a.Length;
        d.CopyTo(result, index);
        index += a.Length;
        e.CopyTo(result, index);
        index += a.Length;

        return result;
    }

    [Benchmark]
    public long MakeUtf8Abcde_StaticByteArraySpanBench()
    {
        long sum = 0;

        for (int i = 0; i < _loopCount; i++)
        {
            sum += MakeUtf8Abcde_StaticByteArraySpan1();
        }

        return sum;
    }

    public int MakeUtf8Abcde_StaticByteArraySpan1()
    {
        Span<byte> buffer = stackalloc byte[128];
        var result = MakeUtf8Abcde_StaticByteArraySpan2(buffer, b_a, b_b, b_c, b_d, b_e);

        return result.Length;
    }

    public Span<byte> MakeUtf8Abcde_StaticByteArraySpan2(Span<byte> buffer, byte[] a, byte[] b, byte[] c, byte[] d, byte[] e)
    {
        int index = 0;
        a.AsSpan().CopyTo(buffer[index..]);
        index += a.Length;
        b.AsSpan().CopyTo(buffer[index..]);
        index += b.Length;
        c.AsSpan().CopyTo(buffer[index..]);
        index += c.Length;
        d.AsSpan().CopyTo(buffer[index..]);
        index += d.Length;
        e.AsSpan().CopyTo(buffer[index..]);
        index += e.Length;

        return buffer[..index];
    }
}
