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
    public class UserEntityController : ControllerBase
    {
        private readonly IUserEntityRepository _userEntityRepository;
        private readonly IMapper _mapper;

        public UserEntityController(IUserEntityRepository userEntityRepository, IMapper mapper)
        {
            _userEntityRepository = userEntityRepository;
            _mapper = mapper;
        }

        [HttpGet("{email}")]
        [ProducesResponseType(200, Type = typeof(UserEntity))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserByEmailAsync(string email)
        {
            if (!_userEntityRepository.UserExists(email))
                return NotFound();

            var userDto = _mapper.Map<UserEntityDto>(await _userEntityRepository.GetUserByEmailAsync(email));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userDto);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserEntityDto userCreate)
        {
            if (userCreate == null)
                return BadRequest("User data is null");

            var userMap = _mapper.Map<UserEntity>(userCreate);

            var result = await _userEntityRepository.CreateUserAsync(userMap, userCreate.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok("User created successfully");
        }
    }
}
