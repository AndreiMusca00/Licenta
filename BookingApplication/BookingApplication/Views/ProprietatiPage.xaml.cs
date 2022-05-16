using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.Views;
using BookingApplication.ViewModels;
using BookingApplication.Models;
using BookingApplication.DTOs;
using System.Net.Http;
using System.IO;

namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProprietatiPage : ContentPage
    {
        ProprietatiPageVM ViewModel= new ProprietatiPageVM();
        string _role;
        string _numeUtilizator;
        public ProprietatiPage(string Role,string numeUtilizator)
        {
            InitializeComponent();
            _role = Role;
            _numeUtilizator = numeUtilizator;         
        }
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            return handler;
        }
        protected override async void OnAppearing()        
        {
            base.OnAppearing();
            listView.ItemsSource = await ViewModel.CreateBindingContext();
            //listView.ItemsSource
            /*
                List<Proprietate> prop= await ViewModel.GetProprietatiFromDatabase();
            List<ProprietateOnePictureDTO> list = new List<ProprietateOnePictureDTO>();

            HttpClientHandler ch = GetInsecureHandler();
            HttpClient httpClient = new HttpClient(ch);
            string RezervariUrll = "https://192.168.0.103:45455/api/Image/img?idProprietate={0}";

            foreach (var p in prop)
            {
                ProprietateOnePictureDTO item = new ProprietateOnePictureDTO();
                item.Id = p.Id;
                item.Judet = p.Judet;
                item.Numar = p.Numar;
                item.Nume = p.Nume;
                item.Oras = p.Oras;
                item.Pret = p.Pret;
                item.Strada = p.Strada;

                // var imaginea = await httpClient.GetAsync(RezervariUrll);
                // byte[] showing = await imaginea.Content.ReadAsByteArrayAsync();

                // var stream1 = new MemoryStream(showing);
                // var x = ImageSource.FromStream(() => stream1);
                Uri uri = new Uri(String.Format(RezervariUrll, p.Id));
                var imgPath = await httpClient.GetAsync(uri);
                string content = await imgPath.Content.ReadAsStringAsync();
                item.Imagine = content;
                list.Add(item);
            }
            listView.ItemsSource = list;
            */
        }

        async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                ProprietateOnePictureDTO p = e.SelectedItem as ProprietateOnePictureDTO;
                await Navigation.PushAsync(new ProprietatePage(p.Id));
                ((ListView)sender).SelectedItem = null;
            }   
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilulMeuPage(_role,_numeUtilizator));
        }
    }
}