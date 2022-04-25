using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Toast;
using BookingApplication.Models;
using BookingApplication.Views;
using BookingApplication.DTOs;
namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void LoginButtonClicked(object sender, EventArgs e)
        {
            try
            {
                UserLoginDTO user = new UserLoginDTO();
                user.Username = userEntry.Text.ToString();
                user.Password = passwordEntry.Text.ToString();
                var response = await App.Database.LoginUserAsync(user);
                if(response.Token=="Wrong password"||response.Token=="User not found")
                {
                    await DisplayAlert("Eroare Login!","Username sau parola gresita","Reincearca");
                }else
                {
                    await Navigation.PushAsync(new ProprietatiPage());
                }
              

            }
            catch(Exception ex)
            {
                await DisplayAlert("Warning", "Username sau parola gresita ", "OK");
                Console.WriteLine(ex.Message);
            }
           
            
        }
    }
}