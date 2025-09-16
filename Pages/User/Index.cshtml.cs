using blog_e17.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace blog_e17.Pages.User
{
    public class IndexModel(UserManager<UserEntity> _userManager) : PageModel
    {

        public UserEntity currentUser;

        public async Task OnGet()
        {
            currentUser = await _userManager.GetUserAsync(User);
            await _userManager.AddToRoleAsync(currentUser, "Admin");
        }
    }
}
