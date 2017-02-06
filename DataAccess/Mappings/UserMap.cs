using DataAccess.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {

            Table("Users");
            Id(m => m.Id).Column("Id").GeneratedBy.Identity();
            Map(m => m.Username, "Username");
            Map(m => m.Email, "Email");
            Map(m => m.Name, "Name");
            Map(m => m.Password, "Password");

            HasMany<Meeting>(m => m.Meetings)
                .Table("Meetings")
                .KeyColumn("UserId")
                .Cascade.All();

            HasMany<Invitation>(m => m.Invitations)
                .Table("Invitations")
                .KeyColumn("UserId")
                .Cascade.All();

            HasMany<Post>(m => m.Posts)
                .Table("Posts")
                .KeyColumn("UserId")
                .Cascade.All();

            HasMany<Comment>(m => m.Comments)
                .Table("Comments")
                .KeyColumn("UserId")
                .Cascade.All();
        }
    }
}
