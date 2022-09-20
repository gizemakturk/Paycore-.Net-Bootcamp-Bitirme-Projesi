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
    public class OfferMap : ClassMapping<Offer>
    {
        public OfferMap()
        {
            Id(x => x.Id, x =>
            {
            x.Type(NHibernateUtil.Int32);
            x.Column("Id");
            x.UnsavedValue(0);
            x.Generator(Generators.Increment);
        });
            
            Property(b => b.Amount, x =>
            {
            x.Length(50);
            x.Type(NHibernateUtil.Int32);
            x.NotNullable(true);
        });
            ManyToOne(x => x.Product, m => m.Column("ProductId"));
            ManyToOne(x => x.User, m => m.Column("UserId"));


            
            Table("offer");
    }
}
}
