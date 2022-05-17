using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.DTOs
{
    public class UpdateUserDTO
    {
        public string Mail { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Telefon { get; set; }    
    }
}
