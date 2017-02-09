using System;

namespace DataAccess.Models
{
   public class Comment
    {
        public virtual int Id { get; set; }
        public virtual User Author { get; set; }
        public virtual Post Post { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual bool Inactive { get; set; }
    }
}
