using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookingApplication.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilulMeuPage : ContentPage
    {
        string _role;
        public ProfilulMeuPage(string Role)
        {
            InitializeComponent();
            _role = Role;
            if(Role == "Admin")
            {
                lblRole.Text = "Admin using";
            }else if (Role == "Basic")
            {
                lblRole.Text = "Basic user using this";
            }
        }
    }
}