using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.Views;
using BookingApplication.ViewModels;
namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilulMeuPage : ContentPage
    {
        string _role;
        public string _numeUtilizator;

        private string _lblText;
        public string LblText
        {
            get
            {
                return _lblText;
            }
            set
            {
                _lblText = value;
                OnPropertyChanged();
            }
        }

        ProfilulMeuVM profilulMeuVM;

        public ProfilulMeuPage(string Role,string numeUtilizator)
        {
            InitializeComponent();
            profilulMeuVM = new ProfilulMeuVM();
            BindingContext = profilulMeuVM;
            _role = Role;
            _numeUtilizator = numeUtilizator;
           
        }

        protected override void OnAppearing()
        {
            LblText = _numeUtilizator;
            base.OnAppearing();
            if (_role == "Admin")
            {
                btnProprietati.IsVisible = true;
            }
        }
    }
}