using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.Views;
using BookingApplication.ViewModels;
namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProprietatiPage : ContentPage
    {
        public ProprietatiPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()        
        {
            base.OnAppearing();

            listView.ItemsSource = await App.Database.GetProprietati();
        }
    }
}