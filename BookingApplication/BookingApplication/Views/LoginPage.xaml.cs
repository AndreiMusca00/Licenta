using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Toast;
using BookingApplication.Models;
using BookingApplication.Views;
using BookingApplication.DTOs;
using BookingApplication.ViewModels;

namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginPageVM loginPageVM;
        public LoginPage()
        {
            InitializeComponent();
            loginPageVM = new LoginPageVM();
            BindingContext = loginPageVM;
        }
    }
}