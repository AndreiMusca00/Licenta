using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.DTOs;
using Newtonsoft.Json;


namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void checkSeePassword_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (checkSeePassword.IsChecked)
            {
                entryPassword.IsPassword = false;
            }
            else if(!checkSeePassword.IsChecked)
            {
                entryPassword.IsPassword = true;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            UserRegisterDTO user = new UserRegisterDTO();
            user.Username = entryUsername.Text;
            user.Nume = entryNume.Text;
            user.Prenume = entryPrenume.Text;
            user.Mail = entryEmail.Text;
            user.Password = entryPassword.Text;
            
            string json = JsonConvert.SerializeObject(user);
            var response = await App.Database.RegisterUser(user);
            if(response=="User creat cu succes")
            {
                await Navigation.PopAsync();
            }else
            {
                lblShow.Text = Convert.ToString(response);
            }
        }
    }
}