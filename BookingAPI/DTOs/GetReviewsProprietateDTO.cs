using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.DTOs
{
    public class GetReviewsProprietateDTO
    {
        public string Nume { get; set; }
        public string Text { get; set; }
        public int Nota { get; set; }
    }
}
