using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string Name { get; set; }
        public virtual string Password { get; set; }
        public virtual ISet<Meeting> Meetings { get; set; }

        public User()
        {
            Meetings = new HashSet<Meeting>();
        }
    }
}
