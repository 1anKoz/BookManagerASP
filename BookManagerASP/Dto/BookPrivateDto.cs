using BookManagerASP.Models;

namespace BookManagerASP.Dto
{
    public class BookPrivateDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Quote>? Quotes { get; set; }
        public int? Rating { get; set; }
        public bool IsFavourite { get; set; }

        public int BookId { get; set; }

        public int ShelfId { get; set; }
    }
}
