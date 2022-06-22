using FluentNHibernate.Mapping;

namespace EntityLayer.Entities
{
    public class AlbumMap : ClassMap<Album>
    {
        public AlbumMap()
        {
            Table("album");
            Id(x => x.album_id, "album_id").GeneratedBy.SequenceIdentity("sq_album_id");
            Map(x => x.title, "title");
            Map(x => x.artist_id, "artist_id");
        }
    }
}
