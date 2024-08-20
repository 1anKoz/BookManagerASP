using BookManagerASP.Models;
using System.ComponentModel.DataAnnotations;

namespace BookManagerASP.Queries
{
    public class BookPrivateQuery
    {
        public int? Id { get; init; }
        public byte? Rating { get; init; }
        public bool? IsFavourite { get; init; }
        public virtual ICollection<Shelf>? Shelves { get; set; }

        public int? BookId { get; init; }
        public string? UserEntityId { get; init; }
    }
}
