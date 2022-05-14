using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace BookingApplication.DTOs
{
    public class ProprietateOnePictureDTO
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Judet { get; set; }
        public string Oras { get; set; }
        public string Strada { get; set; }
        public string Numar { get; set; }
        public int Pret { get; set; }
        public ImageSource Imagine { get; set; }
    }
}
