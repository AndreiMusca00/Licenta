﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookingApplication.Models
{
   public  class Proprietate
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Judet { get; set; }
        public string Oras { get; set; }
        public string Strada { get; set; }
        public string Numar { get; set; }
        public int Pret { get; set; }
    }
}
