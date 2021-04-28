using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mars.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Name field is required.")]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<BlogPost> Posts { get; set; }
    }
}
