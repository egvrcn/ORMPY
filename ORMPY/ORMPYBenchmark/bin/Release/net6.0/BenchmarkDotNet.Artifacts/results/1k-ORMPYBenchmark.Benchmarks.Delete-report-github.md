``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                  : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Silme Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Silme Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|           Method | count |      Mean |     Error |     StdDev | Rank |     Gen 0 | Allocated |
|----------------- |------ |----------:|----------:|-----------:|-----:|----------:|----------:|
|     DapperDelete |  1000 |  4.950 ms |  2.366 ms |   6.977 ms |    1 |         - |      3 KB |
| NHibernateDelete |  1000 | 30.072 ms | 13.956 ms |  41.149 ms |    2 |         - |  1,864 KB |
|     EFCoreDelete |  1000 | 91.116 ms | 39.358 ms | 116.048 ms |    3 | 1000.0000 |  6,533 KB |
