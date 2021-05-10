using Mars.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mars.Data
{
    public class BlogPostRepository : Repository<BlogPost>, IBlogPostRespository
    {
        public BlogPostRepository(ApplicationDbContext context) : base(context)
        {

        }

        public override BlogPost Get(int id)
        {
            return _context.Posts
                .Where(p => p.BlogPostID == id)
                .Include(p => p.User)
                .Include(p => p.Categories)
                .SingleOrDefault();
        }

        public override IEnumerable<BlogPost> GetAll()
        {
            return _context.Posts
                .Include(p => p.User)
                .Include(p => p.Categories)
                .ToList();
        }
    }
}
