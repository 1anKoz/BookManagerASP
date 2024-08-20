using AutoMapper;
using BookManagerASP.Dto;
using BookManagerASP.Interfaces;
using BookManagerASP.Models;
using BookManagerASP.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpGet("GetUser")]
        [ProducesResponseType(200, Type = typeof(UserEntity))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserAsync(string userNameOrEmail)
        {
            if (!_userEntityRepository.UserExists(userNameOrEmail))
            {
                return NotFound();
            }

            var userEntity = await _userEntityRepository.GetUser(userNameOrEmail);
            var userDto = _mapper.Map<UserEntityDto>(userEntity);

            if (!ModelState.IsValid)
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

        [HttpPut("{userName}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUser(string userName, UserEntityDto userDto)
        {
            if (userDto == null)
                return BadRequest(ModelState);

            if (userName != userDto.UserName)
                return BadRequest(ModelState);

            var user = await _userEntityRepository.GetUser(userName);

            if (user == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            _mapper.Map(userDto, user);

            var result = await _userEntityRepository.UpdateUserAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
