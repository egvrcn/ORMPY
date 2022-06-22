``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                   : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Ekleme Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Ekleme Karsilastirmalari  Runtime=.NET 6.0  InvocationCount=1  
IterationCount=100  RunStrategy=ColdStart  UnrollFactor=1  

```
|           Method | count |    Mean |    Error |   StdDev |   Median | Rank |      Gen 0 |     Gen 1 | Allocated |
|----------------- |------ |--------:|---------:|---------:|---------:|-----:|-----------:|----------:|----------:|
|     EFCoreInsert | 10000 | 1.012 s | 0.0356 s | 0.1050 s | 0.9987 s |    1 | 24000.0000 | 8000.0000 |    146 MB |
| NHibernateInsert | 10000 | 1.642 s | 0.0101 s | 0.0297 s | 1.6358 s |    2 | 21000.0000 | 5000.0000 |     94 MB |
|     DapperInsert | 10000 | 2.023 s | 0.0144 s | 0.0424 s | 2.0101 s |    3 |  6000.0000 |         - |     20 MB |
