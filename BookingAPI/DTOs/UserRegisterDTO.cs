using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.DTOs
{
    public class UserRegisterDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }

    }
}
