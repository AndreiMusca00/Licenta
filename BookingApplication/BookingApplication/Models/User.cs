using System;
using System.Collections.Generic;
using System.Text;

namespace BookingApplication.Models
{
   public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Mail { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
