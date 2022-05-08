using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookingApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TryImageUploadPage_ : ContentPage
    {
        static int  IdProp=8;
        public TryImageUploadPage_()
        {
            InitializeComponent();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            string RezervariUrl = "https://192.168.0.128:45455/api/Image?idProprietate=6";
            var file = await MediaPicker.PickPhotoAsync();
            if (file == null)
            {
                StatusLabel.Text = "null file";
                return;
            }
            var content = new MultipartFormDataContent();

            string json = JsonConvert.SerializeObject(IdProp);
            StringContent contentPropId = new StringContent(json, Encoding.UTF8, "application/json");


            //content.Headers.Add("idProprietate",json);
            content.Add(new StreamContent(await file.OpenReadAsync()), "file", file.FileName);

            HttpClientHandler ch = GetInsecureHandler();
            HttpClient httpClient = new HttpClient(ch);

            var response = new HttpResponseMessage();
       

            response = await httpClient.PostAsync(RezervariUrl,content);
            if(!response.IsSuccessStatusCode)
                StatusLabel.Text ="Imaginea deja a fost incarcata";
            else
                StatusLabel.Text = response.StatusCode.ToString();
        }
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            return handler;
        }
    }
}