using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mars.Models.ViewModels
{
    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }
        public List<IdentityUser> Members { get; set; }
        public List<IdentityUser> NonMembers { get; set; }
    }
}
