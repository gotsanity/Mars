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
        public BlogPostRepository(DbContext context) : base(context)
        {
        }

        public override BlogPost Get(int id)
        {
            return base.Get(id);
        }

        public override IEnumerable<BlogPost> GetAll()
        {
            return base.GetAll();
        }
    }
}
