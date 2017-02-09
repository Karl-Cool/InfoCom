using DataAccess.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappings
{
    public class CommentMap : ClassMap<Comment>
    {
        public CommentMap()
        {

            Table("Comments");
            Id(m => m.Id).Column("Id").GeneratedBy.Identity();
            Map(m => m.Content, "Content");
            Map(m => m.Inactive, "Inactive");
            Map(m => m.CreatedAt, "CreatedAt");
            Map(m => m.Inactive, "Inactive");

            References(x => x.Author).Class<User>().Columns("UserId");
            References(x => x.Post).Class<Post>().Columns("PostId");
        }
    }
}
