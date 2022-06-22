``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Siralama Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Siralama Karsilastirmalari  Runtime=.NET 6.0  IterationCount=100  
RunStrategy=ColdStart  

```
|          Method |     Mean |    Error |   StdDev | Rank |     Gen 0 | Allocated |
|---------------- |---------:|---------:|---------:|-----:|----------:|----------:|
|     DapperOrder | 114.9 ms |  5.01 ms | 14.77 ms |    1 | 1000.0000 |      4 MB |
|     EFCoreOrder | 144.3 ms | 33.41 ms | 98.50 ms |    2 | 1000.0000 |     12 MB |
| NHibernateOrder | 146.2 ms | 12.32 ms | 36.33 ms |    3 | 1000.0000 |     12 MB |
