using BookingAPI.Models;
using BookingAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezervariController : ControllerBase
    {
        private readonly IRezervariRepository _userRepository;
        public RezervariController(IRezervariRepository userRepo)
        {
            _userRepository = userRepo;
        }
        [Authorize(Roles ="Basic")]
        [HttpGet]
        public async Task<IActionResult> UserHistory()
        {
           int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var x = _userRepository.UserRezervariHistory(id);
            return Ok(x);
        }

        [Authorize(Roles ="Basic")]
        [HttpPost]
        public async Task<Rezervare> UserAddRezervare(int proprietateId, string dataRezervare)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return await _userRepository.UserAddRezervare(userId, proprietateId,dataRezervare);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public async Task<IActionResult> AdminHistory()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var x = _userRepository.AdminRezervariHistory(id);
            
            return Ok(x);
        }
    }
}
