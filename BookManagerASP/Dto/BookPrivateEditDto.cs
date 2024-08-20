using BookManagerASP.Models;

namespace BookManagerASP.Dto
{
    public class BookPrivateEditDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? Rating { get; set; }
        public bool IsFavourite { get; set; }

        public int ShelfId { get; set; }
    }
}
