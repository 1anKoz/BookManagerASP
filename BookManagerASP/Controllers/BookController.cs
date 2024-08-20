using AutoMapper;
using BookManagerASP.Dto;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using BookManagerASP.Queries;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace BookManagerASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        //[HttpGet("AllBooks")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        //public IActionResult GetBooks()
        //{
        //    var books = _mapper.Map<List<BookDto>>(_bookRepository.GetBooks());

        //    if(!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(books);
        //}

        //[HttpGet("{author}/GetBooks")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        //[ProducesResponseType(400)]
        //public IActionResult GetBooksByAuthor(string author)
        //{
        //    var books = _mapper.Map<List<BookDto>>(_bookRepository.GetBooksByAuthor(author));

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(books);
        //}

        ////później będzie można zostawić tutaj szukanie po Id, a w metodzie GetBooksByAuthor
        ////umożliwić szukanie po tytułach (umożliwi to być może szukanie i proponowanie książek
        ////nawet jeśli niecały tytuł będzie wpisany). Oczywiście, wówczas również trzeba będzie
        ////zwracać LISTĘ książek, których tytuły pasują do wpisywanego.
        //[HttpGet("GetBook")]
        //[ProducesResponseType(200, Type = typeof(Book))]
        //[ProducesResponseType(400)]
        //public IActionResult GetBook([FromQuery] int? bookId = null, [FromQuery] string? title = null)
        //{
        //    if (!bookId.HasValue && string.IsNullOrEmpty(title))
        //    {
        //        return BadRequest("Either bookId or title must be provided.");
        //    }

        //    Book book = null;

        //    if (bookId.HasValue)
        //    {
        //        if (!_bookRepository.BookExists(bookId.Value))
        //            return NotFound();

        //        book = _bookRepository.GetBook(bookId.Value);
        //    }
        //    else if (!string.IsNullOrEmpty(title))
        //    {
        //        book = _bookRepository.GetBook(title);

        //        if (book == null)
        //            return NotFound();
        //    }

        //    var bookDto = _mapper.Map<BookDto>(book);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(bookDto);
        //}

        //[HttpGet("{bookId}/rating")]
        //[ProducesResponseType(200, Type = typeof(int))]
        //[ProducesResponseType(400)]
        //public IActionResult GetBookRating(int bookId)
        //{
        //    if (!_bookRepository.BookExists(bookId))
        //        return NotFound();

        //    var rating = _bookRepository.GetBookRating(bookId);

        //    if(!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(rating);
        //}

        [HttpGet("GetBook")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetBook([FromQuery] BookQuery query)
        {
            if (!_bookRepository.BookExists(query))
                return NotFound();

            var book = _mapper.Map<BookDto>(_bookRepository.GetBook(query));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(book);
        }

        [HttpGet("Get")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public IActionResult GetBooks([FromQuery] BookQuery query)
        {
            var books = _mapper.Map<List<BookDto>>(_bookRepository.GetBooks(query));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(books);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBook([FromBody] BookDto bookDto)
        {
            if(bookDto == null)
                return BadRequest(ModelState);

            var book = _bookRepository.GetBooks(new BookQuery())
                .Where(b => b.Title.Trim().ToUpper() == bookDto.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(book != null)
            {
                ModelState.AddModelError("", "Book already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookMap = _mapper.Map<Book>(bookDto);

            if(!_bookRepository.CreateBook(bookMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpPut("BookEdit")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBook([FromQuery] BookQuery query, [FromBody] BookDto bookDto)
        {
            if(bookDto == null)
                return BadRequest(ModelState);

            if(query.Id != bookDto.Id)
                return BadRequest(ModelState);

            if(!_bookRepository.BookExists(query))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var bookMap = _mapper.Map<Book>(bookDto);

            if(!_bookRepository.UpdateBook(bookMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating book");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
