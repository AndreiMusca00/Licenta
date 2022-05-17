using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using BookingAPI.DTOs;
using System.Threading.Tasks;

namespace BookingApplication.ViewModels
{
    public class ChangeUserDetailsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
      

        public ChangeUserDetailsVM()
        {
            LoadData();
        }
        
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

        private string userPrenume;
        public string UserPrenume
        {
            get { return userPrenume; }
            set
            {
                userPrenume = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserPrenume"));
            }
        }

        private string userMail;
        public string UserMail
        {
            get { return userMail; }
            set
            {
                userMail = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserMail"));
            }
        }

        private string userTelefon;
        public string UserTelefon
        {
            get { return userTelefon; }
            set
            {
                userTelefon = value;
                PropertyChanged(this, new PropertyChangedEventArgs("UserTelefon"));
            }
        }
        
        public Command UpdateUserCommand
        {
            get
            {
                return new Command(UpdateUser);
            }
        }

        private async void UpdateUser()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserTelefon) || string.IsNullOrEmpty(UserPrenume) || string.IsNullOrEmpty(UserMail))
                await App.Current.MainPage.DisplayAlert("Empty Values!", " Please enter data in each field displayed", "OK");
            else
            {
                UpdateUserDTO user = new UpdateUserDTO();
                user.Nume = UserName;
                user.Prenume = UserPrenume;
                user.Mail = UserMail;
                user.Telefon = UserTelefon;
                var response = await App.Database.UpdateUserDetails(user);
                if (response != "ok" )
                {
                    await Application.Current.MainPage.DisplayAlert("Eroare Login!", "Username sau parola gresita", "Reincearca");
                }
                else
                {
                   await  Application.Current.MainPage.Navigation.PopModalAsync(); 
                }
            }
        }
 
        async private Task LoadData()
        {
            UpdateUserDTO user = await App.Database.GetConnectedUser();
            UserName = user.Nume;
            UserPrenume = user.Prenume;
            UserMail = user.Mail;
            UserTelefon = user.Telefon;
        }
    }
}
