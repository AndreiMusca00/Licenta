using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Drawing;
using System.Net;

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

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            HttpClientHandler ch = GetInsecureHandler();
            HttpClient httpClient = new HttpClient(ch);
            string RezervariUrll = "https://192.168.0.103:45455/api/Image?idProprietate=4";
            
            //WebClient wc = new WebClient();
            //HttpResponseMessage imaginea = await httpClient.GetAsync(RezervariUrll);

            //
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RezervariUrll);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            var test = request.Headers.ToByteArray();
            var stream1 = new MemoryStream(test);
            var x = ImageSource.FromStream(() => stream1);
            imagineaDisplay.Source = x;
            //
            /*
            byte[] showing = await imaginea.Content.ReadAsByteArrayAsync();
            testLabel.Text =imaginea.StatusCode.ToString();
            var stream1 = new MemoryStream(showing);
            var x = ImageSource.FromStream(() => stream1);
            imagineaDisplay.Source = x;
            */
        }

        private async  void Button_Uri(object sender, EventArgs e)
        {
            /* imagineaDisplay.Source = new UriImageSource
             {
                 Uri = new Uri("https://facingissuesonitcom.files.wordpress.com/2019/11/data-uri-schema.jpg"),
                 CachingEnabled = true,
                 CacheValidity = new TimeSpan(3, 0, 0, 0)
             };  */
            string RezervariUrll = "https://192.168.0.103:45455/api/Image/img?idProprietate=7";
            HttpClientHandler ch = GetInsecureHandler();
            HttpClient httpClient = new HttpClient(ch);
            var imgPath = await httpClient.GetAsync(RezervariUrll);
            string content = await imgPath.Content.ReadAsStringAsync();
            
            testLabel.Text = content;
           imagineaDisplay.Source = ImageSource.FromFile(content);
    }
    }
}