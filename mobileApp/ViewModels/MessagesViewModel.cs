using mobileApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobileApp.ViewModels
{
    public class MessagesViewModel:BaseViewModel
    {
        public ObservableCollection<Student> Students { get; set; }
        public ICommand LoadRecipientsCommand { get; set; }
        public MessagesViewModel()
        {
            Title = "Wiadomości";
            Students = new ObservableCollection<Student>();
            LoadRecipientsCommand = new Command(async () => await ExecuteLoadRecipientsCommand());
        }
        async Task ExecuteLoadRecipientsCommand()
        {
            IsBusy = true;

            try
            {
                Students.Clear();
                var items = await StudentDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    //if(item.MessagesReceived)
                    Students.Add(item);
                }
                Students.Remove(Students.FirstOrDefault(s => s.Id == LoggedUser.User.Id));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
