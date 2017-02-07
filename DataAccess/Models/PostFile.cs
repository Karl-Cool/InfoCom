using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class PostFile
    {
        public virtual int Id { get; set; }
        public virtual Post Post { get; set; }
        public virtual string FilePath { get; set; }
    }
}
