using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using BookingApplication.Views;

namespace BookingApplication.ViewModels
{
    public class ReviewPageVM 
    {
        int _proprietateId;
        public ReviewPageVM(int proprietateId)
        {
            _proprietateId = proprietateId;
        }
        public Command AddReviewCommand
        {
            get
            {
                return new Command(AddReview);
            }
        }
        private async void AddReview()
        {
            if(!await App.Database.HasReviewRights(_proprietateId))
                await App.Current.MainPage.DisplayAlert("Lipsa rezervare!", "Pentru a putea lasa review, trebuie sa aveti o rezervare facuta la aceasta proprietate", "OK");
            else
                await Application.Current.MainPage.Navigation.PushModalAsync(new AdaugaReviewPage(_proprietateId));
            
        }
    }
}
