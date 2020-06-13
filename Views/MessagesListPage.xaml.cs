using mobileApp.Models;
using mobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagesListPage : ContentPage
    {
        public MessagesListPage()
        {
            InitializeComponent();
        }
        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Student)layout.BindingContext;
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
        }
    }
}