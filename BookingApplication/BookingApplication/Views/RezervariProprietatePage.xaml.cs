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
    public partial class RezervariProprietatePage : ContentPage
    {
        RezervariProprietateVM viewModel = new RezervariProprietateVM();
        public int _proprietateId;
        public RezervariProprietatePage(int id)
        {
            InitializeComponent();
            _proprietateId = id;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await viewModel.GetProprietatiFromDatabase(_proprietateId);
        }
        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            int idBun = Convert.ToInt32(id);
            var response = await App.Database.DeleteRezervare(idBun);
            if (response == "ok")
                listView.ItemsSource = await viewModel.GetProprietatiFromDatabase(_proprietateId);
        }
    }
}