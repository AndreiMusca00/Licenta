using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.DTOs
{
    public class RezervariProprietateDTO
    {
        public int idRezervare { get; set; }
        public string nume { get; set; }
        public string prenume { get; set; }
        public string mail { get; set; }
        public string telefon { get; set; }
        public DateTime dataRezervare { get; set; }
    }
}
