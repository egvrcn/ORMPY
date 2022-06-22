using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    [Table("track")]
    public class Track
    {
        [Key]
        public virtual int track_id { get; set; }
        public virtual string name { get; set; }
        public virtual int album_id { get; set; }
        public virtual int media_type_id { get; set; }
        public virtual int genre_id { get; set; }
        public virtual string? composer { get; set; }
        public virtual int milliseconds { get; set; }
        public virtual int bytes { get; set; }
        public virtual decimal unit_price { get; set; }
    }
}
