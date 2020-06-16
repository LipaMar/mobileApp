using System;
using System.Collections.Generic;

namespace mobileApp.Web.Models
{
    public interface IItemRepository
    {
        void Add(Student item);
        void Update(Student item);
        Student Remove(string key);
        Student Get(string id);
        IEnumerable<Student> GetAll();
    }
}
