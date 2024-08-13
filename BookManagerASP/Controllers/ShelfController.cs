using AutoMapper;
using BookManagerASP.Dto;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManagerASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelfController : Controller
    {
        private readonly IShelfRepository _shelfRepository;
        private readonly IMapper _mapper;

        public ShelfController(IShelfRepository shelfRepository, IMapper mapper)
        {
            _shelfRepository = shelfRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateShelf([FromBody] ShelfDto shelfDto)
        {
            if (shelfDto == null)
                return BadRequest(ModelState);

            var shelf = _shelfRepository.GetAllShelves()
                .Where(s => s.Name.Trim().ToUpper() == shelfDto.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (shelf != null)
            {
                ModelState.AddModelError("", "Shelf of that name already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var shelfMap = _mapper.Map<Shelf>(shelfDto);

            if (!_shelfRepository.CreateShelf(shelfMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
