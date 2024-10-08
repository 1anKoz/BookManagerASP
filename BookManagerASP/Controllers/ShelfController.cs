﻿using AutoMapper;
using BookManagerASP.Dto;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;
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

        public ShelfController(IShelfRepository shelfRepository, IMapper mapper)
        {
            _shelfRepository = shelfRepository;
            _mapper = mapper;
        }

        [HttpGet("AllShelves")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Shelf>))]
        public IActionResult GetAllShelves()
        {
            var shelves = _mapper.Map<List<ShelfDto>>(_shelfRepository.GetAllShelves());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shelves);
        }

        [HttpGet("/{shelfId}")]
        [ProducesResponseType(200, Type = typeof(Shelf))]
        [ProducesResponseType(400)]
        public IActionResult GetShelf(int shelfId)
        {
            if (!_shelfRepository.ShelfExists(shelfId))
                return NotFound();

            var shelf = _shelfRepository.GetShelf(shelfId);
            var shelfDto = _mapper.Map<ShelfDto>(shelf);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shelfDto);
        }

        [HttpGet("/library/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Shelf>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserShelves(string userId)
        {
            var shelves = _mapper.Map<List<ShelfDto>>(_shelfRepository.GetUserShelves(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shelves);
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


        [HttpPut("{shelfId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int shelfId, [FromBody] ShelfEditDto shelfDto)
        {
            if (shelfDto == null)
                return BadRequest(ModelState);

            if (shelfId != shelfDto.Id)
                return BadRequest(ModelState);

            if (!_shelfRepository.ShelfExists(shelfId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var shelf = _shelfRepository.GetShelf(shelfId);
            if (shelf == null)
                return NotFound();

            _mapper.Map(shelfDto, shelf);

            if (!_shelfRepository.UpdateShelf(shelf))
            {
                ModelState.AddModelError("", "Something went wrong while updating shelf");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
