using mobileApp.Models;
using Org.Apache.Http.Impl.Conn;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobileApp
{
    public class LoggedUser
    {
        private LoggedUser() { }
        private static Student user;
        public static Student User { 
            get 
            { 
                return user; 
            }
            set
            {
                if (user == null) user = value;
                else if (value.Id.Equals(user.Id)) user = value;
            }
        }
        public static void Logout()
        {
            user = null;
        }
    }
}
