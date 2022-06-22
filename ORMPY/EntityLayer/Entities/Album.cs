using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    [Table("album")]
    public class Album
    {
        [Key]
        public virtual int album_id { get; set; }
        public virtual string title { get; set; }
        public virtual int artist_id { get; set; }
    }
}
