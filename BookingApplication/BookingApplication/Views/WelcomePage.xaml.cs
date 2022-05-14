using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.Models;
using BookingApplication.Data;
using BookingApplication.Views;
using Plugin.Toast;
namespace BookingApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }
        async void SignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        async void LoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage {});
           
        }
    }
}