using DataAccess.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappings
{
    public class InvitationMap : ClassMap<Invitation>
    {
        public InvitationMap()
        {

            Table("Invitation");
            Id(m => m.Id).Column("Id").GeneratedBy.Identity();
            Map(m => m.Status, "Status");
            Map(m => m.Notified, "Notified");

            References(x => x.Meeting).Class<Meeting>().Columns("MeetingId");
            References(x => x.User).Class<User>().Columns("UserId");
        }
    }
}
