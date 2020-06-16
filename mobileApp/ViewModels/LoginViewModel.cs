using Java.Util;
using mobileApp.Models;
using mobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobileApp.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public LoginViewModel()
        {
            Title = "Logowanie";
            tryToLoginCommand = new Command(async() => await tryToLoginAsync());

        }
        public ICommand tryToLoginCommand { get; set; }
        async System.Threading.Tasks.Task tryToLoginAsync()
        {
            try
            {
                var list = await StudentDataStore.GetItemsAsync(true);
                var user = list.FirstOrDefault(s => s.UserName.Equals(Login) && s.Password.Equals(Password));
                if (user != null)
                {
                    LoggedUser.User = user;
                    Application.Current.MainPage = new MainPage();
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Błąd","Nieprawidłowe dane logowania","Ok");
            }
            catch(Exception )
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", "Wystąpił błąd połączenia z bazą", "Ok");
            }
        }
    }
}
