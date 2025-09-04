using blog_e17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace blog_e17.Pages.Category
{
    public class IndexModel(AppDBContext _db) : PageModel
    {
        public IEnumerable<CategoryEntity> Categories { get; set; }
        public async Task OnGet()
        {
            Categories = await _db.Categories.ToListAsync();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            var existCate = await _db.Categories.FindAsync(id);
            _db.Categories.Remove(existCate);
            await _db.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
