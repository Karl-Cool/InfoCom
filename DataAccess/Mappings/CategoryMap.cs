using DataAccess.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappings
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {

            Table("Categories");
            Id(m => m.Id).Column("Id").GeneratedBy.Identity();
            Map(m => m.Name, "Name");

            HasMany<Post>(m => m.Posts)
                .Table("Posts")
                .KeyColumn("CategoryId");
        }
    }
}
