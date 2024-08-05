namespace BookManagerASP.Models
{
    public class BookUserReview
    {
        public string UserEntityId { get; set; }
        public UserEntity UserEntity { get; set; }

        public int ReviewId { get; set; }
        public Review Review { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
