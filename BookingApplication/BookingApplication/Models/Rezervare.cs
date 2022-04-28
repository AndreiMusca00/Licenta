using System;
using System.Collections.Generic;
using System.Text;

namespace BookingApplication.Models
{
    public class Rezervare
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int userId { get; set; }
        public int proprietateId { get; set; }
    }
}
