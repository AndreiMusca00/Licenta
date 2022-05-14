using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin;
using Xamarin.Forms;
namespace BookingApplication.ViewModels
{
    public class WelcomePageVM
    {
        public WelcomePageVM()
        {
            Afisari = GetAfisari();
        }
        public List<TextDeAfisat> Afisari { get; set; }

        public List<TextDeAfisat> GetAfisari()
        {
            return new List<TextDeAfisat> 
            {
            new TextDeAfisat { Titlu = "Rezerva locul dorit instant", Detalii = "In cadrul aplicatiei poti rezerva foarte usor " },
            new TextDeAfisat { Titlu = "Alege din mii de proprietati", Detalii = "Selecteaza proprietatea dorita din multitudinea de optiuni" },
            new TextDeAfisat { Titlu = "Rezerva din calitate de proprietar", Detalii = "Rezerva un loc de relaxare pentru o pauza bine meritata" }
            };
        }
       
    }

    public class TextDeAfisat
    {
        public string Titlu { get; set; }
        public string Detalii { get; set; }

    }

}
