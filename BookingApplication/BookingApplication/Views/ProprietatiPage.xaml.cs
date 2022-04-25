using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.Views;
using BookingApplication.ViewModels;
using BookingApplication.Models;
namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProprietatiPage : ContentPage
    {
        ProprietatiPageVM ViewModel = new ProprietatiPageVM();
        public ProprietatiPage()
        {
            InitializeComponent();
           
        }
        protected override async void OnAppearing()        
        {
            base.OnAppearing();
            listView.ItemsSource = await ViewModel.GetProp();
        }

        async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
          
                if (e.SelectedItem != null)
                {
                    await Navigation.PushAsync(new ProprietatePage
                    {
                        BindingContext = e.SelectedItem as Proprietate
                    });
                }
            
        }
    }
}