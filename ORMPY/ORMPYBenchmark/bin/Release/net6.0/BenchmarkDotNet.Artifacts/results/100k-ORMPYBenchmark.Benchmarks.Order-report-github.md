``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Siralama Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Siralama Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|          Method |       Mean |    Error |    StdDev | Rank |      Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|---------------- |-----------:|---------:|----------:|-----:|-----------:|----------:|----------:|----------:|
|     DapperOrder |   884.4 ms |  6.70 ms |  19.77 ms |    1 |  5000.0000 | 2000.0000 |         - |     37 MB |
|     EFCoreOrder | 1,097.3 ms | 41.12 ms | 121.25 ms |    2 | 18000.0000 | 6000.0000 | 1000.0000 |    121 MB |
| NHibernateOrder | 1,406.7 ms | 18.82 ms |  55.49 ms |    3 | 19000.0000 | 8000.0000 | 2000.0000 |    128 MB |
