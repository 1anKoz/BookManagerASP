using BookManagerASP.Models;

namespace BookManagerASP.Dto
{
    public class BookPrivateDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        //public virtual ICollection<Quote>? Quotes { get; set; }
        public byte? Rating { get; set; }
        public bool IsFavourite { get; set; }

        public int BookId { get; set; }

        //public int ShelfId { get; set; }

        public string UserEntityId { get; set; }
    }
}
