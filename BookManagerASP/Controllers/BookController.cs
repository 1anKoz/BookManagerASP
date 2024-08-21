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
        public async Task<IActionResult> GetBook([FromQuery] BookQuery query)
        {
            if (!await _bookRepository.BookExistsAsync(query))
                return NotFound();

            var book = _mapper.Map<BookDto>(await _bookRepository.GetBookAsync(query));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(book);
        }

        [HttpGet("Get")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBooks([FromQuery] BookQuery query)
        {
            var books = _mapper.Map<List<BookDto>>(await _bookRepository.GetBooksAsync(query));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(books);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateBook([FromBody] BookDto bookDto)
        {
            if(bookDto == null)
                return BadRequest(ModelState);

            var books = await _bookRepository.GetBooksAsync(new BookQuery());

            var book = books.Where(b => b.Isbn.Trim() == bookDto.Isbn.Trim()).FirstOrDefault();

            if(book != null)
            {
                ModelState.AddModelError("", "Book already exists");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookMap = _mapper.Map<Book>(bookDto);

            if(!await _bookRepository.CreateBookAsync(bookMap))
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
        public async Task<IActionResult> UpdateBook([FromBody] BookDto bookDto,
            [FromQuery] string? Isbn = null, [FromQuery] int? bookId = null)
        {
            BookQuery query = new BookQuery();
            query.Isbn = Isbn;
            query.Id = bookId;

            if(bookDto == null)
                return BadRequest(ModelState);

            if(query.Id != bookDto.Id)
                return BadRequest(ModelState);

            if(!await _bookRepository.BookExistsAsync(query))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var bookMap = _mapper.Map<Book>(bookDto);

            if(!await _bookRepository.UpdateBookAsync(bookMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating book");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
