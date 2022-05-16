using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookingApplication.Models;
namespace BookingApplication.ViewModels
{
    public class ProprietatePageVM
    {
        List<string> paths = new List<string>();
        Proprietate proprietate = new Proprietate();
        public async Task<List<string>> GetImages(int proprietateId)
        {
            paths = await App.Database.GetImages(proprietateId);
            return paths;
        }

        public async Task<Proprietate> GetProprietate(int proprietateId)
        {
            proprietate = await App.Database.GetProprietate(proprietateId);
            return proprietate;
        }
    }
}
