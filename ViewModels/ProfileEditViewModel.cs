using Android.Util;
using mobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobileApp.ViewModels
{
    public class ProfileEditViewModel:BaseViewModel
    {
        public Student Student { get; set; }
        public ICommand UpdateUser { get; set; }
        public ProfileEditViewModel()
        {
            Title = "Mój profil";
            var LoggedStudent = LoggedUser.User;
            Student = new Student
            {
                Id = LoggedStudent.Id,
                UserName = LoggedStudent.UserName,
                Password = LoggedStudent.Password,
                Email = LoggedStudent.Email,
                Name = LoggedStudent.Name,
                Surname = LoggedStudent.Surname,
                PhoneNr = LoggedStudent.PhoneNr,
                Picture = LoggedStudent.Picture== null ? new byte[0] : LoggedStudent.Picture,
                Profile = LoggedStudent.Profile,
                MessagesReceived = LoggedStudent.MessagesReceived,
                MessagesSent = LoggedStudent.MessagesSent
            };
            UpdateUser = new Command(async()=> { 
                IsBusy = true;
                await StudentDataStore.UpdateItemAsync(Student);
                LoggedUser.User = Student;
                await Application.Current.MainPage.DisplayAlert("Aktualizacja profilu", "Pomyślnie zaktualizowano profil", "Ok");
                IsBusy = false;
            });

        }
    }
}
