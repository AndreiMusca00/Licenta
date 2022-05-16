using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookingApplication.Models;
using BookingApplication.DTOs;
namespace BookingApplication.ViewModels
{
   public  class ProprietatiPageVM
    {
        public List<Proprietate> list = new List<Proprietate>();
        public List<ProprietateOnePictureDTO> proprietati = new List<ProprietateOnePictureDTO>();
       
        public async Task<List<Proprietate>> GetProprietatiFromDatabase()
        {
           list = await App.Database.GetProprietati();
           return list;
        }
        public async Task<List<ProprietateOnePictureDTO>> CreateBindingContext()
        {
            await GetProprietatiFromDatabase();
            foreach(var p in list)
            {
                ProprietateOnePictureDTO item = new ProprietateOnePictureDTO();
                item.Id = p.Id;
                item.Judet = p.Judet;
                item.Numar = p.Numar;
                item.Nume = p.Nume;
                item.Oras = p.Oras;
                item.Pret = p.Pret;
                item.Strada = p.Strada;
                item.Imagine = await App.Database.GetOneImagePath(p.Id);
                proprietati.Add(item);
            }
            return proprietati ;
        }


    }
    
}
