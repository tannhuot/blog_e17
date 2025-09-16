using blog_e17.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace blog_e17.Pages.Post
{
    [Authorize(Roles ="Admin, StockControler, IT")]
    public class CreateModel(AppDBContext _db, IWebHostEnvironment _environment) : PageModel
    {
        [BindProperty]
        public PostEntity Post { get; set; }

        IEnumerable<CategoryEntity> Categories;
        public SelectList CategoryItems { get; set; }
        [BindProperty]
        public int CategoryId { get; set; }

        public IEnumerable<TagEntity> Tags { get; set; }
        [BindProperty]
        public List<int> selectedTags { get; set; } = new List<int>();

        public async Task OnGet()
        {
            await getData();
            CategoryId = Categories.First().Id;
        }

        async Task getData()
        {
            Categories = await _db.Categories.ToListAsync();
            CategoryItems = new SelectList(Categories, nameof(CategoryEntity.Id), nameof(CategoryEntity.Name));

            Tags = await _db.Tags.ToListAsync();
            foreach (var id in selectedTags)
            {
                foreach (var tag in Tags)
                {
                    if (tag.Id == id)
                    {
                        tag.IsSelected = true;
                    }
                }
            }
        }

        public async Task<IActionResult> OnPost()
        {
            ModelState.Remove("Post.Thumnail");
            ModelState.Remove("Post.Category");
            ModelState.Remove("Post.Tags");
            ModelState.Remove("Post.ImageFile");

            if (ModelState.IsValid)
            {
                if (Post.ImageFile != null && Post.ImageFile.Length > 0)
                {
                    // Get the directory "wwwroot"
                    var uploadFolder = Path.Combine(_environment.WebRootPath, "uploads");

                    // Create folder "uploads" if not exist
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    // Create unique file name
                    var fileName = Path.GetFileNameWithoutExtension(Post.ImageFile.FileName);
                    var extension = Path.GetExtension(Post.ImageFile.FileName);
                    var uploadFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                    var filePath = Path.Combine(uploadFolder, uploadFileName);
                    //System.IO.File.Delete(filePath);
                    // Save file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Post.ImageFile.CopyToAsync(fileStream);
                    }
                    // Set file name to DB
                    Post.Thumnail = uploadFileName;
                }
                else
                {
                    Post.Thumnail = "";
                }

                // one-to-many with tbl_Category
                var cate = await _db.Categories.Include(c => c.Posts).FirstAsync(c => c.Id == CategoryId);
                cate.Posts.Add(Post);
                // Post.Category = cate;

                //Many-to-Many
                Post.Tags = [];
                foreach (var tagId in selectedTags)
                {
                    var existTag = await _db.Tags.FindAsync(tagId);
                    Post.Tags.Add(existTag);
                }

                await _db.Posts.AddAsync(Post);

                _db.SaveChanges();

                return RedirectToPage("/post/index");
            }

            await getData();
            return Page();
        }
    }
}
