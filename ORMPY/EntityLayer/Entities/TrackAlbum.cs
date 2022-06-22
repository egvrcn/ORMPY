using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    //[Table("track")]
    public class TrackAlbum
    {
        //[Key]
        public virtual int track_id { get; set; }
        public virtual string name { get; set; }
        public virtual int album_id { get; set; }
        public virtual int media_type_id { get; set; }
        public virtual int genre_id { get; set; }
        public virtual string? composer { get; set; }
        public virtual int milliseconds { get; set; }
        public virtual int bytes { get; set; }
        public virtual decimal unit_price { get; set; }
        public virtual string title { get; set; }
        public virtual int artist_id { get; set; }
    }
}
