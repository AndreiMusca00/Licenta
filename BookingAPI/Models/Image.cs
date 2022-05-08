using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Models
{
    public class Image
    {
        public int id { get; set; }

        [ForeignKey("Proprietate")]
        public int idProprietate { get; set; }
        public Proprietate proprietate { get; set; }

        public string imagePath { get; set; }
    }
}
