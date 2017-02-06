using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public virtual ISet<TimeChoice> TimeChoices { get; set; }

        public Time()
        {
            TimeChoices = new HashSet<TimeChoice>();
            Meeting = new Meeting();
        }
    }
}
