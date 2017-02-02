using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Invitation
    {
        public virtual int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Meeting Meeting { get; set; }
        public virtual int Status { get; set; }
        public virtual bool Notified { get; set; }
    }
}
