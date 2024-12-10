■**概要**<br>
Benchmarkは、さまざまな方法のパフォーマンスを比較するためのベンチマークです。<br>
ベンチマーク用のライブラリ「BenchmarkDotNet」を使用して実装されていて<br>
ご自身の環境でベンチマークをご利用いただけます。<br>

プロジェクトURL : [https://github.com/gitan-dev/Benchmark](https://github.com/gitan-dev/Benchmark)<br>

■**前提条件**<br>
・NET SDKをインストール(.net8.0 .net9.0)<br>
・BenchmarkDotNetをインストール<br>

・.csprojのターゲットフレームワークを.net9.0と.net8.0の複数に書き換える<br>
```
<TargetFrameworks>net9.0;net8.0</TargetFrameworks>
```

・Programを下記のコードに書き換える<br>
```
using BenchmarkDotNet.Running;

public class Program
{
    static void Main(string[] args)
    {
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
```

・計測したいベンチマークをご自身のコンソールへコピー

■**使用方法**<br>

・**コンソール**<br>
　プログラムを実行し計測したいBenchmarkを選択する(「*」すべて or「0～16(個々のベンチマーク)」)

・**コマンドプロンプト**<br>
　Benchmark.csprojがあるプロジェクトのルートディレクトリまで移動して下記を実行<br>
　dotnet run -c Release -f net9.0 --filter "*"　　全てのベンチマークを実行するコマンド<br>
　dotnet run -c Release -f net9.0 --filter "*CommandStringUtf8Benchmark*"　　特定のベンチマークを実行するコマンド<br>

■**ベンチマーク**<br>
| BenchmarkName                 | Explanation    |
|------------------------------ |--------------- |
|[**CommandStringUtf8Benchmark**](https://gitan.dev/?p=213)|文字列とUtf8のbyte[]のコマンド比較ベンチマーク|
|**ByteArrayROSSplitBenchmark**|byte[]をReadOnlySpan<byte>で分けた時と比較したベンチマーク|
|[**DivShiftBenchmark**](https://gitan.dev/?p=275)|Int、UInt、Long、ULongの整数の割り算を比較したベンチマーク|
|[**ForForeachBenchmark**](https://gitan.dev/?p=180)|配列かListをfor、foreachで要素を足していくベンチマーク|
|[**HighPerformanceStringBenchmark**](https://gitan.dev/?p=336)|stringの作成を比較したベンチマーク|
|**IntLongUtf8FormatBenchmark**|int、longをUTF-8バイト配列に変換する方法のベンチマーク|
|[**ListSortBenchmark**](https://gitan.dev/?p=124)|Listの並び替えの速度を比較したベンチマーク|
|[**ReferenceUpdateBenchmark**](https://gitan.dev/?p=171)|APIリクエストの署名生成とリクエスト送信のパフォーマンスベンチマーク|
|[**CopyPerformanceBenchmark**](https://gitan.dev/?p=55)|byte[]のCopyでSpanを使った速度比較|
|**SpanToArrayDirectArrayBenchmark**|longをSpan<byte>とbyte[]に変換した場合の比較ベンチマーク|
|[**StreamCopyBenchmark**](https://gitan.dev/?p=180)|Streamのデータを読み込む方法を比較したベンチマーク|
|[**StringDollerBenchmark**](https://gitan.dev/?p=148)|文字列結合のパフォーマンスベンチマーク|
|[**TenToTheNConversionBenchmark**](https://gitan.dev/?p=230)|longで10のn乗するベンチマーク|
|**ToStringToArrayBenchmark**|数値を文字列やバイト配列に変換する際のパフォーマンスベンチマーク|
|[**UnixTimeBenchmark**](https://gitan.dev/?p=358)|UnixTimeを作る方法のベンチマーク|
|[**Utf8JsonBenchmark**](https://gitan.dev/?p=320)|Utf8文字列の作り方とパフォーマンス|
|[**VariousBenchmark**](https://gitan.dev/?p=109)|C#のいろいろな、遅くなる要素のベンチマーク|

■**詳細**<br>
・[**CommandStringUtf8Benchmark**](https://gitan.dev/?p=213)　　文字列とUtf8のbyte[]のコマンド比較ベンチマーク

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

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method                        | Job        | Runtime  | Mean       | Error     | StdDev    | Median     | Ratio | RatioSD |
|------------------------------ |----------- |--------- |-----------:|----------:|----------:|-----------:|------:|--------:|
| StringSwitchBench             | Job-DDXAAB | .NET 8.0 |   7.044 ns | 0.1661 ns | 0.3575 ns |   7.007 ns |  1.00 |    0.07 |
| StringSwitchBench             | Job-WLOQWX | .NET 9.0 |   7.962 ns | 0.1835 ns | 0.4502 ns |   7.854 ns |  1.13 |    0.08 |
| StringIfBench                 | Job-DDXAAB | .NET 8.0 |   6.836 ns | 0.1606 ns | 0.4029 ns |   6.765 ns |  1.00 |    0.08 |
| StringIfBench                 | Job-WLOQWX | .NET 9.0 |   7.461 ns | 0.1703 ns | 0.2273 ns |   7.403 ns |  1.10 |    0.07 |
| StaticBytesSequenceEqualBench | Job-DDXAAB | .NET 8.0 | 167.429 ns | 3.3318 ns | 6.4192 ns | 165.706 ns |  1.00 |    0.05 |
| StaticBytesSequenceEqualBench | Job-WLOQWX | .NET 9.0 |  62.324 ns | 0.3659 ns | 0.2857 ns |  62.367 ns |  0.37 |    0.01 |
| StaticRosSequenceEqualBench   | Job-DDXAAB | .NET 8.0 |  20.165 ns | 0.4247 ns | 0.9673 ns |  19.744 ns |  1.00 |    0.07 |
| StaticRosSequenceEqualBench   | Job-WLOQWX | .NET 9.0 |  18.088 ns | 0.1299 ns | 0.1085 ns |  18.090 ns |  0.90 |    0.04 |
| StaticBytesMyEqualsBench      | Job-DDXAAB | .NET 8.0 |  18.799 ns | 0.3960 ns | 0.7724 ns |  18.741 ns |  1.00 |    0.06 |
| StaticBytesMyEqualsBench      | Job-WLOQWX | .NET 9.0 |  16.243 ns | 0.3189 ns | 0.3412 ns |  16.091 ns |  0.87 |    0.04 |
| U8BytesBench                  | Job-DDXAAB | .NET 8.0 |   9.424 ns | 0.1999 ns | 0.5300 ns |   9.326 ns |  1.00 |    0.08 |
| U8BytesBench                  | Job-WLOQWX | .NET 9.0 |   7.160 ns | 0.1011 ns | 0.1038 ns |   7.118 ns |  0.76 |    0.04 |
```

・**ByteArrayROSSplitBenchmark**　　byte[]をReadOnlySpan<byte>で分けた時と比較したベンチマーク

    [Benchmark]
    public int ReadOnlySpanSplitBench()
    {
        int sum = 0;

        for (int i = 0; i < 100; i++)
        {
            ReadOnlySpanSplit(SampleBytes, out var left, out var right);
            sum += left.Length + right.Length;
        }

        return sum;
    }

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method                 | Job        | Runtime  | Mean       | Error    | StdDev   | Ratio | RatioSD |
|----------------------- |----------- |--------- |-----------:|---------:|---------:|------:|--------:|
| ByteArraySplitBench    | Job-DDXAAB | .NET 8.0 | 1,638.0 ns | 20.34 ns | 18.03 ns |  1.00 |    0.02 |
| ByteArraySplitBench    | Job-WLOQWX | .NET 9.0 | 1,615.0 ns | 32.23 ns | 64.37 ns |  0.99 |    0.04 |
| ReadOnlySpanSplitBench | Job-DDXAAB | .NET 8.0 |   453.1 ns |  8.79 ns | 10.80 ns |  1.00 |    0.03 |
| ReadOnlySpanSplitBench | Job-WLOQWX | .NET 9.0 |   439.8 ns |  3.71 ns |  2.90 ns |  0.97 |    0.02 |
```

・[**DivShiftBenchmark**](https://gitan.dev/?p=275)　　Int、UInt、Long、ULongの整数の割り算を比較したベンチマーク

    [Benchmark]
    public int Div10IntBench()
    {
        int sum = 0;
        for (int i = 0; i < 10000; i++)
        {
            sum += i / 10;
        }
        return sum;
    }


```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method                 | Job        | Runtime  | Mean     | Error     | StdDev    | Ratio | RatioSD |
|----------------------- |----------- |--------- |---------:|----------:|----------:|------:|--------:|
| Div10IntBench          | Job-DDXAAB | .NET 8.0 | 5.251 us | 0.0497 us | 0.0415 us |  1.00 |    0.01 |
| Div10IntBench          | Job-WLOQWX | .NET 9.0 | 5.170 us | 0.0286 us | 0.0254 us |  0.98 |    0.01 |
| Div100IntBench         | Job-DDXAAB | .NET 8.0 | 5.277 us | 0.0608 us | 0.0568 us |  1.00 |    0.01 |
| Div100IntBench         | Job-WLOQWX | .NET 9.0 | 5.154 us | 0.0279 us | 0.0247 us |  0.98 |    0.01 |
| Div1000IntBench        | Job-DDXAAB | .NET 8.0 | 5.265 us | 0.0636 us | 0.0595 us |  1.00 |    0.02 |
| Div1000IntBench        | Job-WLOQWX | .NET 9.0 | 5.138 us | 0.0278 us | 0.0232 us |  0.98 |    0.01 |
| PowShift10IntBench     | Job-DDXAAB | .NET 8.0 | 2.306 us | 0.0243 us | 0.0228 us |  1.00 |    0.01 |
| PowShift10IntBench     | Job-WLOQWX | .NET 9.0 | 2.271 us | 0.0108 us | 0.0096 us |  0.99 |    0.01 |
| PowShift100IntBench    | Job-DDXAAB | .NET 8.0 | 2.283 us | 0.0146 us | 0.0129 us |  1.00 |    0.01 |
| PowShift100IntBench    | Job-WLOQWX | .NET 9.0 | 2.266 us | 0.0138 us | 0.0129 us |  0.99 |    0.01 |
| PowShift1000IntBench   | Job-DDXAAB | .NET 8.0 | 2.297 us | 0.0250 us | 0.0233 us |  1.00 |    0.01 |
| PowShift1000IntBench   | Job-WLOQWX | .NET 9.0 | 2.284 us | 0.0244 us | 0.0204 us |  0.99 |    0.01 |
| Div10LongBench         | Job-DDXAAB | .NET 8.0 | 5.482 us | 0.0669 us | 0.0625 us |  1.00 |    0.02 |
| Div10LongBench         | Job-WLOQWX | .NET 9.0 | 5.176 us | 0.0442 us | 0.0392 us |  0.94 |    0.01 |
| Div100LongBench        | Job-DDXAAB | .NET 8.0 | 7.209 us | 0.0771 us | 0.0722 us |  1.00 |    0.01 |
| Div100LongBench        | Job-WLOQWX | .NET 9.0 | 7.082 us | 0.0390 us | 0.0346 us |  0.98 |    0.01 |
| Div1000LongBench       | Job-DDXAAB | .NET 8.0 | 5.457 us | 0.0464 us | 0.0387 us |  1.00 |    0.01 |
| Div1000LongBench       | Job-WLOQWX | .NET 9.0 | 5.188 us | 0.0542 us | 0.0480 us |  0.95 |    0.01 |
| PowShift10LongBench    | Job-DDXAAB | .NET 8.0 | 2.271 us | 0.0204 us | 0.0170 us |  1.00 |    0.01 |
| PowShift10LongBench    | Job-WLOQWX | .NET 9.0 | 2.283 us | 0.0154 us | 0.0129 us |  1.01 |    0.01 |
| PowShift100LongBench   | Job-DDXAAB | .NET 8.0 | 2.282 us | 0.0250 us | 0.0234 us |  1.00 |    0.01 |
| PowShift100LongBench   | Job-WLOQWX | .NET 9.0 | 2.286 us | 0.0124 us | 0.0116 us |  1.00 |    0.01 |
| PowShift1000LongBench  | Job-DDXAAB | .NET 8.0 | 2.310 us | 0.0443 us | 0.0435 us |  1.00 |    0.03 |
| PowShift1000LongBench  | Job-WLOQWX | .NET 9.0 | 2.302 us | 0.0194 us | 0.0172 us |  1.00 |    0.02 |
| Div10UIntBench         | Job-DDXAAB | .NET 8.0 | 4.073 us | 0.0376 us | 0.0352 us |  1.00 |    0.01 |
| Div10UIntBench         | Job-WLOQWX | .NET 9.0 | 4.088 us | 0.0361 us | 0.0302 us |  1.00 |    0.01 |
| Div100UIntBench        | Job-DDXAAB | .NET 8.0 | 2.599 us | 0.0212 us | 0.0198 us |  1.00 |    0.01 |
| Div100UIntBench        | Job-WLOQWX | .NET 9.0 | 2.720 us | 0.0372 us | 0.0348 us |  1.05 |    0.02 |
| Div1000UIntBench       | Job-DDXAAB | .NET 8.0 | 2.598 us | 0.0204 us | 0.0191 us |  1.00 |    0.01 |
| Div1000UIntBench       | Job-WLOQWX | .NET 9.0 | 2.684 us | 0.0152 us | 0.0127 us |  1.03 |    0.01 |
| PowShift10UIntBench    | Job-DDXAAB | .NET 8.0 | 2.369 us | 0.0472 us | 0.0561 us |  1.00 |    0.03 |
| PowShift10UIntBench    | Job-WLOQWX | .NET 9.0 | 2.317 us | 0.0208 us | 0.0184 us |  0.98 |    0.02 |
| PowShift100UIntBench   | Job-DDXAAB | .NET 8.0 | 2.314 us | 0.0341 us | 0.0302 us |  1.00 |    0.02 |
| PowShift100UIntBench   | Job-WLOQWX | .NET 9.0 | 2.297 us | 0.0239 us | 0.0199 us |  0.99 |    0.01 |
| PowShift1000UIntBench  | Job-DDXAAB | .NET 8.0 | 2.310 us | 0.0274 us | 0.0243 us |  1.00 |    0.01 |
| PowShift1000UIntBench  | Job-WLOQWX | .NET 9.0 | 2.283 us | 0.0269 us | 0.0252 us |  0.99 |    0.01 |
| Div10ULongBench        | Job-DDXAAB | .NET 8.0 | 4.633 us | 0.0857 us | 0.0802 us |  1.00 |    0.02 |
| Div10ULongBench        | Job-WLOQWX | .NET 9.0 | 4.530 us | 0.0242 us | 0.0227 us |  0.98 |    0.02 |
| Div100ULongBench       | Job-DDXAAB | .NET 8.0 | 5.528 us | 0.0457 us | 0.0427 us |  1.00 |    0.01 |
| Div100ULongBench       | Job-WLOQWX | .NET 9.0 | 4.557 us | 0.0422 us | 0.0374 us |  0.82 |    0.01 |
| Div1000ULongBench      | Job-DDXAAB | .NET 8.0 | 5.492 us | 0.0662 us | 0.0619 us |  1.00 |    0.02 |
| Div1000ULongBench      | Job-WLOQWX | .NET 9.0 | 4.568 us | 0.0317 us | 0.0297 us |  0.83 |    0.01 |
| PowShift10ULongBench   | Job-DDXAAB | .NET 8.0 | 2.322 us | 0.0376 us | 0.0352 us |  1.00 |    0.02 |
| PowShift10ULongBench   | Job-WLOQWX | .NET 9.0 | 2.274 us | 0.0140 us | 0.0124 us |  0.98 |    0.02 |
| PowShift100ULongBench  | Job-DDXAAB | .NET 8.0 | 2.298 us | 0.0319 us | 0.0267 us |  1.00 |    0.02 |
| PowShift100ULongBench  | Job-WLOQWX | .NET 9.0 | 2.268 us | 0.0067 us | 0.0062 us |  0.99 |    0.01 |
| PowShift1000ULongBench | Job-DDXAAB | .NET 8.0 | 2.288 us | 0.0310 us | 0.0275 us |  1.00 |    0.02 |
| PowShift1000ULongBench | Job-WLOQWX | .NET 9.0 | 2.267 us | 0.0127 us | 0.0119 us |  0.99 |    0.01 |
```

・[**ForForeachBenchmark**](https://gitan.dev/?p=180)　　配列かListをfor、foreachで要素を足していくベンチマーク


```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method                  | Job        | Runtime  | Mean      | Error    | StdDev    | Ratio | RatioSD |
|------------------------ |----------- |--------- |----------:|---------:|----------:|------:|--------:|
| ArrayForBench           | Job-DDXAAB | .NET 8.0 |  30.07 ns | 0.535 ns |  0.501 ns |  1.00 |    0.02 |
| ArrayForBench           | Job-WLOQWX | .NET 9.0 |  32.40 ns | 0.674 ns |  0.631 ns |  1.08 |    0.03 |
| ArrayForEachBench       | Job-DDXAAB | .NET 8.0 |  30.15 ns | 0.605 ns |  0.595 ns |  1.00 |    0.03 |
| ArrayForEachBench       | Job-WLOQWX | .NET 9.0 |  32.15 ns | 0.672 ns |  0.774 ns |  1.07 |    0.03 |
| ArrayAsSpanForEachBench | Job-DDXAAB | .NET 8.0 |  31.13 ns | 0.375 ns |  0.332 ns |  1.00 |    0.01 |
| ArrayAsSpanForEachBench | Job-WLOQWX | .NET 9.0 |  31.12 ns | 0.648 ns |  0.636 ns |  1.00 |    0.02 |
| ListForBench            | Job-DDXAAB | .NET 8.0 |  53.22 ns | 1.073 ns |  1.004 ns |  1.00 |    0.03 |
| ListForBench            | Job-WLOQWX | .NET 9.0 |  52.14 ns | 1.061 ns |  1.744 ns |  0.98 |    0.04 |
| ListForEachBench        | Job-DDXAAB | .NET 8.0 |  53.41 ns | 1.046 ns |  1.396 ns |  1.00 |    0.04 |
| ListForEachBench        | Job-WLOQWX | .NET 9.0 |  51.76 ns | 1.046 ns |  1.245 ns |  0.97 |    0.03 |
| ListAsSpanForEachBench  | Job-DDXAAB | .NET 8.0 |  29.73 ns | 0.357 ns |  0.278 ns |  1.00 |    0.01 |
| ListAsSpanForEachBench  | Job-WLOQWX | .NET 9.0 |  32.69 ns | 0.667 ns |  0.956 ns |  1.10 |    0.03 |
| LinkedListForEachBench  | Job-DDXAAB | .NET 8.0 | 103.59 ns | 0.853 ns |  0.798 ns |  1.00 |    0.01 |
| LinkedListForEachBench  | Job-WLOQWX | .NET 9.0 |  98.51 ns | 0.854 ns |  0.757 ns |  0.95 |    0.01 |
| SortedSetForEachBench   | Job-DDXAAB | .NET 8.0 | 430.95 ns | 8.629 ns | 11.812 ns |  1.00 |    0.04 |
| SortedSetForEachBench   | Job-WLOQWX | .NET 9.0 | 425.45 ns | 4.880 ns |  4.326 ns |  0.99 |    0.03 |
| HashSetForEachBench     | Job-DDXAAB | .NET 8.0 |  97.53 ns | 1.333 ns |  1.247 ns |  1.00 |    0.02 |
| HashSetForEachBench     | Job-WLOQWX | .NET 9.0 |  77.11 ns | 1.553 ns |  2.325 ns |  0.79 |    0.03 |
| StackForEachBench       | Job-DDXAAB | .NET 8.0 | 181.10 ns | 3.565 ns |  5.551 ns |  1.00 |    0.04 |
| StackForEachBench       | Job-WLOQWX | .NET 9.0 | 182.40 ns | 3.610 ns |  6.602 ns |  1.01 |    0.05 |
| QueueForEachBench       | Job-DDXAAB | .NET 8.0 | 222.52 ns | 4.358 ns |  4.475 ns |  1.00 |    0.03 |
| QueueForEachBench       | Job-WLOQWX | .NET 9.0 | 218.69 ns | 4.135 ns |  3.868 ns |  0.98 |    0.03 |
```

・[**HighPerformanceStringBenchmark**](https://gitan.dev/?p=336)　　stringの作成を比較したベンチマーク

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


```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method                        | Job        | Runtime  | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD |
|------------------------------ |----------- |--------- |----------:|----------:|----------:|----------:|------:|--------:|
| ToHexStringBench              | Job-PUSYPP | .NET 8.0 | 471.57 ns |  7.829 ns |  9.320 ns | 469.43 ns |  1.00 |    0.03 |
| ToHexStringBench              | Job-FMYSKB | .NET 9.0 | 478.20 ns |  9.401 ns | 11.545 ns | 476.75 ns |  1.01 |    0.03 |
| ToHexString_SpanBench         | Job-PUSYPP | .NET 8.0 |  47.45 ns |  0.983 ns |  1.133 ns |  47.04 ns |  1.00 |    0.03 |
| ToHexString_SpanBench         | Job-FMYSKB | .NET 9.0 |  48.87 ns |  0.978 ns |  1.201 ns |  48.78 ns |  1.03 |    0.03 |
| ToHexString_CreateBench       | Job-PUSYPP | .NET 8.0 |  39.58 ns |  0.258 ns |  0.201 ns |  39.50 ns |  1.00 |    0.01 |
| ToHexString_CreateBench       | Job-FMYSKB | .NET 9.0 |  41.52 ns |  0.786 ns |  1.335 ns |  41.30 ns |  1.05 |    0.03 |
| ToHexString_CreateUnsafeBench | Job-PUSYPP | .NET 8.0 |  32.07 ns |  0.220 ns |  0.205 ns |  32.06 ns |  1.00 |    0.01 |
| ToHexString_CreateUnsafeBench | Job-FMYSKB | .NET 9.0 |  34.86 ns |  1.678 ns |  4.869 ns |  33.40 ns |  1.09 |    0.15 |
```

・**IntLongUtf8FormatBenchmark**　　int、longをUTF-8バイト配列に変換する方法のベンチマーク

    static uint Div1000(uint value) => value * 8389 >> 23;
    static uint Div100(uint value) => value * 5243 >> 19;
    static uint Div10(uint value) => value * 6554 >> 16;

    public static int IntWriteUtf8(int value, Span<byte> destination)
    {

        int offset = 0;
        int charsWritten;

        uint num1, num2, num3, div;
        int valueB;

        if (value < 0)
        {
            if (value == int.MinValue)
            {
                ReadOnlySpan<byte> minValue = "-2147483648"u8;

                charsWritten = minValue.Length;
                minValue.CopyTo(destination);
                return charsWritten;
            }

            destination[offset++] = (byte)'-';
            valueB = unchecked(-value);
        }
        //else
        //{
        //    valueB = value;
        //}

        if (value < 10000)
        {
            num1 = (uint)value;
            if (num1 < 10) { goto L1; }
            if (num1 < 100) { goto L2; }
            if (num1 < 1000) { goto L3; }
            goto L4;
        }
        else
        {
            valueB = value / 10000;
            num1 = (uint)(value - valueB * 10000);
            if (valueB < 10000)
            {
                num2 = (uint)valueB;
                if (num2 < 100)
                {
                    if (num2 < 10) { goto L5; }
                    goto L6;
                }
                if (num2 < 1000) { goto L7; }
                goto L8;
            }
            else
            {
                value = valueB / 10000;
                num2 = (uint)(valueB - value * 10000);

                num3 = (uint)value;
                if (num3 < 100)
                {
                    if (num3 < 10) { goto L9; }
                    goto L10;
                }
                if (num3 < 1000) { goto L11; }
                goto L12;

            L12:
                destination[offset++] = (byte)('0' + (div = Div1000(num3)));
                num3 -= div * 1000;
            L11:
                destination[offset++] = (byte)('0' + (div = Div100(num3)));
                num3 -= div * 100;
            L10:
                destination[offset++] = (byte)('0' + (div = Div10(num3)));
                num3 -= div * 10;
            L9:
                destination[offset++] = (byte)('0' + num3);

            }
        L8:
            destination[offset++] = (byte)('0' + (div = Div1000(num2)));
            num2 -= div * 1000;
        L7:
            destination[offset++] = (byte)('0' + (div = Div100(num2)));
            num2 -= div * 100;
        L6:
            destination[offset++] = (byte)('0' + (div = Div10(num2)));
            num2 -= div * 10;
        L5:
            destination[offset++] = (byte)('0' + num2);

        }

    L4:
        destination[offset++] = (byte)('0' + (div = Div1000(num1)));
        num1 -= div * 1000;
    L3:
        destination[offset++] = (byte)('0' + (div = Div100(num1)));
        num1 -= div * 100;
    L2:
        destination[offset++] = (byte)('0' + (div = Div10(num1)));
        num1 -= div * 10;
    L1:
        destination[offset++] = (byte)('0' + num1);

        charsWritten = offset;
        return charsWritten;
    }

    [Benchmark]
    public int IntUtf8Write()
    {
        Span<byte> buffer = stackalloc byte[20];

        var result = IntWriteUtf8(i, buffer);

        return result;
    }  

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method        | Job        | Runtime  | Mean     | Error     | StdDev    | Median   | Ratio | RatioSD |
|-------------- |----------- |--------- |---------:|----------:|----------:|---------:|------:|--------:|
| IntTryFormat  | Job-DDXAAB | .NET 8.0 | 5.403 ns | 0.1318 ns | 0.1667 ns | 5.392 ns |  1.00 |    0.04 |
| IntTryFormat  | Job-WLOQWX | .NET 9.0 | 5.355 ns | 0.1334 ns | 0.2155 ns | 5.222 ns |  0.99 |    0.05 |
| LongTryFormat | Job-DDXAAB | .NET 8.0 | 6.562 ns | 0.1522 ns | 0.1979 ns | 6.526 ns |  1.00 |    0.04 |
| LongTryFormat | Job-WLOQWX | .NET 9.0 | 6.202 ns | 0.0929 ns | 0.0776 ns | 6.213 ns |  0.95 |    0.03 |
| IntUtf8Write  | Job-DDXAAB | .NET 8.0 | 7.713 ns | 0.1487 ns | 0.1391 ns | 7.645 ns |  1.00 |    0.02 |
| IntUtf8Write  | Job-WLOQWX | .NET 9.0 | 6.950 ns | 0.0623 ns | 0.0552 ns | 6.943 ns |  0.90 |    0.02 |
| LongUtf8Write | Job-DDXAAB | .NET 8.0 | 7.748 ns | 0.1710 ns | 0.1756 ns | 7.740 ns |  1.00 |    0.03 |
| LongUtf8Write | Job-WLOQWX | .NET 9.0 | 7.825 ns | 0.1017 ns | 0.0951 ns | 7.819 ns |  1.01 |    0.03 |
```

・[**ListSortBenchmark**](https://gitan.dev/?p=124)　　Listの並び替えの速度を比較したベンチマーク

    [Benchmark]
    public List<int> ListSort()
    {
        var list = _originalList.ToList();

        list.Sort();

        return list;
    }

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method                  | Job        | Runtime  | Mean     | Error     | StdDev    | Ratio | RatioSD |
|------------------------ |----------- |--------- |---------:|----------:|----------:|------:|--------:|
| ListSort                | Job-DDXAAB | .NET 8.0 | 4.401 ms | 0.0776 ms | 0.0688 ms |  1.00 |    0.02 |
| ListSort                | Job-WLOQWX | .NET 9.0 | 4.348 ms | 0.0178 ms | 0.0157 ms |  0.99 |    0.02 |
| ListSortReverse         | Job-DDXAAB | .NET 8.0 | 4.449 ms | 0.0576 ms | 0.0539 ms |  1.00 |    0.02 |
| ListSortReverse         | Job-WLOQWX | .NET 9.0 | 4.406 ms | 0.0164 ms | 0.0145 ms |  0.99 |    0.01 |
| ListSortReverseComparer | Job-DDXAAB | .NET 8.0 | 5.320 ms | 0.0347 ms | 0.0325 ms |  1.00 |    0.01 |
| ListSortReverseComparer | Job-WLOQWX | .NET 9.0 | 4.613 ms | 0.0212 ms | 0.0198 ms |  0.87 |    0.01 |
```

・[**ReferenceUpdateBenchmark**](https://gitan.dev/?p=171)　　APIリクエストの署名生成とリクエスト送信のパフォーマンスベンチマーク

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

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method    | Job        | Runtime  | Mean       | Error    | StdDev   | Ratio | RatioSD |
|---------- |----------- |--------- |-----------:|---------:|---------:|------:|--------:|
| Reference | Job-DDXAAB | .NET 8.0 | 1,915.3 ns | 23.15 ns | 19.33 ns |  1.00 |    0.01 |
| Reference | Job-WLOQWX | .NET 9.0 | 1,954.3 ns | 16.42 ns | 13.71 ns |  1.02 |    0.01 |
| Update    | Job-DDXAAB | .NET 8.0 |   831.6 ns |  8.33 ns |  7.39 ns |  1.00 |    0.01 |
| Update    | Job-WLOQWX | .NET 9.0 |   887.3 ns | 17.51 ns | 16.38 ns |  1.07 |    0.02 |
```
　
・[**CopyPerformanceBenchmark**](https://gitan.dev/?p=55)　　byte[]のCopyでSpanを使った速度比較

    [Benchmark]
    public byte[] CopyArray()
    {
        var result = new byte[50];
        for (int i = 0; i < 10; i++)
        {
            data.CopyTo(result, i * 5);
        }
        return result;
    }

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method    | Job        | Runtime  | Mean     | Error    | StdDev   | Ratio | RatioSD |
|---------- |----------- |--------- |---------:|---------:|---------:|------:|--------:|
| CopyArray | Job-DDXAAB | .NET 8.0 | 51.27 ns | 0.846 ns | 0.791 ns |  1.00 |    0.02 |
| CopyArray | Job-WLOQWX | .NET 9.0 | 52.23 ns | 0.596 ns | 0.528 ns |  1.02 |    0.02 |
| CopySpan  | Job-DDXAAB | .NET 8.0 | 29.84 ns | 0.629 ns | 0.673 ns |  1.00 |    0.03 |
| CopySpan  | Job-WLOQWX | .NET 9.0 | 29.47 ns | 0.399 ns | 0.373 ns |  0.99 |    0.02 |
```

・**SpanToArrayDirectArrayBenchmark**　　longをSpan<byte>とbyte[]に変換した場合の比較ベンチマーク
 
    [Benchmark]
    public void SpanToArray()
    {
        GetBytesFromInt64_SpanToArray(value);
    }  

　　public static byte[] GetBytesFromInt64_SpanToArray(long value)
    {
        Span<byte> buffer = stackalloc byte[21];

        int offset = 0;
        uint num1, num2, num3, num4, num5, div;
        ulong valueA, valueB;

        if (value < 0)
        {
            if (value == long.MinValue)
            {
                ReadOnlySpan<byte> minValue = "-9223372036854775808"u8;
                return minValue.ToArray();
            }

            buffer[offset++] = (byte)'-';
            valueA = (ulong)(unchecked(-value));
        }
        else
        {
            valueA = (ulong)value;
        }

        if (valueA < 10000)
        {
            num1 = (uint)valueA;
            if (num1 < 10) { goto L1; }
            if (num1 < 100) { goto L2; }
            if (num1 < 1000) { goto L3; }
            goto L4;
        }
        else
        {
            valueB = valueA / 10000;
            num1 = (uint)(valueA - valueB * 10000);
            if (valueB < 10000)
            {
                num2 = (uint)valueB;
                if (num2 < 100)
                {
                    if (num2 < 10) { goto L5; }
                    goto L6;
                }
                if (num2 < 1000) { goto L7; }
                goto L8;
            }
            else
            {
                valueA = valueB / 10000;
                num2 = (uint)(valueB - valueA * 10000);
                if (valueA < 10000)
                {
                    num3 = (uint)valueA;
                    if (num3 < 100)
                    {
                        if (num3 < 10) { goto L9; }
                        goto L10;
                    }
                    if (num3 < 1000) { goto L11; }
                    goto L12;
                }
                else
                {
                    valueB = valueA / 10000;
                    num3 = (uint)(valueA - valueB * 10000);
                    if (valueB < 10000)
                    {
                        num4 = (uint)valueB;
                        if (num4 < 100)
                        {
                            if (num4 < 10) { goto L13; }
                            goto L14;
                        }
                        if (num4 < 1000) { goto L15; }
                        goto L16;
                    }
                    else
                    {
                        valueA = valueB / 10000;
                        num4 = (uint)(valueB - valueA * 10000);
                        //if (value2 < 10000)
                        {
                            num5 = (uint)valueA;
                            if (num5 < 100)
                            {
                                if (num5 < 10) { goto L17; }
                                goto L18;
                            }
                            if (num5 < 1000) { goto L19; }
                            goto L20;
                        }
                    L20:
                        buffer[offset++] = (byte)('0' + (div = (num5 * 8389) >> 23));
                        num5 -= div * 1000;
                    L19:
                        buffer[offset++] = (byte)('0' + (div = (num5 * 5243) >> 19));
                        num5 -= div * 100;
                    L18:
                        buffer[offset++] = (byte)('0' + (div = (num5 * 6554) >> 16));
                        num5 -= div * 10;
                    L17:
                        buffer[offset++] = (byte)('0' + (num5));
                    }
                L16:
                    buffer[offset++] = (byte)('0' + (div = (num4 * 8389) >> 23));
                    num4 -= div * 1000;
                L15:
                    buffer[offset++] = (byte)('0' + (div = (num4 * 5243) >> 19));
                    num4 -= div * 100;
                L14:
                    buffer[offset++] = (byte)('0' + (div = (num4 * 6554) >> 16));
                    num4 -= div * 10;
                L13:
                    buffer[offset++] = (byte)('0' + (num4));
                }
            L12:
                buffer[offset++] = (byte)('0' + (div = (num3 * 8389) >> 23));
                num3 -= div * 1000;
            L11:
                buffer[offset++] = (byte)('0' + (div = (num3 * 5243) >> 19));
                num3 -= div * 100;
            L10:
                buffer[offset++] = (byte)('0' + (div = (num3 * 6554) >> 16));
                num3 -= div * 10;
            L9:
                buffer[offset++] = (byte)('0' + (num3));
            }
        L8:
            buffer[offset++] = (byte)('0' + (div = (num2 * 8389) >> 23));
            num2 -= div * 1000;
        L7:
            buffer[offset++] = (byte)('0' + (div = (num2 * 5243) >> 19));
            num2 -= div * 100;
        L6:
            buffer[offset++] = (byte)('0' + (div = (num2 * 6554) >> 16));
            num2 -= div * 10;
        L5:
            buffer[offset++] = (byte)('0' + (num2));
        }
    L4:
        buffer[offset++] = (byte)('0' + (div = (num1 * 8389) >> 23));
        num1 -= div * 1000;
    L3:
        buffer[offset++] = (byte)('0' + (div = (num1 * 5243) >> 19));
        num1 -= div * 100;
    L2:
        buffer[offset++] = (byte)('0' + (div = (num1 * 6554) >> 16));
        num1 -= div * 10;
    L1:
        buffer[offset++] = (byte)('0' + (num1));

        return buffer[..offset].ToArray();
    }

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method      | Job        | Runtime  | Mean     | Error    | StdDev   | Ratio | RatioSD |
|------------ |----------- |--------- |---------:|---------:|---------:|------:|--------:|
| SpanToArray | Job-DDXAAB | .NET 8.0 | 12.40 ns | 0.100 ns | 0.089 ns |  1.00 |    0.01 |
| SpanToArray | Job-WLOQWX | .NET 9.0 | 12.86 ns | 0.095 ns | 0.085 ns |  1.04 |    0.01 |
| DirectArray | Job-DDXAAB | .NET 8.0 | 11.84 ns | 0.067 ns | 0.059 ns |  1.00 |    0.01 |
| DirectArray | Job-WLOQWX | .NET 9.0 | 12.59 ns | 0.271 ns | 0.333 ns |  1.06 |    0.03 |
```

・[**StreamCopyBenchmark**](https://gitan.dev/?p=180)　　Streamのデータを読み込む方法を比較したベンチマーク

    public static byte[] Source { get; set; } = Enumerable.Range(32, 80).Select(x => (byte)x).ToArray();
    public static byte[] Buffer { get; set; } = new byte[8192];

    public static MemoryStream ShareMemoryStream { get; set; } = new MemoryStream(Buffer);

    [Benchmark]
    public byte[] ToArray()
    {
        return Source.ToArray();
    }

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method                    | Job        | Runtime  | Mean      | Error     | StdDev    | Median    | Ratio | RatioSD |
|-------------------------- |----------- |--------- |----------:|----------:|----------:|----------:|------:|--------:|
| ToArray                   | Job-DDXAAB | .NET 8.0 |  26.73 ns |  0.557 ns |  0.572 ns |  26.63 ns |  1.00 |    0.03 |
| ToArray                   | Job-WLOQWX | .NET 9.0 |  20.80 ns |  0.464 ns |  0.998 ns |  20.57 ns |  0.78 |    0.04 |
| MemoryStreamCopyUseBuffer | Job-DDXAAB | .NET 8.0 |  49.36 ns |  1.012 ns |  2.345 ns |  48.78 ns |  1.00 |    0.07 |
| MemoryStreamCopyUseBuffer | Job-WLOQWX | .NET 9.0 |  45.58 ns |  0.960 ns |  2.677 ns |  44.82 ns |  0.93 |    0.07 |
| MemoryStreamCopyUseShare  | Job-DDXAAB | .NET 8.0 |  39.69 ns |  0.648 ns |  0.606 ns |  39.71 ns |  1.00 |    0.02 |
| MemoryStreamCopyUseShare  | Job-WLOQWX | .NET 9.0 |  40.12 ns |  0.838 ns |  0.743 ns |  39.88 ns |  1.01 |    0.02 |
| MemoryStreamCopyNoBuffer  | Job-DDXAAB | .NET 8.0 |  91.10 ns |  1.163 ns |  1.031 ns |  91.04 ns |  1.00 |    0.02 |
| MemoryStreamCopyNoBuffer  | Job-WLOQWX | .NET 9.0 |  82.76 ns |  1.648 ns |  1.618 ns |  82.43 ns |  0.91 |    0.02 |
| StringStreamCopy          | Job-DDXAAB | .NET 8.0 | 609.81 ns | 11.380 ns | 25.217 ns | 606.42 ns |  1.00 |    0.06 |
| StringStreamCopy          | Job-WLOQWX | .NET 9.0 | 548.01 ns | 10.706 ns | 10.514 ns | 544.07 ns |  0.90 |    0.04 |
```

・[**StringDollerBenchmark**](https://gitan.dev/?p=148)　　文字列結合のパフォーマンスベンチマーク

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

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method        | Job        | Runtime  | Mean      | Error     | StdDev    | Ratio | RatioSD |
|-------------- |----------- |--------- |----------:|----------:|----------:|------:|--------:|
| StringPlus4   | Job-DDXAAB | .NET 8.0 |  9.000 ms | 0.1336 ms | 0.1591 ms |  1.00 |    0.02 |
| StringPlus4   | Job-WLOQWX | .NET 9.0 |  4.818 ms | 0.0818 ms | 0.0803 ms |  0.54 |    0.01 |
| StringPlus5   | Job-DDXAAB | .NET 8.0 | 39.726 ms | 0.6578 ms | 1.1520 ms |  1.00 |    0.04 |
| StringPlus5   | Job-WLOQWX | .NET 9.0 | 30.297 ms | 0.6028 ms | 1.0715 ms |  0.76 |    0.03 |
| DollarFormat4 | Job-DDXAAB | .NET 8.0 |  8.621 ms | 0.1130 ms | 0.1002 ms |  1.00 |    0.02 |
| DollarFormat4 | Job-WLOQWX | .NET 9.0 |  4.891 ms | 0.0961 ms | 0.1144 ms |  0.57 |    0.01 |
| DollarFormat5 | Job-DDXAAB | .NET 8.0 | 45.668 ms | 0.9042 ms | 0.8880 ms |  1.00 |    0.03 |
| DollarFormat5 | Job-WLOQWX | .NET 9.0 | 27.381 ms | 0.2700 ms | 0.2526 ms |  0.60 |    0.01 |
```

・[**TenToTheNConversionBenchmark**](https://gitan.dev/?p=230)　　longで10のn乗するベンチマーク

    static long sourceValue = 123;
    static int powerValue = 11;

     [Benchmark]
    public long RosBench()
    {
        return Power7(sourceValue, powerValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Power7(long baseNumber, int power)
    {
        ReadOnlySpan<long> powerSpan = new long[] {
    0,
    10,
    100,
    1_000,
    10_000,
    100_000,
    1_000_000,
    10_000_000,
    100_000_000,
    1_000_000_000,
    10_000_000_000,
    100_000_000_000,
    1_000_000_000_000,
    10_000_000_000_000,
    100_000_000_000_000,
    1_000_000_000_000_000,
    10_000_000_000_000_000,
    100_000_000_000_000_0000,
    1_000_000_000_000_000_000
　　};

        return baseNumber * powerSpan[power];
    }

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method                  | Job        | Runtime  | Mean       | Error     | StdDev    | Median     | Ratio | RatioSD |
|------------------------ |----------- |--------- |-----------:|----------:|----------:|-----------:|------:|--------:|
| MathPowBench            | Job-DDXAAB | .NET 8.0 | 18.7808 ns | 0.3266 ns | 0.3055 ns | 18.7770 ns |  1.00 |    0.02 |
| MathPowBench            | Job-WLOQWX | .NET 9.0 | 21.0214 ns | 0.3496 ns | 0.2919 ns | 21.0356 ns |  1.12 |    0.02 |
| ForPower10Bench         | Job-DDXAAB | .NET 8.0 |  2.7717 ns | 0.0468 ns | 0.0438 ns |  2.7887 ns |  1.00 |    0.02 |
| ForPower10Bench         | Job-WLOQWX | .NET 9.0 |  2.7439 ns | 0.0781 ns | 0.1428 ns |  2.7185 ns |  0.99 |    0.05 |
| WhileMinusPower10Bench  | Job-DDXAAB | .NET 8.0 |  2.5811 ns | 0.0516 ns | 0.0431 ns |  2.5613 ns |  1.00 |    0.02 |
| WhileMinusPower10Bench  | Job-WLOQWX | .NET 9.0 |  3.1264 ns | 0.0829 ns | 0.0814 ns |  3.1155 ns |  1.21 |    0.04 |
| WhileMinus4Power10Bench | Job-DDXAAB | .NET 8.0 |  0.9920 ns | 0.0448 ns | 0.0786 ns |  0.9768 ns |  1.01 |    0.11 |
| WhileMinus4Power10Bench | Job-WLOQWX | .NET 9.0 |  1.2445 ns | 0.0483 ns | 0.0556 ns |  1.2188 ns |  1.26 |    0.11 |
| SwitchStatementBench    | Job-DDXAAB | .NET 8.0 |  0.4969 ns | 0.0336 ns | 0.0533 ns |  0.4756 ns |  1.01 |    0.15 |
| SwitchStatementBench    | Job-WLOQWX | .NET 9.0 |  0.4859 ns | 0.0287 ns | 0.0403 ns |  0.4754 ns |  0.99 |    0.13 |
| SwitchExpressionBench   | Job-DDXAAB | .NET 8.0 |  0.5067 ns | 0.0335 ns | 0.0691 ns |  0.4883 ns |  1.02 |    0.19 |
| SwitchExpressionBench   | Job-WLOQWX | .NET 9.0 |  0.5182 ns | 0.0346 ns | 0.0588 ns |  0.4984 ns |  1.04 |    0.18 |
| ArrayBench              | Job-DDXAAB | .NET 8.0 |  0.2772 ns | 0.0302 ns | 0.0657 ns |  0.2667 ns |  1.05 |    0.34 |
| ArrayBench              | Job-WLOQWX | .NET 9.0 |  0.2492 ns | 0.0266 ns | 0.0284 ns |  0.2434 ns |  0.95 |    0.24 |
| RosBench                | Job-DDXAAB | .NET 8.0 |  0.2407 ns | 0.0300 ns | 0.0411 ns |  0.2354 ns |  1.03 |    0.25 |
| RosBench                | Job-WLOQWX | .NET 9.0 |  0.2546 ns | 0.0277 ns | 0.0447 ns |  0.2346 ns |  1.09 |    0.27 |
```

・**ToStringToArrayBenchmark**　　数値を文字列やバイト配列に変換する際のパフォーマンスベンチマーク

    static uint Div1000(uint value) => value * 8389 >> 23;
    static uint Div100(uint value) => value * 5243 >> 19;
    static uint Div10(uint value) => value * 6554 >> 16;

    public static bool TryWriteUtf8(long value, Span<byte> destination, out int writtenLength)
    {

        writtenLength = 0;

        int offset = 0;
        int offset2;
        uint num1, num2, num3, num4, num5, num6, num7, num8, div;
        ulong valueA, valueB;

        if (value < 0)
        {
            if (value == long.MinValue)
            {
                ReadOnlySpan<byte> minValue = "-92233720368.54775808"u8;

                writtenLength = minValue.Length;
                return minValue.TryCopyTo(destination);
            }

            if (destination.Length == 0) { return false; }
            destination[offset++] = (byte)'-';
            valueB = (ulong)(unchecked(-value));
        }
        else
        {
            valueB = (ulong)value;
        }

        valueA = valueB / 1__0000_0000;

        uint underPoint = (uint)(valueB - (valueA * 1__0000_0000));

        if (valueA < 10000)
        {
            num1 = (uint)valueA;
            if (num1 < 10)
            {
                if (destination.Length < offset + 1) { return false; }
                goto L1;
            }
            if (num1 < 100)
            {
                if (destination.Length < offset + 2) { return false; }
                goto L2;
            }
            if (num1 < 1000)
            {
                if (destination.Length < offset + 3) { return false; }
                goto L3;
            }
            if (destination.Length < offset + 4) { return false; }
            goto L4;
        }
        else
        {
            valueB = valueA / 10000;
            num1 = (uint)(valueA - valueB * 10000);
            if (valueB < 10000)
            {
                num2 = (uint)valueB;
                if (num2 < 100)
                {
                    if (num2 < 10)
                    {
                        if (destination.Length < offset + 5) { return false; }
                        goto L5;
                    }
                    if (destination.Length < offset + 6) { return false; }
                    goto L6;
                }
                if (num2 < 1000)
                {
                    if (destination.Length < offset + 7) { return false; }
                    goto L7;
                }
                if (destination.Length < offset + 8) { return false; }
                goto L8;
            }
            else
            {
                valueA = valueB / 10000;
                num2 = (uint)(valueB - valueA * 10000);
                num3 = (uint)valueA;
                if (num3 < 100)
                {
                    if (num3 < 10)
                    {
                        if (destination.Length < offset + 9) { return false; }
                        goto L9;
                    }
                    if (destination.Length < offset + 10) { return false; }
                    goto L10;
                }
                if (num3 < 1000)
                {
                    if (destination.Length < offset + 11) { return false; }
                    goto L11;
                }
                if (destination.Length < offset + 12) { return false; }
                goto L12;

            L12:
                destination[offset++] = (byte)('0' + (div = Div1000(num3)));
                num3 -= div * 1000;
            L11:
                destination[offset++] = (byte)('0' + (div = Div100(num3)));
                num3 -= div * 100;
            L10:
                destination[offset++] = (byte)('0' + (div = Div10(num3)));
                num3 -= div * 10;
            L9:
                destination[offset++] = ((byte)('0' + (num3)));
            }
        L8:
            destination[offset++] = (byte)('0' + (div = Div1000(num2)));
            num2 -= div * 1000;
        L7:
            destination[offset++] = (byte)('0' + (div = Div100(num2)));
            num2 -= div * 100;
        L6:
            destination[offset++] = (byte)('0' + (div = Div10(num2)));
            num2 -= div * 10;
        L5:
            destination[offset++] = (byte)('0' + num2);
        }

    L4:
        destination[offset++] = (byte)('0' + (div = Div1000(num1)));
        num1 -= div * 1000;
    L3:
        destination[offset++] = (byte)('0' + (div = Div100(num1)));
        num1 -= div * 100;
    L2:
        destination[offset++] = (byte)('0' + (div = Div10(num1)));
        num1 -= div * 10;
    L1:
        destination[offset++] = (byte)('0' + num1);

        if (underPoint > 0)
        {
            num8 = underPoint;

            num8 -= (num4 = num8 / 10000) * 10000; // 実行後、num4:1～4桁目の値 num8:5～8桁目の値
            num4 -= (num2 = Div100(num4)) * 100; // 実行後、num2:1～2桁目の値 num4:3～4桁目の値
            num2 -= (num1 = Div10(num2)) * 10; // 実行後、num1:1桁目の値 num2:2桁目の値

            if (num8 > 0) // 5～8桁を評価
            {
                // 小数点以下出力は、5桁以上
                num4 -= (num3 = Div10(num4)) * 10; // 実行後、num3:3桁目の値 num4:4桁目の値
                num8 -= (num6 = Div100(num8)) * 100; // 実行後、num6:5～6桁目の値 num8:7～8桁目の値
                num6 -= (num5 = Div10(num6)) * 10; // 実行後、num5:5桁目の値 num6:6桁目の値
                if (num8 > 0) // 7～8桁を評価
                {
                    // 小数点以下出力は、7or8桁
                    num8 -= (num7 = Div10(num8)) * 10; // 実行後、num7:7桁目の値 num8:8桁目の値
                    if (num8 > 0) // 8桁を評価
                    {
                        // 小数点以下出力は、8桁
                        offset2 = offset += 9;
                        if (destination.Length < offset) { return false; }
                        goto LM8;
                    }
                    else
                    {
                        // 小数点以下出力は、7桁
                        offset2 = offset += 8;
                        if (destination.Length < offset) { return false; }
                        goto LM7;
                    }
                }
                else
                {
                    // 小数点以下出力は、5or6桁
                    if (num6 > 0) // 6桁を評価
                    {
                        // 小数点以下出力は、6桁
                        offset2 = offset += 7;
                        if (destination.Length < offset) { return false; }
                        goto LM6;
                    }
                    else
                    {
                        // 小数点以下出力は、5桁
                        offset2 = offset += 6;
                        if (destination.Length < offset) { return false; }
                        goto LM5;
                    }
                }
            }
            else
            {
                // 小数点以下出力は、4桁以下
                if (num4 > 0) // 3～4桁を評価
                {
                    // 小数点以下出力は、3or4桁
                    num4 -= (num3 = Div10(num4)) * 10; // 実行後、num3:3桁目の値 num4:4桁目の値
                    if (num4 > 0) // 4桁を評価
                    {
                        // 小数点以下出力は、4桁
                        offset2 = offset += 5;
                        if (destination.Length < offset) { return false; }
                        goto LM4;
                    }
                    else
                    {
                        // 小数点以下出力は、3桁
                        offset2 = offset += 4;
                        if (destination.Length < offset) { return false; }
                        goto LM3;
                    }
                }
                else
                {
                    // 小数点以下出力は、1or2桁
                    if (num2 > 0) // 2桁を評価
                    {
                        // 小数点以下出力は、2桁
                        offset2 = offset += 3;
                        if (destination.Length < offset) { return false; }
                        goto LM2;
                    }
                    else
                    {
                        // 小数点以下出力は、1桁
                        offset2 = offset += 2;
                        if (destination.Length < offset) { return false; }
                        goto LM1;
                    }
                }
            }

        LM8:
            destination[--offset2] = (byte)('0' + num8);
        LM7:
            destination[--offset2] = (byte)('0' + num7);
        LM6:
            destination[--offset2] = (byte)('0' + num6);
        LM5:
            destination[--offset2] = (byte)('0' + num5);
        LM4:
            destination[--offset2] = (byte)('0' + num4);
        LM3:
            destination[--offset2] = (byte)('0' + num3);
        LM2:
            destination[--offset2] = (byte)('0' + num2);
        LM1:
            destination[--offset2] = (byte)('0' + num1);
            destination[--offset2] = (byte)'.';
        }
        writtenLength = offset;
        return true;
    }

    [Benchmark]
    public (byte[], int) FixedPointReturnBuffer()
    {
        int offset = 0;
        if (TryWriteUtf8(_fixedPointValue, _buffer.AsSpan(offset), out var writtenLength) == false)
        {
            throw new Exception("Buffer不足");
        }
        offset += writtenLength;

        return (_buffer, offset);
    }

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method                 | Job        | Runtime  | Mean       | Error     | StdDev    | Ratio | RatioSD |
|----------------------- |----------- |--------- |-----------:|----------:|----------:|------:|--------:|
| DoubleToString         | Job-DDXAAB | .NET 8.0 | 107.245 ns | 2.0928 ns | 2.1492 ns |  1.00 |    0.03 |
| DoubleToString         | Job-WLOQWX | .NET 9.0 | 106.349 ns | 1.6986 ns | 1.5058 ns |  0.99 |    0.02 |
| DecimalToString        | Job-DDXAAB | .NET 8.0 |  51.025 ns | 1.0646 ns | 1.4212 ns |  1.00 |    0.04 |
| DecimalToString        | Job-WLOQWX | .NET 9.0 |  48.642 ns | 0.4995 ns | 0.4672 ns |  0.95 |    0.03 |
| FixedPointToByteArray  | Job-DDXAAB | .NET 8.0 |  15.967 ns | 0.3197 ns | 0.2834 ns |  1.00 |    0.02 |
| FixedPointToByteArray  | Job-WLOQWX | .NET 9.0 |  15.902 ns | 0.3661 ns | 0.3917 ns |  1.00 |    0.03 |
| FixedPointReturnBuffer | Job-DDXAAB | .NET 8.0 |   8.542 ns | 0.1726 ns | 0.2363 ns |  1.00 |    0.04 |
| FixedPointReturnBuffer | Job-WLOQWX | .NET 9.0 |   8.733 ns | 0.1392 ns | 0.1162 ns |  1.02 |    0.03 |
```

・[**UnixTimeBenchmark**](https://gitan.dev/?p=358)　　UnixTimeを作る方法のベンチマーク

    private static DateTime _unixTime_BaseTime = new DateTime(1970, 1, 1);

    [Benchmark]
    public long A_DateTime_Now_TimeSpan_TotalMilliseconds() =>
        (long)(DateTime.Now.ToUniversalTime().Subtract(_unixTime_BaseTime).TotalMilliseconds);

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method                                         | Job        | Runtime  | Mean     | Error    | StdDev   | Ratio |
|----------------------------------------------- |----------- |--------- |---------:|---------:|---------:|------:|
| A_DateTime_Now_TimeSpan_TotalMilliseconds      | Job-QHJRNO | .NET 8.0 | 46.32 ns | 0.271 ns | 0.241 ns |  1.00 |
| A_DateTime_Now_TimeSpan_TotalMilliseconds      | Job-POTQGX | .NET 9.0 | 49.01 ns | 0.610 ns | 0.541 ns |  1.06 |
| A_DateTime_Now_TimeSpan_TotalMilliseconds      | InProcess  | .NET 9.0 | 52.39 ns | 0.491 ns | 0.436 ns |  1.13 |
| B_DateTime_UtcNow_TimeSpan_TotalMilliseconds   | Job-QHJRNO | .NET 8.0 | 32.06 ns | 0.194 ns | 0.172 ns |  1.00 |
| B_DateTime_UtcNow_TimeSpan_TotalMilliseconds   | Job-POTQGX | .NET 9.0 | 35.38 ns | 0.301 ns | 0.282 ns |  1.10 |
| B_DateTime_UtcNow_TimeSpan_TotalMilliseconds   | InProcess  | .NET 9.0 | 36.49 ns | 0.195 ns | 0.173 ns |  1.14 |
| C_DateTimeOffset_UtcNow_ToUnixTimeMilliseconds | Job-QHJRNO | .NET 8.0 | 28.77 ns | 0.319 ns | 0.283 ns |  1.00 |
| C_DateTimeOffset_UtcNow_ToUnixTimeMilliseconds | Job-POTQGX | .NET 9.0 | 28.29 ns | 0.171 ns | 0.160 ns |  0.98 |
| C_DateTimeOffset_UtcNow_ToUnixTimeMilliseconds | InProcess  | .NET 9.0 | 31.31 ns | 0.157 ns | 0.139 ns |  1.09 |
| D_DateTime_UtcNow_SelfCalc                     | Job-QHJRNO | .NET 8.0 | 28.03 ns | 0.228 ns | 0.213 ns |  1.00 |
| D_DateTime_UtcNow_SelfCalc                     | Job-POTQGX | .NET 9.0 | 28.22 ns | 0.174 ns | 0.154 ns |  1.01 |
| D_DateTime_UtcNow_SelfCalc                     | InProcess  | .NET 9.0 | 29.63 ns | 0.101 ns | 0.094 ns |  1.06 |
```

・[**Utf8JsonBenchmark**](https://gitan.dev/?p=320)　　Utf8文字列の作り方とパフォーマンス


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

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method                           | Job        | Runtime  | Mean      | Error    | StdDev   | Ratio | RatioSD |
|--------------------------------- |----------- |--------- |----------:|---------:|---------:|------:|--------:|
| GetBytes_StringSomeAdd           | Job-ZUTAWM | .NET 8.0 | 212.34 ns | 1.512 ns | 1.263 ns |  1.00 |    0.01 |
| GetBytes_StringSomeAdd           | Job-XLZJOB | .NET 9.0 | 193.92 ns | 3.516 ns | 3.288 ns |  0.91 |    0.02 |
| GetBytes_StringSomeAdd           | InProcess  | .NET 9.0 | 248.77 ns | 3.170 ns | 2.647 ns |  1.17 |    0.01 |
| GetBytes_StringBuilder           | Job-ZUTAWM | .NET 8.0 | 156.38 ns | 3.040 ns | 2.844 ns |  1.00 |    0.02 |
| GetBytes_StringBuilder           | Job-XLZJOB | .NET 9.0 | 153.45 ns | 2.915 ns | 2.726 ns |  0.98 |    0.02 |
| GetBytes_StringBuilder           | InProcess  | .NET 9.0 | 175.66 ns | 3.558 ns | 3.328 ns |  1.12 |    0.03 |
| GetBytes_StringPlusToUtf8        | Job-ZUTAWM | .NET 8.0 | 174.11 ns | 2.562 ns | 2.271 ns |  1.00 |    0.02 |
| GetBytes_StringPlusToUtf8        | Job-XLZJOB | .NET 9.0 | 156.10 ns | 1.268 ns | 1.124 ns |  0.90 |    0.01 |
| GetBytes_StringPlusToUtf8        | InProcess  | .NET 9.0 | 194.36 ns | 3.882 ns | 3.632 ns |  1.12 |    0.02 |
| GetBytes_DollarStringToUtf8      | Job-ZUTAWM | .NET 8.0 | 154.62 ns | 1.054 ns | 0.934 ns |  1.00 |    0.01 |
| GetBytes_DollarStringToUtf8      | Job-XLZJOB | .NET 9.0 | 136.90 ns | 2.543 ns | 2.379 ns |  0.89 |    0.02 |
| GetBytes_DollarStringToUtf8      | InProcess  | .NET 9.0 | 162.12 ns | 1.571 ns | 1.312 ns |  1.05 |    0.01 |
| GetBytes_DollarStringToAscii     | Job-ZUTAWM | .NET 8.0 | 146.38 ns | 0.885 ns | 0.828 ns |  1.00 |    0.01 |
| GetBytes_DollarStringToAscii     | Job-XLZJOB | .NET 9.0 | 130.60 ns | 1.601 ns | 1.337 ns |  0.89 |    0.01 |
| GetBytes_DollarStringToAscii     | InProcess  | .NET 9.0 | 156.26 ns | 2.201 ns | 2.058 ns |  1.07 |    0.01 |
| GetSpan_CopyToTryFormat          | Job-ZUTAWM | .NET 8.0 |  80.76 ns | 0.422 ns | 0.352 ns |  1.00 |    0.01 |
| GetSpan_CopyToTryFormat          | Job-XLZJOB | .NET 9.0 |  84.16 ns | 0.556 ns | 0.493 ns |  1.04 |    0.01 |
| GetSpan_CopyToTryFormat          | InProcess  | .NET 9.0 |  92.64 ns | 0.779 ns | 0.728 ns |  1.15 |    0.01 |
| GetSpan_Utf8TryWriteDollarString | Job-ZUTAWM | .NET 8.0 |  87.52 ns | 0.433 ns | 0.338 ns |  1.00 |    0.01 |
| GetSpan_Utf8TryWriteDollarString | Job-XLZJOB | .NET 9.0 |  88.03 ns | 0.659 ns | 0.515 ns |  1.01 |    0.01 |
| GetSpan_Utf8TryWriteDollarString | InProcess  | .NET 9.0 | 102.79 ns | 1.327 ns | 1.241 ns |  1.17 |    0.01 |
| GetSpan_Utf8TryWriteDollarUtf8   | Job-ZUTAWM | .NET 8.0 |  84.79 ns | 0.741 ns | 0.693 ns |  1.00 |    0.01 |
| GetSpan_Utf8TryWriteDollarUtf8   | Job-XLZJOB | .NET 9.0 |  84.45 ns | 1.146 ns | 0.957 ns |  1.00 |    0.01 |
| GetSpan_Utf8TryWriteDollarUtf8   | InProcess  | .NET 9.0 |  96.31 ns | 1.387 ns | 1.297 ns |  1.14 |    0.02 |
```

・[**VariousBenchmark**](https://gitan.dev/?p=109)　　C#のいろいろな、遅くなる要素のベンチマーク

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

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2454)
AMD Ryzen 9 5900HS with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.101
  [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2

| Method                                 | Job      | Runtime  | Mean       | Error      | StdDev     |
|--------------------------------------- |--------- |--------- |-----------:|-----------:|-----------:|
| IntBench                               | .NET 8.0 | .NET 8.0 |   2.596 ms |  0.0515 ms |  0.1028 ms |
| IntBench                               | .NET 9.0 | .NET 9.0 |   2.423 ms |  0.0391 ms |  0.0366 ms |
| DoubleBench                            | .NET 8.0 | .NET 8.0 |   7.045 ms |  0.0982 ms |  0.0870 ms |
| DoubleBench                            | .NET 9.0 | .NET 9.0 |   6.948 ms |  0.0247 ms |  0.0207 ms |
| IntNoInlineBench                       | .NET 8.0 | .NET 8.0 |  14.671 ms |  0.2921 ms |  0.3364 ms |
| IntNoInlineBench                       | .NET 9.0 | .NET 9.0 |  12.072 ms |  0.2407 ms |  0.2866 ms |
| IntStructBench                         | .NET 8.0 | .NET 8.0 |   2.503 ms |  0.0457 ms |  0.0880 ms |
| IntStructBench                         | .NET 9.0 | .NET 9.0 |   2.429 ms |  0.0424 ms |  0.0435 ms |
| IntClassBench                          | .NET 8.0 | .NET 8.0 |  89.588 ms |  1.6472 ms |  1.4602 ms |
| IntClassBench                          | .NET 9.0 | .NET 9.0 |  52.030 ms |  1.0328 ms |  1.1051 ms |
| IntBoxingBench                         | .NET 8.0 | .NET 8.0 |  90.626 ms |  1.7631 ms |  2.9457 ms |
| IntBoxingBench                         | .NET 9.0 | .NET 9.0 |  52.065 ms |  0.9568 ms |  0.8950 ms |
| IntSubClassVirtualBench                | .NET 8.0 | .NET 8.0 |  89.364 ms |  1.7565 ms |  2.4624 ms |
| IntSubClassVirtualBench                | .NET 9.0 | .NET 9.0 |  51.825 ms |  0.8904 ms |  1.0935 ms |
| IntSubClassVirtualBench2               | .NET 8.0 | .NET 8.0 |   6.126 ms |  0.1184 ms |  0.1316 ms |
| IntSubClassVirtualBench2               | .NET 9.0 | .NET 9.0 |   5.402 ms |  0.1023 ms |  0.1005 ms |
| IntSubClassSealedVirtualBench          | .NET 8.0 | .NET 8.0 |  89.590 ms |  1.7893 ms |  1.7574 ms |
| IntSubClassSealedVirtualBench          | .NET 9.0 | .NET 9.0 |  52.483 ms |  1.0029 ms |  1.0731 ms |
| IntInterfaceVirtualBench               | .NET 8.0 | .NET 8.0 |  88.633 ms |  1.7638 ms |  1.7323 ms |
| IntInterfaceVirtualBench               | .NET 9.0 | .NET 9.0 |  51.850 ms |  0.5811 ms |  0.5436 ms |
| IntObjectLockBench                     | .NET 8.0 | .NET 8.0 |  55.637 ms |  0.9580 ms |  1.5195 ms |
| IntLockClassBench                      | .NET 8.0 | .NET 8.0 |         NA |         NA |         NA | 
| IntLockClassBench                      | .NET 9.0 | .NET 9.0 |  45.437 ms |  0.9031 ms |  1.9823 ms |
| IntObjectLockBench                     | .NET 9.0 | .NET 9.0 |  60.174 ms |  1.2005 ms |  2.2548 ms |
| AsyncAwaitIntBench                     | .NET 8.0 | .NET 8.0 | 108.412 ms |  2.1606 ms |  3.2994 ms |
| AsyncAwaitIntBench                     | .NET 9.0 | .NET 9.0 |  68.081 ms |  1.3307 ms |  2.1864 ms |
| AwaitFromResultIntBench                | .NET 8.0 | .NET 8.0 |  17.419 ms |  0.3019 ms |  0.3230 ms |
| AwaitFromResultIntBench                | .NET 9.0 | .NET 9.0 |  17.001 ms |  0.3197 ms |  0.3421 ms |
| AsyncAwaitValueTaskIntBench            | .NET 8.0 | .NET 8.0 |  70.075 ms |  1.3937 ms |  3.0299 ms |
| AsyncAwaitValueTaskIntBench            | .NET 9.0 | .NET 9.0 |  66.920 ms |  1.3256 ms |  2.1781 ms |
| AwaitFromResultValueTaskIntBench       | .NET 8.0 | .NET 8.0 |  17.619 ms |  0.3348 ms |  0.3288 ms |
| AwaitFromResultValueTaskIntBench       | .NET 9.0 | .NET 9.0 |  16.871 ms |  0.3001 ms |  0.2807 ms |
| MakeUtf8Abcde_StringBench              | .NET 8.0 | .NET 8.0 | 533.238 ms | 10.3895 ms | 17.0703 ms |
| MakeUtf8Abcde_StringBench              | .NET 9.0 | .NET 9.0 | 378.176 ms |  5.6952 ms |  5.0487 ms |
| MakeUtf8Abcde_StaticStringBench        | .NET 8.0 | .NET 8.0 | 556.038 ms | 10.8551 ms | 12.5007 ms |
| MakeUtf8Abcde_StaticStringBench        | .NET 9.0 | .NET 9.0 | 489.077 ms |  9.4713 ms | 11.6316 ms |
| MakeUtf8Abcde_StaticByteArrayBench     | .NET 8.0 | .NET 8.0 | 299.827 ms |  5.9924 ms | 13.4029 ms |
| MakeUtf8Abcde_StaticByteArrayBench     | .NET 9.0 | .NET 9.0 | 252.715 ms |  3.5583 ms |  3.3284 ms |
| MakeUtf8Abcde_StaticByteArraySpanBench | .NET 8.0 | .NET 8.0 | 161.314 ms |  3.0117 ms |  5.1950 ms |
| MakeUtf8Abcde_StaticByteArraySpanBench | .NET 9.0 | .NET 9.0 | 160.378 ms |  3.1420 ms |  5.4197 ms |
```

※IntLockClassBenchは.net9.0から実装なので.net8.0は計測できない
