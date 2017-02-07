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
        public virtual bool Inactive { get; set; }
        public virtual ISet<Meeting> Meetings { get; set; }
        public virtual ISet<Invitation> Invitations { get; set; }
        public virtual ISet<Comment> Comments { get; set; }
        public virtual ISet<Post> Posts { get; set; }
        public virtual ISet<TimeChoice> TimeChoices { get; set; }

        public User()
        {
            Meetings = new HashSet<Meeting>();
            Comments = new HashSet<Comment>();
            Posts = new HashSet<Post>();
            Invitations = new HashSet<Invitation>();
            TimeChoices = new HashSet<TimeChoice>();
        }
    }
}
