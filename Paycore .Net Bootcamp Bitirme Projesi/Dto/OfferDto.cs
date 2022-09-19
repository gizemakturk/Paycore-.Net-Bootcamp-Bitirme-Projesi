using Base.Attribute;
using Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dto
{
    public class OfferDto
    {
        [Required]
        [Display(Name = "Amount")]
        public int Amount { get; set; }


    [Required]
    [Display(Name = " Product")]
    public int ProductId { get; set; }


}
}

