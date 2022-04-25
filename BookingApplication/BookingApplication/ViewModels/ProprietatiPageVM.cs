using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookingApplication.Models;
namespace BookingApplication.ViewModels
{
   public  class ProprietatiPageVM
    {
        public List<Proprietate> Prop = new List<Proprietate>();
        public string counter;
        public  ProprietatiPageVM()
        {
           // GetProp();
        }
        public async Task<List<Proprietate>> GetProp()
        {
           return  await App.Database.GetProprietati();
        }

    }
    
}
