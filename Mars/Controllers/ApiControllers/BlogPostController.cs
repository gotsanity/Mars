using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mars.Data;
using Mars.Models;
using Mars.Models.ViewModels;

namespace Mars.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogPostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BlogPost
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPostViewModel>>> GetPosts()
        {
            IEnumerable<BlogPost> posts = await _context.Posts.Include(p => p.User).ToListAsync();

            List<BlogPostViewModel> postsModel = new List<BlogPostViewModel>();

            foreach (BlogPost post in posts)
            {
                postsModel.Add(new BlogPostViewModel {
                     BlogPostID = post.BlogPostID,
                     Title = post.Title,
                     Body = post.Body,
                     PostedOn = post.PostedOn,
                     EditedOn = post.EditedOn,
                     Categories = post.Categories,
                     UserId = post.UserId,
                     UserName = post.User.UserName,
                     UserNormalizedUserName = post.User.NormalizedUserName,
                     UserEmail = post.User.Email,
                     UserNormalizedEmail = post.User.NormalizedEmail
                });
            }

            return postsModel;
        }

        // GET: api/BlogPost/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostViewModel>> GetBlogPost(int id)
        {
            var post = await _context.Posts
                .Include(p => p.User)
                .Where(p => p.BlogPostID == id)
                .FirstOrDefaultAsync();

            if (post == null)
            {
                return NotFound();
            }

            BlogPostViewModel blogPost = new BlogPostViewModel
            {
                BlogPostID = post.BlogPostID,
                Title = post.Title,
                Body = post.Body,
                PostedOn = post.PostedOn,
                EditedOn = post.EditedOn,
                Categories = post.Categories,
                UserId = post.UserId,
                UserName = post.User.UserName,
                UserNormalizedUserName = post.User.NormalizedUserName,
                UserEmail = post.User.Email,
                UserNormalizedEmail = post.User.NormalizedEmail
            };

            return blogPost;
        }

        // PUT: api/BlogPost/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogPost(int id, BlogPost blogPost)
        {
            if (id != blogPost.BlogPostID)
            {
                return BadRequest();
            }

            _context.Entry(blogPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BlogPost
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogPost>> PostBlogPost(BlogPost blogPost)
        {
            _context.Posts.Add(blogPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogPost", new { id = blogPost.BlogPostID }, blogPost);
        }

        // DELETE: api/BlogPost/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var blogPost = await _context.Posts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(blogPost);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogPostExists(int id)
        {
            return _context.Posts.Any(e => e.BlogPostID == id);
        }
    }
}
