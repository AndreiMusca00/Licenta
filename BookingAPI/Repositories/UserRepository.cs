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
using Newtonsoft;
using BookingAPI.DTOs;

namespace BookingAPI.Repositories
{
    public interface IUserRepository
    {
        Task<int> Register(User user, string password);
        Task<bool> UserExists(string username);
        Task<LoginTokenDTO> Login(string username, string password);
        Task<List<User>> GetUsers();
        Task<int> Delete(int id);
        Task<string> ChangePassword(int userId, string password);
        Task<string> UpdateUserDetails(UpdateUserDTO user, int id);
        Task<UpdateUserDTO> GetConnectedUser(int userId);
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
        public async Task<LoginTokenDTO> Login(string username,string password)
        {
            User user = await _context.User.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
            LoginTokenDTO token = new LoginTokenDTO();
            if (user == null)
            {
                token.Token = "User not found";
                return token;
            }else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                token.Token = "Wrong password";
                return token;
            }else
            {
                
                token.Token = CreateToken(user);
                token.Username = username;
                token.Role = user.Role;
                return token;
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
                new Claim(ClaimTypes.GivenName,user.Prenume),
                new Claim(ClaimTypes.Role,user.Role)
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
        public async Task<string> ChangePassword(int userId, string password)
        {
            try { 
                User userul = _context.User.Find(userId);
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                userul.PasswordHash = passwordHash;
                userul.PasswordSalt = passwordSalt;
                _context.User.Update(userul);
                await _context.SaveChangesAsync();
                return "ok";
            }
            catch (Exception)
            {
                return "fail";
            }
        }
        public async Task<string> UpdateUserDetails(UpdateUserDTO user, int userId)
        {
            try
            {
                User userul = _context.User.Find(userId);
                userul.Nume = user.Nume;
                userul.Mail = user.Mail;
                userul.Prenume = user.Prenume;
                userul.Telefon = user.Telefon;
                _context.User.Update(userul);
                await _context.SaveChangesAsync();
                return "ok";
            }
            catch (Exception)
            {
                return "fail";
            }
        }

        public async Task<UpdateUserDTO> GetConnectedUser(int userId)
        {
            try
            {
                User userul = await _context.User.FindAsync(userId);
                UpdateUserDTO connectedUser = new UpdateUserDTO() {
                    Nume = userul.Nume,
                    Prenume=userul.Prenume,
                    Mail=userul.Mail,
                    Telefon=userul.Telefon
                };
                return connectedUser;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
