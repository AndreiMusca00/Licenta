using System;
using System.Collections.Generic;
using System.Text;
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
            new TextDeAfisat { Titlu = "Rezerva locul dorit instant", Detalii = "In cadrul aplii poti rezerva foarte usor " },
            new TextDeAfisat { Titlu = "Alege din mii de proprietati", Detalii = "In cadrul aplicatiei poti rezerva foarte usor " },
            new TextDeAfisat { Titlu = "Urmareste recenziile in timp real", Detalii = "plicatiei poti rezerva foarte usor " }
            };
        }
       
    }

    public class TextDeAfisat
    {
        public string Titlu { get; set; }
        public string Detalii { get; set; }

    }

}
