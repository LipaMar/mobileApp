using mobileApp.Models;
using mobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobileApp.ViewModels
{
    public class RegistrationViewModel : BaseViewModel 
    {
        public ObservableCollection<Student> Students { get; set; }
        public RegistrationViewModel()
        {
            Title = "Rejestracja";
            Students = new ObservableCollection<Student>();
            MessagingCenter.Subscribe<RegistrationPage, Student>(this, "RegisterStudent", async (obj, item) =>
            {
                var newItem = item as Student;
                Students.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }
    }
}

