using BookManagerASP.Models;

namespace BookManagerASP.Dto
{
    public class BookPrivateEditDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte? Rating { get; set; }
        public bool IsFavourite { get; set; }
        public virtual ICollection<Shelf> Shelves { get; set; }
    }
}
