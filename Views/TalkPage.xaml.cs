using mobileApp.Models;
using mobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TalkPage : ContentPage
    {
        TalkViewModel viewModel;

        public TalkPage(TalkViewModel recipient)
        {
            InitializeComponent();
            BindingContext = viewModel = recipient;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var mess = new Message() { Id = Guid.NewGuid().ToString(), Content = Message.Text, RecipentId = viewModel.Recipient.Id, SenderId = LoggedUser.User.Id,Date=DateTime.Now.ToString() };
            MessagingCenter.Send(this, "SendMessage", mess);
            Message.Text = "";
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Messages.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}