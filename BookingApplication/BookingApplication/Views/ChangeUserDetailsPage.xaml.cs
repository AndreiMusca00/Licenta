using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.ViewModels;
namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeUserDetailsPage : ContentPage
    {
        ChangeUserDetailsVM viewModel;
        public ChangeUserDetailsPage()
        {
            InitializeComponent();
            viewModel = new ChangeUserDetailsVM();
            BindingContext = viewModel;
        }
    }

}