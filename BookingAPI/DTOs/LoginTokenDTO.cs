using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.DTOs
{
    public class LoginTokenDTO
    {
        public string Token { get; set; }
        public string Username { get; set; }
    }
}
