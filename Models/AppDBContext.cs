using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace blog_e17.Models
{
    public class AppDBContext(IConfiguration configuration) : IdentityDbContext<UserEntity>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("conn"));
        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
    }
}
