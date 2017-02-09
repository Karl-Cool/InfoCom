using System;
using DataAccess.Models;

namespace InfoCom.ViewModels
{
    public class CommentViewModel
    {
        public Post Post { get; set; }
        public User Author { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual bool Inactive { get; set; }

    }
}