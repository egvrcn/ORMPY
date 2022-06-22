``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                 : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Join Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Join Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|         Method |    Mean |    Error |   StdDev | Rank |      Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|--------------- |--------:|---------:|---------:|-----:|-----------:|----------:|----------:|----------:|
|     DapperJoin | 1.594 s | 0.0368 s | 0.1086 s |    1 |  9000.0000 | 4000.0000 | 1000.0000 |     50 MB |
|     EFCoreJoin | 1.610 s | 0.0390 s | 0.1149 s |    1 |  7000.0000 | 2000.0000 |         - |     46 MB |
| NHibernateJoin | 1.945 s | 0.0346 s | 0.1019 s |    2 | 21000.0000 | 9000.0000 | 2000.0000 |    137 MB |
