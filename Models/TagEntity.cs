namespace blog_e17.Models
{
    public class TagEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<PostEntity> Posts { get; set; }
    }
}
