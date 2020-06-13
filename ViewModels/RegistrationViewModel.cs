using mobileApp.Models;
using mobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobileApp.ViewModels
{
    public class RegistrationViewModel : BaseViewModel 
    {
        public ObservableCollection<Student> Students { get; set; }
        private Student newStudent;
        public RegistrationViewModel()
        {
            Title = "Rejestracja";
            Students = new ObservableCollection<Student>();
            LoadStudents();

            MessagingCenter.Subscribe<RegistrationPage, Student>(this, "RegisterStudent", async (obj, student) =>
            {
                newStudent = student as Student;
                if(isAlreadyRegistered())
                {
                    DisplayAlertAboutReservation();
                }
                else
                {
                    await StudentDataStore.AddItemAsync(newStudent);
                    Students.Add(newStudent);
                }
            });
        }
        private bool isAlreadyRegistered()
        {
            if (isUserNameReserved() || isEmailReserved() || isPhoneNrReserved()) return true;
            return false;

        }
        private bool isUserNameReserved()
        {
            return Students.FirstOrDefault(s => s.UserName.Equals(newStudent.UserName)) != null ? true : false;
        }        
        private bool isEmailReserved()
        {
            return Students.FirstOrDefault(s => s.Email.Equals(newStudent.Email)) != null ? true : false;
        }        
        private bool isPhoneNrReserved()
        {
            return Students.FirstOrDefault(s => s.PhoneNr.Equals(newStudent.PhoneNr)) != null ? true : false;
        }
        private async void DisplayAlertAboutReservation()
        {
            if(isUserNameReserved())
                await Application.Current.MainPage.DisplayAlert("Błąd","Podana nazwa użytkownika jest już zajęta","Ok");
            if(isEmailReserved())
                await Application.Current.MainPage.DisplayAlert("Błąd", "Podany adres email jest już zajęty", "Ok");
            if (isPhoneNrReserved())
                await Application.Current.MainPage.DisplayAlert("Błąd", "Podany numer telefonu jest już zajęty", "Ok");
        }
        async void LoadStudents()
        {
            try
            {
                Students.Clear();
                var items = await StudentDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Students.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}

