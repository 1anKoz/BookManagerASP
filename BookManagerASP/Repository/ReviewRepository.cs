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

        //public ICollection<Review> GetBookReviews(int bookId)
        //{
        //    var bookUR = _context.BookUserReviews.Where(bur => bur.BookId == bookId).Include(r => r.Review).ToList();
        //    var reviews = bookUR.Select(r => r.Review).ToList();
        //    //var reviews = new List<Review>();
        //    //foreach (var bookReview in bookUR)
        //    //{
        //    //    var review = bookReview.Review;
        //    //    reviews.Add(review);
        //    //}
        //    return reviews;
        //}

        public Review GetReview(int reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetUserReviews(string userId)
        {
            throw new NotImplementedException();
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
    }
}
