using BookManagerASP.Data;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagerASP.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }

        public ICollection<Review> GetAllReviews()
        {
            return _context.Reviews.ToList();
        }

        public Review GetReview(int reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetUserReviews(string userId)
        {
            return _context.Reviews.Where(r => r.UserEntityId == userId).ToList();
        }
        public ICollection<Review> GetBookReviews(int bookId)
        {
            return _context.Reviews.Where(r => r.BookId == bookId).ToList();
        }


        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }
    }
}
