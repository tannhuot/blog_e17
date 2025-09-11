using System.ComponentModel.DataAnnotations.Schema;

namespace blog_e17.Models
{
    public class TagEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; } = false;

        public ICollection<PostEntity> Posts { get; set; }
    }
}
