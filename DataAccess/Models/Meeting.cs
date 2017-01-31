using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Meeting
    {
        public virtual int Id { get; set; }
        public virtual User Creator { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual ISet<Time> Times { get; set; }

        public Meeting()
        {
            Times = new HashSet<Time>();
        }
    }
}
