using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Time
    {
        public virtual int Id { get; set; }
        public virtual Meeting Meeting { get; set; }
        public virtual DateTime Date { get; set; }

    }
}
