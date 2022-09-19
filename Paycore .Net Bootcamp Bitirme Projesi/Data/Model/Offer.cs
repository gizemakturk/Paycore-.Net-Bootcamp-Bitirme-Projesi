﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Offer
    {
        public virtual int Id { get; set; }
        [Required]
        public virtual int? Amount { get; set; }
       // public virtual int? Percentage { get; set; }
        public virtual int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
