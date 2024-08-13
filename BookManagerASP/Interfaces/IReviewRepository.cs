﻿using BookManagerASP.Models;

namespace BookManagerASP.Interfaces
{
    public interface IReviewRepository
    {
        bool ReviewExists(int reviewId);

        Review GetReview(int reviewId);
        ICollection<Review> GetAllReviews();
        ICollection<Review> GetBookReviews(int bookId);
    }
}
