using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Product
    {
        public virtual int Id { get; set; }
        
        public virtual string Name { get; set; }
        public virtual string Color { get; set; }
        public virtual double Price { get; set; }
        public virtual DateTime  AddedDate{ get; set; }
        public virtual bool isOfferable { get; set; }
        public virtual bool isSold { get; set; }
         public  virtual int CategoryId { get; set; }
        public virtual int UserId { get; set; }
        public virtual User  User { get; set; }


        public virtual Category Category { get; set; }




    }
}
