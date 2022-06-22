using Dapper;
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
    public class Join
    {
        DbContextOptionsBuilder<ChinookDbContext> _optionsBuilder;
        string connectionString;
        //NHibernate
        private readonly ISessionFactory _sessionFactory;

        public Join()
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
                Adi = eOrmType.ToString() + "Join",
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
                    EFCoreJoin();
                    break;
                case EOrmType.Dapper:
                    Thread.Sleep(500);
                    DapperJoin();
                    break;
                case EOrmType.NHibernate:
                    Thread.Sleep(500);
                    NHibernateJoin();
                    break;
                default:
                    break;
            }

            // Sistemin birden fazla çekirdeği varsa, bu dikkate alınmalıdır.
            resultCPU.CPUUsage = Math.Round(cpu.NextValue() / Environment.ProcessorCount, 2);

            return resultCPU;
        }

        public void EFCoreJoin()
        {
            using (ChinookDbContext _context = new ChinookDbContext(_optionsBuilder.Options))
            {
                var list = _context.Track.Join(_context.Album, t => t.album_id, a => a.album_id,
                 (t, a) => new
                 {
                     album_id = t.album_id,
                     bytes = t.bytes,
                     composer = t.composer,
                     genre_id = t.genre_id,
                     media_type_id = t.media_type_id,
                     milliseconds = t.milliseconds,
                     name = t.name,
                     title = a.title,
                     track_id = t.track_id,
                     unit_price = t.unit_price,
                     artist_id = a.artist_id
                 }).ToList();
            }
        }

        public void DapperJoin()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var list = connection.Query<TrackAlbum>($@"select * from track t inner join album a 
                                on t.album_id = a.album_id").ToList();
            }
        }

        public void NHibernateJoin()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var list = session.CreateQuery("from Track t inner join Album a on t.album_id = a.album_id").List();
            }
        }
    }

}
