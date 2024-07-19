namespace BookManagerASP.Models
{
    public class BookPrivate
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<Quote>? Quotes { get; set; }
        public bool IsFavourite { get; set; }

        public virtual Book Book { get; set; }
        public int BookId { get; set; }

        public int UserEntityId { get; set; }
        public UserEntity UserEntity { get; set; }
    }
}
