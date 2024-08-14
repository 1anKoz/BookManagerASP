using BookManagerASP.Models;

namespace BookManagerASP.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Content { get; set; }

        public int BookId { get; set; }

        public string UserEntityId { get; set; }
    }
}
