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
        public async Task<List<ProprietateOnePictureDTO>> CreateFilteredBindingContext(string filter,int lowerValue, int upperValue)
        {
            List<Proprietate> list = new List<Proprietate>();
            list = await GetProprietatiFilteredFromDatabase(filter,lowerValue,upperValue);
            List<ProprietateOnePictureDTO> proprietati = new List<ProprietateOnePictureDTO>();
            foreach (var p in list)
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
            return proprietati;
        }
        public async Task<List<Proprietate>> GetProprietatiFilteredFromDatabase(string filter, int lowerValue, int upperValue)
        {
            return await App.Database.GetProprietatiFiltered(filter,lowerValue,upperValue);
        }
    }
    
}
