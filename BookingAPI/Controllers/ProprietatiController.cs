using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Managers;
using BookingAPI.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BookingAPI.DTOs;

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
    
        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<IActionResult> GetProprietati(string filtru,int? filtruPretMax, int? filtruPretMin)
        {
            int idUser = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            List<Proprietate> pr=new List<Proprietate>();
            bool stringFilter = false;
            if (filtru != null)
                stringFilter = true;
            switch ((stringFilter, filtruPretMax, filtruPretMin))
            {
                case (false, >0, >0):
                    pr = await _proprietatiManager.GetProprietati(idUser, null, filtruPretMax, filtruPretMin);
                    break;
                case (true, > 0, > 0):
                    pr = await _proprietatiManager.GetProprietati(idUser, filtru, filtruPretMax, filtruPretMin);
                    break;
                case (true, null, null):
                    pr = await _proprietatiManager.GetProprietati(idUser, filtru, null, null);
                    break;
                default:
                    pr = await _proprietatiManager.GetProprietati(idUser, null, null, null);
                    break;
            }
            if (pr != null)
                return Ok(pr);
            else
                return BadRequest();
            
        }
        [Authorize(Roles ="Admin")]
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
        //Partea de get / add proprietati pe baza unui user !!
        /*
         * 
         * 
         * 
         * 
         */
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> GetPropByUserId()
        {
            int id = 0;
            id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            
            return Ok(await _proprietatiManager.GetPropByUserId(id));
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<Proprietate> AddProprietate(AddProprietateDTO prop)
        {
            int id = 0;
            id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return await _proprietatiManager.AddProprietate(prop, id);
           
        }

        [AllowAnonymous]
        [HttpGet("proprietateId")]
        public async Task<IActionResult> GetProprietate(int proprietateId)
        {
            var pr = await _proprietatiManager.GetProprietate(proprietateId);
            if (pr != null)
            {
                return Ok(pr);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
