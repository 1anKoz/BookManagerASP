using BookManagerASP.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace BookManagerASP.Queries
{
    public class BookQuery
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Isbn { get; set; }
        public Genre? Genre { get; set; }
        //public float Rating { get; init; }
    }
}
