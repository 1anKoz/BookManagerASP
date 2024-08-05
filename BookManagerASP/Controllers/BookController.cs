using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;

namespace BookManagerASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        public IActionResult GetBooks()
        {
            var books = _bookRepository.GetBooks();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(books);
        }
    }
}
