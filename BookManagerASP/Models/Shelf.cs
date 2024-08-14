namespace BookManagerASP.Models
{
    public class Shelf
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? IconUrl { get; set; }

        public virtual ICollection<BookPrivate> BookPrivates { get; set; }

        public string UserEntityId { get; set; }
        public virtual UserEntity UserEntity { get; set; }
    }
}
