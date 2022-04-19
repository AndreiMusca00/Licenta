using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookingAPI.Data;
using BookingAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace BookingAPI.Repositories
{
    public interface IUserRepository
    {
        Task<int> Register(User user, string password);
        Task<bool> UserExists(string username);
        Task<string> Login(string username, string password);
        Task<List<User>> GetUsers();
        Task<int> Delete(int id);
    }
    public class UserRepository : IUserRepository
    {
        private readonly BookingAPIContext _context;
        private readonly IConfiguration _configuration;
        public UserRepository(BookingAPIContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        //Delete user - incercare 
        public async Task<int> Delete(int id)
        {
            if (await _context.User.AnyAsync(x => x.Id == id))
            {
                var del = await _context.User.FindAsync(id);
                _context.User.Remove(del);
                await _context.SaveChangesAsync();
            }
            return id;
        }
        //Register with pass creation 
        public async Task<int> Register(User user, string password)
        {
            if(await UserExists(user.Username))
            {
                return -1;
            }  
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public async Task<bool> UserExists(string username)
        {
            if (await _context.User.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        //Login with pass verification 
        public async Task<string> Login(string username,string password)
        {
            User user = await _context.User.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
            if(user == null)
            {
                return "User not found";
            }else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return "Wrong Password";
            }else
            {
                return CreateToken(user);
            }
             
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash,byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0;i<computedHash.Length;i++)
                {
                    if(computedHash[i]!=passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        // JXT TOKENS 
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                //incercare
                new Claim(ClaimTypes.Email,user.Mail),
                new Claim(ClaimTypes.Surname,user.Nume),
                new Claim(ClaimTypes.GivenName,user.Prenume)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
                );

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor 
            { 
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        //get users 
        public async Task<List<User>> GetUsers()
        {
            return await _context.User.ToListAsync();
        }
    }
}
