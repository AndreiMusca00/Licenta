using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilulMeuIstoricRezervari : ContentPage
    {
        public ProfilulMeuIstoricRezervari()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetIstoricRezervariBasic();
        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            int idBun = Convert.ToInt32(id);
            var response = await App.Database.DeleteRezervare(idBun);
            if(response=="ok")
                listView.ItemsSource = await App.Database.GetIstoricRezervariBasic();
        }
    }
}