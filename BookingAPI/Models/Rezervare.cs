﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Models
{
    public class Rezervare
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        public User User { get; set; }

        [ForeignKey("Proprietate")]
        public int proprietateId { get; set; }
        public Proprietate Proprietate { get; set; }
    }
}
