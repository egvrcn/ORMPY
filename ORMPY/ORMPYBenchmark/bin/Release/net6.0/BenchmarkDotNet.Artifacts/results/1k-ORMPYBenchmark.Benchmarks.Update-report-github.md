``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                       : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Guncelleme Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Guncelleme Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|           Method | count |     Mean |    Error |   StdDev | Rank |     Gen 0 | Allocated |
|----------------- |------ |---------:|---------:|---------:|-----:|----------:|----------:|
|     EFCoreUpdate |  1000 | 126.4 ms | 28.00 ms | 82.57 ms |    1 | 1000.0000 |     14 MB |
| NHibernateUpdate |  1000 | 175.0 ms |  4.61 ms | 13.58 ms |    2 |         - |      3 MB |
|     DapperUpdate |  1000 | 243.9 ms |  4.03 ms | 11.87 ms |    3 |         - |      2 MB |
