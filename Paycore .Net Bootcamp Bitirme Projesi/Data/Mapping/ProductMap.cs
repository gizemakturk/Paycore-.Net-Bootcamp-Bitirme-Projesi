using NHibernate.Mapping.ByCode;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode.Conformist;
using Data.Model;

namespace Data.Mapping
{
    public class ProductMap:ClassMapping<Product>
    {
        public ProductMap()
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
            x.Length(100);
            x.Type(NHibernateUtil.String);
            x.NotNullable(true);
        });
            Property(b => b.Description, x =>
            {
                x.Length(500);
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });
            Property(b => b.Brand, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });
            Property(b => b.Color, x =>
            {
            x.Length(50);
            x.Type(NHibernateUtil.String);
            x.NotNullable(true);
        });
            Property(b => b.Price, x =>
            {
            x.Type(NHibernateUtil.Double);
            x.NotNullable(true);
        });
            Property(b => b.AddedDate, x =>
            {
                x.Length(100);
            x.Type(NHibernateUtil.DateTime);
            x.NotNullable(true);
        });
            Property(b => b.isOfferable, x =>
            {
                x.Type(NHibernateUtil.Boolean);
                x.NotNullable(true);
            });
            Property(b => b.isSold, x =>
            {
                x.Type(NHibernateUtil.Boolean);
                x.NotNullable(true);
            });

            ManyToOne(x => x.Category, m => m.Column("CategoryId"));
            ManyToOne(x => x.User, m => m.Column("UserId"));
            Bag(x => x.Offers, m => m.Key(k => k.Column("Id")), rel => rel.OneToMany());

            Table("product");
    }
}
}
