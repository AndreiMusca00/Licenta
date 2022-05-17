using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using BookingApplication.DTOs;
using BookingAPI.DTOs;

namespace BookingApplication.ViewModels
{
    public class AdaugaProprietateVM: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string nume;
        public string Nume
        {
            get { return nume; }
            set
            {
                nume = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Nume"));
            }
        }

        private string judet;
        public string Judet
        {
            get { return judet; }
            set
            {
                judet = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Judet"));
            }
        }

        private string oras;
        public string Oras
        {
            get { return oras; }
            set
            {
                oras = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Oras"));
            }
        }

        private string strada;
        public string Strada
        {
            get { return strada; }
            set
            {
                strada = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Strada"));
            }
        }

        private string numar;
        public string Numar
        {
            get { return numar; }
            set
            {
                numar = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Numar"));
            }
        }

        private string pret;
        public string Pret
        {
            get { return pret; }
            set
            {
                pret = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Pret"));
            }
        }

        public Command CreateProprietateCommand
        {
            get
            {
                return new Command(CreateProprietate);
            }
        }

        private async void CreateProprietate()
        {
            if (string.IsNullOrEmpty(Numar) || string.IsNullOrEmpty(Pret) || string.IsNullOrEmpty(Strada) || string.IsNullOrEmpty(Oras) || string.IsNullOrEmpty(Judet) || string.IsNullOrEmpty(Nume))
                await App.Current.MainPage.DisplayAlert("Empty Values!", " Please enter data in each field displayed", "OK");
            else
            {
                AddProprietateDTO proprietate = new AddProprietateDTO()
                {
                    Nume = Nume,
                    Judet =Judet,
                    Oras = Oras,
                    Strada = Strada,
                    Numar = Numar,
                    Pret = Convert.ToInt32(Pret)
                };

                var response = await App.Database.AddProprietate(proprietate);
                if (response != "ok")
                {
                    await Application.Current.MainPage.DisplayAlert("Eroare Introducere!", "Try again", "Reincearca");
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
            }
        }
    }
}
