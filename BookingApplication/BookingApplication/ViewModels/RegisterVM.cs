using BookingApplication.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace BookingApplication.ViewModels
{
    public class RegisterVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
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
        private string prenume;
        public string Prenume
        {
            get { return prenume; }
            set
            {
                prenume = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Prenume"));
            }
        }
        private string mail;
        public string Mail
        {
            get { return mail; }
            set
            {
                mail = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Mail"));
            }
        }
        private string telefon;
        public string Telefon
        {
            get { return telefon; }
            set
            {
                telefon = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Telefon"));
            }
        }
        public Command RegisterCommand
        {
            get
            {
                return new Command(Register);
            }
        }
        public async void Register()
        {
            UserRegisterDTO user = new UserRegisterDTO();
            user.Username = UserName;
            user.Nume = Nume;
            user.Prenume = Prenume;
            user.Mail = Mail;
            user.Telefon = Telefon;
            user.Password = Password;


            string json = JsonConvert.SerializeObject(user);
            var response = await App.Database.RegisterUser(user);
            if (response == "User creat cu succes")
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Numele de utilizator este deja folosit", "Ok");
            }
        }
    }
}
