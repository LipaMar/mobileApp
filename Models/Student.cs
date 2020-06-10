using System;

namespace mobileApp.Models
{
    public class Student
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNr { get; set; }
        public string Profile { get; set; }
        public byte[] Picture { get; set; }

    }
}