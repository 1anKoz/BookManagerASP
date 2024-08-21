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
                .Where(b => b.Isbn.Trim() == bookDto.Isbn.Trim())
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
        public IActionResult UpdateBook([FromBody] BookDto bookDto,
            [FromQuery] string? Isbn = null, [FromQuery] int? bookId = null)
        {
            BookQuery query = new BookQuery();
            query.Isbn = Isbn;
            query.Id = bookId;

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
