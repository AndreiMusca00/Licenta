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
    public partial class ChangePasswordPage : ContentPage
    {
        ChangePasswordVM viewModel;
        public ChangePasswordPage()
        {
            InitializeComponent();
            viewModel = new ChangePasswordVM();
            BindingContext = viewModel;
        }
    }
}