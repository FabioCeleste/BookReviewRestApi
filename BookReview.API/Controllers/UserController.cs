using System;
using System.Threading.Tasks;
using BookReview.Application.Dtos;
using BookReview.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.API.Controllers
{
    [ApiController]
    [Route("[controller]")]    
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetAllUserByName(string name)
        {
            try
            {
                var users = await _userService.GetAllUsersByNameAsync(name);
                if (users == null) NotFound("users not found!");

                return Ok(users);
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetAllUserByEmail(string email)
        {
            try
            {
                var user = await _userService.GetAllUsersByEmailAsync(email);
                if (user == null) return NotFound("user not found!");
                return Ok(user);
            }
            catch (Exception ex)
            {    
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetAllUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null) return NotFound("users not found");
                return Ok(user);
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewUser(UserDto model)
        {
            try
            {
                var user = await _userService.AddUser(model);
                if (user == null)
                {
                    return BadRequest("error to create the user");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {       
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(UserDto model, int id) {
            try
            {
                var user = await _userService.UpdateUser(model, id);
                if (user == null)
                {
                    return BadRequest("error to update the user");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {       
                throw new Exception(ex.Message);
            }
        }

    }
}