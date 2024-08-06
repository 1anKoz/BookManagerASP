using AutoMapper;
using BookManagerASP.Dto;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;
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


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateQuote([FromBody] QuoteDto quoteCreate)
        {
            if (quoteCreate == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var quoteMap = _mapper.Map<Quote>(quoteCreate);

            if(!_quoteRepository.CreateQuote(quoteMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(quoteMap);
        }
    }
}
