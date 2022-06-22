``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                 : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Join Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Join Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|         Method |    Mean |    Error |   StdDev |  Median | Rank |     Gen 0 |     Gen 1 | Allocated |
|--------------- |--------:|---------:|---------:|--------:|-----:|----------:|----------:|----------:|
|     DapperJoin | 1.449 s | 0.0109 s | 0.0321 s | 1.438 s |    1 |         - |         - |      5 MB |
|     EFCoreJoin | 1.456 s | 0.0325 s | 0.0959 s | 1.437 s |    1 |         - |         - |      5 MB |
| NHibernateJoin | 1.511 s | 0.0165 s | 0.0487 s | 1.501 s |    2 | 2000.0000 | 1000.0000 |     14 MB |
