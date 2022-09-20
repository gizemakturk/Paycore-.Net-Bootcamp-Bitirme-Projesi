using Base.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class User : IdentityUser
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual Role Role { get; set; }
        public virtual string Password { get; set; }
        public virtual IList<Product> Products { get; set; }
    }
}
