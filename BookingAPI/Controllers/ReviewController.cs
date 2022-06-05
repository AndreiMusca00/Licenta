using BookingAPI.DTOs;
using BookingAPI.Managers;
using BookingAPI.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewManager _reviewManager;
        public ReviewController(IReviewManager reviewManager)
        {
            _reviewManager = reviewManager;
        }

        [Authorize(Roles = "Basic, Admin")]
        [HttpPost]
        public async Task<IActionResult> UserAddReview(AddReviewDTO review)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var response = await _reviewManager.AddReview(userId, review.Nota, review.Text,review.ProprietateId);
            if (response == "Succes")
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Basic, Admin")]
        [HttpGet]
        public async Task<IActionResult> GetReviewsProprietate(int proprietateId)
        {
            List<GetReviewsProprietateDTO> reviews = new List<GetReviewsProprietateDTO>();
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            reviews = _reviewManager.GetReviewsProprietate(proprietateId, userId);
            return Ok(reviews);
        }
    }
}
