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
    public class CategoryDto
    {
        [Required]
        [MaxLength(125)]
        [UserNameAttribute]
        [Display(Name = " Name")]
        public string Name { get; set; }


    [Required]
    [MaxLength(125)]
    [UserNameAttribute]
    [Display(Name = " Description")]
    public string Description { get; set; }
}
}

