using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using Dapper;
using DataAccessLayer.Utility;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using ORMPYBenchmark.Benchmarks;
using System;
using System.Collections.Generic;
using static EntityLayer.Enums.Enums;

namespace ORMPYBenchmark
{
    class Program
    {
        static void Main()
        {
            SelectTestType();
        }

        #region UI

        public static List<Option> options;

        public static void SelectTestType()
        {
            Console.Clear();
            Console.ResetColor();

            options = new List<Option>
            {
                new Option("Okuma", () => StartTest(EProcessType.Read)),
                new Option("Ekleme", () =>  StartTest(EProcessType.Insert)),
                new Option("Güncelleme", () =>  StartTest(EProcessType.Update)),
                new Option("Silme", () => StartTest(EProcessType.Delete)),
                new Option("Sıralama", () =>  StartTest(EProcessType.Order)),
                new Option("Arama", () =>  StartTest(EProcessType.Search)),
                new Option("Join", () =>  StartTest(EProcessType.Join)),
                new Option("Çıkış", () => Environment.Exit(0)),
            };

            ConsoleSelect();
        }

        public static void StartTest(EProcessType selectedProcessType)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (selectedProcessType == EProcessType.Read ||
                selectedProcessType == EProcessType.Order ||
                selectedProcessType == EProcessType.Search ||
                selectedProcessType == EProcessType.Join)
            {
                Console.WriteLine($"*{selectedProcessType} işlemleri, işlem doğruluğu için track tablosundaki kayıt sayısı kadar gerçekleştirilmektedir.");
                Console.WriteLine("Tabloda bulunan kayıt sayısı test için istenen sayıya göre ayarlanmalıdır.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"*{selectedProcessType} işlemleri {selectedProcessType}.cs içerisindeki metodların Arguments özniteliğindeki sayıya göre gerçekleştirilmektedir.");
                Console.WriteLine("[Arguments(\"istenilen kayıt sayısı\")] özniteliğinde test için istenen sayıya göre ayarlama yapılabilir. (Varsayılan: 1.000)");
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{selectedProcessType} işlemi gerçekleştiriliyor...");
            Console.ResetColor();

            switch (selectedProcessType)
            {
                case EProcessType.Read:
                    BenchmarkRunner.Run<Read>();
                    break;
                case EProcessType.Insert:
                    BenchmarkRunner.Run<Insert>();
                    break;
                case EProcessType.Update:
                    BenchmarkRunner.Run<Update>();
                    break;
                case EProcessType.Delete:
                    BenchmarkRunner.Run<Delete>();
                    break;
                case EProcessType.Order:
                    BenchmarkRunner.Run<Order>();
                    break;
                case EProcessType.Search:
                    BenchmarkRunner.Run<Search>();
                    break;
                case EProcessType.Join:
                    BenchmarkRunner.Run<Join>();
                    break;
                default:
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"İşlemler \"bin\\Release\\net6.0\\BenchmarkDotNet.Artifacts\" " +
                $"altında ORMPYBenchmark.Benchmarks.{selectedProcessType}-*.log adındaki dosyaya yazılmıştır.");
            Console.WriteLine($"Sonuçlar \"bin\\Release\\net6.0\\BenchmarkDotNet.Artifacts\\results\" " +
                $"altında ORMPYBenchmark.Benchmarks.{selectedProcessType}-report.html, csv ve md uzantılı dosyalardan görüntülenebilir. ");

            Console.ResetColor();
            Console.ReadLine();
        }

        public static void ConsoleSelect()
        {
            int index = 0;

            WriteMenu(options, options[index]);

            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();

                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        WriteMenu(options, options[index]);
                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        WriteMenu(options, options[index]);
                    }
                }

                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    options[index].Selected.Invoke();
                    index = 0;
                }
            }
            while (keyinfo.Key != ConsoleKey.X);

            Console.ReadKey();
        }

        static void WriteMenu(List<Option> options, Option selectedOption)
        {
            Console.Clear();

            foreach (Option option in options)
            {
                if (option == selectedOption)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(" ");
                }

                Console.WriteLine(option.Name);
            }
        }
        #endregion
    }
}
