using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using mobileApp.Models;

namespace mobileApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.List, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            MenuPages.Clear();
                switch (id)
                {
                    case (int)MenuItemType.List:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()) { BarBackgroundColor = Color.LightPink });
                        break;
                    case (int)MenuItemType.Profile:
                        MenuPages.Add(id, new NavigationPage(new ProfileEditPage()) { BarBackgroundColor = Color.LightPink });
                        break;
                    case (int)MenuItemType.Logout:
                    LoggedUser.Logout();
                    Application.Current.MainPage = new NavigationPage(new LoginPage()) { BarBackgroundColor = Color.LightPink };
                        return;
                }
            

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}