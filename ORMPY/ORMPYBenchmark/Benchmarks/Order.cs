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
        id: "Siralama Karsilastirmalari", targetCount: 100)]
    [MemoryDiagnoser]
    public class Order
    {
        DbContextOptionsBuilder<ChinookDbContext> _optionsBuilder;
        string connectionString;
        //NHibernate
        private readonly ISessionFactory _sessionFactory;

        public Order()
        {
            connectionString = Utility.GetConnectionString();

            //EFCore
            _optionsBuilder = new DbContextOptionsBuilder<ChinookDbContext>().UseNpgsql(connectionString);

            //NHibernate
            NHibernateConnection nHibernateConnection = new NHibernateConnection();
            _sessionFactory = nHibernateConnection.CreateSessionFactory(connectionString);
        }

        [Benchmark]
        public void EFCoreOrder()
        {
            using (ChinookDbContext _context = new ChinookDbContext(_optionsBuilder.Options))
            {
                var list = _context.Track.OrderByDescending(x => x.name).ToList();
            }
        }

        [Benchmark]
        public void DapperOrder()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var list = connection.Query<Track>("select * from track order by name desc").ToList();
            }
        }

        [Benchmark]
        public void NHibernateOrder()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var list = session.CreateQuery("from Track order by name desc").List<Track>();
            }
        }
    }
}
