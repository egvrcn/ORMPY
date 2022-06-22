``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                  : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Okuma Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Okuma Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|         Method |     Mean |     Error |   StdDev | Rank |     Gen 0 | Allocated |
|--------------- |---------:|----------:|---------:|-----:|----------:|----------:|
|     DapperRead | 63.47 ms |  4.503 ms | 13.28 ms |    1 |         - |      4 MB |
|     EFCoreRead | 91.12 ms | 31.751 ms | 93.62 ms |    2 | 1000.0000 |     12 MB |
| NHibernateRead | 97.06 ms | 10.303 ms | 30.38 ms |    3 | 1000.0000 |     12 MB |
