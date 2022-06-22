``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                  : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Silme Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Silme Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|           Method | count |        Mean |     Error |     StdDev | Rank |      Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|----------------- |------ |------------:|----------:|-----------:|-----:|-----------:|----------:|----------:|----------:|
|     DapperDelete | 10000 |    39.77 ms |  2.676 ms |   7.889 ms |    1 |          - |         - |         - |      3 KB |
|     EFCoreDelete | 10000 |   788.94 ms | 47.453 ms | 139.916 ms |    2 | 10000.0000 | 4000.0000 | 1000.0000 | 61,397 KB |
| NHibernateDelete | 10000 | 1,840.51 ms | 41.682 ms | 122.901 ms |    3 |  2000.0000 | 1000.0000 |         - | 19,147 KB |
