using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookingApplication.Data;
namespace BookingApplication
{
    public partial class App : Application
    {
        public static BookingDatabase Database { get; private set; }
        public App()
        {
            InitializeComponent();

            Database = new BookingDatabase(new RestService());
            MainPage = new NavigationPage(new WelcomePage());
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
