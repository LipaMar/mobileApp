using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using mobileApp.Models;
using mobileApp.Views;
using System.IO;
using System.Linq;

namespace mobileApp.ViewModels
{
    public class StudentsViewModel : BaseViewModel
    {
        public ObservableCollection<Student> Students { get; set; }
        public Command LoadItemsCommand { get; set; }

        public StudentsViewModel()
        {
            Title = "Students";
            Students = new ObservableCollection<Student>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Students.Clear();
                var items = await StudentDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Students.Add(item);
                }
                Students.Remove(Students.FirstOrDefault(s=>s.Id==LoggedUser.User.Id));
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