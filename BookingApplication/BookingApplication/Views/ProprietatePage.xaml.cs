using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.Models;
namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProprietatePage : ContentPage
    {
        Proprietate proprietateSelectata;
        public ProprietatePage(Proprietate proprietate)
        {
            InitializeComponent();
            BindingContext = proprietate;
            proprietateSelectata = proprietate;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            btnAdd.Text=Convert.ToString( proprietateSelectata.Id);
            int proprietateId = proprietateSelectata.Id;
            await Navigation.PushModalAsync(new RezervarePage(proprietateId));
        }
    }
}