using Android.App;
using Android.Graphics;
using mobileApp.Models;
using mobileApp.ViewModels;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Xaml;

namespace mobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        private Plugin.Media.Abstractions.MediaFile file = null;
        public Student Student { get; set; }
        public RegistrationPage()
        {
            BindingContext = new RegistrationViewModel();
            InitializeComponent();
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            if (isFormValid())
            {
                var _imageConverter = new ImageSourceConverter();

                Student = new Student { Email = email.Text, Id = Guid.NewGuid().ToString(),UserName=userName.Text, Name = name.Text, Password = pass.Text, PhoneNr = phoneNr.Text, Picture = ImageToBytes(), Profile = profile.Text, Surname = lastName.Text };
                MessagingCenter.Send(this, "RegisterStudent", Student);
                await Navigation.PopAsync();
            }
            else
                await DisplayAlert("Błąd","Błędnie wypełony formularz!","Ok");
        }
        private bool isFormValid()
        {
            if (String.IsNullOrEmpty(userName.Text) ||
                String.IsNullOrEmpty(name.Text) ||
                String.IsNullOrEmpty(lastName.Text) ||
                String.IsNullOrEmpty(pass.Text) ||
                String.IsNullOrEmpty(email.Text) ||
                String.IsNullOrEmpty(phoneNr.Text)) return false;
            if (pass.Text != confPass.Text) return false;
            return true;
        }

        private async void ImageButton_ClickedAsync(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("no upload", "picking a photo is not supported", "Ok");
                return;
            }

            file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return;

            Image.Source = ImageSource.FromStream(() => file.GetStream());
        }
        private byte[] ImageToBytes()
        {
            if (file == null) return null;
            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                file.Dispose();
                return memoryStream.ToArray();
            }
        }

    }
}