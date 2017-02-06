using DataAccess.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappings
{
    public class MeetingMap :  ClassMap<Meeting>
    {
        public MeetingMap()
        {

            Table("Meetings");
            Id(m => m.Id).Column("Id").GeneratedBy.Identity();
            Map(m => m.Title, "Title");
            Map(m => m.Description, "Description");

            HasMany<Time>(m => m.Times)
                .Table("Times")
                .KeyColumn("MeetingId")
                .Cascade.All();

            HasMany<Invitation>(m => m.Invitations)
                 .Table("Invitations")
                 .KeyColumn("MeetingId")
                 .Cascade.All();

            References(x => x.Creator).Class<User>().Columns("UserId");
        }
    }
}
