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
        string _role;
        string _numeUtilizator;
        public ProprietatiPage(string Role,string numeUtilizator)
        {
            InitializeComponent();
            _role = Role;
            _numeUtilizator = numeUtilizator;
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
                Proprietate prop = e.SelectedItem as Proprietate;
                await Navigation.PushAsync(new ProprietatePage(prop));
                /*
                    await Navigation.PushAsync(new ProprietatePage
                    {
                        BindingContext = e.SelectedItem as Proprietate
                    });
                */
            }
            
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilulMeuPage(_role,_numeUtilizator));
        }
    }
}