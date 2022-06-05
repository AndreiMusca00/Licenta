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
    public partial class AdaugaReviewPage : ContentPage
    {
       
        AdaugaReviewVM adaugaReviewVM;
        public AdaugaReviewPage(int proprietateId)
        {
            InitializeComponent();
            adaugaReviewVM = new AdaugaReviewVM(proprietateId);
            BindingContext = adaugaReviewVM;
        }
    }
}