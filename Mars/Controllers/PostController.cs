using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mars.Data;
using Mars.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Mars.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _uow;
        private IBlogPostRepository _repo;

        public PostController(ApplicationDbContext context, UserManager<IdentityUser> usr, IUnitOfWork unitOfWork, IBlogPostRepository blogPostRespository)
        {
            _context = context;
            _userManager = usr;
            _uow = unitOfWork;
            _repo = blogPostRespository;
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
            return View(_repo.GetAllByUser(_userManager.GetUserId(User)));
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = _repo.Get(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // GET: Post/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            BlogPost blog = new BlogPost();
            blog.User = user;
            blog.UserId = user.Id;
            blog.PostedOn = DateTime.Now;
            blog.EditedOn = DateTime.Now;

            TempData["userid"] = user.Id;

            return View(blog);
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogPostID,Title,Body,PostedOn,EditedOn,CategoryId,UserId")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(blogPost);
                _uow.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = _repo.Get(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            blogPost.EditedOn = DateTime.Now;

            return View(blogPost);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogPostID,Title,Body,PostedOn,EditedOn,CategoryId,UserId")] BlogPost model)
        {
            if (id != model.BlogPostID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                BlogPost post = _repo.Get(model.BlogPostID);
                post.Body = model.Body;
                post.EditedOn = DateTime.Now;
                post.Title = model.Title;
                post.Categories = model.Categories;
                post.CategoryId = model.CategoryId;

                _uow.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = _repo.Get(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            BlogPost item = _repo.Get(id);
            _repo.Remove(item);
            _uow.Complete();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(int id)
        {
            return _repo.Get(id) == null;
        }
    }
}
