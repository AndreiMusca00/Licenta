using BookingApplication.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookingApplication.ViewModels
{
    public class RezervariProprietateVM
    {
        public List<RezervariProprietateDTO> rezervari = new List<RezervariProprietateDTO>();
        public async Task<List<RezervariProprietateDTO>> GetProprietatiFromDatabase(int proprietateId)
        {
            rezervari = await App.Database.GetRezervariProprietate(proprietateId);
            return rezervari;
        }
    }
}
