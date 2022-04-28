using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.DTOs
{
    public class AddRezervareDTO
    {
        public int proprietateId { get; set; }
        public DateTime dataRezervare { get; set; }
    }
}
