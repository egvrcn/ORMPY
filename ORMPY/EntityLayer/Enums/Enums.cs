using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Enums
{
    public class Enums
    {
        public enum EOrmType
        {
            EFCore,
            Dapper,
            NHibernate
        }

        public enum EProcessType
        {
            Read,
            Insert,
            Update,
            Delete,
            Order,
            Search,
            Join
        }
    }
}
