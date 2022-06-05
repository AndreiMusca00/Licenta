using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.DTOs
{
    public class AddReviewDTO
    {
        public int Nota { get; set; }
        public int ProprietateId { get; set; }
        public string Text { get; set; }
    }
}
