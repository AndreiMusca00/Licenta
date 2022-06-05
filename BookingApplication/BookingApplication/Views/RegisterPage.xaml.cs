using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.DTOs;
using Newtonsoft.Json;
using BookingApplication.ViewModels;

namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        RegisterVM registerVM;
        public RegisterPage()
        {
            InitializeComponent();
            registerVM = new RegisterVM();
            BindingContext = registerVM;
        }

        private void checkSeePassword_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (checkSeePassword.IsChecked)
            {
                entryPassword.IsPassword = false;
            }
            else if(!checkSeePassword.IsChecked)
            {
                entryPassword.IsPassword = true;
            }
        }

        
    }
}