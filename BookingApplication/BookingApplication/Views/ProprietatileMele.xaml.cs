using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.ViewModels;
using BookingApplication.DTOs;

namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProprietatileMele : ContentPage
    {
        ProprietatileMelePageVM ViewModel = new ProprietatileMelePageVM();
        public ProprietatileMele()
        {
            InitializeComponent();
            BindingContext = ViewModel;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await ViewModel.CreateBindingContext();

        }

        async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                ProprietateOnePictureDTO p = e.SelectedItem as ProprietateOnePictureDTO;
                await Navigation.PushAsync(new RezervariProprietatePage(p.Id));
                ((ListView)sender).SelectedItem = null;
            }
        }
    }
}