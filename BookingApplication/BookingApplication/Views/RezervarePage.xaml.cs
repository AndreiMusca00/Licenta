﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.Models;

namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RezervarePage : ContentPage
    {
        int _proprietateId;
        public RezervarePage(int proprietateId)
        {
            
            InitializeComponent();
            _proprietateId = proprietateId;
            lblPropId.Text = Convert.ToString(proprietateId);
            
        }

        private async void btnRezerva_Clicked(object sender, EventArgs e)
        {
            string response = await App.Database.AddRezervare(_proprietateId, datePicker.Date);
            if(response == "ok")
            {
                await Navigation.PopModalAsync();
            }
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}