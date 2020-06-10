using System;
using System.Collections.Generic;
using System.Text;

namespace mobileApp.Models
{
    public enum MenuItemType
    {
        List,
        Profile,
        Logout
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
