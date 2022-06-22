``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
Intel Core i5-7300HQ CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=6.0.101
  [Host]                   : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT  [AttachedDebugger]
  Ekleme Karsilastirmalari : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT

Job=Ekleme Karsilastirmalari  Runtime=.NET 6.0  InvocationCount=1  
IterationCount=100  RunStrategy=ColdStart  UnrollFactor=1  

```
|           Method | count |    Mean |    Error |   StdDev | Rank |      Gen 0 |      Gen 1 | Allocated |
|----------------- |------ |--------:|---------:|---------:|-----:|-----------:|-----------:|----------:|
|     EFCoreInsert | 25000 | 2.483 s | 0.0425 s | 0.1253 s |    1 | 63000.0000 | 18000.0000 |    362 MB |
| NHibernateInsert | 25000 | 4.283 s | 0.0240 s | 0.0708 s |    2 | 55000.0000 | 12000.0000 |    236 MB |
|     DapperInsert | 25000 | 5.249 s | 0.1641 s | 0.4838 s |    3 | 16000.0000 |          - |     49 MB |
