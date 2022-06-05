using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace BookingApplication.ViewModels
{
    public class AdaugaReviewVM : INotifyPropertyChanged
    {
        int _proprietateId;
        public AdaugaReviewVM(int proprietateId)
        {
            _proprietateId = proprietateId;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private string textReview;
        public string TextReview
        {
            get { return textReview; }
            set
            {
                textReview = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TextReview"));
            }
        }

        private float nota;
        public float Nota
        {
            get { return nota; }
            set
            {
                nota = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Nota"));
            }
        }
        public Command AddReviewCommand
        {
            get
            {
                return new Command(AddReview);
            }
        }
        public async void AddReview()
        {
            int Notaint = Convert.ToInt32(Nota);
            string raspuns = await App.Database.AddReview(_proprietateId, TextReview, Notaint);
            if(raspuns == "ok")
                await Application.Current.MainPage.Navigation.PopModalAsync();
            else
                await Application.Current.MainPage.DisplayAlert("E de rau !", "Username sau parola gresita", "Reincearca");

            await Application.Current.MainPage.Navigation.PopModalAsync();
        } 
    }
}
