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
    id: "Silme Karsilastirmalari", targetCount: 100)]
    [MemoryDiagnoser]
    public class Delete
    {
        DbContextOptionsBuilder<ChinookDbContext> _optionsBuilder;
        string connectionString;

        //NHibernate
        private readonly ISessionFactory _sessionFactory;

        public Delete()
        {
            connectionString = Utility.GetConnectionString();

            //EFCore
            _optionsBuilder = new DbContextOptionsBuilder<ChinookDbContext>().UseNpgsql(connectionString);

            //NHibernate
            NHibernateConnection nHibernateConnection = new NHibernateConnection();
            _sessionFactory = nHibernateConnection.CreateSessionFactory(connectionString);
        }

        [Benchmark]
        //[Arguments(1000)]
        //[Arguments(10000)]
        [Arguments(25000)]
        public void EFCoreDelete(int count)
        {
            using (ChinookDbContext _context = new ChinookDbContext(_optionsBuilder.Options))
            {
                _context.Track.RemoveRange(_context.Track.Take(count));
                _context.SaveChanges();
            }
        }

        [Benchmark]
        //[Arguments(1000)]
        //[Arguments(10000)]
        [Arguments(25000)]
        public void DapperDelete(int count)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Execute($"delete from track where track_id in" +
                    $"(select track_id from track limit {count})");
            }
        }

        [Benchmark]
        //[Arguments(1000)]
        //[Arguments(10000)]
        [Arguments(25000)]
        public void NHibernateDelete(int count)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                session.CreateQuery("delete Track t where t.track_id IN (:idList)")
                    .SetParameterList("idList", session.CreateQuery("select track_id from Track")
                    .SetMaxResults(count).List())
                    .ExecuteUpdate();
            }
        }
    }
}
