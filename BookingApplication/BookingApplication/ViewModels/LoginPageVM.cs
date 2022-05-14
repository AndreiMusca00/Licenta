using BookingApplication.DTOs;
using BookingApplication.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace BookingApplication.ViewModels
{
    public class LoginPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                PropertyChanged(this,new PropertyChangedEventArgs("UserName"));
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

        public Command LoginCommand
        {
            get
            {
                return new Command(Login);
            }
        }
        private async void Login()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
               await App.Current.MainPage.DisplayAlert("Empty Values!", " Please enter Username and Password", "OK");
            else
            {
                UserLoginDTO user = new UserLoginDTO();
                user.Username = UserName;
                user.Password = Password;
                var response = await App.Database.LoginUserAsync(user);
                if (response.Token == "Wrong password" || response.Token == "User not found")
                {
                   await Application.Current.MainPage.DisplayAlert("Eroare Login!", "Username sau parola gresita", "Reincearca");
                }
                else
                {
                    Application.Current.MainPage =new NavigationPage(new ProprietatiPage(response.Role, response.Username));
                }   
            }
        }
    }
}
