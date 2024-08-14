using BookManagerASP.Models;

namespace BookManagerASP.Interfaces
{
    public interface IReviewRepository
    {
        bool ReviewExists(int reviewId);

        Review GetReview(int reviewId);
        ICollection<Review> GetAllReviews();
        ICollection<Review> GetUserReviews(string userId);
        ICollection<Review> GetBookReviews(int bookId);

        bool CreateReview(Review review);
        bool Save();
    }
}
