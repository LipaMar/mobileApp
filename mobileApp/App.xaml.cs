using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using mobileApp.Services;
using mobileApp.Views;
using mobileApp.Models;

namespace mobileApp
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "https://192.168.1.11:45455" : "https://localhost:44300/";
        public static bool UseMockDataStore = false;

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<IDataStore<Student>,MockDataStore>();
            else
                DependencyService.Register<IDataStore<Student>, StudentDataStore>();

            DependencyService.Register<IDataStore<Message>, MessageDataStore>();
            MainPage = new NavigationPage(new LoginPage()) { BarBackgroundColor = Color.LightPink };
        }

        protected override void OnStart()
        {


        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
