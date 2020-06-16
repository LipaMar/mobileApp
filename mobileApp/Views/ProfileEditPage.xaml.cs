using mobileApp.Models;
using mobileApp.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileEditPage : ContentPage
    {
        ProfileEditViewModel viewmodel;
        public ProfileEditPage()
        {
            InitializeComponent();
            BindingContext = viewmodel = new ProfileEditViewModel();
        }

        private async void ChangePictureButton_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("no upload", "picking a photo is not supported", "Ok");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return;

            Image.Source = ImageSource.FromStream(() => file.GetStream());
            viewmodel.Student.Picture = MediaFileToBytes(file);

        }
        private byte[] MediaFileToBytes(MediaFile file)
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