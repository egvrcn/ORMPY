``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                  : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Arama Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Arama Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|           Method |     Mean |    Error |    StdDev | Rank | Allocated |
|----------------- |---------:|---------:|----------:|-----:|----------:|
|     DapperSearch | 722.8 ms |  4.27 ms |  12.60 ms |    1 |    307 KB |
| NHibernateSearch | 726.3 ms | 10.63 ms |  31.34 ms |    1 |    980 KB |
|     EFCoreSearch | 750.7 ms | 39.24 ms | 115.71 ms |    1 |    952 KB |
