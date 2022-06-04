using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace BookingApplication.ViewModels
{
    public class ChangePasswordVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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
        private string password2;
        public string Password2
        {
            get { return password2; }
            set
            {
                password2 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password2"));
            }
        }
        //change pass with pass as parameter 
        public Command ChangePasswordCommand
        {
            get
            {
                return new Command(ChangePassword);
            }
        }
        private async void ChangePassword()
        {
            if (string.IsNullOrEmpty(Password))
                await App.Current.MainPage.DisplayAlert("Empty Values!", " Please enter a valid password", "OK");
            if(Password!=Password2)
                await App.Current.MainPage.DisplayAlert("Passwords don't match!", " Please try again!", "OK");
            else
            {
                var response = await App.Database.ChangePassword(Password);
                if (response== "Fail" )
                {
                    await App.Current.MainPage.DisplayAlert("Parola nu a fost schimbata!", "Try again ", "OK");
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
            }
        }
    }
}
