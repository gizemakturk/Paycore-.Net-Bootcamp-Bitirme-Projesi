using Base.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dto
{
    public class ProductDto
    {
        [Required]
        [MaxLength(100)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        [Display(Name = "Product Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Product Color")]
        public string Color { get; set; }
        [Required]
        [Display(Name = "Product Brand")]
        public string Brand { get; set; }
        [Required]
      //  [MaxLength(500)]
        public double Price { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(500)]
        public string Email { get; set; }


  


        [Display(Name = "Added Date")]
        public DateTime AddedDate { get; set; }
    }
}

