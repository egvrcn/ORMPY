using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Order;
using Dapper;
using DataAccessLayer;
using DataAccessLayer.Utility;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using NHibernate;
using Npgsql;
using System.Linq;

namespace ORMPYBenchmark.Benchmarks
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    [SimpleJob(BenchmarkDotNet.Engines.RunStrategy.ColdStart,
        BenchmarkDotNet.Jobs.RuntimeMoniker.Net60,
        id: "Arama Karsilastirmalari", targetCount: 100)]
    [MemoryDiagnoser]
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

        [Benchmark]
        public void EFCoreSearch()
        {
            using (ChinookDbContext _context = new ChinookDbContext(_optionsBuilder.Options))
            {
                var list = _context.Track.Where(x => x.name.Contains("Song")).ToList();
                int x = 0;
            }
        }

        [Benchmark]
        public void DapperSearch()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var list = connection.Query<Track>("select * from track where name like '%Song%'").ToList();
                int x = 0;
            }
        }

        [Benchmark]
        public void NHibernateSearch()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var list = session.CreateQuery("from Track where name like '%Song%'").List<Track>();
            }
        }
    }
}
