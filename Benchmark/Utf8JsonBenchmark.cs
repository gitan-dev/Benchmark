using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Runtime.CompilerServices;
using System.Text;

namespace Gitan.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class Utf8JsonBenchmark
{
    static string _sideString = "buy";
    static byte[] _sideUtf8 = "buy"u8.ToArray();
    static long _price = 1000000;
    static double _size = 0.01;

    [Benchmark]
    public byte[] GetBytes_StringSomeAdd()
    {
        return MakeBytes_StringSomeAdd(_sideString, _price, _size);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public byte[] MakeBytes_StringSomeAdd(string side, long price, double size)
    {
        string st = "";
        st += "{\"side\":\"";
        st += side;
        st += "\",\"price\":";
        st += price;
        st += ",\"size\":";
        st += size;
        st += "}";

        return Encoding.UTF8.GetBytes(st.ToString());
    }


    [Benchmark]
    public byte[] GetBytes_StringBuilder()
    {
        return MakeBytes_StringBuilder(_sideString, _price, _size);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public byte[] MakeBytes_StringBuilder(string side, long price, double size)
    {
        var stb = new StringBuilder(64);
        stb.Append("{\"side\":\"");
        stb.Append(side);
        stb.Append("\",\"price\":");
        stb.Append(price);
        stb.Append(",\"size\":");
        stb.Append(size);
        stb.Append("}");

        return Encoding.UTF8.GetBytes(stb.ToString());
    }


    [Benchmark]
    public byte[] GetBytes_StringPlusToUtf8()
    {
        return MakeBytes_StringPlusToUtf8(_sideString, _price, _size);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public byte[] MakeBytes_StringPlusToUtf8(string side, long price, double size)
    {
        var stringJson = "{\"side\":\"" + side + "\",\"price\":" + price.ToString() + ",\"size\":" + size.ToString() + "}";
        return Encoding.UTF8.GetBytes(stringJson);
    }


    [Benchmark]
    public byte[] GetBytes_DollarStringToUtf8()
    {
        return MakeBytes_DollarStringToUtf8(_sideString, _price, _size);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public byte[] MakeBytes_DollarStringToUtf8(string side, long price, double size)
    {
        var stringJson = $$"""{"side":"{{side}}","price":{{price}},"size":{{size}}}""";
        return Encoding.UTF8.GetBytes(stringJson);
    }



    [Benchmark]
    public byte[] GetBytes_DollarStringToAscii()
    {
        return MakeBytes_DollarStringToAscii(_sideString, _price, _size);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public byte[] MakeBytes_DollarStringToAscii(string side, long price, double size)
    {
        var stringJson = $$"""{"side":"{{side}}","price":{{price}},"size":{{size}}}""";
        return Encoding.ASCII.GetBytes(stringJson);
    }


    public byte[] GetByte_CopyToTryFormat()
    {
        return MakeBytes_CopyToTryFormat(_sideUtf8, _price, _size);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public byte[] MakeBytes_CopyToTryFormat(ReadOnlySpan<byte> side, long price, double size)
    {
        Span<byte> buffer = stackalloc byte[128];

        int offset = 0;
        """
    {"side":"
    """u8.CopyTo(buffer[offset..]);
        offset += 9;
        side.CopyTo(buffer[offset..]);
        offset += side.Length;
        """
    ","price":
    """u8.CopyTo(buffer[offset..]);
        offset += 10;
        price.TryFormat(buffer[offset..], out int written);
        offset += written;
        """
    ,"size":
    """u8.CopyTo(buffer[offset..]);
        offset += 8;
        size.TryFormat(buffer[offset..], out written);
        offset += written;
        "}"u8.CopyTo(buffer[offset..]);
        offset += 1;

        return buffer[..offset].ToArray();
    }


    [Benchmark]
    public int GetSpan_CopyToTryFormat()
    {
        Span<byte> buffer = stackalloc byte[128];

        var json = MakeSpan_CopyToTryFormat(buffer, _sideUtf8, _price, _size);
        return json.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public Span<byte> MakeSpan_CopyToTryFormat(Span<byte> buffer, ReadOnlySpan<byte> side, long price, double size)
    {
        int offset = 0;
        """
    {"side":"
    """u8.CopyTo(buffer[offset..]);
        offset += 9;
        side.CopyTo(buffer[offset..]);
        offset += side.Length;
        """
    ","price":
    """u8.CopyTo(buffer[offset..]);
        offset += 10;
        price.TryFormat(buffer[offset..], out int written);
        offset += written;
        """
    ,"size":
    """u8.CopyTo(buffer[offset..]);
        offset += 8;
        size.TryFormat(buffer[offset..], out written);
        offset += written;
        "}"u8.CopyTo(buffer[offset..]);
        offset += 1;

        return buffer[..offset];
    }


    [Benchmark]
    public int GetSpan_Utf8TryWriteDollarString()
    {
        Span<byte> buffer = stackalloc byte[128];
        return MakeSpan_Utf8TryWriteDollarString(buffer, _sideString, _price, _size).Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public Span<byte> MakeSpan_Utf8TryWriteDollarString(Span<byte> buffer, string side, long price, double size)
    {
        System.Text.Unicode.Utf8.TryWrite(buffer, $$"""{"side":"{{side}}","price":{{price}},"size":{{size}}}""", out var bytesWritten);

        return buffer[0..bytesWritten];
    }


    [Benchmark]
    public int GetSpan_Utf8TryWriteDollarUtf8()
    {
        Span<byte> buffer = stackalloc byte[128];
        return MakeSpan_Utf8TryWriteDollarUtf8(buffer, _sideUtf8, _price, _size).Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public Span<byte> MakeSpan_Utf8TryWriteDollarUtf8(Span<byte> buffer, ReadOnlySpan<byte> side, long price, double size)
    {
        System.Text.Unicode.Utf8.TryWrite(buffer, $$"""{"side":"{{side}}","price":{{price}},"size":{{size}}}""", out var bytesWritten);

        return buffer[0..bytesWritten];
    }
}

