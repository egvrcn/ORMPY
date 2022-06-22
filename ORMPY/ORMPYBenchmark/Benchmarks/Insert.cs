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
    id: "Ekleme Karsilastirmalari", targetCount: 100)]
    [MemoryDiagnoser]
    public class Insert
    {
        DbContextOptionsBuilder<ChinookDbContext> _optionsBuilder;
        List<Track> listTrack;
        string connectionString;
        //NHibernate
        private readonly ISessionFactory _sessionFactory;

        public Insert()
        {
            connectionString = Utility.GetConnectionString();

            //EFCore
            _optionsBuilder = new DbContextOptionsBuilder<ChinookDbContext>().UseNpgsql(connectionString);

            //NHibernate
            NHibernateConnection nHibernateConnection = new NHibernateConnection();
            _sessionFactory = nHibernateConnection.CreateSessionFactory(connectionString);

            //Insert için kayıt çekilir
            GetTracks();
        }

        public void GetTracks()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                listTrack = connection.Query<Track>("select 0 as track_id, name, album_id, media_type_id," +
                    " genre_id, composer, milliseconds, bytes, unit_price from" +
                    " track").ToList();
            }
        }

        [IterationSetup(Target = nameof(EFCoreInsert))]
        public void IterationSetup()
           => listTrack.ForEach(f => f.track_id = 0);

        [Benchmark]
        //[Arguments(1000)]
        //[Arguments(10000)]
        [Arguments(25000)]
        public void EFCoreInsert(int count)
        {
            using (ChinookDbContext _context = new ChinookDbContext(_optionsBuilder.Options))
            {
                _context.AddRange(listTrack.Take(count));
                _context.SaveChanges();
            }
        }

        [Benchmark]
        //[Arguments(1000)]
        //[Arguments(10000)]
        [Arguments(25000)]
        public void DapperInsert(int count)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Execute("insert into track(album_id, bytes," +
                    "composer, genre_id, media_type_id, milliseconds," +
                    "name, unit_price) values(@album_id, " +
                    "@bytes, @composer, @genre_id, @media_type_id, @milliseconds, @name, " +
                    " @unit_price)", listTrack.Take(count));
            }
        }

        [Benchmark]
        //[Arguments(1000)]
        //[Arguments(10000)]
        [Arguments(25000)]
        public void NHibernateInsert(int count)
        {
            using (var session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (var item in listTrack.Take(count))
                {
                    session.Save(item);
                }
                transaction.Commit();
            }
        }
    }
}
