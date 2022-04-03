using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.Models;
namespace BookingApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var slist = (Users)BindingContext;
            //slist.Email = DateTime.UtcNow;

            await App.Database.SaveUserAsync(slist,true);
            await Navigation.PopAsync();
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var slist = (Users)BindingContext;
            await App.Database.DeleteUserAsync(slist);
            await Navigation.PopAsync();
        }
    }
}