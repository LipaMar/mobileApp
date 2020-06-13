using System;
using System.Windows.Input;
using Android.Util;
using mobileApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mobileApp.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ICommand OpenWebsiteCommand { get; set; }
        public ICommand OpenSmsCommand { get; set; }
        public ICommand OpenEmailCommand { get; set; }
        public Student Student { get; set; }
        public  string ContactTel{ get; set; }
        public  string ContactMail{ get; set; }
        public ItemDetailViewModel(Student student = null)
        {
            Title = student?.Name;
            Student = student;
            ContactTel = "Tel: "+Student.PhoneNr;
            ContactMail = "Email: "+Student.Email;
            if (!String.IsNullOrEmpty(Student.Profile)) {
                try
                {
                    OpenWebsiteCommand = new Command(async () => await Browser.OpenAsync(Student.Profile, BrowserLaunchMode.SystemPreferred));
                }
                catch (Exception ){
                    Application.Current.MainPage.DisplayAlert("Błąd", "Wystąpił błąd przy otwieraniu strony internetowej", "Ok");
                }
            }
            else
            {
                OpenWebsiteCommand = new Command(async () => await Application.Current.MainPage.DisplayAlert("Ups", "Brak strony do wyświetlenia", "OK"));
            }
            OpenSmsCommand = new Command(async () => await Sms.ComposeAsync(new SmsMessage("", Student.PhoneNr)));
            OpenEmailCommand = new Command(async () => await Email.ComposeAsync("","",Student.Email) );
        }
    }
}
