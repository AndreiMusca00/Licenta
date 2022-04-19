using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Managers;
using BookingAPI.Models;
namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProprietatiController : ControllerBase
    {
        private IProprietatiManager _proprietatiManager;
        public ProprietatiController(IProprietatiManager manager)
        {
            _proprietatiManager = manager;
        }
        [HttpPost]
        public async Task<Proprietate> AddProprietate(Proprietate prop)
        {
            return await _proprietatiManager.AddProprietate(prop);
        }

        [HttpGet]
        public async Task<IActionResult> GetProprietati()
        {
            var pr = await _proprietatiManager.GetProprietati();
            if (pr != null)
            {
                return Ok(pr);
            }else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProprietate(int id)
        {
            var pr = await _proprietatiManager.DeleteProprietate(id);
            if (pr != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
