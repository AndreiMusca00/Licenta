using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingAPI.Data;
using BookingAPI.Models;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BookingAPIContext _context;

        public UsersController(BookingAPIContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        
        // GET: api/Users/5
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
           
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }
        
        // GET: api?users dupa member username
        
        [HttpGet("login/{username}")]
        public async Task<ActionResult<Users>> GetUsersUserName(string username,string parola)
        {
            var userCaut = new Users();
            try
            {
                var user =  await _context.Users.Where(userCaut => userCaut.Username == username).FirstAsync();
                if (user == null)
                {
                    return NotFound();
                }
                else if(user.Password!=parola)
                { 
                    return BadRequest();
                }
                else
                {
                    return Ok(user);
                }
            }catch(Exception ex)
            {
                return BadRequest();
            }
            
        }
        

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.ID)
            {
                return BadRequest();
            }
            
            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.ID }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("del/{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
