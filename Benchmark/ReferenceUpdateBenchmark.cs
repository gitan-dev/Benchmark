using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace Gitan.Benchmark;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class ReferenceUpdateBenchmark
{
    static readonly Uri endpointUri = new("https://api.bitflyer.com");
    static readonly string apiKey = "{{ YOUR API KEY }}";
    static readonly string apiSecret = "{{ YOUR API SECRET }}";

    [Benchmark]
    public async Task<(HttpClient, HttpRequestMessage)> Reference()
    {
        var method = "POST";
        var path = "/v1/me/sendchildorder";
        var query = "";
        var body = @"";

        //using (var client = new HttpClient())
        //using (var request = new HttpRequestMessage(new HttpMethod(method), path + query))
        //using (var content = new StringContent(body))
        var client = new HttpClient();
        var request = new HttpRequestMessage(new HttpMethod(method), path + query);
        var content = new StringContent(body);
        {
            client.BaseAddress = endpointUri;
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;

            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            var data = timestamp + method + path + query + body;
            var hash = SignWithHMACSHA256(data, apiSecret);
            request.Headers.Add("ACCESS-KEY", apiKey);
            request.Headers.Add("ACCESS-TIMESTAMP", timestamp);
            request.Headers.Add("ACCESS-SIGN", hash);

            return (client, request);
            //var message = await client.SendAsync(request);
            //var response = await message.Content.ReadAsStringAsync();

            //Console.WriteLine(response);
        }
    }

    static string SignWithHMACSHA256(string data, string secret)
    {
        using var encoder = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        var hash = encoder.ComputeHash(Encoding.UTF8.GetBytes(data));
        return ToHexString(hash);
    }

    static string ToHexString(byte[] bytes)
    {
        var sb = new StringBuilder(bytes.Length * 2);
        foreach (var b in bytes)
        {
            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();
    }

    static MediaTypeHeaderValue _mediaType = new("application/json");

    [Benchmark]
    public async Task<(HttpClient, HttpRequestMessage)> Update()
    {
        var method = HttpMethod.Post;
        var path = "/v1/me/sendchildorder";
        var query = "";
        var body = @"";

        //using (var client = new HttpClient())
        //using (var request = new HttpRequestMessage(new HttpMethod(method), path + query))
        //using (var content = new StringContent(body))
        var client = new HttpClient();
        var request = new HttpRequestMessage(method, path + query);
        var content = new StringContent(body);
        {
            client.BaseAddress = endpointUri;
            content.Headers.ContentType = _mediaType;
            request.Content = content;

            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            var data = timestamp + method + path + query + body;
            var hash = SignWithHMACSHA256New(data);
            request.Headers.Add("ACCESS-KEY", apiKey);
            request.Headers.Add("ACCESS-TIMESTAMP", timestamp);
            request.Headers.Add("ACCESS-SIGN", hash);

            return (client, request);
            //var message = await client.SendAsync(request);
            //var response = await message.Content.ReadAsStringAsync();

            //Console.WriteLine(response);
        }
    }
    static HMACSHA256 _hmac = new(Encoding.UTF8.GetBytes(apiSecret));

    static string SignWithHMACSHA256New(string data)
    {
        byte[] hash;
        lock (_hmac)
        {
            hash = _hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        }
        return ToHex_Lower(hash);
    }

    public static string ToHex_Lower(ReadOnlySpan<byte> bytes)
    {
        const string HexValues = "0123456789abcdef";

        Span<char> chars = stackalloc char[bytes.Length * 2];

        for (int i = 0; i < bytes.Length; i++)
        {
            var b = bytes[i];
            chars[i * 2] = HexValues[b >> 4];
            chars[i * 2 + 1] = HexValues[b & 0xF];
        }

        return new string(chars);
    }
}
