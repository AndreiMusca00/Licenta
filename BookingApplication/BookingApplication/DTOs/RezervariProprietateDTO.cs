using System;
using System.Collections.Generic;
using System.Text;

namespace BookingApplication.DTOs
{
    public class RezervariProprietateDTO
    {
        public string nume { get; set; }
        public string prenume { get; set; }
        public string mail { get; set; }
        public string telefon { get; set; }
        public DateTime dataRezervare { get; set; }
    }
}
