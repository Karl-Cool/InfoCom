using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    class Category
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ISet<Post> Posts { get; set; }

        public Category()
        {
            Posts = new HashSet<Post>();
        }
    }
}
