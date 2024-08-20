using BookManagerASP.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace BookManagerASP.Queries
{
    public class BookQuery
    {
        public int? Id { get; init; }
        public string? Title { get; init; }
        public string? Author { get; init; }
        public string? Isbn { get; init; }
        public Genre? Genre { get; init; }
        //public float Rating { get; init; }
    }
}
