using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BookingApplication.Views;
namespace BookingApplication.ViewModels
{
    public class ProfilulMeuVM
    {
        public Command ChangePasswordCommand
        {
            get
            {
                return new Command(ChangePassword);
            }
        }
        private async void ChangePassword()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ChangePasswordPage());
        }

        public Command IstoricRezervariCommand
        {
            get
            {
                return new Command(IstoricRezervari);
            }
        }
        private async void IstoricRezervari()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProfilulMeuIstoricRezervari());
        }

        public Command EditUserCommand
        {
            get
            {
                return new Command(EditUser);
            }
        }
        private async void EditUser()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ChangeUserDetailsPage());
        }

        public Command ManagerCommand
        {
            get
            {
                return new Command(Manager);
            }
        }
        public async void Manager()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProprietatileMele());
        }
    }
}
