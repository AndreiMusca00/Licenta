using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingAPI.Models;
namespace BookingAPI.Data
{
    public interface IAuthRepo
    {
        Task<int> Register(User user, string password);
        Task<string> Login(string username, string password);
        Task<bool> UserExists(string username);
        
    }
}
