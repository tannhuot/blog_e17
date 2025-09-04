using System.ComponentModel.DataAnnotations.Schema;

namespace blog_e17.Models
{
    public class PostEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Thumnail { get; set; }
        
        public int CategoryId { get; set; } //FK
        public CategoryEntity Category { get; set; }

        // Many-to-Many
        public ICollection<TagEntity> Tags { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
