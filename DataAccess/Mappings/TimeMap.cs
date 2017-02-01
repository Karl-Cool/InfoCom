using DataAccess.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappings
{
    public class TimeMap : ClassMap<Time>
    {
        public TimeMap()
        {

            Table("Times");
            Id(m => m.Id).Column("Id").GeneratedBy.Identity();
            Map(m => m.Date, "Time");

            References(x => x.Meeting).Class<Meeting>().Columns("MeetingId");
        }
    }
}
