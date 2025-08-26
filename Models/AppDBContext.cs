using Microsoft.EntityFrameworkCore;

namespace blog_e17.Models
{
    public class AppDBContext(IConfiguration configuration) : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("conn"));
        }

        public DbSet<CategoryEntity> Categories { get; set; }
    }
}
