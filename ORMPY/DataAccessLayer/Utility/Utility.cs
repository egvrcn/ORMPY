using EntityLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Utility
{
    public static class Utility
    {
        public static string GetConnectionString()
        {
            IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile(
           "appsettings.json", optional: true, reloadOnChange: true).Build();

            return _configuration.GetConnectionString("Default");
        }

        public static void ShowTable(string fileName, List<ResultCPU> listResult)
        {
            string result = "CPU Usage - " + DateTime.Now + "\n";
            result += String.Format("|{0,-20}|{1,-20}|{2,-20}|", "Metot Adı", "Kayıt Sayısı", "CPU Kullanımı") + "\n";
            result += String.Format("|{0,-20}|{1,-20}|{2,-20}|", "---------------", "---------------", "---------------") + "\n";

            for (int i = 0; i < listResult.Count; i++)
            {
                if (i != listResult.Count - 1)
                {
                    var item = listResult[i];
                    result += String.Format("|{0,-20}|{1,-20}|{2,-20}|", item.Adi, item.KayitSayisi, item.CPUUsage);
                    result += "\n";
                }
                else
                {
                    var item = listResult[i];
                    result += "----------------------------------------------------------------";
                    result += "\n";
                    result += String.Format("|{0,-20}|{1,-20}|{2,-20}|", item.Adi, item.KayitSayisi, item.CPUUsage);
                    result += "\n";
                }
            }


            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "result-" + fileName + ".txt"), result);
            Console.WriteLine(Path.Combine(Environment.CurrentDirectory, "result-" + fileName + ".txt"));

            Console.WriteLine(result);
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
