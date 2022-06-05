using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Repositories;
using BookingAPI.DTOs;
using BookingAPI.Models;
using System.Security.Claims;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepo)
        {
            _userRepository = userRepo;
        }
        [HttpDelete("{id}")]
        public async Task<int> DeleteUser([FromRoute] int id)
        {
            return await _userRepository.Delete(id);
        }

        [HttpGet]
        [Route("SeeUsers")]
        public async Task<List<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO request)
        {
            int response = await _userRepository.Register(
                new User { Username = request.Username, Mail=request.Mail, Nume=request.Nume, Prenume=request.Prenume, Telefon=request.Telefon}, request.Password);
            if (response != -1)
            { 
                return Ok(response);
            }else
            {
                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("password")]
        public async Task<IActionResult> ChangePassword(UserLoginDTO user)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            string response = await _userRepository.ChangePassword(id, user.Password);
            if (response == "ok")
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("user")]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDTO user)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            string response = await _userRepository.UpdateUserDetails(user, id);
            if (response == "ok")
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            LoginTokenDTO token = await _userRepository.Login(request.Username,request.Password);
            if (token.Token.Contains("not"))
            {
                return BadRequest(token);
            }
            else
            {
                return Ok(token);
            }
            /*
            string response = await _userRepository.Login(request.Username, request.Password);
            if (response.Contains("not"))
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }*/
        }

        [HttpGet]
        [Route("ConnectedUser")]
        public async Task<IActionResult> GetConnectedUser()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var response = await _userRepository.GetConnectedUser(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}
