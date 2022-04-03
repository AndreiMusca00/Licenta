using BookingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Data;
using Microsoft.EntityFrameworkCore;
namespace BookingAPI.Data
{
    public class AuthRepo : IAuthRepo
    {
        private readonly BookingAPIContext _context;
        public AuthRepo(BookingAPIContext context)
        {
            _context = context;
        }
        public Task<string> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Register(User user, string password)
        {
            if(await UserExists(user.Username))
            {
                return 0;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.User.AnyAsync(x=> x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
