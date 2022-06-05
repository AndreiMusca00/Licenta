using System;
using System.Collections.Generic;
using System.Text;

namespace BookingApplication.DTOs
{
    public class GetRezervareUserDTO
    {
        public int idRezervare { get; set; }
        public string user { get; set; }
        public int idUser { get; set; }
        public int idProprietate { get; set; }
        public string numeProprietate { get; set; }
        public string judetProprietate { get; set; }
        public string orasProprietate { get; set; }
        public string stradaProprietate { get; set; }
        public string numarProprietate { get; set; }
        public DateTime dataRezervare { get; set; }
    }
}
