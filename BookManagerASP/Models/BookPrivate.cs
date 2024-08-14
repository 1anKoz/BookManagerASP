namespace BookManagerASP.Models
{
    public class BookPrivate
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Quote>? Quotes { get; set; }
        public int? Rating { get; set; }
        public bool IsFavourite { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public int ShelfId { get; set; }
        public virtual Shelf Shelf { get; set; }
    }
}
