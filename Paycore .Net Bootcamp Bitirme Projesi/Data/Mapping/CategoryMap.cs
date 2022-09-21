using NHibernate.Mapping.ByCode;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Model;
using NHibernate.Mapping.ByCode.Conformist;
using FluentNHibernate.Automapping.Steps;

namespace Data.Mapping
{
    public class CategoryMap : ClassMapping<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id, x =>
            {
            x.Type(NHibernateUtil.Int32);
            x.Column("Id");
            x.UnsavedValue(0);
            x.Generator(Generators.Increment);
        });
            
            Property(b => b.Name, x =>
            {
            x.Length(50);
            x.Type(NHibernateUtil.String);
            x.NotNullable(true);
        });

            Property(b => b.Description, x =>
            {
            x.Length(50);
            x.Type(NHibernateUtil.String);
            x.NotNullable(true);
        });

          
            Table("category");
    }
}
}
