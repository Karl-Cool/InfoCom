using DataAccess.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappings
{
    public class TimeChoiceMap : ClassMap<TimeChoice>
    {
        public TimeChoiceMap()
        {

            Table("TimeChoices");
            Id(m => m.Id).Column("Id").GeneratedBy.Identity();
            

            References(x => x.Time).Class<Time>().Columns("TimeId");
            References(x => x.Meeting).Class<Meeting>().Columns("MeetingId");
            References(x => x.User).Class<User>().Columns("UserId");
        }
    }
}
