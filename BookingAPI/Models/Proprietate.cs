using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Models
{
    public class Proprietate
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Judet { get; set; }
        public string Oras { get; set; }
        public string Strada { get; set; }
        public string Numar { get; set; }
        public int Pret { get; set; }

        
        public int userId { get; set; }
    }
}
