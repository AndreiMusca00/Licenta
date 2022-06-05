using BookingApplication.ViewModels;
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
    public partial class ReviewPage : ContentPage
    {
        ReviewPageVM reviewPageVM;
        readonly int _proprietateId;
        public ReviewPage(int proprietateId)
        {
            _proprietateId = proprietateId;
            InitializeComponent();
            reviewPageVM = new ReviewPageVM(proprietateId);
            BindingContext = reviewPageVM;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetReviewsProprietate(_proprietateId);
        }

    }
}