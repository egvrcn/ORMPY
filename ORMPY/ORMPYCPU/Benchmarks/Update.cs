using Dapper;
using DataAccessLayer;
using DataAccessLayer.Utility;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using NHibernate;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using static EntityLayer.Enums.Enums;

namespace ORMPYCPU.Benchmarks
{
    public class Update
    {
        DbContextOptionsBuilder<ChinookDbContext> _optionsBuilder;
        List<Track> listTrack;
        string connectionString;
        //NHibernate
        private readonly ISessionFactory _sessionFactory;

        public Update()
        {
            connectionString = Utility.GetConnectionString();

            //EFCore
            _optionsBuilder = new DbContextOptionsBuilder<ChinookDbContext>().UseNpgsql(connectionString);

            //NHibernate
            NHibernateConnection nHibernateConnection = new NHibernateConnection();
            _sessionFactory = nHibernateConnection.CreateSessionFactory(connectionString);

            //Update için kayıt çekilir
            GetTracks();
        }

        public void GetTracks()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                listTrack = connection.Query<Track>("select track_id," +
                    " name, album_id, media_type_id," +
                    " genre_id, composer, milliseconds, bytes+1 as bytes, unit_price from" +
                    " track order by track_id limit 100000").ToList();
            }
        }

        public ResultCPU CPUUsage(EOrmType eOrmType, int count)
        {
            ResultCPU resultCPU = new ResultCPU()
            {
                Adi = eOrmType.ToString() + "Update",
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
                    EFCoreUpdate(count);
                    break;
                case EOrmType.Dapper:
                    Thread.Sleep(500);
                    DapperUpdate(count);
                    break;
                case EOrmType.NHibernate:
                    Thread.Sleep(500);
                    NHibernateUpdate(count);
                    break;
                default:
                    break;
            }

            // Sistemin birden fazla çekirdeği varsa, bu dikkate alınmalıdır.
            resultCPU.CPUUsage = Math.Round(cpu.NextValue() / Environment.ProcessorCount, 2);

            return resultCPU;
        }

        public void EFCoreUpdate(int count)
        {
            using (ChinookDbContext _context = new ChinookDbContext(_optionsBuilder.Options))
            {
                _context.UpdateRange(listTrack.Take(count));
                _context.SaveChanges();
            }
        }

        public void DapperUpdate(int count)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Execute("update track set album_id=@album_id, " +
                    "bytes=@bytes, composer=@composer, genre_id=@genre_id, " +
                    "media_type_id=@media_type_id, milliseconds=@milliseconds, " +
                    "name=@name, unit_price=@unit_price " +
                    "where track_id=@track_id", listTrack.Take(count));
            }
        }

        public void NHibernateUpdate(int count)
        {
            using (var session = _sessionFactory.OpenSession())
            using (ITransaction tx = session.BeginTransaction())
            {
                foreach (var item in listTrack.Take(count))
                {
                    session.Update(item);
                }
                tx.Commit();
            }
        }
    }
}
