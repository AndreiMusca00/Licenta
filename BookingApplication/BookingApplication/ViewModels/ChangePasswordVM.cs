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
