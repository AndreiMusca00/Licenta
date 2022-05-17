using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.ViewModels;
using BookingApplication.DTOs;
namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RezervariProprietatePage : ContentPage
    {
        RezervariProprietateVM viewModel = new RezervariProprietateVM();
        public int _proprietateId;
        //List<RezervariProprietateDTO> mock = new List<RezervariProprietateDTO>();
        public RezervariProprietatePage(int id)
        {
            InitializeComponent();
            _proprietateId = id;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            /*
            RezervariProprietateDTO  r = new RezervariProprietateDTO()
            {
                dataRezervare = DateTime.Today,
                mail = "sada",
                nume = "ASdad",
                prenume = "asdajdashfkjsf",
                telefon = "0752279403"
            };
            for(int i=0;i<10;i++)
                mock.Add(r);*/
            listView.ItemsSource = await viewModel.GetProprietatiFromDatabase(_proprietateId);

        }
    }
}