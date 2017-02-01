using DataAccess.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappings
{
    public class FileMap : ClassMap<File>
    {
        public FileMap()
        {

            Table("Files");
            Id(m => m.Id).Column("Id").GeneratedBy.Identity();
            Map(m => m.FilePath, "FilePath");

            References(x => x.Post).Class<File>().Columns("PostId");
        }
    }
}
