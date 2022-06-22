``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                  : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Silme Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Silme Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|           Method | count |        Mean |    Error |    StdDev |       Median | Rank |      Gen 0 |     Gen 1 |     Gen 2 |  Allocated |
|----------------- |------ |------------:|---------:|----------:|-------------:|-----:|-----------:|----------:|----------:|-----------:|
|     DapperDelete | 25000 |    112.0 ms | 13.22 ms |  38.97 ms |     91.55 ms |    1 |          - |         - |         - |       3 KB |
|     EFCoreDelete | 25000 |  1,820.1 ms | 45.98 ms | 135.58 ms |  1,779.80 ms |    2 | 30000.0000 | 9000.0000 | 2000.0000 | 150,814 KB |
| NHibernateDelete | 25000 | 11,566.5 ms | 64.02 ms | 188.75 ms | 11,533.71 ms |    3 |  8000.0000 | 4000.0000 |         - |  47,588 KB |
