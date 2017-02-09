using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "You need to write a title.")]
        public virtual string Title { get; set; }
        [StringLength(5-20)]
        [Required(ErrorMessage ="You need to write a few words.")]
        public virtual string Content { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual bool Formal { get; set; }
        public virtual ISet<Comment> Comments { get; set; }
        public virtual ISet<PostFile> Files { get; set; }
        public virtual bool Inactive { get; set; }
        public Post()
        {
            Comments = new HashSet<Comment>();
            Files = new HashSet<PostFile>();
        }
    }


}
