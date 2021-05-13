using Mars.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mars.Data
{
    public class BlogPostRepository : Repository<BlogPost>, IBlogPostRepository
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

        public IEnumerable<BlogPost> GetAllByUser(string UserId)
        {
            return _context.Posts
                .Where(p => p.UserId == UserId)
                .Include(p => p.User)
                .Include(p => p.Categories)
                .ToList();
        }

        public IEnumerable<BlogPost> GetAllByUser(IdentityUser user)
        {
            return _context.Posts.Where(p => p.UserId == user.Id)
                .Include(p => p.User)
                .Include(p => p.Categories)
                .ToList();
        }
    }
}
