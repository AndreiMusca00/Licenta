using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Toast;
using BookingApplication.Models;
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
                //var user = new Users();
                //user.Username = userEntry.Text.ToString();
                var p = await App.Database.GetUserByUserName(userEntry.Text.ToString());
                
          
            }
            catch(Exception ex)
            {
                await DisplayAlert("Warning", "Username sau parola gresita ", "OK");
                Console.WriteLine(ex.Message);
            }
           // 
            
        }
    }
}