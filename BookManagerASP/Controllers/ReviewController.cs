using AutoMapper;
using BookManagerASP.Dto;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using BookManagerASP.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookManagerASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        public readonly IReviewRepository _reviewRepository;
        public readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly IUserEntityRepository _userEntityRepository;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper,
            IBookRepository bookRepository, IUserEntityRepository userEntityRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _bookRepository = bookRepository;
            _userEntityRepository = userEntityRepository;
        }

        [HttpGet("AllReviews")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetAllReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetAllReviews());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{bookId}/GetBookReviews")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetBookReviews(int bookId)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetBookReviews(bookId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }

            var review = _reviewRepository.GetReview(reviewId);
            var reviewDto = _mapper.Map<ReviewDto>(review);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewDto);
        }


        //[HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult CreateReview([FromBody] ReviewDto reviewDto,
        //    [FromQuery] int userId, [FromQuery] int bookId)
        //{
        //    if (reviewDto == null)
        //        return BadRequest(ModelState);

        //    var review = _reviewRepository.GetAllReviews()
        //        .Where(r => r.Id == reviewDto.Id).FirstOrDefault();

        //    if (review != null)
        //    {
        //        ModelState.AddModelError("", "You have already reviewed this book");
        //        return StatusCode(422, ModelState);
        //    }

        //    if(!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var reviewMap = _mapper.Map<Review>(reviewDto);
        //    reviewMap.

        //    return Ok("Successfully created");
        //}
    }
}
