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
using BookingAPI.Managers;
using BookingAPI.DTOs;
namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezervariController : ControllerBase
    {
        
        private readonly IRezervariManager _rezervariManager;
        public RezervariController(IRezervariManager rezervariManager)
        {
            _rezervariManager = rezervariManager;
        }
        [Authorize(Roles ="Basic,Admin")]
        [HttpGet]
        public async Task<IActionResult> UserHistory()
        {
           int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var x = _rezervariManager.UserRezervariHistory(id);
            return Ok(x);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("proprietate")]
        public async Task<IActionResult> GetRezervariProprietate(int proprietateId)
        {
            var x = _rezervariManager.GetRezervariProrietate(proprietateId);
            return Ok(x);
        }

        [Authorize(Roles ="Basic, Admin")]
        [HttpPost]
        public async Task<IActionResult> UserAddRezervare(AddRezervareDTO rezervare)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            string response= await _rezervariManager.UserAddRezervare(userId, rezervare.proprietateId, rezervare.dataRezervare);
            if (response == "Succes")
            {
                return Ok(response);
            }else
            {
                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public async Task<IActionResult> AdminHistory()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var x = _rezervariManager.AdminRezervariHistory(id);
            return Ok(x);
        }
        [Authorize(Roles = "Admin,Basic")]
        [HttpGet("hasReview")]
        public async Task<IActionResult> CheckUserHasReservation(int proprietateId)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var x = _rezervariManager.HasReview(id,proprietateId);
            if (x == true)
                return Ok();
            else
                return BadRequest();
        }
    }
}
