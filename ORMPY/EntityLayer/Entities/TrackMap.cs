using FluentNHibernate.Mapping;

namespace EntityLayer.Entities
{
    public class TrackMap : ClassMap<Track>
    {
        public TrackMap()
        {
            Table("track");
            Id(x => x.track_id, "track_id").GeneratedBy.SequenceIdentity("sq_track_id");
            Map(x => x.name, "name");
            Map(x => x.album_id, "album_id");
            Map(x => x.media_type_id, "media_type_id");
            Map(x => x.genre_id, "genre_id");
            Map(x => x.composer, "composer");
            Map(x => x.milliseconds, "milliseconds");
            Map(x => x.bytes, "bytes");
            Map(x => x.unit_price, "unit_price");
        }
    }
}
