using DataAccess.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappings
{
    public class PostMap : ClassMap<Post>
    {
        public PostMap()
        {

            Table("Posts");
            Id(m => m.Id).Column("Id").GeneratedBy.Identity();
            Map(m => m.Title, "Title");
            Map(m => m.Content, "Content");
            Map(m => m.CreatedAt, "CreatedAt");
            Map(m => m.Formal, "Formal");
            Map(m => m.Inactive, "Inactive");

            References(x => x.Author).Class<User>().Columns("UserId");
            References(x => x.Category).Class<Category>().Columns("CategoryId");

            HasMany<Comment>(m => m.Comments)
              .Table("Comments")
              .KeyColumn("PostId");

            HasMany<PostFile>(m => m.Files)
               .Table("Files")
               .KeyColumn("PostId");
        }
    }
}
