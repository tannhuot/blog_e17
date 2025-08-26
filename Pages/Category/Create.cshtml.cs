using blog_e17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace blog_e17.Pages.Category
{
    public class CreateModel(AppDBContext _db) : PageModel
    {

        [BindProperty]
        public CategoryEntity Category { get; set; }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost() {

            if (ModelState.IsValid) {
                await _db.Categories.AddAsync(Category);
                await _db.SaveChangesAsync();
                return RedirectToPage("/category/index");
            }

            return Page();
        }
    }
}
