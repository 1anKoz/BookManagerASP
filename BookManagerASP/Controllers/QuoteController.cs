using AutoMapper;
using BookManagerASP.Dto;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using BookManagerASP.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookManagerASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : Controller
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IMapper _mapper;
        public QuoteController(IQuoteRepository quoteRepository, IMapper mapper)
        {
            _quoteRepository = quoteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Quote>))]
        public IActionResult GetQuotes()
        {
            var quotes = _mapper.Map<List<QuoteDto>>(_quoteRepository.GetQuotes());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(quotes);
        }

        [HttpGet("/favourites")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Quote>))]
        [ProducesResponseType(400)]
        public IActionResult GetFavouriteQuotes()
        {
            var quotes = _mapper.Map<List<QuoteDto>>(_quoteRepository.GetFavouriteQuotes());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(quotes);
        }

        [HttpGet("Quote/user/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Quote>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserQuotes(string userId)
        {
            var quotes = _mapper.Map<List<QuoteDto>>(
                _quoteRepository.GetUserQuotes(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(quotes);
        }

        [HttpGet("bp/{bookPrivateId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Quote>))]
        [ProducesResponseType(400)]
        public IActionResult GetBookPrivateQuotes(int bookPrivateId)
        {
            var quotes = _mapper.Map<List<QuoteDto>>(
                _quoteRepository.GetBookPrivateQuotes(bookPrivateId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(quotes);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateQuote([FromBody] QuoteDto quoteCreate)
        {
            if (quoteCreate == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var quoteMap = _mapper.Map<Quote>(quoteCreate);

            if (!_quoteRepository.CreateQuote(quoteMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(quoteMap);
        }


        [HttpPut("{quoteId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateQuote(int quoteId, [FromBody] QuoteDto quoteDto,
            [FromQuery] int bookPrivateId)
        {
            if (quoteDto == null)
                return BadRequest(ModelState);

            if (quoteId != quoteDto.Id)
                return BadRequest(ModelState);

            if (!_quoteRepository.QuoteExists(quoteId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var quoteMap = _mapper.Map<Quote>(quoteDto);

            if(!_quoteRepository.UpdateQuote(quoteMap, bookPrivateId))
            {
                ModelState.AddModelError("", "Something went wrong while updating quote");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
