``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                  : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Okuma Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Okuma Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|         Method |     Mean |    Error |   StdDev | Rank |      Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|--------------- |---------:|---------:|---------:|-----:|-----------:|----------:|----------:|----------:|
|     DapperRead | 142.5 ms |  7.80 ms | 23.00 ms |    1 |  2000.0000 | 1000.0000 |         - |     19 MB |
|     EFCoreRead | 265.0 ms | 33.23 ms | 97.99 ms |    2 | 10000.0000 | 4000.0000 | 1000.0000 |     60 MB |
| NHibernateRead | 400.2 ms | 14.57 ms | 42.96 ms |    3 | 10000.0000 | 4000.0000 | 1000.0000 |     64 MB |
