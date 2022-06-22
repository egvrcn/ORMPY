``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                 : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Join Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Join Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|         Method |    Mean |    Error |   StdDev |  Median | Rank |      Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|--------------- |--------:|---------:|---------:|--------:|-----:|-----------:|----------:|----------:|----------:|
|     DapperJoin | 1.478 s | 0.0242 s | 0.0714 s | 1.439 s |    1 |  4000.0000 | 1000.0000 |         - |     25 MB |
|     EFCoreJoin | 1.535 s | 0.0509 s | 0.1502 s | 1.517 s |    1 |  3000.0000 | 1000.0000 |         - |     23 MB |
| NHibernateJoin | 1.618 s | 0.0364 s | 0.1074 s | 1.604 s |    2 | 10000.0000 | 4000.0000 | 1000.0000 |     68 MB |
