namespace BookManagerASP.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }

        public ICollection<BookUserReview> BookUserReviews { get; set; }
    }
}
