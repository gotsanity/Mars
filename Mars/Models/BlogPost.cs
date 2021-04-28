using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mars.Models
{
    public class BlogPost
    {
        public int BlogPostID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime EditedOn { get; set; }

        public virtual int CategoryId { get; set; }
        public virtual List<Category> Categories { get; set; }

        public virtual string AuthorId { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
