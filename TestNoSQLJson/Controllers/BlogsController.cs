using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestNoSQLJson.Models;

namespace EFGetStarted.AspNetCore.NewDb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogsController : ControllerBase
    {
        private BloggingContext _context;

        public BlogsController(BloggingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IList<Blog>> Get()
        {
            return await _context.Blogs.ToListAsync();
        }

        [HttpGet("{Owner}")]
        public async Task<IList<Blog>> Search(decimal versionNumber)
        {
            // Option 1: .Net side filter using LINQ:
            //var blogs = await _context.Blogs
            //                .Where(b => b.Owner.Name == Owner)
            //                .ToListAsync();

            // Option 2: SQL Server filter using T-SQL:
            var blogs = await _context.Blogs
                            .FromSqlRaw(@"SELECT * FROM Blogs
                                WHERE JSON_VALUE(Owner, '$.Version') >= {0}", versionNumber)
                            .ToListAsync();

            return blogs;
        }

        [HttpPost]
        public async Task<Blog> Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                await _context.Blogs.AddAsync(blog);
                await _context.SaveChangesAsync();
            }

            return blog;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var blog = _context.Blogs.Single(x => x.BlogId == id);
            if (blog is null)
                throw new KeyNotFoundException();

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }
    }
}