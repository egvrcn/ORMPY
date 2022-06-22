``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                  : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Okuma Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Okuma Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|         Method |     Mean |    Error |    StdDev | Rank |      Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|--------------- |---------:|---------:|----------:|-----:|-----------:|----------:|----------:|----------:|
|     DapperRead | 245.1 ms |  6.88 ms |  20.28 ms |    1 |  6000.0000 | 2000.0000 |         - |     37 MB |
|     EFCoreRead | 465.3 ms | 35.24 ms | 103.91 ms |    2 | 18000.0000 | 6000.0000 | 1000.0000 |    121 MB |
| NHibernateRead | 802.8 ms | 16.34 ms |  48.18 ms |    3 | 19000.0000 | 8000.0000 | 2000.0000 |    128 MB |
