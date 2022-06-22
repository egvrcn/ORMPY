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
         id: "Join Karsilastirmalari", targetCount: 100)]
    [MemoryDiagnoser]
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

        [Benchmark]
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

        [Benchmark]
        public void DapperJoin()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var list = connection.Query<TrackAlbum>($@"select * from track t inner join album a 
                                on t.album_id = a.album_id").ToList();
            }
        }

        [Benchmark]
        public void NHibernateJoin()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var list = session.CreateQuery("from Track t inner join Album a on t.album_id = a.album_id").List();
            }
        }
    }

}
