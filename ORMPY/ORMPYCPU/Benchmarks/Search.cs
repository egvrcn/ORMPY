﻿using Dapper;
using DataAccessLayer;
using DataAccessLayer.Utility;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using NHibernate;
using Npgsql;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using static EntityLayer.Enums.Enums;

namespace ORMPYCPU.Benchmarks
{
    public class Search
    {
        DbContextOptionsBuilder<ChinookDbContext> _optionsBuilder;
        string connectionString;
        //NHibernate
        private readonly ISessionFactory _sessionFactory;

        public Search()
        {
            connectionString = Utility.GetConnectionString();

            //EFCore
            _optionsBuilder = new DbContextOptionsBuilder<ChinookDbContext>().UseNpgsql(connectionString);

            //NHibernate
            NHibernateConnection nHibernateConnection = new NHibernateConnection();
            _sessionFactory = nHibernateConnection.CreateSessionFactory(connectionString);
        }

        public ResultCPU CPUUsage(EOrmType eOrmType, int count)
        {
            ResultCPU resultCPU = new ResultCPU()
            {
                Adi = eOrmType.ToString() + "Search",
                KayitSayisi = count
            };

            // Mevcut süreç bilgisi alınır
            var process = Process.GetCurrentProcess();

            var name = string.Empty;

            foreach (var instance in new PerformanceCounterCategory("Process").GetInstanceNames())
            {
                if (instance.StartsWith(process.ProcessName))
                {
                    using (var processId = new PerformanceCounter("Process", "ID Process", instance, true))
                    {
                        if (process.Id == (int)processId.RawValue)
                        {
                            //Çalıştırmış olduğumuz uygulamaya ait instance yakalanır.
                            name = instance;
                            break;
                        }
                    }
                }
            }

            //Yukarıda bulmuş olduğumuz instance değerimize ait cpu kullanım bilgisi alınır.
            var cpu = new PerformanceCounter("Process", "% Processor Time", name, true);
            cpu.NextValue();

            //Ölçüm yapılacak işlem başlatılır
            switch (eOrmType)
            {
                case EOrmType.EFCore:
                    // Sonraki sorgu sırasında CPU kullanımının doğru değerlerini almak için uygulama bekletilir.
                    Thread.Sleep(500);
                    EFCoreSearch();
                    break;
                case EOrmType.Dapper:
                    Thread.Sleep(500);
                    DapperSearch();
                    break;
                case EOrmType.NHibernate:
                    Thread.Sleep(500);
                    NHibernateSearch();
                    break;
                default:
                    break;
            }

            // Sistemin birden fazla çekirdeği varsa, bu dikkate alınmalıdır.
            resultCPU.CPUUsage = Math.Round(cpu.NextValue() / Environment.ProcessorCount, 2);

            return resultCPU;
        }

        public void EFCoreSearch()
        {
            using (ChinookDbContext _context = new ChinookDbContext(_optionsBuilder.Options))
            {
                var list = _context.Track.Where(x => x.name.Contains("Song")).ToList();
            }
        }

        public void DapperSearch()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var list = connection.Query<Track>("select * from track where name like '%Song%'").ToList();
            }
        }

        public void NHibernateSearch()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var list = session.CreateQuery("from Track where name like '%Song%'").List<Track>();
            }
        }
    }
}
