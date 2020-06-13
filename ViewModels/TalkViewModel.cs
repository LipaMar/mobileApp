using mobileApp.Models;
using mobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobileApp.ViewModels
{
    public class TalkViewModel:BaseViewModel
    {
        public ObservableCollection<Message> Messages { get; set; }
        public ICommand LoadMessages { get; set; }
        public Student Recipient;
        public TalkViewModel(Student recipient)
        {
            Title = recipient.Name + " " + recipient.Surname;
            Recipient = recipient;
            Messages = new ObservableCollection<Message>();
            LoadMessages = new Command(async () =>await ExecutLoadMessages());
            MessagingCenter.Subscribe<TalkPage,Message>(this, "SendMessage", async (obj, message) =>
            {
                var newMessage = message as Message;
                    await MessageDataStore.AddItemAsync(newMessage);
                    Messages.Add(newMessage);
                
            });
        }
        async Task ExecutLoadMessages()
        {
            Messages.Clear();
            IsBusy = true;
            var messages = await MessageDataStore.GetItemsAsync(true);
            var userID = LoggedUser.User.Id;
            var recipientID = Recipient.Id;
            messages = messages.OrderBy(s => s.Date);
            foreach (var message in messages)
                {

                if (message.RecipentId.Equals(recipientID) && message.SenderId.Equals(userID))
                {
                    message.Content = "Wysłano: " + message.Content;
                    Messages.Add(message);
                }
                if (message.SenderId.Equals(recipientID) && message.RecipentId.Equals(userID)) 
                {
                    message.Content = $"{Recipient.Name} napisał: " + message.Content;
                    Messages.Add(message);
                }
            }
            IsBusy = false;
        }
    }
}
