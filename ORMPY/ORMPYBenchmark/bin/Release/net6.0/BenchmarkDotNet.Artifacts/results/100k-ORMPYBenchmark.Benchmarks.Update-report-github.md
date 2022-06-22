``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                       : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Guncelleme Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Guncelleme Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|           Method | count |    Mean |    Error |   StdDev |  Median | Rank |      Gen 0 |      Gen 1 | Allocated |
|----------------- |------ |--------:|---------:|---------:|--------:|-----:|-----------:|-----------:|----------:|
|     EFCoreUpdate | 25000 | 3.130 s | 0.0756 s | 0.2230 s | 3.089 s |    1 | 50000.0000 | 16000.0000 |    313 MB |
| NHibernateUpdate | 25000 | 4.514 s | 0.1189 s | 0.3505 s | 4.381 s |    2 | 13000.0000 |  3000.0000 |     64 MB |
|     DapperUpdate | 25000 | 6.186 s | 0.0315 s | 0.0928 s | 6.162 s |    3 | 18000.0000 |          - |     54 MB |
