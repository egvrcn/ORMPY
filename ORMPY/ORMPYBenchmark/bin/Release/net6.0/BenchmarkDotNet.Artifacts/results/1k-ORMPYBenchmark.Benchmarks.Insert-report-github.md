``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                   : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Ekleme Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Ekleme Karsilastirmalari  Runtime=.NET 6.0  InvocationCount=1  
IterationCount=100  RunStrategy=ColdStart  UnrollFactor=1  

```
|           Method | count |     Mean |    Error |   StdDev | Rank |     Gen 0 |     Gen 1 | Allocated |
|----------------- |------ |---------:|---------:|---------:|-----:|----------:|----------:|----------:|
|     EFCoreInsert |  1000 | 109.9 ms | 27.43 ms | 80.88 ms |    1 | 2000.0000 | 1000.0000 |     15 MB |
| NHibernateInsert |  1000 | 174.3 ms |  5.56 ms | 16.39 ms |    2 | 1000.0000 |         - |      9 MB |
|     DapperInsert |  1000 | 206.9 ms |  3.51 ms | 10.36 ms |    3 |         - |         - |      2 MB |
