using EntityLayer.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using Microsoft.Extensions.Configuration;
using NHibernate;

namespace DataAccessLayer
{
    public class NHibernateConnection
    {
        IConfiguration _configuration;

        public NHibernateConnection()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile(
                "appsettings.json", optional: true, reloadOnChange: true).Build();
        }

        public ISessionFactory CreateSessionFactory(string connectionString)
        {
            AutoPersistenceModel model = CreateMappings();
            AutoPersistenceModel albumModel = CreateMappingsAlbum();

            return Fluently.Configure()
                .Database(
                FluentNHibernate.Cfg.Db.PostgreSQLConfiguration.Standard
                .ConnectionString(connectionString)
                ).Mappings(m =>
                {
                    m.AutoMappings.Add(model);
                    m.AutoMappings.Add(albumModel);
                    m.FluentMappings.Conventions.Setup(x =>
                    {
                        x.Add(FluentNHibernate.Conventions.Helpers.AutoImport.Always()); // AutoImport.Never
                        x.AddFromAssemblyOf<TrackMap>();
                        x.AddFromAssemblyOf<AlbumMap>();
                        x.AddFromAssemblyOf<Track>();
                        x.AddFromAssemblyOf<Album>();
                    }); // End FluentMappings.Conventions.Setup
                })
                .BuildSessionFactory();
        }

        protected static AutoPersistenceModel CreateMappings()
        {
            return new AutoPersistenceModel().AddMappingsFromAssemblyOf<TrackMap>();
        }

        protected static AutoPersistenceModel CreateMappingsAlbum()
        {
            return new AutoPersistenceModel().AddMappingsFromAssemblyOf<AlbumMap>();
        }
    }
}
