﻿using AutoMapper;
using BookManagerASP.Dto;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using Microsoft.AspNetCore.Mvc;

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

            bookPrivateMap.Shelf = _shelfRepository.GetShelf(shelfId);
            bookPrivateMap.Book = _bookRepository.GetBook(bookId);
            //bookPrivateMap.Quotes.Add(_quoteRepository.GetQuote(quoteId));


            if (!_bookPrivateRepository.CreateBookPrivate(bookPrivateMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
