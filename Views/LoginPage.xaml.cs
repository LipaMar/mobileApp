using mobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Registration_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        private void Login_Button_Clicked(object sender, EventArgs e)
        {
            if (isPasswordValid())
            {
                Application.Current.MainPage = new MainPage();
            }
        }
        private bool isPasswordValid()
        {
            return true;
        }
    }
}