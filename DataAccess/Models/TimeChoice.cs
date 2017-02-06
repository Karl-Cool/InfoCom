using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
   public class TimeChoice
    {
        public virtual int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Time Time { get; set; }
        public virtual Meeting Meeting { get; set; }
    }
}
