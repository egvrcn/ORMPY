``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                  : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Arama Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Arama Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|           Method |     Mean |     Error |   StdDev |   Median | Rank | Allocated |
|----------------- |---------:|----------:|---------:|---------:|-----:|----------:|
|     DapperSearch | 16.38 ms |  3.437 ms | 10.14 ms | 15.18 ms |    1 |    307 KB |
| NHibernateSearch | 19.16 ms | 10.041 ms | 29.61 ms | 16.01 ms |    2 |    979 KB |
|     EFCoreSearch | 31.60 ms | 32.951 ms | 97.16 ms | 21.12 ms |    3 |    952 KB |
