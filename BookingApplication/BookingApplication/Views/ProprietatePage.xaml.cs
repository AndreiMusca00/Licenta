using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.Models;
using BookingApplication.ViewModels;
namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProprietatePage : ContentPage
    {
        //Proprietate proprietateSelectata;
        ProprietatePageVM ViewModel = new ProprietatePageVM();
        int _proprietateId;
        public ProprietatePage(int  proprietateId)
        {
            InitializeComponent();
            _proprietateId = proprietateId;            
        }

        protected override async void OnAppearing()
        {
            List<string> paths = await ViewModel.GetImages(_proprietateId);
            CarouselView.ItemsSource = paths;
            Proprietate proprietate = await ViewModel.GetProprietate(_proprietateId);
            BindingContext = proprietate;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RezervarePage(_proprietateId));
        }
        private async void Button_Clicked1(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ReviewPage(_proprietateId));
        }

    }
}