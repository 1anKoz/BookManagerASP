using AutoMapper;
using BookManagerASP.Dto;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using BookManagerASP.Queries;
using BookManagerASP.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookManagerASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelfController : Controller
    {
        private readonly IShelfRepository _shelfRepository;
        private readonly IMapper _mapper;
        private readonly IUserEntityRepository _userEntityRepository;

        public ShelfController(IShelfRepository shelfRepository, IMapper mapper, IUserEntityRepository userEntityRepository)
        {
            _shelfRepository = shelfRepository;
            _mapper = mapper;
            _userEntityRepository = userEntityRepository;
        }

        [HttpGet("GetShelf")]
        [ProducesResponseType(200, Type = typeof(Shelf))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetShelf([FromQuery] string userId,
            [FromQuery] int? shelfId = null, [FromQuery] string? shelfName = null)
        {
            ShelfQuery query = new ShelfQuery
            {
                UserEntityId = userId,
                Name = shelfName,
                Id = shelfId
            };

            if (!await _shelfRepository.ShelfExistsAsync(query))
                return NotFound();

            var shelf = await _shelfRepository.GetShelfAsync(query);
            var shelfDto = _mapper.Map<ShelfDto>(shelf);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shelfDto);
        }

        [HttpGet("UserShelves")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Shelf>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserShelves([FromQuery] string userId)
        {
            var shelves = _mapper.Map<List<ShelfDto>>(await _shelfRepository.GetShelvesAsync(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shelves);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateShelf([FromBody] ShelfDto shelfDto,
            [FromQuery] UserEntityQuery query)
        {
            if (shelfDto == null)
                return BadRequest(ModelState);

            UserEntity user = await _userEntityRepository.GetUser(query);

            var shelves = await _shelfRepository.GetShelvesAsync(user.Id);

            var shelf = shelves.Where(s => s.Name.Trim().ToUpper() == shelfDto.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (shelf != null)
            {
                ModelState.AddModelError("", "Shelf of that name already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var shelfMap = _mapper.Map<Shelf>(shelfDto);

            if (!await _shelfRepository.CreateShelfAsync(shelfMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpPut("ShelfEdit")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateShelf([FromBody] ShelfEditDto shelfDto,
            [FromQuery] string userId, [FromQuery] int? shelfId = null, [FromQuery] string? shelfName = null)
        {
            if (shelfDto == null)
                return BadRequest(ModelState);

            //if (shelfId != shelfDto.Id)
            //    return BadRequest(ModelState);

            ShelfQuery query = new ShelfQuery
            {
                Id = shelfId,
                Name = shelfName,
                UserEntityId = userId
            };

            if (!await _shelfRepository.ShelfExistsAsync(query))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var shelf = await _shelfRepository.GetShelfAsync(query);
            if (shelf == null)
                return NotFound();

            _mapper.Map(shelfDto, shelf);

            if (!await _shelfRepository.UpdateShelfAsync(shelf))
            {
                ModelState.AddModelError("", "Something went wrong while updating shelf");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
