``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Siralama Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Siralama Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|          Method |     Mean |    Error |    StdDev | Rank |      Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|---------------- |---------:|---------:|----------:|-----:|-----------:|----------:|----------:|----------:|
|     DapperOrder | 401.3 ms |  6.62 ms |  19.51 ms |    1 |  3000.0000 | 1000.0000 |         - |     19 MB |
|     EFCoreOrder | 539.2 ms | 41.85 ms | 123.41 ms |    2 | 10000.0000 | 3000.0000 | 1000.0000 |     60 MB |
| NHibernateOrder | 638.0 ms | 16.65 ms |  49.10 ms |    3 | 10000.0000 | 4000.0000 | 1000.0000 |     64 MB |
