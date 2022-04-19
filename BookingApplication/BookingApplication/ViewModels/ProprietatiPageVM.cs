using System;
using System.Collections.Generic;
using System.Text;
using BookingApplication.Models;
namespace BookingApplication.ViewModels
{
   public  class ProprietatiPageVM
    {
        public List<Proprietate> Prop = new List<Proprietate>();
        public string counter;
        public  ProprietatiPageVM()
        {
            GetProp();
        }
        public async void GetProp()
        {
            Prop =  await App.Database.GetProprietati();
        }

    }
    
}
