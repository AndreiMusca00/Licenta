using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.Views;
namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilulMeuPage : ContentPage
    {
        string _role;
        string _numeUtilizator;
        public ProfilulMeuPage(string Role,string numeUtilizator)
        {
            InitializeComponent();
            _role = Role;
            _numeUtilizator = numeUtilizator;
           
        }

        protected override  void OnAppearing()
        {
            base.OnAppearing();
            if (_role == "Admin")
            {
                btnProprietati.IsVisible = true;
            }
        }

        private async void btnIstoricClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilulMeuIstoricRezervari());
        }

        private async void btnProprietatiClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProprietatileMele());
        }
    }
}