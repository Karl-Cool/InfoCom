using DataAccess.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappings
{
    public class PostFileMap : ClassMap<PostFile>
    {
        public PostFileMap()
        {

            Table("Files");
            Id(m => m.Id).Column("Id").GeneratedBy.Identity();
            Map(m => m.FilePath, "FilePath");

            References(x => x.Post).Class<Post>().Columns("PostId");
        }
    }
}
