using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using Dapper;
using DataAccessLayer;
using DataAccessLayer.Utility;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using NHibernate;
using Npgsql;
using System.Collections.Generic;
using System.Linq;

namespace ORMPYBenchmark.Benchmarks
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    [SimpleJob(RunStrategy.ColdStart, BenchmarkDotNet.Jobs.RuntimeMoniker.Net60,
        id: "Guncelleme Karsilastirmalari", targetCount: 100)]
    [MemoryDiagnoser]
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
                    " track order by track_id").ToList();
            }
        }

        [Benchmark]
        //[Arguments(1000)]
        //[Arguments(10000)]
        [Arguments(25000)]
        public void EFCoreUpdate(int count)
        {
            using (ChinookDbContext _context = new ChinookDbContext(_optionsBuilder.Options))
            {
                _context.UpdateRange(listTrack.Take(count));
                _context.SaveChanges();
            }
        }

        [Benchmark]
        //[Arguments(1000)]
        //[Arguments(10000)]
        [Arguments(25000)]
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

        [Benchmark]
        //[Arguments(1000)]
        //[Arguments(10000)]
        [Arguments(25000)]
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
