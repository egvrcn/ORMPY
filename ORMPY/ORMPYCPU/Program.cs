using EntityLayer.Entities;
using ORMPYCPU.Benchmarks;
using System;
using System.Collections.Generic;
using System.Threading;
using static EntityLayer.Enums.Enums;
using DataAccessLayer.Utility;
using System.Reflection;
using System.Linq;

namespace ORMPYCPU
{
    class Program
    {
        static void Main(string[] args)
        {
            SelectORM();
        }

        /// <summary>
        /// Read işlemi için CPU Test İşlemi
        /// </summary>
        /// <param name="eOrmType">ORM tipi</param>
        /// <param name="count">Kayıt sayısı</param>
        /// <param name="cycleCount">Tekrar sayısı</param>
        /// <returns></returns>
        public static List<ResultCPU> ReadCPUCycle(EOrmType eOrmType, int count, int cycleCount)
        {
            List<ResultCPU> listResultCPU = new List<ResultCPU>();
            Read read = new Read();
            double totalCPUUsage = 0;
            for (int i = 0; i < cycleCount; i++)
            {
                ResultCPU result = read.CPUUsage(eOrmType, count);
                totalCPUUsage += result.CPUUsage;
                listResultCPU.Add(result);
                IslemYaz(i, "Okuma", result.CPUUsage);
                Thread.Sleep(500);
            }

            //Ortalama Eklenir
            listResultCPU.Add(new ResultCPU()
            {
                Adi = "Ortalama Değer",
                CPUUsage = Math.Round(totalCPUUsage / cycleCount, 2),
                KayitSayisi = count
            });

            return listResultCPU;
        }

        /// <summary>
        /// Insert işlemi için CPU Test İşlemi
        /// </summary>
        /// <param name="eOrmType">ORM tipi</param>
        /// <param name="count">Kayıt sayısı</param>
        /// <param name="cycleCount">Tekrar sayısı</param>
        /// <returns></returns>
        public static List<ResultCPU> InsertCPUCycle(EOrmType eOrmType, int count, int cycleCount)
        {
            List<ResultCPU> listResultCPU = new List<ResultCPU>();

            double totalCPUUsage = 0;
            for (int i = 0; i < cycleCount; i++)
            {
                Insert insert = new Insert();
                ResultCPU result = insert.CPUUsage(eOrmType, count);
                totalCPUUsage += result.CPUUsage;
                listResultCPU.Add(result);
                IslemYaz(i, "Ekleme", result.CPUUsage);
                Thread.Sleep(500);
            }

            //Ortalama Eklenir
            listResultCPU.Add(new ResultCPU()
            {
                Adi = "Ortalama Değer",
                CPUUsage = Math.Round(totalCPUUsage / cycleCount, 2),
                KayitSayisi = count
            });

            return listResultCPU;
        }

        /// <summary>
        /// Update işlemi için CPU Test İşlemi
        /// </summary>
        /// <param name="eOrmType">ORM tipi</param>
        /// <param name="count">Kayıt sayısı</param>
        /// <param name="cycleCount">Tekrar sayısı</param>
        /// <returns></returns>
        public static List<ResultCPU> UpdateCPUCycle(EOrmType eOrmType, int count, int cycleCount)
        {
            List<ResultCPU> listResultCPU = new List<ResultCPU>();

            double totalCPUUsage = 0;
            for (int i = 0; i < cycleCount; i++)
            {
                Update update = new Update();
                ResultCPU result = update.CPUUsage(eOrmType, count);
                totalCPUUsage += result.CPUUsage;
                listResultCPU.Add(result);
                IslemYaz(i, "Guncelleme", result.CPUUsage);
                Thread.Sleep(500);
            }

            //Ortalama Eklenir
            listResultCPU.Add(new ResultCPU()
            {
                Adi = "Ortalama Değer",
                CPUUsage = Math.Round(totalCPUUsage / cycleCount, 2),
                KayitSayisi = count
            });

            return listResultCPU;
        }

        /// <summary>
        /// Sıralama işlemi için CPU Test İşlemi
        /// </summary>
        /// <param name="eOrmType">ORM tipi</param>
        /// <param name="count">Kayıt sayısı</param>
        /// <param name="cycleCount">Tekrar sayısı</param>
        /// <returns></returns>
        public static List<ResultCPU> OrderCPUCycle(EOrmType eOrmType, int count, int cycleCount)
        {
            List<ResultCPU> listResultCPU = new List<ResultCPU>();
            Order order = new Order();
            double totalCPUUsage = 0;
            for (int i = 0; i < cycleCount; i++)
            {
                ResultCPU result = order.CPUUsage(eOrmType, count);
                totalCPUUsage += result.CPUUsage;
                listResultCPU.Add(result);
                IslemYaz(i, "Siralama", result.CPUUsage);
                Thread.Sleep(500);
            }

            //Ortalama Eklenir
            listResultCPU.Add(new ResultCPU()
            {
                Adi = "Ortalama Değer",
                CPUUsage = Math.Round(totalCPUUsage / cycleCount, 2),
                KayitSayisi = count
            });

            return listResultCPU;
        }

        /// <summary>
        /// Arama işlemi için CPU Test İşlemi
        /// </summary>
        /// <param name="eOrmType">ORM tipi</param>
        /// <param name="count">Kayıt sayısı</param>
        /// <param name="cycleCount">Tekrar sayısı</param>
        /// <returns></returns>
        public static List<ResultCPU> SearchCPUCycle(EOrmType eOrmType, int count, int cycleCount)
        {
            List<ResultCPU> listResultCPU = new List<ResultCPU>();
            Search search = new Search();
            double totalCPUUsage = 0;
            for (int i = 0; i < cycleCount; i++)
            {
                ResultCPU result = search.CPUUsage(eOrmType, count);
                totalCPUUsage += result.CPUUsage;
                listResultCPU.Add(result);
                IslemYaz(i, "Arama", result.CPUUsage);
                Thread.Sleep(500);
            }

            //Ortalama Eklenir
            listResultCPU.Add(new ResultCPU()
            {
                Adi = "Ortalama Değer",
                CPUUsage = Math.Round(totalCPUUsage / cycleCount, 2),
                KayitSayisi = count
            });

            return listResultCPU;
        }

        /// <summary>
        /// Silme işlemi için CPU Test İşlemi
        /// </summary>
        /// <param name="eOrmType">ORM tipi</param>
        /// <param name="count">Kayıt sayısı</param>
        /// <param name="cycleCount">Tekrar sayısı</param>
        /// <returns></returns>
        public static List<ResultCPU> DeleteCPUCycle(EOrmType eOrmType, int count, int cycleCount)
        {
            List<ResultCPU> listResultCPU = new List<ResultCPU>();
            Delete delete = new Delete();
            double totalCPUUsage = 0;
            for (int i = 0; i < cycleCount; i++)
            {
                ResultCPU result = delete.CPUUsage(eOrmType, count);
                totalCPUUsage += result.CPUUsage;
                listResultCPU.Add(result);
                IslemYaz(i, "Silme", result.CPUUsage);
                Thread.Sleep(500);
            }

            //Ortalama Eklenir
            listResultCPU.Add(new ResultCPU()
            {
                Adi = "Ortalama Değer",
                CPUUsage = Math.Round(totalCPUUsage / cycleCount, 2),
                KayitSayisi = count
            });

            return listResultCPU;
        }

        /// <summary>
        /// Join işlemi için CPU Test İşlemi
        /// </summary>
        /// <param name="eOrmType">ORM tipi</param>
        /// <param name="count">Kayıt sayısı</param>
        /// <param name="cycleCount">Tekrar sayısı</param>
        /// <returns></returns>
        public static List<ResultCPU> JoinCPUCycle(EOrmType eOrmType, int count, int cycleCount)
        {
            List<ResultCPU> listResultCPU = new List<ResultCPU>();
            Join join = new Join();
            double totalCPUUsage = 0;
            for (int i = 0; i < cycleCount; i++)
            {
                ResultCPU result = join.CPUUsage(eOrmType, count);
                totalCPUUsage += result.CPUUsage;
                listResultCPU.Add(result);
                IslemYaz(i, "Join", result.CPUUsage);
                Thread.Sleep(500);
            }

            //Ortalama Eklenir
            listResultCPU.Add(new ResultCPU()
            {
                Adi = "Ortalama Değer",
                CPUUsage = Math.Round(totalCPUUsage / cycleCount, 2),
                KayitSayisi = count
            });

            return listResultCPU;
        }

        /// <summary>
        /// Reflection Yöntemi ile CPU Test işlemi
        /// </summary>
        /// <typeparam name="T">İlgili sınıf</typeparam>
        /// <param name="eOrmType">ORM tipi</param>
        /// <param name="count">Kayıt sayısı</param>
        /// <param name="cycleCount">Tekrar sayısı</param>
        /// <returns></returns>
        public static List<ResultCPU> CPUTest<T>(EOrmType eOrmType, int count, int cycleCount) where T : class, new()
        {
            List<ResultCPU> listResultCPU = new List<ResultCPU>();

            double totalCPUUsage = 0;
            for (int i = 0; i < cycleCount; i++)
            {
                //Reflection ile class instance'ı ve metoduna erişilir.
                T CPUClass = Activator.CreateInstance<T>();
                MethodInfo CPUMethod = typeof(T).GetMethod("CPUUsage");
                ResultCPU result = (ResultCPU)CPUMethod.Invoke(CPUClass, new object[] { eOrmType, count });

                totalCPUUsage += result.CPUUsage;
                listResultCPU.Add(result);
                Thread.Sleep(500);
            }

            //Ortalama Değer Eklenir
            listResultCPU.Add(new ResultCPU()
            {
                Adi = "Ortalama Değer",
                CPUUsage = Math.Round(totalCPUUsage / cycleCount, 2),
                KayitSayisi = count
            });

            return listResultCPU;
        }

        public static void IslemYaz(int sira, string islemAdi, double cpuUsage)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"> {sira + 1}. {islemAdi} işlemi gerçekleştirildi.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"CPU Kullanım: %{cpuUsage}");
            Console.ResetColor();
        }

        #region UI

        public static List<Option> options;
        public static EOrmType selectedORM;
        public static EProcessType selectedProcessType;

        public static void SelectORM()
        {
            options = new List<Option>
            {
                new Option("Dapper", () => SelectTestType(EOrmType.Dapper)),
                new Option("Entity Framework Core", () =>  SelectTestType(EOrmType.EFCore)),
                new Option("NHibernate", () =>  SelectTestType(EOrmType.NHibernate)),
                new Option("Çıkış", () => Environment.Exit(0)),
            };
            ConsoleSelect();
        }

        public static void SelectTestType(EOrmType eOrmType)
        {
            Console.Clear();
            selectedORM = eOrmType;
            // Create options that you want your menu to have
            options = new List<Option>
            {
                new Option("Okuma", () => SelectCount(EProcessType.Read)),
                new Option("Ekleme", () =>  SelectCount(EProcessType.Insert)),
                new Option("Güncelleme", () =>  SelectCount(EProcessType.Update)),
                new Option("Silme", () => SelectCount(EProcessType.Delete)),
                new Option("Sıralama", () =>  SelectCount(EProcessType.Order)),
                new Option("Arama", () =>  SelectCount(EProcessType.Search)),
                new Option("Join", () =>  SelectCount(EProcessType.Join)),
                new Option("Çıkış", () => Environment.Exit(0)),
            };

            ConsoleSelect();
        }

        static void SelectCount(EProcessType eProcessType)
        {
            Console.Clear();
            selectedProcessType = eProcessType;
            if (eProcessType == EProcessType.Insert ||
                eProcessType == EProcessType.Update ||
                eProcessType == EProcessType.Delete)
            {
                options = new List<Option>
                {
                    new Option("1.000", () => StartTestCPU(1000)),
                    new Option("10.000", () =>  StartTestCPU(10000)),
                    new Option("25.000", () =>  StartTestCPU(25000)),
                    new Option("Çıkış", () => Environment.Exit(0)),
                };
            }
            else
            {
                StartTestCPU(0);
            }
            ConsoleSelect();
        }

        public static void StartTestCPU(int count)
        {
            Console.Clear();
            if (count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"*{selectedProcessType} işlemleri, işlem doğruluğu için track tablosundaki kayıt sayısı kadar gerçekleştirilmektedir.");
                Console.WriteLine("Tabloda bulunan kayıt sayısı test için istenen sayıya göre ayarlanmalıdır.");
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            string countComment = count == 0 ? "veritabanındaki kayıt sayısına göre" : count +
            " kayıt sayısı ile";
            Console.WriteLine($"{selectedORM} {selectedProcessType} işlemi {countComment} gerçekleştiriliyor...");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Sonucun doğruluğunu arttırmak için işlem 500 defa tekrarlanacaktır.");
            Console.ResetColor();

            List<ResultCPU> listResultCPU = new List<ResultCPU>();
            switch (selectedProcessType)
            {
                case EProcessType.Read:
                    listResultCPU = ReadCPUCycle(selectedORM, count, 500);
                    break;
                case EProcessType.Insert:
                    listResultCPU = InsertCPUCycle(selectedORM, count, 500);
                    break;
                case EProcessType.Update:
                    listResultCPU = UpdateCPUCycle(selectedORM, count, 500);
                    break;
                case EProcessType.Delete:
                    listResultCPU = DeleteCPUCycle(selectedORM, count, 500);
                    break;
                case EProcessType.Order:
                    listResultCPU = OrderCPUCycle(selectedORM, count, 500);
                    break;
                case EProcessType.Search:
                    listResultCPU = SearchCPUCycle(selectedORM, count, 500);
                    break;
                case EProcessType.Join:
                    listResultCPU = JoinCPUCycle(selectedORM, count, 500);
                    break;
                default:
                    break;
            }
            //Veriler ekrana yazılır  
            string strCount = count == 0 ? "" : count + "";
            Utility.ShowTable($"{selectedORM}-{selectedProcessType}{strCount}", listResultCPU);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Sonuçlar \"bin\\Release\\net6.0\" altında result-{selectedORM}-{selectedProcessType}{strCount}.txt adındaki dosyaya yazılmıştır.");
            Console.ResetColor();
            Console.Read();
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
