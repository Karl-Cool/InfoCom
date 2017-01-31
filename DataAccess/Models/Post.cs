using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Post
    {
        public virtual int Id { get; set; }
        public virtual User Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual string CreatedAt { get; set; }
        public virtual bool Formal { get; set; }
        public virtual ISet<Comment> Comments { get; set; }
        public virtual ISet<File> Files { get; set; }
        public Post()
        {
            Comments = new HashSet<Comment>();
            Files = new HashSet<File>();
        }
    }


}
