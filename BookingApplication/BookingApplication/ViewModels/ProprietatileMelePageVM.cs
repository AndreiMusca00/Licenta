using BookingApplication.DTOs;
using BookingApplication.Models;
using BookingApplication.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookingApplication.ViewModels
{
    public class ProprietatileMelePageVM
    {
        public List<Proprietate> list = new List<Proprietate>();
        public List<ProprietateOnePictureDTO> proprietati = new List<ProprietateOnePictureDTO>();
        public async Task<List<Proprietate>> GetProprietatiFromDatabase()
        {
            list = await App.Database.GetProprietatiAdmin();
            return list;
        }
        public async Task<List<ProprietateOnePictureDTO>> CreateBindingContext()
        {
            await GetProprietatiFromDatabase();
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
        public Command AdaugaCommand
        {
            get
            {
                return new Command(AdaugaProprietate);
            }
        }
        private async void AdaugaProprietate()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AdaugaProprietatePage());
        }

    }
}
