using blog_e17.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace blog_e17.Pages.Post
{
    [Authorize]
    public class IndexModel(AppDBContext _db) : PageModel
    {
        public IEnumerable<PostEntity> Posts { get; set; }

        public async Task OnGet()
        {
            Posts = await _db.Posts
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .ToListAsync();
        }
    }
}
