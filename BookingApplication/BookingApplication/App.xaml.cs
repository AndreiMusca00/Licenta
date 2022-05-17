using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.Data;
using BookingApplication.Views;

namespace BookingApplication
{
    public partial class App : Application
    {
        public static BookingDatabase Database { get; private set; }
        public App()
        {
            InitializeComponent();
            
           Database = new BookingDatabase(new RestService(),new userRestService());
           MainPage = new NavigationPage(new WelcomePage()) {
               BarBackgroundColor = Color.Gray,
             BarTextColor = Color.White
            };
            //MainPage = new NavigationPage(new TryImageUploadPage_());
            //MainPage = new NavigationPage(new RezervariProprietatePage(2));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
