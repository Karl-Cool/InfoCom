using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfoCom.ViewModels
{
    public class PostViewModel
    {
        public virtual int Id { get; set; }
        public virtual User Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual bool Formal { get; set; }
        public virtual ISet<Comment> Comments { get; set; }
        public virtual ISet<File> Files { get; set; }
        public virtual List<Post> PostList {get; set; }
       

    }
}