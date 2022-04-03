using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Data;
using BookingAPI.Models;
namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _authRepo;
        public AuthController(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }
        //  public async Task<IActionResult> Register(User request)
        // {
        //      await _authRepo.Register(new Models.User { Username=request.Username},request.pas)
        //  }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            var response = await _authRepo.Register(new Models.User { Username = username }, password);
            if (response == 0)
            {
                return BadRequest();
            }
            return Ok(response);
        }

    }
}
