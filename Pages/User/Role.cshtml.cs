using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace blog_e17.Pages.User
{
    public class RoleModel(RoleManager<IdentityRole> _roleManager) : PageModel
    {

        [BindProperty]
        public string Name { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = Name });
            
            return RedirectToPage("/Index");
        }
    }
}
