``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                       : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Guncelleme Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Guncelleme Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|           Method | count |    Mean |    Error |   StdDev |  Median | Rank |      Gen 0 |     Gen 1 | Allocated |
|----------------- |------ |--------:|---------:|---------:|--------:|-----:|-----------:|----------:|----------:|
|     EFCoreUpdate | 10000 | 1.310 s | 0.0426 s | 0.1257 s | 1.275 s |    1 | 19000.0000 | 6000.0000 |    127 MB |
| NHibernateUpdate | 10000 | 1.726 s | 0.0114 s | 0.0336 s | 1.719 s |    2 |  5000.0000 | 2000.0000 |     25 MB |
|     DapperUpdate | 10000 | 2.398 s | 0.0158 s | 0.0465 s | 2.386 s |    3 |  7000.0000 |         - |     22 MB |
