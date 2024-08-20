using AutoMapper;
using BookManagerASP.Dto;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace BookManagerASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookPrivateController : Controller
    {
        private readonly IBookPrivateRepository _bookPrivateRepository;
        private readonly IMapper _mapper;
        private readonly IShelfRepository _shelfRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IQuoteRepository _quoteRepository;

        public BookPrivateController(IBookPrivateRepository bookPrivateRepository,
            IMapper mapper, IShelfRepository shelfRepository, IBookRepository bookRepository,
            IQuoteRepository quoteRepository)
        {
            _bookPrivateRepository = bookPrivateRepository;
            _mapper = mapper;
            _shelfRepository = shelfRepository;
            _bookRepository = bookRepository;
            _quoteRepository = quoteRepository;
        }

        [HttpGet("GetBookPrivate")]
        [ProducesResponseType(200, Type = typeof(BookPrivate))]
        [ProducesResponseType(400)]
        public IActionResult GetBookPrivate([FromQuery] int? bookId = null, [FromQuery] string? title = null)
        {
            if (!bookId.HasValue && string.IsNullOrEmpty(title))
                return BadRequest("Either bookId or title must be provided");

            BookPrivate bookPrivate = null;

            if (bookId.HasValue)
            {
                if (!_bookPrivateRepository.BookPrivateExists(bookId))
                    return NotFound();
                bookPrivate = _bookPrivateRepository.GetBookPrivate(bookId);
            }
            else if (title != null)
            {
                bookPrivate = _bookPrivateRepository.GetBookPrivate(title);

                if (bookPrivate == null)
                    return NotFound();
            }
            var bookPrivateDto = _mapper.Map<BookPrivateDto>(bookPrivate);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookPrivateDto);
        }

        [HttpGet("GetBookPrivates/{author}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookPrivate>))]
        [ProducesResponseType(400)]
        public IActionResult GetBookPrivatesByAuthor(string author)
        {
            var bookPrivates = _mapper.Map<List<BookPrivateDto>>(
                _bookPrivateRepository.GetBookPrivatesByBookAuthor(author));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookPrivates);
        }

        [HttpGet("{shelfId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookPrivate>))]
        [ProducesResponseType(400)]
        public IActionResult GetBookPrivatesOnShelf (int shelfId)
        {
            var bookPrivates = _mapper.Map<List<BookPrivateDto>>(
                _bookPrivateRepository.GetBookPrivatesOnShelf(shelfId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookPrivates);
        }

        [HttpGet("BookPrivates/favourites")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookPrivate>))]
        [ProducesResponseType(400)]
        public IActionResult GetFavouriteBookPrivates()
        {
            var bookPrivates = _mapper.Map<List<BookPrivateDto>>(
                _bookPrivateRepository.GetFavouriteBookPrivates());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookPrivates);
        }

        [HttpGet("BookPrivates/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookPrivate>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserBookPrivates(string userId)
        {
            var bookPrivates = _mapper.Map<List<BookPrivateDto>>(
                _bookPrivateRepository.GetUserBookPrivates(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookPrivates);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBookPrivate([FromBody] BookPrivateDto bookPrivateDto,
            [FromQuery] int shelfId, [FromQuery] int bookId/*, [FromQuery] int? quoteId = null*/)
        {
            if (bookPrivateDto == null)
                return BadRequest(ModelState);

            var bookPrivate = _bookPrivateRepository.GetAllBookPrivates()
                .Where(b => b.BookId == bookPrivateDto.BookId)
                .FirstOrDefault();

            if (bookPrivate != null)
            {
                ModelState.AddModelError("", "BookPrivate already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookPrivateMap = _mapper.Map<BookPrivate>(bookPrivateDto);

            //bookPrivateMap.Shelf = _shelfRepository.GetShelf(shelfId);
            bookPrivateMap.Book = _bookRepository.GetBook(bookId);

            if (!_bookPrivateRepository.CreateBookPrivate(bookPrivateMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{bookPrivateId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBookPrivate(int bookPrivateId,
            [FromBody] BookPrivateEditDto bookPrivateDto, [FromQuery] int shelfId)
        {
            if (bookPrivateDto == null)
                return BadRequest(ModelState);

            if (bookPrivateId != bookPrivateDto.Id)
                return BadRequest(ModelState);

            if (!_bookRepository.BookExists(bookPrivateId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var bookPrivate = _bookPrivateRepository.GetBookPrivate(bookPrivateId);
            if (bookPrivate == null)
                return NotFound();

            _mapper.Map(bookPrivateDto, bookPrivate);

            if (!_bookPrivateRepository.UpdateBookPrivate(shelfId, bookPrivate))
            {
                ModelState.AddModelError("", "Something went wrong while updating bookPrivate");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{bookPrivateId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBookPrivate(int bookPrivateId)
        {
            if (!_bookPrivateRepository.BookPrivateExists(bookPrivateId))
                return NotFound();

            var bookPrivate = _bookPrivateRepository.GetBookPrivate(bookPrivateId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_bookPrivateRepository.DeleteBookPrivate(bookPrivate))
            {
                ModelState.AddModelError("", "Sth went wrong while deleting BookPrivate");
            }

            return NoContent();
        }
    }
}
