using Mars.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mars.Data
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {
        IEnumerable<BlogPost> GetAllByUser(string UserId);
        IEnumerable<BlogPost> GetAllByUser(IdentityUser user);
    }
}
