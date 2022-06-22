``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                  : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Arama Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Arama Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|           Method |      Mean |     Error |    StdDev |    Median | Rank | Allocated |
|----------------- |----------:|----------:|----------:|----------:|-----:|----------:|
|     DapperSearch |  8.590 ms |  3.934 ms | 11.599 ms |  7.287 ms |    1 |    154 KB |
| NHibernateSearch | 11.243 ms | 10.450 ms | 30.811 ms |  8.162 ms |    2 |    493 KB |
|     EFCoreSearch | 20.868 ms | 32.653 ms | 96.279 ms | 10.652 ms |    3 |    492 KB |
