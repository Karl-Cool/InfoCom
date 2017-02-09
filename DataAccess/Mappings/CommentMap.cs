using DataAccess.Models;
using FluentNHibernate.Mapping;

namespace DataAccess.Mappings
{
    public class CommentMap : ClassMap<Comment>
    {
        public CommentMap()
        {

            Table("Comments");
            Id(m => m.Id).Column("Id").GeneratedBy.Identity();
            Map(m => m.Content, "Comment");
            Map(m => m.CreatedAt, "CreatedAt");
            Map(m => m.Inactive, "Inactive");

            References(x => x.Author).Class<User>().Columns("UserId");
            References(x => x.Post).Class<Post>().Columns("PostId");
        }
    }
}
