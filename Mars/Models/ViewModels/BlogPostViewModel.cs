using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mars.Models.ViewModels
{
    public class BlogPostViewModel
    {
        public int BlogPostID { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Body of post is required.")]
        public string Body { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime EditedOn { get; set; }

        public virtual int CategoryId { get; set; }
        public virtual List<Category> Categories { get; set; }

        public virtual string UserId { get; set; }
        public string UserName { get; set; }
        public string UserNormalizedUserName { get; set; }
        public string UserEmail { get; set; }
        public string UserNormalizedEmail { get; set; }
    }
}
